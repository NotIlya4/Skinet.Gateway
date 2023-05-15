namespace Api.Controllers.Views;

public class BadResponseView
{
    public string Title { get; }
    public string Detail { get; }

    public BadResponseView(string title, string detail)
    {
        Title = title;
        Detail = detail;
    }
}