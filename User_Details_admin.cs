using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace PcPoint
{
    public partial class User_Details_admin : Form
    {
        public User_Details_admin(string user_id, Image img)
        {
            InitializeComponent();
            lbl_user_id.Text = user_id;
            profile_pic.BackgroundImage = img;
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


        private void User_Details_admin_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();
                SqlCommand sp_fetch_user = new SqlCommand("sp_fetch_user", connect);
                sp_fetch_user.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(sp_fetch_user);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                dataGridView1.DataSource = dt;
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    txtbox_user_id.Text = dataGridView1.Rows[e.RowIndex].Cells["user_id"].FormattedValue.ToString();
                    txtbox_firstname.Text = dataGridView1.Rows[e.RowIndex].Cells["first_name"].FormattedValue.ToString();
                    txtbox_lastname.Text = dataGridView1.Rows[e.RowIndex].Cells["last_name"].FormattedValue.ToString();
                    txtbox_user_name.Text = dataGridView1.Rows[e.RowIndex].Cells["user_name"].FormattedValue.ToString();
                    comboBox_user_type.Text = dataGridView1.Rows[e.RowIndex].Cells["user_type"].FormattedValue.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void refreshdata()
        {
            try
            {
                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();
                SqlCommand sp_fetch_user = new SqlCommand("sp_fetch_user", connect);
                sp_fetch_user.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(sp_fetch_user);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                dataGridView1.DataSource = dt;

                txtbox_user_id.Clear();
                txtbox_firstname.Clear();
                txtbox_lastname.Clear();
                txtbox_user_name.Clear();
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are You Sure to Update", "Update Document", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (txtbox_firstname.Text.Trim() != "" && txtbox_lastname.Text.Trim() != "" && txtbox_user_name.Text.Trim() != "" && comboBox_user_type.Text.Trim() != "")
                    {
                        SqlConnection connect = new SqlConnection(connectionstring);
                        connect.Open();
                        SqlCommand sp_update_user = new SqlCommand("sp_update_user", connect);
                        sp_update_user.CommandType = CommandType.StoredProcedure;

                        SqlParameter first_name = new SqlParameter("@first_name", SqlDbType.VarChar);
                        sp_update_user.Parameters.Add(first_name).Value = txtbox_firstname.Text.Trim();

                        SqlParameter last_name = new SqlParameter("@last_name", SqlDbType.VarChar);
                        sp_update_user.Parameters.Add(last_name).Value = txtbox_lastname.Text.Trim();

                        SqlParameter user_name = new SqlParameter("@user_name", SqlDbType.VarChar);
                        sp_update_user.Parameters.Add(user_name).Value = txtbox_user_name.Text.Trim();

                        SqlParameter user_type = new SqlParameter("@user_type", SqlDbType.VarChar);
                        sp_update_user.Parameters.Add(user_type).Value = comboBox_user_type.Text;

                        SqlParameter user_id = new SqlParameter("@user_id", SqlDbType.Int);
                        sp_update_user.Parameters.Add(user_id).Value = txtbox_user_id.Text.Trim();

                        int i = sp_update_user.ExecuteNonQuery();

                        if (i > 0)
                        {
                            MessageBox.Show("updated Successfully");

                            txtbox_user_id.Clear();
                            txtbox_firstname.Clear();
                            txtbox_lastname.Clear();
                            txtbox_user_name.Clear();
                            refreshdata();

                        }
                        else
                        {
                            MessageBox.Show("Try Again");
                        }
                        connect.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please Fill the Data");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

            try
            {
                if (MessageBox.Show("Are You Sure to Delete", "Delete Document",MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (txtbox_user_id.Text.Trim() != "")
                    {

                        SqlConnection connect = new SqlConnection(connectionstring);
                        connect.Open();
                        SqlCommand sp_delete_user = new SqlCommand("sp_delete_user", connect);
                        sp_delete_user.CommandType = CommandType.StoredProcedure;

                        SqlParameter user_id = new SqlParameter("@user_id", SqlDbType.Int);
                        sp_delete_user.Parameters.Add(user_id).Value = txtbox_user_id.Text.Trim();


                        int i = sp_delete_user.ExecuteNonQuery();

                        if (i > 0)
                        {
                            MessageBox.Show("Deleted Successfully");
                            txtbox_user_id.Clear();
                            txtbox_firstname.Clear();
                            txtbox_lastname.Clear();
                            txtbox_user_name.Clear();
                            refreshdata();

                        }
                        else
                        {
                            MessageBox.Show("Try Again");
                        }
                        connect.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please Fill the Data");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    txtbox_user_id.Text = dataGridView1.Rows[e.RowIndex].Cells["user_id"].FormattedValue.ToString();
                    txtbox_firstname.Text = dataGridView1.Rows[e.RowIndex].Cells["first_name"].FormattedValue.ToString();
                    txtbox_lastname.Text = dataGridView1.Rows[e.RowIndex].Cells["last_name"].FormattedValue.ToString();
                    txtbox_user_name.Text = dataGridView1.Rows[e.RowIndex].Cells["user_name"].FormattedValue.ToString();
                    comboBox_user_type.Text = dataGridView1.Rows[e.RowIndex].Cells["user_type"].FormattedValue.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    txtbox_user_id.Text = dataGridView1.Rows[e.RowIndex].Cells["user_id"].FormattedValue.ToString();
                    txtbox_firstname.Text = dataGridView1.Rows[e.RowIndex].Cells["first_name"].FormattedValue.ToString();
                    txtbox_lastname.Text = dataGridView1.Rows[e.RowIndex].Cells["last_name"].FormattedValue.ToString();
                    txtbox_user_name.Text = dataGridView1.Rows[e.RowIndex].Cells["user_name"].FormattedValue.ToString();
                    comboBox_user_type.Text = dataGridView1.Rows[e.RowIndex].Cells["user_type"].FormattedValue.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
