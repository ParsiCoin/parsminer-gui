namespace parsMG
{
    partial class PoolEntry
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.selectedCb = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.addressLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pingLbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.payoutLbl = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.feeLbl = new System.Windows.Forms.Label();
            this.hashLbl = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.updateStatsTimer = new System.Windows.Forms.Timer(this.components);
            this.label13 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // selectedCb
            // 
            this.selectedCb.AutoSize = true;
            this.selectedCb.Dock = System.Windows.Forms.DockStyle.Left;
            this.selectedCb.Location = new System.Drawing.Point(0, 0);
            this.selectedCb.Name = "selectedCb";
            this.selectedCb.Size = new System.Drawing.Size(15, 32);
            this.selectedCb.TabIndex = 0;
            this.selectedCb.UseVisualStyleBackColor = true;
            this.selectedCb.CheckedChanged += new System.EventHandler(this.selectedCb_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(15, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3, 9, 0, 0);
            this.label1.Size = new System.Drawing.Size(54, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Address:";
            // 
            // addressLbl
            // 
            this.addressLbl.AutoSize = true;
            this.addressLbl.Dock = System.Windows.Forms.DockStyle.Left;
            this.addressLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addressLbl.ForeColor = System.Drawing.Color.Navy;
            this.addressLbl.Location = new System.Drawing.Point(69, 0);
            this.addressLbl.Name = "addressLbl";
            this.addressLbl.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.addressLbl.Size = new System.Drawing.Size(13, 22);
            this.addressLbl.TabIndex = 2;
            this.addressLbl.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(82, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(3, 9, 0, 0);
            this.label3.Size = new System.Drawing.Size(36, 22);
            this.label3.TabIndex = 3;
            this.label3.Text = "Ping:";
            // 
            // pingLbl
            // 
            this.pingLbl.AutoSize = true;
            this.pingLbl.Dock = System.Windows.Forms.DockStyle.Left;
            this.pingLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pingLbl.ForeColor = System.Drawing.Color.Navy;
            this.pingLbl.Location = new System.Drawing.Point(118, 0);
            this.pingLbl.Name = "pingLbl";
            this.pingLbl.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.pingLbl.Size = new System.Drawing.Size(13, 22);
            this.pingLbl.TabIndex = 4;
            this.pingLbl.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(131, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(3, 9, 0, 0);
            this.label5.Size = new System.Drawing.Size(68, 22);
            this.label5.TabIndex = 5;
            this.label5.Text = "MinPayout:";
            // 
            // payoutLbl
            // 
            this.payoutLbl.AutoSize = true;
            this.payoutLbl.Dock = System.Windows.Forms.DockStyle.Left;
            this.payoutLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.payoutLbl.ForeColor = System.Drawing.Color.Navy;
            this.payoutLbl.Location = new System.Drawing.Point(199, 0);
            this.payoutLbl.Name = "payoutLbl";
            this.payoutLbl.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.payoutLbl.Size = new System.Drawing.Size(13, 22);
            this.payoutLbl.TabIndex = 6;
            this.payoutLbl.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(212, 0);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(3, 9, 0, 0);
            this.label7.Size = new System.Drawing.Size(31, 22);
            this.label7.TabIndex = 7;
            this.label7.Text = "Fee:";
            // 
            // feeLbl
            // 
            this.feeLbl.AutoSize = true;
            this.feeLbl.Dock = System.Windows.Forms.DockStyle.Left;
            this.feeLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.feeLbl.ForeColor = System.Drawing.Color.Navy;
            this.feeLbl.Location = new System.Drawing.Point(243, 0);
            this.feeLbl.Name = "feeLbl";
            this.feeLbl.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.feeLbl.Size = new System.Drawing.Size(13, 22);
            this.feeLbl.TabIndex = 8;
            this.feeLbl.Text = "0";
            // 
            // hashLbl
            // 
            this.hashLbl.AutoSize = true;
            this.hashLbl.Dock = System.Windows.Forms.DockStyle.Left;
            this.hashLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hashLbl.ForeColor = System.Drawing.Color.Navy;
            this.hashLbl.Location = new System.Drawing.Point(315, 0);
            this.hashLbl.Name = "hashLbl";
            this.hashLbl.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.hashLbl.Size = new System.Drawing.Size(13, 22);
            this.hashLbl.TabIndex = 10;
            this.hashLbl.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Location = new System.Drawing.Point(256, 0);
            this.label10.Name = "label10";
            this.label10.Padding = new System.Windows.Forms.Padding(3, 9, 0, 0);
            this.label10.Size = new System.Drawing.Size(59, 22);
            this.label10.TabIndex = 9;
            this.label10.Text = "Hashrate:";
            // 
            // updateStatsTimer
            // 
            this.updateStatsTimer.Interval = 10000;
            this.updateStatsTimer.Tick += new System.EventHandler(this.updateStatsTimer_Tick);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Navy;
            this.label13.Location = new System.Drawing.Point(490, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(22, 25);
            this.label13.TabIndex = 11;
            this.label13.Text = "x";
            this.label13.Visible = false;
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // PoolEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label13);
            this.Controls.Add(this.hashLbl);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.feeLbl);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.payoutLbl);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pingLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.addressLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selectedCb);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PoolEntry";
            this.Size = new System.Drawing.Size(512, 32);
            this.Load += new System.EventHandler(this.PoolEntry_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label addressLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label pingLbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label payoutLbl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label feeLbl;
        private System.Windows.Forms.Label hashLbl;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Timer updateStatsTimer;
        public System.Windows.Forms.CheckBox selectedCb;
        private System.Windows.Forms.Label label13;
    }
}
