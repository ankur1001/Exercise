using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rabobank.Training.ClassLibrary;
using Rabobank.Training.WebApp.Models;

namespace Rabobank.Training.WebApp.Controllers
{
    /// <summary>
    /// The PortfolioController.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class PortfolioController : Controller
    {
        private readonly IMandateService mandateService;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PortfolioController"/> class.
        /// The PortfolioController.
        /// </summary>
        /// <param name="mandateService">mandateService.</param>
        public PortfolioController(IMandateService mandateService, ILogger<PortfolioController> logger, IConfiguration configuration)
        {
            this.mandateService = mandateService;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// The GetPortfolio  - returns position object
        /// </summary>
        /// <returns>.</returns>
        [HttpGet]
        public IEnumerable<PositionVM> GetPortfolio()
        {
            try
            {
                //Get the data of postions static object
                var portfolio = this.mandateService.GetPortfolio();

                var filePath = _configuration["FilePath"];
                //Get funds of Mandate data from file
                string fileName = Path.GetFullPath(filePath);
                var fundOfMandates = this.mandateService.GetFundOfMandatesFromXML(fileName);

                _logger.LogInformation("Received funds of Mandate data from XML.", fileName.Count());

                //Map response and get calculated mandates
                var position = DomainModelMapper.MapPosition(portfolio);
                var calculatedPosition = this.mandateService.CalculateMandates(position, fundOfMandates);

                _logger.LogInformation("Mandate Calculations completed.", position);

                //Map Response from API
                var response = ResponseBuilder.MapResponse(calculatedPosition);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
