using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Calculator.Tests
{
    [TestClass]
    public class CalcTests
    {
        [TestMethod]
        public void Calc_Sum_3_and_4_result_7()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(3, 4);

            //Assert
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void Calc_Sum_n3_and_n8_result_n11()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(-3, -8);

            //Assert
            Assert.AreEqual(-11, result);
        }

        [TestMethod]
        public void Calc_Sum_0_and_0_result_0()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(0, 0);

            //Assert
            Assert.AreEqual(0, result);
        }


        [TestMethod]
        [TestCategory("ExceptionsTests")]
        public void Calc_Sum_MAX_and_1_throws_OverflowException()
        {
            var calc = new Calc();

            Assert.ThrowsException<OverflowException>(() => calc.Sum(int.MaxValue, 1));
        }


        [TestMethod]
        [TestCategory("Data Driven Test")]
        [DataRow(1, 4, 5)]
        [DataRow(19, 7, 26)]
        [DataRow(0, 0, 0)]
        [DataRow(-3, -5, -8)]
        [DataRow(-9, 12, 3)]
        public void Calc_Sum(int a, int b, int exp)
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(a, b);

            //Assert
            Assert.AreEqual(exp, result);
        }

    }
}
