using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Engion
{
    public enum LogType
    {
        Information,
        Warning,
        Error,
        Verbose
    }

    public class LogEventArgs : EventArgs
    {
        public LogType Type { get; set; }
        public int ID { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;

        public LogEventArgs(LogType type, int id, string message)
        {
            Type = type;
            ID = id;
            Message = message;
        }
    }

    public class LogEx
    {
        private string _logPath;
        public EventHandler<LogEventArgs> OnLogEvent;

        public LogEx(string path)
        {
            _logPath = path;
            if (!Directory.Exists(_logPath))
            {
                Directory.CreateDirectory(_logPath);
            }
        }

        public void AddLogMessage(LogType type, int id, string message)
        {
            var args = new LogEventArgs(type, id, message);
            OnLogEvent?.Invoke(this, args);
            
            // Console output for debugging
            Console.WriteLine($"[{type}] {message}");

            // Basic file logging
            try
            {
                string fileName = Path.Combine(_logPath, $"{DateTime.Now:yyyyMMdd}.log");
                string logLine = $"[{DateTime.Now:HH:mm:ss.fff}] [{type}] {message}";
                File.AppendAllText(fileName, logLine + Environment.NewLine);
            }
            catch { }
        }

        public void Information(string message)
        {
            AddLogMessage(LogType.Information, 0, message);
        }

        public void sInformation(string message)
        {
            Information(message);
        }

        public void Error(string message)
        {
            AddLogMessage(LogType.Error, 0, message);
        }
        
        public void Warning(string message)
        {
            AddLogMessage(LogType.Warning, 0, message);
        }

        public void Verbose(string message)
        {
            AddLogMessage(LogType.Verbose, 0, message);
        }
    }

    public static class Global
    {
        public static LogEx Log { get; set; }
    }

    public class LogViewer : RichTextBox
    {
        public LogEx Log { get; set; }
        public string Format { get; set; } = "[{0:MM-dd HH:mm:ss.fff}] {3}";

        public LogViewer()
        {
            this.ReadOnly = true;
            this.BackColor = Color.FromArgb(32, 32, 32);
            this.ForeColor = Color.Gainsboro;
        }

        public LogViewer(System.ComponentModel.IContainer container) : this()
        {
            if (container != null)
            {
                container.Add(this);
            }
        }

        public void ManageLog(LogEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => ManageLog(e)));
                return;
            }

            try
            {
                string msg = string.Format(Format, e.DateTime, e.Type, e.ID, e.Message);
                
                // Color based on type
                Color color = Color.Gainsboro;
                switch (e.Type)
                {
                    case LogType.Error: color = Color.Red; break;
                    case LogType.Warning: color = Color.Gold; break;
                    case LogType.Verbose: color = Color.Silver; break;
                }

                this.SelectionStart = this.TextLength;
                this.SelectionLength = 0;
                this.SelectionColor = color;
                this.AppendText(msg + Environment.NewLine);
                
                // Limit lines
                if (this.Lines.Length > 1000)
                {
                    this.Select(0, this.GetFirstCharIndexFromLine(this.Lines.Length - 1000));
                    this.SelectedText = "";
                }

                this.ScrollToCaret();
            }
            catch { }
        }
    }
}
