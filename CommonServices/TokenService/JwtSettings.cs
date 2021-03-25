namespace CommonServices.TokenService
{
    public class JwtSettings
    {
        public string Secret { get; set; } = "5290C2B2-1C40-47D2-8C25-346ED51C4DA6";
        public int LifetimeInSeconds { get; set; } = 24;
    }
}