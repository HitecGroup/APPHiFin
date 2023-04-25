using APPHiFin.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPHiFin.Repositories
{
    public interface IMarcaRepository
    {
        Task<bool> InsertMarca(Marca marca);
        Task<bool> UpdateMarca(Marca marca);
        Task DeleteMarca(int id);
        Task<IEnumerable<Marca>> GetAllMarca();
        Task<Marca> GetMarcaById(int id);
    }
}
