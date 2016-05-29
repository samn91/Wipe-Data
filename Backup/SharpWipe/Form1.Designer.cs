namespace SharpWipe
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
            this.buttWipe = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.buttOpenFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttWipe
            // 
            this.buttWipe.Location = new System.Drawing.Point(239, 42);
            this.buttWipe.Name = "buttWipe";
            this.buttWipe.Size = new System.Drawing.Size(53, 23);
            this.buttWipe.TabIndex = 0;
            this.buttWipe.Text = "Wipe";
            this.buttWipe.UseVisualStyleBackColor = true;
            this.buttWipe.Click += new System.EventHandler(this.buttWipe_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(12, 47);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(35, 13);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "lblInfo";
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.Location = new System.Drawing.Point(12, 15);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(221, 20);
            this.txtFileName.TabIndex = 3;
            // 
            // buttOpenFile
            // 
            this.buttOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttOpenFile.Location = new System.Drawing.Point(239, 13);
            this.buttOpenFile.Name = "buttOpenFile";
            this.buttOpenFile.Size = new System.Drawing.Size(53, 23);
            this.buttOpenFile.TabIndex = 4;
            this.buttOpenFile.Text = "Browse";
            this.buttOpenFile.UseVisualStyleBackColor = true;
            this.buttOpenFile.Click += new System.EventHandler(this.buttOpenFile_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 71);
            this.Controls.Add(this.buttOpenFile);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.buttWipe);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SharpWipe";
            this.TopMost = true;
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttWipe;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button buttOpenFile;
    }
}

