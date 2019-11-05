using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace UdpComTest
{
    public sealed class LocalUdpSender : IDisposable
    {
        private string RemoteHostAddress { get; }

        private UdpClient Client { get; }

        public LocalUdpSender()
        {
            this.RemoteHostAddress = "127.0.0.1";

            this.Client = new UdpClient();
        }

        public void Send(string message, int port)
        {
            byte[] msg_bytes = Encoding.UTF8.GetBytes(message);

            int msg_len = msg_bytes.Length;

            this.Client.Send(msg_bytes, msg_len, this.RemoteHostAddress, port);
        }

        public void SendAsync(string message, int port)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);

            int length = buffer.Length;

            this.Client.BeginSend(buffer, length, this.RemoteHostAddress, port, this.SendCallback, this.Client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            var client = ar.AsyncState as UdpClient;

            try
            {
                client.EndSend(ar);
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Socket Error : {ex.Message} Error Code : {ex.ErrorCode}");
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine($"Socket Closed Error : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unknown Error : {ex.Message}");
            }
        }

        public void Close()
        {
            this.Client.Close();
        }

        public void Dispose()
        {
            this.Client.Dispose();
        }
    }
}
