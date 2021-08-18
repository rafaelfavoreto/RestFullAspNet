using RestFullAspNet.Data.VO;
using RestFullAspNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFullAspNet.Repository.Generic
{
    public interface IUserrepository
    {
        User ValidateCredentials(UserVO user);
    }
}
