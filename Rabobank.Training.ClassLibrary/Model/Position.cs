using System;
using System.Collections.Generic;

namespace Rabobank.Training.ClassLibrary.Model
{
    /// <summary>
    /// The Position.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Gets or sets the Code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets the Mandates.
        /// </summary>
        public List<Mandate> Mandates { get; set; }
    }
}
