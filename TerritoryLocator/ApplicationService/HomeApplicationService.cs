using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Npgsql;
using TerritoryLocator.Base;
using TerritoryLocator.DataLayer;
using TerritoryLocator.Models;
 
using System.Data;
using NpgsqlTypes;
 
using System.Configuration;
using TerritoryLocator.EmailManager.model;
using TerritoryLocator.EmailManager;
using System.Net;
using System.IO;
 
using TerritoryLocator.Encryptor;
using System.Xml;
using System.Xml.Serialization;
using System.Security.Cryptography;
using Newtonsoft.Json;
 
 
using System.Net.Mail;
using excel = Microsoft.Office.Interop.Excel;
using System.Text;


namespace TerritoryLocator.ApplicationService
{
    public class HomeApplicationService : ApplicationServiceBase, IHomeApplicationService
    {
        #region Private Properties
   
        #endregion

        /// <summary>
        /// Initializes a new instance of the HomeApplicationService class.
        /// </summary>
        public HomeApplicationService()
        {
 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeApplicationService"/> class.
        /// </summary>
        /// <param name="dataLayer">The data layer.</param>
        public HomeApplicationService(IGenericDataLayer<NpgsqlConnection, NpgsqlCommand, NpgsqlDataAdapter, NpgsqlDataReader> dataLayer)
            : this()
        {
            this.DataLayer = dataLayer;

        }

       

       
    }
}