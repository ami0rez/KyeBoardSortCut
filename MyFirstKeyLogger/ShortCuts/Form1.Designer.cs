namespace MyFirstKeyLogger
{
    partial class ShortCut
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
            this.alphaPadFLP = new System.Windows.Forms.FlowLayoutPanel();
            this.otrPnl = new System.Windows.Forms.Panel();
            this.statelbl = new System.Windows.Forms.Label();
            this.numPadFLP = new System.Windows.Forms.FlowLayoutPanel();
            this.otrPnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // alphaPadFLP
            // 
            this.alphaPadFLP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alphaPadFLP.Location = new System.Drawing.Point(0, 0);
            this.alphaPadFLP.Name = "alphaPadFLP";
            this.alphaPadFLP.Size = new System.Drawing.Size(717, 226);
            this.alphaPadFLP.TabIndex = 12;
            // 
            // otrPnl
            // 
            this.otrPnl.BackColor = System.Drawing.Color.White;
            this.otrPnl.Controls.Add(this.statelbl);
            this.otrPnl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.otrPnl.Location = new System.Drawing.Point(0, 226);
            this.otrPnl.Name = "otrPnl";
            this.otrPnl.Size = new System.Drawing.Size(717, 49);
            this.otrPnl.TabIndex = 14;
            // 
            // statelbl
            // 
            this.statelbl.AutoSize = true;
            this.statelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold);
            this.statelbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.statelbl.Location = new System.Drawing.Point(6, 8);
            this.statelbl.Name = "statelbl";
            this.statelbl.Size = new System.Drawing.Size(34, 36);
            this.statelbl.TabIndex = 5;
            this.statelbl.Text = "●";
            this.statelbl.Click += new System.EventHandler(this.statelbl_Click);
            // 
            // numPadFLP
            // 
            this.numPadFLP.Dock = System.Windows.Forms.DockStyle.Right;
            this.numPadFLP.Location = new System.Drawing.Point(717, 0);
            this.numPadFLP.Name = "numPadFLP";
            this.numPadFLP.Size = new System.Drawing.Size(217, 275);
            this.numPadFLP.TabIndex = 13;
            // 
            // ShortCut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(934, 275);
            this.Controls.Add(this.alphaPadFLP);
            this.Controls.Add(this.otrPnl);
            this.Controls.Add(this.numPadFLP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShortCut";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ShortCut";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.otrPnl.ResumeLayout(false);
            this.otrPnl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel alphaPadFLP;
        private System.Windows.Forms.Panel otrPnl;
        private System.Windows.Forms.Label statelbl;
        private System.Windows.Forms.FlowLayoutPanel numPadFLP;
    }
}

