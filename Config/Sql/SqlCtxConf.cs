using Microsoft.EntityFrameworkCore;

namespace UserAccountsApi.ConfigNS.SqlNS;

public class SqlDbCtx : DbContext
{
  public SqlDbCtx(DbContextOptions<SqlDbCtx> options) : base(options)
  {
  }
}