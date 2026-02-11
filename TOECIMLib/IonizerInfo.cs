using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOECIMLib
{
    [Serializable]
    public class IonizerInfo
    {
        public IonizerInfo()
        {

        }

        public byte ID { get; set; } = 0;
        public string Status { get; set; } = "D";
    }
}
