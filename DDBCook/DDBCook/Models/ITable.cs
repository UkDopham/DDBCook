using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    /// <summary>
    /// interface used to simplifed the mysql connection 
    /// </summary>
    public interface ITable
    {
        /// <summary>
        /// name of the table
        /// </summary>
        /// <returns></returns>
        string GetTableName();

        /// <summary>
        /// properties of the table
        /// </summary>
        /// <returns></returns>
        string GetTableProperties();

        /// <summary>
        /// values of the table 
        /// </summary>
        /// <returns></returns>
        string GetTableValues();
    }
}
