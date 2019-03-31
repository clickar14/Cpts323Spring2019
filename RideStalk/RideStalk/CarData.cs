﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Extensions;
using Firebase.Database.Offline;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using Newtonsoft.Json;

namespace RideStalk
{
    // Below is the main services object for a single car
    public class serviceData
    {
        public user user { get; set; }
        public driver driver { get; set; }
        public carPosition carPosition { get; set; }
        public destination destination { get; set; }
        public origin origin { get; set; }
        public pointList pointList { get; set; }
        public string initialTime { get; set; }
        public string acepted { get; set; }
        public double travelDistance { get; set; }
        public int travelTime { get; set; }
        public string payMode { get; set; }
        public int pickupDurationTime { get; set; }
        public string estimatedPrice { get; set; }
        public string finalPrice { get; set; }
        
    }
    public class user
    {
        public string username { get; set; }
        public int uid { get; set; }
        public string image { get; set; }
        public string userCellphone { get; set; }
        public string userStar { get; set; }
    }
    public class driver
    {
        public string Company { get; set; }
        public int did { get; set; }
        public string image { get; set; }
        public car car { get; set; }

    }
    public class car
    {
        public string carPlate { get; set; }
        public string carStars { get; set; }
    }

    public class carPosition
    {
        public double lat { get; set; }
        public double lng { get; set; }
        [JsonProperty("time")]
        public ServerTimeStamp TimestampPlaceholder { get; } = new ServerTimeStamp();
    }
    public class destination
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public string destinationName { get; set; }
        [JsonProperty("time")]
        public ServerTimeStamp TimestampPlaceholder { get; } = new ServerTimeStamp();
    }
    public class origin
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public string originName { get; set; }
        [JsonProperty("time")]
        public ServerTimeStamp TimestampPlaceholder { get; } = new ServerTimeStamp();
    }
    public class pointList
    {
        public point point { set; get; }
    }
    public class point
    {
        public double lat { set; get; }
        public double lng { set; get; }
        [JsonProperty("time")]
        public ServerTimeStamp TimestampPlaceholder { get; } = new ServerTimeStamp();
    }

    public class ServerTimeStamp
    {
        [JsonProperty(".sv")]
        public string TimestampPlaceholder { get; } = "timestamp";
    }

}
