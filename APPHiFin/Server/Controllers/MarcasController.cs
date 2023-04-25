using APPHiFin.Repositories;
using APPHiFin.Shared;
using Microsoft.AspNetCore.Mvc;

namespace APPHiFin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly IMarcaRepository _marcaRepository;

        public MarcasController(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Marca marca)        
        {
            if (marca == null)
                return BadRequest();
            if (string.IsNullOrEmpty(marca.Nombre))
                ModelState.AddModelError("Nombre", "Falta nombre de la marca");
            if (string.IsNullOrEmpty(marca.Puerto))
                ModelState.AddModelError("Puerto", "Falta puerto de llegada");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _marcaRepository.InsertMarca(marca);

            return NoContent();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Marca marca)
        {
            if (marca == null)
                return BadRequest();
            if (string.IsNullOrEmpty(marca.idPais.ToString()))
                ModelState.AddModelError("idPais", "Falta Id de País");
            if (string.IsNullOrEmpty(marca.Nombre))
                ModelState.AddModelError("Nombre", "Falta nombre de la marca");
            if (string.IsNullOrEmpty(marca.Puerto))
                ModelState.AddModelError("Puerto", "Falta puerto de llegada");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _marcaRepository.UpdateMarca(marca);

            return NoContent();

        }

        [HttpGet]
        public async Task<IEnumerable<Marca>> Get()
        { 
            return await _marcaRepository.GetAllMarca();
        }

        [HttpGet("{id}")]
        public async Task<Marca> Get(int id)
        {
            return await _marcaRepository.GetMarcaById(id);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        { 
            await _marcaRepository.DeleteMarca(id);
        }
    }
}
