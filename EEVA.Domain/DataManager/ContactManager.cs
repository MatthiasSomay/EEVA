using EEVA.Domain.Models;
using EEVA.Domain.Models.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EEVA.Domain.DataManager
{
    public class ContactManager : IDataManager<Contact>
    {
        public EEVAContext _eevaContext;

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
            return _eevaContext.Contacts.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Contact> GetAll()
        {
            return _eevaContext.Contacts.ToList();
        }

        //Get all Teachers
        //TODO make method generic
        public IEnumerable<Teacher> GetAllTeachers()
        {
            return _eevaContext.Contacts.OfType<Teacher>().ToList();
        }

        public IEnumerable<Contact> Search(string keyword)
        {
            keyword = keyword.ToUpper();
            
            return _eevaContext.Contacts
                .Where(c => c.FirstName.ToUpper()
                .Contains(keyword) || c.LastName.ToUpper()
                .Contains(keyword) || c.Email.ToUpper()
                .Contains(keyword)).ToList();
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

        //Getting the contact by the user logged in via Identity framework
        public Contact GetByEmail(ClaimsPrincipal user)
        {
            return _eevaContext.Contacts.FirstOrDefault(c => c.Email == user.Identity.Name);
        }
    }
}
