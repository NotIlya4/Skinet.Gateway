using Infrastructure.Auther;
using Microsoft.Extensions.DependencyInjection;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Transforms;

namespace Infrastructure.Yarp;

public class YarpConfigurator
{
    private readonly IServiceCollection _services;
    private readonly List<Type> _forwardInfos = new();
    private readonly List<Type> _requestTransformProviders = new();
    private readonly Dictionary<string, Func<RequestTransformContext, IAuther, ValueTask>> _requestTransformers = new();
    private readonly Dictionary<string, Func<ResponseTransformContext, ValueTask>> _responseTransformers = new();

    public YarpConfigurator(IServiceCollection services)
    {
        _services = services;
    }

    public void RegisterForwardInfo<T>() where T : class, IForwardInfo
    {
        _forwardInfos.Add(typeof(T));
        _services.AddTransient<T>();
    }

    public void RegisterForwardInfo(Type forwardInfoType)
    {
        _forwardInfos.Add(forwardInfoType);
        _services.AddTransient(forwardInfoType);
    }

    public void RegisterForwardInfosFromAssembly<TAssemblySource>()
    {
        List<Type> forwardInfoTypes = typeof(TAssemblySource).Assembly.GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IForwardInfo)) && t.IsClass).ToList();
        foreach (var type in forwardInfoTypes)
        {
            RegisterForwardInfo(type);
        }
    }

    public void RegisterRequestTransformer<T>() where T : class, IRequestTransformer
    {
        _requestTransformProviders.Add(typeof(T));
        _services.AddTransient<T>();
    }

    public void Apply(IReverseProxyBuilder builder)
    {
        ServiceProvider services = _services.BuildServiceProvider();

        ApplyForwarderInfo(builder, services);
        ApplyTransformers(builder, services);
    }

    private void ApplyForwarderInfo(IReverseProxyBuilder builder, ServiceProvider services)
    {
        List<IForwardInfo> providers = GetRequiredServices<IForwardInfo>(services, _forwardInfos);
        foreach (var provider in providers)
        {
            _requestTransformers[provider.Route.RouteId] = async (context, auther) =>
            {
                await provider.TransformRequest(context, auther);
            };
            _responseTransformers[provider.Route.RouteId] = async context =>
            {
                await provider.TransformResponse(context);
            };
        }
        List<RouteConfig> routes = providers.Select(p => p.Route).ToList();
        List<ClusterConfig> clusters = providers.Select(p => p.Cluster).ToList();
        
        builder.LoadFromMemory(routes, clusters);
    }

    private void ApplyTransformers(IReverseProxyBuilder builder, ServiceProvider services)
    {
        builder.AddTransforms(context =>
        {
            List<IRequestTransformer> providers = GetRequiredServices<IRequestTransformer>(services, _requestTransformProviders);
            foreach (var provider in providers)
            {
                context.AddRequestTransform(async context1 => await provider.Transform(context1));
            }

            foreach (var transformer in _requestTransformers)
            {
                if (context.Route.RouteId == transformer.Key)
                {
                    context.AddRequestTransform(async transformContext =>
                    {
                        await transformer.Value(transformContext,
                            context.Services.CreateScope().ServiceProvider.GetRequiredService<IAuther>());
                    });
                }
            }

            foreach (var responseTransformer in _responseTransformers)
            {
                if (context.Route.RouteId == responseTransformer.Key)
                {
                    context.AddResponseTransform(async transformContext =>
                    {
                        await responseTransformer.Value(transformContext);
                    });
                }
            }
        });
    }

    private List<T> GetRequiredServices<T>(ServiceProvider services, List<Type> types) where T : class
    {
        return types.Select(t => (T)services.GetRequiredService(t)).ToList();
    }
}