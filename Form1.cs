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

namespace RegistrationForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(SqlConnection conn = connectionSql.connect())
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Please fill full name field", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtName.Focus();
                    return;
                }
                else if (txtAddress.Text == "")
                {
                    MessageBox.Show("Please fill address field", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtAddress.Focus();
                    return;

                }
                else if(txtPhone.Text == "") 
                {
                    MessageBox.Show("Please fill the phone number field", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtName.Focus();
                    return;

                }
                else if(txtDate.Text == "")
                {
                    MessageBox.Show("Please fill date field", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtName.Focus();
                    return;
                }
                //normal c#
                //  SqlCommand cmd = new SqlCommand("INSERT INTO STUDENTS(fullname, address, phone, regdate) VALUES('"+txtName.Text+"','"+txtAddress.Text+"', '"+txtPhone.Text+"', '"+txtDate.Text+"')",conn);

                //parametirized c#
                //SqlCommand cmd = new SqlCommand("INSERT INTO STUDENTS(fullname, address, phone, regdate) values(@fullname, @address, @phone, @regdate)", conn);
                // cmd.Parameters.AddWithValue("@fullname", txtName.Text);
                //cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                //cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                // cmd.Parameters.AddWithValue("@regdate", txtDate.Text);

                //Stored procedure
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", "");
                cmd.CommandText = "dbo.student";
                cmd.Parameters.AddWithValue("@fullname", txtName.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@regdate", txtDate.Text);
                cmd.Parameters.AddWithValue("@type", "INSERT");


                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Saved Succesfully", "Saving data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                displayData();
                clearText();


            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            displayData();
            lbId.Visible = false;
            btnDelete.Enabled= false;
            btnUpdate.Enabled= false;
            if (!btnDelete.Enabled)
            {
                btnDelete.BackColor = Color.White;
                btnDelete.ForeColor = Color.Black;
            }

            if (!btnUpdate.Enabled)
            {
                btnUpdate.BackColor = Color.White;
                btnUpdate.ForeColor = Color.Black;
            }
            if (btnUpdate.Enabled)
            {
                btnSave.BackColor = Color.Coral;
                btnSave.ForeColor = Color.White;
            }

        }

        private void displayData()
        {
            using(SqlConnection conn = connectionSql.connect()) 
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from students", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                studentData.DataSource = dt;
            }

        }

        private void studentData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lbId.Text= studentData.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = studentData.CurrentRow.Cells[1].Value.ToString();
            txtAddress.Text = studentData.CurrentRow.Cells[2].Value.ToString();
            txtPhone.Text = studentData.CurrentRow.Cells[3].Value.ToString();
            txtDate.Text = studentData.CurrentRow.Cells[4].Value.ToString().Trim();
            btnSave.Enabled = false;
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
            if (btnDelete.Enabled)
            {
                btnDelete.BackColor = Color.Coral;
                btnDelete.ForeColor = Color.White;
            }

            if (btnUpdate.Enabled)
            {
                btnUpdate.BackColor = Color.Coral;
                btnUpdate.ForeColor = Color.White;
            }
            if (!btnSave.Enabled)
            {
                btnSave.BackColor = Color.White;
                btnSave.ForeColor = Color.Black;
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using(SqlConnection conn = connectionSql.connect()) 
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", lbId.Text);
                cmd.CommandText = "dbo.student";
                cmd.Parameters.AddWithValue("@fullname", "");
                cmd.Parameters.AddWithValue("@address", "");
                cmd.Parameters.AddWithValue("@phone", "");
                cmd.Parameters.AddWithValue("@regdate", "");
                cmd.Parameters.AddWithValue("@type", "DELETE");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Data succesfully", "Deletion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                displayData();
                clearText();
                
            }
        }

        private void clearText()
        {
            txtName.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            txtDate.Text = "";

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using(SqlConnection connection = connectionSql.connect())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.CommandText = "dbo.student";
                cmd.Parameters.AddWithValue("@id", lbId.Text);
                cmd.Parameters.AddWithValue("@fullname", txtName.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@regdate", txtDate.Text);
                cmd.Parameters.AddWithValue("@type", "UPDATE");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated Successfully", "Updating", MessageBoxButtons.OK, MessageBoxIcon.Information);
                displayData();
                clearText();
                btnSave.Enabled = true;
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
                if (!btnDelete.Enabled)
                {
                    btnDelete.BackColor = Color.White;
                    btnDelete.ForeColor = Color.Black;
                }

                if (!btnUpdate.Enabled)
                {
                    btnUpdate.BackColor = Color.White;
                    btnUpdate.ForeColor = Color.Black;
                }
                if (btnSave.Enabled)
                {
                    btnSave.BackColor = Color.Coral;
                    btnSave.ForeColor = Color.White;
                }
            }
        }
    }
}
