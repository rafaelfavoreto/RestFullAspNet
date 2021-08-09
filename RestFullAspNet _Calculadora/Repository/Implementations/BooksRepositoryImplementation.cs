using RestFullAspNet.Model;
using RestFullAspNet.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestFullAspNet.Repository.Implementations
{
    public class BooksRepositoryImplementation : IBooksRepository
    {
        private MysqlContext _context;

        public BooksRepositoryImplementation(MysqlContext context)
        {
            _context = context;
        }
        public Books Create(Books books)
        {
            try
            {
                _context.Add(books);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            return books;
        }

        public void Delete(long id)
        {
            var result = _context.Books.SingleOrDefault(p => p.id.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.Books.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public List<Books> FindAll()
        {
            return _context.Books.ToList();
        }

        public Books FindByID(long id)
        {
            return _context.Books.SingleOrDefault(p => p.id.Equals(id));
        }

        public Books Update(Books books)
        {
            if (!Exists(books.id)) return null;

            var result = _context.Books.SingleOrDefault(p => p.id.Equals(books.id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(books);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }                
            }
            return books;
        }

        private bool Exists(long id)
        {
            return _context.Books.Any(p => p.id.Equals(id));
        }
    }
}
