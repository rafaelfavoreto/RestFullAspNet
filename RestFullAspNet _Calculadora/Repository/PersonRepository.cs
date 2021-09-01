using RestFullAspNet.Model;
using RestFullAspNet.Model.Context;
using RestFullAspNet.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFullAspNet.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MysqlContext context) : base(context) { }
        public Person Disable(long id)
        {
            if (!_context.Persons.Any(p => p.Id.Equals(id))) return null;
            var user = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if(user != null)
            {
                user.Enabled = false;
                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }                
            }
            return user;
        }

        public List<Person> FindByName(string firstName, string lastName)
        {
            //Rafael
            //string name = firstName;
            //string lastN = lastName;

            //if(!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(lastName))
            //{
            //    return _context.Persons.Where(p => p.FirstName.Contains(name) && p.LastName.Contains(lastN)).ToList();
            //}
            //return null;

            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(p => p.FirstName.Contains(firstName)
                       && p.LastName.Contains(lastName)).ToList();
            }
            else if(!string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(p => p.FirstName.Contains(firstName)).ToList();
            }
            else if (string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(p => p.LastName.Contains(lastName)).ToList();
            }          
                return null;
        }  
    }
}
