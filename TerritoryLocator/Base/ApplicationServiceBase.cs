//-----------------------------------------------------------------------
// <copyright file="ApplicationServiceBase.cs" company="Speridian">
//     Copyright (c) 2011 Speridian. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Data.SqlClient;
using Npgsql;
using TerritoryLocator.DataLayer;
using TerritoryLocator.Models;
using TerritoryLocator.EmailManager;

namespace TerritoryLocator.Base
{
    /// <summary>
    /// The class which will act as the base class for all the Application service class.
    /// </summary>
    public class ApplicationServiceBase
    {
       

        
        public IGenericDataLayer<NpgsqlConnection, NpgsqlCommand, NpgsqlDataAdapter, NpgsqlDataReader> DataLayer { get; set; }

 
        
        #region Method


        

        /// <summary>
        /// Gets the formatted search string.
        /// </summary>
        /// <param name="stringToFormat">The string to format.</param>
        /// <returns></returns>
        protected string GetFormattedSearchString(string stringToFormat)
        {
            if (stringToFormat != null)
            {
                stringToFormat = stringToFormat.Replace('*', '%');
            }
            return stringToFormat;
        }

        public T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        #endregion Method

    }
}