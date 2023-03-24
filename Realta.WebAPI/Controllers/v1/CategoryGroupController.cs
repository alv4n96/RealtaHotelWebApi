using Microsoft.AspNetCore.Mvc;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryGroupController : ControllerBase
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public CategoryGroupController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }


        // GET: api/<Category_GroupController>
        [HttpGet]
        public async Task<IActionResult> FindAllCategory_GroupAsync()
        {
            try
            {
                var categoryGroups = await _repositoryManager.CategoryGroupRepository.FindAllCategoryGroupAsync();

                var result = categoryGroups.Select(cg => new CategoryGroupDto
                {
                    CagroId = cg.CagroId,
                    CagroName = cg.CagroName,
                    CagroDescription= cg.CagroDescription,
                    CagroType = cg.CagroType,
                    CagroIcon = cg.CagroIcon,
                    CagroIconUrl = cg.CagroIconUrl
                });

                return Ok(result);
            }
            catch (Exception)
            {

                _logger.LogError($"Error : {nameof(FindAllCategory_GroupAsync)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
