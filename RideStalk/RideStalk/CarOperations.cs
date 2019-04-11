using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;

namespace RideStalk
{
    
    public class CarOperations
    {
        
        // Generate four car objects
        public List<serviceData> generateCars(List<serviceData> carList)
        {
            for (int x = 0; x < 4; ++x)
            {
                carList.Add(new CarOperations().genService());
                Random ranNum = new Random();
                int num = 0;
                num = ranNum.Next(100, 999);
                carList[x].driver.Company = "RideStalk";
                carList[x].driver.did = x + 65;
                carList[x].driver.car.carPlate = $"AWE{num}";
                if (x == 0)
                {
                    carList[0].driver.image = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Albert_Einstein_Head.jpg/220px-Albert_Einstein_Head.jpg";
                    carList[0].driver.car.carStars = 4.9;
                }
                else if (x == 1)
                {
                    carList[1].driver.image = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/25/Grand_Duchess_Anastasia_Nikolaevna_Crisco_edit_letters_removed.jpg/220px-Grand_Duchess_Anastasia_Nikolaevna_Crisco_edit_letters_removed.jpg";
                    carList[1].driver.car.carStars = 4.7;
                }
                else if (x == 2)
                {
                    carList[2].driver.image = "http://www.induslibrary.com/wp-content/uploads/2015/12/Mother-teresa.jpg";
                    carList[2].driver.car.carStars = 4.0;
                }
                else if (x == 3)
                {
                    carList[3].driver.image = "https://cdn.britannica.com/s:300x300/92/60892-004-ED605A68.jpg";
                    carList[3].driver.car.carStars = 5.0;
                }
            }
            return carList;
        }
        // Generate a car object
        public serviceData genService()
        {
            serviceData newService = new serviceData
            {
                user = genUser(),
                driver = genDriver(),
                carPosition = genPosition(),
                destination = genDestination(),
                origin = genOrigin(),
                pointList = new List<point>(),
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
        public user genUser()
        {
            user newUser = new user
            {
                username = "null",
                uid = 0,
                image = "null",
                userCellphone = "null",
                userStar = "null",
            };
            return newUser;
        }
        public driver genDriver()
        {
            driver newDriver = new driver
            {
                Company = "null",
                did = 0,
                image = "null",
                car = new car
                {
                    carPlate = "null",
                    carStars = 0,
                }
            };
            return newDriver;
        }

        public carPosition genPosition()
        {
            carPosition newPosition = new carPosition
            {
                lat = 0.0,
                lng = 0.0,
            };
            return newPosition;
        }

        public destination genDestination()
        {
            destination newDestination = new destination
            {
                lat = 0.0,
                lng = 0.0,
                destinationName = "null",
            };
            return newDestination;
        }

        public origin genOrigin()
        {
            origin newOrigin = new origin
            {
                lat = 0.0,
                lng = 0.0,
                originName = "null",
            };
            return newOrigin;
        }
        public point genPoint()
        {
            point newPoint = new point
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
