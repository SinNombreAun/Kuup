using Mod.Entity;

namespace Negocio.Kuup.Interfaces
{
    interface InterfazGen<T>
    {
        bool Insert();
        bool Delete();
        bool Update();
    }
}
