namespace AudioPin
{
    partial class DeviceSelect
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
            DeviceDropDown = new ComboBox();
            OK = new Button();
            Cancel = new Button();
            CommCheck = new CheckBox();
            SuspendLayout();
            // 
            // DeviceDropDown
            // 
            DeviceDropDown.FormattingEnabled = true;
            DeviceDropDown.Location = new Point(12, 12);
            DeviceDropDown.Name = "DeviceDropDown";
            DeviceDropDown.Size = new Size(365, 23);
            DeviceDropDown.TabIndex = 0;
            DeviceDropDown.SelectedIndexChanged += DeviceDropDown_SelectedIndexChanged;
            // 
            // OKButton
            // 
            OK.Location = new Point(221, 41);
            OK.Name = "OKButton";
            OK.Size = new Size(75, 23);
            OK.TabIndex = 1;
            OK.Text = "OK";
            OK.UseVisualStyleBackColor = true;
            OK.Click += OKButton_Click;
            // 
            // CancelButton
            // 
            Cancel.Location = new Point(302, 41);
            Cancel.Name = "CancelButton";
            Cancel.Size = new Size(75, 23);
            Cancel.TabIndex = 2;
            Cancel.Text = "Cancel";
            Cancel.UseVisualStyleBackColor = true;
            Cancel.Click += CancelButton_Click;
            // 
            // CommCheck
            // 
            CommCheck.AutoSize = true;
            CommCheck.Location = new Point(12, 44);
            CommCheck.Name = "CommCheck";
            CommCheck.Size = new Size(104, 19);
            CommCheck.TabIndex = 3;
            CommCheck.Text = "Comm. Device";
            CommCheck.UseVisualStyleBackColor = true;
            // 
            // DeviceSelect
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(389, 73);
            Controls.Add(CommCheck);
            Controls.Add(Cancel);
            Controls.Add(OK);
            Controls.Add(DeviceDropDown);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "DeviceSelect";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Device";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox DeviceDropDown;
        private Button OK;
        private Button Cancel;
        private CheckBox CommCheck;
    }
}