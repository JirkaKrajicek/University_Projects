namespace DSA_Krajicek_1
{
    partial class Form1
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
            this.button_save_text = new System.Windows.Forms.Button();
            this.button_hash = new System.Windows.Forms.Button();
            this.button_zip = new System.Windows.Forms.Button();
            this.button_unzip = new System.Windows.Forms.Button();
            this.textBox_text = new System.Windows.Forms.TextBox();
            this.textBox_sha256 = new System.Windows.Forms.TextBox();
            this.textBox_hashCheck = new System.Windows.Forms.TextBox();
            this.button_choose_pub_key = new System.Windows.Forms.Button();
            this.button_choose_encr_hash = new System.Windows.Forms.Button();
            this.textBox_delivery = new System.Windows.Forms.TextBox();
            this.button_show_deliv_hash = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox_saveKeys = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button_save_text
            // 
            this.button_save_text.BackColor = System.Drawing.Color.Yellow;
            this.button_save_text.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_save_text.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_save_text.Location = new System.Drawing.Point(343, 34);
            this.button_save_text.Name = "button_save_text";
            this.button_save_text.Size = new System.Drawing.Size(155, 49);
            this.button_save_text.TabIndex = 0;
            this.button_save_text.Text = "ULOŽ ZPRÁVU";
            this.button_save_text.UseVisualStyleBackColor = false;
            this.button_save_text.Click += new System.EventHandler(this.button_save_text_Click);
            // 
            // button_hash
            // 
            this.button_hash.BackColor = System.Drawing.Color.Yellow;
            this.button_hash.Enabled = false;
            this.button_hash.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_hash.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_hash.Location = new System.Drawing.Point(343, 89);
            this.button_hash.Name = "button_hash";
            this.button_hash.Size = new System.Drawing.Size(155, 49);
            this.button_hash.TabIndex = 1;
            this.button_hash.Text = "ZOBRAZ HASH ZPRÁVY";
            this.button_hash.UseVisualStyleBackColor = false;
            this.button_hash.Click += new System.EventHandler(this.button_hash_Click);
            // 
            // button_zip
            // 
            this.button_zip.BackColor = System.Drawing.Color.Yellow;
            this.button_zip.Enabled = false;
            this.button_zip.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_zip.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_zip.Location = new System.Drawing.Point(343, 144);
            this.button_zip.Name = "button_zip";
            this.button_zip.Size = new System.Drawing.Size(155, 49);
            this.button_zip.TabIndex = 2;
            this.button_zip.Text = "ZAŠIFRUJ A ZAZIPUJ";
            this.button_zip.UseVisualStyleBackColor = false;
            this.button_zip.Click += new System.EventHandler(this.button_zip_Click);
            // 
            // button_unzip
            // 
            this.button_unzip.BackColor = System.Drawing.Color.Yellow;
            this.button_unzip.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_unzip.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_unzip.Location = new System.Drawing.Point(343, 199);
            this.button_unzip.Name = "button_unzip";
            this.button_unzip.Size = new System.Drawing.Size(155, 49);
            this.button_unzip.TabIndex = 3;
            this.button_unzip.Text = "ROZBAL SOUBORY";
            this.button_unzip.UseVisualStyleBackColor = false;
            this.button_unzip.Click += new System.EventHandler(this.button_unzip_Click);
            // 
            // textBox_text
            // 
            this.textBox_text.Font = new System.Drawing.Font("Palatino Linotype", 9.75F);
            this.textBox_text.Location = new System.Drawing.Point(12, 34);
            this.textBox_text.Multiline = true;
            this.textBox_text.Name = "textBox_text";
            this.textBox_text.Size = new System.Drawing.Size(310, 80);
            this.textBox_text.TabIndex = 4;
            // 
            // textBox_sha256
            // 
            this.textBox_sha256.Font = new System.Drawing.Font("Palatino Linotype", 9.75F);
            this.textBox_sha256.Location = new System.Drawing.Point(12, 142);
            this.textBox_sha256.Multiline = true;
            this.textBox_sha256.Name = "textBox_sha256";
            this.textBox_sha256.ReadOnly = true;
            this.textBox_sha256.Size = new System.Drawing.Size(310, 80);
            this.textBox_sha256.TabIndex = 5;
            // 
            // textBox_hashCheck
            // 
            this.textBox_hashCheck.Font = new System.Drawing.Font("Palatino Linotype", 9.75F);
            this.textBox_hashCheck.Location = new System.Drawing.Point(12, 361);
            this.textBox_hashCheck.Multiline = true;
            this.textBox_hashCheck.Name = "textBox_hashCheck";
            this.textBox_hashCheck.ReadOnly = true;
            this.textBox_hashCheck.Size = new System.Drawing.Size(310, 80);
            this.textBox_hashCheck.TabIndex = 6;
            // 
            // button_choose_pub_key
            // 
            this.button_choose_pub_key.BackColor = System.Drawing.Color.Yellow;
            this.button_choose_pub_key.Enabled = false;
            this.button_choose_pub_key.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_choose_pub_key.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_choose_pub_key.Location = new System.Drawing.Point(343, 254);
            this.button_choose_pub_key.Name = "button_choose_pub_key";
            this.button_choose_pub_key.Size = new System.Drawing.Size(155, 49);
            this.button_choose_pub_key.TabIndex = 7;
            this.button_choose_pub_key.Text = "VYBER VEŘEJNÝ KLÍČ\r\n";
            this.button_choose_pub_key.UseVisualStyleBackColor = false;
            this.button_choose_pub_key.Click += new System.EventHandler(this.button_choose_pub_key_Click);
            // 
            // button_choose_encr_hash
            // 
            this.button_choose_encr_hash.BackColor = System.Drawing.Color.Yellow;
            this.button_choose_encr_hash.Enabled = false;
            this.button_choose_encr_hash.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_choose_encr_hash.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_choose_encr_hash.Location = new System.Drawing.Point(343, 364);
            this.button_choose_encr_hash.Name = "button_choose_encr_hash";
            this.button_choose_encr_hash.Size = new System.Drawing.Size(155, 49);
            this.button_choose_encr_hash.TabIndex = 8;
            this.button_choose_encr_hash.Text = "VYBER ZPRÁVU K ROZŠIFROVÁNÍ\r\n";
            this.button_choose_encr_hash.UseVisualStyleBackColor = false;
            this.button_choose_encr_hash.Click += new System.EventHandler(this.button_choose_encr_hash_Click);
            // 
            // textBox_delivery
            // 
            this.textBox_delivery.Font = new System.Drawing.Font("Palatino Linotype", 9.75F);
            this.textBox_delivery.Location = new System.Drawing.Point(12, 250);
            this.textBox_delivery.Multiline = true;
            this.textBox_delivery.Name = "textBox_delivery";
            this.textBox_delivery.ReadOnly = true;
            this.textBox_delivery.Size = new System.Drawing.Size(310, 80);
            this.textBox_delivery.TabIndex = 9;
            this.textBox_delivery.TextChanged += new System.EventHandler(this.textBox_delivery_TextChanged);
            // 
            // button_show_deliv_hash
            // 
            this.button_show_deliv_hash.BackColor = System.Drawing.Color.Yellow;
            this.button_show_deliv_hash.Enabled = false;
            this.button_show_deliv_hash.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_show_deliv_hash.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_show_deliv_hash.Location = new System.Drawing.Point(343, 309);
            this.button_show_deliv_hash.Name = "button_show_deliv_hash";
            this.button_show_deliv_hash.Size = new System.Drawing.Size(155, 49);
            this.button_show_deliv_hash.TabIndex = 10;
            this.button_show_deliv_hash.Text = "ZOBRAZ HASH PŘIJATÉ ZPRÁVY\r\n";
            this.button_show_deliv_hash.UseVisualStyleBackColor = false;
            this.button_show_deliv_hash.Click += new System.EventHandler(this.button_show_deliv_hash_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 22);
            this.label1.TabIndex = 11;
            this.label1.Text = "Zpráva k odeslání:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(12, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 22);
            this.label2.TabIndex = 12;
            this.label2.Text = "HASH uložené zprávy:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(12, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 22);
            this.label3.TabIndex = 13;
            this.label3.Text = "HASH načtené zprávy:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(12, 336);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 22);
            this.label4.TabIndex = 14;
            this.label4.Text = "Kontrola HASHe:";
            // 
            // checkBox_saveKeys
            // 
            this.checkBox_saveKeys.AutoSize = true;
            this.checkBox_saveKeys.Enabled = false;
            this.checkBox_saveKeys.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold);
            this.checkBox_saveKeys.Location = new System.Drawing.Point(343, 422);
            this.checkBox_saveKeys.Name = "checkBox_saveKeys";
            this.checkBox_saveKeys.Size = new System.Drawing.Size(102, 22);
            this.checkBox_saveKeys.TabIndex = 15;
            this.checkBox_saveKeys.Text = "Uložit klíče?";
            this.checkBox_saveKeys.UseVisualStyleBackColor = true;
            this.checkBox_saveKeys.CheckedChanged += new System.EventHandler(this.checkBox_saveKeys_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gold;
            this.ClientSize = new System.Drawing.Size(510, 456);
            this.Controls.Add(this.checkBox_saveKeys);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_show_deliv_hash);
            this.Controls.Add(this.textBox_delivery);
            this.Controls.Add(this.button_choose_encr_hash);
            this.Controls.Add(this.button_choose_pub_key);
            this.Controls.Add(this.textBox_hashCheck);
            this.Controls.Add(this.textBox_sha256);
            this.Controls.Add(this.textBox_text);
            this.Controls.Add(this.button_unzip);
            this.Controls.Add(this.button_zip);
            this.Controls.Add(this.button_hash);
            this.Controls.Add(this.button_save_text);
            this.Name = "Form1";
            this.Text = "DSA";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_save_text;
        private System.Windows.Forms.Button button_hash;
        private System.Windows.Forms.Button button_zip;
        private System.Windows.Forms.Button button_unzip;
        private System.Windows.Forms.TextBox textBox_text;
        private System.Windows.Forms.TextBox textBox_sha256;
        private System.Windows.Forms.TextBox textBox_hashCheck;
        private System.Windows.Forms.Button button_choose_pub_key;
        private System.Windows.Forms.Button button_choose_encr_hash;
        private System.Windows.Forms.TextBox textBox_delivery;
        private System.Windows.Forms.Button button_show_deliv_hash;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox_saveKeys;
    }
}

