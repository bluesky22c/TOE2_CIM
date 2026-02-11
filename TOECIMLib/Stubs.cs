using System;
using System.Collections.Generic;

namespace TOECIMLib
{
    public class AxisPositions
    {
        public Dictionary<string, double> Positions { get; set; } = new Dictionary<string, double>();
    }

    public class CommonGlassInfo
    {
        public string GlassID { get; set; }
        public string LotID { get; set; }
        public string CassetteID { get; set; }
        public string SlotNo { get; set; }
        public string PPID { get; set; }
        public string ProcessID { get; set; }
        public string GlassODR { get; set; }
        public string JudgeCode { get; set; }
        public string PanelInfo { get; set; }
        public string SubMDLInfo { get; set; }
        public byte PortID { get; set; }
        public byte GlassCode { get; set; }
        public byte ReInput { get; set; }
        public string GlassThick { get; set; }
        public string PartNum { get; set; }
        public string ProdType { get; set; }
        public string Attribute { get; set; }
        public string GlassType { get; set; }
        public string KeyID { get; set; }
        public byte Dicode { get; set; }
        public string GlassProcess { get; set; }
        public ushort Dest { get; set; }
        public bool LastGlass { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class MCC
    {
        public string Name { get; set; }
    }

    public class ETimeout
    {
        public bool IsTimeout { get; set; }
        public event EventHandler<TimeoutParam> TimeoutEvent;

        public static string toGlassDataLoad = "toGlassDataLoad";
        public static string toCarrierDataLoad = "toCarrierDataLoad";
        public static string toGlassUnloadData = "toGlassUnloadData";
        public static string toGlassDataDelete = "toGlassDataDelete";
        public static string toRecipeDataRequest = "toRecipeDataRequest";
        
        public static string S2F18 = "S2F18";
        public static string S2F36 = "S2F36";
        public static string S2F38 = "S2F38";
        public static string S5F2 = "S5F2";
        public static string S5F4 = "S5F4";
        public static string S6F12 = "S6F12";
        public static string T3 = "T3";
        public static string T5 = "T5";
        public static string T6 = "T6";
        public static string T7 = "T7";
        public static string T8 = "T8";

        public void RegNewTimeout(string name, int ms) { }
        public void AbortTimeout(string name) { }
        public void ReleaseTimeout(string name) { }
    }

    public class TimeoutParam : EventArgs
    {
        public string Name { get; set; }
    }

    public class TimeoutInfo
    {
        public string Name { get; set; }
        public double Duration { get; set; }
    }

    public enum ScanDir
    {
        Normal,
        Forward,
        Reverse,
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop
    }


}
