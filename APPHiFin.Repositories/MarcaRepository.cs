using APPHiFin.Shared;
using Dapper;
using System.Data;

namespace APPHiFin.Repositories
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly IDbConnection _dbConnection;

        public MarcaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<bool> InsertMarca(Marca marca)
        {
            try
            {
                var sql = @"INSERT INTO Marcas (idPais,Nombre,Puerto) VALUES(@idPais,@Nombre,@Puerto)";
                var result = await _dbConnection.ExecuteAsync(sql, new
                {
                    marca.idPais,
                    marca.Nombre,
                    marca.Puerto
                });

                return result > 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> UpdateMarca(Marca marca)
        {
            try
            {
                var sql = @"UPDATE Marcas SET idPais = @idPais, Nombre = @Nombre, Puerto = @Puerto WHERE idMarca = @idMarca";
                var result = await _dbConnection.ExecuteAsync(sql, new
                {   marca.idMarca,
                    marca.idPais,
                    marca.Nombre,
                    marca.Puerto
                });

                return result > 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteMarca(int id)
        {
            var sql = @"DELETE FROM Marcas WHERE idMarca = @id";
            var result = await _dbConnection.ExecuteAsync(sql, new { id = id });
        }
        public async Task<IEnumerable<Marca>> GetAllMarca()
        {           
            var sql = @"SELECT idMarca,idPais,Nombre,Puerto FROM Marcas";
            return await _dbConnection.QueryAsync<Marca>(sql, new {});            
        }
        public async Task<Marca> GetMarcaById(int id)
        {          
            var sql = @"SELECT idMarca,idPais,Nombre,Puerto FROM Marcas WHERE idMarca = @id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Marca>(sql, new{ id = id });
        }
    }
}