using EEVA.Domain.Models;
using EEVA.Domain.Models.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EEVA.Domain.DataManager
{
    public class ContactManager : IDataManager<Contact>
    {
        private readonly EEVAContext _eevaContext;

        public ContactManager()
        {
        }

        public ContactManager(EEVAContext context)
        {
            _eevaContext = context;
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

        public Contact Get(int? id)
        {
            Contact contact = _eevaContext.Contacts.FirstOrDefault(c => c.Id == id);
            contact.FullName = SetFullName(contact);
            return contact;
        }

        public IEnumerable<Contact> GetAll()
        {
            List<Contact> contacts = _eevaContext.Contacts.ToList();
            foreach (Contact contact in contacts)
            {
                contact.FullName = SetFullName(contact);
            }
            return contacts;
        }

        //Get all Teachers
        //TODO make method generic
        public IEnumerable<Teacher> GetAllTeachers()
        {
            List<Teacher> teachers = _eevaContext.Contacts.OfType<Teacher>().ToList();
            foreach (Teacher teacher in teachers)
            {
                teacher.FullName = SetFullName(teacher);
            }
            return teachers;

        }

        public IEnumerable<Contact> Search(string keyword)
        {
            keyword = keyword.ToUpper();
            
            IEnumerable<Contact> entities = _eevaContext.Contacts
                .Where(c => c.FirstName.ToUpper().Contains(keyword) || c.LastName.ToUpper().Contains(keyword) || c.Email.ToUpper().Contains(keyword));
            
            return entities.ToList();

        }

        public void Update(Contact entity)
        {
            Contact dbEntity = Get(entity.Id);
            dbEntity.FirstName = entity.FirstName;
            dbEntity.LastName = entity.LastName;
            dbEntity.Email = entity.Email;
            dbEntity.PhoneNumber = entity.PhoneNumber;
            _eevaContext.SaveChanges();
        }

        public string SetFullName(Contact contact)
        {
            return contact.LastName + " " + contact.FirstName;
        }
    }
}
