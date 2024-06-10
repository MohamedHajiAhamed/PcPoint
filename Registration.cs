using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Drawing;

namespace PcPoint
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        public string connectionstring = Connection.GetConnectionString();

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_maximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void btn_minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }


        private void btn_show_Click(object sender, EventArgs e)
        {
            if (txtbox_password.UseSystemPasswordChar == true && txtbox_cpassword.UseSystemPasswordChar == true)
            {
                this.txtbox_password.UseSystemPasswordChar = false;
                this.txtbox_cpassword.UseSystemPasswordChar = false;
                btn_show.SendToBack();
            }
            else
            {
                this.txtbox_password.UseSystemPasswordChar = true;
                this.txtbox_cpassword.UseSystemPasswordChar = true;
                btn_show.BringToFront();

            }

        }

        private void btn_hide_Click(object sender, EventArgs e)
        {
            if (txtbox_password.UseSystemPasswordChar == false && txtbox_cpassword.UseSystemPasswordChar == false)
            {
                this.txtbox_password.UseSystemPasswordChar = true;
                this.txtbox_cpassword.UseSystemPasswordChar = true;
                btn_hide.SendToBack();

            }
            else
            {
                this.txtbox_password.UseSystemPasswordChar = false;
                this.txtbox_cpassword.UseSystemPasswordChar = false;
                btn_hide.BringToFront();

            }

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            this.txtbox_firstname.Clear();
            this.txtbox_lastname.Clear();
            this.txtbox_username.Clear();
            this.txtbox_mail.Clear();
            this.txtbox_password.Clear();
            this.txtbox_cpassword.Clear();
        }
        public bool IsEmailValid(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }


        private void btn_submit_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtbox_firstname.Text.Trim() != "" && txtbox_lastname.Text.Trim() != "" && txtbox_username.Text.Trim() != "" && txtbox_mail.Text.Trim() != "" && txtbox_password.Text.Trim() != "" && txtbox_cpassword.Text.Trim() != "")
                {
                    string emailAddress = txtbox_mail.Text;
                    bool isValid = IsEmailValid(emailAddress);
                    if (isValid)
                    {
                        if (txtbox_password.Text.Trim().Length >= 8)
                        {
                            if (txtbox_password.Text.Trim() == txtbox_cpassword.Text.Trim())
                            {

                                SqlConnection connect = new SqlConnection(connectionstring);
                                connect.Open();
                                SqlCommand sp_insert_user = new SqlCommand("sp_insert_user", connect);
                                sp_insert_user.CommandType = CommandType.StoredProcedure;

                                SqlParameter first_name = new SqlParameter("@first_name", SqlDbType.VarChar);
                                sp_insert_user.Parameters.Add(first_name).Value = txtbox_firstname.Text.Trim();

                                SqlParameter last_name = new SqlParameter("@last_name", SqlDbType.VarChar);
                                sp_insert_user.Parameters.Add(last_name).Value = txtbox_lastname.Text.Trim();

                                SqlParameter user_name = new SqlParameter("@user_name", SqlDbType.VarChar);
                                sp_insert_user.Parameters.Add(user_name).Value = txtbox_username.Text.Trim();

                                SqlParameter email = new SqlParameter("@email", SqlDbType.VarChar);
                                sp_insert_user.Parameters.Add(email).Value = txtbox_mail.Text.Trim();

                                SqlParameter password = new SqlParameter("@password", SqlDbType.NVarChar);
                                sp_insert_user.Parameters.Add(password).Value = SecureData.EncryptData(txtbox_password.Text.Trim());

                                SqlParameter confirm_password = new SqlParameter("@confirm_password", SqlDbType.NVarChar);
                                sp_insert_user.Parameters.Add(confirm_password).Value = SecureData.EncryptData(txtbox_cpassword.Text.Trim());

                                int i = sp_insert_user.ExecuteNonQuery();
                                connect.Close();

                                if (i > 0)
                                {
                                    MessageBox.Show("Registered Successfully");
                                    Login login = new Login();
                                    login.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Try Again");
                                }

                            }
                            else
                            {
                                MessageBox.Show("Password and Confirm Password Must be Same");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Password must be atleast 8 characters");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Valid E-Mail");
                    }
                }
                else
                {
                    MessageBox.Show("Please Fill the Data");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Welcome_page wp = new Welcome_page();
            wp.Show();
            this.Hide();
        }
        private void btn_back_MouseHover(object sender, EventArgs e)
        {
            btn_back.BackColor = Color.MidnightBlue;
        }

        private void btn_back_MouseLeave(object sender, EventArgs e)
        {
            btn_back.BackColor = Color.Transparent;
        }
    }
}
