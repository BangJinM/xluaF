require("games.ui.common.LayerDefines")
local LayerStack = require("games.ui.common.LayerStack")
local rootStr = "UI"
local uiCameraStr = "UICamera"
local uiRootStr = "Canvas"

local UIManager = {}

function UIManager:init( )
    self.root = createGameObjectByPerfabs("Layer/UI")
    self.root.name = "UIRoot"
    CS.UnityEngine.GameObject.DontDestroyOnLoad(self.root)
    self.uiCamera = getChildByName(self.root.transform, uiCameraStr)
    self.uiRoot = getChildByName(self.root.transform, uiRootStr)
    self.layerStacks = {}

    for k,v in pairs(global.LayerDefine) do
        local gameObject =getChildByName(self.uiRoot, k)
        self.layerStacks[v] = LayerStack:new(gameObject)
    end
    return self
end

function UIManager:pushLayer( Type, layer )
    return self.layerStacks[Type]:push(layer)
end

function UIManager:popLayer( Type )
    self.layerStacks[Type]:pop()
end

return UIManager