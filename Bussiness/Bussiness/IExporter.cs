﻿using Bussiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Bussiness
{
    public interface IExporter
    {
        void ExportTickets(ShowModel show);
    }
}
