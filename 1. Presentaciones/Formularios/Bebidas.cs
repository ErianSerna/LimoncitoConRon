using LimoncitoConRon._2._Servicios.lib_servicios;
using LimoncitoConRon._3.Comunes.lib_entidades.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace LimoncitoConRon._1._Presentaciones.Formularios
{
    public partial class Bebidas: Form
    {
        //Objeto de ServicioBebidas para usar los metodos
        private ServicioBebidas _serviciobebidas;
        private ServicioTipoBebidas _serviciotipobebidas;
        private ServicioDescuentos _serviciodescuentos;

        public Bebidas(ServicioBebidas servicioBebidas, ServicioTipoBebidas serviciotipobebidas, ServicioDescuentos serviciodescuentos)
        {
            InitializeComponent();
            this._serviciobebidas = servicioBebidas;
            this._serviciotipobebidas = serviciotipobebidas;
            this._serviciodescuentos = serviciodescuentos;
            llenarCBTipoBebidas(); // Inicializa el combobox de tipobebidas
            llenarCBDescuentos(); // Inicializa el combobox de descuentos
        }

        public DataSet listar()
        {
            return new DataSet();
        }

        // Metodo Bebidas_Load es como el document.addEventListener('DOMContentLoaded', function () ...) del javascript
        private void Bebidas_Load(object sender, EventArgs e) { 
  
            //Los métodos que se llamen aqui, se van a ejecutar cuando cargué la ventanita
            CambiarLabels();
            _serviciobebidas.Listar(dgvBebidas); // Para llenar la tabla de bebidas
            //CargarDatos();
            //Aca hay que poner la logica para cargar los datos de las bebidas (llenar el DataGridView [la tabla])
        }

        // Metodo para asignar texto a cada uno de los elementos en el formulario
        private void CambiarLabels()
        {
            // Cambia los labels de la vista
            lblNombre.Text = "Nombre:";
            lblPrecio.Text = "Precio:";
            lblTipo.Text = "Tipo:";
            lblBuscar.Text = "Buscar:";
            lblNombreAdmin.Text = $"Bienvenido {lblNombreAdmin}"; //Hay que traerlo de la bd
        }

        // Metodo para llenar el comboBox de tipo de bebida
        private void llenarCBTipoBebidas()
        {
            comboTipoBebida.Items.Clear(); // Limpiar todos los elementos del combobox
            List<TipoBebidasModel> lista = _serviciotipobebidas.Listar(); // Llamar al método Listar del servicio

            // Recorrer la lista y agregar cada elemento al comboBox
            foreach (TipoBebidasModel tipobebida in lista)
            {
                comboTipoBebida.Items.Add(tipobebida.Nombre);
            }

            comboTipoBebida.SelectedIndex = 0; // muestra el primer elemento por defecto
        }

        private void llenarCBDescuentos()
        {
            comboDescuento.Items.Clear(); // Limpiar todos los elementos del combobox
            List<DescuentosModel> lista = _serviciodescuentos.Listar(); // Llamar al método Listar del servicio

            // Recorrer la lista y agregar cada elemento al comboBox
            foreach (DescuentosModel descuento in lista)
            {
                comboDescuento.Items.Add(descuento.Porcentaje); // Por el momento, mostramos solo el porcentaje
            }

            comboDescuento.SelectedIndex = 0; // muestra el primer elemento por defecto
        }
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            // Obtener los campos que se van a insertar
            string nombre = txtNombre.Text.Trim();
            double precio = txtPrecio.Text.Trim() != "" ? Convert.ToDouble(txtPrecio.Text.Trim()) : 0;
            int cantidad = txtCantidad.Text.Trim() != "" ? Convert.ToInt32(txtCantidad.Text.Trim()) : 0;
            string opc_combotb = comboTipoBebida.Text.Trim();
            double opc_combod = Convert.ToDouble(comboDescuento.Text.Trim());

            // Mandar los datos al servicio para ser validados
            string respuesta = _serviciobebidas.Guardar(nombre, precio, cantidad, opc_combotb, opc_combod);

            // Limpiar los campos después de guardar
            txtNombre.Clear();
            txtPrecio.Clear();
            txtCantidad.Clear();
            comboTipoBebida.SelectedIndex = 0; // Limpiar el combo box de tipo bebidas
            comboDescuento.SelectedIndex = 0; // Limpiar el combo box de descuentos
            //MessageBox.Show($"Los datos a insertar son: \nNombre: {nombre}\nPrecio: {precio}\nCantidad: {cantidad}\nCombo: {opc_combotb}\nDescuento: {opc_combod}");

            MessageBox.Show(respuesta);

            _serviciobebidas.Listar(dgvBebidas); // Para actualizar la tabla de bebidas
        }

        private void btnBebidas_Click(object sender, EventArgs e)
        {

        }

        private void btnCatalogos_Click(object sender, EventArgs e)
        {

        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {

        }

        private void btnReservas_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {

        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {

        }

        private void lblNombreAdmin_Click(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private void CargarDatos()
        //{

        //    //ServiciosBebidas registro = new ServiciosBebidas();
        //    //DataTable dt = registro.ListarMarcaciones();

        //    //if (dt != null && dt.Rows.Count > 0)
        //    //{
        //    //    dvgBebidas.DataSource = dt;

        //    //    dvgBebidas.Columns["Id"].Width = 120;
        //    //    dvgBebidas.Columns["Nombre"].Width = 120;
        //    //    dvgBebidas.Columns["Precio"].Width = 120;
        //    //    dvgBebidas.Columns["Cantidad_Existente"].Width = 120;

        //    //}
        //    //else
        //    //{
        //    //    dvgBebidas.DataSource = null; // Limpia el grid si no hay datos
        //    //}

        //}
    }
}
