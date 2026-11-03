using MMABooksBusiness;
using MMABooksDB;
using MMABooksProps;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using DBCommand = MySql.Data.MySqlClient.MySqlCommand;

namespace MMABooksTests
{
    public class CustomerDBTests
    {
        CustomerDB db;

        [SetUp]
        public void ResetData()
        {
            db = new CustomerDB();
            DBCommand command = new DBCommand();
            command.CommandText = "usp_testingResetData";
            command.CommandType = CommandType.StoredProcedure;
            db.RunNonQueryProcedure(command);
        }

        [Test]
        public void TestCreate()
        {
            CustomerProps p = new CustomerProps();
            p.Name = "Shaggy Rogers";
            p.Address = "210 Spooky Street";
            p.City = "Coolsville";
            p.State = "OH";
            p.ZipCode = "1969";
            db.Create(p);
            CustomerProps p2 = (CustomerProps)db.Retrieve(p.CustomerID);
            Assert.AreEqual(p.GetState(), p2.GetState());
        }

        [Test]
        public void TestDelete()
        {
            List<CustomerProps> customers = (List<CustomerProps>)db.RetrieveAll();
            CustomerProps p = customers.OrderByDescending(c => c.CustomerID).First();
            Assert.IsTrue(db.Delete(p));
            Assert.Throws<Exception>(() => db.Retrieve(p.CustomerID));
        } 

        [Test]
        public void TestRetrieve()
        {
            CustomerProps p = (CustomerProps)db.Retrieve(1); // Using "CustomerID" instead of 1 kept giving an error.
            Assert.AreEqual(1, p.CustomerID);
            Assert.AreEqual("Molunguri, A", p.Name);
        }

        [Test]
        public void TestRetrieveAll()
        {
            List<CustomerProps> list = (List<CustomerProps>)db.RetrieveAll();
            Assert.AreEqual(696, list.Count);
        }

        [Test]
        public void TestUpdate()
        {
            List<CustomerProps> customers = (List<CustomerProps>)db.RetrieveAll();
            CustomerProps p = customers[customers.Count - 1];
            p.Name = "Shahgy Rogers";
            Assert.True(db.Update(p));
            p = (CustomerProps)db.Retrieve(p.CustomerID);
            Assert.AreEqual("Shahgy Rogers", p.Name);
        }
    }
}