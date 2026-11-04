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
    public class ProductTests
    {
        [SetUp]
        public void TestResetDatabase()
        {
            ProductDB db = new ProductDB();

            DBCommand command = new DBCommand();
            command.CommandText = "usp_testingResetProductData";
            command.CommandType = CommandType.StoredProcedure;

            db.RunNonQueryProcedure(command);
        }

        [Test]
        public void TestConcurrencyIssue()
        {
            Product p1 = new Product(4);
            Product p2 = new Product(4);

            p1.ProductCode = "Updated first";
            p1.Save();

            p2.ProductCode = "Updated second";
            Assert.Throws<Exception>(() => p2.Save());
        }

        [Test]
        public void TestDelete()
        {
            Product p = new Product(7);
            p.Delete();
            p.Save();
            Assert.Throws<Exception>(() => new Product(7));
        }

        [Test]
        public void TestGetList()
        {
            Product p = new Product();
            List<Product> products = (List<Product>)p.GetList();
            Assert.AreEqual(16, products.Count);
            Assert.AreEqual("A4CS", products[0].ProductCode);
            Assert.AreEqual("Murach's ASP.NET 4 Web Programming with C# 2010", products[0].Description);
        }

        [Test]
        public void TestInvalidPropertySet()
        {
            Product p = new Product();
            Assert.Throws<ArgumentOutOfRangeException>(() => p.OnHandQuantity = -1);
        }

        [Test]
        public void TestNewProdictConstructor()
        {
            Product p = new Product();
            Assert.AreEqual(string.Empty, p.ProductCode);
            Assert.AreEqual(string.Empty, p.Description);
            Assert.IsTrue(p.IsNew);
            Assert.IsFalse(p.IsValid);
        }

        [Test]
        public void TestNoRequiredPropertiesNotSet()
        {
            Product p = new Product();
            Assert.Throws<Exception>(() => p.Save());
        }

        [Test]
        public void TestRetrieveFromDataStoreContructor()
        {
            Product p = new Product(1);
            Assert.AreEqual("A4CS", p.ProductCode);
            Assert.AreEqual("Murach's ASP.NET 4 Web Programming with C# 2010", p.Description);
            Assert.IsFalse(p.IsNew);
            Assert.IsTrue(p.IsValid);
        }

        [Test]
        public void TestSaveToDataStore()
        {
            Product p = new Product();
            p.ProductCode = "B640";
            p.Description = "Dot Monster Cookie";
            p.UnitPrice = 4.00m;
            p.OnHandQuantity = 500;
            p.Save();
            Product p2 = new Product(p.ProductID);
            Assert.AreEqual(p2.ProductCode, p.ProductCode);
            Assert.AreEqual(p2.Description, p.Description);
        }

        [Test]
        public void TestSomeRequiredPropertiesNotSet()
        {
            Product p = new Product();
            Assert.Throws<Exception>(() => p.Save());
            p.ProductCode = "A320";
            Assert.Throws<Exception>(() => p.Save());
        }

        [Test]
        public void TestUpdate()
        {
            Product p = new Product(14);
            p.Description = "Edited Description";
            p.Save();

            Product p2 = new Product(14);
            Assert.AreEqual(p2.ProductCode, p.ProductCode);
            Assert.AreEqual(p2.Description, p.Description);
        }
    }
}