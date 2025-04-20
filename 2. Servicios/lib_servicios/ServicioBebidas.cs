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
        public ServicioBebidas(RepositorioBebidas Repositorio)
        {
            this.Repositorio = Repositorio;
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
                dgvBebidas.Columns["Id"].Width = 50;
                dgvBebidas.Columns["Nombre"].Width = 150;
                dgvBebidas.Columns["Precio"].Width = 50;
                dgvBebidas.Columns["Cantidad_Existente"].Width = 50;
                dgvBebidas.Columns["Tipo"].Width = 100;
            }
            else
            {
                dgvBebidas.DataSource = null; // Limpia el grid si no hay datos
                MessageBox.Show("No hay registros disponibles.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
