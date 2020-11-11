using System.Collections.Generic;
using System.Linq;
using Rabobank.Training.ClassLibrary.Model;

namespace Rabobank.Training.WebApp.Models
{
    /// <summary>
    /// Creates Mapping of ViewModels with DomainModels.
    /// </summary>
    public class DomainModelMapper
    {
        /// <summary>
        /// Maps the position
        /// </summary>
        /// <param name="portfolios"></param>
        /// <returns></returns>
        public static List<Position> MapPosition(List<Portfolio> portfolios)
        {
            if (portfolios == null || !portfolios.Any())
            {
                return null;
            }

            List<Position> positions = new List<Position>();
            foreach (var item in portfolios)
            {
                positions = item.Positions;
            }

            return positions;
        }
    }
}
