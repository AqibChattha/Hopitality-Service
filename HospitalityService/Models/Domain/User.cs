using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace HospitalityService.Models.Domain
{
	public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public string? ImgURL { get; set; }
		public string? UserType { get; set; }

		public virtual ICollection<HolidayRequest> HolidayRequests { get; set; }
		public virtual ICollection<UserTask> UserTasks { get; set; }
		public virtual ICollection<News> News { get; set; }
	}
}
