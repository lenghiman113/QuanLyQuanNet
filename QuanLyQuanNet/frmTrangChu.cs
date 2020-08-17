using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyQuanNet
{
    public partial class frmTrangChu : Form
    {
     
        // Luu y 1 : Khi trinh bay csdl
        public frmTrangChu()
        {
            InitializeComponent();

            Load_Customer_Data();

            Load_Customer_Hour();

            

            Load_Revenue();
        }

		private void Load_Revenue()
		{
            string query = "SELECT * FROM revenue";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query);
            if(dt != null)
            {
                lbRevenue.Text = dt.Rows[0]["revenueNumber"].ToString();
            }
                
            
		}

		private void Load_Customer_Hour()
		{
            // Kích hoạt timer
            timer1.Enabled = true;
        }

		private void Load_Customer_Data()
		{
            DataTable dt_customer_info = new DataTable();
            // Tạo câu truy vấn
            string query = "SELECT * FROM customer , playingHour WHERE customer.id = playingHour.id_customer ";

            // Đổ dữ liệu vào data table 
            dt_customer_info = DataProvider.Instance.ExcuteQuery(query);

            // Đổ dữ liệu vào datagrid view để hiện thị lên cho người dùng
            dtgvCustomerInfo.DataSource = dt_customer_info;

            
		}

        private void Load_Customer_Data2()
        {
            DataTable dt_customer_info = new DataTable();
            // Tạo câu truy vấn
            string query = "SELECT * FROM customer ";

            // Đổ dữ liệu vào data table 
            dt_customer_info = DataProvider.Instance.ExcuteQuery(query);

            // Đổ dữ liệu vào datagrid view để hiện thị lên cho người dùng
            dtgvCustomerInfo.DataSource = dt_customer_info;


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        // Hiển thị thông tin customer bằng cách click vào datagrid view
		private void dtgvCustomerInfo_CellClick(object sender, DataGridViewCellEventArgs e)
		{
            if (dtgvCustomerInfo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dtgvCustomerInfo.CurrentRow.Selected = true;
                lbID.Text = dtgvCustomerInfo.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
                txtUsername.Text = dtgvCustomerInfo.Rows[e.RowIndex].Cells["username"].FormattedValue.ToString();
                lbIDHour.Text = dtgvCustomerInfo.Rows[e.RowIndex].Cells["id1"].FormattedValue.ToString();
                
            }

            
        }

        // Với mỗi giây, giảm giờ chơi của khách hàng 
        // Sử dụng phương thức DATEADD trong sql server
		private void timer1_Tick(object sender, EventArgs e)
		{
            string query = "UPDATE playingHour SET hour_db = DATEADD(SECOND, -1, hour_db) ";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query);
            Load_Customer_Data();


		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
            // Thêm giờ 
            Add_CustomerHour();
            MessageBox.Show("Thêm giờ thành công ! ");
            // Tính doanh thu
		}


		private void Add_CustomerHour()
		{
            // Không được là số thực, chỉ có thể là số nguyên
            if (txtAddHour.Text.Contains(',') || txtAddHour.Text.Contains('.'))
			{
                MessageBox.Show("Input must be interger ");
			}
            else
			{
                // Lấy giờ mà người dùng (nhân viên) nhập vào từ textbox
                int hour = int.Parse(txtAddHour.Text);
                // Lấy id của customer
                int id_hour = int.Parse(lbIDHour.Text);

                string query = "EXEC UpdateHourFromCustomer @input_hour , @id_hour";
                DataTable dt = DataProvider.Instance.ExcuteQuery(query, new object[] { hour , id_hour });

                Load_Customer_Data();

                Calculate_Revenue(hour);
                Load_Revenue();
            }
        }

        private void Calculate_Revenue(int hour)
        {
            // Số tiền = Số giờ * 5000 (1 tiếng/5000vnđ)
            // Ví dụ : 1 tiếng --> 5000 vnđ , 2 tiếng --> 10000 vnđ
            int money = hour * 5000;
            string query = "EXEC UpdateRevenue @revenue";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query, new object[] { money });
                
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAddHour.Clear();
            txtUsername.Clear();
            lbID.Text = "";
            lbIDHour.Text = "";
            lbRevenue.Text = "";
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            frmThemTaiKhoan frm = new frmThemTaiKhoan();
            frm.ShowDialog();
            Load_Customer_Data();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
