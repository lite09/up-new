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
            this.lb = new System.Windows.Forms.Label();
            this.l_bo = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(253, 466);
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
            // lb
            // 
            this.lb.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.lb.Location = new System.Drawing.Point(12, 421);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(114, 30);
            this.lb.TabIndex = 4;
            this.lb.Text = "Свойства замены";
            this.lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // l_bo
            // 
            this.l_bo.AllowDrop = true;
            this.l_bo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.l_bo.FormattingEnabled = true;
            this.l_bo.Location = new System.Drawing.Point(132, 421);
            this.l_bo.Name = "l_bo";
            this.l_bo.Size = new System.Drawing.Size(420, 30);
            this.l_bo.TabIndex = 5;
            this.l_bo.DragDrop += new System.Windows.Forms.DragEventHandler(this.l_bo_DragDrop);
            this.l_bo.DragEnter += new System.Windows.Forms.DragEventHandler(this.l_bo_DragEnter);
            // 
            // description
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(564, 501);
            this.ControlBox = false;
            this.Controls.Add(this.l_bo);
            this.Controls.Add(this.lb);
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
        private System.Windows.Forms.Label lb;
        private System.Windows.Forms.ListBox l_bo;
    }
}