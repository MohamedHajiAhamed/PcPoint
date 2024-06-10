using iText.Kernel.Pdf;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using iText.Layout;

namespace PcPoint
{
    public partial class sales_admin : Form
    {
        public sales_admin(string user_id, Image img)
        {
            InitializeComponent();
            lbl_user_id.Text = user_id;
            profile_pic.BackgroundImage = img;
            sales_no();
        }
        public string connectionstring = Connection.GetConnectionString();

        private void txtbox_product_name_TextChanged(object sender, EventArgs e)
        {
            try
            {

                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();
                SqlCommand sp_fetch_product_name_sales = new SqlCommand("sp_fetch_product_name_sales", connect);
                sp_fetch_product_name_sales.CommandType = CommandType.StoredProcedure;

                SqlParameter product_name = new SqlParameter("@product_name", SqlDbType.VarChar);

                sp_fetch_product_name_sales.Parameters.Add(product_name).Value = txtbox_product_name.Text.Trim();

                SqlDataAdapter sda = new SqlDataAdapter(sp_fetch_product_name_sales);
                DataTable dt = new DataTable();
                dt.Clear();
                sda.Fill(dt);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
                connect.Close();
                lbl_txt.Visible = true;
                lbl_txt_value.Visible = true;
                lbl_txt.Text = "Product Name";
                lbl_txt_value.Text = txtbox_product_name.Text.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtbox_Invoice_number_TextChanged(object sender, EventArgs e)
        {
            try
            {

                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();
                SqlCommand sp_fetch_invoice_no = new SqlCommand("sp_fetch_invoice_no", connect);
                sp_fetch_invoice_no.CommandType = CommandType.StoredProcedure;

                SqlParameter invoice_number = new SqlParameter("@invoice_number", SqlDbType.VarChar);

                sp_fetch_invoice_no.Parameters.Add(invoice_number).Value = txtbox_Invoice_number.Text.Trim();

                SqlDataAdapter sda = new SqlDataAdapter(sp_fetch_invoice_no);
                DataTable dt = new DataTable();
                dt.Clear();
                sda.Fill(dt);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
                connect.Close();
                lbl_txt.Visible = true;
                lbl_txt_value.Visible = true;
                lbl_txt.Text = "Invoice Number";
                lbl_txt_value.Text = txtbox_Invoice_number.Text.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtbox_sales_person_TextChanged(object sender, EventArgs e)
        {
            try
            {

                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();
                SqlCommand sp_fetch_sales_person = new SqlCommand("sp_fetch_sales_person", connect);
                sp_fetch_sales_person.CommandType = CommandType.StoredProcedure;

                SqlParameter sales_person = new SqlParameter("@sales_person", SqlDbType.VarChar);

                sp_fetch_sales_person.Parameters.Add(sales_person).Value = txtbox_sales_person.Text.Trim();

                SqlDataAdapter sda = new SqlDataAdapter(sp_fetch_sales_person);
                DataTable dt = new DataTable();
                dt.Clear();
                sda.Fill(dt);


                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
                connect.Close();
                lbl_txt.Visible = true;
                lbl_txt_value.Visible = true;
                lbl_txt.Text = "Sales Person";
                lbl_txt_value.Text = txtbox_sales_person.Text.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtbox_Sales_date_ValueChanged(object sender, EventArgs e)
        {
            try
            {

                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();
                SqlCommand sp_fetch_date = new SqlCommand("sp_fetch_date", connect);
                sp_fetch_date.CommandType = CommandType.StoredProcedure;

                SqlParameter sales_date = new SqlParameter("@sales_date", SqlDbType.VarChar);

                sp_fetch_date.Parameters.Add(sales_date).Value = txtbox_Sales_date.Text;

                SqlDataAdapter sda = new SqlDataAdapter(sp_fetch_date);
                DataTable dt = new DataTable();
                dt.Clear();
                sda.Fill(dt);


                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
                connect.Close();

                lbl_txt.Visible = true;
                lbl_txt_value.Visible = true;
                lbl_txt.Text = "Date";
                lbl_txt_value.Text = txtbox_Sales_date.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void radioButton_SearchByProductName_CheckedChanged(object sender, EventArgs e)
        {
            txtbox_product_name.Visible = true;
            checkBox_with_Date.Visible = true;
            txtbox_Invoice_number.Visible = false;
            txtbox_Sales_date.Visible = false;
            txtbox_sales_person.Visible = false;
            lbl_product_name.Text = "Product Name";
            lbl_date_h.Visible = false;
            lbl_date.Visible = false;
            lbl_total_sales.Text = "0";
            lbl_sales_date_addon.Text = "Date";
            lbl_txt.Visible = false;
            lbl_txt_value.Visible = false;
        }

        private void radioButton_SearchBysalesPerson_CheckedChanged(object sender, EventArgs e)
        {
            txtbox_sales_person.Visible = true;
            checkBox_with_Date.Visible = true;
            txtbox_product_name.Visible = false;
            txtbox_Invoice_number.Visible = false;
            txtbox_Sales_date.Visible = false;
            lbl_product_name.Text = "Sales Person";
            lbl_date_h.Visible = false;
            lbl_date.Visible = false;
            lbl_total_sales.Text = "0";
            lbl_sales_date_addon.Text = "Date";
            lbl_txt.Visible = false;
            lbl_txt_value.Visible = false;
        }

        private void radioButton_SearchBySalesDate_CheckedChanged(object sender, EventArgs e)
        {
            txtbox_Sales_date.Visible = true;
            checkBox_with_Date.Visible = true;
            txtbox_sales_person.Visible = false;
            txtbox_product_name.Visible = false;
            txtbox_Invoice_number.Visible = false;

            if (radioButton_SearchBySalesDate.Checked && checkBox_with_Date.Checked)
            {
                lbl_product_name.Text = "From Date";

            }
            else
            {
                lbl_product_name.Text = "Sales Date";
            }
            lbl_date_h.Visible = false;
            lbl_date.Visible = false;
            lbl_total_sales.Text = "0";
            lbl_sales_date_addon.Text = "To Date";
            lbl_txt.Visible = false;
            lbl_txt_value.Visible = false;
        }
        private void radioButton_SearchByInvoiceNumber_CheckedChanged(object sender, EventArgs e)
        {
            txtbox_Invoice_number.Visible = true;
            checkBox_with_Date.Visible = true;
            txtbox_Sales_date.Visible = false;
            txtbox_sales_person.Visible = false;
            txtbox_product_name.Visible = false;
            lbl_product_name.Text = "Invoice Number";
            lbl_date_h.Visible = false;
            lbl_date.Visible = false;
            lbl_total_sales.Text = "0";
            lbl_sales_date_addon.Text = "Date";
            lbl_txt.Visible = false;
            lbl_txt_value.Visible = false;
        }
        public void sales_no()
        {
            Random random = new Random();
            int a = random.Next(14598, 15590);
            lbl_sales_no.Text = a.ToString();
        }
        
        

        private void btn_CalculateTotalSales_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dataTable = (DataTable)dataGridView1.DataSource;

                if (dataTable.Rows.Count > 0)
                {
                    double totalAmount = 0;
                    int costColumnIndex = dataTable.Columns["cost"].Ordinal;

                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row[costColumnIndex] != DBNull.Value && double.TryParse(row[costColumnIndex].ToString(), out double cost))
                        {
                            totalAmount += cost;
                        }
                    }

                    DataRow totalRow = dataTable.NewRow();
                    DataRow total = dataTable.NewRow();
                    total[costColumnIndex] = totalAmount;
                    total[3] = "Total Sales = ";
                    dataTable.Rows.Add(totalRow);
                    dataTable.Rows.Add(total);

                    dataGridView1.Refresh();
                    lbl_total_sales.Text = totalAmount.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtbox_date_addon_ValueChanged(object sender, EventArgs e)
        {
            try
            {

                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();
                if (txtbox_sales_person.Visible == true)
                {
                    if (txtbox_sales_person.Text.Trim() != "")
                    {
                        SqlCommand sp_fetch_sales_person_with_date = new SqlCommand("sp_fetch_sales_person_with_date", connect);
                        sp_fetch_sales_person_with_date.CommandType = CommandType.StoredProcedure;

                        SqlParameter sales_person = new SqlParameter("@sales_person", SqlDbType.VarChar);
                        sp_fetch_sales_person_with_date.Parameters.Add(sales_person).Value = txtbox_sales_person.Text.Trim();

                        SqlParameter date = new SqlParameter("@date", SqlDbType.VarChar);
                        sp_fetch_sales_person_with_date.Parameters.Add(date).Value = txtbox_date_addon.Text.Trim();

                        SqlDataAdapter sda = new SqlDataAdapter(sp_fetch_sales_person_with_date);
                        DataTable dt = new DataTable();
                        dt.Clear();
                        sda.Fill(dt);


                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = dt;

                        lbl_date_h.Visible = true;
                        lbl_date.Visible = true;
                        lbl_date_h.Text = "Date";
                        lbl_date.Text = txtbox_date_addon.Text;
                    }

                }
                if (txtbox_product_name.Visible == true)
                {
                    if (txtbox_product_name.Text.Trim() != "")
                    {
                        SqlCommand sp_fetch_product_name_with_date = new SqlCommand("sp_fetch_product_name_with_date", connect);
                        sp_fetch_product_name_with_date.CommandType = CommandType.StoredProcedure;

                        SqlParameter product_name = new SqlParameter("@product_name", SqlDbType.VarChar);
                        sp_fetch_product_name_with_date.Parameters.Add(product_name).Value = txtbox_product_name.Text.Trim();
                        SqlParameter date = new SqlParameter("@date", SqlDbType.VarChar);
                        sp_fetch_product_name_with_date.Parameters.Add(date).Value = txtbox_date_addon.Text.Trim();

                        SqlDataAdapter sda = new SqlDataAdapter(sp_fetch_product_name_with_date);
                        DataTable dt = new DataTable();
                        dt.Clear();
                        sda.Fill(dt);

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = dt;
                        lbl_date_h.Visible = true;
                        lbl_date.Visible = true;
                        lbl_date_h.Text = "Date";
                        lbl_date.Text = txtbox_date_addon.Text;
                    }
                }

                if (txtbox_Invoice_number.Visible == true)
                {
                    if (txtbox_Invoice_number.Text.Trim() != "")
                    {
                        SqlCommand sp_fetch_invoice_no_with_date = new SqlCommand("sp_fetch_invoice_no_with_date", connect);
                        sp_fetch_invoice_no_with_date.CommandType = CommandType.StoredProcedure;

                        SqlParameter invoice_number = new SqlParameter("@invoice_number", SqlDbType.VarChar);
                        sp_fetch_invoice_no_with_date.Parameters.Add(invoice_number).Value = txtbox_Invoice_number.Text.Trim();

                        SqlParameter date = new SqlParameter("@date", SqlDbType.VarChar);
                        sp_fetch_invoice_no_with_date.Parameters.Add(date).Value = txtbox_date_addon.Text.Trim();

                        SqlDataAdapter sda = new SqlDataAdapter(sp_fetch_invoice_no_with_date);
                        DataTable dt = new DataTable();
                        dt.Clear();
                        sda.Fill(dt);

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = dt;

                        lbl_date_h.Visible = true;
                        lbl_date.Visible = true;
                        lbl_date_h.Text = "Date";
                        lbl_date.Text = txtbox_date_addon.Text;
                    }
                }

                if (txtbox_Sales_date.Visible == true)
                {
                    if (txtbox_Sales_date.Text != "")
                    {
                        SqlCommand sp_fetch_date_between_date = new SqlCommand("sp_fetch_date_between_date", connect);
                        sp_fetch_date_between_date.CommandType = CommandType.StoredProcedure;

                        SqlParameter sales_date = new SqlParameter("@sales_date", SqlDbType.VarChar);
                        sp_fetch_date_between_date.Parameters.Add(sales_date).Value = txtbox_Sales_date.Text;

                        SqlParameter date = new SqlParameter("@date", SqlDbType.VarChar);
                        sp_fetch_date_between_date.Parameters.Add(date).Value = txtbox_date_addon.Text.Trim();

                        SqlDataAdapter sda = new SqlDataAdapter(sp_fetch_date_between_date);
                        DataTable dt = new DataTable();
                        dt.Clear();
                        sda.Fill(dt);


                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = dt;

                        lbl_product_name.Text = "From Date";
                        lbl_txt.Visible = true;
                        lbl_txt_value.Visible = true;
                        lbl_txt.Text = "From Date";
                        lbl_txt_value.Text = txtbox_Sales_date.Text.Trim();
                        lbl_date_h.Visible = true;
                        lbl_date.Visible = true;
                        lbl_date_h.Text = "To Date";
                        lbl_date.Text = txtbox_date_addon.Text;
                    }
                }

                connect.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox_with_Date_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_with_Date.Checked)
            {
                lbl_sales_date_addon.Visible = true;
                txtbox_date_addon.Visible = true;
                if (radioButton_SearchBySalesDate.Checked && checkBox_with_Date.Checked)
                {
                    lbl_product_name.Text = "From Date";

                }
                else if (radioButton_SearchBySalesDate.Checked && !checkBox_with_Date.Checked)
                {
                    lbl_product_name.Text = "Sales Date";
                }
            }
            else
            {
                lbl_sales_date_addon.Visible = false;
                txtbox_date_addon.Visible = false;
                lbl_date.Visible = false;
                lbl_date_h.Visible = false;

                txtbox_product_name.Clear();
                txtbox_sales_person.Clear();
                txtbox_Invoice_number.Clear();
            }

        }

        private Bitmap PrintDocumentToBitmap()
        {
            Bitmap bitmap = new Bitmap(1450, 1200);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                int marginLeft = 30;
                int marginTop = 20;
                int rowHeight = 50;


                Bitmap logoBitmap = Properties.Resources.Logo;
                Image logoImage = new Bitmap(logoBitmap);

                int marginX = 620;
                int marginY = 5;
                int newWidth = 200;
                int newHeight = (int)((double)logoImage.Height / logoImage.Width * newWidth);

                Rectangle destinationRect = new Rectangle(marginX, marginY, newWidth, newHeight);
                graphics.DrawImage(logoImage, destinationRect);

                DateTime dateTime = DateTime.Now;

                string textToPrint = "Sales Report";
                string textToPrint1 = "Address:";
                string textToPrint2 = "Pc Point";
                string textToPrint3 = "No: 29/21, OMR Service Rd,";
                string textToPrint4 = "Chennai, Tamil Nadu 600097";
                string textToPrint5 = "Sales Report No: " + lbl_sales_no.Text;
                string textToPrint6 = $"Date: {dateTime.Day} / {dateTime.Month} / {dateTime.Year}";
                Font Font_for_invoice = new Font("Tahoma", 25);
                Font Font_for_other = new Font("Arial", 15);
                Font printFont1 = new Font("Arial-bold", 16);
                Font printFont2 = new Font("Arial", 14);
                graphics.DrawString(textToPrint, Font_for_invoice, Brushes.Blue, 620, 100);
                graphics.DrawString(textToPrint1, Font_for_other, Brushes.Black, 10, 200);
                graphics.DrawString(textToPrint2, Font_for_other, Brushes.Black, 10, 230);
                graphics.DrawString(textToPrint3, Font_for_other, Brushes.Black, 10, 258);
                graphics.DrawString(textToPrint4, Font_for_other, Brushes.Black, 10, 282);
                graphics.DrawString(textToPrint5, Font_for_other, Brushes.Black, 1220, 238);
                graphics.DrawString(textToPrint6, Font_for_other, Brushes.Black, 1220, 268);



                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    graphics.DrawString(dataGridView1.Columns[i].HeaderText,
                        printFont1, Brushes.Red,
                        marginLeft + i * 160, marginTop = 350);
                }


                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        graphics.DrawString(dataGridView1.Rows[i].Cells[j].FormattedValue.ToString(),
                            printFont2, Brushes.Navy,
                            marginLeft + j * 160, marginTop + (i + 1) * rowHeight);
                    }

                }
            }

                return bitmap;
        }

        private void btn_printSalesReport_Click(object sender, EventArgs e)
        {
           try
            {
                Bitmap bitmap = PrintDocumentToBitmap();
                SaveBitmapAsPdf(bitmap);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void SaveBitmapAsPdf(Bitmap bitmap)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF File (*.pdf)|*.pdf";
                saveFileDialog.Title = "Save Sales Report";
                saveFileDialog.FileName = "Sales Report " +lbl_sales_no.Text + ".pdf";
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (PdfWriter writer = new PdfWriter(saveFileDialog.FileName))
                        {
                            using (PdfDocument pdf = new PdfDocument(writer))
                            {
                                Document document = new Document(pdf);

                                using (MemoryStream stream = new MemoryStream())
                                {
                                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                                    byte[] byteArray = stream.ToArray();

                                    iText.Layout.Element.Image pdfImage = new iText.Layout.Element.Image(iText.IO.Image.ImageDataFactory.Create(byteArray));
                                    document.Add(pdfImage);

                                    document.Close();
                                }
                            }
                        }

                        MessageBox.Show("Sales Report saved As PDF successfully!");
                        System.Diagnostics.Process.Start(saveFileDialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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

        private void sales_admin_Load_1(object sender, EventArgs e)
        {
            try
            {

                SqlConnection connect = new SqlConnection(connectionstring);
                connect.Open();
                SqlCommand sp_fetch_sales_all = new SqlCommand("sp_fetch_sales_all", connect);


                SqlDataAdapter sd = new SqlDataAdapter(sp_fetch_sales_all);
                DataTable dt1 = new DataTable();
                sd.Fill(dt1);

                connect.Close();

                AutoCompleteStringCollection product_name = new AutoCompleteStringCollection();

                AutoCompleteStringCollection sales_person = new AutoCompleteStringCollection();

                AutoCompleteStringCollection invoice_number = new AutoCompleteStringCollection();

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    product_name.Add(dt1.Rows[i][3].ToString());

                    sales_person.Add(dt1.Rows[i][7].ToString());

                    invoice_number.Add(dt1.Rows[i][1].ToString());
                }

                txtbox_product_name.AutoCompleteCustomSource = product_name;

                txtbox_sales_person.AutoCompleteCustomSource = sales_person;

                txtbox_Invoice_number.AutoCompleteCustomSource = invoice_number;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
