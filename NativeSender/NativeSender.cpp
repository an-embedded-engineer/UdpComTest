#include <iostream>
#include "LibCLI.h"

int main()
{
    auto sender = LibCLI::CreateUdpSender();

    std::cout << "Start Tx Process" << std::endl;

    while (true)
    {
        std::cout << "Input Tx Message>";
        std::string message;
        std::cin >> message;
        sender->SendAsync(message, 2000);

        if (message == "exit")
        {
            break;
        }
    }

    std::cout << "End Tx Process" << std::endl;
}

