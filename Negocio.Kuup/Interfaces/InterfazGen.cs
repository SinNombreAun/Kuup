using Mod.Entity;

namespace Negocio.Kuup.Interfaces
{
    interface InterfazGen<T>
    {
        DBKuupEntities db { get; set; }
        bool Insert();
        bool Delete();
        bool Update();
    }
}
