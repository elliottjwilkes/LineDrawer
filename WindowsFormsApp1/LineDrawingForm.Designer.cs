
namespace WindowsFormsApp1
{
    partial class LineDrawingForm
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
            this.lineCanvas = new System.Windows.Forms.PictureBox();
            this.Colour = new System.Windows.Forms.Button();
            this.Remove = new System.Windows.Forms.Button();
            this.FindIntersections = new System.Windows.Forms.Button();
            this.LinesBox = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.lineCanvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LinesBox)).BeginInit();
            this.SuspendLayout();
            // 
            // lineCanvas
            // 
            this.lineCanvas.Location = new System.Drawing.Point(12, 12);
            this.lineCanvas.Name = "lineCanvas";
            this.lineCanvas.Size = new System.Drawing.Size(330, 411);
            this.lineCanvas.TabIndex = 5;
            this.lineCanvas.TabStop = false;
            this.lineCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lineCanvas_MouseUp);
            // 
            // Colour
            // 
            this.Colour.Location = new System.Drawing.Point(367, 393);
            this.Colour.Name = "Colour";
            this.Colour.Size = new System.Drawing.Size(69, 26);
            this.Colour.TabIndex = 10;
            this.Colour.Text = "Colour";
            this.Colour.UseVisualStyleBackColor = true;
            this.Colour.Click += new System.EventHandler(this.Colour_Click);
            // 
            // Remove
            // 
            this.Remove.Location = new System.Drawing.Point(576, 390);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(69, 29);
            this.Remove.TabIndex = 12;
            this.Remove.Text = "Remove";
            this.Remove.UseVisualStyleBackColor = true;
            this.Remove.Click += new System.EventHandler(this.Remove_Click);
            // 
            // FindIntersections
            // 
            this.FindIntersections.Location = new System.Drawing.Point(442, 393);
            this.FindIntersections.Name = "FindIntersections";
            this.FindIntersections.Size = new System.Drawing.Size(128, 26);
            this.FindIntersections.TabIndex = 13;
            this.FindIntersections.Text = "Find Intersections";
            this.FindIntersections.UseVisualStyleBackColor = true;
            this.FindIntersections.Click += new System.EventHandler(this.FindIntersections_Click);
            // 
            // LinesBox
            // 
            this.LinesBox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LinesBox.Location = new System.Drawing.Point(367, 13);
            this.LinesBox.Name = "LinesBox";
            this.LinesBox.RowHeadersWidth = 51;
            this.LinesBox.RowTemplate.Height = 24;
            this.LinesBox.Size = new System.Drawing.Size(278, 371);
            this.LinesBox.TabIndex = 14;
            this.LinesBox.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.LinesBox_CellClick);
            this.LinesBox.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.LinesBox_CellEndEdit);
            this.LinesBox.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.LinesBox_CellFormatting);
            // 
            // LineDrawingForm
            // 
            this.ClientSize = new System.Drawing.Size(660, 435);
            this.Controls.Add(this.LinesBox);
            this.Controls.Add(this.FindIntersections);
            this.Controls.Add(this.Remove);
            this.Controls.Add(this.Colour);
            this.Controls.Add(this.lineCanvas);
            this.Name = "LineDrawingForm";
            ((System.ComponentModel.ISupportInitialize)(this.lineCanvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LinesBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox lineCanvas;
        private System.Windows.Forms.Button Colour;
        private System.Windows.Forms.Button Remove;
        private System.Windows.Forms.Button FindIntersections;
        private System.Windows.Forms.DataGridView LinesBox;
    }
}

