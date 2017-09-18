using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PizzaButikenOnline.Controllers;
using PizzaButikenOnline.Models;
using PizzaButikenOnline.Repositories;

namespace PizzaButikenOnline.Test.Controllers.Api
{
    [TestClass]
    public class CategoryControllerTest
    {
        private CategoriesController _controller;
        public CategoryControllerTest()
        {
            var mockCategoryRepository = new Mock<IRepository<Category>>();

            _controller = new CategoriesController(mockCategoryRepository.Object);
        }

        [TestMethod]
        public void Delete_NoDishFoundWithGivenId_ShouldReturnNotFound()
        {
            var category = new Category
            {
                Id = 1,
                Name = "Pizzor"
            };
            _controller.Edit(1, category).Should().BeOfType<NotFoundResult>();
        }
    }
}
