using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCADA.RTDB.Common.Design;

namespace SCADA.RTDB.RunTime
{
    public class Class1
    {
        public void Load(IVariableDesignRepository iVariableDesignRepository)
        {
            iVariableDesignRepository.Load();
        }

        public bool GetBoolValue(string path);
    }
}
