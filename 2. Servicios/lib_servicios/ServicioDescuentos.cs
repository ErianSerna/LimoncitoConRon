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
    public class ServicioDescuentos
    {
        private RepositorioDescuentos Repositorio;
        public ServicioDescuentos(RepositorioDescuentos Repositorio)
        {
            this.Repositorio = Repositorio;
        }
        //metodo de listar
        public List<DescuentosModel> Listar()
        {
            List<DescuentosModel> lista = Repositorio.Listar();
            return lista;
        }

        // Metodo de Guardar
        public void Guardar()
        {
            // Obtener los campos que se van a guardar


            // Crear un objeto BebidasModel y asignar los valores de los campos
            // Llamar al método Guardar del repositorio para guardar el objeto en la base de datos

            // Ejemplo:
            // BebidasModel bebida = new BebidasModel
            // {
            //     Nombre = txtNombre.Text,
            //     Precio = Convert.ToDouble(txtPrecio.Text),
            //     Cantidad_Existente = Convert.ToInt32(txtCantidad.Text),
            //     Id_TipoBebidas = Convert.ToInt32(cmbTipoBebida.SelectedValue)
            // };
            // Repositorio.Guardar(bebida);
            // Limpiar los campos después de guardar
            // txtNombre.Clear();
            // txtPrecio.Clear();
            // txtCantidad.Clear();
            // cmbTipoBebida.SelectedIndex = -1; // Limpiar el combo box


        }
    }
}