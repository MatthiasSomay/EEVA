using EEVA.Domain;
using EEVA.Domain.DataManager;
using EEVA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EEVA.xUnitTest
{
    public class ContactManagerTest : IDisposable
    {
        private readonly ContactManager _manager = new ContactManager();
        private Teacher TEACHER = new Teacher { FirstName = "Kenneth", LastName = "Van Den Borne", Email = "kenneth@gmail.com", PhoneNumber = "03893843949" };

        public ContactManagerTest()
        {
            var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlServer()
            .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<EEVAContext>();

            builder.UseSqlServer($"Server=tcp:eevakbms.database.windows.net,1433;Database = contacts_db_{ Guid.NewGuid()};Persist Security Info=False;User ID=eevakbms;Password=EEVA_KBMS007;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
                    .UseInternalServiceProvider(serviceProvider);

            _manager._eevaContext = new EEVAContext(builder.Options);
            _manager._eevaContext.Database.Migrate(); 
        }

        [Fact]
        public void AddContact_Test()
        {
            _manager.Add(TEACHER);

            IList<Contact> contacts = _manager.GetAll().ToList();

            Assert.Equal(1, contacts.Count);
            Contact contact = contacts.First();
            Assert.Equal("Kenneth", contact.FirstName);
            
        }

        [Fact]
        public void DeleteContact_Test()
        {
            _manager.Add(TEACHER);


            IList<Contact> contacts = _manager.GetAll().ToList();
            Contact contact = contacts.First();

            _manager.Delete(contact);


            IList<Contact> deleteContacts = _manager.GetAll().ToList();

            Assert.Equal(0, deleteContacts.Count);


        }

        [Fact]
        public void GetContact_Test()
        {
            _manager.Add(TEACHER);

            Contact contact = _manager.Get(1);

            Assert.Equal("Kenneth", contact.FirstName);

        }

        [Fact]
        public void GetAllContacts_Test()
        {
            _manager.Add(TEACHER);
            _manager.Add(new Student { FirstName = "Matthias", LastName = "Somay", Email = "somay@gmail.com", PhoneNumber = "04768282929" });

            IList<Contact> contacts = _manager.GetAll().ToList();

            Assert.Equal(2, contacts.Count);
        }

        [Fact]
        public void SearchContacts_Test()
        {
            _manager.Add(TEACHER);

            IList<Contact> contacts = _manager.Search("ken").ToList();

            Assert.True(contacts.Count > 0);
        }

        [Fact]
        public void UpdateContact_Test()
        {
            _manager.Add(TEACHER);

            IList<Contact> contacts = _manager.GetAll().ToList();
            Contact contact = contacts.First();
            contact.Email = "kenneth@live.be";

            _manager.Update(contact);

            IList<Contact> updatedContacts = _manager.GetAll().ToList();
            Contact updatedContact = updatedContacts.First();

            Assert.Equal("kenneth@live.be", updatedContact.Email);


        }


        public void Dispose()
        {
            _manager._eevaContext.Database.EnsureDeleted();
        }
    }
}
