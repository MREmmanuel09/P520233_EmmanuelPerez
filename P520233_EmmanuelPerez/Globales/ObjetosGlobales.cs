using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P520233_EmmanuelPerez.Globales
{
    public static class ObjetosGlobales
    {
        // defiir un objeto grobal para el formulario principal 
        public static Form MiFormularioPrincipal = new Formularios.FrmPrincipal();

        public static Formularios.FrmUsuariosGestion MiFormularioDeGestionDeUsuarios = new Formularios.FrmUsuariosGestion();
    }
}
