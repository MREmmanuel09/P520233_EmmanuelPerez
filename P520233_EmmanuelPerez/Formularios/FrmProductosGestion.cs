using Logica;
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
    public partial class FrmProductosGestion : Form
    {
        public FrmProductosGestion()
        {
            InitializeComponent();
        }

        private void FrmProductosGestion_Load(object sender, EventArgs e)
        {
            CargarListaProductos();
        }

        private void CargarListaProductos()
        {
            // Lógica para cargar productos desde la base de datos.
            DataTable dt = ObtenerProductosDesdeBaseDeDatos();

            // Asigna la lista de productos al DataGridView.
            dataGridViewProductos.DataSource = dt;

            // Puedes ajustar el ancho de las columnas según tus necesidades.
            AjustarAnchoColumnas();
        }

        private DataTable ObtenerProductosDesdeBaseDeDatos()
        {
            DataTable dataTable = new DataTable();

            // Crea una instancia de la clase Conexion
            Conexion conexion = new Conexion();

            try
            {
                // Nombre del procedimiento almacenado que obtiene los productos
                string nombreSP = "ObtenerProductos";

                // Ejecuta el procedimiento almacenado y obtiene los resultados
                dataTable = conexion.EjecutarSelect(nombreSP);
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción que pueda ocurrir durante la conexión o la ejecución del SP
                MessageBox.Show($"Error al obtener los productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataTable;
        }

        private void AjustarAnchoColumnas()
        {
            // Puedes ajustar el ancho de las columnas según tus preferencias.
            dataGridViewProductos.Columns["ProductoID"].Width = 50;
            dataGridViewProductos.Columns["CodigoBarras"].Width = 120;
            dataGridViewProductos.Columns["NombreProducto"].Width = 200;
            dataGridViewProductos.Columns["Costo"].Width = 80;
            dataGridViewProductos.Columns["Utilidad"].Width = 80;
            dataGridViewProductos.Columns["SubTotal"].Width = 80;
            dataGridViewProductos.Columns["TasaImpuesto"].Width = 80;
            dataGridViewProductos.Columns["PrecioUnitario"].Width = 80;
            dataGridViewProductos.Columns["CantidadStock"].Width = 80;
            dataGridViewProductos.Columns["Activo"].Width = 50;
            dataGridViewProductos.Columns["ProductoCategoriaID"].Width = 80;
        }

        private void dataGridViewProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

