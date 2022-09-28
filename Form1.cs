using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace online_student_registration_system
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=LAPTOP-NH5220L7\\SQLEXPRESS01; Initial Catalog=Osrs; User Id=chandra; Password=123456");
        SqlCommand cmd;
        SqlDataReader read;
        string id;
        bool Mode = true;
        string sql;

        // if the mode is true means add records otherwise update the record 
        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string course = txtCourse.Text;
            string fee = txtFee.Text;


            if(Mode == true)
            {
                sql = "insert into student table(stname,course,fee) values(@stname,@course,@fee)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@stname", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);
                MessageBox.Show("Record Added");
                cmd.ExecuteNonQuery();


                txtName.Clear();
                txtCourse.Clear(); 
                txtFee.Clear();
                txtName.Focus();

            }
            else
            {




            }
            con.Close();



        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

         



        }


    }
}

