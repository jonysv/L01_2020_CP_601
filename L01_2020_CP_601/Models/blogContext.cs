using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using L01_2020_CP_601.Models;

namespace L01_2020_CP_601.Models
{
    public class blogContext:DbContext
    {
        public blogContext(DbContextOptions<blogContext> options) : base(options)
        {

        }

        public DbSet<usuarios> usuarios { get; set; }

        public DbSet<publicaciones> publicaciones { get; set; }

        public DbSet<comentarios> comentarios { get; set; } 

    }
}
