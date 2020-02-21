local UIBase = require("games.ui.common.UIBase")
local LoginViewLayer = class("LoginViewLayer", UIBase)

function LoginViewLayer:ctor( )
    self:init()
end

function LoginViewLayer:init( )
     self.loginViewLayer =  createGameObjectByPerfabs("layer/LoginLayer") 
     global.uiManager:pushLayer( global.LayerDefine.TopLayer, self.loginViewLayer)
     local buttonC = self.loginViewLayer.transform:Find("Button"):GetComponent(typeof(CS.UnityEngine.UI.Button))
     buttonC.onClick:AddListener( function(data)
        -- CS.Networks.NetworkManager.RegisterLuaHandler(1, function( str, str2  )
        --     print(str.. str2)
        -- end)
        -- CS.Networks.NetworkManager.SendMessage("djsflsajl")
        local Client = require("framework.network.Client")
        local client = Client.new()

    end)
end

return LoginViewLayer