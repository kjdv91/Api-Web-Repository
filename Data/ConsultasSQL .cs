using log4net;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml;

namespace Prueba.Data
{
    public class ConsultasSQL : IDisposable
    {
        private SqlConnection laConexion;
        public SqlCommand Comando { get; set; }

        public ConsultasSQL()
        {
            laConexion = Base.ObtenerConexionSQL();
            Comando = new SqlCommand();
        }

        public void abrirConexion()
        {
            if (laConexion.State != ConnectionState.Open)
                laConexion.Open();
        }

        public void cerrarConexion()
        {
            if (laConexion.State != ConnectionState.Closed)
                laConexion.Close();
        }

        public IDataReader ejecutaReader()
        {
            IDataReader retValue;
            abrirConexion();
            Comando.Connection = laConexion;
            retValue = Comando.ExecuteReader(CommandBehavior.CloseConnection);

            return retValue;
        }

        public int ejecutaQuery()
        {
            int retValue;

            try
            {
                abrirConexion();
                Comando.Connection = laConexion;
                retValue = Comando.ExecuteNonQuery();
                cerrarConexion();
            }
            catch (Exception)
            {
                retValue = -1;
                throw;
            }

            return retValue;
        }

        public void Dispose()
        {
            cerrarConexion();
            laConexion = null;
        }

        public DataTable ejecutaDataTable()
        {
            DataTable dt = new DataTable();
            abrirConexion();
            Comando.Connection = laConexion;
            SqlDataAdapter ad = new SqlDataAdapter(Comando);
            ad.Fill(dt);
            cerrarConexion();
            return dt;
        }
    }
    public interface ILoggerManager
    {
        void InfoFormat(string message);
        void Error(string message, Exception e);
        void InfoFormat(string message, object p);
    }
    public class LoggerManager : ILoggerManager
    {
        private readonly ILog _logger;

        public LoggerManager(ILog logger)
        {
            _logger = logger;
            try
            {
                /*XmlDocument log4netConfig = new XmlDocument();
                using (var fs = System.IO.File.OpenRead("log4net.config"))
                {
                    log4netConfig.Load(fs);
                    var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
                    XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
                }*/
                XmlDocument log4netConfig = new XmlDocument();
                log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));
                //_logger.Info("Log Inicializando el Sistema Reclamos-BBolivariano");
            }
            catch (Exception ex)
            {
                _logger.Error("Error", ex);
            }
        }
        public void InfoFormat(string message)
        {
            _logger.InfoFormat(message);
        }

        public void Error(string message, Exception e)
        {
            _logger.Error(message, e);
        }

        public void InfoFormat(string message, object p)
        {
            _logger.InfoFormat(message, p);
        }
    }
}
