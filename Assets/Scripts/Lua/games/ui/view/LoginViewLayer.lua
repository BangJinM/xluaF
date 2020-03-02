local UIBase = require("games.ui.common.UIBase")
local LoginViewLayer = class("LoginViewLayer", UIBase)

function LoginViewLayer:ctor( )
    self:init()
end

function LoginViewLayer:init( )
     self.loginViewLayer =  createGameObjectByPerfabs("layer/LoginLayer") 
     global.uiManager:pushLayer( global.LayerDefine.TopLayer, self.loginViewLayer)
     local buttonC = self.loginViewLayer.transform:Find("Button"):GetComponent(typeof(CS.UnityEngine.UI.Button))
     local Client = require("framework.network.Client")
     local client = Client.new()
     buttonC.onClick:AddListener( function(data)
        client:SendMsg(1, 1, 1, "jldjal")
    end)
end

return LoginViewLayer