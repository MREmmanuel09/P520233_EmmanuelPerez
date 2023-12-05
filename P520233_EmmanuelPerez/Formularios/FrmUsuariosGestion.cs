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
        // objeto local de tipo usuario 

        private Logica.Models.Usuario MiUsuarioLocal{ get; set; }
        public FrmUsuariosGestion()
        {
            InitializeComponent();

            MiUsuarioLocal = new Logica.Models.Usuario();
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void FrmUsuariosGestion_Load(object sender, EventArgs e)
        {
            MdiParent = Globales.ObjetosGlobales.MiFormularioPrincipal;

            CargarComboRolesDeUsuario();

            CargarListaUsuarios(); 

        }

        private void CargarComboRolesDeUsuario()
        {
            Logica.Models.UsuarioRol MiRol = new Logica.Models.UsuarioRol();

            DataTable dt = new DataTable();

            dt = MiRol.Listar();

            if (dt != null && dt.Rows.Count > 0 )
            {
                // una vez asegurado que el dt tenga vcalores, los "dibujo" en el combobox

                CboxUsuarioTipoRol.ValueMember = "id";
                CboxUsuarioTipoRol.DisplayMember = "Descripcion";

                CboxUsuarioTipoRol.DataSource = dt;
                CboxUsuarioTipoRol.SelectedIndex = -1; 
            }

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
            //tomando en consideracion el caso de uso expandido del usuario en el diagrama, validar que no haya problemas de redudancia como la cedula y el correo

            // si las dos validaciones son degativas se procede agregar el usuario 

            // se usara un objeto de tipo usuari, que sera el que daremos forma para luego usar las funsiones el CRUD 

            MiUsuarioLocal = new Logica.Models.Usuario();
            MiUsuarioLocal.Cedula = TxtUsuarioCedula.Text.Trim();
            MiUsuarioLocal.Nombre = TxtUsuarioNombre.Text.Trim();   
            MiUsuarioLocal.Correo = TxtUsuarioCorreo.Text.Trim();
            MiUsuarioLocal.Telefono = TxtUsuarioTelefono.Text.Trim();
            // con el combo de rol  hay que extarer el valuemember seleccionado 
            MiUsuarioLocal.MiUsuarioRol.UsuarioRolID = Convert.ToInt32(CboxUsuarioTipoRol.SelectedValue);
           
            MiUsuarioLocal.Contrasennia = TxtUsuarioContrasennia.Text.Trim();   
            MiUsuarioLocal.Direccion = TxtUsuarioDireccion.Text.Trim();

            bool CedulaOK = MiUsuarioLocal.ConsultarPorCedula(MiUsuarioLocal.Cedula);    
        
            bool CorreoOK = MiUsuarioLocal.ConsultarPorCorreo(MiUsuarioLocal.Correo);

            if (CedulaOK == false && CedulaOK == false)
            {
                // TODO
                // se solicita confirmacion por parte del usuario 
                string Pregunta = string.Format("Esta seguro de agregar al usuario{0}?",MiUsuarioLocal );

                DialogResult respuesta = MessageBox.Show(Pregunta, "???", MessageBoxButtons.YesNo);


                if (respuesta == DialogResult.Yes)

                {
                    bool ok = MiUsuarioLocal.Agregar();




                    if (ok)

                    {
                        MessageBox.Show("Usuario ingresado correctamente!", ":)", MessageBoxButtons.OK);

                        LimpiarForm();
                        CargarListaUsuarios();

                    }
                    else
                    {
                        MessageBox.Show("Usuario no se agrego  correctamente!", ":)", MessageBoxButtons.OK);
                    }
                }
            }

        }




        private void LimpiarForm()
        { 
            TxtUsuarioCodigo.Clear();
            TxtUsuarioCedula.Clear();
            TxtUsuarioNombre.Clear();
            TxtUsuarioCorreo.Clear();
            TxtUsuarioTelefono.Clear();
            TxtUsuarioContrasennia.Clear();
            TxtUsuarioDireccion.Clear();


            CboxUsuarioTipoRol.SelectedIndex = -1;

            CbUsuarioActivo.Checked = false;    



        }






        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void CboxUsuarioTipoRol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
