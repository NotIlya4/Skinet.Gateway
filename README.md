# Gateway
This is a gateway based on YARP for my YetAnotherMarketplace. Its an entrypoint for my frontend app. For swagger docs go `localhost:5003/swagger/index.html`, there you can observe shrink docs of all services. For a full docs go to a docs of a specific service.

## Environment Variables
Gateway contains several environment variables that you can change to control its behavior:
- `AccountServiceUrls__BaseUrl` Defines base url of account service. That url will be used for authorization of several endpoints (Not for forwarding).
- `AccountServiceUrls__GetUserByJwtPath` Path that will be concated with account service's base url (Authorization, not forwarding). It must have `{jwt}` since this part will be replaced with real jwt token. Example: `users/jwt/{jwt}`.
- `ProductServiceForwardInfo__DestinationUrl` Product service base url for request forwarding.
- `BasketServiceForwardInfo__DestinationUrl` Basket service base url for request forwarding.
- `AccountServiceForwardInfo__DestinationUrl` Account service base url for request forwarding.
- `SeqUrl` Seq url for logging.
- `Serilog` Override serilog configuration.
