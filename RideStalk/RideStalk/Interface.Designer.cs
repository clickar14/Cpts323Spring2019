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
            this.components = new System.ComponentModel.Container();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.navigationTabs = new MetroFramework.Controls.MetroTabControl();
            this.mapInterface = new System.Windows.Forms.TabPage();
            this.mapView = new GMap.NET.WindowsForms.GMapControl();
            this.mainInterface = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.navigationTabs.SuspendLayout();
            this.mapInterface.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            this.metroStyleManager1.Style = MetroFramework.MetroColorStyle.Lime;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // navigationTabs
            // 
            this.navigationTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.navigationTabs.Controls.Add(this.mapInterface);
            this.navigationTabs.Controls.Add(this.mainInterface);
            this.navigationTabs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.navigationTabs.FontSize = MetroFramework.MetroTabControlSize.Tall;
            this.navigationTabs.ItemSize = new System.Drawing.Size(100, 40);
            this.navigationTabs.Location = new System.Drawing.Point(23, 63);
            this.navigationTabs.Name = "navigationTabs";
            this.navigationTabs.SelectedIndex = 0;
            this.navigationTabs.Size = new System.Drawing.Size(1069, 575);
            this.navigationTabs.TabIndex = 10;
            this.navigationTabs.UseSelectable = true;
            this.navigationTabs.UseStyleColors = true;
            // 
            // mapInterface
            // 
            this.mapInterface.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.mapInterface.Controls.Add(this.mapView);
            this.mapInterface.Font = new System.Drawing.Font("MingLiU-ExtB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mapInterface.Location = new System.Drawing.Point(4, 44);
            this.mapInterface.Name = "mapInterface";
            this.mapInterface.Size = new System.Drawing.Size(1061, 527);
            this.mapInterface.TabIndex = 0;
            this.mapInterface.Text = "Map View    ";
            // 
            // mapView
            // 
            this.mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapView.AutoScroll = true;
            this.mapView.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.mapView.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.mapView.Bearing = 0F;
            this.mapView.CanDragMap = false;
            this.mapView.Cursor = System.Windows.Forms.Cursors.Arrow;
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
            this.mapView.Size = new System.Drawing.Size(1061, 531);
            this.mapView.TabIndex = 10;
            this.mapView.Zoom = 0D;
            // 
            // mainInterface
            // 
            this.mainInterface.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.mainInterface.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.mainInterface.Location = new System.Drawing.Point(4, 44);
            this.mainInterface.Name = "mainInterface";
            this.mainInterface.Size = new System.Drawing.Size(1061, 507);
            this.mainInterface.TabIndex = 1;
            this.mainInterface.Text = "Trip Summaries";
            // 
            // Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 651);
            this.Controls.Add(this.navigationTabs);
            this.MinimumSize = new System.Drawing.Size(1112, 651);
            this.Name = "Interface";
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "RideStalk";
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            this.Load += new System.EventHandler(this.Interface_Load);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.navigationTabs.ResumeLayout(false);
            this.mapInterface.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroTabControl navigationTabs;
        private System.Windows.Forms.TabPage mapInterface;
        private System.Windows.Forms.TabPage mainInterface;
        private GMap.NET.WindowsForms.GMapControl mapView;
    }
}

