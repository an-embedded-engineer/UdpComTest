#include "pch.h"

#include "LibCLI.h"

#pragma managed
#include <gcroot.h>
#include <msclr/marshal_cppstd.h>

using namespace System;

namespace LibCLI
{
    public class UdpSenderImpl : public UdpSender
    {
    public:
        UdpSenderImpl()
        {
            this->m_ManagedUdpSender = gcnew UdpComTest::LocalUdpSender();
        }

        ~UdpSenderImpl()
        {
            this->m_ManagedUdpSender->Close();
        }

        void Send(std::string& message, int port) override
        {
            System::String^ managed_message = msclr::interop::marshal_as<System::String^>(message);

            this->m_ManagedUdpSender->Send(managed_message, port);
        }

    private:
        gcroot<UdpComTest::LocalUdpSender^> m_ManagedUdpSender;
    };

    std::shared_ptr<UdpSender> CreateUdpSender()
    {
        return std::make_shared<UdpSenderImpl>();
    }
}
