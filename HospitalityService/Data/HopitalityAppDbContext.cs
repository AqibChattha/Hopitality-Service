using HospitalityService.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace HospitalityService.Data
{
	public class HopitalityAppDbContext : DbContext
	{
		public HopitalityAppDbContext(DbContextOptions options) : base(options) 
		{
		}

		public DbSet<User> Users { get; set; }

		public DbSet<News> AllNews { get; set; }

        public DbSet<UserTask> UserTasks { get; set; }
        
        public DbSet<HolidayRequest> Holidays { get; set; }
    }
}
