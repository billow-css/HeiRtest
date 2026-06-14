using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HeiResturant.DAL;
using HeiResturant.Helpers;
using HeiResturant.Models;

namespace HeiResturant.Forms
{
    public partial class CashierForm : Form
    {
        private readonly UserInfo _user;
        private readonly List<CartItem> _cart = new List<CartItem>();
        private int _selectedCardId;
        private int _restaurantId;

        public CashierForm(UserInfo user)
        {
            _user = user;
            InitializeComponent();
            lblWelcome.Text = $"收银员：{user.Username}";
            _restaurantId = FoodDAL.GetDefaultRestaurantId();

            dgvCart.DataBindingComplete += (s, e) =>
            {
                GridHelper.EnableEdit(dgvCart, "FoodId", "FoodName", "UnitPrice", "SubTotal");
                if (dgvCart.Columns.Contains("FoodId"))
                    dgvCart.Columns["FoodId"].Visible = false;
            };
            dgvCart.CellEndEdit += dgvCart_CellEndEdit;

            LoadFoods();
        }

        private void LoadFoods()
        {
            dgvFoods.DataSource = FoodDAL.GetAvailableFoods()
                .Select(f => new { f.FoodId, f.CategoryName, f.Name, f.Price, f.Stock })
                .ToList();
        }

        private void RefreshCart()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = _cart.Select(c => new
            {
                c.FoodId,
                c.FoodName,
                c.UnitPrice,
                c.Quantity,
                c.SubTotal
            }).ToList();
            lblTotal.Text = $"合计：{_cart.Sum(c => c.SubTotal):F2} 元";
        }

        private void dgvCart_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvCart.Columns[e.ColumnIndex].Name != "Quantity") return;

            var row = dgvCart.Rows[e.RowIndex];
            int foodId = Convert.ToInt32(row.Cells["FoodId"].Value);
            var item = _cart.FirstOrDefault(c => c.FoodId == foodId);
            if (item == null) return;

            if (!int.TryParse(row.Cells["Quantity"].Value?.ToString(), out int qty) || qty <= 0)
            {
                MessageBox.Show("数量必须为正整数");
                RefreshCart();
                return;
            }

            item.Quantity = qty;
            RefreshCart();
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (dgvFoods.CurrentRow == null) return;

            int foodId = Convert.ToInt32(dgvFoods.CurrentRow.Cells["FoodId"].Value);
            string foodName = dgvFoods.CurrentRow.Cells["Name"].Value.ToString();
            decimal price = Convert.ToDecimal(dgvFoods.CurrentRow.Cells["Price"].Value);
            int stock = Convert.ToInt32(dgvFoods.CurrentRow.Cells["Stock"].Value);

            if (!int.TryParse(txtQuantity.Text, out int qty) || qty <= 0)
            {
                MessageBox.Show("请输入有效数量");
                return;
            }

            if (qty > stock)
            {
                MessageBox.Show($"库存不足，当前库存 {stock}");
                return;
            }

            var existing = _cart.FirstOrDefault(c => c.FoodId == foodId);
            if (existing != null)
            {
                if (existing.Quantity + qty > stock)
                {
                    MessageBox.Show($"库存不足，当前库存 {stock}");
                    return;
                }
                existing.Quantity += qty;
            }
            else
            {
                _cart.Add(new CartItem
                {
                    FoodId = foodId,
                    FoodName = foodName,
                    UnitPrice = price,
                    Quantity = qty
                });
            }

            RefreshCart();
        }

        private void btnRemoveFromCart_Click(object sender, EventArgs e)
        {
            if (dgvCart.CurrentRow == null) return;
            int foodId = Convert.ToInt32(dgvCart.CurrentRow.Cells["FoodId"].Value);
            _cart.RemoveAll(c => c.FoodId == foodId);
            RefreshCart();
        }

        private void btnQueryCard_Click(object sender, EventArgs e)
        {
            var cardNo = txtCardNo.Text.Trim();
            if (string.IsNullOrEmpty(cardNo))
            {
                MessageBox.Show("请输入学号或卡号");
                return;
            }

            var dt = CardDAL.GetCardByCardNo(cardNo);
            if (dt.Rows.Count == 0)
            {
                lblCardInfo.Text = "未找到该校园卡";
                _selectedCardId = 0;
                return;
            }

            var row = dt.Rows[0];
            _selectedCardId = Convert.ToInt32(row["CardId"]);
            lblCardInfo.Text = $"学生：{row["StudentName"]} | 学号：{row["StudentNo"]} | 余额：{Convert.ToDecimal(row["Balance"]):F2} 元 | 状态：{row["Status"]}";
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (_selectedCardId == 0)
            {
                MessageBox.Show("请先查询并确认校园卡");
                return;
            }

            if (_cart.Count == 0)
            {
                MessageBox.Show("购物车为空");
                return;
            }

            var items = _cart.ToList();
            if (_restaurantId == 0)
            {
                MessageBox.Show("未找到餐厅信息，请联系管理员");
                return;
            }

            string error = OrderDAL.Checkout(_selectedCardId, _user.UserId, _restaurantId, items, out int orderId);

            if (error != null)
            {
                MessageBox.Show(error, "结账失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show($"结账成功！订单号：{orderId}\n金额：{items.Sum(i => i.SubTotal):F2} 元",
                "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _cart.Clear();
            RefreshCart();
            btnQueryCard_Click(sender, e);
            LoadFoods();
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            _cart.Clear();
            RefreshCart();
        }
    }
}
