using GrpcService1.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcService1.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UserModel> UserModel => Set<UserModel>();
    }
}
