using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout;

namespace PcPoint
{
    public partial class Payment_admin : Form
    {
        public Payment_admin(DataTable data, int invoice_no, string user_id, Image img)
        {
            InitializeComponent();
            DataTable dataTable = data;
            dataGridView1.DataSource = dataTable;
            lbl_invoice_no.Text = invoice_no.ToString();
            lbl_user_id.Text = user_id;
            profile_pic.BackgroundImage = img;
        }

        private void btn_paid_card_Click(object sender, EventArgs e)
        {
            if (txtbox_card_number.Text.Trim() != "" && txtbox_cvv.Text.Trim() != "" && txtbox_date.Text.Trim() != "")
            {
                groupbox_receipt.Visible = true;
                groupBox_card.Visible = false;
                groupBox_gpay.Visible = false;
                btn_paid_cash.Visible = false;
            }

        }

        private void btn_paid_gpay_Click(object sender, EventArgs e)
        {
            groupbox_receipt.Visible = true;
            groupBox_card.Visible = false;
            groupBox_gpay.Visible = false;
            btn_paid_cash.Visible = false;
        }

        private void btn_paid_cash_Click(object sender, EventArgs e)
        {
            groupbox_receipt.Visible = true;
            groupBox_card.Visible = false;
            groupBox_gpay.Visible = false;
            btn_paid_cash.Visible = false;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int marginLeft = 50;
            int marginLeftrow = 60;
            int marginTop = 20;
            int rowHeight = 50;
            Bitmap bitmap = Properties.Resources.Logo;
            Image image = new Bitmap(bitmap);

            int marginX = 300;
            int marginY = 0;
            int newWidth = 200;
            int newHeight = (int)((double)image.Height / image.Width * newWidth);

            Rectangle destinationRect = new Rectangle(marginX, marginY, newWidth, newHeight);
            e.Graphics.DrawImage(image, destinationRect);

            DateTime dateTime = DateTime.Now;

            string textToPrint = "Invoice";
            string textToPrint1 = "Address:";
            string textToPrint2 = "Pc Point";
            string textToPrint3 = "No:29/21, OMR Service Rd,";
            string textToPrint4 = "Chennai, Tamil Nadu 600097";
            string textToPrint5 = "Invoice No:" + lbl_invoice_no.Text;
            string textToPrint6 = $"Date:{dateTime.Day}/{dateTime.Month}/{dateTime.Year}";
            Font Font_for_invoice = new Font("Tahoma", 25);
            Font Font_for_other = new Font("Arial", 15);
            Font printFont1 = new Font("Arial-bold", 16);
            Font printFont2 = new Font("Arial", 14);
            e.Graphics.DrawString(textToPrint, Font_for_invoice, Brushes.Blue, 330, 100);
            e.Graphics.DrawString(textToPrint1, Font_for_other, Brushes.Black, 15, 200);
            e.Graphics.DrawString(textToPrint2, Font_for_other, Brushes.Black, 15, 230);
            e.Graphics.DrawString(textToPrint3, Font_for_other, Brushes.Black, 15, 258);
            e.Graphics.DrawString(textToPrint4, Font_for_other, Brushes.Black, 15, 282);
            e.Graphics.DrawString(textToPrint5, Font_for_other, Brushes.Black, 630, 238);
            e.Graphics.DrawString(textToPrint6, Font_for_other, Brushes.Black, 630, 268);


            int[] columnWidths = new int[dataGridView1.Columns.Count];

            columnWidths[0] = 50;
            columnWidths[1] = 160;
            columnWidths[2] = 165;
            columnWidths[3] = 165;
            columnWidths[4] = 160;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                e.Graphics.DrawString(dataGridView1.Columns[i].HeaderText,
                    printFont1, Brushes.Red,
                    marginLeft + i * columnWidths[i], marginTop = 350);

            }


            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    e.Graphics.DrawString(dataGridView1.Rows[i].Cells[j].FormattedValue.ToString(),
                        printFont2, Brushes.Navy,
                        marginLeftrow + j * columnWidths[j], marginTop + (i + 1) * rowHeight);
                }
            }


        }

        private void txtbox_date_Enter(object sender, EventArgs e)
        {
            if (txtbox_date.Text == "MM/YY")
            {
                txtbox_date.Text = "";
                txtbox_date.ForeColor = Color.Black;
            }

        }

        private void txtbox_date_Leave(object sender, EventArgs e)
        {
            if (txtbox_date.Text == "")
            {
                txtbox_date.Text = "MM/YY";
                txtbox_date.ForeColor = Color.DarkGray;
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
            User_Details_admin user_Details_Admin = new User_Details_admin(user_id, img);
            user_Details_Admin.Show();
            this.Hide();
        }

        private void btn_dashboard_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            User_Details_admin user_Details_Admin = new User_Details_admin(user_id, img);
            user_Details_Admin.Show();
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
            Billing_admin billing_Admin = new Billing_admin(user_id, img);
            billing_Admin.Show();
            this.Hide();
        }

        private void btn_billing_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Billing_admin billing_Admin = new Billing_admin(user_id, img);
            billing_Admin.Show();
            this.Hide();
        }

       

        private Bitmap PrintDocumentToBitmap()
        {
            Bitmap bitmap = new Bitmap(827, 750);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                int marginLeft =50;
                int marginLeftrow = 60;
                int marginTop = 0;
                int rowHeight = 50;

                Random random = new Random();

                int minValue = 145988;
                int maxValue = 155900;
                int randomInRange = random.Next(minValue, maxValue);
                Bitmap logoBitmap = Properties.Resources.Logo;
                Image logoImage = new Bitmap(logoBitmap);

                int marginX = 320;
                int marginY = 5;
                int newWidth = 200;
                int newHeight = (int)((double)logoImage.Height / logoImage.Width * newWidth);

                Rectangle destinationRect = new Rectangle(marginX, marginY, newWidth, newHeight);
                graphics.DrawImage(logoImage, destinationRect);

                DateTime dateTime = DateTime.Now;

                string textToPrint = "Invoice";
                string textToPrint1 = "Address:";
                string textToPrint2 = "Pc Point";
                string textToPrint3 = "No:29/21, OMR Service Rd,";
                string textToPrint4 = "Chennai, Tamil Nadu 600097";
                string textToPrint5 = "Invoice No:" + lbl_invoice_no.Text;
                string textToPrint6 = $"Date:{dateTime.Day}/{dateTime.Month}/{dateTime.Year}";
                Font Font_for_invoice = new Font("Tahoma", 25);
                Font Font_for_other = new Font("Arial", 15);
                Font printFont1 = new Font("Arial-bold", 16);
                Font printFont2 = new Font("Arial", 14);
                graphics.DrawString(textToPrint, Font_for_invoice, Brushes.Blue, 360, 100);
                graphics.DrawString(textToPrint1, Font_for_other, Brushes.Black, 20, 200);
                graphics.DrawString(textToPrint2, Font_for_other, Brushes.Black, 20, 230);
                graphics.DrawString(textToPrint3, Font_for_other, Brushes.Black, 20, 258);
                graphics.DrawString(textToPrint4, Font_for_other, Brushes.Black, 20, 282);
                graphics.DrawString(textToPrint5, Font_for_other, Brushes.Black, 630, 238);
                graphics.DrawString(textToPrint6, Font_for_other, Brushes.Black, 630, 268);


                int[] columnWidths = new int[dataGridView1.Columns.Count];

                columnWidths[0] = 50;
                columnWidths[1] = 160;
                columnWidths[2] = 165;
                columnWidths[3] = 165;
                columnWidths[4] = 160;
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    graphics.DrawString(dataGridView1.Columns[i].HeaderText,
                        printFont1, Brushes.Red,
                        marginLeft + i * columnWidths[i], marginTop = 350);

                }


                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        graphics.DrawString(dataGridView1.Rows[i].Cells[j].FormattedValue.ToString(),
                            printFont2, Brushes.Navy,
                            marginLeftrow + j * columnWidths[j], marginTop + (i + 1) * rowHeight);
                    }
                }
            }

            return bitmap;
        }

        private void SaveBitmapAsPdf(Bitmap bitmap)
        {
            // Create a new PDF document
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF File (*.pdf)|*.pdf";
                saveFileDialog.Title = "Save Invoice";
                saveFileDialog.FileName = lbl_invoice_no.Text.Trim() + ".pdf";
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

                        MessageBox.Show("Invoice saved As PDF successfully!");
                        System.Diagnostics.Process.Start(saveFileDialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



        private void SaveBitmapToFile(Bitmap bitmap, string filePath)
        {
            bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument printDocument = new PrintDocument();

            PaperSize paperSize = new PaperSize("A4", 827, 869);
            printDocument.DefaultPageSettings.PaperSize = paperSize;

            printDocument.PrintPage += printDocument1_PrintPage;

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.PrinterSettings = printDialog.PrinterSettings;
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                printPreviewDialog.Document = printDocument;
                printPreviewDialog.Icon = (Properties.Resources.Logo_icon_);
                printPreviewDialog.StartPosition = FormStartPosition.CenterScreen;
                printPreviewDialog.Height = 604;
                printPreviewDialog.Width = 1094;
                printPreviewDialog.ShowDialog();
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
                //Bitmap printedBitmap = PrintDocumentToBitmap();
                //SaveBitmapToFile(printedBitmap, "D:\\Documents\\New folder\\" + lbl_invoice_no.Text.Trim() + ".png");
                Bitmap printedBitmap = PrintDocumentToBitmap();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Files (*.png)|*.png";
            saveFileDialog.Title = "Save Invoice";
            saveFileDialog.FileName = lbl_invoice_no.Text.Trim() + ".png";
            saveFileDialog.AddExtension = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveBitmapToFile(printedBitmap, saveFileDialog.FileName);
                MessageBox.Show("Invoice saved As Image successfully!");
                System.Diagnostics.Process.Start(saveFileDialog.FileName);
            }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        private void btn_pdf_Click(object sender, EventArgs e)
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

        private void btn_employee_details_Click(object sender, EventArgs e)
        {
            string user_id = lbl_user_id.Text;
            Image img = profile_pic.BackgroundImage;
            Employee_details_admin employee_details_admin = new Employee_details_admin(user_id, img);
            employee_details_admin.Show();
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

        private void btn_sales_s_Click(object sender, EventArgs e)
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

        private void radioButton_Debitcard_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_card.Visible = true;
            groupBox_gpay.Visible = false;
            btn_paid_cash.Visible = false;
        }

        private void radioButton_gpay_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_gpay.Visible = true;
            groupBox_card.Visible = false;
            btn_paid_cash.Visible = false;
        }

        private void radioButton_creditcard_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_card.Visible = true;
            groupBox_gpay.Visible = false;
            btn_paid_cash.Visible = false;
        }

        private void radioButton_cash_CheckedChanged(object sender, EventArgs e)
        {
            btn_paid_cash.Visible = true;
            groupBox_gpay.Visible = false;
            groupBox_card.Visible = false;

        }

        private void lbl_close_gpay_Click(object sender, EventArgs e)
        {
            groupBox_gpay.Visible = false;
            groupBox_card.Visible = false;
            btn_paid_cash.Visible = false;
        }

        private void lbl_close_card_Click(object sender, EventArgs e)
        {
            groupBox_card.Visible = false;
            groupBox_gpay.Visible = false;
            btn_paid_cash.Visible = false;
        }

        private void btn_receipt_close_Click_1(object sender, EventArgs e)
        {
            groupbox_receipt.Visible = false;
            groupBox_card.Visible = false;
            groupBox_gpay.Visible = false;
            btn_paid_cash.Visible = false;
        }
    }
}
