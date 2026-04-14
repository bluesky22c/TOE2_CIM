using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LGCIM
{
    // EServer, EClient와 유사한 인터페이스를 제공하는 JSON 기반 통신 클래스
    public class JsonClient<T> where T : class
    {
        private string _host;
        private int _port;

        public JsonClient(object owner, string host, int port, object unused)
        {
            _host = host;
            if (_host.ToLower() == "localhost") _host = "127.0.0.1";
            _port = port;
        }

        public T Send(T data)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    // 연결 시도
                    var result = client.BeginConnect(_host, _port, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(3000); // 3초 타임아웃
                    if (!success || !client.Connected)
                    {
                        // throw new Exception($"Failed to connect to {_host}:{_port}");
                        return null;
                    }

                    using (var stream = client.GetStream())
                    {
                        stream.ReadTimeout = 5000; // 5초 읽기 타임아웃
                        stream.WriteTimeout = 5000;

                        // JSON 직렬화
                        string json = JsonConvert.SerializeObject(data);
                        byte[] buffer = Encoding.UTF8.GetBytes(json);
                        byte[] lengthBuffer = BitConverter.GetBytes(buffer.Length);

                        // 길이(4바이트) + 데이터 전송
                        stream.Write(lengthBuffer, 0, 4);
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Flush();

                        // 응답 길이 수신
                        byte[] respLengthBuffer = new byte[4];
                        int bytesRead = 0;
                        try {
                            bytesRead = stream.Read(respLengthBuffer, 0, 4);
                        } catch {
                            return null;
                        }
                        
                        if (bytesRead < 4) return null;

                        int respLength = BitConverter.ToInt32(respLengthBuffer, 0);
                        byte[] respBuffer = new byte[respLength];
                        int totalRead = 0;
                        while (totalRead < respLength)
                        {
                            int read = stream.Read(respBuffer, totalRead, respLength - totalRead);
                            if (read == 0) break;
                            totalRead += read;
                        }

                        if (totalRead < respLength) return null;

                        string respJson = Encoding.UTF8.GetString(respBuffer);
                        return JsonConvert.DeserializeObject<T>(respJson);
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }

    public class JsonServer<T> where T : class
    {
        private TcpListener _listener;
        private bool _isRunning;
        private Func<object, T, T> _onReceive;
        private object _owner;

        public JsonServer(object owner, Func<object, T, T> onReceive, object unused)
        {
            _owner = owner;
            _onReceive = onReceive;
        }

        public void Listen(int port)
        {
            try {
                _listener = new TcpListener(IPAddress.Any, port);
                _listener.Start();
                _isRunning = true;

                Task.Run(() => AcceptClients());
            } catch {
                throw;
            }
        }

        private void AcceptClients()
        {
            while (_isRunning)
            {
                try
                {
                    var client = _listener.AcceptTcpClient();
                    Task.Run(() => HandleClient(client));
                }
                catch
                {
                    // Ignore accept errors
                }
            }
        }

        private void HandleClient(TcpClient client)
        {
            using (client)
            using (var stream = client.GetStream())
            {
                try
                {
                    stream.ReadTimeout = 5000;
                    stream.WriteTimeout = 5000;

                    // 데이터 길이 수신
                    byte[] lengthBuffer = new byte[4];
                    if (stream.Read(lengthBuffer, 0, 4) < 4) return;

                    int length = BitConverter.ToInt32(lengthBuffer, 0);
                    byte[] buffer = new byte[length];
                    int totalRead = 0;
                    while (totalRead < length)
                    {
                        int read = stream.Read(buffer, totalRead, length - totalRead);
                        if (read == 0) break;
                        totalRead += read;
                    }

                    if (totalRead < length) return;

                    // JSON 역직렬화
                    string json = Encoding.UTF8.GetString(buffer);
                    T data = JsonConvert.DeserializeObject<T>(json);

                    // 콜백 호출
                    T response = _onReceive(_owner, data);

                    // 응답 전송
                    if (response != null)
                    {
                        string respJson = JsonConvert.SerializeObject(response);
                        byte[] respBuffer = Encoding.UTF8.GetBytes(respJson);
                        byte[] respLengthBuffer = BitConverter.GetBytes(respBuffer.Length);

                        stream.Write(respLengthBuffer, 0, 4);
                        stream.Write(respBuffer, 0, respBuffer.Length);
                        stream.Flush();
                    }
                }
                catch
                {
                    // Ignore handle errors
                }
            }
        }

        public void Stop()
        {
            _isRunning = false;
            _listener?.Stop();
        }
    }
}
