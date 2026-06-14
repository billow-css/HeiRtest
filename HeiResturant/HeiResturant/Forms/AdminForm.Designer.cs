namespace HeiResturant.Forms
{
    partial class AdminForm
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
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNewUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboNewRole = new System.Windows.Forms.ComboBox();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.tabStudents = new System.Windows.Forms.TabPage();
            this.dgvStudents = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtStudentNo = new System.Windows.Forms.TextBox();
            this.txtStudentName = new System.Windows.Forms.TextBox();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtInitialBalance = new System.Windows.Forms.TextBox();
            this.btnAddStudent = new System.Windows.Forms.Button();
            this.btnRecharge = new System.Windows.Forms.Button();
            this.tabFoods = new System.Windows.Forms.TabPage();
            this.dgvFoods = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.txtFoodName = new System.Windows.Forms.TextBox();
            this.txtFoodPrice = new System.Windows.Forms.TextBox();
            this.txtFoodStock = new System.Windows.Forms.TextBox();
            this.btnAddFood = new System.Windows.Forms.Button();
            this.btnDeleteFood = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.tabStudents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).BeginInit();
            this.tabFoods.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFoods)).BeginInit();
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
            this.lblWelcome.Text = "管理员";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabUsers);
            this.tabControl.Controls.Add(this.tabStudents);
            this.tabControl.Controls.Add(this.tabFoods);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tabControl.Location = new System.Drawing.Point(0, 40);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1000, 560);
            // 
            // tabUsers - 工作人员（不含学生）
            // 
            this.tabUsers.Controls.Add(this.dgvUsers);
            this.tabUsers.Controls.Add(this.label1);
            this.tabUsers.Controls.Add(this.txtNewUsername);
            this.tabUsers.Controls.Add(this.label3);
            this.tabUsers.Controls.Add(this.cboNewRole);
            this.tabUsers.Controls.Add(this.btnAddUser);
            this.tabUsers.Controls.Add(this.btnDeleteUser);
            this.tabUsers.Location = new System.Drawing.Point(4, 28);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Size = new System.Drawing.Size(992, 528);
            this.tabUsers.Text = "工作人员";
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.Location = new System.Drawing.Point(8, 8);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.Size = new System.Drawing.Size(976, 400);
            // 
            // label1, txtNewUsername, label3, cboNewRole, buttons
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 430);
            this.label1.Text = "用户名：";
            this.txtNewUsername.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.txtNewUsername.Location = new System.Drawing.Point(80, 427);
            this.txtNewUsername.Size = new System.Drawing.Size(180, 27);
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(280, 430);
            this.label3.Text = "角色：";
            this.cboNewRole.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.cboNewRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNewRole.Location = new System.Drawing.Point(340, 427);
            this.cboNewRole.Size = new System.Drawing.Size(150, 28);
            this.btnAddUser.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnAddUser.Location = new System.Drawing.Point(520, 423);
            this.btnAddUser.Size = new System.Drawing.Size(100, 32);
            this.btnAddUser.Text = "添加";
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            this.btnDeleteUser.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnDeleteUser.Location = new System.Drawing.Point(640, 423);
            this.btnDeleteUser.Size = new System.Drawing.Size(100, 32);
            this.btnDeleteUser.Text = "删除";
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // tabStudents - 学生与校园卡合一
            // 
            this.tabStudents.Controls.Add(this.dgvStudents);
            this.tabStudents.Controls.Add(this.label4);
            this.tabStudents.Controls.Add(this.label5);
            this.tabStudents.Controls.Add(this.label6);
            this.tabStudents.Controls.Add(this.label7);
            this.tabStudents.Controls.Add(this.label9);
            this.tabStudents.Controls.Add(this.txtStudentNo);
            this.tabStudents.Controls.Add(this.txtStudentName);
            this.tabStudents.Controls.Add(this.txtClassName);
            this.tabStudents.Controls.Add(this.txtPhone);
            this.tabStudents.Controls.Add(this.txtInitialBalance);
            this.tabStudents.Controls.Add(this.btnAddStudent);
            this.tabStudents.Controls.Add(this.btnRecharge);
            this.tabStudents.Location = new System.Drawing.Point(4, 28);
            this.tabStudents.Name = "tabStudents";
            this.tabStudents.Size = new System.Drawing.Size(992, 528);
            this.tabStudents.Text = "学生与校园卡";
            // 
            // dgvStudents
            // 
            this.dgvStudents.AllowUserToAddRows = false;
            this.dgvStudents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStudents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStudents.Location = new System.Drawing.Point(8, 8);
            this.dgvStudents.Name = "dgvStudents";
            this.dgvStudents.Size = new System.Drawing.Size(976, 380);
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true; this.label4.Location = new System.Drawing.Point(8, 410); this.label4.Text = "学号（即卡号）：";
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true; this.label5.Location = new System.Drawing.Point(250, 410); this.label5.Text = "姓名：";
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true; this.label6.Location = new System.Drawing.Point(430, 410); this.label6.Text = "班级：";
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true; this.label7.Location = new System.Drawing.Point(8, 450); this.label7.Text = "电话：";
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true; this.label9.Location = new System.Drawing.Point(250, 450); this.label9.Text = "初始余额：";
            this.txtStudentNo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.txtStudentNo.Location = new System.Drawing.Point(120, 407); this.txtStudentNo.Size = new System.Drawing.Size(120, 27);
            this.txtStudentName.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.txtStudentName.Location = new System.Drawing.Point(300, 407); this.txtStudentName.Size = new System.Drawing.Size(120, 27);
            this.txtClassName.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.txtClassName.Location = new System.Drawing.Point(480, 407); this.txtClassName.Size = new System.Drawing.Size(120, 27);
            this.txtPhone.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.txtPhone.Location = new System.Drawing.Point(55, 447); this.txtPhone.Size = new System.Drawing.Size(180, 27);
            this.txtInitialBalance.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.txtInitialBalance.Location = new System.Drawing.Point(340, 447); this.txtInitialBalance.Size = new System.Drawing.Size(80, 27); this.txtInitialBalance.Text = "0";
            this.btnAddStudent.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnAddStudent.Location = new System.Drawing.Point(450, 443); this.btnAddStudent.Size = new System.Drawing.Size(110, 32);
            this.btnAddStudent.Text = "添加学生";
            this.btnAddStudent.Click += new System.EventHandler(this.btnAddStudent_Click);
            this.btnRecharge.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnRecharge.Location = new System.Drawing.Point(580, 443); this.btnRecharge.Size = new System.Drawing.Size(110, 32);
            this.btnRecharge.Text = "选中充值";
            this.btnRecharge.Click += new System.EventHandler(this.btnRecharge_Click);
            // 
            // tabFoods - 单餐厅，无需选餐厅
            // 
            this.tabFoods.Controls.Add(this.dgvFoods);
            this.tabFoods.Controls.Add(this.label11);
            this.tabFoods.Controls.Add(this.label12);
            this.tabFoods.Controls.Add(this.label13);
            this.tabFoods.Controls.Add(this.label14);
            this.tabFoods.Controls.Add(this.cboCategory);
            this.tabFoods.Controls.Add(this.txtFoodName);
            this.tabFoods.Controls.Add(this.txtFoodPrice);
            this.tabFoods.Controls.Add(this.txtFoodStock);
            this.tabFoods.Controls.Add(this.btnAddFood);
            this.tabFoods.Controls.Add(this.btnDeleteFood);
            this.tabFoods.Location = new System.Drawing.Point(4, 28);
            this.tabFoods.Name = "tabFoods";
            this.tabFoods.Size = new System.Drawing.Size(992, 528);
            this.tabFoods.Text = "菜品管理";
            // 
            // dgvFoods
            // 
            this.dgvFoods.AllowUserToAddRows = false;
            this.dgvFoods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFoods.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFoods.Location = new System.Drawing.Point(8, 8);
            this.dgvFoods.Name = "dgvFoods";
            this.dgvFoods.Size = new System.Drawing.Size(976, 380);
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true; this.label11.Location = new System.Drawing.Point(8, 410); this.label11.Text = "分类：";
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.label12.AutoSize = true; this.label12.Location = new System.Drawing.Point(250, 410); this.label12.Text = "名称：";
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.label13.AutoSize = true; this.label13.Location = new System.Drawing.Point(8, 450); this.label13.Text = "价格：";
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.label14.AutoSize = true; this.label14.Location = new System.Drawing.Point(180, 450); this.label14.Text = "库存：";
            this.cboCategory.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.Location = new System.Drawing.Point(60, 407); this.cboCategory.Size = new System.Drawing.Size(170, 28);
            this.txtFoodName.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.txtFoodName.Location = new System.Drawing.Point(300, 407); this.txtFoodName.Size = new System.Drawing.Size(170, 27);
            this.txtFoodPrice.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.txtFoodPrice.Location = new System.Drawing.Point(60, 447); this.txtFoodPrice.Size = new System.Drawing.Size(100, 27);
            this.txtFoodStock.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.txtFoodStock.Location = new System.Drawing.Point(230, 447); this.txtFoodStock.Size = new System.Drawing.Size(100, 27);
            this.btnAddFood.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnAddFood.Location = new System.Drawing.Point(360, 443); this.btnAddFood.Size = new System.Drawing.Size(100, 32);
            this.btnAddFood.Text = "添加菜品";
            this.btnAddFood.Click += new System.EventHandler(this.btnAddFood_Click);
            this.btnDeleteFood.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnDeleteFood.Location = new System.Drawing.Point(480, 443); this.btnDeleteFood.Size = new System.Drawing.Size(100, 32);
            this.btnDeleteFood.Text = "删除菜品";
            this.btnDeleteFood.Click += new System.EventHandler(this.btnDeleteFood_Click);
            // 
            // AdminForm
            // 
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.lblWelcome);
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "管理员后台";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabUsers.ResumeLayout(false);
            this.tabUsers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.tabStudents.ResumeLayout(false);
            this.tabStudents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).EndInit();
            this.tabFoods.ResumeLayout(false);
            this.tabFoods.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFoods)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNewUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboNewRole;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.TabPage tabStudents;
        private System.Windows.Forms.DataGridView dgvStudents;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtStudentNo;
        private System.Windows.Forms.TextBox txtStudentName;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtInitialBalance;
        private System.Windows.Forms.Button btnAddStudent;
        private System.Windows.Forms.Button btnRecharge;
        private System.Windows.Forms.TabPage tabFoods;
        private System.Windows.Forms.DataGridView dgvFoods;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.TextBox txtFoodName;
        private System.Windows.Forms.TextBox txtFoodPrice;
        private System.Windows.Forms.TextBox txtFoodStock;
        private System.Windows.Forms.Button btnAddFood;
        private System.Windows.Forms.Button btnDeleteFood;
    }
}
