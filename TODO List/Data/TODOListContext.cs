using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TODO_List.Models;

namespace TODO_List.Data
{
    public class TODOListContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public TODOListContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to mysql with connection string from app settings
            var connectionString = Configuration.GetConnectionString("TODOListContext");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }



        //public TODOListContext (DbContextOptions<TODOListContext> options)
        //    : base(options)
        //{
        //}

        public DbSet<TODO_List.Models.TODOList> TODOList { get; set; } = default!;
    }
}
