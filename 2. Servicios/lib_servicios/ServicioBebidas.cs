using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimoncitoConRon._2._Servicios.lib_repositorios;
using LimoncitoConRon._3.Comunes.lib_entidades.Modelos;
using System.Windows.Forms;


namespace LimoncitoConRon._2._Servicios.lib_servicios
{
    public class ServicioBebidas
    {
        private RepositorioBebidas Repositorio;
        private RepositorioTipoBebidas RepositorioTipoBebidas;
        private RepositorioDescuentos RepositorioDescuentos;

        public ServicioBebidas(RepositorioBebidas Repositorio, RepositorioTipoBebidas RepositorioTB, RepositorioDescuentos RepositorioD)
        {
            this.Repositorio = Repositorio;
            this.RepositorioTipoBebidas = RepositorioTB;
            this.RepositorioDescuentos = RepositorioD;
        }
        //metodo de listar
        public void Listar(DataGridView dgvBebidas)
        {
            // Verificar si la lista esta vacia
            DataTable dt = Repositorio.Listar();

            if (dt != null && dt.Rows.Count > 0)
            {
                // Hay datos para llenar la tabla
                dgvBebidas.DataSource = dt; // Llenar el datagridView tomando como fuente los datos del dt
                dgvBebidas.Columns["Id"].Width = 80;
                dgvBebidas.Columns["Nombre"].Width = 220;
                dgvBebidas.Columns["Precio"].Width = 100;
                dgvBebidas.Columns["Cantidad_Existente"].Width = 110;
                dgvBebidas.Columns["Tipo"].Width = 110;

                // Centrar los encabezados del DataGridView
                foreach (DataGridViewColumn col in dgvBebidas.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                dgvBebidas.DataSource = null; // Limpia el grid si no hay datos
                MessageBox.Show("No hay registros disponibles.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Metodo de Guardar
        public string Guardar(string nombre, double precio, int cantidadExi, string opc_combotb, double opc_combod)
        {
            string mensaje = "";

            // Buscar y obtener el id del opc_combotd
            List<TipoBebidasModel> listatb = RepositorioTipoBebidas.Listar();
            TipoBebidasModel tipobebida = listatb.FirstOrDefault(tb => tb.Nombre == opc_combotb); // Buscar si el elemento esta dentro de la lista

            // Buscar y obtener el id del opc_combod
            List<DescuentosModel> listad = RepositorioDescuentos.Listar();
            DescuentosModel descuento = listad.FirstOrDefault(d => d.Porcentaje == opc_combod); // Buscar si el elemento esta dentro de la lista

            // Validar que ningun dato este vacio o nulo
            if (nombre.Equals("") || precio <= 0 || cantidadExi <= 0 || tipobebida == null || descuento == null)
            {
                return "Los campos no pueden estar vacios";
            }

            // Obtener el id de tipoBebida y id de descuentos
            int id_TipoBebida = tipobebida.Id;
            int id_Descuentos = descuento.Id;

            BebidasModel objBebida = new BebidasModel(){
                Nombre = nombre,
                Precio = precio,
                Cantidad_Existente = cantidadExi,
                Id_TipoBebidas = id_TipoBebida,
                Id_Descuentos = id_Descuentos
            };

            Dictionary<string,Object> resultado = Repositorio.Guardar(objBebida);

            // Valido si la respuesta fue correcta o no
            mensaje = resultado["estado"].ToString() == "success" ? resultado["mensaje"].ToString() : "Ocurrio un error al insertar la bebida: " + (resultado["mensaje"].ToString());
            return mensaje;
        }
    }
}