using System.Collections.Generic;

namespace Rabobank.Training.WebApp.Models
{
    /// <summary>
    /// The PositionVM.
    /// </summary>
    public class PositionVM
    {
        /// <summary>
        /// Gets or sets Code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Value.
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets Mandates.
        /// </summary>
        public List<MandateVM> Mandates { get; set; }
    }
}
