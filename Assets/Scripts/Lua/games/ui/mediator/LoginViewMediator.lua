local BaseNeduatir = require("games.ui.mediator.BaseNeduatir")

local LoginViewMediator = class("LoginViewMediator", BaseNeduatir)

LoginViewMediator.NAME = "LoginViewMediator"

function LoginViewMediator:listNotificationInterests()
    return {
        "Layer_WorldMiniMap_Open",
        "Layer_WorldMiniMap_Close",
        "Layer_WorldMiniMap_UnLockCity",
    }
end

function LoginViewMediator:handleNotification(notification)
    local id = notification:getName()
    local data = notification:getBody()

    if "Layer_WorldMiniMap_Open" == id then
        self:OpenLayer(data)
    end
end

function LoginViewMediator:OpenLayer( )
    require("games.ui.view.LoginViewLayer").new()
end

return LoginViewMediator