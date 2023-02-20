using Microsoft.AspNetCore.Mvc;
using Realta.Domain.Base;
using Realta.Services.Abstraction;
using System.Runtime.InteropServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilitiesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private IRepositoryManager _repositoryManager;

        public FacilitiesController(ILoggerManager logger, IRepositoryManager repositoryManager)
        {
            _logger = logger;
            _repositoryManager = repositoryManager;
        }


        // GET: api/<FacilitiesController>
        [HttpGet("{hotelId}/facilities")]
        public async Task<IActionResult> GetAllFacilitiesAsync(int hotelId)
        {
            var facilities = await _repositoryManager.FacilitiesRepository.FindAllFacilitiesAsync(hotelId);
            return Ok(facilities.ToList());
        }

        // GET api/<FacilitiesController>/5
        [HttpGet("{id}")]
        public string GetFacilitiesById(int id)
        {
            return "value";
        }

        // POST api/<FacilitiesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FacilitiesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FacilitiesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
