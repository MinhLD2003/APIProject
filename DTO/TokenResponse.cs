namespace Project.API.DTO
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken {  get; set; }
        public string UserRole { get; set; }
    }
}
