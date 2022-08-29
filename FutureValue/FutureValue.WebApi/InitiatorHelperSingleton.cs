using System;
namespace FutureValue.WebApi
{
    /// <summary>
    /// Helps the Program startup with configuration Values
    /// </summary>
    public class InitiatorHelperSingleton
    {
        private static InitiatorHelperSingleton _instance { get; set; }
        public static InitiatorHelperSingleton Instance { get { 
                if( _instance == null)
                {
                    _instance = new InitiatorHelperSingleton();
                }
                return _instance;
            } }
        private InitiatorHelperSingleton() { }
        public string GetConnectionString(WebApplicationBuilder builder)
        {
            string? fvDbConnectionString = Environment.GetEnvironmentVariable("FvDb");
            string defaultConnectionString = builder.Configuration.GetConnectionString("defaultDb");
            return !String.IsNullOrEmpty(fvDbConnectionString) ? fvDbConnectionString : defaultConnectionString;
        }
        public void AllowCorsFromConfig(WebApplicationBuilder builder, WebApplication app)
        {
            string? envOrigins = Environment.GetEnvironmentVariable("AllowedOrigins");
            string defaultOrigins = builder.Configuration.GetSection("AllowedOrigins").Value;
            
            if (String.IsNullOrEmpty(defaultOrigins))
            {
                defaultOrigins = String.Empty;
            }
            
            string[] hosts = String.IsNullOrEmpty(envOrigins) ? defaultOrigins.Split(",") : envOrigins.Split(",");
            for(int i = 0; i < hosts.Length; i++)
            {
                app.UseCors(x => x.AllowAnyHeader()
              .AllowAnyMethod()
              .SetIsOriginAllowed(origin => new Uri(origin).Host == hosts[i]));
            }
        }
    }
}
