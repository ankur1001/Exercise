using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Rabobank.Training.ClassLibrary.Model;

namespace Rabobank.Training.ClassLibrary
{
    /// <summary>
    /// The MandatesService.
    /// </summary>
    public class MandatesService : IMandateService
    {
        /// <summary>
        /// Gets the list of positions available.
        /// </summary>
        /// <returns>Portfolio.</returns>
        public List<Portfolio> GetPortfolio()
        {
            try
            {
                return new List<Portfolio>()
                {
                    new Portfolio()
                    {
                        Positions = new List<Position>()
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
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Calculates data for FundofMandates
        /// </summary>
        /// <param name="positions"></param>
        /// <param name="fundOfMandates"></param>
        /// <returns></returns>
        public List<Position> CalculateMandates(List<Position> positions, List<FundOfMandates> fundOfMandates)
        {
            try
            {
                if (positions == null)
                {
                    throw new ArgumentNullException("positions", "Parameter can not be null");
                }

                if (fundOfMandates == null)
                {
                    throw new ArgumentNullException("fundOfMandates", "Parameter can not be null");
                }

                foreach (var p in positions)
                {
                    foreach (var fund in fundOfMandates)
                    {
                        if (p.Code == fund.InstrumentCode)
                        {
                            p.Mandates = new List<Model.Mandate>();
                            for (int i = 0; i < fund.Mandates.Count(); i++)
                            {
                                p.Mandates.Add(new Model.Mandate
                                {
                                    Name = fund.Mandates[i].MandateName,
                                    Allocation = fund.Mandates[i].Allocation,
                                    Value = Math.Round(decimal.Multiply(p.Value, fund.Mandates[i].Allocation) / 100, MidpointRounding.AwayFromZero),
                                });
                            }
                            if (fund.LiquidityAllocation > 0)
                            {
                                decimal total = p.Mandates.Sum(x => x.Value);
                                p.Mandates.Add(new Model.Mandate
                                {
                                    Name = "Liquidity",
                                    Allocation = fund.LiquidityAllocation,
                                    Value = Math.Round(decimal.Multiply(decimal.Subtract(p.Value, total), fund.LiquidityAllocation), MidpointRounding.AwayFromZero),
                                });
                            }
                        }
                    }
                }

                return positions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// The GetFundOfMandatesFromXML - Fetches data from XML.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public List<FundOfMandates> GetFundOfMandatesFromXML(string fileName)
        {
            try
            {
                if (fileName == string.Empty)
                {
                    throw new ArgumentException("Parameter cannot be empty", "fileName");
                }

                if (fileName == null)
                {
                    throw new ArgumentNullException("fileName", "Parameter can not be null");
                }

                if (!File.Exists(fileName))
                {
                    throw new FileNotFoundException("File doesnot exists.", fileName);
                }

                List<FundOfMandates> listFundOfMandates = new List<FundOfMandates>();

                XmlSerializer serializer = new XmlSerializer(typeof(FundsOfMandatesData));
                using (TextReader reader = new StringReader(File.ReadAllText(fileName)))
                {
                    FundsOfMandatesData result = (FundsOfMandatesData)serializer.Deserialize(reader);

                    listFundOfMandates = result.FundsOfMandates.ToList();
                }

                return listFundOfMandates;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
