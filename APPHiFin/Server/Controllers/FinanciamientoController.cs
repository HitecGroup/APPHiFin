using APPHiFin.Repositories;
using APPHiFin.Shared;
using Microsoft.AspNetCore.Mvc;

namespace APPHiFin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanciamientoController : ControllerBase
    {
        private readonly IFinanciamientoRepository _financiamientoRepository;

        public FinanciamientoController(IFinanciamientoRepository financiamientoRepository)
        {
            _financiamientoRepository = financiamientoRepository;
        }


        [HttpGet("{idModelo}/{vPrecio}/{vMeses}/{vRentaAnt}/{vResidual}/{vTasaRein}/{vInteresPrestamo}")]
        public async Task<Financiamiento> Get(int idModelo, double vPrecio, int vMeses, double vRentaAnt, int vResidual, double vTasaRein, double vInteresPrestamo)
        {
            return await _financiamientoRepository.GetFinanciamiento(idModelo, vPrecio, vMeses, vRentaAnt, vResidual, vTasaRein, vInteresPrestamo);
        }

    }
}
