using RestFullAspNet.Business;
using RestFullAspNet.Data.Convert.Implementations;
using RestFullAspNet.Model;
using RestFullAspNet.Repository;
using System.Collections.Generic;

namespace RestFullAspNet.Business.Implementations
{
    public class BooksBusinessImplementation : IBooksBusiness
    {
        private readonly IRepository<Books> _reposiory;
        private readonly BookConverter _converter;

        public BooksBusinessImplementation(IRepository<Books> reposiory)
        {
            _reposiory = reposiory;
        }
        public Books Create(Books books)
        {
            return _reposiory.Create(books);
        }

        public void Delete(long id)
        {
             _reposiory.Delete(id);
        }
       
        public List<Books> FindAll()
        {
            return _reposiory.FindAll();
        }

        public Books FindByID(long id)
        {
            return _reposiory.FindByID(id);
        }

        public Books Update(Books books)
        {
            return _reposiory.Update(books);
        }
       
    }
}
