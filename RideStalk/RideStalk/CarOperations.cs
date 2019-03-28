using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;

namespace RideStalk
{
    class CarOperations
    {
        // Change the call to have the ability to have paramters passed.
        public serviceData genService()
        {
            serviceData newService = new serviceData
            {
                user = genUser(),
                driver = genDriver(),
                carPosition = genPosition(),
                destination = genDestination(),
                origin = genOrigin(),
                pointList = genPointList(),
                initialTime = DateTime.Now.ToString("yy-mm-dd hh:mm:ss"),
                acepted = "false",
                travelDistance = 0.0,
                travelTime = 0,
                payMode = "null",
                pickupDurationTime = 0,
                estimatedPrice = "null",
                finalPrice = "null",
            };
            return newService;
        }
        public object genUser()
        {
            object newUser = new user
            {
                username = "null",
                uid = 0,
                image = "null",
                userCellphone = "null",
                userStar = "null",
            };
            return newUser;
        }
        public object genDriver()
        {
            object newDriver = new driver
            {
                Company = "null",
                did = 0,
                image = "null",
                car = new car
                {
                    carPlate = "null",
                    carStars = "null",
                }
            };
            return newDriver;
        }

        public object genPosition()
        {
            var newPosition = new carPosition
            {
                lat = 0.0,
                lng = 0.0,
            };
            return newPosition;
        }

        public object genDestination()
        {
            var newDestination = new destination
            {
                lat = 0.0,
                lng = 0.0,
                destinationName = "null",
            };
            return newDestination;
        }

        public object genOrigin()
        {
            var newOrigin = new origin
            {
                lat = 0.0,
                lng = 0.0,
                originName = "null",
            };
            return newOrigin;
        }
        public object genPointList()
        {
            var newPointList = new pointList
            {
                point = "null",
            };
            return newPointList;
        }
        public object genPoint()
        {
            var newPoint = new point
            {
                lat = 0.0,
                lng = 0.0,
            };
            return newPoint;
        }
        public object updateUser()
        {
            var carUser = new user
            {
                image = "something or another",
                uid = 12,
                userCellphone = "5099959555",
                username = "billy joe",
                userStar = "2.5",
            };
            return carUser;
        }
    }
}
