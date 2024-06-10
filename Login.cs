using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PcPoint
{
    public partial class Login : Form
    {
        public Login()
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

        private void btn_back_Click(object sender, EventArgs e)
        {
            Welcome_page wp = new Welcome_page();
            wp.Show();
            this.Hide();
        }

        private void link_lbl_forgor_password_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Forgot_Password forgot_Password = new Forgot_Password();
            forgot_Password.Show();
            this.Hide();
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtbox_username.Text.Trim() != "" && txtbox_password.Text.Trim() != "")
                {
                    
                    SqlConnection connect = new SqlConnection(connectionstring);
                    connect.Open();
                    SqlCommand sp_login = new SqlCommand("sp_login", connect);
                    sp_login.CommandType = CommandType.StoredProcedure;

                    SqlParameter user_name = new SqlParameter("@user_name", SqlDbType.VarChar);
                    sp_login.Parameters.Add(user_name).Value = txtbox_username.Text.Trim();

                    SqlParameter password = new SqlParameter("@password", SqlDbType.NVarChar);
                    sp_login.Parameters.Add(password).Value = SecureData.EncryptData(txtbox_password.Text.Trim());

                    SqlDataAdapter sda = new SqlDataAdapter(sp_login);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    connect.Close();
                    if (dt.Rows.Count > 0)
                    {
                        string user_id_fetch = dt.Rows[0][3].ToString();
                        if (dt.Rows[0][4] != DBNull.Value)
                        {
                            byte[] a = ((byte[])dt.Rows[0][4]);

                            MemoryStream ms = new MemoryStream(a);
                            Image image = Image.FromStream(ms);

                            profile_pic.BackgroundImage = image;
                        }
                        else
                        {
                            profile_pic.BackgroundImage = Properties.Resources.icons8_user_100;
                        }


                        lbl_user_id.Text = user_id_fetch;

                        string user_type = dt.Rows[0][2].ToString();

                        if (user_type == "Employee")
                        {
                            string user_id = lbl_user_id.Text;
                            Image img = profile_pic.BackgroundImage;
                            Dashboard_Employee dashboard_employee = new Dashboard_Employee(user_id,img);
                            dashboard_employee.Show();
                            this.Hide();
                        }
                        else if (user_type == "Admin")
                        {
                            string user_id = lbl_user_id.Text;
                            Image img = profile_pic.BackgroundImage;
                            Dashboard_admin dashboard_admin = new Dashboard_admin(user_id,img);
                            dashboard_admin.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter Valid UserName/Password");
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Username/Password");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_back_MouseHover(object sender, EventArgs e)
        {
            btn_back.BackColor = Color.MidnightBlue;
        }

        private void btn_back_MouseLeave(object sender, EventArgs e)
        {
            btn_back.BackColor = Color.Transparent;
        }

        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            this.txtbox_username.Clear();
            this.txtbox_password.Clear();
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            if (txtbox_password.UseSystemPasswordChar == true)
            {
                this.txtbox_password.UseSystemPasswordChar = false;
                btn_show.SendToBack();
            }
            else
            {
                this.txtbox_password.UseSystemPasswordChar = true;
                btn_show.BringToFront();

            }
        }

        private void btn_hide_Click(object sender, EventArgs e)
        {
            if (txtbox_password.UseSystemPasswordChar == false)
            {
                this.txtbox_password.UseSystemPasswordChar = true;
                btn_hide.SendToBack();

            }
            else
            {
                this.txtbox_password.UseSystemPasswordChar = false;
                btn_hide.BringToFront();

            }
        }
    }
}
