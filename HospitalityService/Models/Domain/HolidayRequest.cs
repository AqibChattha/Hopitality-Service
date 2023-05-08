using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace HospitalityService.Models.Domain
{
    public class HolidayRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public bool Status { get; set; }
        public bool IsAccepted { get; set; }
		[ForeignKey("Users")]
		public Guid UserId { get; set; }
	}
}
