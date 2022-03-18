using Microsoft.EntityFrameworkCore;

namespace SpringBoard.Data
{
    public class DatabContext: DbContext
    {
        public DatabContext() : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DB_SpringBoard");
        }

    }
}