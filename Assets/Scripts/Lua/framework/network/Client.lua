local Client =  class("Client")

local ip = "127.0.0.1"
local port = 8080

local networkState = CS.Networks.NetWorkState.CLOSED

function Client:ctor( )
    self.client = CS.Networks.Client(ip, port)
    self.client:setNeworkChangedEventListener(function(state)
        print(state)
        networkState = state
        print("dddddddddddddddddddddddd")
    end)
    self.client:Connect()
    self.client:RegisterLuaHandler(1, function( message )
        print("type:"..message:GetType())
        print("area:"..message:GetArea())
        print("command:"..message:GetCommand())
        print("message:"..message:GetMessage())
    end)
end

function Client:SendMsg( type, area, command, message )
    self.client:SendMsg(type, area, command, message)
end

return Client