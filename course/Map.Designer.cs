namespace course
{
    partial class Map
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
            this.gmap1 = new GMap.NET.WindowsForms.GMapControl();
            this.SuspendLayout();
            // 
            // gmap1
            // 
            this.gmap1.Bearing = 0F;
            this.gmap1.CanDragMap = true;
            this.gmap1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap1.GrayScaleMode = false;
            this.gmap1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap1.LevelsKeepInMemory = 5;
            this.gmap1.Location = new System.Drawing.Point(12, 12);
            this.gmap1.MarkersEnabled = true;
            this.gmap1.MaxZoom = 2;
            this.gmap1.MinZoom = 2;
            this.gmap1.MouseWheelZoomEnabled = true;
            this.gmap1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap1.Name = "gmap1";
            this.gmap1.NegativeMode = false;
            this.gmap1.PolygonsEnabled = true;
            this.gmap1.RetryLoadTile = 0;
            this.gmap1.RoutesEnabled = true;
            this.gmap1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap1.ShowTileGridLines = false;
            this.gmap1.Size = new System.Drawing.Size(776, 426);
            this.gmap1.TabIndex = 0;
            this.gmap1.Zoom = 0D;
            // 
            // Map
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gmap1);
            this.Name = "Map";
            this.Text = "Map";
            this.ResumeLayout(false);

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gmap1;
    }
}