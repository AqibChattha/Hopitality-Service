using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalityService.Models.Domain
{
	public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public DateTime? Date { get; set; }
		public Guid UserId { get; set; }
	}
}
