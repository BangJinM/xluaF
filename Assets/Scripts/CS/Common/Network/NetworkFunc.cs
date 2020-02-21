using XLua;
namespace Networks
{
   [CSharpCallLua]
    public delegate void NetworkStateChange(NetWorkState state);

    [CSharpCallLua]
    public delegate void LuaHandlerAction(string a, string b);
}