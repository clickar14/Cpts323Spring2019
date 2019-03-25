using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.CacheProviders;
using GMap.NET.Internals;
using GMap.NET.MapProviders;
using GMap.NET.ObjectModel;
using GMap.NET.Projections;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Threading;

// Video for routes: https://www.youtube.com/watch?v=FF-PJQxpjOY

namespace RideStalk
{
    public partial class Interface : MetroFramework.Forms.MetroForm
    {
        List<PointLatLng> _points;
        public Interface()
        {
            InitializeComponent();
            //Create a single list to be used for routes
            _points = new List<PointLatLng>();

        }
        // On initial interface load, create the map
        private void Interface_Load(object sender, EventArgs e)
        {
            GMapProviders.GoogleMap.ApiKey = @"AIzaSyAdLhafjca3jiLothU5wizd4syyQTYK5jQ";
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            mapView.CacheLocation = @"cache";
            this.StyleManager = metroStyleManager1;
            //Loading map
            mapView.MapProvider = GMapProviders.GoogleMap;
            double lat = 46.289428;
            double lng = -119.291794;
            mapView.Position = new GMap.NET.PointLatLng(lat, lng);
            mapView.Zoom = 13;
            mapView.ShowCenter = false;

            // Start an update cars thread, this should be continouse and should never abort.
            // It should update by 5 second itervals
            Thread carUpdateThread = new Thread(updateCars);
            carUpdateThread.Start();
        }
        /* Marker on click event, integrate later to have a form popup when the marker is clicked.
        private void gmap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            Console.WriteLine(String.Format("Marker {0} was clicked.", item.Tag));
        }
        */
        private void mapInterface_Click(object sender, EventArgs e)
        {

        }

        private void mapView_Click(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Thread paintThread = new Thread(paintRoute);
            paintThread.Start();
            
            // Add route points to overlay

            // YOu can clear the point list by calling the following:
            // _points.Clear();
        }

        // Thread process to handle the updating of cars
        private void updateCars(object sender)
        {
            while (true)
            {

                Thread.Sleep(5000);
            }
        }
        // These must be declared as globals
        GMapOverlay routeOverlay = new GMapOverlay("routeInfo");
        private void paintRoute(object variables)
        {
            // Remove the route overlay if it exists


            // Starting location
            _points.Add(new PointLatLng(46.289428, -119.291793));
            // Ending Location
            _points.Add(new PointLatLng(46.221432, -119.277332));
           
            // Create marker
            GMapMarker car = new GMarkerGoogle(
                new PointLatLng(_points[0].Lat, _points[1].Lng),
                GMarkerGoogleType.lightblue);
            routeOverlay.Markers.Add(car);
            car.ToolTipText = $"{_points[0].Lat},{_points[0].Lng}";


            var route = GoogleMapProvider.Instance
            .GetRoute(_points[0], _points[1], false, false, 15);
           
            
            // For the my route, make it unique
            var newRoute = new GMapRoute(route.Points, "My route");

            int pointCount = newRoute.Points.Count();
            var tempPoints = new List<PointLatLng>();
            var referencePoints = new List<PointLatLng>();
            referencePoints = newRoute.Points;
            // Animate painting
            for (int x = 0; x < pointCount; x++)
            {
                tempPoints.Add(referencePoints[x]);
                var currentRoute = new GMapRoute(tempPoints, $"progress{x}");
                routeOverlay.Routes.Clear();
                routeOverlay.Routes.Add(currentRoute);

                // Add the overlay to the map

                car.Position = tempPoints[x];
                routeOverlay.Markers.Clear();
                routeOverlay.Markers.Add(car);
                car.ToolTipText = $"{tempPoints[x].Lat},{tempPoints[x].Lng}";

                // Find a better way to add these to the overlay, maybe creating an index for each trip?
                mapView.Overlays.Add(routeOverlay);

                currentRoute.Stroke.Width = 4;
                currentRoute.Stroke.Color = Color.BlueViolet;
                mapView.Invalidate();
                mapView.UpdateRouteLocalPosition(currentRoute);
                Thread.Sleep(100);
            }
           
        }

        private void metroListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
