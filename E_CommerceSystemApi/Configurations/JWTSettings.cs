namespace E_CommerceSystemApi.Configurations
{
    public class JWTSettings
    {
        public string SigningKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInMinutes { get; set; }
    }
}
