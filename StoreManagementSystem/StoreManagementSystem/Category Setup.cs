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
using StoreManagementSystem.Methods;

namespace StoreManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Category product = new Category();
        string connectionstring = @"server=DESKTOP-1DVPR4V\SQLEXPRESS;database=StoreManagement;Integrated Security=True";
        private void saveButton_Click(object sender, EventArgs e)
        {
            product.Name = nameTextBox.Text;

            bool isCategory = Add(product);

            if (isCategory == true)
            {
                MessageBox.Show("Succesfully Saved");
            }
            else
            {
                MessageBox.Show("Not Saved");

            }

            loadData();

            nameTextBox.Text = "";


        }
        private bool Add(Category product)
        {

            
            SqlConnection con = new SqlConnection(connectionstring);

            string query = @"INSERT INTO Category Values('" + product.Name + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(query,con);
            bool isAdded= cmd.ExecuteNonQuery() >0;
            con.Close();
            return isAdded;


        }
        public void loadData()
        {
            SqlConnection con = new SqlConnection(connectionstring);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT* FROM Category", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            categorydataGridView.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = categorydataGridView.Rows.Add();
                categorydataGridView.Rows[n].Cells[0].Value = item["SL"].ToString();
                categorydataGridView.Rows[n].Cells[1].Value = item["Name"].ToString();
            }




        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();

            Fillcombo();


        }
        void Fillcombo()
        {
            SqlConnection con = new SqlConnection(connectionstring);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT Name FROM Category", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {

                comboBox1.Items.Add(item["Name"].ToString());

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }
    }
}
