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
    public partial class Item_Setup : Form
    {
        public Item_Setup()
        {
            InitializeComponent();
        }

        Items item = new Items();
        string connectionstring = @"server=DESKTOP-1DVPR4V\SQLEXPRESS;database=StoreManagement;Integrated Security=True";

        private void saveButton_Click(object sender, EventArgs e)
        {
            item.Name = itemTextBox.Text;
            item.Order = Convert.ToInt32(reorderTextBox.Text);
            //bool isCategory = Add(item);

            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM Item where ItemName='" + itemTextBox.Text+"'", con);
           //DataSet ds = new DataSet();
            //sda.Fill(ds);
            int Count =(int)cmd.ExecuteScalar();
            if (Count > 0)
            {
                MessageBox.Show("Name already Exists");
                return;
            }
            else
            {
                bool isCategory = Add(item);


                if (isCategory == true)
                {
                    MessageBox.Show("Succesfully Saved");
                }
                else
                {
                    MessageBox.Show("Not Saved");

                }
            }
            con.Close();

            reorderTextBox.Text = "";
            itemTextBox.Text = "";
        }
        private bool Add(Items item)
        {


            SqlConnection con = new SqlConnection(connectionstring);

            string query = @"INSERT INTO Item Values('" + item.Name + "','" + item.Order + "','" + categoryComboBox.Text+ "','" + companyComboBox.Text + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            bool isAdded = cmd.ExecuteNonQuery() > 0;
            con.Close();
            return isAdded;
        }

        private void Item_Setup_Load(object sender, EventArgs e)
        {
            FillCategorycombo();
            FillCompanrycombo();
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
    }
}
