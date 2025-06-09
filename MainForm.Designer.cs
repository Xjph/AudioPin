namespace AudioPin
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TrayIcon = new NotifyIcon(components);
            MainLayoutPanel = new TableLayoutPanel();
            OutButtonLayoutPanel = new TableLayoutPanel();
            OutMinusButton = new Button();
            OutPlusButton = new Button();
            OutDownButton = new Button();
            OutUpButton = new Button();
            OutCommCheck = new CheckBox();
            InButtonLayoutPanel = new TableLayoutPanel();
            InMinusButton = new Button();
            InPlusButton = new Button();
            InDownButton = new Button();
            InUpButton = new Button();
            InCommCheck = new CheckBox();
            OutListLayoutPanel = new TableLayoutPanel();
            OutList = new ListBox();
            OutCommList = new ListBox();
            OutLabelLayoutPanel = new TableLayoutPanel();
            OutLabel = new Label();
            OutCommLabel = new Label();
            InListLayoutPanel = new TableLayoutPanel();
            InList = new ListBox();
            InCommList = new ListBox();
            InLabelLayoutPanel = new TableLayoutPanel();
            InLabel = new Label();
            InCommLabel = new Label();
            AutoLaunchCheckbox = new CheckBox();
            GithubLink = new LinkLabel();
            DonateLink = new LinkLabel();
            linkLabel1 = new LinkLabel();
            MainLayoutPanel.SuspendLayout();
            OutButtonLayoutPanel.SuspendLayout();
            InButtonLayoutPanel.SuspendLayout();
            OutListLayoutPanel.SuspendLayout();
            OutLabelLayoutPanel.SuspendLayout();
            InListLayoutPanel.SuspendLayout();
            InLabelLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // TrayIcon
            // 
            TrayIcon.Text = "AudioPin";
            TrayIcon.Visible = true;
            TrayIcon.MouseClick += TrayIcon_MouseClick;
            TrayIcon.MouseDoubleClick += TrayIcon_MouseDoubleClick;
            // 
            // MainLayoutPanel
            // 
            MainLayoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MainLayoutPanel.ColumnCount = 1;
            MainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            MainLayoutPanel.Controls.Add(OutButtonLayoutPanel, 0, 2);
            MainLayoutPanel.Controls.Add(InButtonLayoutPanel, 0, 5);
            MainLayoutPanel.Controls.Add(OutListLayoutPanel, 0, 1);
            MainLayoutPanel.Controls.Add(OutLabelLayoutPanel, 0, 0);
            MainLayoutPanel.Controls.Add(InListLayoutPanel, 0, 4);
            MainLayoutPanel.Controls.Add(InLabelLayoutPanel, 0, 3);
            MainLayoutPanel.Location = new Point(12, 12);
            MainLayoutPanel.Name = "MainLayoutPanel";
            MainLayoutPanel.RowCount = 6;
            MainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            MainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            MainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            MainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            MainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            MainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            MainLayoutPanel.Size = new Size(671, 504);
            MainLayoutPanel.TabIndex = 0;
            // 
            // OutButtonLayoutPanel
            // 
            OutButtonLayoutPanel.ColumnCount = 6;
            OutButtonLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            OutButtonLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            OutButtonLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            OutButtonLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            OutButtonLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            OutButtonLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            OutButtonLayoutPanel.Controls.Add(OutMinusButton, 5, 0);
            OutButtonLayoutPanel.Controls.Add(OutPlusButton, 4, 0);
            OutButtonLayoutPanel.Controls.Add(OutDownButton, 3, 0);
            OutButtonLayoutPanel.Controls.Add(OutUpButton, 2, 0);
            OutButtonLayoutPanel.Controls.Add(OutCommCheck, 0, 0);
            OutButtonLayoutPanel.Dock = DockStyle.Fill;
            OutButtonLayoutPanel.Location = new Point(3, 215);
            OutButtonLayoutPanel.Name = "OutButtonLayoutPanel";
            OutButtonLayoutPanel.RowCount = 1;
            OutButtonLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            OutButtonLayoutPanel.Size = new Size(665, 34);
            OutButtonLayoutPanel.TabIndex = 4;
            // 
            // OutMinusButton
            // 
            OutMinusButton.Dock = DockStyle.Fill;
            OutMinusButton.Enabled = false;
            OutMinusButton.Font = new Font("Segoe UI", 9.75F);
            OutMinusButton.Location = new Point(628, 3);
            OutMinusButton.Name = "OutMinusButton";
            OutMinusButton.Size = new Size(34, 28);
            OutMinusButton.TabIndex = 4;
            OutMinusButton.Text = "-";
            OutMinusButton.UseVisualStyleBackColor = true;
            OutMinusButton.Click += OutMinusButton_Click;
            // 
            // OutPlusButton
            // 
            OutPlusButton.Dock = DockStyle.Fill;
            OutPlusButton.Font = new Font("Segoe UI", 9.75F);
            OutPlusButton.Location = new Point(588, 3);
            OutPlusButton.Name = "OutPlusButton";
            OutPlusButton.Size = new Size(34, 28);
            OutPlusButton.TabIndex = 3;
            OutPlusButton.Text = "+";
            OutPlusButton.UseVisualStyleBackColor = true;
            OutPlusButton.Click += OutPlusButton_Click;
            // 
            // OutDownButton
            // 
            OutDownButton.Dock = DockStyle.Fill;
            OutDownButton.Enabled = false;
            OutDownButton.Font = new Font("Segoe UI", 9.75F);
            OutDownButton.Location = new Point(548, 3);
            OutDownButton.Name = "OutDownButton";
            OutDownButton.Size = new Size(34, 28);
            OutDownButton.TabIndex = 2;
            OutDownButton.Text = "↓";
            OutDownButton.UseVisualStyleBackColor = true;
            OutDownButton.Click += OutDownButton_Click;
            // 
            // OutUpButton
            // 
            OutUpButton.Dock = DockStyle.Fill;
            OutUpButton.Enabled = false;
            OutUpButton.Font = new Font("Segoe UI", 9.75F);
            OutUpButton.Location = new Point(508, 3);
            OutUpButton.Name = "OutUpButton";
            OutUpButton.Size = new Size(34, 28);
            OutUpButton.TabIndex = 1;
            OutUpButton.Text = "↑";
            OutUpButton.UseVisualStyleBackColor = true;
            OutUpButton.Click += OutUpButton_Click;
            // 
            // OutCommCheck
            // 
            OutCommCheck.AutoSize = true;
            OutCommCheck.Dock = DockStyle.Top;
            OutCommCheck.Location = new Point(3, 3);
            OutCommCheck.Name = "OutCommCheck";
            OutCommCheck.Size = new Size(154, 19);
            OutCommCheck.TabIndex = 5;
            OutCommCheck.Text = "Different Comm. Device";
            OutCommCheck.UseVisualStyleBackColor = true;
            OutCommCheck.CheckedChanged += OutCommCheck_CheckedChanged;
            // 
            // InButtonLayoutPanel
            // 
            InButtonLayoutPanel.ColumnCount = 6;
            InButtonLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            InButtonLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            InButtonLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            InButtonLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            InButtonLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            InButtonLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            InButtonLayoutPanel.Controls.Add(InMinusButton, 5, 0);
            InButtonLayoutPanel.Controls.Add(InPlusButton, 4, 0);
            InButtonLayoutPanel.Controls.Add(InDownButton, 3, 0);
            InButtonLayoutPanel.Controls.Add(InUpButton, 2, 0);
            InButtonLayoutPanel.Controls.Add(InCommCheck, 0, 0);
            InButtonLayoutPanel.Dock = DockStyle.Fill;
            InButtonLayoutPanel.Location = new Point(3, 467);
            InButtonLayoutPanel.Name = "InButtonLayoutPanel";
            InButtonLayoutPanel.RowCount = 1;
            InButtonLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            InButtonLayoutPanel.Size = new Size(665, 34);
            InButtonLayoutPanel.TabIndex = 5;
            // 
            // InMinusButton
            // 
            InMinusButton.Dock = DockStyle.Fill;
            InMinusButton.Enabled = false;
            InMinusButton.Font = new Font("Segoe UI", 9.75F);
            InMinusButton.Location = new Point(628, 3);
            InMinusButton.Name = "InMinusButton";
            InMinusButton.Size = new Size(34, 28);
            InMinusButton.TabIndex = 4;
            InMinusButton.Text = "-";
            InMinusButton.UseVisualStyleBackColor = true;
            InMinusButton.Click += InMinusButton_Click;
            // 
            // InPlusButton
            // 
            InPlusButton.Dock = DockStyle.Fill;
            InPlusButton.Font = new Font("Segoe UI", 9.75F);
            InPlusButton.Location = new Point(588, 3);
            InPlusButton.Name = "InPlusButton";
            InPlusButton.Size = new Size(34, 28);
            InPlusButton.TabIndex = 3;
            InPlusButton.Text = "+";
            InPlusButton.UseVisualStyleBackColor = true;
            InPlusButton.Click += InPlusButton_Click;
            // 
            // InDownButton
            // 
            InDownButton.Dock = DockStyle.Fill;
            InDownButton.Enabled = false;
            InDownButton.Font = new Font("Segoe UI", 9.75F);
            InDownButton.Location = new Point(548, 3);
            InDownButton.Name = "InDownButton";
            InDownButton.Size = new Size(34, 28);
            InDownButton.TabIndex = 2;
            InDownButton.Text = "↓";
            InDownButton.UseVisualStyleBackColor = true;
            InDownButton.Click += InDownButton_Click;
            // 
            // InUpButton
            // 
            InUpButton.Dock = DockStyle.Fill;
            InUpButton.Enabled = false;
            InUpButton.Font = new Font("Segoe UI", 9.75F);
            InUpButton.Location = new Point(508, 3);
            InUpButton.Name = "InUpButton";
            InUpButton.Size = new Size(34, 28);
            InUpButton.TabIndex = 1;
            InUpButton.Text = "↑";
            InUpButton.UseVisualStyleBackColor = true;
            InUpButton.Click += InUpButton_Click;
            // 
            // InCommCheck
            // 
            InCommCheck.AutoSize = true;
            InCommCheck.Dock = DockStyle.Top;
            InCommCheck.Location = new Point(3, 3);
            InCommCheck.Name = "InCommCheck";
            InCommCheck.Size = new Size(154, 19);
            InCommCheck.TabIndex = 5;
            InCommCheck.Text = "Different Comm. Device";
            InCommCheck.UseVisualStyleBackColor = true;
            InCommCheck.CheckedChanged += InCommCheck_CheckedChanged;
            // 
            // OutListLayoutPanel
            // 
            OutListLayoutPanel.ColumnCount = 2;
            OutListLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            OutListLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            OutListLayoutPanel.Controls.Add(OutList, 0, 0);
            OutListLayoutPanel.Controls.Add(OutCommList, 1, 0);
            OutListLayoutPanel.Dock = DockStyle.Fill;
            OutListLayoutPanel.Location = new Point(3, 23);
            OutListLayoutPanel.Name = "OutListLayoutPanel";
            OutListLayoutPanel.RowCount = 1;
            OutListLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            OutListLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            OutListLayoutPanel.Size = new Size(665, 186);
            OutListLayoutPanel.TabIndex = 6;
            // 
            // OutList
            // 
            OutList.Dock = DockStyle.Fill;
            OutList.FormattingEnabled = true;
            OutList.ItemHeight = 15;
            OutList.Location = new Point(3, 3);
            OutList.Name = "OutList";
            OutList.Size = new Size(326, 180);
            OutList.TabIndex = 2;
            OutList.SelectedIndexChanged += OutList_SelectedIndexChanged;
            // 
            // OutCommList
            // 
            OutCommList.Dock = DockStyle.Fill;
            OutCommList.FormattingEnabled = true;
            OutCommList.ItemHeight = 15;
            OutCommList.Location = new Point(335, 3);
            OutCommList.Name = "OutCommList";
            OutCommList.Size = new Size(327, 180);
            OutCommList.TabIndex = 3;
            OutCommList.SelectedIndexChanged += OutCommList_SelectedIndexChanged;
            // 
            // OutLabelLayoutPanel
            // 
            OutLabelLayoutPanel.ColumnCount = 2;
            OutLabelLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            OutLabelLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            OutLabelLayoutPanel.Controls.Add(OutLabel, 0, 0);
            OutLabelLayoutPanel.Controls.Add(OutCommLabel, 1, 0);
            OutLabelLayoutPanel.Dock = DockStyle.Fill;
            OutLabelLayoutPanel.Location = new Point(3, 3);
            OutLabelLayoutPanel.Name = "OutLabelLayoutPanel";
            OutLabelLayoutPanel.RowCount = 1;
            OutLabelLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            OutLabelLayoutPanel.Size = new Size(665, 14);
            OutLabelLayoutPanel.TabIndex = 7;
            // 
            // OutLabel
            // 
            OutLabel.AutoSize = true;
            OutLabel.Dock = DockStyle.Fill;
            OutLabel.Location = new Point(3, 0);
            OutLabel.Name = "OutLabel";
            OutLabel.Size = new Size(326, 14);
            OutLabel.TabIndex = 0;
            OutLabel.Text = "Pinned Output Devices";
            // 
            // OutCommLabel
            // 
            OutCommLabel.AutoSize = true;
            OutCommLabel.Dock = DockStyle.Fill;
            OutCommLabel.Location = new Point(335, 0);
            OutCommLabel.Name = "OutCommLabel";
            OutCommLabel.Size = new Size(327, 14);
            OutCommLabel.TabIndex = 1;
            OutCommLabel.Text = "Pinned Output Comm. Devices";
            // 
            // InListLayoutPanel
            // 
            InListLayoutPanel.ColumnCount = 2;
            InListLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            InListLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            InListLayoutPanel.Controls.Add(InList, 0, 0);
            InListLayoutPanel.Controls.Add(InCommList, 1, 0);
            InListLayoutPanel.Dock = DockStyle.Fill;
            InListLayoutPanel.Location = new Point(3, 275);
            InListLayoutPanel.Name = "InListLayoutPanel";
            InListLayoutPanel.RowCount = 1;
            InListLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            InListLayoutPanel.Size = new Size(665, 186);
            InListLayoutPanel.TabIndex = 8;
            // 
            // InList
            // 
            InList.Dock = DockStyle.Fill;
            InList.FormattingEnabled = true;
            InList.ItemHeight = 15;
            InList.Location = new Point(3, 3);
            InList.Name = "InList";
            InList.Size = new Size(326, 180);
            InList.TabIndex = 3;
            InList.SelectedIndexChanged += InList_SelectedIndexChanged;
            // 
            // InCommList
            // 
            InCommList.Dock = DockStyle.Fill;
            InCommList.FormattingEnabled = true;
            InCommList.ItemHeight = 15;
            InCommList.Location = new Point(335, 3);
            InCommList.Name = "InCommList";
            InCommList.Size = new Size(327, 180);
            InCommList.TabIndex = 4;
            InCommList.SelectedIndexChanged += InCommList_SelectedIndexChanged;
            // 
            // InLabelLayoutPanel
            // 
            InLabelLayoutPanel.ColumnCount = 2;
            InLabelLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            InLabelLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            InLabelLayoutPanel.Controls.Add(InLabel, 0, 0);
            InLabelLayoutPanel.Controls.Add(InCommLabel, 1, 0);
            InLabelLayoutPanel.Dock = DockStyle.Fill;
            InLabelLayoutPanel.Location = new Point(3, 255);
            InLabelLayoutPanel.Name = "InLabelLayoutPanel";
            InLabelLayoutPanel.RowCount = 1;
            InLabelLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            InLabelLayoutPanel.Size = new Size(665, 14);
            InLabelLayoutPanel.TabIndex = 9;
            // 
            // InLabel
            // 
            InLabel.AutoSize = true;
            InLabel.Dock = DockStyle.Fill;
            InLabel.Location = new Point(3, 0);
            InLabel.Name = "InLabel";
            InLabel.Size = new Size(326, 14);
            InLabel.TabIndex = 1;
            InLabel.Text = "Pinned Input Devices";
            // 
            // InCommLabel
            // 
            InCommLabel.AutoSize = true;
            InCommLabel.Location = new Point(335, 0);
            InCommLabel.Name = "InCommLabel";
            InCommLabel.Size = new Size(161, 14);
            InCommLabel.TabIndex = 2;
            InCommLabel.Text = "Pinned Input Comm. Devices";
            // 
            // AutoLaunchCheckbox
            // 
            AutoLaunchCheckbox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            AutoLaunchCheckbox.AutoSize = true;
            AutoLaunchCheckbox.Location = new Point(18, 528);
            AutoLaunchCheckbox.Name = "AutoLaunchCheckbox";
            AutoLaunchCheckbox.Size = new Size(123, 19);
            AutoLaunchCheckbox.TabIndex = 1;
            AutoLaunchCheckbox.Text = "Launch on Startup";
            AutoLaunchCheckbox.UseVisualStyleBackColor = true;
            AutoLaunchCheckbox.CheckedChanged += AutoLaunchCheckbox_CheckedChanged;
            // 
            // GithubLink
            // 
            GithubLink.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            GithubLink.AutoSize = true;
            GithubLink.Location = new Point(635, 529);
            GithubLink.Name = "GithubLink";
            GithubLink.Size = new Size(42, 15);
            GithubLink.TabIndex = 2;
            GithubLink.TabStop = true;
            GithubLink.Text = "github";
            GithubLink.LinkClicked += GithubLink_LinkClicked;
            // 
            // DonateLink
            // 
            DonateLink.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            DonateLink.AutoSize = true;
            DonateLink.Location = new Point(584, 529);
            DonateLink.Name = "DonateLink";
            DonateLink.Size = new Size(45, 15);
            DonateLink.TabIndex = 3;
            DonateLink.TabStop = true;
            DonateLink.Text = "Donate";
            DonateLink.LinkClicked += DonateLink_LinkClicked;
            // 
            // linkLabel1
            // 
            linkLabel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel1.Location = new Point(451, 525);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(127, 21);
            linkLabel1.TabIndex = 4;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Update Available";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(695, 559);
            Controls.Add(linkLabel1);
            Controls.Add(DonateLink);
            Controls.Add(GithubLink);
            Controls.Add(AutoLaunchCheckbox);
            Controls.Add(MainLayoutPanel);
            MinimumSize = new Size(420, 350);
            Name = "MainForm";
            Text = "Audio Pin";
            Resize += MainForm_Resize;
            MainLayoutPanel.ResumeLayout(false);
            OutButtonLayoutPanel.ResumeLayout(false);
            OutButtonLayoutPanel.PerformLayout();
            InButtonLayoutPanel.ResumeLayout(false);
            InButtonLayoutPanel.PerformLayout();
            OutListLayoutPanel.ResumeLayout(false);
            OutLabelLayoutPanel.ResumeLayout(false);
            OutLabelLayoutPanel.PerformLayout();
            InListLayoutPanel.ResumeLayout(false);
            InLabelLayoutPanel.ResumeLayout(false);
            InLabelLayoutPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NotifyIcon TrayIcon;
        private TableLayoutPanel MainLayoutPanel;
        private Label OutLabel;
        private Label InLabel;
        private ListBox OutList;
        private ListBox InList;
        private TableLayoutPanel OutButtonLayoutPanel;
        private TableLayoutPanel InButtonLayoutPanel;
        private Button OutUpButton;
        private Button OutDownButton;
        private Button OutPlusButton;
        private Button InUpButton;
        private Button InDownButton;
        private Button InPlusButton;
        private Button OutMinusButton;
        private Button InMinusButton;
        private CheckBox OutCommCheck;
        private CheckBox InCommCheck;
        private TableLayoutPanel OutListLayoutPanel;
        private ListBox OutCommList;
        private TableLayoutPanel OutLabelLayoutPanel;
        private Label OutCommLabel;
        private TableLayoutPanel InListLayoutPanel;
        private ListBox InCommList;
        private TableLayoutPanel InLabelLayoutPanel;
        private Label InCommLabel;
        private CheckBox AutoLaunchCheckbox;
        private LinkLabel GithubLink;
        private LinkLabel DonateLink;
        private LinkLabel linkLabel1;
    }
}
