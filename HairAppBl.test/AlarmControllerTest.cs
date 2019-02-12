using NUnit.Framework;
using HairAppBl.Models;
using HairAppBl.Controller;
using System;
using System.Collections.Generic;

namespace HairAppBl.Tests
{
    public class AlarmControllerTest
    {

    
        [Test]
        public void ControllerCanBeLoaded()
        {
            var dbMock = new Moq.Mock<Interfaces.IDataBase>();
            
            AlarmController ac = new AlarmController(dbMock.Object);
            var days = ac.GetWashDays();
            Assert.NotNull(days);
        }

 







    }
}
