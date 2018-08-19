using StoreManagementSystem.Methods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreManagementSystem
{
    public partial class StockIn : Form
    {
        public StockIn()
        {
            InitializeComponent();
        }
        Stock stock = new Stock();
        string connectionstring = @"server=DESKTOP-1DVPR4V\SQLEXPRESS;database=StoreManagement;Integrated Security=True";



        private void saveButton_Click(object sender, EventArgs e)
        {
            
            
            //bool isCategory = Add(item);
            bool isCategory = Add(stock);


             if (isCategory == true)
             {
                    MessageBox.Show("Succesfully Saved");
             }
             else
             {
                    MessageBox.Show("Not Saved");

             }
            
            

           
        }



        private bool Add(Stock stock)
        {


            SqlConnection con = new SqlConnection(connectionstring);

            string query = @"INSERT INTO Item Values('" + stock.stockIn + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            bool isAdded = cmd.ExecuteNonQuery() > 0;
            con.Close();
            return isAdded;
        }

        

        void FillItemcombo()
        {
            SqlConnection con = new SqlConnection(connectionstring);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT ItemName FROM Item Where Company='"+companyComboBox.Text+"' AND Category='"+categoryComboBox.Text+"' ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {

                companyComboBox.Items.Add(item["ItemName"].ToString());

            }
        }

        void FillCategorycombo()
        {
            SqlConnection con = new SqlConnection(connectionstring);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT Name FROM Category", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {

                categoryComboBox.Items.Add(item["Name"].ToString());

            }
        }
        void FillCompanrycombo()
        {
            SqlConnection con = new SqlConnection(connectionstring);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT Name FROM Company", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {

                companyComboBox.Items.Add(item["Name"].ToString());

            }
        }

        private void StockIn_Load(object sender, EventArgs e)
        {
            FillCategorycombo();
            FillCompanrycombo();
            FillItemcombo();
        }
    }
}
