using APPHiFin.Shared;

namespace APPHiFin.Client.Services
{
    public interface IFinanciamientoService
    {        
        Task<Financiamiento> GetFinanciamiento(int idModelo, double vPrecio, int vMeses,  double vRentaAnt, int vResidual, double vTasaRein, double vInteresPrestamo);        
    }
}
