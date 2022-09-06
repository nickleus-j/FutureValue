using AutoMapper;
using FutureValue.Domain;
using FutureValue.Domain.Entities;
using FutureValue.Domain.Exceptions;
using FutureValue.Persistence.Shared;
using FutureValue.WebApi.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FutureValue.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AspuserController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AspuserController(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }
        // GET: api/<AspuserController>
        [HttpGet]
        public IActionResult Get([FromBody] LoginDto logInValues)
        {
            if (logInValues.UnhashedPassword == null)
            {
                return BadRequest(new ArgumentNullException("Null password"));
            }
            var result = unitOfWork.AspUserRepository.Find(logInValues.UserName,logInValues.UnhashedPassword);
            AspUserDto dto = _mapper.Map<AspUserDto>(result);
             return result!=null?Ok(dto):BadRequest(new InvalidOperationException("Invalid Credentials"));
        }
        [HttpPost]
        [Route("login")]
        public IActionResult login([FromBody] LoginDto logInValues)
        {
            if (logInValues.UnhashedPassword == null)
            {
                return BadRequest(new ArgumentNullException("Null password"));
            }
            var result = unitOfWork.AspUserRepository.Find(logInValues.UserName, logInValues.UnhashedPassword);
            AspUserDto dto = _mapper.Map<AspUserDto>(result);
            return result!=null?Ok(dto):BadRequest(new InvalidOperationException("Invalid Credentials"));
        }
        // GET api/<AspuserController>/5
        [HttpGet("{name}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get(string name)
        {
            JwtUtils jUtil = new JwtUtils();
            var result = jUtil.GetUserFromToken(unitOfWork, HttpContext, _configuration);

            if (result == null)
            {
                return NotFound(name);
            }
            AspUserDto dto = _mapper.Map<AspUserDto>(result);
           
            if (name.Trim().ToLower() == result.UserName.Trim().ToLower())
            {
                return Ok(dto);
            }
            return Unauthorized(name);
        }

        // POST api/<AspuserController>
        [HttpPost]
        public void Post([FromBody] AspUserDto aspUser)
        {
            AspUser entity = _mapper.Map<AspUser>(aspUser);
            unitOfWork.AspUserRepository.Register(entity, aspUser.UnhashedPassword);
            unitOfWork.Save();
        }

        // PUT api/<AspuserController>/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public void Put([FromBody] AspUserDto aspUser)
        {
            AspUser entity = _mapper.Map<AspUser>(aspUser);
            unitOfWork.AspUserRepository.Update(entity);
            unitOfWork.Save();
        }

        // DELETE api/<AspuserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
