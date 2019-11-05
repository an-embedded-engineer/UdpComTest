using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using UdpComTest;
using System.Threading;

namespace ManagedReceiver
{
    class Program
    {
        static bool IsEndFlag = false;

        static bool ReceiveCallback(string message, IPEndPoint remote_ep)
        {
            Console.WriteLine($"Rx Message : {message}");
            Console.WriteLine($"Remote Host : {remote_ep.Address}:{remote_ep.Port}");

            if (message == "exit")
            {
                IsEndFlag = true;
                return false;
            }
            else
            {
                return true;
            }
        }

        static void Main(string[] args)
        {
            using (var udp_client = new LocalUdpReceiver(2000, ReceiveCallback))
            {
                Console.WriteLine("Start Rx Process");

                udp_client.ListenAsync();

                while (true)
                {
                    if (IsEndFlag == true)
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }

                Console.WriteLine("End Rx Process");
            }
        }
    }
}
