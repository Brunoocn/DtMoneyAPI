using DtMoneyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DtMoneyAPI.Data
{
    public class SqlContext: DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> contextOptions): base(contextOptions)
        {
            
        }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
