using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using EEVA.Domain.Models;
using System.Linq;

namespace EEVA.Domain.Test
{
    [TestClass]
    public class ContactManagerTest
    {
        [TestMethod]
        public void TestGetContact()
        {
            DummyContactManager manager = new DummyContactManager();

            Contact c = manager.Get(1);

            Assert.IsNotNull(c);
            Assert.AreEqual("Kenneth", c.FirstName);
            Assert.AreEqual("Van Den Borne", c.LastName);
          
        }

        [TestMethod]
        public void TestSearchContacts()
        {
            DummyContactManager manager = new DummyContactManager();

            List<Contact> contacts = manager.Search("ken").ToList();
           

            Assert.IsNotNull(contacts);
            Assert.IsTrue(contacts.Count > 0);
        }

        [TestMethod]
        public void TestAddContact()
        {
            DummyContactManager manager = new DummyContactManager();

            Teacher contact = new Teacher()
            {
                Id = 3,
                FirstName = "Donald",
                LastName = "Trump",
                Email = "donal@whitehouse.gov",
                PhoneNumber = "027267189"
            };

            manager.Add(contact);

            Assert.IsTrue(contact.Id > 0);
            Assert.AreEqual("Donald", contact.FirstName);
            Contact freshContact = manager.Get(contact.Id);
            Assert.IsNotNull(freshContact);

        }

        [TestMethod]
        public void TestUpdateContact()
        {
            DummyContactManager manager = new DummyContactManager();
            Student c = new Student()
            {
                FirstName = "Donald",
                LastName = "Trump",
                Email = "donald@whitehouse.gov",
                PhoneNumber = "0289298330"
            };
            Contact insertedContact = manager.Add(c);

            c.Email = "potus@whitehouse.gov";
            manager.Update(c);

            Contact freshContact = manager.Get(insertedContact.Id);
            Assert.AreEqual("potus@whitehouse.gov", freshContact.Email);

        }

        [TestMethod]
        public void TestDeleteContact()
        {
            DummyContactManager manager = new DummyContactManager();

            Teacher contact = new Teacher()
            {
                Id = 3,
                FirstName = "Donald",
                LastName = "Trump",
                Email = "donal@whitehouse.gov",
                PhoneNumber = "027267189"
            };

            manager.Add(contact);
            manager.Delete(contact);

            List<Contact> contacts = manager.Search("Donald").ToList();

            Assert.IsFalse(contacts.Contains(contact));
        }
    }
}
