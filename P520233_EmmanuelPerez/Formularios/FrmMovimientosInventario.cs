using CrystalDecisions.ReportAppServer;
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
    public partial class FrmMovimientosInventario : Form
    {

        public Logica.Models.Movimiento MiMovimientoLocal { get; set; }

        public DataTable DtListaDetalleProductos { get; set; }


        public FrmMovimientosInventario()
        {
            InitializeComponent();

            MiMovimientoLocal = new Logica.Models.Movimiento();

            DtListaDetalleProductos = new DataTable();

        }

        private void BtnAplicar_Click(object sender, EventArgs e)
        {
            //debemos validar que estén los datos minimos necesarios 
            if (ValidarMovimiento())
            {

                DialogResult respuesta = MessageBox.Show("¿Desea Continuar?", "???", MessageBoxButtons.YesNo);

                if (respuesta == DialogResult.Yes)
                {
                    //una vez que tenemos los requisitos completos, se procede a "dar forma"
                    //al objeto de movimiento local. 

                    //primero los atributos simples y compuestos del encabezado. 
                    //luego a asignación de los detalles 
                    MiMovimientoLocal.Fecha = DtpFecha.Value.Date;
                    MiMovimientoLocal.Anotaciones = TxtAnotaciones.Text.Trim();

                    MiMovimientoLocal.MiTipo.MovimientoTipoID = Convert.ToInt32(CboxTipo.SelectedValue);
                    //a nivel de funcionalidad solo necesitamos el FK o sea el ID del tipo, 
                    //la parte del texto no es necesario. 

                    MiMovimientoLocal.MiUsuario = Globales.ObjetosGlobales.MiUsuarioGlobal;

                    //llenar la lista de detalles en el objeto local a partir de las filas
                    //del datatable de detalles. 
                    TrasladarDetalles();

                    //ahora que tenemos todo listo, procedemos a agregar el movimiento 
                    if (MiMovimientoLocal.Agregar())
                    {

                        MessageBox.Show("El Movimiento se ha agregado correctamente",
                            ":)", MessageBoxButtons.OK);

                        //GENERAR REPORTE 
                        //1. crear un objeto de tipo documento 
                        CrystalDecisions.CrystalReports.Engine.ReportDocument MiReporte = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

                        //2. Crear objeto del reporte que se quiere usar 
                        MiReporte = new Reportes.RptMovimiento();

                        //3. Llamar a la función que extrae los datos de la base de datos 
                        MiReporte = MiMovimientoLocal.Imprimir(MiReporte);

                        //4. Dibujar el reporte en pantalla 
                        FrmVisualizadorReportes MiVisualizador = new FrmVisualizadorReportes();

                        MiVisualizador.CrvVisualizador.ReportSource = MiReporte;

                        MiVisualizador.Show();

                        //TODO: Limpiar formulario 

                    }

                }

            }

        }

        private void TrasladarDetalles()
        {

            foreach (DataRow item in DtListaDetalleProductos.Rows)
            {
                //en cada iteración creamos un nuevo objeto de MovimientoDetalle, que luego
                //será agregado a la lista de detalles del objeto local. 

                Logica.Models.MovimientoDetalle NuevoDetalle = new Logica.Models.MovimientoDetalle();

                NuevoDetalle.CantidadMovimiento = Convert.ToDecimal(item["CantidadMovimiento"]);

                NuevoDetalle.Costo = Convert.ToDecimal(item["Costo"]);

                NuevoDetalle.PrecioUnitario = Convert.ToDecimal(item["PrecioUnitario"]);

                NuevoDetalle.SubTotal = Convert.ToDecimal(item["SubTotal"]);

                NuevoDetalle.TotalIVA = Convert.ToDecimal(item["TotalIVA"]);

                //atributo compuesto simple
                NuevoDetalle.MiProducto.ProductoID = Convert.ToInt32(item["ProductoID"]);

                //agregar el detalle nuevo a la lista del objeto local 
                MiMovimientoLocal.Detalles.Add(NuevoDetalle);

            }

        }


        private bool ValidarMovimiento()
        {
            bool R = false;

            if (DtpFecha.Value.Date <= DateTime.Now.Date &&
                CboxTipo.SelectedIndex > -1 &&
                DtListaDetalleProductos.Rows.Count > 0)
            {
                R = true;
            }
            else
            {
                if (DtpFecha.Value.Date > DateTime.Now.Date)
                {
                    MessageBox.Show("La fecha del movimiento no puede " +
                        "ser superior a la fecha actual", "Error de Validación",
                        MessageBoxButtons.OK);
                    return false;
                }

                if (CboxTipo.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un tipo de movimiento",
                        "Error de Validación", MessageBoxButtons.OK);
                    return false;
                }

                if (DtListaDetalleProductos == null || DtListaDetalleProductos.Rows.Count == 0)
                {
                    MessageBox.Show("No se puede procesar un movimiento sin detalles",
                        "Error de Validación", MessageBoxButtons.OK);
                    return false;
                }

            }

            return R;
        }


        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMovimientosInventario_Load(object sender, EventArgs e)
        {
            MdiParent = Globales.ObjetosGlobales.MiFormularioPrincipal;

            CargarComboMovimientos();

            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            DtpFecha.Value = DateTime.Now.Date;
            CboxTipo.SelectedIndex = -1;
            TxtAnotaciones.Clear();

            //en este caso particular el datatable que alimenta el dgv
            //DEBE tener estructura, pero no datos inicialmente
            //considerando eso, llenaremos el datatable con el ESQUEMA
            //de la consulta del SP SPMovimientoCargarDetalle. 
            //esto permite tener el dt sin filas, pero con estructura, 
            //que permite agregar filas posteriormente. 

            DtListaDetalleProductos = MiMovimientoLocal.AsignarEsquemaDelDetalle();

            DgvListaDetalle.DataSource = DtListaDetalleProductos;

            //limpiamos los totales
            LblTotalCosto.Text = "0";
            LblTotalGranTotal.Text = "0";
            LblTotalImpuestos.Text = "0";
            LblTotalSubTotal.Text = "0";

        }



        private void CargarComboMovimientos()
        {
            Logica.Models.MovimientoTipo MiTipo = new Logica.Models.MovimientoTipo();

            DataTable dt = new DataTable();

            dt = MiTipo.Listar();

            if (dt != null && dt.Rows.Count > 0)
            {
                //una asegurado que el dt tiene valores, los "dibujo" en el combobox
                CboxTipo.ValueMember = "id";
                CboxTipo.DisplayMember = "Descripcion";

                CboxTipo.DataSource = dt;

                CboxTipo.SelectedIndex = -1;
            }

        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            //el formulario que muestra la lista de items, se debe mostrar en este
            //caso particular en formato de Dialogo, ya que queremos cortar
            //temporalmente el funcionamiento del form actual, hacer algo en 
            //el otro form y esperar una respuesta. 

            Form FormDetalleProducto = new Formularios.FrmMovimientosInventarioDetalleProducto();

            DialogResult resp = FormDetalleProducto.ShowDialog();

            if (resp == DialogResult.OK)
            {

                DgvListaDetalle.DataSource = DtListaDetalleProductos;

                Totalizar();

            }

        }

        private void Totalizar()
        {
            decimal TotalCosto = 0;
            decimal TotalSubtotal = 0;
            decimal TotalImpuestos = 0;
            decimal Total = 0;

            if (DtListaDetalleProductos != null && DtListaDetalleProductos.Rows.Count > 0)
            {
                foreach (DataRow item in DtListaDetalleProductos.Rows)
                {
                    decimal Cantidad = Convert.ToDecimal(item["CantidadMovimiento"]);
                    TotalCosto += Convert.ToDecimal(item["Costo"]) * Cantidad;
                    TotalSubtotal += Convert.ToDecimal(item["SubTotal"]) * Cantidad;
                    TotalImpuestos += Convert.ToDecimal(item["TotalIVA"]) * Cantidad;
                    Total += TotalSubtotal + TotalImpuestos;
                }
            }

            LblTotalCosto.Text = string.Format("{0:C2}", TotalCosto);
            LblTotalSubTotal.Text = string.Format("{0:C2}", TotalSubtotal);
            LblTotalImpuestos.Text = string.Format("{0:C2}", TotalImpuestos);
            LblTotalGranTotal.Text = string.Format("{0:C2}", Total);

        }


    }
}
