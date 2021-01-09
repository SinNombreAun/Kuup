using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Negocio.Kuup.Interfaces
{
    interface DataTables
    {
        String draw { get; set; }
        String start { get; set; }
        String length { get; set; }
        String sortColumn { get; set; }
        String sortColumnDir { get; set; }
        String searchValue { get; set; }
        int pageSize { get; set;}
        int skip { get;set; }
        int recordsTotal { get; set; }
        NameValueCollection Form { get; set; }
        Object DatosJson { get; set; }
    }
}
