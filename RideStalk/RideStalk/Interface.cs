using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.CacheProviders;
using GMap.NET.Internals;
using GMap.NET.MapProviders;
using GMap.NET.ObjectModel;
using GMap.NET.Projections;
using GMap.NET.WindowsForms;


namespace RideStalk
{
    public partial class Interface : MetroFramework.Forms.MetroForm
    {
        public Interface()
        {
            InitializeComponent();

        }

        private void Interface_Load(object sender, EventArgs e)
        {
            this.StyleManager = metroStyleManager1;
            //Loading map
            mapView.MapProvider = GMapProviders.GoogleMap;
            double lat = 46.289428;
            double longt = -119.291794;
            mapView.Position = new GMap.NET.PointLatLng(lat, longt);
            mapView.Zoom = 13;
            mapView.ShowCenter = false;
        }
        private void mapInterface_Click(object sender, EventArgs e)
        {

        }

        private void mapView_Load(object sender, EventArgs e)
        {

        }

        private void mainInterface_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
