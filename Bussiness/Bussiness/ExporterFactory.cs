using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bussiness.Bussiness
{
    public class ExporterFactory
    {
        public IExporter GetExporter(string type)
        {
            if(type.Equals("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                return new XmlExporter();
            }
            if (type.Equals("csv", StringComparison.InvariantCultureIgnoreCase))
            {
                return new CsvExporter();
            }
            return null;
        }
    }
}