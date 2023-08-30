namespace FutureValue.WebApi
{
    public class JwtConfig
    {
        public JwtConfig(string secret) { Secret = secret; }
        public string Secret { get; set; }
    }
}
