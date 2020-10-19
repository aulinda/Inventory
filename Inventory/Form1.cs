using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace Inventory
{
    public partial class frmAddProduct : Form
    {
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellPrice;
        public frmAddProduct()
        {
            InitializeComponent();
        }
        public class ProductClass
        {

            private int _Quantity;
            private double _SellingPrice;
            private string _ProductName, _Category, _ManufacturingDate, _ExpirationDate, _Description;

            public ProductClass(string ProductName, string Category, string MfgDate, string ExpDate, double Price, int Quantity, string Description)
            {
                this._Quantity = Quantity;
                this._SellingPrice = Price;
                this._ProductName = ProductName;
                this._Category = Category;
                this._ManufacturingDate = MfgDate;
                this._ExpirationDate = ExpDate;
                this._Description = Description;

            }
            public string productName
            {
                get { return this._ProductName; }
                set { this._ProductName = value; }
            }
            public string category
            {
                get { return this._Category; }
                set { this._Category = value; }
            }
            public string manufacturingDate
            {
                get { return this._ManufacturingDate; }
                set { this._ManufacturingDate = value; }
            }
            public string expirationDate
            {
                get { return this._ExpirationDate; }
                set { this._ExpirationDate = value; }
            }
            public string description
            {
                get { return this._Description; }
                set { this._Description = value; }
            }
            public int quantity
            {
                get { return this._Quantity; }
                set { this._Quantity = value; }
            }
            public double sellingPrice
            {
                get { return this._SellingPrice; }
                set { this._SellingPrice = value; }
            }

        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = new string[]{
           "Bevarages",
           "Bread/Bakery",
           "Canned/Jarred Goods",
           "Dairy",
           "Frozen Goods",
           "Meat",
           "Personal Care",
           "Other"
           };
            for (int i = 0; i < 8; i++)
            {
                cbCategory.Items.Add(ListOfProductCategory[i].ToString());
            }


        }


        class NumberFormatException : Exception
        {
            public NumberFormatException(string name)
                : base(String.Format("Invalid Quantity: {0}", name))
            {

            }
        }
        class StringFormatException : Exception
        {
            public StringFormatException(string name)
                : base(String.Format("Invalid: {0}", name))
            {

            }
        }
        class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string name)
                : base(String.Format("Invalid Price: {0}", name))
            {

            }
        }


        public string Product_Name(string name)
        {
            try
            {
                if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                    throw new StringFormatException(name);
                return name;
            }
            catch (StringFormatException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (NumberFormatException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (CurrencyFormatException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                Console.WriteLine("Name is Executed");
            }
            return _ProductName;
        }

        private void richTxtDescirption_TextChanged(object sender, EventArgs e)
        {

        }

        public int Quantity(string qty)
        {
            try
            {
                if (!Regex.IsMatch(qty, @"^[0-9]"))
                    throw new NumberFormatException(qty);

                return Convert.ToInt32(qty);
            }
            catch (StringFormatException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (NumberFormatException e)
            {

                MessageBox.Show(e.Message);


            }
            catch (CurrencyFormatException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                Console.WriteLine("Quantity is Executed");
            }
            return _Quantity;
        }

        public double SellingPrice(string price)
        {
            try
            {
                if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                    throw new CurrencyFormatException(price);

                return Convert.ToDouble(price);

            }
            catch (StringFormatException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (NumberFormatException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (CurrencyFormatException e)
            {
                MessageBox.Show(e.Message);

            }
            finally
            {
                Console.WriteLine("Price is Executed");
            }
            return _SellPrice;
        }




        BindingSource showProductList = new BindingSource();

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            _ProductName = Product_Name(txtProductName.Text);
            _Category = cbCategory.Text;
            _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
            _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
            _Description = richTxtDescription.Text;
            _Quantity = Quantity(txtQuantity.Text);
            _SellPrice = SellingPrice(txtSellPrice.Text);
            showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellPrice, _Quantity, _Description));
            gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridViewProductList.DataSource = showProductList;




        }





    }
}

