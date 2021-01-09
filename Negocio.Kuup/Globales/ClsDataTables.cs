using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Kuup.Globales
{
    public class ClsDataTables : Interfaces.DataTables
    {
        public String draw { get; set; }
        public String start { get; set; }
        public String length { get; set; }
        public String sortColumn { get; set; }
        public String sortColumnDir { get; set; }
        public String searchValue { get; set; }
        public int pageSize { get; set; }
        public int skip { get; set; }
        public int recordsTotal { get; set; }
        public NameValueCollection Form { get; set; }
        public Object DatosJson { get; set; }
        public ClsDataTables(NameValueCollection _Form)
        {
            Form = _Form;
        }
    }
}
