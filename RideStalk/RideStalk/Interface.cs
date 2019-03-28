using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reactive.Linq;
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
using Firebase.Database;
using Firebase.Database.Extensions;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using Firebase.Database.Http;
// This allows the console to appear
using System.Runtime.InteropServices;


// Video for routes: https://www.youtube.com/watch?v=FF-PJQxpjOY

namespace RideStalk
{
    public partial class Interface : MetroFramework.Forms.MetroForm
    {
        serviceData car1;
        serviceData car2;
        serviceData car3;
        serviceData car4;
        string key1, key2, key3, key4;
        
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
            // Remove this function when you're done utilizing the console
            AllocConsole();

            // Gmap Setup
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

            // Populate car objects and post them to the server.
            Thread carUpdateThread = new Thread(runCars);
            carUpdateThread.Start();

            // Start an update cars thread, this should be continouse and should never abort.
            // It should update by 5 second itervals


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
        // This is for the test route button
        private void metroButton1_Click(object sender, EventArgs e)
        {
            Thread paintThread = new Thread(paintRoute);
            paintThread.Start();
            
        }

        // Thread process to handle the updating of cars
        private void runCars()
        {
            updateCars().Wait();
        }
        private async Task updateCars()
        {
            var firebase = new FirebaseClient("https://test-24354.firebaseio.com/");
            // Creates a child called services
            var newService = firebase.Child("services");
            // Clear the list in database for now, get rid of later
            await newService.DeleteAsync();

            car1 = new CarOperations().genService();
            car2 = new CarOperations().genService();
            car3 = new CarOperations().genService();
            car4 = new CarOperations().genService();

            // Populating the database and assigning keys for patch/pull reference.
            var post1 = await newService.PostAsync(car1);
            var post2 = await newService.PostAsync(car2);
            var post3 = await newService.PostAsync(car3);
            var post4 = await newService.PostAsync(car4);
            key1 = post1.Key;
            key2 = post2.Key;
            key3 = post3.Key;
            key4 = post4.Key;
            // Pull information from database
            car1 = await newService.Child($"{post1.Key}").OnceSingleAsync<serviceData>();
            car2 = await newService.Child($"{post2.Key}").OnceSingleAsync<serviceData>();
            car3 = await newService.Child($"{post3.Key}").OnceSingleAsync<serviceData>();
            car4 = await newService.Child($"{post4.Key}").OnceSingleAsync<serviceData>();
            
           
            //// In order to change the value of a variable a new object must be made.

            //var retrievePost = new serviceData();

            //await newService.Child($"{post2.Key}").PatchAsync(retrievePost);

            //// Example of retrieving data by referencing the service key of that post.
            //var retrieveagain = await newService.Child($"{post1.Key}").OnceSingleAsync<serviceData>();
            //retrieveagain.acepted = "true";
            //// Posting once again to update driver to bob
            //await newService.Child($"{post1.Key}").PatchAsync(retrieveagain);
            //Console.WriteLine($"{retrieveagain.driver}");

            // Testing the creation of a new car
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

        // This is the stuff that manages the console delete later
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}
