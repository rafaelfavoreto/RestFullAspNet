using RestFullAspNet.Model;
using RestFullAspNet.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestFullAspNet.Repository.Implementations
{
    public class PersonRepositoryImplementation : IPersonRepository
    {          
        private MysqlContext _context;

        public PersonRepositoryImplementation(MysqlContext context)
        {
            _context = context;
        }


        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

        public Person FindByID(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return person;
        }

        public Person Update(Person person)
        {
            // We check if the person exists in the database
            // If it doesn't exist we return an empty person instance
            if (!Exists(person.Id)) return null;

            // Get the current status of the record in the database
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));
            if (result != null)
            {
                try
                {
                    // set changes and save
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return person;
        }
        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        private bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
