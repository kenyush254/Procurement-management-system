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
    public partial class Units : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Procurement\Procurement Implementation\Procurement.mdf;Integrated Security=True");
        public Units()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int j;

            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select * from Units where unit_name='"+textBox1.Text+"'and unit_desc='"+textBox2.Text+"'";
            cmd1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            j = Convert.ToInt32(dt1.Rows.Count.ToString());

            if (j == 0)
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Units values('" + textBox1.Text + "','" + textBox2.Text + "')";
                cmd.ExecuteNonQuery();
                disp();
            }
            else
            {
                MessageBox.Show("This unit is already added");
            }
        }

        private void Units_Load(object sender, EventArgs e)
        {
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            disp();
        }
        public void disp()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Units";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Units where unit_id=" + id + "";
            cmd.ExecuteNonQuery();
            disp();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            int id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());


            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select * from Units where unit_id=" + id + "";
            cmd1.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox3.Text = dr["unit_name"].ToString();
                textBox4.Text = dr["unit_desc"].ToString();

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());


            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "Update Units set unit_name='" + textBox3.Text + "',unit_desc='" + textBox4.Text + "'where unit_id =" + id + " ";
            cmd1.ExecuteNonQuery();

            panel2.Visible = false;
            disp();
        }
    }
}
