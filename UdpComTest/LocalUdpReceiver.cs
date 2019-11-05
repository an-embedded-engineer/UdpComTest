using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace UdpComTest
{
    public sealed class LocalUdpReceiver : IDisposable
    {
        private UdpClient Client { get; }

        private Func<string, IPEndPoint, bool> Callback { get; }

        public LocalUdpReceiver(int port, Func<string, IPEndPoint, bool> callback = null)
        {
            var local_ep = new IPEndPoint(IPAddress.Any, port);

            this.Client = new UdpClient(local_ep);

            this.Callback = callback;
        }

        public bool Listen()
        {
            var remote_ep = default(IPEndPoint);

            var buffer = this.Client.Receive(ref remote_ep);

            var message = Encoding.UTF8.GetString(buffer);

            var result = this.Callback?.Invoke(message, remote_ep) ?? true;

            return result;
        }

        public void ListenAsync()
        {
            this.Client.BeginReceive(this.ReceiveCallback, this.Client);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            var client = ar.AsyncState as UdpClient;

            try
            {
                var remote_ep = default(IPEndPoint);

                var buffer = client.EndReceive(ar, ref remote_ep);

                var message = Encoding.UTF8.GetString(buffer);

                var result = this.Callback?.Invoke(message, remote_ep) ?? true;

                if (result == true)
                {
                    client.BeginReceive(ReceiveCallback, client);
                }
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

        public void Dispose()
        {
            this.Client.Close();
            this.Client.Dispose();
        }
    }
}
