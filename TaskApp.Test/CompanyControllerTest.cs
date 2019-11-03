using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Web.Controllers;
using TaskApp.Web.Interfaces;
using TaskApp.Web.Models;
using System.Linq;
using TaskApp.Web.ViewModels;

namespace TaskApp.Test
{
    [TestClass]
    public class CompanyControllerTest
    {

        [TestMethod]
        public async Task Index_GetallCompany_ShouldReturnAllCompany()
        {
            //Arrange
            var mockservice = new Mock<ICompanyService>();
            IEnumerable<Company> companies = new List<Company>()
            {
                new Company
                {
                    ID = 1,
                    Name = "Company A",
                    Address = "29 good news street"
                },
                new Company
                {
                    ID = 2,
                    Name = "Company A",
                    Address = "29 good news street"
                },
                new Company
                {
                    ID = 2,
                    Name = "Company A",
                    Address = "29 good news street"
                }
            };

            mockservice.Setup(x => x.GetCompaniesAsync()).Returns(Task.FromResult(companies));
            var controller = new CompanyController(mockservice.Object);

            //Act
            var actionResult = await controller.Index();

            // Assert
            var okResult = actionResult as ViewResult;
            Assert.IsInstanceOfType(okResult, typeof(ViewResult));
            var companyViewModel = okResult.Model as CompanyViewModel;
            Assert.IsNotNull(companyViewModel);
            Assert.AreEqual(companyViewModel.CompanyList.Count(), companies.Count());
        }
    }
}