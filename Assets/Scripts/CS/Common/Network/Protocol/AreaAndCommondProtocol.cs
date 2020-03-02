namespace Networks
{
    public class AreaAndCommondProtocol
    {

        //---------------------Area---------------------------//
        public const int RegisterRequest = 5; //请求注册
        public const int LoginRequest = 1;     // 请求登陆


        

        //---------------------登录---------------------------//

        public const int Login_InvalidMessage = 1;//无效消息
        public const int Login_InvalidUsername = 2;//无效用户名
        public const int Login_InvalidPassword = 3;//密码错误
        public const int Login_DisConnect = 4;      //第二个客服端登陆断开连接
        public const int Login_Succeed = 10;//登陆成功

        //---------------------注册---------------------------//
        
        public const int RegisterFail = 6;//注册失败
        public const int RegisterSuccess = 7;//注册成功
        public const int Register_InvalidUsername = 8;

    }
}