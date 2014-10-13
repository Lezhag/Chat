namespace Server
{
    partial class ServerControl
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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabClients = new System.Windows.Forms.TabPage();
            this.lstClients = new System.Windows.Forms.ListView();
            this.tabChat = new System.Windows.Forms.TabPage();
            this.rtbChat = new System.Windows.Forms.RichTextBox();
            this.tabSearchMsg = new System.Windows.Forms.TabPage();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.dgvMessages = new System.Windows.Forms.DataGridView();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.lbSearchKeyword = new System.Windows.Forms.Label();
            this.btnSearchKeyword = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.btnSearchDate = new System.Windows.Forms.Button();
            this.btnSearchID = new System.Windows.Forms.Button();
            this.lblID = new System.Windows.Forms.Label();
            this.txtSearchID = new System.Windows.Forms.TextBox();
            this.btnSearchUserName = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearchUserName = new System.Windows.Forms.TextBox();
            this.btnRemoveUserName = new System.Windows.Forms.Button();
            this.btnRemoveID = new System.Windows.Forms.Button();
            this.tabMain.SuspendLayout();
            this.tabClients.SuspendLayout();
            this.tabChat.SuspendLayout();
            this.tabSearchMsg.SuspendLayout();
            this.tabUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessages)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabClients);
            this.tabMain.Controls.Add(this.tabChat);
            this.tabMain.Controls.Add(this.tabSearchMsg);
            this.tabMain.Controls.Add(this.tabUsers);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(544, 266);
            this.tabMain.TabIndex = 0;
            // 
            // tabClients
            // 
            this.tabClients.Controls.Add(this.lstClients);
            this.tabClients.Location = new System.Drawing.Point(4, 22);
            this.tabClients.Name = "tabClients";
            this.tabClients.Padding = new System.Windows.Forms.Padding(3);
            this.tabClients.Size = new System.Drawing.Size(536, 240);
            this.tabClients.TabIndex = 1;
            this.tabClients.Text = "Client Manager";
            this.tabClients.UseVisualStyleBackColor = true;
            // 
            // lstClients
            // 
            this.lstClients.Location = new System.Drawing.Point(3, 3);
            this.lstClients.Name = "lstClients";
            this.lstClients.Size = new System.Drawing.Size(530, 234);
            this.lstClients.TabIndex = 0;
            this.lstClients.UseCompatibleStateImageBehavior = false;
            this.lstClients.View = System.Windows.Forms.View.Details;
            // 
            // tabChat
            // 
            this.tabChat.Controls.Add(this.rtbChat);
            this.tabChat.Location = new System.Drawing.Point(4, 22);
            this.tabChat.Name = "tabChat";
            this.tabChat.Padding = new System.Windows.Forms.Padding(3);
            this.tabChat.Size = new System.Drawing.Size(536, 240);
            this.tabChat.TabIndex = 0;
            this.tabChat.Text = "Chat";
            this.tabChat.UseVisualStyleBackColor = true;
            // 
            // rtbChat
            // 
            this.rtbChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbChat.Location = new System.Drawing.Point(3, 3);
            this.rtbChat.Name = "rtbChat";
            this.rtbChat.ReadOnly = true;
            this.rtbChat.Size = new System.Drawing.Size(530, 234);
            this.rtbChat.TabIndex = 0;
            this.rtbChat.Text = "";
            // 
            // tabSearchMsg
            // 
            this.tabSearchMsg.Controls.Add(this.btnSearchDate);
            this.tabSearchMsg.Controls.Add(this.lblDate);
            this.tabSearchMsg.Controls.Add(this.dateTimePicker1);
            this.tabSearchMsg.Controls.Add(this.btnSearchKeyword);
            this.tabSearchMsg.Controls.Add(this.lbSearchKeyword);
            this.tabSearchMsg.Controls.Add(this.txtKeyword);
            this.tabSearchMsg.Controls.Add(this.dgvMessages);
            this.tabSearchMsg.Location = new System.Drawing.Point(4, 22);
            this.tabSearchMsg.Name = "tabSearchMsg";
            this.tabSearchMsg.Padding = new System.Windows.Forms.Padding(3);
            this.tabSearchMsg.Size = new System.Drawing.Size(536, 240);
            this.tabSearchMsg.TabIndex = 2;
            this.tabSearchMsg.Text = "Search Messages";
            this.tabSearchMsg.UseVisualStyleBackColor = true;
            // 
            // tabUsers
            // 
            this.tabUsers.Controls.Add(this.btnRemoveID);
            this.tabUsers.Controls.Add(this.btnRemoveUserName);
            this.tabUsers.Controls.Add(this.btnSearchUserName);
            this.tabUsers.Controls.Add(this.label1);
            this.tabUsers.Controls.Add(this.txtSearchUserName);
            this.tabUsers.Controls.Add(this.btnSearchID);
            this.tabUsers.Controls.Add(this.lblID);
            this.tabUsers.Controls.Add(this.txtSearchID);
            this.tabUsers.Controls.Add(this.dgvUsers);
            this.tabUsers.Location = new System.Drawing.Point(4, 22);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabUsers.Size = new System.Drawing.Size(536, 240);
            this.tabUsers.TabIndex = 3;
            this.tabUsers.Text = "Search Users";
            this.tabUsers.UseVisualStyleBackColor = true;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(17, 295);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(418, 20);
            this.txtMessage.TabIndex = 1;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(453, 293);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // dgvUsers
            // 
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Location = new System.Drawing.Point(3, 48);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.Size = new System.Drawing.Size(530, 186);
            this.dgvUsers.TabIndex = 0;
            // 
            // dgvMessages
            // 
            this.dgvMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMessages.Location = new System.Drawing.Point(3, 54);
            this.dgvMessages.Name = "dgvMessages";
            this.dgvMessages.Size = new System.Drawing.Size(530, 186);
            this.dgvMessages.TabIndex = 1;
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(58, 20);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(100, 20);
            this.txtKeyword.TabIndex = 2;
            // 
            // lbSearchKeyword
            // 
            this.lbSearchKeyword.AutoSize = true;
            this.lbSearchKeyword.Location = new System.Drawing.Point(6, 21);
            this.lbSearchKeyword.Name = "lbSearchKeyword";
            this.lbSearchKeyword.Size = new System.Drawing.Size(51, 13);
            this.lbSearchKeyword.TabIndex = 3;
            this.lbSearchKeyword.Text = "Keyword:";
            // 
            // btnSearchKeyword
            // 
            this.btnSearchKeyword.Location = new System.Drawing.Point(166, 18);
            this.btnSearchKeyword.Name = "btnSearchKeyword";
            this.btnSearchKeyword.Size = new System.Drawing.Size(79, 23);
            this.btnSearchKeyword.TabIndex = 4;
            this.btnSearchKeyword.Text = "Word Search";
            this.btnSearchKeyword.UseVisualStyleBackColor = true;
            this.btnSearchKeyword.Click += new System.EventHandler(this.btnSearchKeyword_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(356, 20);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(79, 20);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(323, 23);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(33, 13);
            this.lblDate.TabIndex = 6;
            this.lblDate.Text = "Date:";
            // 
            // btnSearchDate
            // 
            this.btnSearchDate.Location = new System.Drawing.Point(445, 18);
            this.btnSearchDate.Name = "btnSearchDate";
            this.btnSearchDate.Size = new System.Drawing.Size(79, 23);
            this.btnSearchDate.TabIndex = 8;
            this.btnSearchDate.Text = "Date Search";
            this.btnSearchDate.UseVisualStyleBackColor = true;
            this.btnSearchDate.Click += new System.EventHandler(this.btnSearchDate_Click);
            // 
            // btnSearchID
            // 
            this.btnSearchID.Location = new System.Drawing.Point(88, 13);
            this.btnSearchID.Name = "btnSearchID";
            this.btnSearchID.Size = new System.Drawing.Size(50, 23);
            this.btnSearchID.TabIndex = 7;
            this.btnSearchID.Text = "Search";
            this.btnSearchID.UseVisualStyleBackColor = true;
            this.btnSearchID.Click += new System.EventHandler(this.btnSearchID_Click);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(10, 18);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(21, 13);
            this.lblID.TabIndex = 6;
            this.lblID.Text = "ID:";
            // 
            // txtSearchID
            // 
            this.txtSearchID.Location = new System.Drawing.Point(33, 14);
            this.txtSearchID.Name = "txtSearchID";
            this.txtSearchID.Size = new System.Drawing.Size(49, 20);
            this.txtSearchID.TabIndex = 5;
            // 
            // btnSearchUserName
            // 
            this.btnSearchUserName.Location = new System.Drawing.Point(411, 13);
            this.btnSearchUserName.Name = "btnSearchUserName";
            this.btnSearchUserName.Size = new System.Drawing.Size(49, 23);
            this.btnSearchUserName.TabIndex = 10;
            this.btnSearchUserName.Text = "Search";
            this.btnSearchUserName.UseVisualStyleBackColor = true;
            this.btnSearchUserName.Click += new System.EventHandler(this.btnSearchUserName_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(236, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "User Name:";
            // 
            // txtSearchUserName
            // 
            this.txtSearchUserName.Location = new System.Drawing.Point(305, 14);
            this.txtSearchUserName.Name = "txtSearchUserName";
            this.txtSearchUserName.Size = new System.Drawing.Size(100, 20);
            this.txtSearchUserName.TabIndex = 8;
            // 
            // btnRemoveUserName
            // 
            this.btnRemoveUserName.Location = new System.Drawing.Point(466, 13);
            this.btnRemoveUserName.Name = "btnRemoveUserName";
            this.btnRemoveUserName.Size = new System.Drawing.Size(58, 23);
            this.btnRemoveUserName.TabIndex = 11;
            this.btnRemoveUserName.Text = "Remove";
            this.btnRemoveUserName.UseVisualStyleBackColor = true;
            this.btnRemoveUserName.Click += new System.EventHandler(this.btnRemoveUserName_Click);
            // 
            // btnRemoveID
            // 
            this.btnRemoveID.Location = new System.Drawing.Point(144, 13);
            this.btnRemoveID.Name = "btnRemoveID";
            this.btnRemoveID.Size = new System.Drawing.Size(58, 23);
            this.btnRemoveID.TabIndex = 12;
            this.btnRemoveID.Text = "Remove";
            this.btnRemoveID.UseVisualStyleBackColor = true;
            this.btnRemoveID.Click += new System.EventHandler(this.btnRemoveID_Click);
            // 
            // ServerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 337);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.tabMain);
            this.Name = "ServerControl";
            this.Text = "Server Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerControl_FormClosing);
            this.tabMain.ResumeLayout(false);
            this.tabClients.ResumeLayout(false);
            this.tabChat.ResumeLayout(false);
            this.tabSearchMsg.ResumeLayout(false);
            this.tabSearchMsg.PerformLayout();
            this.tabUsers.ResumeLayout(false);
            this.tabUsers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabClients;
        private System.Windows.Forms.ListView lstClients;
        private System.Windows.Forms.TabPage tabChat;
        private System.Windows.Forms.RichTextBox rtbChat;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TabPage tabSearchMsg;
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.Button btnSearchDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnSearchKeyword;
        private System.Windows.Forms.Label lbSearchKeyword;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.DataGridView dgvMessages;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.Button btnRemoveID;
        private System.Windows.Forms.Button btnRemoveUserName;
        private System.Windows.Forms.Button btnSearchUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearchUserName;
        private System.Windows.Forms.Button btnSearchID;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtSearchID;
    }
}