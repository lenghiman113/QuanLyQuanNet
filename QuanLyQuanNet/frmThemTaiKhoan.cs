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
    public partial class frmThemTaiKhoan : Form
    {
        public frmThemTaiKhoan()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            insert_Account();

            // Get the ID of customer 
            string query_select_account = "SELECT * FROM customer";
            DataTable dt_select_account = DataProvider.Instance.ExcuteQuery(query_select_account);
            CustomerAccount customerAccount = null;

            foreach(DataRow row in dt_select_account.Rows)
            {
                customerAccount = new CustomerAccount(row);
            }
            string query_insertHour = "EXEC PROC_insertHour_ForCustomerAccount @id_customer";
            DataTable dt_insert_hour = DataProvider.Instance.ExcuteQuery(query_insertHour, new object[] { customerAccount.Id });

            // using ID above, insert hour



        }

        private void insert_Account()
        {
            string username = txtTaiKhoan.Text;
            string query = "EXEC PROC_insertAccount @username ";
            DataTable dt_insert_account = DataProvider.Instance.ExcuteQuery(query, new object[] { username });
            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
