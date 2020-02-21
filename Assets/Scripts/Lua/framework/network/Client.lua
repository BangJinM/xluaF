local Client =  class("Client")

local ip = "127.0.0.1"
local port = 8003

local networkState = CS.Networks.NetWorkState.CLOSED

function Client:ctor( )
    local client = CS.Networks.Client(ip, port)
    client:setNeworkChangedEventListener(function(state)
        print(state)
        networkState = state
        print("dddddddddddddddddddddddd")
    end)
    client:Connect()
end

return Client