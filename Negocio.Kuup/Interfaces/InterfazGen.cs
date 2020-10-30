using Mod.Entity;

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
