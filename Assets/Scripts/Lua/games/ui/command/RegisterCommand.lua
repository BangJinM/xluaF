local RegisterCommand = class('RegisterCommand', framework.MacroCommand)

function RegisterCommand:ctor()
    RegisterCommand.super.ctor(self)
end

function RegisterCommand:initializeMacroCommand()
    -- local RegisterController = requireCommand("register/RegisterControllerCommand")
    local RegisterMediator = require("games.ui.command.registerMediatorCommand")
    -- local RegisterProxy = requireCommand("register/RegisterProxyCommand")

    -- self:addSubCommand(RegisterController)
    -- self:addSubCommand(RegisterProxy)
    self:addSubCommand(RegisterMediator)
end

return RegisterCommand
