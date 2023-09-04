namespace KazganYerIstasyonu
{
    partial class GirisForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GirisForm));
            this.ustPanel = new System.Windows.Forms.Panel();
            this.lblAnamenu = new System.Windows.Forms.Label();
            this.btnkapat = new System.Windows.Forms.Button();
            this.solpanel = new System.Windows.Forms.Panel();
            this.altpanel = new System.Windows.Forms.Panel();
            this.sagPanel = new System.Windows.Forms.Panel();
            this.KullanicitextBox = new System.Windows.Forms.TextBox();
            this.sifretextBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.kaydet_button = new System.Windows.Forms.Button();
            this.ustPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ustPanel
            // 
            this.ustPanel.Controls.Add(this.lblAnamenu);
            this.ustPanel.Controls.Add(this.btnkapat);
            this.ustPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ustPanel.Location = new System.Drawing.Point(0, 0);
            this.ustPanel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.ustPanel.Name = "ustPanel";
            this.ustPanel.Size = new System.Drawing.Size(704, 55);
            this.ustPanel.TabIndex = 0;
            // 
            // lblAnamenu
            // 
            this.lblAnamenu.AutoSize = true;
            this.lblAnamenu.Location = new System.Drawing.Point(215, 17);
            this.lblAnamenu.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblAnamenu.Name = "lblAnamenu";
            this.lblAnamenu.Size = new System.Drawing.Size(252, 22);
            this.lblAnamenu.TabIndex = 2;
            this.lblAnamenu.Text = "Kazgan Yer İstasyonu Giriş";
            // 
            // btnkapat
            // 
            this.btnkapat.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnkapat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnkapat.Location = new System.Drawing.Point(649, 0);
            this.btnkapat.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnkapat.Name = "btnkapat";
            this.btnkapat.Size = new System.Drawing.Size(55, 55);
            this.btnkapat.TabIndex = 0;
            this.btnkapat.Text = "X";
            this.btnkapat.UseVisualStyleBackColor = true;
            this.btnkapat.Click += new System.EventHandler(this.btnkapat_Click);
            // 
            // solpanel
            // 
            this.solpanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.solpanel.Location = new System.Drawing.Point(0, 55);
            this.solpanel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.solpanel.Name = "solpanel";
            this.solpanel.Size = new System.Drawing.Size(10, 602);
            this.solpanel.TabIndex = 0;
            // 
            // altpanel
            // 
            this.altpanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.altpanel.Location = new System.Drawing.Point(10, 647);
            this.altpanel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.altpanel.Name = "altpanel";
            this.altpanel.Size = new System.Drawing.Size(684, 10);
            this.altpanel.TabIndex = 1;
            // 
            // sagPanel
            // 
            this.sagPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.sagPanel.Location = new System.Drawing.Point(694, 55);
            this.sagPanel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.sagPanel.Name = "sagPanel";
            this.sagPanel.Size = new System.Drawing.Size(10, 602);
            this.sagPanel.TabIndex = 1;
            // 
            // KullanicitextBox
            // 
            this.KullanicitextBox.Location = new System.Drawing.Point(293, 490);
            this.KullanicitextBox.Name = "KullanicitextBox";
            this.KullanicitextBox.Size = new System.Drawing.Size(174, 27);
            this.KullanicitextBox.TabIndex = 3;
            // 
            // sifretextBox
            // 
            this.sifretextBox.Location = new System.Drawing.Point(293, 535);
            this.sifretextBox.Name = "sifretextBox";
            this.sifretextBox.Size = new System.Drawing.Size(174, 27);
            this.sifretextBox.TabIndex = 3;
            this.sifretextBox.UseSystemPasswordChar = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::KazganYerIstasyonu.Properties.Resources.TurkiyeKazgan301;
            this.pictureBox1.Location = new System.Drawing.Point(126, 63);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(406, 385);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(137, 493);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 22);
            this.label1.TabIndex = 5;
            this.label1.Text = "Kullanıcı Adı";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(137, 538);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 22);
            this.label2.TabIndex = 5;
            this.label2.Text = "Şifre";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // kaydet_button
            // 
            this.kaydet_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kaydet_button.Location = new System.Drawing.Point(502, 490);
            this.kaydet_button.Name = "kaydet_button";
            this.kaydet_button.Size = new System.Drawing.Size(90, 70);
            this.kaydet_button.TabIndex = 6;
            this.kaydet_button.Text = "Giriş";
            this.kaydet_button.UseVisualStyleBackColor = true;
            this.kaydet_button.Click += new System.EventHandler(this.kaydet_button_Click);
            // 
            // GirisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 657);
            this.Controls.Add(this.kaydet_button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.sifretextBox);
            this.Controls.Add(this.KullanicitextBox);
            this.Controls.Add(this.altpanel);
            this.Controls.Add(this.sagPanel);
            this.Controls.Add(this.solpanel);
            this.Controls.Add(this.ustPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "GirisForm";
            this.Text = "GirisForm";
            this.Load += new System.EventHandler(this.GirisForm_Load);
            this.ustPanel.ResumeLayout(false);
            this.ustPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel ustPanel;
        private System.Windows.Forms.Button btnkapat;
        private System.Windows.Forms.Panel solpanel;
        private System.Windows.Forms.Panel altpanel;
        private System.Windows.Forms.Panel sagPanel;
        private System.Windows.Forms.Label lblAnamenu;
        private System.Windows.Forms.TextBox KullanicitextBox;
        private System.Windows.Forms.TextBox sifretextBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button kaydet_button;
    }
}