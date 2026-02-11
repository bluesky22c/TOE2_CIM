using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DBMulti
{
    public class StatSender<T> : StatCommBase<T>
    {
        public IPPortList PortList { get; set; } = new IPPortList { IPPortsString = "localhost,11000" };

        public int SendInterval { get; set; } = 100;

        static ManualResetEvent stopEvent = new ManualResetEvent(false);
        List<Thread> threads = new List<Thread>();
        public void Start()
        {
            threads.Clear(); 
            stopEvent.Reset();
            int cnt = 0;
            // udp thread
            if (UseUDP)
            {
                foreach (var p in PortList.Items)
                {
                    Thread thread = new Thread(new ThreadStart(() =>
                    {
                        try
                        {
                            UdpClient udpclient = new UdpClient();
                            IPHostEntry hostEntry = Dns.GetHostEntry(p.IPAddr);
                            var address = hostEntry.AddressList.Where(a => a.AddressFamily == AddressFamily.InterNetwork);
                            IPAddress ipAddress = address.FirstOrDefault();
                            IPEndPoint endPoint = new IPEndPoint(ipAddress, p.PortNo);
                            //var eqpStat = new EqpStat { Name = Name, SendObject = SendObject };
                            while (true)
                            {
                                EqpStat.IndexCount++;
                                //eqpStat.SendObject = SendObject;

                                var jstring = JsonConvert.SerializeObject(EqpStat);
                                byte[] msg = Encoding.ASCII.GetBytes(jstring);
                                //Console.WriteLine($"Sending data. {jstring}");
                                // This call blocks.
                                udpclient.Send(msg, msg.Length, endPoint);
                                if (stopEvent.WaitOne(SendInterval))
                                {
                                    break;
                                }
                            }
                            udpclient.Close();
                            Console.WriteLine($"{Thread.CurrentThread.Name} Ended");
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }));
                    thread.Name = $"Thread {cnt++}";
                    thread.IsBackground = true;
                    thread.Start();
                    Console.WriteLine($"{thread.Name} Started");
                    threads.Add(thread);
                }
            }

            // db thread
            if (UseDB)
            {
                db = new EqpStatDb<T>();
                db.CreateTableIfNotExist();
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    while (true)
                    {
                        db.SetObject(Name, EqpStat.DataObject);
                        if (stopEvent.WaitOne(SendInterval))
                        {
                            break;
                        }
                    }
                }));
                thread.Start();
            }
        }

        public void UpdateDBData()
        {
            db.CreateTableIfNotExist();
            db.SetObject(Name, EqpStat.DataObject);
        }

        public void LoadStatFromDb()
        {
            db.GetStat(Name);
        }

        public void LoadDataFromDb()
        {
            db.GetData(Name);
        }

        public void Stop()
        {
            stopEvent.Set();
        }
    }

    [Serializable]
    public class IPPortList
    {
        public List<IPPortPair> Items { get; set; } = new List<IPPortPair>();

        [XmlIgnore]
        public string IPPortsString
        {
            get
            {
                var sl = "";
                foreach(var i in Items)
                {
                    sl += $"{i};";
                }
                return sl.TrimEnd(';');
            }
            set
            {
                Items.Clear();
                if (value == null)
                    return;
               
                var lines = value.Split(';');
                foreach (var l in lines)
                {
                    if (string.IsNullOrWhiteSpace(l))
                        continue;
                    var t = l.Split(',');
                    if (t.Length != 2)
                        continue;

                    if (int.TryParse(t[1], out int portNo) == false)
                        throw new Exception($"Invalid PortNo {t[1]}");

                    Items.Add(new IPPortPair { IPAddr = t[0].Trim(), PortNo = portNo });                    
                }
            }
        }
    }

    [Serializable]
    public class IPPortPair
    {
        public string IPAddr { get; set; } = "localhost";
        public int PortNo { get; set; } = 7777;

        public override string ToString()
        {
            return $"{IPAddr}, {PortNo}";
        }
    }
}
