using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reactive.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
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
using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Drawing;
using MetroFramework.Properties;
using MetroFramework.Native;
using MetroFramework.Interfaces;
using MetroFramework.Fonts;
using MetroFramework.Animation;
using MetroFramework.Forms;

using System.Diagnostics;


// Video for routes: https://www.youtube.com/watch?v=FF-PJQxpjOY

namespace RideStalk
{
    public partial class Interface : MetroFramework.Forms.MetroForm
    {
        // Used to measure time elapsed for speed accuracy.
        Stopwatch triptime;
        List<serviceData> carList;
        List<MetroListView> tripSummariesList;
        List<string> carKeys;
        List<ListViewGroup> carGroups;
        List<MetroGrid> serviceLists;
        List<MetroComboBox> comboBoxNavigation;
        List<PointLatLng> _points;
        List<GMapRoute> routeList;
        int updateTime;
        System.Threading.Thread updateList;
        public Interface()
        {

            InitializeComponent();
            // Allocate List object for keys
            carKeys = new List<string>();
            // Allocate list object for cars
            carList = new List<serviceData>();
            carGroups = new List<ListViewGroup>();
            serviceLists = new List<MetroGrid>();
            tripSummariesList = new List<MetroListView>();
            comboBoxNavigation = new List<MetroComboBox>();
            routeList = new List<GMapRoute>();
            //Create a single list to be used for routes
            _points = new List<PointLatLng>();
            updateTime = 5000;

        }

        // On initial interface load, create the map
        private void Interface_Load(object sender, EventArgs e)
        {
           
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
            carList = new CarOperations().generateCars(carList);
            // Populate car objects and post them to the server.

            tripSummariesList.Add(carSumList);
            // Move the below to be executed by the thread above later
            renderLists();
            // Creates the service lists for each car
            populateServiceLists();
            this.Size = new Size(1260, 775);
            navigationTabs.SelectedIndex = 0;

            // Start the thread that manages updating of the list and server values

                       
        }

        public void realTimeListUpdate()
        {
            while (true) { 
            MethodInvoker listManage = delegate ()
            {
                for (int x = 0; x < carList.Count; x++)
                {
                    if (carList[x].acepted == "false")
                    {
                        carSumList.Groups[x].Header = $"Car {x + 1} - Inactive";
                        
                    }
                    else if (carList[x].acepted == "true")
                    {
                        carSumList.Groups[x].Header = $"Car {x + 1} - Active";
                    }
                }
                // Update trip summaries list
                for (int x = 0; x < carGroups.Count; x++)
                {
                    int selectedCarNumber = Int32.Parse(carGroups[x].Name);
                    carSumList.Groups[x].Items[0].SubItems[1].Text = carList[selectedCarNumber].user.username;
                    carSumList.Groups[x].Items[1].SubItems[1].Text = carList[selectedCarNumber].driver.did.ToString();
                    carSumList.Groups[x].Items[2].SubItems[1].Text = carList[selectedCarNumber].origin.originName;
                    carSumList.Groups[x].Items[3].SubItems[1].Text = carList[selectedCarNumber].destination.destinationName;
                    carSumList.Groups[x].Items[4].SubItems[1].Text = carList[selectedCarNumber].estimatedPrice.ToString();
                }
                // Update service item list
                for (int x = 0; x < 4; x++)
                {
                    serviceLists[x].Rows[0].Cells[0].Value = carList[x].acepted;
                    serviceLists[x].Rows[1].Cells[0].Value = carList[x].initialTime;
                    serviceLists[x].Rows[2].Cells[0].Value = carList[x].estimatedPrice;
                    serviceLists[x].Rows[3].Cells[0].Value = carList[x].finalPrice;
                    serviceLists[x].Rows[4].Cells[0].Value = carList[x].payMode;
                    serviceLists[x].Rows[5].Cells[0].Value = carList[x].travelTime;
                    serviceLists[x].Rows[6].Cells[0].Value = carList[x].travelDistance;
                    serviceLists[x].Rows[7].Cells[0].Value = carList[x].pickupDurationTime;
                }
                // Update user information lists
                for (int x = 4; x < 8; x++)                
                {
                    serviceLists[x].Rows[0].Cells[0].Value = carList[x-4].user.username;
                    serviceLists[x].Rows[1].Cells[0].Value = carList[x-4].user.uid;
                    serviceLists[x].Rows[2].Cells[0].Value = carList[x-4].user.image;
                    serviceLists[x].Rows[3].Cells[0].Value = carList[x-4].user.userCellphone;
                    serviceLists[x].Rows[4].Cells[0].Value = carList[x-4].user.userStar;
                }
                // Update driver information lists
                for (int x = 8; x < 12; x++)                
                {
                    serviceLists[x].Rows[0].Cells[0].Value = carList[x - 8].driver.Company;
                    serviceLists[x].Rows[1].Cells[0].Value = carList[x - 8].driver.did;
                    serviceLists[x].Rows[2].Cells[0].Value = carList[x - 8].driver.image;
                    serviceLists[x].Rows[3].Cells[0].Value = carList[x - 8].driver.car.carPlate;
                    serviceLists[x].Rows[4].Cells[0].Value = carList[x - 8].driver.car.carStars;
                }
                // Update trip information lists
                for (int x = 12; x < 16; x++)
                {
                    serviceLists[x].Rows[0].Cells[0].Value = carList[x - 12].carPosition.lat;
                    serviceLists[x].Rows[1].Cells[0].Value = carList[x - 12].carPosition.lng;
                    serviceLists[x].Rows[2].Cells[0].Value = carList[x - 12].origin.originName;
                    serviceLists[x].Rows[3].Cells[0].Value = carList[x - 12].origin.lat;
                    serviceLists[x].Rows[4].Cells[0].Value = carList[x - 12].origin.lng;
                    serviceLists[x].Rows[5].Cells[0].Value = carList[x - 12].destination.destinationName;
                    serviceLists[x].Rows[6].Cells[0].Value = carList[x - 12].destination.lat;
                    serviceLists[x].Rows[7].Cells[0].Value = carList[x - 12].destination.lng;
                }
                // Sends information to the server
                Thread carUpdateThread = new Thread(patchCars);
                carUpdateThread.Start();
                carUpdateThread.Join();
            };
            this.Invoke(listManage);

            Thread.Sleep(updateTime);
            }
            
        }

        private void patchCars()
        {
            runPatch().Wait();
        }
        private async Task runPatch()
        {
            var firebase = new FirebaseClient("https://test-24354.firebaseio.com/");
            // Creates a child called services
            var newService = firebase.Child("services");
            // Clear the list in database for now, get rid of later
                        
            for (int x = 0; x < 4; ++x)
            {
                if (carKeys[x] != "null")
                {
                    await newService.Child($"{carKeys[x]}").PatchAsync(carList[x]);
                }
            }

        }
        public void renderLists()
        {
            for (int x = 0; x < tripSummariesList.Count(); x++)
            {
                // Set the view to show details.
                tripSummariesList[x].View = View.Details;
                // Allow the user to edit item text.
                tripSummariesList[x].LabelEdit = true;
                tripSummariesList[x].CheckBoxes = false;
                // Allow the user to rearrange columns.
                tripSummariesList[x].AllowColumnReorder = true;
                // Display check boxes.
                // Select the item and subitems when selection is made.
                tripSummariesList[x].FullRowSelect = true;
                // Display grid lines.
                tripSummariesList[x].GridLines = true;
                // Sort the items in the list in ascending order.
                tripSummariesList[x].Sorting = SortOrder.Ascending;

                tripSummariesList[x].Columns.Add("Service Item", -2, HorizontalAlignment.Left);

                tripSummariesList[x].Columns.Add("Information", -2, HorizontalAlignment.Left);
                tripSummariesList[x].Columns[0].Tag = 1;
                tripSummariesList[x].Columns[1].Tag = 1;

            }

            //////////////////////////////////////////////
            // Render car objects into the inactive list//
            //////////////////////////////////////////////

            for(int x = 0; x < carList.Count(); x++)
            {

                carGroups.Add(new ListViewGroup($"{x}",$"Car {x+1}"));
                
                carSumList.Groups.Add(carGroups[x]);

                ListViewItem item1 = new ListViewItem("User:", $"0car{x+1}", carGroups[x]);
                item1.SubItems.Add(carList[x].user.username);

                ListViewItem item2 = new ListViewItem("Driver ID:", $"1car{x+1}", carGroups[x]);
                item2.SubItems.Add(carList[x].driver.did.ToString());

                ListViewItem item3 = new ListViewItem("Origin:", $"2car{x+1}", carGroups[x]);
                item3.SubItems.Add(carList[x].origin.originName);

                ListViewItem item4 = new ListViewItem("Destination:", $"3car{x+1}", carGroups[x]);
                item4.SubItems.Add(carList[x].destination.destinationName);

                ListViewItem item5 = new ListViewItem("Estimated Cost:", $"4car{x+1}", carGroups[x]);
                item5.SubItems.Add(carList[x].estimatedPrice.ToString());

                //Add the items to the ListView.

                carSumList.Items.AddRange(new ListViewItem[] { item1, item2, item3, item4, item5 });
                carKeys.Add("null");

            }
            
            
        }

        // This is for the test route button
        private void metroButton1_Click(object sender, EventArgs e)
        {
            //Thread tripRetrieveThread = new Thread(getCars);
            //tripRetrieveThread.Start();
            Thread paintThread = new Thread(paintRoute);
            paintThread.Start();
           // tripRetrieveThread.Join();
        }

        // Thread process to handle the initial grabbing of trips
        private void getCars()
        {
            initialRetrieveTrip().Wait();
            updateList = new System.Threading.Thread(realTimeListUpdate);
            updateList.Start();

        }
        private async Task initialRetrieveTrip()
        {
            while (true)
            {
                var firebase = new FirebaseClient("https://test-24354.firebaseio.com/");
                // Create a list of every service in the database
                var patchService = firebase.Child("services");
                var services = await patchService.OnceAsync<serverData>();

                foreach (var serviceItem in services)
                {

                    if (serviceItem.Object.acepted == "false")
                    {
                        for (int x = 0; x < 4; ++x)
                        {
                            if (carList[x].acepted == "false")
                            {
                                // Attempt to patch the car information to the service item
                                try
                                {
                                    carList[x].acepted = "true";
                                    await patchService.Child($"{serviceItem.Key}").PatchAsync(carList[x]);
                                }
                                // If the service has been claimed by another, break and go to the next service item 
                                catch
                                {
                                    carList[x].acepted = "false";
                                    break;
                                }
                                // If an exception wasn't raised, add the trip information to the carList
                                // and add the key to the key list.
                                carKeys[x] = $"{serviceItem.Key}";
                                carList[x].destination = serviceItem.Object.destination;
                                carList[x].origin = serviceItem.Object.origin;
                                carList[x].user = serviceItem.Object.user;
                                carList[x].estimatedPrice = serviceItem.Object.estimatedPrice;
                                carList[x].finalPrice = serviceItem.Object.finalPrice;
                                carList[x].payMode = serviceItem.Object.payMode;
                                carList[x].initialTime = serviceItem.Object.initialTime;
                                carList[x].pickupDurationTime = serviceItem.Object.pickupDurationTime;
                                carList[x].travelTime = serviceItem.Object.travelTime;
                                break;
                            }
                        }
                    }

                }

                //await newService.DeleteAsync();
                int acceptanceCheck = 0;
                for(int x = 0; x < 4; ++x)
                {
                    if(carList[x].acepted == "true")
                    {
                        acceptanceCheck++;
                    }
                }
                if(acceptanceCheck == 4)
                {
                    break;
                }
            }

        }


        // These must be declared as globals
        GMapOverlay routeOverlay = new GMapOverlay("routeInfo");
        private void paintRoute(object variables)
        {
            // Remove the route overlay if it exists


                // Starting location
                _points.Add(new PointLatLng(46.293070, -119.290000));
                // Ending Location
                _points.Add(new PointLatLng(46.275019, -119.289959));
                // Load image
                Bitmap carImage = new Bitmap("car-icon-top-view-1.png");

                // Create marker
                GMarkerGoogle car = new GMarkerGoogle(
                    _points[0], carImage);
                routeOverlay.Markers.Add(car);
                car.Tag = 1;

                var route = GoogleMapProvider.Instance
                .GetRoute(_points[0], _points[1], false, false, 15);
                

                // For the my route, make it unique
                GMapRoute newRoute = new GMapRoute(route.Points, "My route");
                

                int pointCount = newRoute.Points.Count();
                
                double distance = new CarOperations().getDistance(_points[0], _points[1]);
                routeOverlay.Routes.Add(newRoute);
                mapView.Overlays.Add(routeOverlay);
                newRoute.Stroke.Width = 4;
                newRoute.Stroke.Color = Color.BlueViolet;
                mapView.Invalidate();
                mapView.UpdateRouteLocalPosition(newRoute);
                List<PointLatLng> extendedPointList = expandPoints(newRoute);
                
                // Animate painting
                animateTimer.Enabled = true;
                long totalticks = 0;
                triptime = Stopwatch.StartNew();
                for (int x = 0; x < extendedPointList.Count(); x++)
                {
                    float heading;
                    
                    car.Position = extendedPointList[x];
                    if(x+1 != extendedPointList.Count())
                {
                    heading = bearing(extendedPointList[x], extendedPointList[x + 1]);
                    car.Bitmap = RotateImage(carImage, heading);
                }
                        
                    // Add the overlay to the map
                    mapView.UpdateMarkerLocalPosition(car);
                                    
                Thread.Sleep(100);
                }
                triptime.Stop();
                totalticks = triptime.ElapsedMilliseconds;
        }
        private void mapView_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            navigationTabs.SelectedIndex = (int)item.Tag + 1;
        }
        

        // Rotates the car image
        private Bitmap RotateImage(Bitmap bmp, float angle)
        {
            Bitmap rotatedImage = new Bitmap(bmp.Width, bmp.Height);
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                // Set the rotation point to the center in the matrix
                g.TranslateTransform(bmp.Width / 2, bmp.Height / 2);
                // Rotate
                g.RotateTransform(angle);
                // Restore rotation point in the matrix
                g.TranslateTransform(-bmp.Width / 2, -bmp.Height / 2);
                // Draw the image on the bitmap
                g.DrawImage(bmp, new Point(0, 0));
            }

            return rotatedImage;
        }
        // Returns the angle to be used to rotate the marker.
        public static float bearing(PointLatLng cur, PointLatLng dest)
        {
            double lon1, lon2, lat1, lat2;
            double conversion = Math.PI / 180;
            lon1 = conversion*(cur.Lng);
            lon2 = conversion * dest.Lng;
            lat1 = conversion * cur.Lat;
            lat2 = conversion * dest.Lat;
            double longDiff = lon2 - lon1;
            double y = Math.Sin(longDiff) * Math.Cos(lat2);
            double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(longDiff);

            return (float)((180/Math.PI)*(Math.Atan2(y, x)) + 360) % 360;
        }
        // This function create a new point list for the animation.
        public List<PointLatLng> expandPoints(GMapRoute route)
        {
            List<PointLatLng> extendedPointList = new List<PointLatLng>();
            for(int x = 0; x < route.Points.Count(); ++x)
            {
                if ((x+1) != route.Points.Count())
                {
                    PointLatLng currentPoint = route.Points[x];
                    PointLatLng nextPoint = route.Points[x + 1];
                    double distance = new CarOperations().getDistance(currentPoint, nextPoint);
                    if (distance > 0.00097222222)
                    {
                        int numberOfNewPoints = (int)(distance / 0.00097222222);
                        extendedPointList.Add(currentPoint);
                        double newPointDistance = 0.00097222222;
                        for (int y = 0; y < numberOfNewPoints; ++y)
                        {
                            double lat, lng;
                            lng = (currentPoint.Lng + ((newPointDistance / distance) * (nextPoint.Lng - currentPoint.Lng)));
                            lat = (currentPoint.Lat + ((newPointDistance / distance) * (nextPoint.Lat - currentPoint.Lat)));
                            PointLatLng newPoint = new PointLatLng(lat, lng);
                            extendedPointList.Add(newPoint);
                            newPointDistance += 0.00097222222;
                        }
                    }
                }
                else
                {
                    extendedPointList.Add(route.Points[x]);
                }

            }
            return extendedPointList;
        }

        //** Sizing Management for car lists **//

        private bool Resizing = false;
        private void carSumList_SizeChanged(object sender, EventArgs e)
        {
            // Don't allow overlapping of SizeChanged calls
            if (!Resizing)
            {
                // Set the resizing flag
                Resizing = true;

                MetroListView carSumList = sender as MetroListView;
                if (carSumList != null)
                {
                    float totalColumnWidth = 0;

                    // Get the sum of all column tags
                    for (int i = 0; i < carSumList.Columns.Count; i++)
                        totalColumnWidth += Convert.ToInt32(carSumList.Columns[i].Tag);

                    // Calculate the percentage of space each column should 
                    // occupy in reference to the other columns and then set the 
                    // width of the column to that percentage of the visible space.
                    for (int i = 0; i < carSumList.Columns.Count; i++)
                    {
                        float colPercentage = (Convert.ToInt32(carSumList.Columns[i].Tag) / totalColumnWidth);
                        carSumList.Columns[i].Width = (int)(colPercentage * carSumList.ClientRectangle.Width);
                    }
                }
            }

            //// Clear the resizing flag
            Resizing = false;
        }
        // Handles the on group click event.
        private void groupClicked(object sender, EventArgs e)
        {
            System.Windows.Forms.ListView.SelectedIndexCollection index = carSumList.SelectedIndices;
            if(index.Count == 5)
            {
                if(carSumList.Items[index[0]].Group.Name == "0")
                {
                    navigationTabs.SelectTab(2);
                }
                else if (carSumList.Items[index[0]].Group.Name == "1")
                {
                    navigationTabs.SelectTab(3);
                }
                else if (carSumList.Items[index[0]].Group.Name == "2")
                {
                    navigationTabs.SelectTab(4);
                }
                else if (carSumList.Items[index[0]].Group.Name == "3")
                {
                    navigationTabs.SelectTab(5);
                }
                carSumList.SelectedItems.Clear();
            }
        }

        public class CustomHeaderCell : DataGridViewRowHeaderCell
        {
            protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
            {
                var size1 = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
                var value = string.Format("{0}", this.DataGridView.Rows[rowIndex].HeaderCell.FormattedValue);
                var size2 = TextRenderer.MeasureText(value, cellStyle.Font);
                var padding = cellStyle.Padding;
                return new Size(size2.Width + padding.Left + padding.Right, size1.Height);
            }
            protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, DataGridViewPaintParts.Background);
                base.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
                TextRenderer.DrawText(graphics, string.Format("{0}", formattedValue), cellStyle.Font, cellBounds, cellStyle.ForeColor);
            }
        }
        private void clearSelected(object sender, EventArgs e)
        {
            for(int x = 0; x < 4; x++)
            {
                if (navigationTabs.SelectedIndex > 1)
                {
                    metroComboBox5.Visible = false;
                    comboBoxNavigation[x].SelectedIndex = navigationTabs.SelectedIndex - 2;
                    serviceLists[x].ClearSelection();
                    serviceLists[x+4].ClearSelection();
                    serviceLists[x + 8].ClearSelection();
                    serviceLists[x + 12].ClearSelection();
                    metroLabel5.Visible = false;
                }
                else
                {
                    metroComboBox5.Visible = true;
                    metroLabel5.Visible = true;
                }
            }
        }
        private void navBoxSelection1(object sender, EventArgs e)
        {
            navigationTabs.SelectedIndex = comboBoxNavigation[0].SelectedIndex + 2;
        }
        private void navBoxSelection2(object sender, EventArgs e)
        {
            navigationTabs.SelectedIndex = comboBoxNavigation[1].SelectedIndex + 2;
        }
        private void navBoxSelection3(object sender, EventArgs e)
        {
            navigationTabs.SelectedIndex = comboBoxNavigation[2].SelectedIndex + 2;
        }
        private void navBoxSelection4(object sender, EventArgs e)
        {
            navigationTabs.SelectedIndex = comboBoxNavigation[3].SelectedIndex + 2;
        }
        private void navBoxSelection5(object sender, EventArgs e)
        {
            navigationTabs.SelectedIndex = comboBoxNavigation[4].SelectedIndex + 2;
        }




        /////////////////////////////////////////////
        // Creates the service lists for each car ///
        public void populateServiceLists()
        {
            serviceLists.Add(metroGrid1);
            serviceLists.Add(metroGrid2);
            serviceLists.Add(metroGrid3);
            serviceLists.Add(metroGrid4);
            comboBoxNavigation.Add(metroComboBox1);
            comboBoxNavigation.Add(metroComboBox2);
            comboBoxNavigation.Add(metroComboBox3);
            comboBoxNavigation.Add(metroComboBox4);
            comboBoxNavigation.Add(metroComboBox5);

            for (int x = 0; x < 4; x++)
            {
                comboBoxNavigation[x].Items.Add("Car One");
                comboBoxNavigation[x].Items.Add("Car Two");
                comboBoxNavigation[x].Items.Add("Car Three");
                comboBoxNavigation[x].Items.Add("Car Four");

                DataGridViewRow row1 = new DataGridViewRow();
                DataGridViewRow row2 = new DataGridViewRow();
                DataGridViewRow row3 = new DataGridViewRow();
                DataGridViewRow row4 = new DataGridViewRow();
                DataGridViewRow row5 = new DataGridViewRow();
                DataGridViewRow row6 = new DataGridViewRow();
                DataGridViewRow row7 = new DataGridViewRow();
                DataGridViewRow row8 = new DataGridViewRow();
                row1.HeaderCell.Value = "Accepted:";
                row2.HeaderCell.Value = "Date:";
                row3.HeaderCell.Value = "Estimated Price:";
                row4.HeaderCell.Value = "Final Price:";
                row5.HeaderCell.Value = "Pay Mode:";
                row6.HeaderCell.Value = "Travel Time:";
                row7.HeaderCell.Value = "Travel Distance:";
                row8.HeaderCell.Value = "Pick-Up Time:";

                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell7 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell8 = new DataGridViewTextBoxCell();

                cell1.Value = carList[x].acepted;
                row1.Cells.Add(cell1);
                cell2.Value = carList[x].initialTime;
                row2.Cells.Add(cell2);
                cell3.Value = carList[x].estimatedPrice;
                row3.Cells.Add(cell3);
                cell4.Value = carList[x].finalPrice;
                row4.Cells.Add(cell4);
                cell5.Value = carList[x].payMode;
                row5.Cells.Add(cell5);
                cell6.Value = carList[x].travelTime;
                row6.Cells.Add(cell6);
                cell7.Value = carList[x].travelDistance;
                row7.Cells.Add(cell7);
                cell8.Value = carList[x].pickupDurationTime;
                row8.Cells.Add(cell8);
                serviceLists[x].Rows.Add(row1);
                serviceLists[x].Rows.Add(row2);
                serviceLists[x].Rows.Add(row3);
                serviceLists[x].Rows.Add(row4);
                serviceLists[x].Rows.Add(row5);
                serviceLists[x].Rows.Add(row6);
                serviceLists[x].Rows.Add(row7);
                serviceLists[x].Rows.Add(row8);

                serviceLists[x].RowTemplate.DefaultHeaderCellType = typeof(CustomHeaderCell);
                serviceLists[x].RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                serviceLists[x].SelectionMode = 0;
            }
            serviceLists.Add(metroGrid5);
            serviceLists.Add(metroGrid6);
            serviceLists.Add(metroGrid7);
            serviceLists.Add(metroGrid8);
            for (int x = 4; x < 8; x++)
            {

                DataGridViewRow row1 = new DataGridViewRow();
                DataGridViewRow row2 = new DataGridViewRow();
                DataGridViewRow row3 = new DataGridViewRow();
                DataGridViewRow row4 = new DataGridViewRow();
                DataGridViewRow row5 = new DataGridViewRow();
                row1.HeaderCell.Value = "User Name:";
                row2.HeaderCell.Value = "User ID:";
                row3.HeaderCell.Value = "Image: ";
                row4.HeaderCell.Value = "Phone Number:";
                row5.HeaderCell.Value = "User Rating:";

                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();

                cell1.Value = carList[x-4].user.username;
                row1.Cells.Add(cell1);
                cell2.Value = carList[x-4].user.uid;
                row2.Cells.Add(cell2);
                cell3.Value = carList[x-4].user.image;
                row3.Cells.Add(cell3);
                cell4.Value = carList[x-4].user.userCellphone;
                row4.Cells.Add(cell4);
                cell5.Value = carList[x-4].user.userStar;
                row5.Cells.Add(cell5);

                serviceLists[x].Rows.Add(row1);
                serviceLists[x].Rows.Add(row2);
                serviceLists[x].Rows.Add(row3);
                serviceLists[x].Rows.Add(row4);
                serviceLists[x].Rows.Add(row5);

                serviceLists[x].RowTemplate.DefaultHeaderCellType = typeof(CustomHeaderCell);
                serviceLists[x].RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                serviceLists[x].SelectionMode = 0;
            }
            serviceLists.Add(metroGrid9);
            serviceLists.Add(metroGrid10);
            serviceLists.Add(metroGrid11);
            serviceLists.Add(metroGrid12);
            for (int x = 8; x < 12; x++)
            {

                DataGridViewRow row1 = new DataGridViewRow();
                DataGridViewRow row2 = new DataGridViewRow();
                DataGridViewRow row3 = new DataGridViewRow();
                DataGridViewRow row4 = new DataGridViewRow();
                DataGridViewRow row5 = new DataGridViewRow();
                row1.HeaderCell.Value = "Company:";
                row2.HeaderCell.Value = "Driver ID:";
                row3.HeaderCell.Value = "Image:";
                row4.HeaderCell.Value = "Car Plate:";
                row5.HeaderCell.Value = "Car Rating:";

                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();

                cell1.Value = carList[x - 8].driver.Company;
                row1.Cells.Add(cell1);
                cell2.Value = carList[x - 8].driver.did;
                row2.Cells.Add(cell2);
                cell3.Value = carList[x - 8].driver.image;
                row3.Cells.Add(cell3);
                cell4.Value = carList[x - 8].driver.car.carPlate;
                row4.Cells.Add(cell4);
                cell5.Value = carList[x - 8].driver.car.carStars;
                row5.Cells.Add(cell5);

                serviceLists[x].Rows.Add(row1);
                serviceLists[x].Rows.Add(row2);
                serviceLists[x].Rows.Add(row3);
                serviceLists[x].Rows.Add(row4);
                serviceLists[x].Rows.Add(row5);

                serviceLists[x].RowTemplate.DefaultHeaderCellType = typeof(CustomHeaderCell);
                serviceLists[x].RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                serviceLists[x].SelectionMode = 0;
            }
            serviceLists.Add(metroGrid13);
            serviceLists.Add(metroGrid14);
            serviceLists.Add(metroGrid15);
            serviceLists.Add(metroGrid16);
            for (int x = 12; x < 16; x++)
            {

                DataGridViewRow row1 = new DataGridViewRow();
                DataGridViewRow row2 = new DataGridViewRow();
                DataGridViewRow row3 = new DataGridViewRow();
                DataGridViewRow row4 = new DataGridViewRow();
                DataGridViewRow row5 = new DataGridViewRow();
                DataGridViewRow row6 = new DataGridViewRow();
                DataGridViewRow row7 = new DataGridViewRow();
                DataGridViewRow row8 = new DataGridViewRow();
                row1.HeaderCell.Value = "Current Lat:";
                row2.HeaderCell.Value = "Current Lng:";
                row3.HeaderCell.Value = "Origin:";
                row4.HeaderCell.Value = "Origin Lat:";
                row5.HeaderCell.Value = "Origin Lng:";
                row6.HeaderCell.Value = "Destination:";
                row7.HeaderCell.Value = "Dest Lat:";
                row8.HeaderCell.Value = "Dest Lng:";

                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell7 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell8 = new DataGridViewTextBoxCell();

                cell1.Value = carList[x - 12].carPosition.lat;
                row1.Cells.Add(cell1);
                cell2.Value = carList[x - 12].carPosition.lng;
                row2.Cells.Add(cell2);
                cell3.Value = carList[x - 12].origin.originName;
                row3.Cells.Add(cell3);
                cell4.Value = carList[x - 12].origin.lat;
                row4.Cells.Add(cell4);
                cell5.Value = carList[x - 12].origin.lng;
                row5.Cells.Add(cell5);
                cell6.Value = carList[x - 12].destination.destinationName;
                row6.Cells.Add(cell6);
                cell7.Value = carList[x - 12].destination.lat;
                row7.Cells.Add(cell7);
                cell8.Value = carList[x - 12].destination.lng;
                row8.Cells.Add(cell8);

                serviceLists[x].Rows.Add(row1);
                serviceLists[x].Rows.Add(row2);
                serviceLists[x].Rows.Add(row3);
                serviceLists[x].Rows.Add(row4);
                serviceLists[x].Rows.Add(row5);
                serviceLists[x].Rows.Add(row6);
                serviceLists[x].Rows.Add(row7);
                serviceLists[x].Rows.Add(row8);

                serviceLists[x].RowTemplate.DefaultHeaderCellType = typeof(CustomHeaderCell);
                serviceLists[x].RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                serviceLists[x].SelectionMode = 0;
            }
            comboBoxNavigation[4].Items.Add("Car One");
            comboBoxNavigation[4].Items.Add("Car Two");
            comboBoxNavigation[4].Items.Add("Car Three");
            comboBoxNavigation[4].Items.Add("Car Four");

        }
        // Clears the position information after a trip is completed
        void clearCarTrip(int carToClear)
        {
            carList[carToClear].acepted = "false";
            carList[carToClear].user = new CarOperations().genUser();
            carList[carToClear].destination = new CarOperations().genDestination();
            carList[carToClear].origin = new CarOperations().genOrigin();
            carList[carToClear].pointList = new List<point>();
        }


    }

}
