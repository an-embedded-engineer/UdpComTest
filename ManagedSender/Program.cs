using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using UdpComTest;

namespace ManagedSender
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sender = new LocalUdpSender())
            {
                Console.WriteLine("Start Tx Process");

                while (true)
                {
                    Console.Write("Input Tx Message>");
                    var message = Console.ReadLine();

                    sender.SendAsync(message, 2000);

                    if (message == "exit")
                    {
                        break;
                    }
                }

                Console.WriteLine("End Tx Process");
            }
        }
    }
}
