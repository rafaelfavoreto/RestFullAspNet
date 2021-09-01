using RestFullAspNet.Configuration;
using RestFullAspNet.Data.VO;
using RestFullAspNet.Model;
using RestFullAspNet.Repository.Generic;
using RestFullAspNet.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestFullAspNet.Business.Implementations
{
    public class LoginBusinessImplementation : IloginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;
        private IUserRepository _repository;
        private readonly ITokenServices _tokenService;

        public LoginBusinessImplementation(TokenConfiguration configuration, IUserRepository repository, ITokenServices tokenService)
        {
            _configuration = configuration;
            _repository = repository;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredentials(UserVO userCredentials)
        {
            var user = _repository.ValidateCredentials(userCredentials);
            if (user == null) return null;
            var claims = new List<Claim>
            {
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString("N")),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
             };

            var acessToken = _tokenService.GenerateAcessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

            _repository.RefreshUserInfo(user);

            return NewTokenVO(acessToken, refreshToken);

            //DateTime createDate = DateTime.Now;
            //DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);



            //return new TokenVO(
            //    true,
            //    createDate.ToString(DATE_FORMAT),
            //    expirationDate.ToString(DATE_FORMAT),
            //    acessToken,
            //    refreshToken);
        }

        public TokenVO ValidateCredentials(TokenVO token)
        {
            var acessToken = token.AcessToken;
            var refreshToken = token.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(acessToken);

            var username = principal.Identity.Name;

            var user = _repository.ValidateCredentials(username);
            if (user == null ||
                user.RefreshToken != refreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now) return null;

            user.RefreshToken = refreshToken;
            _repository.RefreshUserInfo(user);

            return NewTokenVO(acessToken, refreshToken);

            //DateTime createDate = DateTime.Now;
            //DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            //return new TokenVO(
            //    true,
            //    createDate.ToString(DATE_FORMAT),
            //    expirationDate.ToString(DATE_FORMAT),
            //    acessToken,
            //    refreshToken);
        }

        public TokenVO NewTokenVO(string acessToken , string refreshToken)
        {
            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                acessToken,
                refreshToken);
        }

        public bool RevokeToken(string username)
        {
            return _repository.RevokeToken(username);
        }
    }
}
