using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P520233_EmmanuelPerez.Formularios
{
    public partial class FrmUsuariosGestion : Form
    {
        public FrmUsuariosGestion()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void FrmUsuariosGestion_Load(object sender, EventArgs e)
        {
            MdiParent = Globales.ObjetosGlobales.MiFormularioPrincipal;

            CargarListaUsuarios(); 

        }

        // Todas la funcionaliades espeificas y que se puedan reutilizar deben de ser encapsuladas 

        private void CargarListaUsuarios()
        {
            Logica.Models.Usuario miusuario = new Logica.Models.Usuario();

            DataTable lista = new DataTable();

            lista = miusuario.ListarActivos();

            DgvListaUsuarios.DataSource = lista;

        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {

            //lo primero que se hace es validar los datos minimos requeridos.
            //se hace con el fin de evitar que quede registros vacidos o sin datos.
           // tambien si un campo de base de datos no acepta valores nulos y e llama al insert dara un error.




        }
    }
}
