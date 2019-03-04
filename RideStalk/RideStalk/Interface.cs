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
    public partial class Interface : Form
    {
        public Interface()
        {
            InitializeComponent();
        }

        private void Interface_Load(object sender, EventArgs e)
        {

        }

        private void mapView_Load(object sender, EventArgs e)
        {
           
        }
        private void changeInterface_Click(object sender, EventArgs e)
        {
            if(mapInterface.Visible == false)
            {
                mapInterface.Visible = true;
                changeInterface.Text = "View Interface";
                mapView.MapProvider = GMapProviders.GoogleMap;
                double lat = 46.289428;
                double longt = -119.291794;
                mapView.Position = new GMap.NET.PointLatLng(lat, longt);
                mapView.Zoom = 13;
            }
            else
            {
                mapInterface.Visible = false;
                changeInterface.Text = "View Map";
            }
        }

    }
}
