﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooliesX.Interface
{
   public interface IJsonConnector
    {
        string MakeRequest();
        string MakeRequest(string parameters);
    }
}
