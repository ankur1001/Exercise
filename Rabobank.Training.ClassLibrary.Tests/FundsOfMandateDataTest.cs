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

        [TestMethod]
        public void GetFundOfMandatesFromXML_ParameterEmpty()
        {
            string fileName = string.Empty;
            MandatesService mandatesService = new MandatesService();
            Action act = () => mandatesService.GetFundOfMandatesFromXML(fileName);

            act.Should().Throw<ArgumentException>()
             .And.Message.Should().Be("Parameter cannot be empty (Parameter 'fileName')");
        }

        [TestMethod]
        public void GetFundOfMandatesFromXML_ParameterNull()
        {
            string fileName = null;
            MandatesService mandatesService = new MandatesService();
            Action act = () => mandatesService.GetFundOfMandatesFromXML(fileName);

            act.Should().Throw<ArgumentNullException>()
             .And.Message.Should().Be("Parameter can not be null (Parameter 'fileName')");
        }

        [TestMethod]
        public void GetFundOfMandatesFromXML_FileNotFound()
        {
            string fileName = Path.GetFullPath(@"TestData.xml"); 
            MandatesService mandatesService = new MandatesService();
            
            Action act = () => mandatesService.GetFundOfMandatesFromXML(fileName);
            act.Should().Throw<FileNotFoundException>()
             .And.Message.Should().Be("File doesnot exists.");
        }

        /// <summary>
        /// Test to check if code match found for fundofmandates.
        /// </summary>
        [TestMethod]
        public void CalculateMandates_CodeMatchFound()
        {
            MandatesService mandatesService = new MandatesService();

            List<Position> position = CreateMockPositionVM();
            List<FundOfMandates> fundOfMandates = CreateMockFundOfMandates();

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

            List<Position> position = CreateMockPositionVM_NoCodeMatch();
            List<FundOfMandates> fundOfMandates = CreateMockFundOfMandates();

            var result = mandatesService.CalculateMandates(position, fundOfMandates);
            result[0].Mandates.Should().BeNull("Code Mandates not found", result[0].Mandates);
        }

        [TestMethod]
        public void CalculateMandates_Null_Positions_Parameter()
        {
            MandatesService mandatesService = new MandatesService();

            List<Position> position = null;
            List<FundOfMandates> fundOfMandates = CreateMockFundOfMandates();

            Action act = () => mandatesService.CalculateMandates(position, fundOfMandates);

            act.Should().Throw<ArgumentNullException>()
             .And.Message.Should().Be("Parameter can not be null (Parameter 'positions')");
        }

        [TestMethod]
        public void CalculateMandates_Null_FundOfMandates_Parameter()
        {
            MandatesService mandatesService = new MandatesService();

            List<Position> position = CreateMockPositionVM();
            List<FundOfMandates> fundOfMandates = null;

            Action act = () => mandatesService.CalculateMandates(position, fundOfMandates);

            act.Should().Throw<ArgumentNullException>()
             .And.Message.Should().Be("Parameter can not be null (Parameter 'fundOfMandates')");
        }

        /// <summary>
        /// The GetPortfolio_Success.
        /// </summary>
        [TestMethod]
        public void GetPortfolio_Success()
        {
            MandatesService mandatesService = new MandatesService();
            List<FundOfMandates> position = CreateMockFundOfMandates();

            var result = mandatesService.GetPortfolio();
            result.Should().NotBeEmpty("Should Not be Empty", result);
        }

        [TestMethod]
        public void CalculateMandates_LiquidityAllocation_LessThanZero()
        {
            MandatesService mandatesService = new MandatesService();

            List<Position> position = CreateMockPositionVM();
            List<FundOfMandates> fundOfMandates = CreateMockFundOfMandates_LiquidityAllocation_LessThanZero();

            var result = mandatesService.CalculateMandates(position, fundOfMandates);
            result[1].Mandates.Should().HaveCount(3, "Liquidity Not Found.");
        }

        [TestMethod]
        public void CalculateMandates_LiquidityAllocation_GreaterThanZero()
        {
            MandatesService mandatesService = new MandatesService();

            List<Position> position = CreateMockPositionVM();
            List<FundOfMandates> fundOfMandates = CreateMockFundOfMandates();

            var result = mandatesService.CalculateMandates(position, fundOfMandates);
            result[1].Mandates.Should().HaveCount(4, "Liquidity Mandate Found.");
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

        private List<FundOfMandates> CreateMockFundOfMandates_LiquidityAllocation_LessThanZero()
        {
            return new List<FundOfMandates>()
            {
                new FundOfMandates()
                {
                    InstrumentCode = "NL0000287100",
                    InstrumentName = "Optimix Mix Fund",
                    LiquidityAllocation = 0,
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
                    LiquidityAllocation = 0,
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
                    LiquidityAllocation = 0,
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
