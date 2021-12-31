local breakSocketHandle, debugXpCall = require("LuaDebug.lua")("localhost", 7003) --本机调试 win mac

function main()
    local network = CS.US.Net.SocketClient()
    network:SetNetStateChanged(
        function(netState)
            if netState == CS.US.Net.NetState.Close then
                print("aaaaaaaaaaaaaaaaaaa net State close")
            end
        end
    )
    local connected = network:Connect("127.0.0.1", 10086)
end

main()
