using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LuggerWPF;

namespace LuggerTest
{


    [TestClass]
    public class ItemsTests //lol, these aren't item tests
    {
        [TestMethod]
        public void CircleCircleDistanceTest1()
        {
            Item item = (new Item()
            {
                Owner = null,
                Name = "Big 1",
                Thickness = 1.0,
                Demand = 500,
                Shapes = new ObservableCollection<IShape>(
                    new List<IShape>()
                    {
                        new Rectangle() {X = 0, Y = 0, Height = 90, Width = 150},
                        new Circle() {Diameter = 30, X = 50, Y = 50},
                    }),
            });
            double result = Item.Calculate(item);
            Assert.AreEqual(0.3333, result, 0.01, "Bad circle distance.");
        }

    }




    [TestClass]
    public class ItemTests //lol, these aren't item tests
    {
        [TestMethod]
        public void CircleCircleDistanceTest1()
        {
            Circle cir1 = new Circle() { X = 10, Y = 10, Diameter = 5 };
            Circle cir2 = new Circle() { X = 00, Y = 00, Diameter = 5 };
            double distance = Item.GetDistance(cir1, cir2);
            Assert.AreEqual((Math.Sqrt(2) * 10) - 5, distance, 0.01, "Bad circle distance.");
        }

        [TestMethod]
        public void CircleCircleDistanceTest2()
        {
            Circle cir1 = new Circle() { X = 0, Y = 10, Diameter = 5 };
            Circle cir2 = new Circle() { X = 0, Y = 00, Diameter = 5 };
            double distance = Item.GetDistance(cir1, cir2);
            Assert.AreEqual(5, distance, 0.01, "Bad circle distance.");
        }

        [TestMethod]
        public void CircleCenterOutsideOfRectangle1()
        {
            Circle cir1 = new Circle() { Diameter = 30, X = -20, Y = 20 };
            Rectangle rect1 = new Rectangle() { X = 0, Y = 0, Height = 90, Width = 150 };
            double distance = Item.GetDistance(cir1, rect1);
            Assert.AreEqual(-1, distance, "Non '-1 result when outside is done.");
        }

        [TestMethod]
        public void CircleCenterOutsideOfRectangle2()
        {
            Circle cir1 = new Circle() { Diameter = 30, X = 200, Y = 20 };
            Rectangle rect1 = new Rectangle() { X = 0, Y = 0, Height = 90, Width = 150 };
            double distance = Item.GetDistance(cir1, rect1);
            Assert.AreEqual(-1, distance, "Non '-1 result when outside is done.");

        }

        [TestMethod]
        public void CircleCenterOutsideOfRectangle3()
        {
            Circle cir1 = new Circle() { Diameter = 30, X = 20, Y = -20 };
            Rectangle rect1 = new Rectangle() { X = 0, Y = 0, Height = 90, Width = 150 };
            double distance = Item.GetDistance(cir1, rect1);
            Assert.AreEqual(-1, distance, "Non '-1 result when outside is done.");

        }

        [TestMethod]
        public void CircleCenterOutsideOfRectangle4()
        {
            Circle cir1 = new Circle() { Diameter = 30, X = 20, Y = 200 };
            Rectangle rect1 = new Rectangle() { X = 0, Y = 0, Height = 90, Width = 150 };
            double distance = Item.GetDistance(cir1, rect1);
            Assert.AreEqual(-1, distance, "Non '-1 result when outside is done.");

        }

        [TestMethod]
        public void CircleNodeOutsideOfRectangle1()
        {
            Circle cir1 = new Circle() { Diameter = 30, X = 10, Y = 10 };
            Rectangle rect1 = new Rectangle() { X = 0, Y = 0, Height = 90, Width = 150 };
            double distance = Item.GetDistance(cir1, rect1);
            Assert.AreEqual(-100, distance, 0.01, "Didn't calculate circle nodes to be outside of rectangle.");
        }

        [TestMethod]
        public void CircleDistanceToRectangleEdge1()
        {
            Circle cir1 = new Circle() { Diameter = 20, X = 20, Y = 30 };
            Rectangle rect1 = new Rectangle() { X = 0, Y = 0, Height = 90, Width = 150 };
            double distance = Item.GetDistance(cir1, rect1);
            Assert.AreEqual(10, distance, 0.01, "Bad circle quad nodes to rectangle distance.");
        }


        [TestMethod]
        public void CircleDistanceToRectangleEdge2()
        {
            Circle cir1 = new Circle() { Diameter = 20, X = 20, Y = 20 };
            Rectangle rect1 = new Rectangle() { X = 0, Y = 0, Height = 90, Width = 150 };
            double distance = Item.GetDistance(cir1, rect1);
            Assert.AreEqual(10, distance, 0.01, "Bad circle quad nodes to rectangle distance.");
        }

        [TestMethod]
        public void CircleDistanceToRectangleEdge3()
        {
            Circle cir1 = new Circle() { Diameter = 20, X = 20, Y = 75 };
            Rectangle rect1 = new Rectangle() { X = 0, Y = 0, Height = 90, Width = 150 };
            double distance = Item.GetDistance(cir1, rect1);
            Assert.AreEqual(5, distance, 0.01, "Bad circle quad nodes to rectangle distance.");
        }


        [TestMethod]
        public void CircleDistanceToRectangleEdge4()
        {
            Circle cir1 = new Circle() { Diameter = 20, X = 20, Y = 80 };
            Rectangle rect1 = new Rectangle() { X = 0, Y = 0, Height = 90, Width = 150 };
            double distance = Item.GetDistance(cir1, rect1);
            Assert.AreEqual(0, distance, 0.01, "Bad circle quad nodes to rectangle distance.");
        }


        [TestMethod]
        public void CircleNotOutsideOfRectangle1()
        {
            Circle cir1 = new Circle() { Diameter = 30, X = 20, Y = 20 };
            Rectangle rect1 = new Rectangle() { X = 0, Y = 0, Height = 90, Width = 150 };
            double distance = Item.GetDistance(cir1, rect1);
        }








        [TestMethod]
        public void CircleRectangleDistanceTest1()
        {
            Circle cir1 = new Circle() { Diameter = 30, X = 20, Y = 20 };
            Rectangle rect1 = new Rectangle() { X = 0, Y = 0, Height = 90, Width = 150 };
            double distance = Item.GetDistance(cir1, rect1);
            Assert.AreEqual(5, distance, 0.01, "Bad circle distance.");
        }



    }
}
