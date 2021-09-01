using RestFullAspNet.Data.VO;
using RestFullAspNet.Model;
using System.Collections.Generic;

namespace RestFullAspNet.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindByID(long id);

        List<PersonVO> FindByName(string fisrtName, string lastName);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO person);
        PersonVO Disable(long id);
        void Delete(long id);
    }
}
