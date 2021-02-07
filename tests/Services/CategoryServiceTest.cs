using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieAPI.Models;
using MovieAPI.Services;

namespace MovieAPI.Test.Services
{
    
    [TestClass]
    public class CategoryServiceTest
    {
        private readonly ICategoriesService _CategoriesService = new CategoriesService(null);

        [TestMethod]
        public void AddCategoryTestNullException()
        {
            CreateCategoryDto myNullExceptionCategory = new CreateCategoryDto();
            myNullExceptionCategory = null;
            Task.WaitAll(Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _CategoriesService.CreateAsync(myNullExceptionCategory)));
        }
        [TestMethod]
        public void AddCategoryNameException()
        {
            CreateCategoryDto myNameExceptionCategory = new CreateCategoryDto();
            myNameExceptionCategory.Name = "jkfdshbijkdsfbsjdkfdsjkfbjksdfbjksdfbjkfsjbf";
            Task.WaitAll(Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _CategoriesService.CreateAsync(myNameExceptionCategory), "Firstname length cannot be greater than 35."));
        }
        [TestMethod]
        public void DeleteCategoryTest()
        {
            CreateCategoryDto myDeleteExceptionCategory = new CreateCategoryDto();
            Task.WaitAll(Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _CategoriesService.Delete(-1), "Id cannot be lower than 1."));
        }
    }
    
}