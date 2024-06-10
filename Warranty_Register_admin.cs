using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace PcPoint
{
    public partial class Warranty_Register_admin : Form
    {
        public Warranty_Register_admin(string user_id, Image img)
        {
            InitializeComponent();
            loadValues();
            lbl_user_id.Text = user_id;
            profile_pic.BackgroundImage = img;
        }
        public string connectionstring = Connection.GetConnectionString();

        private void loadValues()
        {
            try
            {

                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();

                SqlCommand sp_fetch_stocks = new SqlCommand("sp_fetch_stocks", connect);
                sp_fetch_stocks.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = sp_fetch_stocks.ExecuteReader();

                AutoCompleteStringCollection product_id = new AutoCompleteStringCollection();

                while (dr.Read())
                {
                    product_id.Add(dr.GetInt32(0).ToString());
                }
                combobox_product_id.AutoCompleteCustomSource = product_id;

                combobox_product_id.DataSource = product_id;
                connect.Close();

                connect.Open();

                SqlCommand sp_fetch_sales_all = new SqlCommand("sp_fetch_sales_all", connect);
                sp_fetch_sales_all.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr1 = sp_fetch_sales_all.ExecuteReader();

                AutoCompleteStringCollection sales_id = new AutoCompleteStringCollection();

                while (dr1.Read())
                {
                    sales_id.Add(dr1.GetInt32(0).ToString());
                }
                combobox_sales_id.AutoCompleteCustomSource = sales_id;

                combobox_sales_id.DataSource = sales_id;

                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton_register_CheckedChanged(object sender, EventArgs e)
        {
            panel_register.Visible = true;
            panel_check.Visible = false;

        }

        private void radioButton_check_CheckedChanged(object sender, EventArgs e)
        {
            panel_check.Visible = true;
            panel_register.Visible = false;
        }

        private void radioButton_SearchByWarrantyId_CheckedChanged(object sender, EventArgs e)
        {
            lbl_text.Text = "Warranty ID";
            txtbox_warranty_id_check.Visible = true;
            txtbox_serial_number_check.Visible = false;
        }

        private void btn_check_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton_SearchByWarrantyId.Checked)
                {
                    lbl_text.Text = "Warranty ID";
                    txtbox_warranty_id_check.Visible = true;
                    txtbox_serial_number_check.Visible = false;
                    if (txtbox_warranty_id_check.Text.Trim() != "")
                    {

                        SqlConnection connect = new SqlConnection(connectionstring);

                        connect.Open();

                        SqlCommand sp_fetch_warranty = new SqlCommand("sp_fetch_warranty", connect);
                        sp_fetch_warranty.CommandType = CommandType.StoredProcedure;

                        SqlParameter warranty_id = new SqlParameter("@Warranty_number", SqlDbType.Int);
                        sp_fetch_warranty.Parameters.Add(warranty_id).Value = txtbox_warranty_id_check.Text.Trim();

                        SqlParameter serial_number = new SqlParameter("@serial_number", SqlDbType.VarChar);
                        sp_fetch_warranty.Parameters.Add(serial_number).Value = 0;


                        SqlDataReader dr = sp_fetch_warranty.ExecuteReader();

                        txtbox_warranty.Text = txtbox_warranty_id_check.Text;



                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                txtbox_warranty_date.Text = dr.GetDateTime(8).ToString("yyy/MM/dd");

                            }
                            panel_warranty_check.Visible = true;
                            lbl_warranty_id_text.Visible = true;
                            lbl_warranty_id_text.Text = "Warranty ID";
                            txtbox_warranty.Visible = true;
                            txtbox_warranty.Text = txtbox_warranty_id_check.Text.ToString();
                            lbl_valid_date.Visible = true;
                            txtbox_warranty_date.Visible = true;

                            string a = txtbox_warranty_date.Text.Substring(0, 4);
                            string b = txtbox_warranty_date.Text.Substring(5, 2);
                            string c = txtbox_warranty_date.Text.Substring(8, 2);
                            string warranty_date1 = a + b + c;

                            int warranty_date = Convert.ToInt32(warranty_date1);

                            DateTime date = DateTime.Now;
                            string todaydate1 = date.ToString("yyyyMMdd");

                            int todaydate = Convert.ToInt32(todaydate1);

                            if (warranty_date >= todaydate)
                            {
                                panel_warranty_valid.Visible = true;
                                panel_warranty_expired.Visible = false;
                            }
                            else
                            {
                                panel_warranty_expired.Visible = true;
                                panel_warranty_valid.Visible = false;
                            }
                        }
                        else
                        {
                            panel_warranty_check.Visible = false;
                            MessageBox.Show("Not Found!");
                            //lbl_warranty_id_text.Visible = false;
                            //txtbox_warranty.Visible = false;
                            //lbl_valid_date.Visible = false;
                            //txtbox_warranty_date.Visible = false;
                            //panel_warranty_expired.Visible = false;
                            //panel_warranty_valid.Visible = false;
                        }
                    }
                }
                else if (radioButton_SearchBySerialNumber.Checked)
                {
                    lbl_text.Text = "Serial Number";
                    txtbox_serial_number_check.Visible = true;
                    txtbox_warranty_id_check.Visible = false;
                    if (txtbox_serial_number_check.Text.Trim() != "")
                    {

                        SqlConnection connect = new SqlConnection(connectionstring);

                        connect.Open();

                        SqlCommand sp_fetch_warranty = new SqlCommand("sp_fetch_warranty", connect);
                        sp_fetch_warranty.CommandType = CommandType.StoredProcedure;

                        SqlParameter warranty_id = new SqlParameter("@Warranty_number", SqlDbType.Int);
                        sp_fetch_warranty.Parameters.Add(warranty_id).Value = 0;

                        SqlParameter serial_number = new SqlParameter("@serial_number", SqlDbType.VarChar);
                        sp_fetch_warranty.Parameters.Add(serial_number).Value = txtbox_serial_number_check.Text.Trim();


                        SqlDataReader dr = sp_fetch_warranty.ExecuteReader();

                        txtbox_warranty.Text = txtbox_warranty_id_check.Text;


                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                txtbox_warranty_date.Text = dr.GetDateTime(8).ToString("yyy/MM/dd");

                            }
                            panel_warranty_check.Visible = true;
                            lbl_warranty_id_text.Visible = true;
                            lbl_warranty_id_text.Text = "Serial Number";
                            txtbox_warranty.Visible = true;
                            txtbox_warranty.Text = txtbox_serial_number_check.Text.ToString();
                            lbl_valid_date.Visible = true;
                            txtbox_warranty_date.Visible = true;

                            string a = txtbox_warranty_date.Text.Substring(0, 4);
                            string b = txtbox_warranty_date.Text.Substring(5, 2);
                            string c = txtbox_warranty_date.Text.Substring(8, 2);
                            string warranty_date1 = a + b + c;

                            int warranty_date = Convert.ToInt32(warranty_date1);

                            DateTime date = DateTime.Now;
                            string todaydate1 = date.ToString("yyyyMMdd");

                            int todaydate = Convert.ToInt32(todaydate1);

                            if (warranty_date >= todaydate)
                            {
                                panel_warranty_valid.Visible = true;
                                panel_warranty_expired.Visible = false;
                            }
                            else
                            {
                                panel_warranty_expired.Visible = true;
                                panel_warranty_valid.Visible = false;
                            }
                        }
                        else
                        {
                            panel_warranty_check.Visible = false;
                            MessageBox.Show("Not Found!");
                            //lbl_warranty_id_text.Visible = false;
                            //txtbox_warranty.Visible = false;
                            //lbl_valid_date.Visible = false;
                            //txtbox_warranty_date.Visible = false;
                            //panel_warranty_expired.Visible = false;
                            //panel_warranty_valid.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_browse_file_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image files(*.jpg,*.png,*bmp)|*.jpg;*.png;.bmp";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    pictureBox_invoice.Image = Image.FromFile(dialog.FileName);
                    txtbox_invoice_file.Text = dialog.FileName.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtbox_product_name.Text.Trim() != "" && txtbox_serial_number.Text.Trim() != "" && txtbox_invoice_number.Text.Trim() != "" && txtbox_invoice_file.Text !="")
                {
                    if (combobox_product_id.SelectedItem != null && combobox_sales_id.SelectedItem != null)
                    {

                        SqlConnection connect = new SqlConnection(connectionstring);

                        connect.Open();

                        SqlCommand sp_insert_warranty = new SqlCommand("sp_insert_warranty", connect);
                        sp_insert_warranty.CommandType = CommandType.StoredProcedure;

                        SqlParameter product_id = new SqlParameter("@product_id", SqlDbType.Int);
                        sp_insert_warranty.Parameters.Add(product_id).Value = Convert.ToInt32(combobox_product_id.Text);

                        SqlParameter sales_id = new SqlParameter("@sales_id", SqlDbType.Int);
                        sp_insert_warranty.Parameters.Add(sales_id).Value = Convert.ToInt32(combobox_sales_id.Text);

                        SqlParameter invoice_number = new SqlParameter("@invoice_number", SqlDbType.Int);
                        sp_insert_warranty.Parameters.Add(invoice_number).Value = txtbox_invoice_number.Text.Trim();

                        SqlParameter product_name = new SqlParameter("@product_name", SqlDbType.VarChar);
                        sp_insert_warranty.Parameters.Add(product_name).Value = txtbox_product_name.Text.Trim();

                        SqlParameter purchased_date = new SqlParameter("@purchased_date", SqlDbType.Date);
                        sp_insert_warranty.Parameters.Add(purchased_date).Value = txtbox_purcahsed_date.Text.Trim();

                        SqlParameter serial_number = new SqlParameter("@serial_number", SqlDbType.VarChar);
                        sp_insert_warranty.Parameters.Add(serial_number).Value = txtbox_serial_number.Text.Trim();

                        Image img = pictureBox_invoice.Image;

                        ImageConverter coverter = new ImageConverter();
                        var ImageConvert = coverter.ConvertTo(img, typeof(byte[]));

                        SqlParameter invoice_copy = new SqlParameter("@invoice_copy", SqlDbType.Image);
                        sp_insert_warranty.Parameters.Add(invoice_copy).Value = ImageConvert;

                        DateTime purchase_date = txtbox_purcahsed_date.Value;

                        string day = purchase_date.Date.ToString("dd");

                        string warranty_date = purchase_date.Year + 1 + "-" + purchase_date.Month + "-" + day;


                        SqlParameter warranty_period = new SqlParameter("@warranty_period", SqlDbType.Date);
                        sp_insert_warranty.Parameters.Add(warranty_period).Value = warranty_date;

                        int i = sp_insert_warranty.ExecuteNonQuery();

                        if (i > 0)
                        {
                            
                            SqlCommand sp_fetch_warranty_number = new SqlCommand("sp_fetch_warranty_number", connect);
                            sp_fetch_warranty_number.CommandType = CommandType.StoredProcedure;

                            SqlParameter serial_number1 = new SqlParameter("@serial_number", SqlDbType.VarChar);
                            sp_fetch_warranty_number.Parameters.Add(serial_number1).Value = txtbox_serial_number.Text.Trim();


                            SqlDataReader dr = sp_fetch_warranty_number.ExecuteReader();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    MessageBox.Show("Warranty Registered! Your Warranty Registered Number :"+dr.GetInt32(0));
                                    txtbox_serial_number.Clear();
                                    txtbox_invoice_number.Clear();
                                    txtbox_invoice_file.Clear();
                                    txtbox_product_name.Clear();

                                }
                            }
                                
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Valid Product ID/Sales ID");
                    }

                }
                else
                {
                    MessageBox.Show("Please Fill the All Fields");
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txtbox_product_name.Clear();
            txtbox_invoice_number.Clear();
            txtbox_invoice_file.Clear();
            txtbox_serial_number.Clear();
            pictureBox_invoice.Image = null;
            combobox_product_id.SelectedItem = null;
            combobox_sales_id.SelectedItem = null;

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

        private void btn_logout_MouseHover(object sender, EventArgs e)
        {
            btn_logout.BackColor = Color.Red;
        }

        private void btn_logout_MouseLeave(object sender, EventArgs e)
        {
            btn_logout.BackColor = Color.Transparent;
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            Welcome_page wp = new Welcome_page();
            wp.Show();
            this.Hide();
        }

        private void sidebar_expand_Click(object sender, EventArgs e)
        {
            sidebar.Visible = true;
            sidebar_expand.Visible = false;
        }

        private void sidebar_hide_Click(object sender, EventArgs e)
        {

            sidebar.Visible = false;
            sidebar_expand.Visible = true;

        }

        private void btn_dashboard_s_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Dashboard_admin dashboard_Admin = new Dashboard_admin(user_id, img);
            dashboard_Admin.Show();
            this.Hide();
        }

        private void btn_dashboard_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Dashboard_admin dashboard_Admin = new Dashboard_admin(user_id, img);
            dashboard_Admin.Show();
            this.Hide();
        }

        private void btn_profile_s_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Profile_admin profile_Admin = new Profile_admin(user_id, img);
            profile_Admin.Show();
            this.Hide();

        }

        private void btn_profile_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Profile_admin profile_Admin = new Profile_admin(user_id, img);
            profile_Admin.Show();
            this.Hide();

        }

        private void btn_stock_s_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Stocks_admin stocks_Admin = new Stocks_admin(user_id, img);
            stocks_Admin.Show();
            this.Hide();
        }

        private void btn_stocks_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Stocks_admin stocks_Admin = new Stocks_admin(user_id, img);
            stocks_Admin.Show();
            this.Hide();
        }

        private void btn_userdetails_s_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            User_Details_admin user_Details_Admin = new User_Details_admin(user_id, img);
            user_Details_Admin.Show();
            this.Hide();
        }

        private void btn_userdetail_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            User_Details_admin user_Details_Admin = new User_Details_admin(user_id, img);
            user_Details_Admin.Show();
            this.Hide();
        }

        private void btn_billing_s_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Billing_admin billing_admin = new Billing_admin(user_id, img);
            billing_admin.Show();
            this.Hide();
        }

        private void btn_billing_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Billing_admin billing_admin = new Billing_admin(user_id, img);
            billing_admin.Show();
            this.Hide();
        }

        private void btn_sales_s_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            sales_admin sales_admin = new sales_admin(user_id, img);
            sales_admin.Show();
            this.Hide();
        }

        private void btn_sales_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            sales_admin sales_admin = new sales_admin(user_id, img);
            sales_admin.Show();
            this.Hide();
        }

        private void btn_employee_details_s_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Employee_details_admin employee_details_admin = new Employee_details_admin(user_id, img);
            employee_details_admin.Show();
            this.Hide();
        }

        private void btn_employee_details_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Employee_details_admin employee_details_admin = new Employee_details_admin(user_id, img);
            employee_details_admin.Show();
            this.Hide();
        }

        private void radioButton_SearchBySerialNumber_CheckedChanged(object sender, EventArgs e)
        {
            lbl_text.Text = "Serial Number";
            txtbox_serial_number_check.Visible = true;
            txtbox_warranty_id_check.Visible = false;
        }
    }
}
