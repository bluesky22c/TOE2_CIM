using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOECIMLib
{
    [Serializable]
    public class APDInfo
    {
        public APDInfo()
        {
            APDName = "APDName";
            APDValue = "APDValue";
        }

        public string APDName { get; set; } = null!;
        public string APDValue { get; set; } = null!;
    }
}
