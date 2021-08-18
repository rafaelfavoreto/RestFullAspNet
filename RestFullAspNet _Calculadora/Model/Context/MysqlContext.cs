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
        public DbSet<BooksVO> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
