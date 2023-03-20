﻿using Basler.Pylon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.Manager.Base.Interface
{
    public interface IMasterDriver
    {
        bool Init();
        void Close();
    }
}
