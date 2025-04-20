using LimoncitoConRon._1._Presentaciones.Formularios;
using LimoncitoConRon._2._Servicios;
using LimoncitoConRon._2._Servicios.lib_repositorios;
using LimoncitoConRon._2._Servicios.lib_servicios;
using LimoncitoConRon._1._Presentaciones.Formularios;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LimoncitoConRon
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Preparo los objetos de servicio que se usaran en el programa
            SqlConnection conexion = Conexion.obtenerInstancia().obtenerConexion(); // obtener la conexion

            // Repositorio y Servicio de Bebidas
            var repo = new RepositorioBebidas(conexion);
            var serv = new ServicioBebidas(repo);

            /*
             * Se crearia un objeto del formulario principal (Seria el menu con las opciones de las tbls a trabajar)
             * Pero en este caso, para la entrega 1, como no vamos a hacer el menu ese, entonces el formulario principal vendria siendo el de bebidas
             */
            Application.Run(new Bebidas(serv)); 
        }
    }
}