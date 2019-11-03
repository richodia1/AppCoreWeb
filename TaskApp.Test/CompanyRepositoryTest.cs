
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Web.Interfaces;
using TaskApp.Web.Models;
using TaskApp.Web.Services;

namespace TaskApp.Test
{
    [TestClass]
    public class CompanyRepositoryTest
    {
        [TestMethod]
        public async Task CreateCompanyAsync_WithValidCompany_ShouldSucceed()
        {
            //arrange
            var mockRepo = new Mock<ICompanyRepository>();
            var company = new Company
            {
                ID = 1,
                Name = "Company A",
                Address = "29 good news street"
            };

            mockRepo.Setup(x => x.CreateCompanyAsync(It.IsAny<Company>())).Returns(Task.FromResult<bool>(true));
            var service = new CompanyService(mockRepo.Object);
            //Act
            var result = await service.CreateCompanyAsync(company);
            //Assert
            Assert.IsTrue(result, "Company Created Successfully");
            mockRepo.Verify(repo => repo.CreateCompanyAsync(company), Times.Once);
        }

        [TestMethod]
        public async Task CreateCompanyAsync_withInvalidCompany_ShouldFails()
        {
            //arrange
            var mockRepo = new Mock<ICompanyRepository>();
            mockRepo.Setup(x => x.CreateCompanyAsync(null)).Throws<InvalidOperationException>();
            var service = new CompanyService(mockRepo.Object);

            //Act
            try
            {
                var result = await service.CreateCompanyAsync(null);
                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid Company data", ex.Message);
            }
        }

        [TestMethod]
        public async Task GetCompanyByIdAsync_withValidId_ShouldReturnCompany()
        {
            //Arrange
            var mockRepo = new Mock<ICompanyRepository>();

            var company = new Company
            {
                ID = 1,
                Name = "Company A",
                Address = "29 good news street"
            };

            mockRepo.Setup(x => x.GetCompanyByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(company));
            var service = new CompanyService(mockRepo.Object);

            //Act
            var result = await service.GetCompanyByIdAsync(company.ID);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ID, company.ID);
        }

        [TestMethod]
        public async Task GetCompaniesAsync_ShouldReturnAllCompanies()
        {
            //Arrange
            var mockRepo = new Mock<ICompanyRepository>();

            List<Company> companies = new List<Company>()
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

            mockRepo.Setup(x => x.GetCompaniesAsync()).Returns(Task.FromResult(companies.ToArray()));
            var service = new CompanyService(mockRepo.Object);

            //Act
            var response = await service.GetCompaniesAsync();
            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count(), companies.Count());
        }
    }
}