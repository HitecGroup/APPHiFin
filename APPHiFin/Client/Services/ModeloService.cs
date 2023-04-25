using APPHiFin.Shared;
using System.Net.Http;
using System.Net.Http.Json;

namespace APPHiFin.Client.Services
{
    public class ModeloService : IModeloService
    {
        private readonly HttpClient _httpClient;
        public ModeloService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Modelo>> GetAllModelos()
        {
           return await _httpClient.GetFromJsonAsync<IEnumerable<Modelo>>($"api/modelos");
        }

        public async Task<IEnumerable<Modelo>> GetAllModelosByMarca(int id)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Modelo>>($"api/modelos/{id}");            
        }
    }
}
