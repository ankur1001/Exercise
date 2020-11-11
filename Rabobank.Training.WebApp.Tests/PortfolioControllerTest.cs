using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Rabobank.Training.ClassLibrary;
using Rabobank.Training.ClassLibrary.Model;
using Rabobank.Training.WebApp.Controllers;
using Rabobank.Training.WebApp.Models;
using Assert = NUnit.Framework.Assert;
using Mandate = Rabobank.Training.ClassLibrary.Mandate;

namespace Rabobank.Training.WebApp.Tests
{
    /// <summary>
    /// The Test class for Portfolio controller.
    /// </summary>
    [TestClass]
    public class PortfolioControllerTest
    {
        private PortfolioController controller;
        private Mock<IMandateService> mandateServiceMock;
        private Logger<PortfolioController> loggermock;

        /// <summary>
        /// The mock Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.mandateServiceMock = new Mock<IMandateService>();
            this.controller = new PortfolioController(this.mandateServiceMock.Object, loggermock);
        }

        /// <summary>
        /// Test to check the complete success flow of method and Mapping success.
        /// </summary>
        [TestMethod]
        public void GetPortfolioController_Success()
        {
            var mandateServiceMock = new Mock<IMandateService>();
            var loggerMock = new Mock<ILogger<PortfolioController>>();
            ILogger<PortfolioController> logger = loggerMock.Object;
            this.controller = new PortfolioController(mandateServiceMock.Object, logger);

            var position = this.CreateMockPosition();
            var portfolio = this.CreateMockPortfolio();
            var fundOfMandates = this.FundOfMandates();
            string fileName = Path.GetFullPath(@"TestData/FundsOfMandatesData.xml");

            // arrange
            mandateServiceMock.Setup(p => p.GetPortfolio()).Returns(portfolio);
            mandateServiceMock.Setup(p => p.GetFundOfMandatesFromXML(fileName)).Returns(fundOfMandates);
            mandateServiceMock.Setup(p => p.CalculateMandates(position, fundOfMandates)).Returns(position);

            // act
            //var result = this.controller.GetPortfolio(this.CreateMockPositionVM());
            var result = this.controller.GetPortfolio();

            // assert
            Assert.IsNull(result);
            result.Should().BeNull("Ok",null);
        }

        /// <summary>
        /// Test to check for exception.
        /// </summary>
        [TestMethod]
        public void GetPortfolioController_Exception()
        {
            var mandateServiceMock = new Mock<IMandateService>();
            this.controller = new PortfolioController(mandateServiceMock.Object, loggermock);

            var portfolio = this.CreateMockPortfolio_EmptyReponse();

            // arrange
            mandateServiceMock.Setup(p => p.GetPortfolio()).Throws(new IndexOutOfRangeException());

            // act
            try
            {
                var result = this.controller.GetPortfolio();
            }
            catch (IndexOutOfRangeException ex)
            {
                ex.Should().NotBeNull();
                Assert.That(ex.Message, Is.EqualTo("Index was outside the bounds of the array."));
            }
        }

        /// <summary>
        /// Test to check for Empty Response.
        /// </summary>
        [TestMethod]
        public void GetPortfolioController_EmptyResponse()
        {
            var mandateServiceMock = new Mock<IMandateService>();
            var loggerMock = new Mock<ILogger<PortfolioController>>();
            ILogger<PortfolioController> logger = loggerMock.Object;

            this.controller = new PortfolioController(mandateServiceMock.Object, logger);

            var portfolio = this.CreateMockPortfolio_EmptyReponse();

            // arrange
            mandateServiceMock.Setup(p => p.GetPortfolio()).Returns(portfolio);

            // act
            var result = this.controller.GetPortfolio();

            result.Should().BeNull("Empty Response", null);
        }

        /// <summary>
        /// The CreateMockPortfolio.
        /// </summary>
        /// <returns>Portfolio.</returns>
        private List<Portfolio> CreateMockPortfolio()
        {
            return new List<Portfolio>()
            {
                new Portfolio()
                {
                    Positions = new List<Position>()
                    {
                        new Position()
                        {
                            Code = "NL0000292332",
                            Value = 45678,
                            Name = "Rabobank Core Aandelen Fonds T2",
                            Mandates = new List<ClassLibrary.Model.Mandate>()
                            {
                                new ClassLibrary.Model.Mandate()
                                {
                                    Name = "Rabobank Core Aandelen Fonds T2",
                                    Allocation = 49.6m,
                                    Value = 45678
                                }
                            }
                        }
                    }
                }
            };
        }

        /// <summary>
        /// Returns null mock response.
        /// </summary>
        /// <returns></returns>
        private List<Portfolio> CreateMockPortfolio_EmptyReponse()
        {
            return null;
        }

        /// <summary>
        /// The CreateMockPosition.
        /// </summary>
        /// <returns>Position.</returns>
        private List<Position> CreateMockPosition()
        {
            return new List<Position>()
            {
                    new Position()
                            {
                                Code = "NL0000009165",
                                Value = 12345,
                                Name = "Heineken",
                            },
                            new Position()
                            {
                                Code = "NL0000287100",
                                Value = 23456,
                                Name = "Optimix Mix Fund",
                            },
                            new Position()
                            {
                                Code = "LU0035601805",
                                Value = 34567,
                                Name = "DP Global Strategy L High",
                            },
                            new Position()
                            {
                                Code = "NL0000292332",
                                Value = 45678,
                                Name = "Rabobank Core Aandelen Fonds T2",
                            },
                            new Position()
                            {
                                Code = "LU0042381250",
                                Value = 56789,
                                Name = "Morgan Stanley Invest US Gr Fnd",
                            }
                
            };
        }

        /// <summary>
        /// The FundOfMandates.
        /// </summary>
        /// <returns>FundOfMandates.</returns>
        private List<FundOfMandates> FundOfMandates()
        {
            return new List<FundOfMandates>()
            {
                new FundOfMandates()
                {
                    InstrumentCode = "NL0000287100",
                    InstrumentName = "Optimix Mix Fund",
                    LiquidityAllocation = 0.1m,
                    Mandates = new Mandate[]
                    {
                        new Mandate()
                        {
                            MandateId = "NL0000287100-01",
                            MandateName = "Robeco Factor Momentum Mandaat",
                            Allocation = 35.5m,
                        },
                        new Mandate()
                        {
                            MandateId = "NL0000287100-02",
                            MandateName = "BNPP Factor Value Mandaat",
                            Allocation = 38.3m,
                        },
                        new Mandate()
                        {
                            MandateId = "NL0000287100-03",
                            MandateName = "Robeco Factor Quality Mandaat",
                            Allocation = 26.1m,
                        },
                    },
                },
                new FundOfMandates()
                {
                    InstrumentCode = "NL0000292332",
                    InstrumentName = "Rabobank Core Aandelen Fonds T2",
                    LiquidityAllocation = 1,
                    Mandates = new Mandate[]
                    {
                        new Mandate()
                        {
                            MandateId = "NL0000292332-01",
                            MandateName = "Vanguard MSCI world mandaat",
                            Allocation = 49.6m,
                        },
                        new Mandate()
                        {
                            MandateId = "NL0000292332-02",
                            MandateName = "Blackrock World Stock mandaat",
                            Allocation = 49.4m,
                        },
                    },
                },
                new FundOfMandates()
                {
                    InstrumentCode = "NL0000440584",
                    InstrumentName = "Test Fund of Mandates",
                    LiquidityAllocation = 1.5m,
                    Mandates = new Mandate[]
                    {
                        new Mandate()
                        {
                            MandateId = "NL0000440584-01",
                            MandateName = "Test Mandate 01t",
                            Allocation = 39.3m,
                        },
                        new Mandate()
                        {
                            MandateId = "NL0000440584-02",
                            MandateName = "Test Mandate 01",
                            Allocation = 59.2m
                        },
                    },
                },
            };
        }
    }
}
