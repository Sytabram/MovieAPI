using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieAPI.Services;
using MovieAPI.Models;

namespace MovieAPI.Test.Services
{
    [TestClass]
    public class MovieServiceTest
    {
        private readonly IMoviesService _MovieService = new MoviesService(null);
        [TestMethod]
        public void AddMovieTestNullException()
        {
            CUMovieDto myNullEyxeotionMovie = new CUMovieDto();
            myNullEyxeotionMovie = null;
            Task.WaitAll(Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _MovieService.AddMovie(myNullEyxeotionMovie)));
        }
        [TestMethod]
        public void AddMovieTestNameException()
        {
            CUMovieDto myNameExceptionMovie = new CUMovieDto();
            myNameExceptionMovie.Name = "odswhfsljkdfhsdljkfhkslfdhklsdhfljksdhfljksdhffdsklfljkdsfhsdlkfhsljkfhfsdlkfhsldkjfhdfhsfdhflks";
            Task.WaitAll(Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _MovieService.AddMovie(myNameExceptionMovie), "Name length cannot be greater than 35."));
        }
        [TestMethod]
        public void AddMovieTestYearException()
        {
            CUMovieDto myYearExceptionMovie = new CUMovieDto();
            myYearExceptionMovie.Name = "My Movie 1";
            myYearExceptionMovie.Year = 12345;
            Task.WaitAll(Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _MovieService.AddMovie(myYearExceptionMovie), "Year length must be 4."));
        }
        [TestMethod]
        public void AddMovieTestRatingException()
        {
            CUMovieDto myPegiExceptionMovie = new CUMovieDto();
            myPegiExceptionMovie.Name = "My Movie 1";
            myPegiExceptionMovie.Rating = 0;
            Task.WaitAll(Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _MovieService.AddMovie(myPegiExceptionMovie), "Rating must be between 1 and 5"));
        }
        [TestMethod]
        public void DeleteMovieTest()
        {
            CUMovieDto myDeleteExceptionMovie = new CUMovieDto();
            Task.WaitAll(Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _MovieService.Delete(-1), "Id cannot be lower than 1."));
        }
    }
    
}