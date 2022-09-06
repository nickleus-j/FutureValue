using FutureValue.Domain.Entities;
using FutureValue.Persistence.Shared;
using FutureValue.WebApi.DTO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FutureValue.WebApi
{
    public class JwtUtils
    {
       
        public string GenerateToken(AspUserDto user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.ID.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(new byte[1]), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string getJwtTokenRequest(HttpContext ctx)
        {
            try
            {
                return ctx.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            }catch(NullReferenceException nre)
            {
                return String.Empty;
            }
        }
        public AspUser? GetUserFromToken(IUnitOfWork unitOfWork,HttpContext ctx, IConfiguration _configuration)
        {
            string token = getJwtTokenRequest(ctx);
            int? id= ValidateToken(token, _configuration);
            if (id == null)
            {
                return null;
            }
            return unitOfWork.AspUserRepository.Get(id.Value);
        }
        public int? ValidateToken(string token, IConfiguration _configuration)
        {
            if (token == null)
                return null;
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

           
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = authSigningKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
