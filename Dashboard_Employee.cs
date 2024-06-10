using System;
using System.Drawing;
using System.Windows.Forms;

namespace PcPoint
{
    public partial class Dashboard_Employee : Form
    {
        public Dashboard_Employee(string user_id, Image img)
        {
            InitializeComponent();
            lbl_user_id.Text = user_id;
            profile_pic.BackgroundImage = img;
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

        private void btn_profile_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Profile_Employee profile_Employee = new Profile_Employee(user_id, img);

            profile_Employee.Show();
            this.Hide();
        }

        private void btn_Stocks_admin_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Stocks_Employee stocks_Employee = new Stocks_Employee(user_id, img);
            stocks_Employee.Show();
            this.Hide();
        }

        private void btn_profile_s_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Profile_Employee profile_Employee = new Profile_Employee(user_id, img);
            profile_Employee.Show();
            this.Hide();
        }

        private void btn_billing_admin_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Billing_Employee Billing_Employee = new Billing_Employee(user_id, img);
            Billing_Employee.Show();
            this.Hide();
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            Welcome_page wp = new Welcome_page();
            wp.Show();
            this.Hide();
        }

        private void profile_pic_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Profile_Employee profile_Employee = new Profile_Employee(user_id, img);
            profile_Employee.Show();
            this.Hide();
        }

        private void btn_warranty_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Warranty_Register_Employee warranty_register_Employee = new Warranty_Register_Employee(user_id, img);
            warranty_register_Employee.Show();
            this.Hide();
        }

        private void btn_logout_MouseHover(object sender, EventArgs e)
        {
            btn_logout.BackColor = Color.Red;
        }

        private void btn_logout_MouseLeave(object sender, EventArgs e)
        {
            btn_logout.BackColor = Color.Transparent;
        }

        private void profile_pic_Click_1(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Profile_Employee profile_Employee = new Profile_Employee(user_id, img);
            profile_Employee.Show();
            this.Hide();
        }
    }
}
