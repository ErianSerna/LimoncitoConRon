using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimoncitoConRon._1._Presentaciones.Formularios;
using LimoncitoConRon._2._Servicios.lib_repositorios;
using LimoncitoConRon._3.Comunes.lib_entidades.Modelos;

namespace LimoncitoConRon._2._Servicios.lib_repositorios
{
    public class RepositorioBebidas
    {
        private readonly SqlConnection _conexion;

        public RepositorioBebidas(SqlConnection conexion)
        {
            _conexion = conexion;
        }

        //public DataTable Listar()
        //{
        //    //se crea una lista de bebidas 
        //    List<BebidasModel> lista = new List<BebidasModel>();

        //    //se ejecuta el sp llamado sp_ListarBebidas
        //    using (SqlCommand cmd = new SqlCommand("sp_ListarBebidas", _conexion))
        //    {
        //        //indica que se hace no es un texto sino un sp
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        //lee las filas 
        //        using (SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //           //bucle para leeer las filas 
        //           while(reader.Read())
        //            {
        //                lista.Add(new BebidasModel
        //                {
        //                    Id = reader.GetInt32(0),
        //                    Nombre = reader.GetString(1),
        //                    Precio = reader.GetDouble(2),
        //                    Cantidad_Existente = reader.GetInt32(3),
        //                    Id_TipoBebidas = reader.GetInt32(4),
        //                });
        //            }
        //        }
        //    }
        //    return lista;
        //}//fin de metodo

        public DataTable Listar()
        {
            // Crear el dataTable que el adaptador va a llenar con los datos resultantes de la consulta
            DataTable dt = new DataTable();
            var consulta = "sp_ListarBebidas";
            using (SqlCommand cmd = new SqlCommand(consulta, _conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure; // Especificar el tipo de comando

                using (SqlDataAdapter adaptador = new SqlDataAdapter(cmd))
                {
                    adaptador.Fill(dt);
                }
            }

            // Retornar el dataTable
            return dt;
        }

        //metodo de guardar
        public Dictionary<string, object> Guardar(BebidasModel bebida)
        {
            Dictionary<string, object> resultado = new Dictionary<string, object>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_GuardarBebida", _conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", bebida.Nombre);
                    cmd.Parameters.AddWithValue("@precio", bebida.Precio);
                    cmd.Parameters.AddWithValue("@cant_exis", bebida.Cantidad_Existente);
                    cmd.Parameters.AddWithValue("@id_desc", bebida.Id_Descuentos);
                    cmd.Parameters.AddWithValue("@id_tbebida", bebida.Id_TipoBebidas);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    
                    if (filasAfectadas > 0)
                    {
                        resultado["estado"] = "success";
                        resultado["mensaje"] = "La bebida fue insertada correctamente";
                    }
                    return resultado;
                }
            }
            catch (SqlException ex)
            {
                resultado["estado"] = "error";
                resultado["mensaje"] = ex.Message;
                return resultado;
            }
        }

        //metodo de actualizar 
        public void Actualizar(BebidasModel bebida)
        {
            using (SqlCommand cmd = new SqlCommand("sp_ActualizarBebida", _conexion))
            {
               
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", bebida.Id);
                cmd.Parameters.AddWithValue("@Nombre", bebida.Nombre);
                cmd.Parameters.AddWithValue("@Precio", bebida.Precio);
                cmd.Parameters.AddWithValue("@Cantidad_Existente", bebida.Cantidad_Existente);
                cmd.Parameters.AddWithValue("@Id_Descuentos", bebida.Id_Descuentos);
                cmd.Parameters.AddWithValue("@Id_TipoBebidas", bebida.Id_TipoBebidas);

                _conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //metodo de Eliminar
        public void Borrar(int id)
        {
            using (SqlCommand cmd = new SqlCommand("sp_BorrarBebida", _conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }

        //metodos de buscar bebidas
        //public List<Bebidas> Buscar(string nombre)
        //{
        //    List<Bebidas> lista = new List<Bebidas>();
        //    using (SqlCommand cmd = new SqlCommand("sp_BuscarBebida", _conexion))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Nombre", nombre);
        //        using (SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            while(reader.Read())
        //            {

        //            }
        //        }
        //    }
        //}
    }
}
