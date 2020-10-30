using Mod.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Kuup.Interfaces
{
    interface InterfazGen<T>
    {
        DBKuupEntities db { get; set; }
        bool Insert();
        bool ToInsert(DBKuupEntities db);
        bool Delete();
        bool ToDelete(DBKuupEntities db);
        bool Update();
        bool ToUpdate(DBKuupEntities db);
    }
}
