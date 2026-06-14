using System;
using System.Windows.Forms;
using HeiResturant.DAL;
using HeiResturant.Models;

namespace HeiResturant.Forms
{
    public partial class StudentForm : Form
    {
        private readonly UserInfo _user;

        public StudentForm(UserInfo user)
        {
            _user = user;
            InitializeComponent();
            lblWelcome.Text = $"欢迎，{user.DisplayName}";
            LoadCardInfo();
        }

        private void LoadCardInfo()
        {
            var dt = CardDAL.GetCardByStudentId(_user.StudentId);
            if (dt.Rows.Count == 0)
            {
                lblBalance.Text = "校园卡信息异常，请联系管理员";
                dgvTransactions.DataSource = null;
                return;
            }

            var row = dt.Rows[0];
            _user.CardId = Convert.ToInt32(row["CardId"]);
            lblBalance.Text = $"学号/卡号：{row["StudentNo"]}    余额：{Convert.ToDecimal(row["Balance"]):F2} 元";
            lblStudentInfo.Text = $"姓名：{row["StudentName"]}    班级：{row["ClassName"]}    状态：{row["Status"]}";

            dgvTransactions.DataSource = CardDAL.GetTransactions(_user.CardId);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCardInfo();
        }
    }
}
