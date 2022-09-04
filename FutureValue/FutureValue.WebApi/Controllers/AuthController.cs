using AutoMapper;
using FutureValue.Persistence.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using System.Reflection;
using FutureValue.Persistence.EfImplementation.Shared;
using FutureValue.WebApi.DTO;
using FutureValue.Persistence.AspUsers;
using Microsoft.IdentityModel.Tokens;

namespace FutureValue.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration _configuration;
        public AuthController(IUnitOfWork _unitOfWork, IConfiguration configuration, IMapper _mapper)
        {
            unitOfWork= _unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            mapper = _mapper;
            _configuration = configuration ?? throw new ArgumentNullException(nameof(_configuration));
        }
        [HttpPost]
        //[Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto logInValues)
        {
            IAspUserRepository userRepo = unitOfWork.AspUserRepository;
            var user =  userRepo.Find(logInValues.UserName);
            if (user != null && unitOfWork.AspUserRepository.Find(logInValues.UserName, logInValues.UnhashedPassword)!=null)
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };


                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
