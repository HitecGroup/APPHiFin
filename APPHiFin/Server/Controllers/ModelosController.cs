using APPHiFin.Repositories;
using APPHiFin.Shared;
using Microsoft.AspNetCore.Mvc;

namespace APPHiFin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelosController : ControllerBase
    {
        private readonly IModeloRepository _modeloRepository;

        public ModelosController(IModeloRepository modeloRepository)
        {
            _modeloRepository = modeloRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<Modelo>> Get()
        {
            return await _modeloRepository.GetAllModelos();
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<Modelo>> Get(int id)
        {
            return await _modeloRepository.GetAllModelosByMarca(id);
        }
    }
}
