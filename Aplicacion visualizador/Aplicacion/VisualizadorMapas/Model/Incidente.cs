using System;
using System.Collections.Generic;

namespace VisualizadorMapas.Model
{
    public partial class Incidente
    {
        public int ID { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public int Anio { get; set; }
        public string Comuna { get; set; }
        public string Calle1 { get; set; }
        public string Calle2 { get; set; }
        public int? Numero { get; set; }
        public int? Fallecidos { get; set; }
        public int? Graves { get; set; }
        public int? MenosGraves { get; set; }
        public int? Leves { get; set; }
    }
}
