local UIBase = class("UIBase")

local LayerDefines = require("games.ui.common.LayerDefines")

function UIBase:ctor( )
end

function UIBase:_createUI( layerType, layerPath)
    assert(layerPath ~= "" or  layerPath ~= nil)
    layerType = layerType or global.LayerDefine.NormalLayer
    layerPath =  layerPath or ""
    return global.uiManager:pushLayer( layerType, layerPath)
end

return UIBase