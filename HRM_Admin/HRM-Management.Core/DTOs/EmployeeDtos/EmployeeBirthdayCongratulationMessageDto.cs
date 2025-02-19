namespace HRM_Management.Core.DTOs.EmployeeDtos
{
    public class EmployeeBirthdayCongratulationMessageDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string EmployeePersonalPhotoLink { get; set; }
        public string CongratulationMessagePrompt { get; set; }
    }
}