using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TSystems_ADO.net
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;


        public Form1()
        {
           
            InitializeComponent();
            con = new SqlConnection("Server=DESKTOP-T6SDT7B;Database=TSystems;Integrated Security=True");
           
        }
        private void cleardata()
        {
            textId.Clear();
            textName.Clear();
            textDesignation.Clear();
            textSalary.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select MAX(Id) from Employee";
                cmd = new SqlCommand(qry, con);
                con.Open();
                Object obj = cmd.ExecuteScalar();
                if (obj == DBNull.Value)
                {
                    textId.Text = "1";
                }
                else
                {
                    int id = Convert.ToInt32(obj);
                    id++;
                    textId.Text = id.ToString();
                    textId.Enabled = false;

                }
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {


                string qry = "insert into Employee values(@id,@name,@Designation,@salary)";

                cmd = new SqlCommand(qry, con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textId.Text));

                cmd.Parameters.AddWithValue("@name", (textName.Text));

                cmd.Parameters.AddWithValue("@Designation", textDesignation.Text);

                cmd.Parameters.AddWithValue("@salary", Convert.ToInt32(textSalary.Text));
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Successfully saved the Record");
                    cleardata();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                 con.Close();
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Employee where Id=@id";
                cmd = new SqlCommand(qry, con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textId.Text));
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    { 
                    textName.Text = dr["Name"].ToString();
                    textDesignation.Text = dr["Designation"].ToString();
                    textSalary.Text = dr["Salary"].ToString();
                    }
                }

                else
                {
                    MessageBox.Show("Record are not found");
                }

             }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {


                con.Close();
            }

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {


                string qry = "update Employee set Name=@name,Designation=@Designation,Salary=@Salary where Id=@id ";

                cmd = new SqlCommand(qry, con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textId.Text));

                cmd.Parameters.AddWithValue("@name", (textName.Text));

                cmd.Parameters.AddWithValue("@Designation", textDesignation.Text);

                cmd.Parameters.AddWithValue("@salary", Convert.ToInt32(textSalary.Text));
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Successfully Updated");
                    cleardata();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                con.Close();
            }

        
    }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {


                string qry = "Delete from Employee where Id=@id";

                cmd = new SqlCommand(qry, con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textId.Text));



                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Successfully Deleted the Record");
                    cleardata();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                con.Close();
            }

        }
    }
    }

