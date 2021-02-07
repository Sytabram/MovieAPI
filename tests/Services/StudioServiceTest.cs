using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieAPI.Models;
using MovieAPI.Services;

namespace MovieAPI.Test.Services
{
    [TestClass]
    public class StudioServiceTest
    {
        private readonly IStudiosService _StudiosService = new StudiosService(null);

        [TestMethod]
        public void AddCastTestNullException()
        {
            CreateStudioDto myNullEyxeotionCast = new CreateStudioDto();
            myNullEyxeotionCast = null;
            Task.WaitAll(Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _StudiosService.CreateAsync(myNullEyxeotionCast)));
        }
        [TestMethod]
        public void AddCastTestFirstNameException()
        {
            CreateStudioDto myFirstNameExceptionCast = new CreateStudioDto();
            myFirstNameExceptionCast.Name = "dsjfhuosdjfhuiofsouifhsduiofsduiofhsdioufhsdufuoij";
            Task.WaitAll(Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _StudiosService.CreateAsync(myFirstNameExceptionCast), "Firstname length cannot be greater than 35."));
        }
        [TestMethod]
        public void AddStudioTestDateException()
        {
            CreateStudioDto myYearExceptionMovie = new CreateStudioDto();
            myYearExceptionMovie.Name = "Studio 1";
            myYearExceptionMovie.Creation_date = 12345;
            Task.WaitAll(Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _StudiosService.CreateAsync(myYearExceptionMovie), "Creation_date length must be 4."));
        }
        [TestMethod]
        public void DeleteCastTest()
        {
            CreateStudioDto myDeleteExceptionCast = new CreateStudioDto();
            Task.WaitAll(Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _StudiosService.Delete(-1), "Id cannot be lower than 1."));
        }
    }
}