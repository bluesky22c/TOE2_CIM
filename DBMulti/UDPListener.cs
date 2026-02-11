using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DBMulti
{
    public class StatReceiver<T> : StatCommBase<T>
    {
        public bool Alive { get; set; } = false;
        //AutoResetEvent stopEvent = new AutoResetEvent(false);
        public int ReceiveInterval { get; set; } = 1000;
        bool stopEvent = false;
        public void Start(int portNo)
        {
            stopEvent = false;
            var thread = new Thread(new ThreadStart(() =>
            {
                UdpClient client = new UdpClient(portNo);
                IPEndPoint localEp = new IPEndPoint(IPAddress.Any, 0);
                client.Client.ReceiveTimeout = ReceiveInterval;
                while (true)
                {
                    try
                    {
                        if (stopEvent == true)
                            break;
                        byte[] data = client.Receive(ref localEp);
                        string strData = Encoding.Default.GetString(data);
                        var stat = JsonConvert.DeserializeObject<EqpStatBase>(strData);
                        EqpStat = EqpStat<T>.CopyFrom(stat);
                        Alive = true;
                        //Console.WriteLine("received data : {0}", strData);
                    }
                    catch (SocketException exp)
                    {
                        Alive = false;
                        if (exp.SocketErrorCode == SocketError.TimedOut)
                        {
                            Console.WriteLine("Timeout");
                        }
                        else
                            Console.WriteLine(exp.Message);
                    }
                }
                stopEvent = false;
                client.Close();
            }));
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
