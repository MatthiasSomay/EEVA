using EEVA.Domain.Models;
using EEVA.Domain.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EEVA.Domain.DataManager.Dummies
{
    class DummyContactManager : IDataManager<Contact>
    {
        private static List<Contact> _contacts = new List<Contact>();

        static DummyContactManager()
        {
            _contacts.Add(new Teacher()
            {
                Id = 1,
                FirstName = "Kenneth",
                LastName = "Van Den Borne",
                Email = "kenneth@live.be",
                PhoneNumber = "01637828929"
            });

            _contacts.Add(new Student()
            {
                Id = 2,
                FirstName = "Matthias",
                LastName = "Somay",
                Email = "somay@live.be",
                PhoneNumber = "0467262789"
            });
        }
        public void Add(Contact entity)
        {
            int maxId = _contacts.Max(c => c.Id);
            entity.Id = maxId + 1;
            _contacts.Add(entity);
           
        }

        public void Delete(Contact entity)
        {
            if (entity == null) return;
            _contacts.Remove(entity);
        }

        public Contact Get(int? id)
        {
            return _contacts.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Contact> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contact> Search(string keyword)
        {
            keyword = keyword.ToUpper();

            IEnumerable<Contact> entities = _contacts
                .Where(c => c.FirstName.ToUpper().Contains(keyword) || c.LastName.ToUpper().Contains(keyword) || c.Email.ToUpper().Contains(keyword));

            return entities.ToList();
        }

        public void Update(Contact entity)
        {
            Contact existingContact = Get(entity.Id);
            if (existingContact == null) throw new ArgumentException("Contact does not exist");
            else
            {
                existingContact.FirstName = entity.FirstName;
                existingContact.LastName = entity.LastName;
                existingContact.Email = entity.Email;
               
            }
        }
    }
}
