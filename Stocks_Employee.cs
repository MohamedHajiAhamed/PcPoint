using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace PcPoint
{
    public partial class Stocks_Employee : Form
    {
        public Stocks_Employee(string user_id, Image img)
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

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Please Enter Valid Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void refreshdata()
        {
            try
            {

                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();
                SqlCommand sp_fetch_stocks = new SqlCommand("sp_fetch_stocks", connect);
                sp_fetch_stocks.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(sp_fetch_stocks);
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

        private void btn_add_Click(object sender, EventArgs e)
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
                        SqlCommand sp_insert = new SqlCommand("sp_insert_stocks", connect);
                        sp_insert.CommandType = CommandType.StoredProcedure;

                        SqlParameter product_name = new SqlParameter("@product_name", SqlDbType.VarChar);
                        sp_insert.Parameters.Add(product_name).Value = dataGridView1.Rows[selectedRowIndex].Cells["product_name"].Value;

                        SqlParameter model_name = new SqlParameter("@model_name", SqlDbType.VarChar);
                        sp_insert.Parameters.Add(model_name).Value = dataGridView1.Rows[selectedRowIndex].Cells["model_name"].Value;

                        SqlParameter quantity = new SqlParameter("@quantity", SqlDbType.Int);
                        sp_insert.Parameters.Add(quantity).Value = dataGridView1.Rows[selectedRowIndex].Cells["quantity"].Value;

                        SqlParameter cost = new SqlParameter("@cost", SqlDbType.Float);
                        sp_insert.Parameters.Add(cost).Value = dataGridView1.Rows[selectedRowIndex].Cells["cost"].Value;

                        int i = sp_insert.ExecuteNonQuery();

                        if (i > 0)
                        {

                            MessageBox.Show("Inserted Successfully");
                            refreshdata();
                        }
                        else
                        {
                            MessageBox.Show("Try Again");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row in the DataGridView");
                }
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
                int selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
                int selectedCellIndex = dataGridView1.CurrentCell.ColumnIndex;

                if (selectedRowIndex >= 0 && selectedCellIndex >= 0)
                {

                    SqlConnection connect = new SqlConnection(connectionstring);
                    connect.Open();
                    SqlCommand sp_update_stocks = new SqlCommand("sp_update_stocks", connect);
                    sp_update_stocks.CommandType = CommandType.StoredProcedure;

                    SqlParameter product_id = new SqlParameter("@product_id", SqlDbType.Int);
                    sp_update_stocks.Parameters.Add(product_id).Value = dataGridView1.Rows[selectedRowIndex].Cells["product_id"].Value;

                    SqlParameter product_name = new SqlParameter("@product_name", SqlDbType.VarChar);
                    sp_update_stocks.Parameters.Add(product_name).Value = dataGridView1.Rows[selectedRowIndex].Cells["product_name"].Value;

                    SqlParameter model_name = new SqlParameter("@model_name", SqlDbType.VarChar);
                    sp_update_stocks.Parameters.Add(model_name).Value = dataGridView1.Rows[selectedRowIndex].Cells["model_name"].Value;

                    SqlParameter quantity = new SqlParameter("@quantity", SqlDbType.Int);
                    sp_update_stocks.Parameters.Add(quantity).Value = dataGridView1.Rows[selectedRowIndex].Cells["quantity"].Value;

                    SqlParameter cost = new SqlParameter("@cost", SqlDbType.Float);
                    sp_update_stocks.Parameters.Add(cost).Value = dataGridView1.Rows[selectedRowIndex].Cells["cost"].Value;

                    int i = sp_update_stocks.ExecuteNonQuery();

                    if (i > 0)
                    {
                        MessageBox.Show("updated Successfully");
                        refreshdata();

                    }
                    else
                    {
                        MessageBox.Show("Try Again");
                    }
                    connect.Close();
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
                int selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
                int selectedCellIndex = dataGridView1.CurrentCell.ColumnIndex;

                if (selectedRowIndex >= 0 && selectedCellIndex >= 0)
                {
                    if (MessageBox.Show("Are You Sure to Delete", "Delete Document", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        using (SqlConnection connect = new SqlConnection(connectionstring))
                        {
                            connect.Open();
                            SqlCommand sp_delete_stocks = new SqlCommand("sp_delete_stocks", connect);
                            sp_delete_stocks.CommandType = CommandType.StoredProcedure;

                            SqlParameter product_id = new SqlParameter("@product_id", SqlDbType.Int);
                            sp_delete_stocks.Parameters.Add(product_id).Value = dataGridView1.Rows[selectedRowIndex].Cells["product_id"].Value;

                            int i = sp_delete_stocks.ExecuteNonQuery();

                            if (i > 0)
                            {
                                MessageBox.Show("Deleted Successfully");
                                refreshdata();
                                dataGridView1.Rows[selectedRowIndex].Cells[selectedCellIndex].Value = DBNull.Value;
                            }
                            else
                            {
                                MessageBox.Show("Try Again");
                            }
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


        private void btn_profile_s_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Profile_Employee profile_Employee = new Profile_Employee(user_id, img);
            profile_Employee.Show();
            this.Hide();
        }

        private void btn_profile_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Profile_Employee profile_Employee = new Profile_Employee(user_id, img);
            profile_Employee.Show();
            this.Hide();
        }

        private void btn_dashboard_s_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Dashboard_Employee Dashboard_Employee = new Dashboard_Employee(user_id, img);
            Dashboard_Employee.Show();
            this.Hide();
        }

        private void btn_dashboard_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Dashboard_Employee Dashboard_Employee = new Dashboard_Employee(user_id, img);
            Dashboard_Employee.Show();
            this.Hide();
        }

        private void btn_billing_s_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Billing_Employee Billing_Employee = new Billing_Employee(user_id, img);
            Billing_Employee.Show();
            this.Hide();
        }

        private void btn_billing_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Billing_Employee Billing_Employee = new Billing_Employee(user_id, img);
            Billing_Employee.Show();
            this.Hide();
        }

        private void btn_warranty_s_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Warranty_Register_Employee warranty_register_Employee = new Warranty_Register_Employee(user_id, img);
            warranty_register_Employee.Show();
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

        private void radioButton_SearchByProductName_CheckedChanged(object sender, EventArgs e)
        {
            lbl_text.Text = "Product Name";
            txtbox_product_name.Visible = true;
            txtbox_product_id.Visible = false;
            txtbox_model_name.Visible = false;
        }

        private void radioButton_SearchByModelName_CheckedChanged(object sender, EventArgs e)
        {
            lbl_text.Text = "Model Name";
            txtbox_model_name.Visible = true;
            txtbox_product_name.Visible = false;
            txtbox_product_id.Visible = false;
        }

        private void radioButton_SearchByProductId_CheckedChanged(object sender, EventArgs e)
        {
            lbl_text.Text = "Product ID";
            txtbox_product_id.Visible = true;
            txtbox_product_name.Visible = false;
            txtbox_model_name.Visible = false;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection connect = new SqlConnection(connectionstring);

                connect.Open();

                if (radioButton_SearchByProductName.Checked)
                {
                    if (txtbox_product_name.Text.Trim() != "")
                    {
                        SqlCommand sp_search_product_name = new SqlCommand("sp_search_product_name", connect);
                        sp_search_product_name.CommandType = CommandType.StoredProcedure;

                        SqlParameter product_name = new SqlParameter("@product_name", SqlDbType.VarChar);
                        sp_search_product_name.Parameters.Add(product_name).Value = txtbox_product_name.Text.Trim();

                        SqlDataAdapter da = new SqlDataAdapter(sp_search_product_name);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Enter Product Name");
                    }


                }
                else if (radioButton_SearchByModelName.Checked)
                {
                    if (txtbox_model_name.Text.Trim() != "")
                    {
                        SqlCommand sp_search_model_name = new SqlCommand("sp_search_model_name", connect);
                        sp_search_model_name.CommandType = CommandType.StoredProcedure;

                        SqlParameter model_name = new SqlParameter("@model_name", SqlDbType.VarChar);
                        sp_search_model_name.Parameters.Add(model_name).Value = txtbox_model_name.Text.Trim();

                        SqlDataAdapter da = new SqlDataAdapter(sp_search_model_name);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Enter Model Name");
                    }

                }
                else if (radioButton_SearchByProductId.Checked)
                {
                    if (txtbox_product_id.Text.Trim() != "")
                    {
                        SqlCommand sp_search_product_id = new SqlCommand("sp_search_product_id", connect);
                        sp_search_product_id.CommandType = CommandType.StoredProcedure;

                        SqlParameter product_id = new SqlParameter("@product_id", SqlDbType.Int);
                        sp_search_product_id.Parameters.Add(product_id).Value = Convert.ToInt32(txtbox_product_id.Text.Trim());

                        SqlDataAdapter da = new SqlDataAdapter(sp_search_product_id);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Enter Product ID");
                    }

                }
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            refreshdata();
            txtbox_model_name.Clear();
            txtbox_product_id.Clear();
            txtbox_product_name.Clear();
        }

        private void Stocks_Employee_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();
                SqlCommand sp_fetch_stocks = new SqlCommand("sp_fetch_stocks", connect);
                sp_fetch_stocks.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(sp_fetch_stocks);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                dataGridView1.DataSource = dt;

                AutoCompleteStringCollection product_name = new AutoCompleteStringCollection();

                AutoCompleteStringCollection product_id = new AutoCompleteStringCollection();

                AutoCompleteStringCollection model_name = new AutoCompleteStringCollection();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    product_name.Add(dt.Rows[i][1].ToString());

                    product_id.Add(dt.Rows[i][0].ToString());

                    model_name.Add(dt.Rows[i][2].ToString());
                }

                txtbox_product_name.AutoCompleteCustomSource = product_name;

                txtbox_product_id.AutoCompleteCustomSource = product_id;

                txtbox_model_name.AutoCompleteCustomSource = model_name;
                connect.Close();

                this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dataGridView1_DataError);


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
