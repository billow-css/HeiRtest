using System;
using System.Windows.Forms;
using HeiResturant.DAL;
using HeiResturant.Helpers;
using HeiResturant.Models;

namespace HeiResturant.Forms
{
    public partial class AdminForm : Form
    {
        private readonly UserInfo _user;
        private bool _loading;

        public AdminForm(UserInfo user)
        {
            _user = user;
            InitializeComponent();
            lblWelcome.Text = $"管理员：{user.Username}（表格可直接编辑，修改后自动保存）";

            dgvUsers.DataBindingComplete += (s, e) =>
            {
                GridHelper.EnableEdit(dgvUsers, "UserId", "CreatedAt");
                _loading = false;
            };
            dgvUsers.CellEndEdit += dgvUsers_CellEndEdit;

            dgvStudents.DataBindingComplete += (s, e) =>
            {
                GridHelper.EnableEdit(dgvStudents, "StudentId", "StudentNo", "CardId", "CardNo");
                _loading = false;
            };
            dgvStudents.CellEndEdit += dgvStudents_CellEndEdit;

            dgvFoods.DataBindingComplete += (s, e) =>
            {
                GridHelper.EnableEdit(dgvFoods, "FoodId", "CategoryName");
                _loading = false;
            };
            dgvFoods.CellEndEdit += dgvFoods_CellEndEdit;

            LoadAllTabs();
        }

        private void LoadAllTabs()
        {
            LoadUsers();
            LoadStudents();
            LoadFoods();
        }

        private void LoadUsers()
        {
            _loading = true;
            dgvUsers.DataSource = UserDAL.GetAllUsers();
        }

        private void LoadStudents()
        {
            _loading = true;
            dgvStudents.DataSource = StudentDAL.GetAllStudents();
        }

        private void LoadFoods()
        {
            _loading = true;
            dgvFoods.DataSource = FoodDAL.GetAllFoods();
        }

        private void dgvUsers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            _loading = false;
        }

        private void dgvUsers_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (_loading || e.RowIndex < 0) return;
            var row = dgvUsers.Rows[e.RowIndex];
            try
            {
                UserDAL.UpdateUser(
                    Convert.ToInt32(row.Cells["UserId"].Value),
                    row.Cells["Username"].Value?.ToString().Trim(),
                    row.Cells["RoleType"].Value?.ToString(),
                    Convert.ToBoolean(row.Cells["IsActive"].Value));
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message);
                LoadUsers();
            }
        }

        private void dgvStudents_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (_loading || e.RowIndex < 0) return;
            var row = dgvStudents.Rows[e.RowIndex];
            try
            {
                int studentId = Convert.ToInt32(row.Cells["StudentId"].Value);
                int cardId = Convert.ToInt32(row.Cells["CardId"].Value);
                string colName = dgvStudents.Columns[e.ColumnIndex].Name;

                if (colName == "Name" || colName == "ClassName" || colName == "Phone")
                {
                    StudentDAL.UpdateStudent(
                        studentId,
                        row.Cells["Name"].Value?.ToString(),
                        row.Cells["ClassName"].Value?.ToString(),
                        row.Cells["Phone"].Value?.ToString());
                }
                else if (colName == "Balance" || colName == "Status")
                {
                    CardDAL.UpdateCard(
                        cardId,
                        Convert.ToDecimal(row.Cells["Balance"].Value),
                        row.Cells["Status"].Value?.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message);
                LoadStudents();
            }
        }

        private void dgvFoods_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (_loading || e.RowIndex < 0) return;
            var row = dgvFoods.Rows[e.RowIndex];
            try
            {
                FoodDAL.UpdateFood(
                    Convert.ToInt32(row.Cells["FoodId"].Value),
                    row.Cells["Name"].Value?.ToString(),
                    Convert.ToDecimal(row.Cells["Price"].Value),
                    Convert.ToInt32(row.Cells["Stock"].Value),
                    Convert.ToBoolean(row.Cells["IsAvailable"].Value));
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message);
                LoadFoods();
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            var username = txtNewUsername.Text.Trim();
            var role = cboNewRole.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("请填写用户名");
                return;
            }

            try
            {
                UserDAL.AddUser(username, role);
                txtNewUsername.Clear();
                LoadUsers();
                MessageBox.Show("添加成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加失败：" + ex.Message);
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;
            int userId = Convert.ToInt32(dgvUsers.CurrentRow.Cells["UserId"].Value);
            if (MessageBox.Show("确定删除该用户？", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    UserDAL.DeleteUser(userId);
                    LoadUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除失败：" + ex.Message);
                }
            }
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            var no = txtStudentNo.Text.Trim();
            var name = txtStudentName.Text.Trim();
            var cls = txtClassName.Text.Trim();
            var phone = txtPhone.Text.Trim();

            if (string.IsNullOrEmpty(no) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("学号和姓名为必填项");
                return;
            }

            if (!decimal.TryParse(txtInitialBalance.Text, out decimal balance))
                balance = 0;

            try
            {
                StudentDAL.AddStudent(no, name, cls, phone, balance);
                txtStudentNo.Clear();
                txtStudentName.Clear();
                txtClassName.Clear();
                txtPhone.Clear();
                txtInitialBalance.Text = "0";
                LoadStudents();
                MessageBox.Show($"学生 {name} 已添加，校园卡号自动设为学号 {no}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加失败：" + ex.Message);
            }
        }

        private void btnRecharge_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow == null) return;
            int cardId = Convert.ToInt32(dgvStudents.CurrentRow.Cells["CardId"].Value);

            using (var input = new InputDialog("充值", "请输入充值金额："))
            {
                if (input.ShowDialog() != DialogResult.OK) return;
                if (!decimal.TryParse(input.InputText, out decimal amount) || amount <= 0)
                {
                    MessageBox.Show("请输入有效金额");
                    return;
                }

                try
                {
                    CardDAL.Recharge(cardId, amount);
                    LoadStudents();
                    MessageBox.Show($"充值 {amount:F2} 元成功");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("充值失败：" + ex.Message);
                }
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            if (cboCategory.SelectedValue == null)
            {
                MessageBox.Show("请选择分类");
                return;
            }

            var name = txtFoodName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("请输入菜品名称");
                return;
            }

            if (!decimal.TryParse(txtFoodPrice.Text, out decimal price) ||
                !int.TryParse(txtFoodStock.Text, out int stock))
            {
                MessageBox.Show("价格和库存格式不正确");
                return;
            }

            try
            {
                FoodDAL.AddFood(Convert.ToInt32(cboCategory.SelectedValue), name, price, stock);
                txtFoodName.Clear();
                txtFoodPrice.Clear();
                txtFoodStock.Clear();
                LoadFoods();
                MessageBox.Show("菜品添加成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加失败：" + ex.Message);
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            if (dgvFoods.CurrentRow == null) return;
            int foodId = Convert.ToInt32(dgvFoods.CurrentRow.Cells["FoodId"].Value);
            if (MessageBox.Show("确定删除该菜品？", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    FoodDAL.DeleteFood(foodId);
                    LoadFoods();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除失败：" + ex.Message);
                }
            }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            cboNewRole.Items.AddRange(new[] { "Admin", "Cashier", "Restaurant" });
            cboNewRole.SelectedIndex = 1;

            cboCategory.DataSource = FoodDAL.GetCategories();
            cboCategory.DisplayMember = "Name";
            cboCategory.ValueMember = "CategoryId";
        }
    }
}
