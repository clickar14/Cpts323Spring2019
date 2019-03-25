﻿namespace RideStalk
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Cars");
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.navigationTabs = new MetroFramework.Controls.MetroTabControl();
            this.mapInterface = new System.Windows.Forms.TabPage();
            this.mapView = new GMap.NET.WindowsForms.GMapControl();
            this.mainInterface = new System.Windows.Forms.TabPage();
            this.metroListView1 = new MetroFramework.Controls.MetroListView();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroContextMenu1 = new MetroFramework.Controls.MetroContextMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.navigationTabs.SuspendLayout();
            this.mapInterface.SuspendLayout();
            this.mainInterface.SuspendLayout();
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
            this.navigationTabs.SelectedIndex = 1;
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
            this.mapView.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.mapView.Bearing = 0F;
            this.mapView.CanDragMap = true;
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
            this.mainInterface.Controls.Add(this.metroListView1);
            this.mainInterface.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.mainInterface.Location = new System.Drawing.Point(4, 44);
            this.mainInterface.Name = "mainInterface";
            this.mainInterface.Size = new System.Drawing.Size(1061, 527);
            this.mainInterface.TabIndex = 1;
            this.mainInterface.Text = "Trip Summaries";
            // 
            // metroListView1
            // 
            this.metroListView1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.metroListView1.FullRowSelect = true;
            this.metroListView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.metroListView1.Location = new System.Drawing.Point(0, 0);
            this.metroListView1.Name = "metroListView1";
            this.metroListView1.OwnerDraw = true;
            this.metroListView1.Size = new System.Drawing.Size(1077, 527);
            this.metroListView1.TabIndex = 0;
            this.metroListView1.UseCompatibleStateImageBehavior = false;
            this.metroListView1.UseSelectable = true;
            this.metroListView1.View = System.Windows.Forms.View.Tile;
            this.metroListView1.SelectedIndexChanged += new System.EventHandler(this.metroListView1_SelectedIndexChanged);
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(137, 20);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(75, 26);
            this.metroButton1.TabIndex = 11;
            this.metroButton1.Text = "Test Trip";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // metroContextMenu1
            // 
            this.metroContextMenu1.Name = "metroContextMenu1";
            this.metroContextMenu1.Size = new System.Drawing.Size(61, 4);
            // 
            // Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 651);
            this.Controls.Add(this.metroButton1);
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
            this.mainInterface.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroTabControl navigationTabs;
        private System.Windows.Forms.TabPage mapInterface;
        private System.Windows.Forms.TabPage mainInterface;
        private GMap.NET.WindowsForms.GMapControl mapView;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroListView metroListView1;
        private MetroFramework.Controls.MetroContextMenu metroContextMenu1;
    }
}

