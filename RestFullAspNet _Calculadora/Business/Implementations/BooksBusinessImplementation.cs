using RestFullAspNet.Model;
using RestFullAspNet.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFullAspNet.Business.Implementations
{
    public class BooksBusinessImplementation : IBooksBusiness
    {
        private IBooksRepository _repository;
        public BooksBusinessImplementation(IBooksRepository repository)
        {
            _repository = repository;
        }
        public Books Create(Books books)
        {
            return _repository.Create(books);
        }

        public void Delete(long id)
        {
             _repository.Delete(id);
        }

        public List<Books> FindAll()
        {
            return _repository.FindAll();
        }

        public Books FindByID(long id)
        {
            return _repository.FindByID(id);
        }

        public Books Update(Books books)
        {
            return _repository.Update(books);
        }
    }
}
