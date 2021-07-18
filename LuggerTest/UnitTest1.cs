using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LuggerWPF;

namespace LuggerTest
{
    [TestClass]
    public class ItemTests
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
    }
}
