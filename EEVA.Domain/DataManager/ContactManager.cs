using EEVA.Domain.Models;
using EEVA.Domain.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EEVA.Domain.Models.DataManager
{
    public class ContactManager : IDataRepository<Contact>
    {

        readonly EEVAContext _eevaContext;

        public ContactManager(EEVAContext context)
        {
            _eevaContext = context;
        }

        public ContactManager()
        {
        }

        public void Add(Contact entity)
        {
            _eevaContext.Contacts.Add(entity);
            _eevaContext.SaveChanges();
        }

        public void Delete(Contact entity)
        {
            _eevaContext.Contacts.Remove(entity);
            _eevaContext.SaveChanges();
        }

        public Contact Get(int id)
        {
            return _eevaContext.Contacts.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Contact> GetAll()
        {
            return _eevaContext.Contacts.ToList();
        }

        public void Update(Contact dbEntity, Contact entity)
        {
            dbEntity.FirstName = entity.FirstName;
            dbEntity.LastName = entity.LastName;
            dbEntity.Email = entity.Email;
            dbEntity.PhoneNumber = entity.PhoneNumber;

            _eevaContext.SaveChanges();
        }

        public IEnumerable<Contact> Search(string keyword)
        {
            keyword = keyword.ToUpper();

            List<Contact> contacts = new List<Contact>();

            IEnumerable<Contact> entities = _eevaContext.Contacts
                .Where(c => c.FirstName.ToUpper().Contains(keyword)
                            ||
                            c.LastName.ToUpper().Contains(keyword));
            /*foreach (var entity in entities)
            {
                contacts.Add(new Contact()
                {
                    Id = entity.Id,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Email = entity.Email,
                    PhoneNumber = entity.PhoneNumber

                });
            }*/
            return contacts;

        }
    }
}
