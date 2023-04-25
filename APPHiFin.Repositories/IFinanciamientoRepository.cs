using APPHiFin.Shared;

namespace APPHiFin.Repositories
{
    public interface IFinanciamientoRepository
    {
        Task<Financiamiento> GetFinanciamiento(int idModelo, double vPrecio, int vMeses, double vRentaAnt, int vResidual, double vTasaRein, double vInteresPrestamo);
    }
}
