using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Kuup.Interfaces
{
    interface InterfazGen<T>
    {
        bool Insert();
        bool Delete();
        bool Update();
    }
}
