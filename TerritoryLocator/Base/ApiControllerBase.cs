//-----------------------------------------------------------------------
// <copyright file="ApiControllerBase.cs" company="Speridian">
//     Copyright (c) 2011 Speridian. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Security;
using Npgsql;
using TerritoryLocator.Base.Attributes;
using TerritoryLocator.DataLayer;
using TerritoryLocator.Models;
using TerritoryLocator.EmailManager;
namespace TerritoryLocator.Base
{
    /// <summary>
    /// The class which will acts as the base class for all the WebAPI controller classes.
    /// </summary>
    [ApiControllerExceptionFilter]
    public class ApiControllerBase : ApiController
    {

        #region Member Constants

        /// <summary>
        /// The constant string used to access the Request.Headers collection to read the client-sent Cookie value.
        /// </summary>
        private const string Cookie = "Cookie";




        #endregion // Member Constants

        #region Construtor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ApiControllerBase()
        {
            // Initialize DataLayer
            this.DataLayer = new GenericDataLayer<NpgsqlConnection, NpgsqlCommand, NpgsqlDataAdapter, NpgsqlDataReader>(new NpgsqlConnection(ConfigurationManager.ConnectionStrings["TerritoryLocatorConnection"].ConnectionString));

        }

        #endregion Construtor

        #region Member Properties

        /// <summary>
        /// Gets or sets the Session object used to interact with the underlying Raven database.
        /// </summary>
        public IGenericDataLayer<NpgsqlConnection, NpgsqlCommand, NpgsqlDataAdapter, NpgsqlDataReader> DataLayer { get; set; }

        /// <summary>
        /// Gets the instance of the application service class used by the WebAPI controller.This must be overridden by all the
        /// class which inherits this classes
        /// </summary>
        protected virtual ApplicationServiceBase ApplicationService
        {
            get
            {
                return null;
            }
        }


        #endregion Member Properties

        /// <summary>
        /// The asynchronous function which will be executed before each WebAPI requests.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>result of the operation.</returns>
   
        protected virtual void ValidateViewModel()
        {
            if (!ModelState.IsValid)
            {
                // Data is not valid
                // Get error messages and Throw validation exception with corresponding error messages
                var validationErrors = new List<string>();

                foreach (var errorState in ModelState.Values)
                {
                    // there are some validation errors, get them to the collection.
                    validationErrors.AddRange(errorState.Errors.Select(x => x.ErrorMessage));
                }
            }
        }

        /// <summary>
        /// Attaches the cookie.
        /// </summary>
        /// <param name="httpResponseMessage">The HTTP response message.</param>
        /// <returns></returns>
      
        /// <summary>
        /// Gets the logged in user from the passed request data.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        /// <returns>LogonUser that is currently logged in.</returns>
       
        #region PRIVATE HELPER METHODS


        #endregion
    }
}