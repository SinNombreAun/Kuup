using Mod.Entity;

namespace Negocio.Kuup.Interfaces
{
    interface InterfazGen<T>
    {
        DBKuupEntities db { get; set; }
        short NumeroDePantallaKuup { get; }
        bool Insert();
        bool Delete();
        bool Update();
    }
}
