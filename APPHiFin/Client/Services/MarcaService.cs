using APPHiFin.Shared;
using System.Net.Http;
using System.Net.Http.Json;

namespace APPHiFin.Client.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly HttpClient _httpClient;
        public MarcaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task DeleteMarca(int id)
        {
            await _httpClient.DeleteAsync($"api/marcas/{id}");
        }

        public async Task<IEnumerable<Marca>> GetAllMarca()
        {
           return await _httpClient.GetFromJsonAsync<IEnumerable<Marca>>($"api/marcas");
        }

        public async Task<Marca> GetMarcaById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Marca>($"api/marcas/{id}");
        }

        public async Task SaveMarca(Marca marca)
        {
            if (marca.idMarca == 0)
                // insertar
                await _httpClient.PostAsJsonAsync<Marca>($"api/marcas", marca);
            else
                // editar
               await _httpClient.PutAsJsonAsync<Marca>($"api/marcas/{marca.idMarca}", marca);           
        }
    }
}
