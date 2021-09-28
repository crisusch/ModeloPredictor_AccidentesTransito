using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VisualizadorMapas.Context;
using VisualizadorMapas.Models;
using static VisualizadorMapas.Models.IncidentesHeat;

namespace VisualizadorMapas.Controllers
{
    public class HomeController : Controller
    {
        private readonly AplicationDbContext _db;
        public HomeController(AplicationDbContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Visualizador/{anio}")]
        public IActionResult Visualizador(int anio)
        {
            ViewBag.Anio = anio;
            return View();
        }

        [Route("Clustering/{anio}")]
        public IActionResult Clustering(int anio)
        {
            ViewBag.Anio = anio;
            return View();
        }

        [Route("ClusteringTodos")]
        public IActionResult ClusteringTodos(int anio)
        {
            return View();
        }

        [Route("Todos")]
        public IActionResult Todos()
        {
            return View();
        }

        [Route("TodosDatos")]
        public IActionResult TodosDatos()
        {
            var listado = _db.Incidentes
                .ToList();

            List<IncidentesHeat> listaHeatMap = new List<IncidentesHeat>();
            IncidentesHeat nodo;
            propiedades properties;
            geometry data;
            foreach (var inc in listado)
            {
                List<double> puntos = new List<double>();
                puntos.Add(inc.Longitud);
                puntos.Add(inc.Latitud);

                data = new geometry()
                {
                    type = "Point",
                    coordinates = puntos
                };

                properties = new propiedades()
                {
                    id = inc.ID,
                    anio = inc.Anio,
                    fallecidos = inc.Fallecidos.HasValue == true ? inc.Fallecidos.Value : 0,
                    graves = inc.Graves.HasValue == true ? inc.Graves.Value : 0,
                    menosGraves = inc.MenosGraves.HasValue == true ? inc.MenosGraves.Value : 0,
                    leves = inc.Leves.HasValue == true ? inc.Leves.Value : 0,
                    direccion = inc.Numero.HasValue == true ? 
                        (inc.Numero.Value == 0 ? $"{inc.Calle1} con {inc.Calle2}, {inc.Comuna}" : $"{inc.Calle1} {inc.Numero.Value}, {inc.Comuna}") : 
                            $"{inc.Calle1} con {inc.Calle2}, {inc.Comuna}"
                };

                nodo = new IncidentesHeat()
                {
                    type = "Feature",
                    geometry = data,
                    id = inc.ID,
                    properties = properties
                };

                listaHeatMap.Add(nodo);
            }

            var metadata = new metaData()
            {
                title = "Incidents reported",
                status = "200",
                count = listaHeatMap.Count.ToString()
            };

            return Ok(new
            {
                type = "FeatureCollection",
                metadata = metadata,
                features = listaHeatMap
            });
        }

        [Route("Datos/{anio}")]
        public IActionResult Datos(int anio)
        {
            var listado = _db.Incidentes
                .Where(e => e.Anio == anio)
                .ToList();

            List<IncidentesHeat> listaHeatMap = new List<IncidentesHeat>();
            IncidentesHeat nodo;
            propiedades properties;
            geometry data;
            foreach (var inc in listado)
            {
                List<double> puntos = new List<double>();
                puntos.Add(inc.Longitud);
                puntos.Add(inc.Latitud);

                data = new geometry()
                {
                    type = "Point",
                    coordinates = puntos
                };

                properties = new propiedades()
                {
                    id = inc.ID,
                    anio = inc.Anio,
                    fallecidos = inc.Fallecidos.HasValue == true ? inc.Fallecidos.Value : 0,
                    graves = inc.Graves.HasValue == true ? inc.Graves.Value : 0,
                    menosGraves = inc.MenosGraves.HasValue == true ? inc.MenosGraves.Value : 0,
                    leves = inc.Leves.HasValue == true ? inc.Leves.Value : 0,
                    direccion = inc.Numero.HasValue == true ?
                        (inc.Numero.Value == 0 ? $"{inc.Calle1} con {inc.Calle2}, {inc.Comuna}" : $"{inc.Calle1} {inc.Numero.Value}, {inc.Comuna}") :
                            $"{inc.Calle1} con {inc.Calle2}, {inc.Comuna}"
                };

                nodo = new IncidentesHeat()
                {
                    type = "Feature",
                    geometry = data,
                    id = inc.ID,
                    properties = properties
                };

                listaHeatMap.Add(nodo);
            }

            var metadata = new metaData()
            {
                title = "Incidents reported",
                status = "200",
                count = listaHeatMap.Count.ToString()
            };

            return Ok(new
            {
                type = "FeatureCollection",
                metadata = metadata,
                features = listaHeatMap
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
