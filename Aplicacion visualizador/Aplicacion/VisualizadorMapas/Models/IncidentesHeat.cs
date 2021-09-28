using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisualizadorMapas.Models
{
    public class metaData
    {
        public string title { get; set; }
        public string status { get; set; }
        public string count { get; set; }
    }

    public class geometry
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class propiedades
    {
        public long id { get; set; }
        public int anio { get; set; }
        public string direccion { get; set; }
        public int fallecidos { get; set; }
        public int graves { get; set; }
        public int menosGraves { get; set; }
        public int leves { get; set; }
    }
    public class IncidentesHeat
    {
        public string type { get; set; }
        public geometry geometry { get; set; }
        public long id { get; set; }
        public propiedades properties { get; set; }
    }
    
}
