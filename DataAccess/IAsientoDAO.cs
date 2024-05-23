using Prueba.Models;
using System.Data;

namespace Prueba.DataAccess
{
    public interface IAsientoDAO
    {
        Task<List<AsientoModel>> GetAllAsientos();
        Task<int> insertAsiento(AsientoModel asiento);
        void UpdateAsiento(AsientoModel asiento);
        List<AsientoModel> ObtenerAsientoId(int id);

    }
}
