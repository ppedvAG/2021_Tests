using FluentAssertions;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pose;
using System;
using System.Diagnostics;
using System.IO;

namespace TDDBank.Tests
{
    [TestClass]
    public class OpenHoursTests
    {
        [TestMethod]
        [DataRow(2021, 03, 15, 10, 29, false)] //mo
        [DataRow(2021, 03, 15, 10, 30, true)] //mo  
        [DataRow(2021, 03, 15, 18, 59, true)] //mo
        [DataRow(2021, 03, 15, 19, 00, false)] //mo  
        [DataRow(2021, 03, 20, 10, 29, false)] //sa  
        [DataRow(2021, 03, 20, 10, 30, true)]//sa  
        [DataRow(2021, 03, 20, 13, 59, true)]//sa  
        [DataRow(2021, 03, 20, 14, 00, false)]//sa  
        [DataRow(2021, 03, 21, 12, 00, false)]//sa  
        public void OpeningHours_IsOpen(int y, int M, int d, int h, int m, bool exp)
        {
            var dt = new DateTime(y, M, d, h, m, 0);
            var oh = new OpeningHours();

            //Assert.AreEqual(exp, oh.IsOpen(dt));
            oh.IsOpen(dt).Should().Be(exp);

            17.Should().BeInRange(10, 19);
            
        }


        [TestMethod]
        public void OpeningHours_IsNowOpen_MS_Fakes()
        {
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2011, 11, 11, 11, 11, 11);
                var oh = new OpeningHours();


                var result = oh.IsNowOpen();

                //Assert.IsTrue(result);
                result.Should().BeFalse("Schade");
            }
        }

        //[TestMethod]
        public void OpeningHours_IsNowOpen_POSE()
        {
            PoseContext.Isolate(() =>
            {
                Shim dateTimeShim = Shim.Replace(() => DateTime.Now).With(() => new DateTime(2011, 11, 11, 1, 11, 11));

                //Shim.Replace(() => File.ReadAllText("A")).With(s => "Hallo Welt");

                Debug.WriteLine(DateTime.Now);
                Debug.WriteLine(File.ReadAllText("A"));
                var oh = new OpeningHours();


                var result = oh.IsNowOpen();

                Assert.IsTrue(result);
                //Assert.IsFalse(result);
            });
        }

    }
}