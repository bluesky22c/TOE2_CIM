using System;

namespace Engion.Net
{
    [Serializable]
    public class TENetData
    {
        public bool WaitResponse { get; set; } = true;
        public int Timeout { get; set; } = 5000;
    }
}
