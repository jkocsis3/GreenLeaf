using SQLite;

namespace GreenLeaf.Core.Data.TableDefinitions
{
    [Table("SystemInfo")]
    class SystemInfo
    {
        /// <summary>
        /// The current schema of the database.
        /// </summary>
        public double Schema { get; set; }
    }
}
