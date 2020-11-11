using System;
using System.Collections.Generic;
using System.Text;

namespace Rabobank.Training.ClassLibrary.Model
{
    /// <summary>
    /// The Mandate class.
    /// </summary>
    public class Mandate
    {
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Allocation.
        /// </summary>
        public decimal Allocation { get; set; }

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        public decimal Value { get; set; }
    }
}
