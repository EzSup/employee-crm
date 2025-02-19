namespace HRM_Management.Core.DTOs.AuthDtos
{
    public record AuthenticationDto
    {
        public AuthenticationDto(string authenticationKey)
        {
            AuthenticationKey = authenticationKey;
        }
        public string AuthenticationKey { get; set; }
    }
}
