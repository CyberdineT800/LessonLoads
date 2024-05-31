namespace LessonLoads
{
    partial class Main
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
            this.tabPages = new System.Windows.Forms.TabControl();
            this.directionPage = new System.Windows.Forms.TabPage();
            this.groupPage = new System.Windows.Forms.TabPage();
            this.subjectPage = new System.Windows.Forms.TabPage();
            this.subjectLoadsPage = new System.Windows.Forms.TabPage();
            this.teacherPage = new System.Windows.Forms.TabPage();
            this.studentLoadPage = new System.Windows.Forms.TabPage();
            this.directionsGrid = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dirID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dirName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dirType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPages.SuspendLayout();
            this.directionPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.directionsGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPages
            // 
            this.tabPages.Controls.Add(this.subjectPage);
            this.tabPages.Controls.Add(this.directionPage);
            this.tabPages.Controls.Add(this.groupPage);
            this.tabPages.Controls.Add(this.subjectLoadsPage);
            this.tabPages.Controls.Add(this.teacherPage);
            this.tabPages.Controls.Add(this.studentLoadPage);
            this.tabPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPages.Location = new System.Drawing.Point(0, 0);
            this.tabPages.Name = "tabPages";
            this.tabPages.SelectedIndex = 0;
            this.tabPages.Size = new System.Drawing.Size(1161, 651);
            this.tabPages.TabIndex = 0;
            // 
            // directionPage
            // 
            this.directionPage.BackColor = System.Drawing.Color.OldLace;
            this.directionPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.directionPage.Controls.Add(this.panel1);
            this.directionPage.Controls.Add(this.directionsGrid);
            this.directionPage.Location = new System.Drawing.Point(4, 24);
            this.directionPage.Name = "directionPage";
            this.directionPage.Size = new System.Drawing.Size(1153, 623);
            this.directionPage.TabIndex = 0;
            this.directionPage.Text = "Yo\'nalishlar";
            this.directionPage.ToolTipText = "Yo\'nalishlar";
            // 
            // groupPage
            // 
            this.groupPage.BackColor = System.Drawing.Color.OldLace;
            this.groupPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.groupPage.Location = new System.Drawing.Point(4, 24);
            this.groupPage.Name = "groupPage";
            this.groupPage.Size = new System.Drawing.Size(1153, 623);
            this.groupPage.TabIndex = 1;
            this.groupPage.Text = "Guruhlar";
            // 
            // subjectPage
            // 
            this.subjectPage.BackColor = System.Drawing.Color.OldLace;
            this.subjectPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.subjectPage.Location = new System.Drawing.Point(4, 24);
            this.subjectPage.Name = "subjectPage";
            this.subjectPage.Size = new System.Drawing.Size(1153, 623);
            this.subjectPage.TabIndex = 2;
            this.subjectPage.Text = "Fanlar";
            // 
            // subjectLoadsPage
            // 
            this.subjectLoadsPage.BackColor = System.Drawing.Color.OldLace;
            this.subjectLoadsPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.subjectLoadsPage.Location = new System.Drawing.Point(4, 24);
            this.subjectLoadsPage.Name = "subjectLoadsPage";
            this.subjectLoadsPage.Size = new System.Drawing.Size(1153, 623);
            this.subjectLoadsPage.TabIndex = 4;
            this.subjectLoadsPage.Text = "Fan yuklamalari";
            // 
            // teacherPage
            // 
            this.teacherPage.BackColor = System.Drawing.Color.OldLace;
            this.teacherPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.teacherPage.Location = new System.Drawing.Point(4, 24);
            this.teacherPage.Name = "teacherPage";
            this.teacherPage.Size = new System.Drawing.Size(1153, 623);
            this.teacherPage.TabIndex = 3;
            this.teacherPage.Text = "O\'qituvchilar";
            // 
            // studentLoadPage
            // 
            this.studentLoadPage.BackColor = System.Drawing.Color.OldLace;
            this.studentLoadPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.studentLoadPage.Location = new System.Drawing.Point(4, 24);
            this.studentLoadPage.Name = "studentLoadPage";
            this.studentLoadPage.Size = new System.Drawing.Size(1153, 623);
            this.studentLoadPage.TabIndex = 5;
            this.studentLoadPage.Text = "Talaba yuklamasi";
            // 
            // directionsGrid
            // 
            this.directionsGrid.AllowUserToAddRows = false;
            this.directionsGrid.AllowUserToDeleteRows = false;
            this.directionsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.directionsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.directionsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.directionsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dirID,
            this.dirName,
            this.dirType});
            this.directionsGrid.Location = new System.Drawing.Point(372, 3);
            this.directionsGrid.Name = "directionsGrid";
            this.directionsGrid.ReadOnly = true;
            this.directionsGrid.Size = new System.Drawing.Size(776, 615);
            this.directionsGrid.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(366, 615);
            this.panel1.TabIndex = 1;
            // 
            // dirID
            // 
            this.dirID.HeaderText = "ID";
            this.dirID.Name = "dirID";
            this.dirID.ReadOnly = true;
            // 
            // dirName
            // 
            this.dirName.HeaderText = "Nomi";
            this.dirName.Name = "dirName";
            this.dirName.ReadOnly = true;
            // 
            // dirType
            // 
            this.dirType.HeaderText = "Turi";
            this.dirType.Name = "dirType";
            this.dirType.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nomi: ";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(71, 16);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(276, 53);
            this.textBox1.TabIndex = 1;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1161, 651);
            this.Controls.Add(this.tabPages);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(1177, 690);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.tabPages.ResumeLayout(false);
            this.directionPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.directionsGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabPages;
        private System.Windows.Forms.TabPage directionPage;
        private System.Windows.Forms.TabPage groupPage;
        private System.Windows.Forms.TabPage subjectPage;
        private System.Windows.Forms.TabPage subjectLoadsPage;
        private System.Windows.Forms.TabPage teacherPage;
        private System.Windows.Forms.TabPage studentLoadPage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView directionsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dirID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dirName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dirType;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}

