using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Kuup.Clases
{
    public class ClsAudit
    {
        public short IdAudit { get; set; }
        public String Terminal { get; set; }
        public String IP { get; set; }
        public String Version { get; set; }
        public String NombreDeUsuario { get; set; }
        public DateTime FechaBase { get; set; }
        public String NombreDeFuncionalidad { get; set; }
    }
}
