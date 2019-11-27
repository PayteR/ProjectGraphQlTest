using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjectGraphQlTest.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        static AppDbContext()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlServer("fake");
            using (var context = new AppDbContext(builder.Options))
            {
                DataModel = context.Model;
            }
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public static readonly IModel DataModel;
    }
}
