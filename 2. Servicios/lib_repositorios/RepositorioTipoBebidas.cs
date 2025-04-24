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
    public class RepositorioTipoBebidas
    {
        private readonly SqlConnection _conexion;

        public RepositorioTipoBebidas(SqlConnection conexion)
        {
            _conexion = conexion;
        }

        public List<TipoBebidasModel> Listar()
        {
            //se crea una lista de TipoBebidas
            List<TipoBebidasModel> lista = new List<TipoBebidasModel>();

            //se ejecuta el sp llamado sp_ListarTipoBebidas
            using (SqlCommand cmd = new SqlCommand("sp_ListarTipoBebida", _conexion))
            {
                //Indicar que el comando es un procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;
                //lee las filas 
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                   //bucle para leer las filas 
                   while(reader.Read())
                    {
                        lista.Add(new TipoBebidasModel
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1)
                        });
                    }
                }
            }
            return lista;
        }//fin de metodo

        //metodo de guardar
        //public void Guardar(BebidasModel bebida)
        //{
        //    using (SqlCommand cmd = new SqlCommand("sp_GuardarBebida", _conexion))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        //cmd.Parameters.AddWithValue("@Id", bebida.Id); ---> El id se genera con Identity
        //        cmd.Parameters.AddWithValue("@Nombre", bebida.Nombre);
        //        cmd.Parameters.AddWithValue("@Precio", bebida.Precio);
        //        cmd.Parameters.AddWithValue("@Cantidad_Existente", bebida.Cantidad_Existente);
        //        cmd.Parameters.AddWithValue("@Id_Descuentos", bebida.Id_Descuentos);
        //        cmd.Parameters.AddWithValue("@Id_TipoBebidas", bebida.Id_TipoBebidas);
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        //metodo de actualizar 
        //public void Actualizar(BebidasModel bebida)
        //{
        //    using (SqlCommand cmd = new SqlCommand("sp_ActualizarBebida", _conexion))
        //    {
               
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Id", bebida.Id);
        //        cmd.Parameters.AddWithValue("@Nombre", bebida.Nombre);
        //        cmd.Parameters.AddWithValue("@Precio", bebida.Precio);
        //        cmd.Parameters.AddWithValue("@Cantidad_Existente", bebida.Cantidad_Existente);
        //        cmd.Parameters.AddWithValue("@Id_Descuentos", bebida.Id_Descuentos);
        //        cmd.Parameters.AddWithValue("@Id_TipoBebidas", bebida.Id_TipoBebidas);

        //        _conexion.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        //metodo de Eliminar
        //public void Borrar(int id)
        //{
        //    using (SqlCommand cmd = new SqlCommand("sp_BorrarBebida", _conexion))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Id", id);

        //        cmd.ExecuteNonQuery();
        //    }
        //}

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
