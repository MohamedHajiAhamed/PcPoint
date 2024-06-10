using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.IO;


namespace PcPoint
{
    public partial class Employee_details_admin : Form
    {
        public Employee_details_admin(string user_id, Image img)
        {
            InitializeComponent();
            lbl_user_id.Text = user_id;
            profile_pic.BackgroundImage = img;
        }
        public string connectionstring = Connection.GetConnectionString();

        private void pic_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image files(*.jpg,*.png,*bmp)|*.jpg;*.png;.bmp";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    pic.BackgroundImage = Image.FromFile(dialog.FileName);
                    txtbox_pic.Text = dialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtbox_user_id.Text.Trim() != "" && txtbox_salary.Text.Trim() != "" && txtbox_aadhar_number.Text.Trim() != "" && txtbox_aadhar_file.Text.Trim() != "" && richTextBox_address.Text.Trim() != "")
                {
                    if(txtbox_pic.Text.Trim() != "")
                    {
                        SqlConnection connect = new SqlConnection(connectionstring);
                        connect.Open();
                        SqlCommand sp_insert_employee_details = new SqlCommand("sp_insert_employee_details", connect);
                        sp_insert_employee_details.CommandType = CommandType.StoredProcedure;

                        SqlParameter user_id = new SqlParameter("@user_id", SqlDbType.Int);
                        sp_insert_employee_details.Parameters.Add(user_id).Value = txtbox_user_id.Text.Trim();

                        SqlParameter @aadhar_number = new SqlParameter("@aadhar_number", SqlDbType.Int);
                        sp_insert_employee_details.Parameters.Add(@aadhar_number).Value = txtbox_aadhar_number.Text.Trim();

                        SqlParameter salary_per_annum = new SqlParameter("@salary_per_annum", SqlDbType.Float);
                        sp_insert_employee_details.Parameters.Add(salary_per_annum).Value = txtbox_salary.Text.Trim();

                        SqlParameter address = new SqlParameter("@address", SqlDbType.Text);
                        sp_insert_employee_details.Parameters.Add(address).Value = richTextBox_address.Text.Trim();


                        string filePath = txtbox_aadhar_file.Text;

                        byte[] fileData = File.ReadAllBytes(filePath);

                        SqlParameter aadhar_file = new SqlParameter("@aadhar_file", SqlDbType.VarBinary);
                        sp_insert_employee_details.Parameters.Add(aadhar_file).Value = fileData;


                        Image img = pic.BackgroundImage;

                        ImageConverter coverter = new ImageConverter();
                        var ImageConvert = coverter.ConvertTo(img, typeof(byte[]));

                        SqlParameter photo = new SqlParameter("@photo", SqlDbType.Image);
                        sp_insert_employee_details.Parameters.Add(photo).Value = ImageConvert;

                        int i = sp_insert_employee_details.ExecuteNonQuery();

                        if (i > 0)
                        {
                            MessageBox.Show("Submitted Successfully");

                        }
                        else
                        {
                            MessageBox.Show("Try Again");
                        }
                        connect.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please Add Image");
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

        private void btn_browse_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF Files|*.pdf";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                txtbox_aadhar_file.Text = filePath;
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txtbox_aadhar_file.Clear();
            txtbox_aadhar_number.Clear();
            txtbox_salary.Clear();
            txtbox_user_id.Clear();
            richTextBox_address.Clear();

        }

        private void radioButton_add_employee_CheckedChanged(object sender, EventArgs e)
        {
            panel_register.Visible = true;
            panel_view.Visible = false;
        }

        private void radioButton_view_employee_CheckedChanged(object sender, EventArgs e)
        {
            panel_view.Visible = true;
            panel_register.Visible = false;
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

        private void btn_logout_Click(object sender, EventArgs e)
        {
            Welcome_page wp = new Welcome_page();
            wp.Show();
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

        private void btn_warranty_s_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Warranty_Register_admin warranty_register_admin = new Warranty_Register_admin(user_id, img);
            warranty_register_admin.Show();
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

        private void Employee_details_admin_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();
                SqlCommand sp_fetch_employee_details = new SqlCommand("sp_fetch_employee_details", connect);
                sp_fetch_employee_details.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = sp_fetch_employee_details.ExecuteReader();

                AutoCompleteStringCollection employee_name = new AutoCompleteStringCollection();

                while (dr.Read())
                {
                    employee_name.Add(dr.GetString(2));
                }

                txtbox_employee_name.AutoCompleteCustomSource = employee_name;

                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_view_aadhar_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
                int selectedCellIndex = dataGridView1.CurrentCell.ColumnIndex;

                if (selectedRowIndex >= 0 && selectedCellIndex >= 0)
                {

                    using (SqlConnection connect = new SqlConnection(connectionstring))
                    {
                        connect.Open();
                        SqlCommand sp_fetch_aadhar_file = new SqlCommand("sp_fetch_aadhar_file", connect);
                        sp_fetch_aadhar_file.CommandType = CommandType.StoredProcedure;

                        SqlParameter employee_id = new SqlParameter("@employee_id", SqlDbType.Int);
                        sp_fetch_aadhar_file.Parameters.Add(employee_id).Value = dataGridView1.Rows[selectedRowIndex].Cells["employee_id"].Value;

                        byte[] fileData = (byte[])sp_fetch_aadhar_file.ExecuteScalar();
                        connect.Close();

                        if (fileData != null)
                        {
                            SaveFileDialog saveFileDialog = new SaveFileDialog();
                            saveFileDialog.FileName = dataGridView1.Rows[selectedRowIndex].Cells["employee_id"].Value + ".pdf";
                            saveFileDialog.Filter = "PDF File(.pdf)|*.pdf";
                            saveFileDialog.Title = "Save Aadhar File";
                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                File.WriteAllBytes(saveFileDialog.FileName, fileData);
                                System.Diagnostics.Process.Start(saveFileDialog.FileName);
                            }

                        }
                        else
                        {
                            MessageBox.Show("No PDF file found");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Please select a cell in the DataGridView");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_image_Click(object sender, EventArgs e)
        {

            try
            {
                int selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
                int selectedCellIndex = dataGridView1.CurrentCell.ColumnIndex;

                if (selectedRowIndex >= 0 && selectedCellIndex >= 0)
                {

                    using (SqlConnection connect = new SqlConnection(connectionstring))
                    {
                        connect.Open();
                        SqlCommand sp_fetch_image_file = new SqlCommand("sp_fetch_image_file", connect);
                        sp_fetch_image_file.CommandType = CommandType.StoredProcedure;

                        SqlParameter employee_id = new SqlParameter("@employee_id", SqlDbType.Int);
                        sp_fetch_image_file.Parameters.Add(employee_id).Value = dataGridView1.Rows[selectedRowIndex].Cells["employee_id"].Value;

                        byte[] fileData = (byte[])sp_fetch_image_file.ExecuteScalar();
                        connect.Close();

                        if (fileData != null)
                        {
                            SaveFileDialog saveFileDialog = new SaveFileDialog();
                            saveFileDialog.FileName = dataGridView1.Rows[selectedRowIndex].Cells["employee_id"].Value + ".png";
                            saveFileDialog.Filter = "Image File(.png,.jpeg,.bmp)|*.png|*.jpeg|*.bmp";
                            saveFileDialog.Title = "Save Image File";
                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                File.WriteAllBytes(saveFileDialog.FileName, fileData);
                                System.Diagnostics.Process.Start(saveFileDialog.FileName);
                            }

                        }
                        else
                        {
                            MessageBox.Show("No Image file found");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Please select a cell in the DataGridView");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lbl_msg.Visible = false;
            btn_image.Visible = true;
            btn_view_aadhar.Visible = true;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();
                SqlCommand sp_search_employee_name = new SqlCommand("sp_search_employee_name", connect);
                sp_search_employee_name.CommandType = CommandType.StoredProcedure;

                SqlParameter employee_name = new SqlParameter("@employee_name", SqlDbType.VarChar);
                sp_search_employee_name.Parameters.Add(employee_name).Value = txtbox_employee_name.Text.Trim();


                SqlDataAdapter sd = new SqlDataAdapter(sp_search_employee_name);
                DataTable dt = new DataTable();
                sd.Fill(dt);

                dataGridView1.DataSource = null;
                lbl_msg.Visible = false;
                btn_image.Visible = false;
                btn_view_aadhar.Visible = false;

                dataGridView1.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    lbl_msg.Visible = true;
                }

                connect.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
    }
}
