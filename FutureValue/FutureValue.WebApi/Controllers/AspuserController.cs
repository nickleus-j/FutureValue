using AutoMapper;
using FutureValue.Domain;
using FutureValue.Domain.Entities;
using FutureValue.Domain.Exceptions;
using FutureValue.Persistence.Shared;
using FutureValue.WebApi.DTO;
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
        public AspuserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // GET: api/<AspuserController>
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "No List", "Secret" };
        }

        // GET api/<AspuserController>/5
        [HttpGet("{name}")]
        [Authorize]
        public IActionResult Get(string name)
        {
            var result = unitOfWork.AspUserRepository.Find(name);
            AspUserDto dto = _mapper.Map<AspUserDto>(result);
            return Ok(dto);
        }

        // POST api/<AspuserController>
        [HttpPost]
        public void Post([FromBody] AspUserDto aspUser)
        {
            AspUser entity = _mapper.Map<AspUser>(aspUser);
            unitOfWork.AspUserRepository.Register(entity, aspUser.UnhashedPassword);
            
        }

        // PUT api/<AspuserController>/5
        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AspuserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
