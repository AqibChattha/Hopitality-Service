using HospitalityService.Models.Domain;

namespace HospitalityService.Models.ViewModels
{
    public class UserHomeVM
    {
        public List<News> news { get; set; }
        public List<UserTask> tasks { get; set; }
        public List<HolidayRequest> holidays { get; set; }
    }
}
