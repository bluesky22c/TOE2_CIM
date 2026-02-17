using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace MMISoft
{
    public class Log
    {
        public enum SendReceive { Send, Receive, Error, Etc }
        public class LogItem
        {
            public DateTime DateTime { get; set; }
            public string Data { get; set; }
        }

        CancellationTokenSource source;
        Thread th;
        ConcurrentQueue<LogItem> Q = new ConcurrentQueue<LogItem>();

        Encoding encoding = Encoding.UTF8;
        readonly int capacity = 0;
        readonly string name;
        string path;
        string subPath;
        string realPath;
        public Log(string name, int capacity, string path, Encoding encoding)
        {
            this.name = name;
            this.capacity = Math.Max(1 << 20, capacity);
            this.subPath = path;
            this.path = Path.Combine(subPath, $"{DateTime.Now:yyyyMMdd}",name);
            this.encoding = encoding;

            Directory.CreateDirectory(path);
            source = new CancellationTokenSource();
            th = new Thread(new ThreadStart(DoWork));
            th.IsBackground = true;
            th.Start();
        }

        public void WriteLine(string logString)
        {
            WriteLine(Log.SendReceive.Etc, 0, "", false, logString, 0);
        }

        public void WriteDesc(string logString)
        {
            DateTime now = DateTime.Now;
            LogItem item = new LogItem
            {
                DateTime = now,
                Data = logString,
            };
            Q.Enqueue(item);
        }

        public void WriteLine(SendReceive sr, int systemBytes, string sf, bool needReply, string logString, int deviceID = 0)
        {
            DateTime now = DateTime.Now;
            LogItem item = new LogItem
            {
                DateTime = now,
                Data = $"{now:yyyy-MM-dd HH:mm:ss.fff}\t{sr}\t{deviceID}\t{systemBytes}\t{sf}\t{needReply}\t\"{logString}\"",
            };
            Q.Enqueue(item);
        }

        private bool IsSameDay(DateTime a, DateTime b)
        {
            return a.Year == b.Year && a.Month == b.Month && a.Day == b.Day;
        }

        private string GetLogFileName(DateTime logDay, long sequence)
        {
            string suffix = sequence == 0 ? "0" : $"{sequence:D3}";
            string datetime = logDay.ToString("_yyyy-MM-dd_");
            return Path.Combine(realPath, $"{name}{datetime}{suffix}.txt");
        }

        private void DoWork()
        {
            DateTime logDay = DateTime.MinValue;
            string filename = null;
            long sequence = 0;
            while (true)
            {
                if (source.Token.WaitHandle.WaitOne(50))
                    return;
                try
                {
                    while (Q.TryDequeue(out LogItem item))
                    {
                        if (filename == null || IsSameDay(logDay, item.DateTime) == false)
                        {
                            logDay = item.DateTime;
                            sequence = 0;
                            realPath = Path.Combine(subPath, $"{DateTime.Now:yyyyMMdd}", name);
                            if (Directory.Exists(realPath) == false)
                            {
                                Directory.CreateDirectory(realPath);
                            }

                            filename = GetLogFileName(logDay, sequence);
                        }
                        while (true)
                        {
                            FileInfo f = new FileInfo(filename);
                            if (f.Exists && f.Length >= capacity)
                            {
                                filename = GetLogFileName(logDay, ++sequence);
                                continue;
                            }
                            break;
                        }
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(filename, true, encoding))
                            {
                                sw.WriteLine(item.Data);
                            }
                        }
                        catch (Exception)
                        {
                            try
                            {
                                string dir = Path.GetDirectoryName(filename);
                                if (!Directory.Exists(dir))
                                {
                                    Directory.CreateDirectory(dir);
                                }
                                using (StreamWriter sw = new StreamWriter(filename, true, encoding))
                                {
                                    sw.WriteLine(item.Data);
                                }
                            }
                            catch { }
                        }
                    }
                }
                catch
                {
                }
            }
        }
    }
}
