using RestFullAspNet.Data.VO;
using RestFullAspNet.Hypermedia.Uteis;
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
        PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
    }
}
