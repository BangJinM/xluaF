global = global or {}
global.uiManager = nil
local function init(  )
    local UIManager = require("games.ui.common.UIManager")
    global.uiManager = UIManager:init()
end

init()