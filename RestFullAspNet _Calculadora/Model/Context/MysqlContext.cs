using Microsoft.EntityFrameworkCore;
using RestFullAspNet.Model;

namespace RestFullAspNet.Model.Context
{
    public class MysqlContext : DbContext
    {
        public  MysqlContext()
        {

        }
        public MysqlContext(DbContextOptions<MysqlContext> options) : base(options){}

        public DbSet<Person> Persons { get; set; }
        public DbSet<Books> Books { get; set; }
    }
}
