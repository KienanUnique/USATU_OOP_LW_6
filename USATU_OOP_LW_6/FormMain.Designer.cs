namespace USATU_OOP_LW_6
{
    partial class FormMain
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
            this.panelForDrawing = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelForDrawing
            // 
            this.panelForDrawing.Location = new System.Drawing.Point(12, 12);
            this.panelForDrawing.Name = "panelForDrawing";
            this.panelForDrawing.Size = new System.Drawing.Size(776, 426);
            this.panelForDrawing.TabIndex = 0;
            this.panelForDrawing.Paint += new System.Windows.Forms.PaintEventHandler(this.panelForDrawing_Paint);
            this.panelForDrawing.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelForDrawing_MouseClick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelForDrawing);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FormMain";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelForDrawing;

        #endregion
    }
}