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
    public class ProductDBTests
    {
        ProductDB db;

        [SetUp]
        public void ResetData()
        {
            db = new ProductDB();
            DBCommand command = new DBCommand();
            command.CommandText = "usp_testingResetProductData";
            command.CommandType = CommandType.StoredProcedure;
            db.RunNonQueryProcedure(command);
        }

        [Test]
        public void TestCreate()
        {
            ProductProps p = new ProductProps();
            p.ProductCode = "B640";
            p.Description = "Dot Monster Cookie";
            p.UnitPrice = 4.00m;
            p.OnHandQuantity = 500;
            db.Create(p);
            ProductProps p2 = (ProductProps)db.Retrieve(p.ProductID);
            Assert.AreEqual(p.GetState(), p2.GetState());
        }

        [Test]
        public void TestDelete()
        {
            List<ProductProps> products = (List<ProductProps>)db.RetrieveAll();
            ProductProps p = products.OrderByDescending(c => c.ProductID).First();
            Assert.IsTrue(db.Delete(p));
            Assert.Throws<Exception>(() => db.Retrieve(p.ProductID));
        } 

        [Test]
        public void TestRetrieve()
        {
            ProductProps p = (ProductProps)db.Retrieve(1);
            Assert.AreEqual(1, p.ProductID);
            Assert.AreEqual("A4CS", p.ProductCode);
        }

        [Test]
        public void TestRetrieveAll()
        {
            List<ProductProps> list = (List<ProductProps>)db.RetrieveAll();
            Assert.AreEqual(16, list.Count);
        }

        [Test]
        public void TestUpdate()
        {
            List<ProductProps> products = (List<ProductProps>)db.RetrieveAll();
            ProductProps p = products[products.Count - 1];
            p.Description = "Dot Monster Cookie (New Recipe)";
            Assert.True(db.Update(p));
            p = (ProductProps)db.Retrieve(p.ProductID);
            Assert.AreEqual("Dot Monster Cookie (New Recipe)", p.Description);
        }
    }
}