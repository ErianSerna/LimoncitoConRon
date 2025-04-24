using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimoncitoConRon._3.Comunes.lib_entidades.Modelos;

namespace LimoncitoConRon._3.Comunes.lib_entidades.Modelos
{
    public class DescuentosModel
    {
        // Modelo Descuentos 
        public int Id { get; set; } 
        public bool Estado { get; set; }
        public string Tipo { get; set; }
        public double Porcentaje { get; set; }
        public DateTime Fecha_inicio { get; set; }
        public DateTime Fecha_final { get; set; }

        // Creacion de objetos (Por el momento sin objetos: Crear clase "TipoDescuentos")
    }
}
