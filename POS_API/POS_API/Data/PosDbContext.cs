using Microsoft.EntityFrameworkCore;

namespace POS_API.Data
{
    public class PosDbContext: DbContext
    {
        public PosDbContext(DbContextOptions<PosDbContext> options) : base(options) { }

    }
}
