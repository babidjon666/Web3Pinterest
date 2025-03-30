using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.DataBase
{
    public class ApplicationDbContext : DbContext
    {   
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}