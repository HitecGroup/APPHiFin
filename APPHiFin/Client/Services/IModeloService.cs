using APPHiFin.Shared;

namespace APPHiFin.Client.Services
{
    public interface IModeloService
    {
        Task<IEnumerable<Modelo>> GetAllModelos();
        Task<IEnumerable<Modelo>> GetAllModelosByMarca(int id);
    }
}
