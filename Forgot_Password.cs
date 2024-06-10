using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.Mail;
using System.Windows.Forms;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace PcPoint
{
    public partial class Forgot_Password : Form
    {
        public Forgot_Password()
        {
            InitializeComponent();
           
        }

        public string connectionstring = Connection.GetConnectionString();
        public int GenerateRandomOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }

        private void btn_change_pwd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtbox_password.Text != "" && txtbox_confirm_password.Text != "")
                {
                    if (txtbox_password.Text == txtbox_confirm_password.Text)
                    {
                        if (txtbox_password.Text.Length >= 8)
                        {
                            SqlConnection connect = new SqlConnection(connectionstring);
                            connect.Open();
                            SqlCommand sp_forgot_password_change = new SqlCommand("sp_forgot_password_change", connect);
                            sp_forgot_password_change.CommandType = CommandType.StoredProcedure;

                            SqlParameter user_id = new SqlParameter("@user_id", SqlDbType.Int);
                            sp_forgot_password_change.Parameters.Add(user_id).Value = Convert.ToInt32(lbl_user_id.Text);

                            SqlParameter password = new SqlParameter("@password", SqlDbType.NVarChar);
                            sp_forgot_password_change.Parameters.Add(password).Value = SecureData.EncryptData(txtbox_password.Text.Trim());

                            SqlParameter confirm_password = new SqlParameter("@confirm_password", SqlDbType.NVarChar);
                            sp_forgot_password_change.Parameters.Add(confirm_password).Value = SecureData.EncryptData(txtbox_confirm_password.Text.Trim());

                            int i = sp_forgot_password_change.ExecuteNonQuery();
                            connect.Close();

                            if (i > 0)
                            {
                                MessageBox.Show("Password Changed Successfuly");
                                Login login = new Login();
                                this.Hide();
                                login.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Password & Confirm Password Atleast have 8 Characters long");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Password & Confirm Password Mismatched");
                    }
                }
                else
                {
                    MessageBox.Show("Enter Password/Confirm Password");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
            Login login = new Login();
            this.Hide();
            login.Show();
        }

       

        private void btn_back_MouseHover(object sender, EventArgs e)
        {
            btn_back.BackColor = Color.MidnightBlue;
        }

        private void btn_back_MouseLeave(object sender, EventArgs e)
        {
            btn_back.BackColor = Color.Transparent;
        }

        private void radioButton_email_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_mail.Visible = true;
            groupBox_mobile.Visible = false;

        }

        private void btn_send_otp_Click(object sender, EventArgs e)
        {

            try
            {

                if (txtbox_username.Text.Trim() != "" && txtbox_email.Text.Trim() != "")
                {
                    SqlConnection connect = new SqlConnection(connectionstring);
                    connect.Open();
                    SqlCommand sp_forget_password = new SqlCommand("sp_forget_password", connect);
                    sp_forget_password.CommandType = CommandType.StoredProcedure;

                    SqlParameter email = new SqlParameter("@email", SqlDbType.VarChar);
                    sp_forget_password.Parameters.Add(email).Value = txtbox_email.Text.Trim();

                    SqlParameter username = new SqlParameter("@username", SqlDbType.VarChar);
                    sp_forget_password.Parameters.Add(username).Value = txtbox_username.Text.Trim();

                    SqlDataAdapter sda1 = new SqlDataAdapter(sp_forget_password);
                    DataTable dt = new DataTable();
                    sda1.Fill(dt);

                    connect.Close();
                    if (dt.Rows.Count > 0)
                    {
                        string emailval = dt.Rows[0][0].ToString();
                        string username1 = dt.Rows[0][2].ToString();
                        string user_id_fetch = dt.Rows[0][1].ToString();

                        lbl_user_id.Text = user_id_fetch;
                        lbl_loading.Visible = true;
                        int generatedOTP = GenerateRandomOTP();
                        lbl_otp.Text = generatedOTP.ToString();
                        string userEmail = txtbox_email.Text.Trim();
                       
                       if (MessageBox.Show("Entered E-Mail is: " + userEmail, "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Question) == DialogResult.OK) 
                        {
                            lbl_loading.Visible = true;
                            SmtpClient client = new SmtpClient("smtp.gmail.com");
                            
                            client.Port = 587;
                            client.Credentials = new System.Net.NetworkCredential("pcpoint656@gmail.com", " ");
                            client.EnableSsl = true;
                            MailMessage mail = new MailMessage();
                            mail.From = new MailAddress("pcpoint656@gmail.com");
                            mail.To.Add(userEmail);
                            mail.Subject = "OTP Verification from PC Point";
                            mail.Body = $"Your OTP is: {generatedOTP}";

                            client.Send(mail);

                            lbl_loading.Visible = false;
                            MessageBox.Show("OTP has been sent to your email. Check your inbox!");
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("UserName & E-Mail Mismatched");
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Your Valid UserName & Email");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_verifyOtp_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtbox_otp.Text != "")
                {
                    int enteredOTP = Convert.ToInt32(txtbox_otp.Text);
                    int generatedOTP = Convert.ToInt32(lbl_otp.Text);
                    if (enteredOTP == generatedOTP)
                    {
                        MessageBox.Show("OTP verification successful!");
                        groupBox1.Visible = true;

                    }
                    else
                    {
                        MessageBox.Show("Incorrect OTP. Please try again.");
                    }
                }
                else
                {
                    MessageBox.Show("Enter OTP");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Enter Valid OTP");
            }
        }

        private void radioButton_mobile_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_mail.Visible = false;
            groupBox_mobile.Visible = true;
        }

        private void btn_verifyOtp_mobile_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtbox_otp_mobile.Text != "")
                {
                    int enteredOTP = Convert.ToInt32(txtbox_otp_mobile.Text);
                    int generatedOTP = Convert.ToInt32(lbl_otp.Text);
                    if (enteredOTP == generatedOTP)
                    {
                        MessageBox.Show("OTP verification successful!");
                        groupBox1.Visible = true;

                    }
                    else
                    {
                        MessageBox.Show("Incorrect OTP. Please try again.");
                    }
                }
                else
                {
                    MessageBox.Show("Enter OTP");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Enter Valid OTP");
            }
        }

        private void btn_send_otp_mobile_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();
                SqlCommand sp_forget_password_userName = new SqlCommand("sp_forget_password_userName", connect);
                sp_forget_password_userName.CommandType = CommandType.StoredProcedure;

                SqlParameter user_name = new SqlParameter("@user_name", SqlDbType.VarChar);
                sp_forget_password_userName.Parameters.Add(user_name).Value = txtbox_username.Text.Trim();

                SqlDataAdapter sda = new SqlDataAdapter(sp_forget_password_userName);

                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string user_id_fetch = dt.Rows[0][0].ToString();
                    string user_name1 = dt.Rows[0][1].ToString();

                    lbl_user_id.Text = user_id_fetch;
                    connect.Close();
                    if (user_name1 == txtbox_username.Text.Trim())
                    {

                        lbl_loading.Visible = true;
                        int generatedOTP = GenerateRandomOTP();
                        lbl_otp.Text = generatedOTP.ToString();

                        var message = MessageResource.Create(
                            body: $"Your OTP is: {generatedOTP}",
                            from: new Twilio.Types.PhoneNumber("+13344384816"),
                            to: new Twilio.Types.PhoneNumber("+91" + txtbox_mobile_number.Text.Trim())
                        );
                        lbl_loading.Visible = false;
                        MessageBox.Show("OTP has been sent to your Mobile Number!");
                    }
                    else
                    {
                        MessageBox.Show("UserName Doesn't Exist");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            if (txtbox_password.UseSystemPasswordChar == true)
            {
                txtbox_password.UseSystemPasswordChar = false;
                txtbox_confirm_password.UseSystemPasswordChar = false;
                btn_show.SendToBack();
            }
            else
            {
                txtbox_password.UseSystemPasswordChar = true;
                txtbox_confirm_password.UseSystemPasswordChar = true;
                btn_show.BringToFront();

            }
        }

        private void btn_hide_Click(object sender, EventArgs e)
        {
            if (txtbox_password.UseSystemPasswordChar == false)
            {
                txtbox_password.UseSystemPasswordChar = true;
                txtbox_confirm_password.UseSystemPasswordChar = true;
                btn_hide.SendToBack();

            }
            else
            {
                txtbox_password.UseSystemPasswordChar = false;
                txtbox_confirm_password.UseSystemPasswordChar = false;
                btn_hide.BringToFront();

            }
        }
    }
}
