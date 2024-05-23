using System.ComponentModel;
using System.Data;

namespace Prueba.Models
{
    public class AsientoModel
    {
        public int CodigoCabecera { get; set; }
        [DisplayName("Unidad Administrativa")]
        public string CodigoUnidadAdministreativa { get; set; }
        [DisplayName("Fecha Contable")]
        public DateTime FechaContable { get; set; }
        [DisplayName("Legado")]
        public string Legado { get; set; }
        [DisplayName("Estado")]
        public string Estado { get; set; }


         
        public static AsientoModel AsientoDataReader(IDataRecord dr)
        {
            AsientoModel asiento = new AsientoModel();
            //asiento.CodigoCabecera = int.Parse(dr["cam_idCampo"].ToString()); 
            asiento.CodigoCabecera = int.Parse(dr["CodigoCabecera"].ToString());
            asiento.CodigoUnidadAdministreativa = dr["CodUnidadAdm"].ToString();
            asiento.Estado = dr["Estado"].ToString();
            asiento.Legado = dr["LegadoC"].ToString();
            asiento.FechaContable = string.IsNullOrEmpty(dr["FechaContable"].ToString()) ? DateTime.Now : DateTime.Parse(dr["FechaContable"].ToString());


            return asiento;
        }

    }

    

}
