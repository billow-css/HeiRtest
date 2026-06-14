using System;
using System.Windows.Forms;
using HeiResturant.DAL;
using HeiResturant.Helpers;
using HeiResturant.Models;

namespace HeiResturant.Forms
{
    public partial class RestaurantForm : Form
    {
        private readonly UserInfo _user;
        private bool _loading;

        public RestaurantForm(UserInfo user)
        {
            _user = user;
            InitializeComponent();
            lblWelcome.Text = $"餐厅管理：{user.Username}（菜品表可直接编辑补货）";

            dgvLowStock.DataBindingComplete += (s, e) =>
            {
                GridHelper.EnableEdit(dgvLowStock, "FoodId", "CategoryName");
                _loading = false;
            };
            dgvLowStock.CellEndEdit += dgvLowStock_CellEndEdit;

            LoadData();
        }

        private void LoadData()
        {
            dgvTodayOrders.DataSource = OrderDAL.GetTodayOrders();
            dgvSales.DataSource = OrderDAL.GetSalesSummary();

            _loading = true;
            dgvLowStock.DataSource = FoodDAL.GetAllFoods();
        }

        private void dgvLowStock_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (_loading || e.RowIndex < 0) return;
            var row = dgvLowStock.Rows[e.RowIndex];
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
                LoadData();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvTodayOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int orderId = Convert.ToInt32(dgvTodayOrders.Rows[e.RowIndex].Cells["OrderId"].Value);
            dgvOrderDetails.DataSource = OrderDAL.GetOrderDetails(orderId);
        }
    }
}
