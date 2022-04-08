local breakSocketHandle, debugXpCall = require("LuaDebug.lua")("localhost", 7003) --本机调试 win mac
local util = require("xlua.util")
local Coroutine = {}

function Coroutine.Start(...)
    return CS.US.DispatchMessageController.Instance:StartCoroutine(util.cs_generator(...))
end

function Coroutine.Stop(c)
    CS.US.DispatchMessageController.Instance:StopCoroutine(c)
end

function Coroutine.StopAll()
    CS.US.DispatchMessageController.Instance:StopAllCoroutines()
end

function Coroutine.Yield(enumerator)
    coroutine.yield(enumerator)
end

function Coroutine.Wait(time)
    coroutine.yield(CS.UnityEngine.WaitForSeconds(time))
end

function Coroutine.Break()
    coroutine.yield(util.move_end)
end

function main()
    local network = CS.US.TcpSocket()
    network:SetSocketStatusChangedImpl(
        function(netState)
            print("aaaaaaaaa")
            if netState == CS.US.TCPSocketStatus.Connected then
                print("ddddddddddddddddddddddddd")
            end
        end
    )

    local connected = network:Connect("127.0.0.1", 12345)
    Coroutine.Start(
        function()
            Coroutine.Wait(2)
            print("Wait for 2 seconds")
            local message = CS.US.Message()
            network:SendMsg(message, false)
        end
    )
    
    CS.US.DispatchMessageController.Instance:RegisterMsgHandler(
        0,
        function(message)
            print("dsadasfas111 \n".. message:ToString())
        end
    )

    CS.US.DispatchMessageController.Instance:RegisterMsgHandler(
        0,
        function(message)
            print("dsadasfas222 \n".. message:ToString())
        end
    )
end

main()
