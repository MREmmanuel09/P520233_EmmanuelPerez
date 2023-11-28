using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    public class MovimientoTipo
    {
       public int MovimientoTipoID { get; set; }

        public int MovimientoTipoDescripcion { get; set; }

        public DataTable Listar()
        { 
            DataTable R = new DataTable();

            return R;
        }
    }
}
