local RegisterMediatorCommand = class('RegisterMediatorCommand', framework.SimpleCommand)

function RegisterMediatorCommand:execute(note)
    self:RegisterNormalLayerMediator()
end

function RegisterMediatorCommand:RegisterNormalLayerMediator()
    local registerMediators = {
        "games.ui.mediator.LoginViewMediator"
    }
    for k,v in pairs(registerMediators) do
        local Mediator = require(v)
        local mediatorInst = Mediator.new()
        global.Facade:registerMediator(mediatorInst)
    end
end

return RegisterMediatorCommand