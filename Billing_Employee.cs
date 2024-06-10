using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace PcPoint
{
    public partial class Billing_Employee : Form
    {
        public Billing_Employee(string user_id, Image img)
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

        private void btn_stock_s_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Stocks_Employee stocks_Employee = new Stocks_Employee(user_id, img);
            stocks_Employee.Show();
            this.Hide();
        }

        private void btn_stocks_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Stocks_Employee stocks_Employee = new Stocks_Employee(user_id, img);
            stocks_Employee.Show();
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

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();
                int requestedQuantity = Convert.ToInt32(txtbox_quantity.Text.Trim());

                SqlCommand sp_fetch_availabilty_quantity = new SqlCommand("sp_fetch_availabilty_quantity", connect);
                sp_fetch_availabilty_quantity.CommandType = CommandType.StoredProcedure;

                SqlParameter product_id_deduct1 = new SqlParameter("@product_id", SqlDbType.Int);
                sp_fetch_availabilty_quantity.Parameters.Add(product_id_deduct1).Value = txtbox_product_id.Text.Trim();

                SqlDataReader reader = sp_fetch_availabilty_quantity.ExecuteReader();

                reader.Read();
                int availableQuantity = reader.GetInt32(0);

                connect.Close();

                if (availableQuantity >= requestedQuantity)
                {
                    string connectionstring1 = ConfigurationManager.ConnectionStrings["pc_point_connection"].ConnectionString;

                    SqlConnection connect1 = new SqlConnection(connectionstring1);
                    connect1.Open();
                    SqlCommand sp_deduct_stocks = new SqlCommand("sp_deduct_stocks", connect1);
                    sp_deduct_stocks.CommandType = CommandType.StoredProcedure;

                    SqlParameter quantity_deduct = new SqlParameter("@quantity", SqlDbType.Int);
                    sp_deduct_stocks.Parameters.Add(quantity_deduct).Value = txtbox_quantity.Text.Trim();

                    SqlParameter product_id_deduct = new SqlParameter("@product_id", SqlDbType.Int);
                    sp_deduct_stocks.Parameters.Add(product_id_deduct).Value = txtbox_product_id.Text.Trim();


                    int i = sp_deduct_stocks.ExecuteNonQuery();
                    dataGridView1.DataSource = null;
                    SqlCommand sp_fetch_stocks = new SqlCommand("sp_fetch_stocks", connect);
                    sp_fetch_stocks.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter sda = new SqlDataAdapter(sp_fetch_stocks);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    dataGridView1.DataSource = dt;

                    if (i > 0)
                    {
                        string product_id = txtbox_product_id.Text;
                        string product_name = txtbox_product_name.Text;
                        string model_name = txtbox_model_name.Text;
                        string quantity = txtbox_quantity.Text;
                        string cost = txtbox_cost.Text;

                        string[] row = { product_id, product_name, model_name, quantity, cost };
                        dataGridView2.Rows.Add(row);

                    }
                    txtbox_product_id.Clear();
                    txtbox_product_name.Clear();
                    txtbox_model_name.Clear();
                    txtbox_quantity.Clear();
                    txtbox_cost.Clear();

                    dataGridView1.Refresh();

                    connect1.Close();
                }
                else
                {
                    MessageBox.Show("Entered Quantity is no Available");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateTotalPrice()
        {
            try
            {

                SqlConnection connect = new SqlConnection(connectionstring);

                connect.Open();

                string selectedProductName = txtbox_product_name.Text;

                if (int.TryParse(txtbox_quantity.Text, out int parsedQuantity))
                {
                    SqlCommand sp_fetch_cost_per_unit = new SqlCommand("sp_fetch_cost_per_unit", connect);

                    sp_fetch_cost_per_unit.CommandType = CommandType.StoredProcedure;

                    SqlParameter product_name = new SqlParameter("@product_name", SqlDbType.VarChar);
                    sp_fetch_cost_per_unit.Parameters.Add(product_name).Value = selectedProductName;

                    SqlDataReader reader = sp_fetch_cost_per_unit.ExecuteReader();

                    if (reader.Read())
                    {
                        double unitPrice = reader.GetDouble(0);
                        double totalPrice = unitPrice * parsedQuantity;
                        txtbox_cost.Text = totalPrice.ToString();

                    }
                    else
                    {
                        MessageBox.Show("Product not found");
                    }

                    connect.Close();
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
                string productId = txtbox_product_id.Text;
                string productName = txtbox_product_name.Text;

                string quantity = txtbox_quantity.Text;
                string cost = txtbox_cost.Text;

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells["dataGridViewproduct_id"].Value.ToString() == productId)
                    {
                        row.Cells["dataGridViewproduct_name"].Value = productName;
                        row.Cells["dataGridViewquantity"].Value = quantity;
                        row.Cells["dataGridViewcost"].Value = cost;

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btn_checkout_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();


                SqlCommand sp_fetch_user_single = new SqlCommand("sp_fetch_user_single", connect);
                sp_fetch_user_single.CommandType = CommandType.StoredProcedure;

                SqlParameter user_id1 = new SqlParameter("@user_id", SqlDbType.Int);
                sp_fetch_user_single.Parameters.Add(user_id1).Value = lbl_user_id.Text;


                SqlDataAdapter sda = new SqlDataAdapter(sp_fetch_user_single);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                string first_name = dt.Rows[0][0].ToString();
                string last_name = dt.Rows[0][1].ToString();

                SqlCommand sp_insert_sales = new SqlCommand("sp_insert_sales", connect);
                sp_insert_sales.CommandType = CommandType.StoredProcedure;


                SqlParameter invoice_number = new SqlParameter("@invoice_number", SqlDbType.Int);

                SqlParameter product_id = new SqlParameter("@product_id", SqlDbType.Int);

                SqlParameter product_name = new SqlParameter("@product_name", SqlDbType.VarChar);

                SqlParameter model_name = new SqlParameter("@model_name", SqlDbType.VarChar);

                SqlParameter quantity = new SqlParameter("@quantity", SqlDbType.Int);

                SqlParameter cost = new SqlParameter("@cost", SqlDbType.Float);

                SqlParameter sales_person = new SqlParameter("@sales_person", SqlDbType.VarChar);

                SqlParameter sales_date = new SqlParameter("@sales_date", SqlDbType.Date);

                sp_insert_sales.Parameters.Add(invoice_number);
                sp_insert_sales.Parameters.Add(product_id);
                sp_insert_sales.Parameters.Add(product_name);
                sp_insert_sales.Parameters.Add(model_name);
                sp_insert_sales.Parameters.Add(quantity);
                sp_insert_sales.Parameters.Add(cost);
                sp_insert_sales.Parameters.Add(sales_person);
                sp_insert_sales.Parameters.Add(sales_date);

                DateTime date = DateTime.Now;
                string Customdate = date.ToString("yyyy-MM-dd");
                //string date1 = $"{date.Year}-{date.Month}-{date.Day}/{date.Hour}:{date.Minute}";
                int invoice_no1 = date.Year + date.Minute + date.Millisecond + date.Second + date.Hour;
                lbl_invoice_no.Text = invoice_no1.ToString();

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    invoice_number.Value = invoice_no1;
                    product_id.Value = Convert.ToInt32(dataGridView2.Rows[i].Cells["dataGridViewproduct_id"].Value);
                    product_name.Value = dataGridView2.Rows[i].Cells["dataGridViewproduct_name"].Value;
                    model_name.Value = dataGridView2.Rows[i].Cells["dataGridViewmodel_name"].Value;
                    quantity.Value = Convert.ToInt32(dataGridView2.Rows[i].Cells["dataGridViewquantity"].Value);
                    cost.Value = Convert.ToDecimal(dataGridView2.Rows[i].Cells["dataGridViewcost"].Value);
                    sales_person.Value = ($"{first_name}_{last_name}");
                    sales_date.Value = Customdate;

                    sp_insert_sales.ExecuteNonQuery();
                    connect.Close();
                }


                DataTable data = new DataTable();

                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    data.Columns.Add(dataGridView2.Columns[i].HeaderText);
                }


                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    DataRow row = data.NewRow();
                    for (int j = 0; j < dataGridView2.Columns.Count; j++)
                    {
                        row[j] = dataGridView2.Rows[i].Cells[j].FormattedValue.ToString();
                    }
                    data.Rows.Add(row);

                }

                decimal totalCost = 0;
                foreach (DataRow row in data.Rows)
                {

                    if (row["Cost"] != DBNull.Value)
                    {
                        totalCost += Convert.ToDecimal(row["Cost"]);
                    }
                }

                data.Rows.Add("", "", "Total Cost = ", "", totalCost);


                int invoice_no = Convert.ToInt32(lbl_invoice_no.Text);
                string user_id = lbl_user_id.Text;
                Image img = profile_pic.BackgroundImage;
                Payment_admin p = new Payment_admin(data, invoice_no, user_id, img);
                this.Hide();
                p.Show();
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
                string ColumnName = "dataGridViewproduct_id";

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    DataGridViewCell cell = row.Cells[ColumnName];

                    if (cell.Value != null && !cell.Value.Equals(DBNull.Value))
                    {
                        if (cell.Value.ToString() == txtbox_product_id.Text)
                        {
                            dataGridView2.Rows.Remove(row);
                            dataGridView2.Refresh();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Billing_Employee_Load(object sender, EventArgs e)
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
                txtbox_product_id.Clear();
                txtbox_product_name.Clear();
                txtbox_model_name.Clear();
                txtbox_quantity.Clear();
                txtbox_cost.Clear();

                SqlCommand sp_fetch_stock = new SqlCommand("sp_fetch_stock", connect);
                sp_fetch_stock.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = sp_fetch_stock.ExecuteReader();


                AutoCompleteStringCollection product_name = new AutoCompleteStringCollection();

                AutoCompleteStringCollection product_id = new AutoCompleteStringCollection();

                AutoCompleteStringCollection model_name = new AutoCompleteStringCollection();

                while (dr.Read())
                {
                    product_id.Add(dr.GetInt32(0).ToString());
                    product_name.Add(dr.GetString(1));
                    model_name.Add(dr.GetString(2));
                }
                txtbox_product_id.AutoCompleteCustomSource = product_id;
                txtbox_product_name.AutoCompleteCustomSource = product_name;
                txtbox_model_name.AutoCompleteCustomSource = model_name;

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
                    txtbox_product_id.Text = dataGridView1.Rows[e.RowIndex].Cells["product_id"].FormattedValue.ToString();
                    txtbox_product_name.Text = dataGridView1.Rows[e.RowIndex].Cells["product_name"].FormattedValue.ToString();
                    txtbox_model_name.Text = dataGridView1.Rows[e.RowIndex].Cells["model_name"].FormattedValue.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    txtbox_product_id.Text = dataGridView2.Rows[e.RowIndex].Cells["dataGridViewproduct_id"].FormattedValue.ToString();
                    txtbox_product_name.Text = dataGridView2.Rows[e.RowIndex].Cells["dataGridViewproduct_name"].FormattedValue.ToString();
                    txtbox_model_name.Text = dataGridView2.Rows[e.RowIndex].Cells["dataGridViewmodel_name"].FormattedValue.ToString();
                    txtbox_quantity.Text = dataGridView2.Rows[e.RowIndex].Cells["dataGridViewquantity"].FormattedValue.ToString();
                    txtbox_cost.Text = dataGridView2.Rows[e.RowIndex].Cells["dataGridViewcost"].FormattedValue.ToString();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    txtbox_product_id.Text = dataGridView1.Rows[e.RowIndex].Cells["product_id"].FormattedValue.ToString();
                    txtbox_product_name.Text = dataGridView1.Rows[e.RowIndex].Cells["product_name"].FormattedValue.ToString();
                    txtbox_model_name.Text = dataGridView1.Rows[e.RowIndex].Cells["model_name"].FormattedValue.ToString();

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
                    txtbox_product_id.Text = dataGridView1.Rows[e.RowIndex].Cells["product_id"].FormattedValue.ToString();
                    txtbox_product_name.Text = dataGridView1.Rows[e.RowIndex].Cells["product_name"].FormattedValue.ToString();
                    txtbox_model_name.Text = dataGridView1.Rows[e.RowIndex].Cells["model_name"].FormattedValue.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtbox_product_name_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();
                SqlCommand sp_fetch_other = new SqlCommand("sp_fetch_other", connect);
                sp_fetch_other.CommandType = CommandType.StoredProcedure;

                SqlParameter product_id = new SqlParameter("@product_id", SqlDbType.VarChar);
                sp_fetch_other.Parameters.Add(product_id).Value = 0;

                SqlParameter product_name = new SqlParameter("@product_name", SqlDbType.VarChar);
                sp_fetch_other.Parameters.Add(product_name).Value = txtbox_product_name.Text.Trim();

                SqlParameter model_name = new SqlParameter("@model_name", SqlDbType.VarChar);
                sp_fetch_other.Parameters.Add(model_name).Value = 0;

                SqlDataReader reader = sp_fetch_other.ExecuteReader();
                {
                    if (reader.Read())
                    {
                        txtbox_product_id.Text = reader.GetInt32(0).ToString();
                        txtbox_model_name.Text = reader.GetString(2);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtbox_quantity_TextChanged(object sender, EventArgs e)
        {
             UpdateTotalPrice();
        }

        private void txtbox_product_id_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();
                SqlCommand sp_fetch_other = new SqlCommand("sp_fetch_other", connect);
                sp_fetch_other.CommandType = CommandType.StoredProcedure;

                SqlParameter product_id = new SqlParameter("@product_id", SqlDbType.VarChar);
                sp_fetch_other.Parameters.Add(product_id).Value = txtbox_product_id.Text.Trim();

                SqlParameter product_name = new SqlParameter("@product_name", SqlDbType.VarChar);
                sp_fetch_other.Parameters.Add(product_name).Value = 0;

                SqlParameter model_name = new SqlParameter("@model_name", SqlDbType.VarChar);
                sp_fetch_other.Parameters.Add(model_name).Value = 0;

                SqlDataReader reader = sp_fetch_other.ExecuteReader();
                {
                    if (reader.Read())
                    {
                        txtbox_product_name.Text = reader.GetString(1);
                        txtbox_model_name.Text = reader.GetString(2);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtbox_model_name_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();
                SqlCommand sp_fetch_other = new SqlCommand("sp_fetch_other", connect);
                sp_fetch_other.CommandType = CommandType.StoredProcedure;

                SqlParameter product_id = new SqlParameter("@product_id", SqlDbType.VarChar);
                sp_fetch_other.Parameters.Add(product_id).Value = 0;

                SqlParameter product_name = new SqlParameter("@product_name", SqlDbType.VarChar);
                sp_fetch_other.Parameters.Add(product_name).Value = 0;

                SqlParameter model_name = new SqlParameter("@model_name", SqlDbType.VarChar);
                sp_fetch_other.Parameters.Add(model_name).Value = txtbox_model_name.Text.Trim();

                SqlDataReader reader = sp_fetch_other.ExecuteReader();
                {
                    if (reader.Read())
                    {
                        txtbox_product_id.Text = reader.GetInt32(0).ToString();
                        txtbox_model_name.Text = reader.GetString(1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Billing_Employee_Load_1(object sender, EventArgs e)
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
                txtbox_product_id.Clear();
                txtbox_product_name.Clear();
                txtbox_model_name.Clear();
                txtbox_quantity.Clear();
                txtbox_cost.Clear();

                SqlCommand sp_fetch_stock = new SqlCommand("sp_fetch_stock", connect);
                sp_fetch_stock.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = sp_fetch_stock.ExecuteReader();


                AutoCompleteStringCollection product_name = new AutoCompleteStringCollection();

                AutoCompleteStringCollection product_id = new AutoCompleteStringCollection();

                AutoCompleteStringCollection model_name = new AutoCompleteStringCollection();

                while (dr.Read())
                {
                    product_id.Add(dr.GetInt32(0).ToString());
                    product_name.Add(dr.GetString(1));
                    model_name.Add(dr.GetString(2));
                }
                txtbox_product_id.AutoCompleteCustomSource = product_id;
                txtbox_product_name.AutoCompleteCustomSource = product_name;
                txtbox_model_name.AutoCompleteCustomSource = model_name;

                connect.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_add_Click_1(object sender, EventArgs e)
        {

            try
            {

                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();
                int requestedQuantity = Convert.ToInt32(txtbox_quantity.Text.Trim());

                SqlCommand sp_fetch_availabilty_quantity = new SqlCommand("sp_fetch_availabilty_quantity", connect);
                sp_fetch_availabilty_quantity.CommandType = CommandType.StoredProcedure;

                SqlParameter product_id_deduct1 = new SqlParameter("@product_id", SqlDbType.Int);
                sp_fetch_availabilty_quantity.Parameters.Add(product_id_deduct1).Value = txtbox_product_id.Text.Trim();

                SqlDataReader reader = sp_fetch_availabilty_quantity.ExecuteReader();

                reader.Read();
                int availableQuantity = reader.GetInt32(0);

                connect.Close();

                if (availableQuantity >= requestedQuantity)
                {
                    string connectionstring1 = ConfigurationManager.ConnectionStrings["pc_point_connection"].ConnectionString;

                    SqlConnection connect1 = new SqlConnection(connectionstring1);
                    connect1.Open();
                    SqlCommand sp_deduct_stocks = new SqlCommand("sp_deduct_stocks", connect1);
                    sp_deduct_stocks.CommandType = CommandType.StoredProcedure;

                    SqlParameter quantity_deduct = new SqlParameter("@quantity", SqlDbType.Int);
                    sp_deduct_stocks.Parameters.Add(quantity_deduct).Value = txtbox_quantity.Text.Trim();

                    SqlParameter product_id_deduct = new SqlParameter("@product_id", SqlDbType.Int);
                    sp_deduct_stocks.Parameters.Add(product_id_deduct).Value = txtbox_product_id.Text.Trim();


                    int i = sp_deduct_stocks.ExecuteNonQuery();
                    dataGridView1.DataSource = null;
                    SqlCommand sp_fetch_stocks = new SqlCommand("sp_fetch_stocks", connect);
                    sp_fetch_stocks.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter sda = new SqlDataAdapter(sp_fetch_stocks);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    dataGridView1.DataSource = dt;

                    if (i > 0)
                    {
                        string product_id = txtbox_product_id.Text;
                        string product_name = txtbox_product_name.Text;
                        string model_name = txtbox_model_name.Text;
                        string quantity = txtbox_quantity.Text;
                        string cost = txtbox_cost.Text;

                        string[] row = { product_id, product_name, model_name, quantity, cost };
                        dataGridView2.Rows.Add(row);

                    }
                    txtbox_product_id.Clear();
                    txtbox_product_name.Clear();
                    txtbox_model_name.Clear();
                    txtbox_quantity.Clear();
                    txtbox_cost.Clear();

                    dataGridView1.Refresh();

                    connect1.Close();
                }
                else
                {
                    MessageBox.Show("Entered Quantity is no Available");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_update_Click_1(object sender, EventArgs e)
        {
            try
            {
                string productId = txtbox_product_id.Text;
                string productName = txtbox_product_name.Text;

                string quantity = txtbox_quantity.Text;
                string cost = txtbox_cost.Text;

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells["dataGridViewproduct_id"].Value.ToString() == productId)
                    {
                        row.Cells["dataGridViewproduct_name"].Value = productName;
                        row.Cells["dataGridViewquantity"].Value = quantity;
                        row.Cells["dataGridViewcost"].Value = cost;

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_delete_Click_1(object sender, EventArgs e)
        {
            try
            {
                string ColumnName = "dataGridViewproduct_id";

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    DataGridViewCell cell = row.Cells[ColumnName];

                    if (cell.Value != null && !cell.Value.Equals(DBNull.Value))
                    {
                        if (cell.Value.ToString() == txtbox_product_id.Text)
                        {
                            dataGridView2.Rows.Remove(row);
                            dataGridView2.Refresh();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_checkout_Click_1(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();


                SqlCommand sp_fetch_user_single = new SqlCommand("sp_fetch_user_single", connect);
                sp_fetch_user_single.CommandType = CommandType.StoredProcedure;

                SqlParameter user_id1 = new SqlParameter("@user_id", SqlDbType.Int);
                sp_fetch_user_single.Parameters.Add(user_id1).Value = lbl_user_id.Text;


                SqlDataAdapter sda = new SqlDataAdapter(sp_fetch_user_single);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                string first_name = dt.Rows[0][0].ToString();
                string last_name = dt.Rows[0][1].ToString();

                SqlCommand sp_insert_sales = new SqlCommand("sp_insert_sales", connect);
                sp_insert_sales.CommandType = CommandType.StoredProcedure;


                SqlParameter invoice_number = new SqlParameter("@invoice_number", SqlDbType.Int);

                SqlParameter product_id = new SqlParameter("@product_id", SqlDbType.Int);

                SqlParameter product_name = new SqlParameter("@product_name", SqlDbType.VarChar);

                SqlParameter model_name = new SqlParameter("@model_name", SqlDbType.VarChar);

                SqlParameter quantity = new SqlParameter("@quantity", SqlDbType.Int);

                SqlParameter cost = new SqlParameter("@cost", SqlDbType.Float);

                SqlParameter sales_person = new SqlParameter("@sales_person", SqlDbType.VarChar);

                SqlParameter sales_date = new SqlParameter("@sales_date", SqlDbType.Date);

                sp_insert_sales.Parameters.Add(invoice_number);
                sp_insert_sales.Parameters.Add(product_id);
                sp_insert_sales.Parameters.Add(product_name);
                sp_insert_sales.Parameters.Add(model_name);
                sp_insert_sales.Parameters.Add(quantity);
                sp_insert_sales.Parameters.Add(cost);
                sp_insert_sales.Parameters.Add(sales_person);
                sp_insert_sales.Parameters.Add(sales_date);

                DateTime date = DateTime.Now;
                string Customdate = date.ToString("yyyy-MM-dd");
                //string date1 = $"{date.Year}-{date.Month}-{date.Day}/{date.Hour}:{date.Minute}";
                int invoice_no1 = date.Year + date.Minute + date.Millisecond + date.Second + date.Hour;
                lbl_invoice_no.Text = invoice_no1.ToString();

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    invoice_number.Value = invoice_no1;
                    product_id.Value = Convert.ToInt32(dataGridView2.Rows[i].Cells["dataGridViewproduct_id"].Value);
                    product_name.Value = dataGridView2.Rows[i].Cells["dataGridViewproduct_name"].Value;
                    model_name.Value = dataGridView2.Rows[i].Cells["dataGridViewmodel_name"].Value;
                    quantity.Value = Convert.ToInt32(dataGridView2.Rows[i].Cells["dataGridViewquantity"].Value);
                    cost.Value = Convert.ToDecimal(dataGridView2.Rows[i].Cells["dataGridViewcost"].Value);
                    sales_person.Value = ($"{first_name}_{last_name}");
                    sales_date.Value = Customdate;

                    sp_insert_sales.ExecuteNonQuery();
                    connect.Close();
                }


                DataTable data = new DataTable();

                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    data.Columns.Add(dataGridView2.Columns[i].HeaderText);
                }


                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    DataRow row = data.NewRow();
                    for (int j = 0; j < dataGridView2.Columns.Count; j++)
                    {
                        row[j] = dataGridView2.Rows[i].Cells[j].FormattedValue.ToString();
                    }
                    data.Rows.Add(row);

                }

                decimal totalCost = 0;
                foreach (DataRow row in data.Rows)
                {

                    if (row["Cost"] != DBNull.Value)
                    {
                        totalCost += Convert.ToDecimal(row["Cost"]);
                    }
                }

                data.Rows.Add("", "", "Total Cost = ", "", totalCost);


                int invoice_no = Convert.ToInt32(lbl_invoice_no.Text);
                string user_id = lbl_user_id.Text;
                Image img = profile_pic.BackgroundImage;
                Payment_employee p = new Payment_employee(data, invoice_no, user_id, img);
                this.Hide();
                p.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
