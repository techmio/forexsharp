using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using System.Linq;
namespace FXSharp.EA.NewsBox.Specs
{
    [TestFixture]
    public class When_calculate_support_and_resistance_zone
    {
        [Test]
        public void Should_find_turning_point()
        {
            var detector = new ZoneDetector();
            detector.AddClosePrice(1);
            detector.AddClosePrice(2);
            detector.AddClosePrice(3);
            detector.AddClosePrice(2);
            detector.AddClosePrice(1);
            
            Assert.AreEqual(3, detector.TurningPoints.First());
        }

        [Test]
        public void Should_find_all_turning_point()
        {
            var detector = new ZoneDetector();
            detector.AddClosePrice(1);
            detector.AddClosePrice(2);
            detector.AddClosePrice(3);
            detector.AddClosePrice(2);
            detector.AddClosePrice(1);
            detector.AddClosePrice(1);
            detector.AddClosePrice(2);
            detector.AddClosePrice(3);
            detector.AddClosePrice(4);
            detector.AddClosePrice(5);                                                                       
            detector.AddClosePrice(1);

            Assert.That(detector.TurningPoints, Is.EqualTo(new List<double> { 3, 1, 5 }));
        }

        [Test]
        public void Should_find_turning_point_for_all_close()
        {
            var closes = File.ReadLines("EURUSD_WeeklyClose.txt");

            var detector = new ZoneDetector();

            foreach (var close in closes)
            {
                detector.AddClosePrice(Convert.ToDouble(close));
            }
            Console.WriteLine(detector.TurningPoints.Count);
            
            //foreach (var turningPoint in detector.TurningPoints)
            //{
            //    Console.WriteLine(turningPoint);
            //}

            //Console.WriteLine(detector.TurningPoints.Count);

            var orderedTurningPoints = detector.TurningPoints.OrderBy(x => x);

            int previous = 0;
            var deltas = new List<SpZone>();
            foreach (var orderedTurningPoint in orderedTurningPoints)
            {
                int delta = Math.Abs((int)(orderedTurningPoint * 10000) - previous);

                //deltas.Add(delta);
                Console.WriteLine((int)(orderedTurningPoint * 10000) + " " + delta);
                previous = (int)(orderedTurningPoint * 10000);
            }

            //var tops = deltas.OrderByDescending(x => x);

            //foreach (var top in tops)
            //{
            //    Console.WriteLine(top);
            //}
            //Console.WriteLine(deltas.OrderBy().take);
            
        }
    }
}