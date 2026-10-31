using NUnit.Framework;

using MMABooksProps;
using MMABooksDB;

using DBCommand = MySql.Data.MySqlClient.MySqlCommand;
using System.Data;

using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

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
        public void TestRetrieve()
        {
            CustomerProps p = (CustomerProps)db.Retrieve(1); // Using "CustomerID" instead of 1 kept giving an error.
            Assert.AreEqual(1, p.CustomerID);
            Assert.AreEqual("Molunguri, A", p.Name);
        }

        /*[Test]
        public void TestRetrieveAll()
        {
            List<StateProps> list = (List<StateProps>)db.RetrieveAll();
            Assert.AreEqual(53, list.Count);
        }

        [Test]
        public void TestDelete()
        {
            StateProps p = (StateProps)db.Retrieve("HI");
            Assert.True(db.Delete(p));
            Assert.Throws<Exception>(() => db.Retrieve("HI"));
        }


        [Test]
        public void TestDeleteForeignKeyConstraint()
        {
            StateProps p = (StateProps)db.Retrieve("OR");
            Assert.Throws<MySqlException>(() => db.Delete(p));
        }

        [Test]
        public void TestUpdate()
        {
            StateProps p = (StateProps)db.Retrieve("OR");
            p.Name = "Oregon";
            Assert.True(db.Update(p));
            p = (StateProps)db.Retrieve("OR");
            Assert.AreEqual("Oregon", p.Name);
        }

        [Test]
        public void TestUpdateFieldTooLong()
        {
            StateProps p = (StateProps)db.Retrieve("OR");
            p.Name = "Oregon is the state where Crater Lake National Park is.";
            Assert.Throws<MySqlException>(() => db.Update(p));
        }

        [Test]
        public void TestCreate()
        {
            StateProps p = new StateProps();
            p.Code = "??";
            p.Name = "Where am I";
            db.Create(p);
            StateProps p2 = (StateProps)db.Retrieve(p.Code);
            Assert.AreEqual(p.GetState(), p2.GetState());
        }

        [Test]
        public void TestCreatePrimaryKeyViolation()
        {
            StateProps p = new StateProps();
            p.Code = "OR";
            p.Name = "Oregon";
            Assert.Throws<MySqlException>(() => db.Create(p));
        }
        */
    }
}