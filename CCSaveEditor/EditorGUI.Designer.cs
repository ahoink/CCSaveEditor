namespace CCSaveEditor
{
    partial class EditorGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.nameLabel = new System.Windows.Forms.Label();
            this.legacyLabel = new System.Windows.Forms.Label();
            this.sessLabel = new System.Windows.Forms.Label();
            this.verLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progLbl = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(985, 500);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.BackgroundImage = global::CCSaveEditor.Properties.Resources.ccbg;
            this.tabPage1.Controls.Add(this.nameLabel);
            this.tabPage1.Controls.Add(this.legacyLabel);
            this.tabPage1.Controls.Add(this.sessLabel);
            this.tabPage1.Controls.Add(this.verLabel);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(977, 474);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.ForeColor = System.Drawing.Color.Azure;
            this.nameLabel.Location = new System.Drawing.Point(448, 3);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(58, 18);
            this.nameLabel.TabIndex = 4;
            this.nameLabel.Text = "NAME";
            // 
            // legacyLabel
            // 
            this.legacyLabel.AutoSize = true;
            this.legacyLabel.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.legacyLabel.ForeColor = System.Drawing.Color.Azure;
            this.legacyLabel.Location = new System.Drawing.Point(311, 3);
            this.legacyLabel.Name = "legacyLabel";
            this.legacyLabel.Size = new System.Drawing.Size(76, 18);
            this.legacyLabel.TabIndex = 3;
            this.legacyLabel.Text = "LEGACY";
            // 
            // sessLabel
            // 
            this.sessLabel.AutoSize = true;
            this.sessLabel.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sessLabel.ForeColor = System.Drawing.Color.Azure;
            this.sessLabel.Location = new System.Drawing.Point(168, 3);
            this.sessLabel.Name = "sessLabel";
            this.sessLabel.Size = new System.Drawing.Size(82, 18);
            this.sessLabel.TabIndex = 2;
            this.sessLabel.Text = "SESSION";
            // 
            // verLabel
            // 
            this.verLabel.AutoSize = true;
            this.verLabel.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verLabel.ForeColor = System.Drawing.Color.Azure;
            this.verLabel.Location = new System.Drawing.Point(24, 3);
            this.verLabel.Name = "verLabel";
            this.verLabel.Size = new System.Drawing.Size(85, 18);
            this.verLabel.TabIndex = 1;
            this.verLabel.Text = "VERSION";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(418, 445);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Generate New Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(977, 474);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Upgrades";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(977, 474);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Achievements";
            // 
            // tabPage4
            // 
            this.tabPage4.BackgroundImage = global::CCSaveEditor.Properties.Resources.ccbg;
            this.tabPage4.Controls.Add(this.label36);
            this.tabPage4.Controls.Add(this.label35);
            this.tabPage4.Controls.Add(this.label34);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(977, 474);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Buildings";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.Azure;
            this.label36.Location = new System.Drawing.Point(731, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(156, 18);
            this.label36.TabIndex = 37;
            this.label36.Text = "Cookies Produced";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.Color.Azure;
            this.label35.Location = new System.Drawing.Point(464, 3);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(67, 18);
            this.label35.TabIndex = 36;
            this.label35.Text = "Bought";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.Color.Azure;
            this.label34.Location = new System.Drawing.Point(199, 3);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(65, 18);
            this.label34.TabIndex = 35;
            this.label34.Text = "Owned";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(4, 500);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(977, 20);
            this.progressBar1.TabIndex = 1;
            // 
            // progLbl
            // 
            this.progLbl.AutoSize = true;
            this.progLbl.BackColor = System.Drawing.Color.Transparent;
            this.progLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progLbl.Location = new System.Drawing.Point(423, 501);
            this.progLbl.Name = "progLbl";
            this.progLbl.Size = new System.Drawing.Size(138, 16);
            this.progLbl.TabIndex = 2;
            this.progLbl.Text = "Loading image X/X";
            // 
            // EditorGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 521);
            this.Controls.Add(this.progLbl);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tabControl1);
            this.Name = "EditorGUI";
            this.Text = "CCSaveEditor v0.1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label legacyLabel;
        private System.Windows.Forms.Label sessLabel;
        private System.Windows.Forms.Label verLabel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label progLbl;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
    }
}