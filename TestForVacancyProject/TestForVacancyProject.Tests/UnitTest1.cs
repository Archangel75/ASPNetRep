using System;
using TestForVacancyProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;

namespace TestForVacancyProject.Tests
{
    [TestClass]
    public class TriangleAreaTest
    {
        TriangleArea triangleArea = new TriangleArea(3, 4, 5);
        [TestMethod]
        public void TriangleArea_345_6returned()
        {
            //Arrange
            double expected = 6;

            //Act
            var actual = triangleArea.Area();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        public void TrianglePerimeter_345_12returned()
        {
            double expected = 12;

            var actual = triangleArea.Perimeter();

            Assert.AreEqual(expected, actual);
        }
    }

    [TestClass]
    public class RoundAreaTest
    {
        RoundArea RoundArea = new RoundArea(5);
        [TestMethod]
        public void RoundArea_5_78returned()
        {
            //Arrange
            double expected = 78.54;

            //Act
            var actual = RoundArea.Area();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        public void RoundPerimeter_5_31returned()
        {
            double expected = 31.41;

            var actual = RoundArea.Perimeter();

            Assert.AreEqual(expected, actual);
        }
    }

    [TestClass]
    public class AnyAreaTest
    {        
        AnyArea AnyArea = new AnyArea(new Point[]{new Point(1, 1),
                new Point(2, 3),
                new Point(5, 4),
                new Point(4, 2) });
        [TestMethod]
        public void AnyArea_Points_5returned()
        {      
            //Arrange
            double expected = 5;

            //Act
            var actual = AnyArea.Area();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        public void AnyPerimeter_Points_10returned()
        {
            double expected = 10.8;

            var actual = AnyArea.Perimeter();

            Assert.AreEqual(expected, actual);
        }
    }
}
