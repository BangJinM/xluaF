local breakSocketHandle, debugXpCall = require("LuaDebug")("localhost", 7003)

local function main()
    print("hello xluaFrame ...")
    require("framework.init")
    require("games.initGames")
    -- require("games.ui.view.LoginViewLayer"):init()
    local enterWorldCommand  = require( "games.ui.command.RegisterCommand" )
    global.Facade:registerCommand( "Register", enterWorldCommand )
    global.Facade:sendNotification("Register")
    global.Facade:sendNotification("Layer_WorldMiniMap_Open")
end

local function logtraceback(msg)
    local tracemsg = debug.traceback()
    print("error: " .. tostring(msg) .. "\n" .. tracemsg)
    return msg
end

local ret, msg = xpcall(main, logtraceback)
if not ret then
    error("\n" .. "lua error msg:" .. "\n" .. "\t" .. msg)
end
