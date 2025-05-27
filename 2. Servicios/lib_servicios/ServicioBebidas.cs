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
            DataTable dt = Repositorio.ListarDataTable();

            if (dt != null && dt.Rows.Count > 0)
            {
                // Hay datos para llenar la tabla
                dgvBebidas.DataSource = dt; // Llenar el datagridView tomando como fuente los datos del dt
                dgvBebidas.Columns["Id"].Width = 80;
                dgvBebidas.Columns["Nombre"].Width = 220;
                dgvBebidas.Columns["Precio"].Width = 100;
                dgvBebidas.Columns["Cantidad_Existente"].Width = 110;
                dgvBebidas.Columns["Tipo"].Width = 110;
                dgvBebidas.Columns["Descuento"].Width = 110;

                // Centrar los encabezados del DataGridView
                foreach (DataGridViewColumn col in dgvBebidas.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // Agregar boton de actualizar/modificar
                if (!dgvBebidas.Columns.Contains("Editar"))
                {
                    DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
                    btnEditar.Name = "Editar";
                    btnEditar.HeaderText = "Editar";
                    btnEditar.Text = "Editar";
                    btnEditar.UseColumnTextForButtonValue = false; // para que el texto del boton sea el mismo que el del encabezado
                    btnEditar.Width = 80;
                    dgvBebidas.Columns.Add(btnEditar);
                }
                

                // Agregar boton eliminar/inhabilitar
                if (!dgvBebidas.Columns.Contains("Eliminar"))
                {
                    DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
                    btnEliminar.Name = "Eliminar";
                    btnEliminar.HeaderText = "Eliminar";
                    btnEliminar.Text = "Eliminar";
                    btnEliminar.UseColumnTextForButtonValue = false; // para que el texto del boton sea el mismo que el del encabezado
                    btnEliminar.Width = 80;
                    dgvBebidas.Columns.Add(btnEliminar);
                }

                // Asignar el texto inicial a los botones (solo si se usa UseColumnTextForButtonValue = false)
                foreach (DataGridViewRow fila in dgvBebidas.Rows)
                {
                    fila.Cells["Editar"].Value = "Editar";
                    fila.Cells["Eliminar"].Value = "Eliminar";
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

        // Metodo para Actualizar 
        public string Actualizar(int id, string nombre, double precio, int cantidadExi, string opc_combotb, double opc_combod)
        {
            string mensaje = "";

            // Buscar y obtener el id del opc_combotd
            List<TipoBebidasModel> listatb = RepositorioTipoBebidas.Listar();
            TipoBebidasModel tipobebida = listatb.FirstOrDefault(tb => tb.Nombre == opc_combotb); // Buscar si el elemento esta dentro de la lista

            // Buscar y obtener el id del opc_combod
            List<DescuentosModel> listad = RepositorioDescuentos.Listar();
            DescuentosModel descuento = listad.FirstOrDefault(d => d.Porcentaje == opc_combod); // Buscar si el elemento esta dentro de la lista

            // Validar que ningun dato este vacio o nulo
            if (id <= 0 || nombre.Equals("") || precio <= 0 || cantidadExi <= 0 || tipobebida == null || descuento == null)
            {
                mensaje = "Los campos no pueden estar vacios";
            } else
            {
                // Validar que el id sea valido 
                if (id <= 0) // No es valido
                {
                    mensaje = "El id no es valido";
                }
                else // Es valido
                {
                    // Validar si el id a eliminar existe
                    List<BebidasModel> listab = Repositorio.Listar(); // Llama al listar que retorna una lista, no el que manda un Datatable
                    BebidasModel bebida = listab.FirstOrDefault(b => b.Id == id); // Buscar si el elemento esta dentro de la lista

                    if (bebida == null)
                    {
                        mensaje = "El id no existe";
                    }
                    else
                    {
                        // Obtener el id de tipoBebida y id de descuentos
                        int id_TipoBebida = tipobebida.Id;
                        int id_Descuentos = descuento.Id;

                        // Creo el objeto y se lo mando al repositorio
                        BebidasModel objBebida2 = new BebidasModel()
                        {
                            Id = id,
                            Nombre = nombre,
                            Precio = precio,
                            Cantidad_Existente = cantidadExi,
                            Id_TipoBebidas = id_TipoBebida,
                            Id_Descuentos = id_Descuentos
                        };

                        Dictionary<string, Object> resultado2 = Repositorio.Actualizar(objBebida2);

                        // Valido si la respuesta fue correcta o no
                        mensaje = resultado2["estado"].ToString() == "success" ? resultado2["mensaje"].ToString() : "Ocurrio un error al actualizar la bebida: " + (resultado2["mensaje"].ToString());
                    }
                }
            }
            return mensaje;
        }

        // Metodo de Eliminar
        public string Eliminar(int id)
        {
            string mensaje = "";

            // Validar si el id es valido
            if (id <= 0) // No es valido
            {
                mensaje ="El id no es valido";
            }
            else // Es valido
            {
                // Validar si el id a eliminar existe
                List<BebidasModel> listab = Repositorio.Listar(); // Llama al listar que retorna una lista, no el que manda un Datatable
                BebidasModel bebida = listab.FirstOrDefault(b => b.Id == id); // Buscar si el elemento esta dentro de la lista

                if (bebida == null)
                {
                    mensaje = "El id no existe";
                }
                Dictionary<string, Object> resultado = Repositorio.Borrar(id);
                // Valido si la respuesta fue correcta o no
                mensaje = resultado["estado"].ToString() == "success" ? resultado["mensaje"].ToString() : "Ocurrio un error al eliminar la bebida: " + (resultado["mensaje"].ToString());
            }

            return mensaje;
        }
        
        //método de buscar
        public void Buscar (DataGridView dgvBebidas,string nombre)
        {
            DataTable dt = Repositorio.Buscar(nombre);

            if (dt != null && dt.Rows.Count > 0)
            {
                dgvBebidas.DataSource = dt;
            }
        }

    }
}