using NUnit.Framework;

using MMABooksBusiness;
using MMABooksProps;
using MMABooksDB;

using DBCommand = MySql.Data.MySqlClient.MySqlCommand;
using System.Data;

using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace MMABooksTests
{
    [TestFixture]
    public class CustomerTests
    {
        [SetUp]
        public void TestResetDatabase()
        {
            CustomerDB db = new CustomerDB();

            DBCommand command = new DBCommand();
            command.CommandText = "usp_testingResetCustomer1Data";
            command.CommandType = CommandType.StoredProcedure;

            DBCommand command2 = new DBCommand();
            command2.CommandText = "usp_testingResetCustomer2Data";
            command2.CommandType = CommandType.StoredProcedure;

            db.RunNonQueryProcedure(command);
            db.RunNonQueryProcedure(command2);
        }

        [Test]
        public void TestConcurrencyIssue()
        {
            Customer c1 = new Customer(5);
            Customer c2 = new Customer(5);

            c1.Name = "Updated first";
            c1.Save();

            c2.Name = "Updated second";
            Assert.Throws<Exception>(() => c2.Save());
        }

        [Test]
        public void TestDelete()
        {
            Customer c = new Customer(157);
            c.Delete();
            c.Save();
            Assert.Throws<Exception>(() => new Customer(157));
        }

        [Test]
        public void TestGetList()
        {
            Customer c = new Customer();
            List<Customer> customers = (List<Customer>)c.GetList();
            Assert.AreEqual(696, customers.Count);
            Assert.AreEqual("Abeyatunge, Derek", customers[0].Name);
            Assert.AreEqual("1414 S. Dairy Ashford", customers[0].Address);
        }

        [Test]
        public void TestInvalidPropertySet()
        {
            Customer c = new Customer();
            Assert.Throws<ArgumentOutOfRangeException>(() => c.ZipCode = "123456789123456789123456789");
        }

        [Test]
        public void TestNewCustomerConstructor()
        {
            Customer c = new Customer();
            Assert.AreEqual(string.Empty, c.Name);
            Assert.AreEqual(string.Empty, c.Address);
            Assert.IsTrue(c.IsNew);
            Assert.IsFalse(c.IsValid);
        }

        [Test]
        public void TestNoRequiredPropertiesNotSet()
        {
            Customer c = new Customer();
            Assert.Throws<Exception>(() => c.Save());
        }

        [Test]
        public void TestRetrieveFromDataStoreContructor()
        {
            Customer c = new Customer(1);
            Assert.AreEqual("Molunguri, A", c.Name);
            Assert.AreEqual("1108 Johanna Bay Drive", c.Address);
            Assert.IsFalse(c.IsNew);
            Assert.IsTrue(c.IsValid);
        }

        [Test]
        public void TestSaveToDataStore()
        {
            Customer c = new Customer();
            c.Name = "Shaggy Rogers";
            c.Address = "210 Spooky Street";
            c.City = "Coolsville";
            c.State = "OH";
            c.ZipCode = "1969";
            c.Save();
            Customer c2 = new Customer(c.CustomerID);
            Assert.AreEqual(c2.Name, c.Name);
            Assert.AreEqual(c2.Address, c.Address);
        }

        [Test]
        public void TestSomeRequiredPropertiesNotSet()
        {
            Customer c = new Customer();
            Assert.Throws<Exception>(() => c.Save());
            c.Name = "Scooby Doo";
            Assert.Throws<Exception>(() => c.Save());
        }

        [Test]
        public void TestUpdate()
        {
            Customer c = new Customer(449);
            c.Name = "Edited Name";
            c.Save();

            Customer c2 = new Customer(449);
            Assert.AreEqual(c2.Name, c.Name);
            Assert.AreEqual(c2.Address, c.Address);
        }
    }
}