using RestFullAspNet.Data.VO;

namespace RestFullAspNet.Business
{
    public  interface IloginBusiness
    {
        TokenVO ValidateCredentials(UserVO user);
        TokenVO ValidateCredentials(TokenVO token);
        bool RevokeToken(string username);
    }
}
