using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
namespace Mod.Entity
{
    public class ClsSequence
    {
        private Database _Database;
        public ClsSequence(Database Database) { _Database = Database; }
        public short SQ_Producto()
        {
            return _Database.SqlQuery<short>("SELECT NEXT VALUE FOR Kuup.SQ_Producto;").SingleAsync().Result;
        }
        public short SQ_FolioVenta()
        {
            return _Database.SqlQuery<short>("SELECT NEXT VALUE FOR Kuup.SQ_FolioVenta;").SingleAsync().Result;
        }
        public short SQ_FolioSurtido()
        {
            return _Database.SqlQuery<short>("SELECT NEXT VALUE FOR Kuup.SQ_FolioSurtido").SingleAsync().Result;
        }
    }
}
