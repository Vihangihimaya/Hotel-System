using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace HMS.User_Control
{
    public partial class UC_AddRoom : UserControl
    {
        public UC_AddRoom()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Db\hotelMgt.mdf;Integrated Security=True;Connect Timeout=30");


        private void button1_Click(object sender, EventArgs e)
        {
            string roomType = comboBox1.Text;
            string bed = comboBox2.Text;
            string room = textBox1.Text;
            int roomNo;
            int.TryParse(textBox1.Text, out roomNo);
            int price;
            int.TryParse(textBox2.Text, out price);

            

            if (roomNo != 0 && price != 0 && roomType != "" && bed != "")
            {
                //data base codes
                string qry = "insert into roomDetails values (" + roomNo + ", '" + roomType + "', '" + bed + "', " + price + ")";
                SqlCommand cmd = new SqlCommand(qry, conn);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    UC_AddRoom_Load(this, null);
                    clearAll();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { conn.Close(); }
            }
            else
            {
                MessageBox.Show("All fields are required");
            }
        }

        public void clearAll()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        private void UC_AddRoom_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

        private void UC_AddRoom_Load(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from roomDetails";
                SqlDataAdapter sda = new SqlDataAdapter(qry, conn);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);

                dataGridView1.DataSource = dTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            int num;
            int.TryParse(textBox1.Text, out num);

            if (num.ToString() != "" && num != 0)
            {
                string qry = "delete from roomDetails where roomNum = '" + num + "'";
                SqlCommand cmd = new SqlCommand(qry, conn);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    UC_AddRoom_Load(this, null);
                    clearAll();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { conn.Close(); }
            }
            else
            {
                MessageBox.Show("Enter the Room No");
            }
        }
    }
}
