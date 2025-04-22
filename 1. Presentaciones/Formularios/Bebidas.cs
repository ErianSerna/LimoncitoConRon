using LimoncitoConRon._2._Servicios.lib_servicios;
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

        public Bebidas(ServicioBebidas servicioBebidas)
        {
            InitializeComponent();
            this._serviciobebidas = servicioBebidas;
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
            lblNombreAdmin.Text = $"!Bienvenido {lblNombreAdmin} !"; //Hay que traerlo de la bd
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Esta es una alerta del programa", "itulo X");
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
