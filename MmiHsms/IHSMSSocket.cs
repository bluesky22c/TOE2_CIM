using System;
namespace MMISoft.HSMS
{
    public interface IHSMSSocket
    {
        HSMSMessage SentMsg { get; }
        HSMSMessage RecMsg { get; }
        HSMSConfig Config { set; get; }
//        bool ParseBinaryToString(byte[] data, int listCount, int depth, ref int startIndex, ref string str);
        void ReceiveProc();
        void ReplyStringMsg(MessageHeader header, string strMsg);
        bool SendMsg(HSMSMessage msg);
        void Select();
        bool SendStringMsg(string strMessage);
        bool SendHsms(int stream, int function, bool reply, string strMsg);
        bool SendMHEAD(int stream, int funtion, MessageHeader strMsg);
        void Start();
        void Close();
    }
}
