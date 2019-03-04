namespace RideStalk
{
    partial class Interface
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
            this.mainInterface = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.mapInterface = new System.Windows.Forms.Panel();
            this.mapView = new GMap.NET.WindowsForms.GMapControl();
            this.changeInterface = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.listView2 = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.listView3 = new System.Windows.Forms.ListView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.mainInterface.SuspendLayout();
            this.mapInterface.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainInterface
            // 
            this.mainInterface.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainInterface.Controls.Add(this.mapInterface);
            this.mainInterface.Controls.Add(this.listView3);
            this.mainInterface.Controls.Add(this.listView2);
            this.mainInterface.Controls.Add(this.label1);
            this.mainInterface.Controls.Add(this.label2);
            this.mainInterface.Location = new System.Drawing.Point(0, 0);
            this.mainInterface.Name = "mainInterface";
            this.mainInterface.Size = new System.Drawing.Size(762, 631);
            this.mainInterface.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Active Cab Trip List";
            // 
            // mapInterface
            // 
            this.mapInterface.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapInterface.Controls.Add(this.mapView);
            this.mapInterface.Location = new System.Drawing.Point(0, 0);
            this.mapInterface.Name = "mapInterface";
            this.mapInterface.Size = new System.Drawing.Size(762, 631);
            this.mapInterface.TabIndex = 0;
            this.mapInterface.Visible = false;
            // 
            // mapView
            // 
            this.mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapView.AutoScroll = true;
            this.mapView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mapView.Bearing = 0F;
            this.mapView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mapView.CanDragMap = true;
            this.mapView.EmptyTileColor = System.Drawing.Color.Navy;
            this.mapView.GrayScaleMode = false;
            this.mapView.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.mapView.LevelsKeepInMemmory = 5;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MarkersEnabled = true;
            this.mapView.MaxZoom = 100;
            this.mapView.MinZoom = 5;
            this.mapView.MouseWheelZoomEnabled = false;
            this.mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.mapView.Name = "mapView";
            this.mapView.NegativeMode = false;
            this.mapView.PolygonsEnabled = true;
            this.mapView.RetryLoadTile = 0;
            this.mapView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mapView.RoutesEnabled = true;
            this.mapView.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.mapView.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.mapView.ShowTileGridLines = false;
            this.mapView.Size = new System.Drawing.Size(762, 631);
            this.mapView.TabIndex = 0;
            this.mapView.Zoom = 0D;
            this.mapView.Load += new System.EventHandler(this.mapView_Load);
            // 
            // changeInterface
            // 
            this.changeInterface.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.changeInterface.Location = new System.Drawing.Point(870, 595);
            this.changeInterface.Name = "changeInterface";
            this.changeInterface.Size = new System.Drawing.Size(110, 23);
            this.changeInterface.TabIndex = 4;
            this.changeInterface.Text = "View Map";
            this.changeInterface.UseVisualStyleBackColor = true;
            this.changeInterface.Click += new System.EventHandler(this.changeInterface_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(705, 630);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // listView2
            // 
            this.listView2.Location = new System.Drawing.Point(54, 90);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(308, 462);
            this.listView2.TabIndex = 2;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(396, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Pending Cabs List";
            // 
            // listView3
            // 
            this.listView3.Location = new System.Drawing.Point(399, 90);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(308, 462);
            this.listView3.TabIndex = 4;
            this.listView3.UseCompatibleStateImageBehavior = false;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(809, 64);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(247, 365);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(893, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Profit Summary";
            // 
            // Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 630);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.changeInterface);
            this.Controls.Add(this.mainInterface);
            this.Controls.Add(this.splitter1);
            this.MinimumSize = new System.Drawing.Size(1112, 651);
            this.Name = "Interface";
            this.Text = "RideStalk";
            this.Load += new System.EventHandler(this.Interface_Load);
            this.mainInterface.ResumeLayout(false);
            this.mainInterface.PerformLayout();
            this.mapInterface.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel mainInterface;
        private System.Windows.Forms.Panel mapInterface;
        private GMap.NET.WindowsForms.GMapControl mapView;
        private System.Windows.Forms.Button changeInterface;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label3;
    }
}

