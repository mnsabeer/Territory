using System;
using System.Collections.Generic;

namespace TerritoryLocator.Validation
{
    /// <summary>
    /// The class which will act as the base class for all the validation classes.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Gets or sets the errors which are in the requests.
        /// </summary>
        public IList<string> Errors { get; set; }

        /// <summary>
        /// Initializes a new instance of the ValidationException class.
        /// </summary>
        public ValidationException()
        {
            this.Errors = new List<string>();
        }

        /// <summary>
        /// Initializes a new instance of the ValidationException class.
        /// </summary>
        /// <param name="errorCode">The error code of the exception message.</param>
        public ValidationException(string errorCode)
            : this()
        {
            this.Errors.Add(errorCode);
        }
    }
}
