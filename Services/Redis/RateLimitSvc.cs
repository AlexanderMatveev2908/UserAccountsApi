
using StackExchange.Redis;
using UserAccountsApi.ConfigNS.RedisNS;

namespace UserAccountsApi.Services.RedisNS;

public static class RateLimitSvc
{
  public static async Task Limit(HttpContext ctx)
  {
    IDatabase db =
                RedisConf.Connection.GetDatabase();

  }
}