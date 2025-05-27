using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace LimoncitoConRon._2._Servicios
{
    public class Conexion // Clase singleton
    {
        private static Conexion instancia;
        private static readonly object bloqueo = new object();
        private SqlConnection conexion;

        // Metodo constructor privado: Crea el objeto de conexion
        private Conexion()
        {
            //string connectionString = "server=ERIAN\\DEV;database=Licorera_DB;integrated security=true";
            //string connectionString = "server=DESKTOP-D1091LH\\DEV;database=Licorera_DB;integrated security=true";
            //string connectionString = "server=ASUS_ISA\\DEV;database=Licorera_DB;integrated security=true";
            string connectionString = "server=DESKTOP-2IGCP51\\DEV;database=Licorera_DB;integrated security=true";
            conexion = new SqlConnection(connectionString);
            conexion.Open(); // Abrir la conexion
        }

        // Metodo de obtenerInstancia para obtener el obj Conexion y poder hacer uso del metodo obtenerConexion
        public static Conexion obtenerInstancia()
        {
            if (instancia == null)
            {
                lock (bloqueo) // Asegura que solo un hilo cree la instancia
                {
                    if (instancia == null)
                    {
                        instancia = new Conexion();
                    }
                }
            }
            return instancia;
        }

        // Metodo para acceder al objeto conexion de sqlConnection
        public SqlConnection obtenerConexion()
        {
            return conexion;
        }

    }
}

//   "ConectionString": "server=ERIAN\\DEV;database=Licorera_DB;Integrated Security=True;TrustServerCertificate=true;"
