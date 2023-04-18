using Microsoft.EntityFrameworkCore;

namespace cloudinaryImg.Models
{
    public class UserImgDbContext:DbContext
    {
        public UserImgDbContext(DbContextOptions<UserImgDbContext> options) : base(options)
        {
        }
        public DbSet<UserImage> UserImage { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=UserImg; User Id=hanoch; Password=1234;TrustServerCertificate= True");
        }


    }
}
