using MMABooksDB;
using MMABooksProps;
using MMABooksTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;

namespace MMABooksBusiness
{
    public class Product : BaseBusiness
    {
        public int ProductID
        {
            get
            {
                return ((ProductProps)mProps).ProductID;
            }
        }

        public string ProductCode
        {
            get
            {
                return ((ProductProps)mProps).ProductCode;
            }

            set
            {
                if (!(value == ((ProductProps)mProps).ProductCode))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 100)
                    {
                        mRules.RuleBroken("ProductCode", false);
                        ((ProductProps)mProps).ProductCode = value;
                        mIsDirty = true;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("ProductCode must be at least one character and no more than 100 characters.");
                    }
                }
            }
        }

        public string Description
        {
            get
            {
                return ((ProductProps)mProps).Description;
            }

            set
            {
                if (!(value == ((ProductProps)mProps).Description))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 100)
                    {
                        mRules.RuleBroken("Description", false);
                        ((ProductProps)mProps).Description = value;
                        mIsDirty = true;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Description must be at least one character and no more than 100 characters.");
                    }
                }
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return ((ProductProps)mProps).UnitPrice;
            }

            set
            {
                if (!(value == ((ProductProps)mProps).UnitPrice))
                {
                    if (value >= 0)
                    {
                        mRules.RuleBroken("UnitPrice", false);
                        ((ProductProps)mProps).UnitPrice = value;
                        mIsDirty = true;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Unit Price cannot be a negative value.");
                    }
                }
            }
        }

        public int OnHandQuantity
        {
            get
            {
                return ((ProductProps)mProps).OnHandQuantity;
            }

            set
            {
                if (!(value == ((ProductProps)mProps).OnHandQuantity))
                {
                    if (value >= 0)
                    {
                        mRules.RuleBroken("OnHandQuantity", false);
                        ((ProductProps)mProps).OnHandQuantity = value;
                        mIsDirty = true;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Quantity cannot be a negative value.");
                    }
                }
            }
        }


        public override object GetList()
        {
            List<Product> products = new List<Product>();
            List<ProductProps> props = new List<ProductProps>();


            props = (List<ProductProps>)mdbReadable.RetrieveAll();
            foreach (ProductProps prop in props)
            {
                Product p = new Product(prop);
                products.Add(p);
            }

            return products;
        }

        protected override void SetDefaultProperties()
        {
        }

        protected override void SetRequiredRules()
        {
            mRules.RuleBroken("ProductCode", true);
            mRules.RuleBroken("Description", true);
            mRules.RuleBroken("UnitPrice", true);
            mRules.RuleBroken("OnHandQuantity", true);
        }

        protected override void SetUp()
        {
            mProps = new ProductProps();
            mOldProps = new ProductProps();

            mdbReadable = new ProductDB();
            mdbWriteable = new ProductDB();
        }

        #region Constructors
        public Product() : base()
        {
        }

        public Product(int v) : base(v)
        {
        }

        public Product(string key) : base(key)
        {
        }

        private Product(ProductProps props) : base(props)
        {
        }

        #endregion
    }
}