//-----------------------------------------------------------------------
// <copyright file="MvcControllerBase.cs" company="Speridian">
//     Copyright (c) 2011 Speridian. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
//using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Npgsql;
using TerritoryLocator.DataLayer;
using TerritoryLocator.Models;
 
using TerritoryLocator.Helpers;

namespace TerritoryLocator.Base
{
    public class MvcControllerBase : Controller
    {

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


        public MvcControllerBase()
        {
            // Initialize DataLayer
            this.DataLayer = new GenericDataLayer<NpgsqlConnection, NpgsqlCommand, NpgsqlDataAdapter, NpgsqlDataReader>(new NpgsqlConnection(ConfigurationManager.ConnectionStrings["TerritoryLocatorConnection"].ConnectionString));

        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {

            // validate the application service instance.
            if (this.ApplicationService != null)
            {
                this.ApplicationService.DataLayer = this.DataLayer;
           
            }

            return base.BeginExecute(requestContext, callback, state);
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
       

    }
}