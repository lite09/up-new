namespace up
{
    partial class description
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
            this.ok = new System.Windows.Forms.Button();
            this.rtb_description = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(251, 415);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(62, 23);
            this.ok.TabIndex = 2;
            this.ok.Text = "Ok";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // rtb_description
            // 
            this.rtb_description.Location = new System.Drawing.Point(12, 12);
            this.rtb_description.Name = "rtb_description";
            this.rtb_description.Size = new System.Drawing.Size(540, 397);
            this.rtb_description.TabIndex = 3;
            this.rtb_description.Text = "";
            // 
            // description
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(564, 450);
            this.ControlBox = false;
            this.Controls.Add(this.rtb_description);
            this.Controls.Add(this.ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "description";
            this.Text = "Описание";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ok;
        protected internal System.Windows.Forms.RichTextBox rtb_description;
    }
}