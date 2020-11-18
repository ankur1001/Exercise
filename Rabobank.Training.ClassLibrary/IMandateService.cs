using System.Collections.Generic;
using Rabobank.Training.ClassLibrary.Model;

namespace Rabobank.Training.ClassLibrary
{
    /// <summary>
    /// The IMandateService.
    /// </summary>
    public interface IMandateService
    {
        /// <summary>
        /// Gets the list of postions available.
        /// </summary>
        /// <returns></returns>
        List<Portfolio> GetPortfolio();

        /// <summary>
        /// The GetFundOfMandatesFromXML gets data from XML.
        /// </summary>
        /// <param name="fileName">name of file.</param>
        /// <returns>FundOfMandates.</returns>
        List<FundOfMandates> GetFundOfMandatesFromXML(string fileName);

        /// <summary>
        /// Method calculates fundofmandates.
        /// </summary>
        /// <param name="positions"></param>
        /// <param name="fundOfMandates"></param>
        /// <returns></returns>
        List<Position> CalculateMandates(List<Position> positions, List<FundOfMandates> fundOfMandates);
    }
}
