namespace iMievCan
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgPids = new System.Windows.Forms.DataGridView();
            this.cbSerialPorts = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSerialConnect = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bit_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bit_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bit_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bit_4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bit_5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bit_6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bit_7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bit_8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.val = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastupdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgPids)).BeginInit();
            this.SuspendLayout();
            // 
            // dgPids
            // 
            this.dgPids.AllowUserToDeleteRows = false;
            this.dgPids.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgPids.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPids.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.pid,
            this.bit_1,
            this.bit_2,
            this.bit_3,
            this.bit_4,
            this.bit_5,
            this.bit_6,
            this.bit_7,
            this.bit_8,
            this.formula,
            this.val,
            this.lastupdate});
            this.dgPids.Location = new System.Drawing.Point(12, 35);
            this.dgPids.Name = "dgPids";
            this.dgPids.Size = new System.Drawing.Size(1209, 612);
            this.dgPids.TabIndex = 0;
            this.dgPids.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgPids_CellBeginEdit);
            this.dgPids.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPids_CellContentClick);
            this.dgPids.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPids_CellEndEdit);
            this.dgPids.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgPids_UserAddedRow);
            // 
            // cbSerialPorts
            // 
            this.cbSerialPorts.FormattingEnabled = true;
            this.cbSerialPorts.Location = new System.Drawing.Point(52, 6);
            this.cbSerialPorts.Name = "cbSerialPorts";
            this.cbSerialPorts.Size = new System.Drawing.Size(121, 21);
            this.cbSerialPorts.TabIndex = 1;
            this.cbSerialPorts.DropDown += new System.EventHandler(this.cbSerialPorts_DropDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "serial:";
            // 
            // btnSerialConnect
            // 
            this.btnSerialConnect.Location = new System.Drawing.Point(179, 6);
            this.btnSerialConnect.Name = "btnSerialConnect";
            this.btnSerialConnect.Size = new System.Drawing.Size(75, 23);
            this.btnSerialConnect.TabIndex = 3;
            this.btnSerialConnect.Text = "Connect";
            this.btnSerialConnect.UseVisualStyleBackColor = true;
            this.btnSerialConnect.Click += new System.EventHandler(this.btnSerialConnect_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(713, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Title
            // 
            this.Title.HeaderText = "title";
            this.Title.Name = "Title";
            // 
            // pid
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pid.DefaultCellStyle = dataGridViewCellStyle4;
            this.pid.HeaderText = "pid";
            this.pid.Name = "pid";
            this.pid.Width = 46;
            // 
            // bit_1
            // 
            this.bit_1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.bit_1.HeaderText = "bit1";
            this.bit_1.Name = "bit_1";
            this.bit_1.Width = 49;
            // 
            // bit_2
            // 
            this.bit_2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.bit_2.HeaderText = "bit2";
            this.bit_2.Name = "bit_2";
            this.bit_2.Width = 49;
            // 
            // bit_3
            // 
            this.bit_3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.bit_3.HeaderText = "bit3";
            this.bit_3.Name = "bit_3";
            this.bit_3.Width = 49;
            // 
            // bit_4
            // 
            this.bit_4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.bit_4.HeaderText = "bit4";
            this.bit_4.Name = "bit_4";
            this.bit_4.Width = 49;
            // 
            // bit_5
            // 
            this.bit_5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.bit_5.HeaderText = "bit5";
            this.bit_5.Name = "bit_5";
            this.bit_5.Width = 49;
            // 
            // bit_6
            // 
            this.bit_6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.bit_6.HeaderText = "bit6";
            this.bit_6.Name = "bit_6";
            this.bit_6.Width = 49;
            // 
            // bit_7
            // 
            this.bit_7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.bit_7.HeaderText = "bit7";
            this.bit_7.Name = "bit_7";
            this.bit_7.Width = 49;
            // 
            // bit_8
            // 
            this.bit_8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.bit_8.HeaderText = "bit8";
            this.bit_8.Name = "bit_8";
            this.bit_8.Width = 49;
            // 
            // formula
            // 
            this.formula.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.formula.DefaultCellStyle = dataGridViewCellStyle5;
            this.formula.HeaderText = "formula";
            this.formula.Name = "formula";
            this.formula.Width = 66;
            // 
            // val
            // 
            this.val.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.val.DefaultCellStyle = dataGridViewCellStyle6;
            this.val.HeaderText = "val";
            this.val.Name = "val";
            // 
            // lastupdate
            // 
            this.lastupdate.HeaderText = "lastupdate";
            this.lastupdate.Name = "lastupdate";
            this.lastupdate.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 659);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSerialConnect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbSerialPorts);
            this.Controls.Add(this.dgPids);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgPids)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgPids;
        private System.Windows.Forms.ComboBox cbSerialPorts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSerialConnect;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn pid;
        private System.Windows.Forms.DataGridViewTextBoxColumn bit_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn bit_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn bit_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn bit_4;
        private System.Windows.Forms.DataGridViewTextBoxColumn bit_5;
        private System.Windows.Forms.DataGridViewTextBoxColumn bit_6;
        private System.Windows.Forms.DataGridViewTextBoxColumn bit_7;
        private System.Windows.Forms.DataGridViewTextBoxColumn bit_8;
        private System.Windows.Forms.DataGridViewTextBoxColumn formula;
        private System.Windows.Forms.DataGridViewTextBoxColumn val;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastupdate;
    }
}

