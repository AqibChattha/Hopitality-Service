namespace HospitalityService.Models.ViewModels
{
    public class UserProfileVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImgURL { get; set; }
        public string UserType { get; set; }
    }
}
