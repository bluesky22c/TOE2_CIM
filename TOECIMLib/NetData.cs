using System;

namespace Elp.Net
{
    [Serializable]
    public class TENetData
    {
        public bool WaitResponse { get; set; } = true;
        public int Timeout { get; set; } = 5000;
    }
}
