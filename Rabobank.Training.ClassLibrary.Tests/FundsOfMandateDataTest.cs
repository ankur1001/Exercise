using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rabobank.Training.ClassLibrary.Model;
using static System.Net.Mime.MediaTypeNames;

namespace Rabobank.Training.ClassLibrary.Tests
{
    /// <summary>
    /// The Test of FundsOfMandateData.
    /// </summary>
    [TestClass]
    public class FundsOfMandateDataTest
    {
        /// <summary>
        /// The GetFundOfMandatesFromXML_Success Test.
        /// </summary>
        [TestMethod]
        public void GetFundOfMandatesFromXML_Success()
        {
            string fileName = Path.GetFullPath(@"TestData/FundsOfMandatesData.xml");

            //string fileName = "C:\\Training\\Rabobank.Training.ClassLibrary.Tests\\TestData\\FundsOfMandatesData.xml";
            MandatesService mandatesService = new MandatesService();
            var result = mandatesService.GetFundOfMandatesFromXML(fileName);
            result.Should().NotBeEmpty("Should Not be Empty", result);
        }

        /// <summary>
        /// Test to check if code match found for fundofmandates.
        /// </summary>
        [TestMethod]
        public void CalculateMandates_CodeMatchFound_New()
        {
            MandatesService mandatesService = new MandatesService();

            List<Position> position = this.CreateMockPositionVM();
            List<FundOfMandates> fundOfMandates = this.CreateMockFundOfMandates();

            var result = mandatesService.CalculateMandates(position, fundOfMandates);
            result.Should().NotBeEmpty("Code Mandates found", result);
        }

        /// <summary>
        /// the CalculateMandates_CodeMatchNotFound test.
        /// </summary>
        [TestMethod]
        public void CalculateMandates_CodeMatchNotFound()
        {
            MandatesService mandatesService = new MandatesService();

            List<Position> position = this.CreateMockPositionVM_NoCodeMatch();
            List<FundOfMandates> fundOfMandates = this.CreateMockFundOfMandates();

            var result = mandatesService.CalculateMandates(position, fundOfMandates);
            result[0].Mandates.Should().BeNull("Code Mandates not found", result[0].Mandates);
        }

        /// <summary>
        /// The GetPortfolio_Success.
        /// </summary>
        [TestMethod]
        public void GetPortfolio_Success()
        {
            MandatesService mandatesService = new MandatesService();
            List<FundOfMandates> position = this.CreateMockFundOfMandates();

            var result = mandatesService.GetPortfolio();
            result.Should().NotBeEmpty("Should Not be Empty", result);
        }

        /// <summary>
        /// The CreateMockPositionVM_NoCodeMatch.
        /// </summary>
        /// <returns>Position mock.</returns>
        private List<Position> CreateMockPositionVM_NoCodeMatch()
        {
            return new List<Position>()
            {
                new Position()
                {
                Code = "NL0000292330",
                Value = 45678,
                Name = "Rabobank Core Aandelen Fonds T2",
                 }
            };
        }

        /// <summary>
        /// The CreateMockFundOfMandates.
        /// </summary>
        /// <returns>FundOfMandates.</returns>
        //private List<FundOfMandates> CreateMockFundOfMandates()
        //{
        //    return new List<FundOfMandates>()
        //    {
        //        new FundOfMandates()
        //        {
        //            InstrumentCode = "NL0000292332",
        //            InstrumentName = "Rabobank Core Aandelen Fonds T2",
        //            LiquidityAllocation = 1,
        //            Mandates = new Mandate[]
        //            {
        //                new Mandate()
        //                {
        //                    MandateId = "NL0000292332-01",
        //                    MandateName = "Vanguard MSCI world mandaat",
        //                    Allocation = 49.6m,
        //                },
        //                new Mandate()
        //                {
        //                    MandateId = "NL0000292332-02",
        //                    MandateName = "Blackrock World Stock mandaat",
        //                    Allocation = 49.4m,
        //                },
        //            },
        //        },
        //    };
        //}

        private List<FundOfMandates> CreateMockFundOfMandates()
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

        /// <summary>
        /// The CreateMockPositionVM.
        /// </summary>
        /// <returns>Position Mock.</returns>
        private List<Position> CreateMockPositionVM()
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
    }
}
