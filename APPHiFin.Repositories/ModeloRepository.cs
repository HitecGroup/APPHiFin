using APPHiFin.Shared;
using Dapper;
using System.Data;

namespace APPHiFin.Repositories
{
    public class ModeloRepository : IModeloRepository
    {
        private readonly IDbConnection _dbConnection;
        public ModeloRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<Modelo>> GetAllModelos()
        {           
            var sql = @"SELECT idModelo,idMarca,origen,TipoMaquina,ModeloMaquina,InternalSAPCode,ModeloSAP,Extra,HQ40,HQ20,FR40,FR20,OT40,OT20 FROM Modelos";
            return await _dbConnection.QueryAsync<Modelo>(sql, new { });            
        }
        public async Task<IEnumerable<Modelo>> GetAllModelosByMarca(int id)
        {
            var sql = @"SELECT idModelo,idMarca,origen,TipoMaquina,ModeloMaquina,InternalSAPCode,ModeloSAP,Extra,HQ40,HQ20,FR40,FR20,OT40,OT20 FROM Modelos WHERE idMarca = @id";
            return await _dbConnection.QueryAsync<Modelo>(sql, new { id = id });
        }
    } 
}