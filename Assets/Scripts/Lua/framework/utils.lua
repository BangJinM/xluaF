--@desc 加载预制体资源
--@unity_type: 资源类型
--@pb_path: 资源路径
function loadPerfabs( pb_path, unity_type )
    local asset = nil
    if not isNull(unity_type) and pb_path ~= nil then
        print(unity_type)
        asset = CS.AssetManager.LoadAsset(pb_path, unity_type)
        return asset
    end
    return asset
end

function createGameObjectByPerfabs(  pb_path, parentTransform )
    local perfabs = loadPerfabs(pb_path, typeof(CS.UnityEngine.GameObject))

    if not isNull(perfabs) then
        return CS.UnityEngine.Object.Instantiate(perfabs, parentTransform)
    end
end

--@desc 检测unity对象是否为null
--@unity_object: unity对象
function isNull(unity_object)
    if unity_object == nil then
        return true
    end
    if type(unity_object) == "userdata" and unity_object.IsNull ~= nil then
        return unity_object:IsNull()
    end
    return false
end

--@desc 递归查找unity对象transform
--@roottf: 根节点的transform
--@name: 节点名称
function getChildByName(roottf, name)
    if isNull(roottf) or not name then
        return nil
    end
    local child = roottf:Find(name)
    if not isNull(child) then
        return child
    end
    local go = nil
    for i = 0, roottf.childCount - 1 do
        local child = roottf:GetChild(i)
        go = getChildByName(child, name)
        if not isNull(go) then
            return go
        end
    end
    return nil
end

function resetTransform( transform )
    transform.localPosition = CS.UnityEngine.Vector3(0,0,0)
    transform.localRotation = CS.UnityEngine.Quaternion.identity
    transform.localScale = CS.UnityEngine.Vector3(1,1,1)
end