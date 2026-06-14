using System;
using System.Windows.Forms;
using HeiResturant.DAL;
using HeiResturant.Models;

namespace HeiResturant.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            cboRole.SelectedIndex = 0;
            UpdateInputHint();
        }

        private void cboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInputHint();
        }

        private void UpdateInputHint()
        {
            bool isStudent = cboRole.SelectedIndex == 0;
            lblInput.Text = isStudent ? "学号：" : "用户名：";
            if (isStudent)
                txtInput.Text = "2024001001";
            else
                txtInput.Text = cboRole.SelectedIndex == 1 ? "cashier01" :
                                cboRole.SelectedIndex == 2 ? "admin" : "restaurant01";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var input = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("请输入" + (cboRole.SelectedIndex == 0 ? "学号" : "用户名"),
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                UserInfo user = null;
                switch (cboRole.SelectedIndex)
                {
                    case 0:
                        user = StudentDAL.LoginByCardOrStudentNo(input);
                        if (user == null)
                            MessageBox.Show("未找到该学号，或校园卡已冻结/挂失", "登录失败",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case 1:
                        user = UserDAL.LoginStaff(input, "Cashier");
                        if (user == null)
                            MessageBox.Show("未找到该收银员账号", "登录失败",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case 2:
                        user = UserDAL.LoginStaff(input, "Admin");
                        if (user == null)
                            MessageBox.Show("未找到该管理员账号", "登录失败",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case 3:
                        user = UserDAL.LoginStaff(input, "Restaurant");
                        if (user == null)
                            MessageBox.Show("未找到该餐厅管理账号", "登录失败",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }

                if (user == null) return;

                Hide();
                Form nextForm = CreateForm(user);
                if (nextForm == null)
                {
                    Show();
                    return;
                }

                nextForm.FormClosed += (s, args) => Show();
                nextForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接数据库失败，请先执行 Database\\InitDatabase.sql。\n\n" + ex.Message,
                    "数据库错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static Form CreateForm(UserInfo user)
        {
            switch (user.RoleType)
            {
                case "Admin": return new AdminForm(user);
                case "Cashier": return new CashierForm(user);
                case "Student": return new StudentForm(user);
                case "Restaurant": return new RestaurantForm(user);
                default:
                    MessageBox.Show("未知角色", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
