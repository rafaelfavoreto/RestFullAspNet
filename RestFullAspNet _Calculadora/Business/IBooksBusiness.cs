using RestFullAspNet.Data.VO;
using RestFullAspNet.Model;
using System.Collections.Generic;

namespace RestFullAspNet.Business
{
    public interface IBooksBusiness 
    {
        BookVO Create(BookVO books);
        BookVO FindByID(long id);
        BookVO Update(BookVO books);
        List<BookVO> FindAll();
        void Delete(long id);
    }
}
