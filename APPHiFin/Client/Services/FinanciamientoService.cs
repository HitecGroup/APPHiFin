using APPHiFin.Shared;
using System.Net.Http;
using System.Net.Http.Json;

namespace APPHiFin.Client.Services
{
    public class FinanciamientoService : IFinanciamientoService
    {
        private readonly HttpClient _httpClient;
        public FinanciamientoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }        
        public async Task<Financiamiento> GetFinanciamiento(int idModelo, double vPrecio, int vMeses, double vRentaAnt, int vResidual, double vTasaRein, double vInteresPrestamo)
        {
          return await _httpClient.GetFromJsonAsync<Financiamiento>($"api/financiamiento/{idModelo}/{vPrecio}/{vMeses}/{vRentaAnt}/{vResidual}/{vTasaRein}/{vInteresPrestamo}");
        }
    }
}
