local UIBase = require("games.ui.common.UIBase")
local LoginViewLayer = class("LoginViewLayer", UIBase)

function LoginViewLayer:ctor( )
    
end

function LoginViewLayer:init( )
     self.loginViewLayer =  createGameObjectByPerfabs("layer/LoginLayer") 
     global.uiManager:pushLayer( global.LayerDefine.TopLayer, self.loginViewLayer)
     local buttonC = self.loginViewLayer.transform:Find("Button"):GetComponent(typeof(CS.UnityEngine.UI.Button))
     buttonC.onClick:AddListener( function(data)
        print("data.info.name")
    end)
end

return LoginViewLayer