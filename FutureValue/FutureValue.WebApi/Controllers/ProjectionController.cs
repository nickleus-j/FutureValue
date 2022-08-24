using AutoMapper;
using FutureValue.Domain;
using FutureValue.Persistence.Shared;
using FutureValue.WebApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FutureValue.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectionController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public ProjectionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // GET: api/<ProjectionController>
        [HttpGet]
        [HttpPost]
        public IActionResult Get([FromBody] ProjectionFormDto dto)
        {
            ProjectionForm form = _mapper.Map<ProjectionForm>(dto);
            return Ok(new ProjectionLister().GenerateProjections(form));
        }

        // GET api/<ProjectionController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ProjectionForm form =unitOfWork.ProjectionFormRepository.Get(id);
            if(form== null)
            {
                return NotFound();
            }
            return Ok(new ProjectionLister().GenerateProjections(form));
        }

        
    }
}
