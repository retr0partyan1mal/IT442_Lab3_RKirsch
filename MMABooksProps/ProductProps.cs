using System;
using System.Collections.Generic;
using System.Text;

using MMABooksTools;
using DBDataReader = MySql.Data.MySqlClient.MySqlDataReader;

using System.Text.Json;
using System.Text.Json.Serialization;


namespace MMABooksProps
{
    [Serializable()]
    public class ProductProps : IBaseProps
    {
        #region Auto-implemented Properties
        public string ProductCode { get; set; } = "";
        public string Description { get; set; } = "";
        public int OnHandQuantity { get; set; } = 0;
        public decimal UnitPrice { get; set; } = 0;

        public int ConcurrencyID { get; set; } = 0;
        #endregion

        public object Clone()
        {
            ProductProps p = new ProductProps();
            p.ProductCode = this.ProductCode;
            p.Description = this.Description;
            p.OnHandQuantity = this.OnHandQuantity;
            p.UnitPrice = this.UnitPrice;
            p.ConcurrencyID = this.ConcurrencyID;
            return p;
        }

        public string GetState()
        {
            string jsonString;
            jsonString = JsonSerializer.Serialize(this);
            return jsonString;
        }

        public void SetState(string jsonString)
        {
            ProductProps p = JsonSerializer.Deserialize<ProductProps>(jsonString);
            this.ProductCode = p.ProductCode;
            this.Description = p.Description;
            this.OnHandQuantity = p.OnHandQuantity;
            this.UnitPrice = p.UnitPrice;
            this.ConcurrencyID = p.ConcurrencyID;
        }

        public void SetState(DBDataReader dr)
        {
            this.ProductCode = (string)dr["ProductCode"];
            this.Description = (string)dr["Description"];
            this.OnHandQuantity = (int)dr["OnHandQuantity"];
            this.UnitPrice = (decimal)dr["UnitPrice"];
            this.ConcurrencyID = (Int32)dr["ConcurrencyID"];
        }
    }
}