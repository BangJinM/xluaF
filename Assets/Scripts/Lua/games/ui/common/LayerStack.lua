local stack = require("framework.stack")
local LayerStack = {}
LayerStack.__index = LayerStack

function LayerStack:new( uiRoot )
    local t = {uiRoot = uiRoot, layerStack = stack:new()}
    return setmetatable(t, LayerStack)
end

function LayerStack:push( layer )
    if isNull(self.uiRoot.transform) then return end
    -- local layer = createGameObjectByPerfabs(layerPath, self.uiRoot.transform)
    layer.transform:SetParent(self.uiRoot.transform)
    resetTransform(layer.transform)
    if not isNull(layer) then 
        layer.transform:SetParent(self.uiRoot.transform)
        self.layerStack:push(layer)
        return layer
    end
    return nil
end

function LayerStack:pop( )
    local layer = stack:peek()
    layer.transform:Destroy()
    self.layerStack:pop()
end

setmetatable(LayerStack, {__call = LayerStack.new})

return LayerStack