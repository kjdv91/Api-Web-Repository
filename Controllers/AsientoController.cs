using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prueba.Data;
using Prueba.DataAccess;
using Prueba.Models;
using Prueba.SQLDataAcces.Asiento;
using System.Reflection;

namespace Prueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsientoController : ControllerBase
    {
        private IAsientoDAO _asientoDAO = new AsientoDAO();
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        readonly ILoggerManager Log = new LoggerManager(Logger);
   

        [HttpGet]
        public async Task<ActionResult<List<AsientoModel>>> GetAsientos()
        {
            try
            {
                var asientos = await _asientoDAO.GetAllAsientos();
                if (asientos.Count == 0)
                {
                    return NoContent();
                }

                return Ok(asientos);
            }
            catch 
            {

                return BadRequest(new { message = "Error retrieving asientos" });
            }
        }



        [HttpPost]
        public async Task<ActionResult<int>> InsertarAsiento(AsientoModel asiento)
        {
            var idAsiento = await _asientoDAO.insertAsiento(asiento);
            if (idAsiento is 0)

                return NotFound("Hero is not found");

            return Ok(idAsiento);



        }
    }
}
