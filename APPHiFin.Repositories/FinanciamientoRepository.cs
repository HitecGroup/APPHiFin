using APPHiFin.Shared;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;

namespace APPHiFin.Repositories
{
    public class FinanciamientoRepository : IFinanciamientoRepository
    {
        private readonly IDbConnection _dbConnection;
        public FinanciamientoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<Financiamiento> GetFinanciamiento(int idModelo, double vPrecio, int vMeses, double vRentaAnt, int vResidual, double vTasaRein, double vInteresPrestamo)
        {         
            var sql = @"dbo.HIFINv1";
            return await _dbConnection.QueryFirstOrDefaultAsync<Financiamiento>(sql, 
                param: new { @IDMODELO = idModelo, @PRECIOSINIVA = vPrecio, @PORCENTAJE = vRentaAnt, @MESES = vMeses, @RESIDUAL = vResidual, @TASAREINVERSION = vTasaRein, @INTERESPRESTAMO = vInteresPrestamo }, 
                commandType: CommandType.StoredProcedure);                       
        }
    } 
}