using Microsoft.EntityFrameworkCore;

namespace MyProgressTracker.Models
{
    public class InMemoryDBContext : DbContext  
    {
        public DbSet<User> Users {  get; set; }

        public InMemoryDBContext(DbContextOptions<InMemoryDBContext> options) 
            : base(options) 
        { 

        }
        
    }
}
