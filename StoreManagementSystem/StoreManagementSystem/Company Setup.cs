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
    public partial class Company_Setup : Form
    {
        public Company_Setup()
        {
            InitializeComponent();
        }


        Company company = new Company();
        string connectionstring = @"server=DESKTOP-1DVPR4V\SQLEXPRESS;database=StoreManagement;Integrated Security=True";



        private void saveButton_Click(object sender, EventArgs e)
        {
            company.Name = nameTextBox.Text;

            bool isCategory = Add(company);

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
        private bool Add(Company company)
        {


            SqlConnection con = new SqlConnection(connectionstring);

            string query = @"INSERT INTO Company Values('" + company.Name + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            bool isAdded = cmd.ExecuteNonQuery() > 0;
            con.Close();
            return isAdded;
        }

        public void loadData()
        {
            SqlConnection con = new SqlConnection(connectionstring);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT* FROM Company", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            companyDataGridView.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = companyDataGridView.Rows.Add();
                companyDataGridView.Rows[n].Cells[0].Value = item["SL"].ToString();
                companyDataGridView.Rows[n].Cells[1].Value = item["Name"].ToString();
            }




        }

        private void Company_Setup_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
