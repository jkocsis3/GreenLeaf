using System;
using System.Collections.Generic;
using System.Text;

namespace GreenLeaf.Core.Interfaces
{
    public interface IModel
    {
        /// <summary>
        /// Save the current object to the database
        /// </summary>
        /// <returns></returns>
        bool Save();

        /// <summary>
        /// Delete the current object from the database.
        /// </summary>
        /// <returns></returns>
        bool Delete();
    }
}
