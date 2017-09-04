using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using System.Text;

namespace core2Angular.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        [HttpGet("[action]")]
        public string GetName()
        {
            string xmlFrag =
            @"<book> 
                 <misc>
                   <style>paperback</style> 
                   <pages>240</pages>
                 </misc> 
                </book>";

            // Create the XmlNamespaceManager.
            NameTable nt = new NameTable();
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(nt);

            // Create the XmlParserContext.
            XmlParserContext context = new XmlParserContext(null, nsmgr, null, XmlSpace.None);

            // Create the reader.
            XmlTextReader reader = new XmlTextReader(xmlFrag, XmlNodeType.Element, context);

            var sb = new StringBuilder();

            // Parse the XML and display each node. 
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        sb.Append($"{reader.Name} {reader.Depth}").Append("<br>");
                        break;
                    //case XmlNodeType.Text:
                    //    sb.Append(string.Format("{0} {1},{2}  ", reader.Depth, reader.LineNumber, reader.LinePosition));
                    //    sb.Append(string.Format("  {0}", reader.Value));
                    //    break;
                    //case XmlNodeType.EndElement:
                    //    sb.Append(string.Format("{0} {1},{2}  ", reader.Depth, reader.LineNumber, reader.LinePosition));
                    //    sb.Append(string.Format("</{0}>", reader.Name));
                    //    break;
                }
            }

            // Close the reader.
            reader.Close();

            return sb.ToString();
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
