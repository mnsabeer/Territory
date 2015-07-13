//-----------------------------------------------------------------------
// <copyright file="Extentions.cs" company="Speridian">
//     Copyright (c) 2011 Speridian. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Npgsql;

namespace TerritoryLocator.Base
{
    public static class Extentions
    {
        public static string Between(this string source, string findfrom, params string[] findto)
        {
            // if input string is valid
            if (!string.IsNullOrEmpty(source))
            {

                var start = source.IndexOf(findfrom);
                if (start < 0)
                {
                    return string.Empty;
                }

                // get string between start and end index
                foreach (var to in findto.Select(sto => source.IndexOf(sto, start + findfrom.Length)))
                {
                    return to >= 0 ? source.Substring(start + findfrom.Length, to - start - findfrom.Length) : source.Substring(start + findfrom.Length);
                }
            }

            return string.Empty;

        }


        /// <summary>
        /// method to convert a data table to list
        /// </summary>
        /// <typeparam name="T">output list type</typeparam>
        /// <param name="table">data table to convert </param>
        /// <returns></returns>
        public static IList<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();

            return (from object row in table.Rows select CreateItemFromRow<T>((DataRow)row, properties)).ToList();
        }

        /// <summary>
        /// method assign the properties of custom object from data row
        /// </summary>
        /// <typeparam name="T">custom object type</typeparam>
        /// <param name="row">data row</param>
        /// <param name="properties">custom object properties</param>
        /// <returns></returns>
        private static T CreateItemFromRow<T>(DataRow row, IEnumerable<PropertyInfo> properties) where T : new()
        {
            var item = new T();
            foreach (var property in properties.Where(property => property.PropertyType == row[property.Name].GetType()))
            {
                property.SetValue(item, row[property.Name], null);
            }
            return item;
        }


        /// <summary>
        /// To Get List of objects from Data Reader
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataReader"></param>
        /// <param name="avoidedProperties">Properties to be avoided while object mapping</param>
        /// <returns></returns>
        public static List<T> GetListFromReader<T>(this NpgsqlDataReader dataReader, List<string> avoidedProperties = null) where T : new()
        {
            //Gathering properties to be mapped
            List<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            if (avoidedProperties != null)
                properties = properties.Where(property => avoidedProperties.Contains(property.Name) == false).ToList();

            //Reading and mapping
            var returnList = new List<T>();
            while (dataReader.Read()) { returnList.Add(GetObjectFromReader<T>(dataReader, properties)); }
            return returnList;
        }


        /// <summary>
        /// Mapping Reader to object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataReader"></param>
        /// <param name="properties">Properties to be mapped</param>
        /// <returns></returns>
        public static T GetObjectFromReader<T>(dynamic dataReader, List<PropertyInfo> properties) where T : new()
        {
            var item = new T();
            foreach (var property in properties.Where(property => property.PropertyType == dataReader[property.Name].GetType()))
            {
                property.SetValue(item, dataReader[property.Name], null);
            }
            return item;
        }
    


    }
}