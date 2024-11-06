
namespace WPCAA.WinApp.Forms
{
    partial class LoginForm
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
            txtUsername = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            txtPassword = new System.Windows.Forms.TextBox();
            btnLogin = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            panel2 = new System.Windows.Forms.Panel();
            btnClose = new System.Windows.Forms.Button();
            label5 = new System.Windows.Forms.Label();
            linkPrivacyPolicy = new System.Windows.Forms.LinkLabel();
            label4 = new System.Windows.Forms.Label();
            panelLoading = new System.Windows.Forms.Panel();
            label3 = new System.Windows.Forms.Label();
            linkTermsAndAgreements = new System.Windows.Forms.LinkLabel();
            checkBoxTermsAndConditions = new System.Windows.Forms.CheckBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panelLoading.SuspendLayout();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtUsername.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtUsername.Location = new System.Drawing.Point(51, 139);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new System.Drawing.Size(312, 29);
            txtUsername.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI Variable Display", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(51, 121);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(59, 16);
            label1.TabIndex = 1;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI Variable Display", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label2.Location = new System.Drawing.Point(51, 185);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(57, 16);
            label2.TabIndex = 3;
            label2.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtPassword.Location = new System.Drawing.Point(51, 203);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new System.Drawing.Size(312, 29);
            txtPassword.TabIndex = 2;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = System.Drawing.Color.FromArgb(46, 64, 81);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLogin.Font = new System.Drawing.Font("Segoe UI Variable Display Semib", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnLogin.ForeColor = System.Drawing.Color.White;
            btnLogin.Location = new System.Drawing.Point(51, 308);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new System.Drawing.Size(312, 39);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "LOG IN";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.White;
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(linkPrivacyPolicy);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(panelLoading);
            panel1.Controls.Add(linkTermsAndAgreements);
            panel1.Controls.Add(checkBoxTermsAndConditions);
            panel1.Controls.Add(txtPassword);
            panel1.Controls.Add(txtUsername);
            panel1.Controls.Add(btnLogin);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(418, 387);
            panel1.TabIndex = 7;
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.Color.FromArgb(46, 64, 81);
            panel2.Controls.Add(btnClose);
            panel2.Dock = System.Windows.Forms.DockStyle.Top;
            panel2.Location = new System.Drawing.Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(418, 28);
            panel2.TabIndex = 44;
            // 
            // btnClose
            // 
            btnClose.BackColor = System.Drawing.Color.FromArgb(214, 50, 48);
            btnClose.BackgroundImage = Properties.Resources.close1;
            btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClose.ForeColor = System.Drawing.Color.White;
            btnClose.Location = new System.Drawing.Point(389, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(29, 28);
            btnClose.TabIndex = 0;
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI Variable Display", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label5.ForeColor = System.Drawing.Color.FromArgb(46, 64, 81);
            label5.Location = new System.Drawing.Point(51, 56);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(316, 36);
            label5.TabIndex = 10;
            label5.Text = "Log In to AMETUS WMS";
            // 
            // linkPrivacyPolicy
            // 
            linkPrivacyPolicy.AutoSize = true;
            linkPrivacyPolicy.Font = new System.Drawing.Font("Segoe UI Variable Display", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            linkPrivacyPolicy.Location = new System.Drawing.Point(141, 275);
            linkPrivacyPolicy.Name = "linkPrivacyPolicy";
            linkPrivacyPolicy.Size = new System.Drawing.Size(83, 16);
            linkPrivacyPolicy.TabIndex = 9;
            linkPrivacyPolicy.TabStop = true;
            linkPrivacyPolicy.Text = "Privacy Policy.";
            linkPrivacyPolicy.LinkClicked += linkPrivacyPolicy_LinkClicked;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI Variable Display", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label4.Location = new System.Drawing.Point(116, 275);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(27, 16);
            label4.TabIndex = 8;
            label4.Text = "and";
            // 
            // panelLoading
            // 
            panelLoading.BackColor = System.Drawing.Color.FromArgb(214, 50, 48);
            panelLoading.Controls.Add(label3);
            panelLoading.ForeColor = System.Drawing.Color.White;
            panelLoading.Location = new System.Drawing.Point(75, 156);
            panelLoading.Name = "panelLoading";
            panelLoading.Size = new System.Drawing.Size(274, 55);
            panelLoading.TabIndex = 7;
            panelLoading.Visible = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label3.Location = new System.Drawing.Point(74, 16);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(115, 21);
            label3.TabIndex = 0;
            label3.Text = "LOGGING IN...";
            // 
            // linkTermsAndAgreements
            // 
            linkTermsAndAgreements.AutoSize = true;
            linkTermsAndAgreements.Location = new System.Drawing.Point(183, 257);
            linkTermsAndAgreements.Name = "linkTermsAndAgreements";
            linkTermsAndAgreements.Size = new System.Drawing.Size(122, 15);
            linkTermsAndAgreements.TabIndex = 6;
            linkTermsAndAgreements.TabStop = true;
            linkTermsAndAgreements.Text = "Terms and Conditions";
            linkTermsAndAgreements.LinkClicked += linkTermsAndAgreements_LinkClicked;
            // 
            // checkBoxTermsAndConditions
            // 
            checkBoxTermsAndConditions.Font = new System.Drawing.Font("Segoe UI Variable Display", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxTermsAndConditions.Location = new System.Drawing.Point(100, 238);
            checkBoxTermsAndConditions.Name = "checkBoxTermsAndConditions";
            checkBoxTermsAndConditions.Size = new System.Drawing.Size(216, 40);
            checkBoxTermsAndConditions.TabIndex = 5;
            checkBoxTermsAndConditions.Text = "I acknowledge that I have read and agree to the ";
            checkBoxTermsAndConditions.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ControlLight;
            ClientSize = new System.Drawing.Size(418, 387);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "WPCAA";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panelLoading.ResumeLayout(false);
            panelLoading.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxTermsAndConditions;
        private System.Windows.Forms.LinkLabel linkTermsAndAgreements;
        private System.Windows.Forms.Panel panelLoading;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkPrivacyPolicy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClose;
    }
}