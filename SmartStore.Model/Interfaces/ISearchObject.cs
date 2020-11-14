﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Models.Interfaces
{
    public interface ISearchObject
    {
        int? PageIndex { get; set; }
        int? PageSize { get; set; }
    }
}
