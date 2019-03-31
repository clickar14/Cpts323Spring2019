﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

// This allows the console to appear
using System.Runtime.InteropServices;


// Video for routes: https://www.youtube.com/watch?v=FF-PJQxpjOY

namespace RideStalk
{
    public partial class Interface : MetroFramework.Forms.MetroForm
    {
        List<serviceData> carList;
        List<MetroListView> tripSummariesList;
        List<string> carKeys;
        List<ListViewGroup> carGroups;
        List<MetroGrid> serviceLists;
        List<MetroComboBox> comboBoxNavigation;
        List<PointLatLng> _points;
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
            //Create a single list to be used for routes
            _points = new List<PointLatLng>();
            updateTime = 5000;

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
            Thread carPopulateThread = new Thread(runCars);
            carPopulateThread.Start();

            tripSummariesList.Add(carSumList);
            // Move the below to be executed by the thread above later
            carPopulateThread.Join();
            renderLists();
            populateServiceLists();
            this.Size = new Size(1260, 775);


            // Start the thread that manages updating of the list and server values
            updateList = new System.Threading.Thread(realTimeListUpdate);
            updateList.Start();
            


        }
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
            comboBoxNavigation[4].Items.Add("Car One");
            comboBoxNavigation[4].Items.Add("Car Two");
            comboBoxNavigation[4].Items.Add("Car Three");
            comboBoxNavigation[4].Items.Add("Car Four");

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
                for (int x = 0; x < carGroups.Count; x++)
                {
                    int selectedCarNumber = Int32.Parse(carGroups[x].Name);
                    carSumList.Groups[x].Items[0].SubItems[1].Text = carList[selectedCarNumber].user.username;
                    carSumList.Groups[x].Items[1].SubItems[1].Text = carList[selectedCarNumber].driver.did.ToString();
                    carSumList.Groups[x].Items[2].SubItems[1].Text = carList[selectedCarNumber].origin.originName;
                    carSumList.Groups[x].Items[3].SubItems[1].Text = carList[selectedCarNumber].destination.destinationName;
                    carSumList.Groups[x].Items[4].SubItems[1].Text = carList[selectedCarNumber].estimatedPrice.ToString();
                }
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
                        
            for (int x = 0; x < carList.Count; ++x)
            {
                await newService.Child($"{carKeys[x]}").PatchAsync(carList[x]);
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

                // Create columns for the items and subitems.
                // Width of -2 indicates auto-size.


                //Add the items to the ListView.

                carSumList.Items.AddRange(new ListViewItem[] { item1, item2, item3, item4, item5 });

            }
            
            
        }

        private void gmap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            Console.WriteLine(String.Format("Marker {0} was clicked.", item.Tag));
        }


        // This is for the test route button
        private void metroButton1_Click(object sender, EventArgs e)
        {
            Thread paintThread = new Thread(paintRoute);
            paintThread.Start();

            carList[1].destination.destinationName = "Jerrrryyrrr";
            carList[3].acepted = "true";
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

            for (int x = 0; x < 4; ++x)
            {
                carList.Add(new CarOperations().genService());
                var post = await newService.PostAsync(carList[x]);
                carKeys.Add(post.Key);
            }

        }


        // These must be declared as globals
        GMapOverlay routeOverlay = new GMapOverlay("routeInfo");
        private void paintRoute(object variables)
        {
            // Remove the route overlay if it exists
            MethodInvoker routeManage = delegate ()
            {

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
                }
            };
            this.Invoke(routeManage);
           
        }

        // This is the stuff that manages the console delete later
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        

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
    }

}
