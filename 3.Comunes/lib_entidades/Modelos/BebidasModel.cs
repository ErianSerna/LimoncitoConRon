using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimoncitoConRon._3.Comunes.lib_entidades.Modelos;

namespace LimoncitoConRon._3.Comunes.lib_entidades.Modelos
{
    public class BebidasModel
    {
        // Modelo Bebidas 
        public int Id { get; set; } 
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Cantidad_Existente { get; set; }
        public int Id_Descuentos { get; set; }
        public int Id_TipoBebidas { get; set; }

        // Creacion de objetos
        public DescuentosModel _Descuentos  { get; set; } // Objeto de descuentos falta mirar lo de los "?" para los nulos

    }
}
