#ifndef LIBCLI_H
#define LIBCLI_H

#pragma unmanaged
#include <memory>
#include <string>

namespace LibCLI
{
    class UdpSender
    {
    public:
        UdpSender() = default;
        virtual ~UdpSender() = default;
        virtual void Send(std::string& message, int port) = 0;
        virtual void SendAsync(std::string& message, int port) = 0;
    private:
        UdpSender(const UdpSender&) = delete;
        UdpSender(UdpSender&&) = delete;
        UdpSender& operator=(const UdpSender&) = delete;
        UdpSender&& operator=(UdpSender&&) = delete;
    };

    extern std::shared_ptr<UdpSender> CreateUdpSender();
}
#endif
