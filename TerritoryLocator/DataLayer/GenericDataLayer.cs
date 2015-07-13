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
    public class GenericDataLayer<TConnection, TCommand, TAdapter, TReader> : IGenericDataLayer<TConnection, TCommand, TAdapter, TReader>
    {

        private bool _disposed;

        /// <summary>
        /// Constructor for initializing the data components
        /// </summary>
        /// <param name="connection">connection object</param>
        public GenericDataLayer (TConnection connection)
        {
            Connection = connection;
            Command = (TCommand)Activator.CreateInstance(typeof(TCommand));
            Adapter = (TAdapter)Activator.CreateInstance(typeof(TAdapter));
        }

        /// <summary>
        /// Variable to hold the Connection object.
        /// </summary>
        public TConnection Connection { get; set; }

        /// <summary>
        /// Variable to hold the Command object.
        /// </summary>
        public TCommand Command { get; set; }

        /// <summary>
        /// Variable to hold the adapter object.
        /// </summary>
        public TAdapter Adapter { get; set; }

        /// <summary>
        /// Variable to hold the connection.
        /// </summary>
        private dynamic DConnection { get { return Connection; } }

        /// <summary>
        /// Variable to hold the adapter.
        /// </summary>
        private dynamic DAdapter { get { return Adapter; } }

        /// <summary>
        /// Executes the command and returns Boolean
        /// </summary>
        /// <param name="cmd">Command</param>
        /// <returns></returns>
        public bool ExecuteNonQueris(dynamic cmd)
        {
            var _return = false;
            try
            {
                cmd.Connection = this.OpenConnection();
                cmd.ExecuteNonQuery();
                _return = true;
            }
            catch (Exception ex)
            {
               // this.Logger.LogException(ex, "ExecuteNonQueris", "DataLayer");
                _return = false;
            }
            finally
            {
                this.CloseConnection();
            }

            return _return;
        }

        /// <summary>
        /// Executes the command and returns Integer Count
        /// </summary>
        /// <param name="cmd">command</param>
        /// <returns>Integer Count</returns>
        public int ExecuteSclarQuerry(dynamic cmd)
        {
            var count = 0;
            try
            {
                cmd.Connection = this.OpenConnection();
                var a = cmd.ExecuteScalar();
                count = a != null ? Convert.ToInt32(a) : 0;
            }
            catch (Exception ex)
            {
                //this.Logger.LogException(ex, "ExecuteSclarQuerry", "DataLayer");

            }
            finally
            {
                this.CloseConnection();

            }

            return count;
        }

        /// <summary>
        /// Executes the command and returns DataTable
        /// </summary>
        /// <param name="command">Command</param>
        /// <returns>DataTable</returns>
        public DataTable ReturnDataTable(dynamic command)
        {
            var dt = new DataTable();
            try
            {
                command.Connection = this.OpenConnection();
                DAdapter.SelectCommand = command;
                DAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                //this.Logger.LogException(ex, "ReturnDataTable", "DataLayer");

            }
            finally
            {
                this.CloseConnection();
            }
            return dt;
        }

        /// <summary>
        /// Creates the Command object for executing a stored procedure
        /// </summary>
        /// <param name="storedProcedure">Stored Procedure Name</param>
        /// <returns>Command object</returns>
        public TCommand ReturnSqlCommand(string storedProcedure)
        {
            dynamic command = (TCommand)Activator.CreateInstance(typeof(TCommand));
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedure;
                return command;
            }
            catch (Exception ex)
            {
               // this.Logger.LogException(ex, "ReturnSqlCommand", "DataLayer");
                return command;
            }
        }

        /// <summary>
        /// Executes the command and returns DataSet
        /// </summary>
        /// <param name="command">command object</param>
        /// <returns></returns>
        public DataSet ReturnDataSet(dynamic command)
        {
            var ds = new DataSet();
            try
            {
                command.Connection = this.OpenConnection();
                DAdapter.SelectCommand = command;
                DAdapter.Fill(ds);
            }
            catch (Exception ex)
            {
                //this.Logger.LogException(ex, "ReturnDataSet", "DataLayer");

            }
            finally
            {
                this.CloseConnection();
            }

            return ds;
        }

        /// <summary>
        /// Executes the  command using a reader
        /// </summary>
        /// <param name="command">command object</param>
        /// <returns></returns>
        public TReader DataReader(dynamic command)
        {
            dynamic reader = null;
            try
            {
                command.Connection = this.OpenConnection();
                command.Connection.BeginTransaction();
                reader = command.ExecuteReader();
                return reader;
            }
            catch
            {
                return reader;
            }
        }

        /// <summary>
        ///  The method to open database connection.
        /// </summary>
        public TConnection OpenConnection()
        {
            this.DConnection.Open();
            return DConnection;
        }

        /// <summary>
        ///  The method to close the open database connection.
        /// </summary>
        public void CloseConnection()
        {
            if (this.DConnection.State == ConnectionState.Open)
            {
                this.DConnection.Close();
            }
        }

        /// <summary>
        /// Method to dispose the object
        /// </summary>
        /// <param name="disposing">flag to set the disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this.CloseConnection();
                }
            }
            this._disposed = true;
        }

        /// <summary>
        ///  Method to dispose the object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
