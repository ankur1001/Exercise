using System;
using System.Collections.Generic;
using System.Linq;
using Rabobank.Training.ClassLibrary.Model;

namespace Rabobank.Training.WebApp.Models
{
    /// <summary>
    /// The ResponseBuilder.
    /// </summary>
    public class ResponseBuilder
    {
        /// <summary>
        /// Maps the Response.
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public static List<PositionVM> MapResponse(List<Position> positions)
        {
            if (positions == null || !positions.Any())
            {
                return null;
            }

            List<PositionVM> positionVMs = new List<PositionVM>();
            foreach (var item in positions)
            {
                positionVMs.Add(new PositionVM
                {
                    Name = item.Name,
                    Value = item.Value,
                    Code = item.Code,
                    Mandates = MapMandate(item.Mandates),
                });
            }
            return positionVMs;
        }

        /// <summary>
        /// Maps the mandates.
        /// </summary>
        /// <param name="mandates"></param>
        /// <returns></returns>
        private static List<MandateVM> MapMandate(List<Mandate> mandates)
        {
            if (mandates == null || !mandates.Any())
            {
                return null;
            }

            List<MandateVM> mandateVMs = new List<MandateVM>();
            foreach (var mandate in mandates)
            {
                mandateVMs.Add(new MandateVM
                {
                    Name = mandate.Name,
                    Value = mandate.Value,
                    Allocation = mandate.Allocation,
                });
            }

            return mandateVMs;
        }

        public static PortfolioVM MapPortfolio(List<Position> positions)
        {
            if (positions == null || !positions.Any())
            {
                return null;
            }

            PortfolioVM portfolioVM = new PortfolioVM();

            portfolioVM.Positions = MapResponse(positions);

            return portfolioVM;
        }

        //private static PositionVM Create(Position positions)
        //{
        //    if (positions == null)
        //    {
        //        return null;
        //    }

        //    PositionVM positionVMs = new PositionVM
        //    {
        //        Name = positions.Name,
        //        Value = positions.Value,
        //        Code = positions.Code,
        //        Mandates = MapMandate(positions.Mandates)
        //    };

        //    return positionVMs;
        //}
    }
}
