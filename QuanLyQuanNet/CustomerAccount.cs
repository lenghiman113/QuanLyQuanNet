using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuanLyQuanNet
{
	class CustomerAccount
	{
		private string username;
		private string password;

		public string Username { get => username; set => username = value; }
		public string Password { get => password; set => password = value; }
        public int Id { get; set; }

        public CustomerAccount() { }

		public CustomerAccount(DataRow row)
		{
			this.Id = (int)row["id"];
		}
	}
}
