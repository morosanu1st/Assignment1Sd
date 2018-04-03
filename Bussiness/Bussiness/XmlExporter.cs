using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Bussiness.Mappers;
using Bussiness.Models;
using DataAccsess.DTO;

namespace Bussiness.Bussiness
{
    public class XmlExporter : IExporter
    {
        public void ExportTickets(ShowModel show)
        {
            var writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Ticket>));
           
            using( var stream= File.OpenWrite(@"E:\Faculta\SD\projekt\WebApplication1\" + show.Title + "_" + show.Day.ToString("dd_MM_yyyy") + ".xml"))
            {
                writer.Serialize(stream, show.Tickets.Select(ticket => ticket.AsDto()).ToList());
            }
            System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Notepad++\notepad++.exe", @"E:\Faculta\SD\projekt\WebApplication1\" + show.Title + "_" + show.Day.ToString("dd_MM_yyyy") + ".xml");

        }
    }
}