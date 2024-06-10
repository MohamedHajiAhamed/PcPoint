using System;
using System.Drawing;
using System.Windows.Forms;

namespace PcPoint
{
    public partial class Dashboard_admin : Form
    {
        public Dashboard_admin(string user_id, Image img)
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
            Profile_admin profile_admin = new Profile_admin(user_id, img);

            profile_admin.Show();
            this.Hide();
        }

        private void btn_user_detail_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            User_Details_admin user_Details_admin = new User_Details_admin(user_id, img);
            user_Details_admin.Show();
            this.Hide();
        }

        private void btn_Stocks_admin_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Stocks_admin stocks_admin = new Stocks_admin(user_id, img);
            stocks_admin.Show();
            this.Hide();
        }

        private void btn_profile_s_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Profile_admin profile_admin = new Profile_admin(user_id, img);
            profile_admin.Show();
            this.Hide();
        }

        private void btn_billing_admin_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Billing_admin billing_Admin = new Billing_admin(user_id, img);
            billing_Admin.Show();
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
            Profile_admin profile_admin = new Profile_admin(user_id, img);
            profile_admin.Show();
            this.Hide();
        }

        private void btn_warranty_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Warranty_Register_admin warranty_register_admin = new Warranty_Register_admin(user_id, img);
            warranty_register_admin.Show();
            this.Hide();
        }

        private void btn_sales_admin_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            sales_admin sales_admin = new sales_admin(user_id, img);
            sales_admin.Show();
            this.Hide();

        }

        private void btn_employee_details_admin_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Employee_details_admin employee_details_admin = new Employee_details_admin(user_id, img);
            employee_details_admin.Show();
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
    }
}
