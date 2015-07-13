using System;
using System.Data;

namespace TerritoryLocator.DataLayer
{
    /// <summary>
    /// Generic Data access layer
    /// </summary>
    /// <typeparam name="TConnection">Type of Connection</typeparam>
    /// <typeparam name="TCommand">Type of Command</typeparam>
    /// <typeparam name="TAdapter">Type of Adapter</typeparam>
    /// <typeparam name="TReader">Type of Reader</typeparam>
    public interface IGenericDataLayer<TConnection, TCommand, TAdapter, out TReader>:IDisposable
    {
        /// <summary>
        /// Variable to hold the Connection object.
        /// </summary>
        TConnection Connection { get; set; }

        /// <summary>
        /// Variable to hold the Command object.
        /// </summary>
        TCommand Command { get; set; }

        /// <summary>
        /// Variable to hold the adapter object.
        /// </summary>
        TAdapter Adapter { get; set; }


        /// <summary>
        /// Executes the command and returns Boolean
        /// </summary>
        /// <param name="cmd">Command</param>
        /// <returns></returns>
        bool ExecuteNonQueris(dynamic cmd);

        /// <summary>
        /// Executes the command and returns Integer Count
        /// </summary>
        /// <param name="cmd">command</param>
        /// <returns>Integer Count</returns>
        int ExecuteSclarQuerry(dynamic cmd);

        /// <summary>
        /// Executes the command and returns DataTable
        /// </summary>
        /// <param name="command">Command</param>
        /// <returns>DataTable</returns>
        DataTable ReturnDataTable(dynamic command);

        /// <summary>
        /// Creates the Command object for executing a stored procedure
        /// </summary>
        /// <param name="storedProcedure">Stored Procedure Name</param>
        /// <returns>Command object</returns>
        TCommand ReturnSqlCommand(string storedProcedure);

        /// <summary>
        /// Executes the command and returns DataSet
        /// </summary>
        /// <param name="command">command object</param>
        /// <returns></returns>
        DataSet ReturnDataSet(dynamic command);

        /// <summary>
        /// Executes the  command using a reader
        /// </summary>
        /// <param name="command">command object</param>
        /// <returns></returns>
        TReader DataReader(dynamic command);

        /// <summary>
        ///  The method to open database connection.
        /// </summary>
        TConnection OpenConnection();

        /// <summary>
        ///  The method to close the open database connection.
        /// </summary>
        void CloseConnection();
    }
}
