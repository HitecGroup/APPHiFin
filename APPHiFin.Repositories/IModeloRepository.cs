using APPHiFin.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPHiFin.Repositories
{
    public interface IModeloRepository
    {
        Task<IEnumerable<Modelo>> GetAllModelos();
        Task<IEnumerable<Modelo>> GetAllModelosByMarca(int id);
    }
}
