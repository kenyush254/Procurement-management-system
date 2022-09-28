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

namespace Procurement_Implementation
{
    public partial class Add_New_User : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Procurement\Procurement Implementation\Procurement.mdf;Integrated Security=True");
        public Add_New_User()
        {
            InitializeComponent();
        }

        private void Add_New_User_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            display();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Registration where Registration_user_name ='" + textBox3.Text + "'  ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());

            if (i == 0)
            {
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = " Insert into Registration values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "') ";
                cmd1.ExecuteNonQuery();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";

                display();

                MessageBox.Show("user record inserted successfully");



            }
            else
            {
                MessageBox.Show("This username already exists");
            }

        }
        public void display()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Registration";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
         

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

            MessageBox.Show(id.ToString());
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "Update Registration set Registration_First_name='" + textBox7.Text + "',Registration_last_name='" + textBox8.Text + "',Registration_user_name='" + textBox9.Text+ "',Registration_password='" + textBox10.Text + "',Registration_email='" + textBox11.Text + "',Registration_contact='" + textBox12.Text + "'where Registration_id ="+id+" ";
            cmd1.ExecuteNonQuery();

            panel2.Visible = false;
            display();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            int id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

           
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select * from Registration where Registration_id=" + id + "";
            cmd1.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox7.Text = dr["Registration_First_name"].ToString();
                textBox8.Text = dr["Registration_last_name"].ToString();
                textBox9.Text = dr["Registration_user_name"].ToString();
                textBox10.Text = dr["Registration_password"].ToString();
                textBox11.Text = dr["Registration_email"].ToString();
                textBox12.Text = dr["Registration_contact"].ToString();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Registration where Registration_id="+id+"";
            cmd.ExecuteNonQuery();

            display();


        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.Value != null)
            {
                e.Value = new string('*',e.Value.ToString().Length);
            }
        }
    }
}
