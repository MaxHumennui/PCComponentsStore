
namespace PCShop
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.orders = new System.Windows.Forms.Button();
            this.cusomers = new System.Windows.Forms.Button();
            this.bin = new System.Windows.Forms.Button();
            this.access = new System.Windows.Forms.Button();
            this.makers = new System.Windows.Forms.Button();
            this.menu = new System.Windows.Forms.Panel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // orders
            // 
            this.orders.Location = new System.Drawing.Point(3, 118);
            this.orders.Name = "orders";
            this.orders.Size = new System.Drawing.Size(120, 23);
            this.orders.TabIndex = 0;
            this.orders.Text = "ЗАМОВЛЕННЯ";
            this.orders.UseVisualStyleBackColor = true;
            this.orders.Click += new System.EventHandler(this.orders_Click);
            // 
            // cusomers
            // 
            this.cusomers.Location = new System.Drawing.Point(3, 60);
            this.cusomers.Name = "cusomers";
            this.cusomers.Size = new System.Drawing.Size(120, 23);
            this.cusomers.TabIndex = 0;
            this.cusomers.Text = "КЛІЄНТИ";
            this.cusomers.UseVisualStyleBackColor = true;
            this.cusomers.Click += new System.EventHandler(this.cusomers_Click);
            // 
            // bin
            // 
            this.bin.Location = new System.Drawing.Point(3, 147);
            this.bin.Name = "bin";
            this.bin.Size = new System.Drawing.Size(120, 23);
            this.bin.TabIndex = 0;
            this.bin.Text = "КОРЗИНА";
            this.bin.UseVisualStyleBackColor = true;
            this.bin.Click += new System.EventHandler(this.bin_Click);
            // 
            // access
            // 
            this.access.Location = new System.Drawing.Point(3, 31);
            this.access.Name = "access";
            this.access.Size = new System.Drawing.Size(120, 23);
            this.access.TabIndex = 0;
            this.access.Text = "КОМПЛЕКТУЮЧІ";
            this.access.UseVisualStyleBackColor = true;
            this.access.Click += new System.EventHandler(this.access_Click);
            // 
            // makers
            // 
            this.makers.Location = new System.Drawing.Point(3, 89);
            this.makers.Name = "makers";
            this.makers.Size = new System.Drawing.Size(120, 23);
            this.makers.TabIndex = 0;
            this.makers.Text = "ВИРОБНИКИ";
            this.makers.UseVisualStyleBackColor = true;
            this.makers.Click += new System.EventHandler(this.makers_Click);
            // 
            // menu
            // 
            this.menu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.menu.Controls.Add(this.access);
            this.menu.Controls.Add(this.bin);
            this.menu.Controls.Add(this.makers);
            this.menu.Controls.Add(this.orders);
            this.menu.Controls.Add(this.cusomers);
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(126, 609);
            this.menu.TabIndex = 1;
            // 
            // contentPanel
            // 
            this.contentPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contentPanel.AutoScroll = true;
            this.contentPanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.contentPanel.Location = new System.Drawing.Point(132, 12);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(1005, 560);
            this.contentPanel.TabIndex = 3;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 609);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "  ";
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button orders;
        private System.Windows.Forms.Button cusomers;
        private System.Windows.Forms.Button bin;
        private System.Windows.Forms.Button access;
        private System.Windows.Forms.Button makers;
        private System.Windows.Forms.Panel menu;
        private System.Windows.Forms.ImageList imageList1;
        public System.Windows.Forms.Panel contentPanel;
    }
}

