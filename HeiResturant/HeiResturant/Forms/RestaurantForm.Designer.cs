namespace HeiResturant.Forms
{
    partial class RestaurantForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblWelcome = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabToday = new System.Windows.Forms.TabPage();
            this.splitToday = new System.Windows.Forms.SplitContainer();
            this.dgvTodayOrders = new System.Windows.Forms.DataGridView();
            this.dgvOrderDetails = new System.Windows.Forms.DataGridView();
            this.tabSales = new System.Windows.Forms.TabPage();
            this.dgvSales = new System.Windows.Forms.DataGridView();
            this.tabStock = new System.Windows.Forms.TabPage();
            this.dgvLowStock = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabToday.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitToday)).BeginInit();
            this.splitToday.Panel1.SuspendLayout();
            this.splitToday.Panel2.SuspendLayout();
            this.splitToday.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTodayOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).BeginInit();
            this.tabSales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).BeginInit();
            this.tabStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWelcome.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.Location = new System.Drawing.Point(0, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblWelcome.Size = new System.Drawing.Size(1000, 40);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "餐厅管理";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRefresh.Location = new System.Drawing.Point(0, 555);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(1000, 45);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "刷新数据";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabToday);
            this.tabControl.Controls.Add(this.tabSales);
            this.tabControl.Controls.Add(this.tabStock);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tabControl.Location = new System.Drawing.Point(0, 40);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1000, 515);
            this.tabControl.TabIndex = 1;
            // 
            // tabToday
            // 
            this.tabToday.Controls.Add(this.splitToday);
            this.tabToday.Location = new System.Drawing.Point(4, 28);
            this.tabToday.Name = "tabToday";
            this.tabToday.Size = new System.Drawing.Size(992, 483);
            this.tabToday.TabIndex = 0;
            this.tabToday.Text = "今日订单";
            this.tabToday.UseVisualStyleBackColor = true;
            // 
            // splitToday
            // 
            this.splitToday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitToday.Location = new System.Drawing.Point(0, 0);
            this.splitToday.Name = "splitToday";
            this.splitToday.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitToday.Panel1
            // 
            this.splitToday.Panel1.Controls.Add(this.dgvTodayOrders);
            // 
            // splitToday.Panel2
            // 
            this.splitToday.Panel2.Controls.Add(this.dgvOrderDetails);
            this.splitToday.Size = new System.Drawing.Size(992, 483);
            this.splitToday.SplitterDistance = 280;
            this.splitToday.TabIndex = 0;
            // 
            // dgvTodayOrders
            // 
            this.dgvTodayOrders.AllowUserToAddRows = false;
            this.dgvTodayOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTodayOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTodayOrders.Location = new System.Drawing.Point(0, 0);
            this.dgvTodayOrders.Name = "dgvTodayOrders";
            this.dgvTodayOrders.ReadOnly = true;
            this.dgvTodayOrders.RowHeadersWidth = 51;
            this.dgvTodayOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTodayOrders.Size = new System.Drawing.Size(992, 280);
            this.dgvTodayOrders.TabIndex = 0;
            this.dgvTodayOrders.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTodayOrders_CellClick);
            // 
            // dgvOrderDetails
            // 
            this.dgvOrderDetails.AllowUserToAddRows = false;
            this.dgvOrderDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrderDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderDetails.Location = new System.Drawing.Point(0, 0);
            this.dgvOrderDetails.Name = "dgvOrderDetails";
            this.dgvOrderDetails.ReadOnly = true;
            this.dgvOrderDetails.RowHeadersWidth = 51;
            this.dgvOrderDetails.Size = new System.Drawing.Size(992, 199);
            this.dgvOrderDetails.TabIndex = 0;
            // 
            // tabSales
            // 
            this.tabSales.Controls.Add(this.dgvSales);
            this.tabSales.Location = new System.Drawing.Point(4, 28);
            this.tabSales.Name = "tabSales";
            this.tabSales.Size = new System.Drawing.Size(992, 483);
            this.tabSales.TabIndex = 1;
            this.tabSales.Text = "销售统计";
            this.tabSales.UseVisualStyleBackColor = true;
            // 
            // dgvSales
            // 
            this.dgvSales.AllowUserToAddRows = false;
            this.dgvSales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSales.Location = new System.Drawing.Point(0, 0);
            this.dgvSales.Name = "dgvSales";
            this.dgvSales.ReadOnly = true;
            this.dgvSales.RowHeadersWidth = 51;
            this.dgvSales.Size = new System.Drawing.Size(992, 483);
            this.dgvSales.TabIndex = 0;
            // 
            // tabStock
            // 
            this.tabStock.Controls.Add(this.dgvLowStock);
            this.tabStock.Location = new System.Drawing.Point(4, 28);
            this.tabStock.Name = "tabStock";
            this.tabStock.Size = new System.Drawing.Size(992, 483);
            this.tabStock.TabIndex = 2;
            this.tabStock.Text = "菜品补货";
            this.tabStock.UseVisualStyleBackColor = true;
            // 
            // dgvLowStock
            // 
            this.dgvLowStock.AllowUserToAddRows = false;
            this.dgvLowStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLowStock.Location = new System.Drawing.Point(0, 0);
            this.dgvLowStock.Name = "dgvLowStock";
            this.dgvLowStock.RowHeadersWidth = 51;
            this.dgvLowStock.Size = new System.Drawing.Size(992, 483);
            this.dgvLowStock.TabIndex = 0;
            // 
            // RestaurantForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblWelcome);
            this.Name = "RestaurantForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "餐厅管理";
            this.tabControl.ResumeLayout(false);
            this.tabToday.ResumeLayout(false);
            this.splitToday.Panel1.ResumeLayout(false);
            this.splitToday.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitToday)).EndInit();
            this.splitToday.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTodayOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).EndInit();
            this.tabSales.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).EndInit();
            this.tabStock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabToday;
        private System.Windows.Forms.SplitContainer splitToday;
        private System.Windows.Forms.DataGridView dgvTodayOrders;
        private System.Windows.Forms.DataGridView dgvOrderDetails;
        private System.Windows.Forms.TabPage tabSales;
        private System.Windows.Forms.DataGridView dgvSales;
        private System.Windows.Forms.TabPage tabStock;
        private System.Windows.Forms.DataGridView dgvLowStock;
        private System.Windows.Forms.Button btnRefresh;
    }
}
