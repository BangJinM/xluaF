local UIManager = require("UIManager")

local PERFAB_NAME = {
    MESSAGEBOX = "MESSAGEBOX",
    LOGINPANEL = "LOGINPANEL"
}

local LayerList = {
    [PERFAB_NAME.MESSAGEBOX] = "Prefabs/MessageBox",
    [PERFAB_NAME.LOGINPANEL] = "Prefabs/LoginPanel"
}

global = global or {}
global.LayerType = PERFAB_NAME
global.LayerList = LayerList
global.UIManager = UIManager()
