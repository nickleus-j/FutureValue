using AutoMapper;
using FutureValue.Domain;
using FutureValue.Domain.Entities;
using FutureValue.Domain.Exceptions;
using FutureValue.Persistence.Shared;
using FutureValue.WebApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FutureValue.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectionFormController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public ProjectionFormController(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            _mapper= mapper;
            _configuration= configuration;
        }
        // GET: api/<ProjectionFormController>
        [HttpGet]
        public IActionResult Get()
        {
            JwtUtils jUtil = new JwtUtils();
            var aspUser = jUtil.GetUserFromToken(unitOfWork, HttpContext, _configuration);
            var results = unitOfWork.ProjectionFormRepository.GetAll(aspUser!=null? aspUser.ID:0);
            IEnumerable<ProjectionFormDto> dto = _mapper.Map<IEnumerable<ProjectionFormDto>>(results);
            return Ok(dto);
        }

        // GET api/<ProjectionFormController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = unitOfWork.ProjectionFormRepository.Get(id);
            ProjectionFormDto dto = _mapper.Map<ProjectionFormDto>(result);
            dto.Projections = new ProjectionLister().GenerateProjections(result);
            return Ok(dto);
        }

        // POST api/<ProjectionFormController>
        [HttpPost]
        public IActionResult Post([FromBody] ProjectionFormDto dto)
        {
            ProjectionForm entity = _mapper.Map<ProjectionForm>(dto);
            
            try
            {
                unitOfWork.ProjectionFormRepository.Add(entity);
            }
            catch (InvalidBoundsException ibe)
            {
                return StatusCode(500,new ErrorDto("Invalid Bounds", ibe.Message));
            }
            unitOfWork.Save();
            return Ok(_mapper.Map<ProjectionFormDto>(entity));
        }

        // PUT api/<ProjectionFormController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] ProjectionFormDto dto)
        {
            ProjectionForm entity = _mapper.Map<ProjectionForm>(dto);
            try
            {
                unitOfWork.ProjectionFormRepository.Update(entity);
            }catch (InvalidBoundsException ibe)
            {
                return BadRequest(new ErrorDto("Invalid Bounds", ibe.Message));
            }
            
            unitOfWork.Save();
            return Ok(dto);
        }

        // DELETE api/<ProjectionFormController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool result=unitOfWork.ProjectionFormRepository.Delete(id);
            unitOfWork.Save();
            return Ok(result);
        }
    }
}
