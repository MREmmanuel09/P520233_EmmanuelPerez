using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    public class UsuarioRol
    {
        // primero digitamos las propiedades de la clase 
        public int UsuarioRolID { get; set; }

        // esta forma de escribir esta propiedad es autoimplementada pero se pierde funciones en get y set.
        public String RolName { get; set; }

        // Luego de , escribir las propiedades se digita las funciones.
        public DataTable Listar()
        { 
           DataTable R = new DataTable();

            // progra funsional para realizar el listar 

            return R;
        }
    }
}
 