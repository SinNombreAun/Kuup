using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Funciones.Kuup.Adicionales;

namespace Funciones.Kuup.Nodo
{
    public class ClsNodo
    {
        long _Id;
        Dictionary<string, object> _Informacion;
        List<ClsNodo> _NodosHijos;
        public long Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public Dictionary<string, object> Informacion
        {
            get { return _Informacion; }
            set { _Informacion = value; }
        }
        public List<ClsNodo> NodosHijos
        {
            get { return _NodosHijos; }
            set { _NodosHijos = value; }
        }
        public ClsNodo(long Id, Dictionary<string, object> Informacion)
        {
            _Id = Id;
            _Informacion = Informacion;
            NodosHijos = new List<ClsNodo>();
        }
        public bool IntegraNodo(long IdNodoPadre, ClsNodo Nodo)
        {
            if (_Id == IdNodoPadre)
            {
                _NodosHijos.Add(Nodo);
                return true;
            }
            else
            {
                foreach (var item in _NodosHijos)
                {
                    if (item.IntegraNodo(IdNodoPadre, Nodo))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool TieneNodosHijos()
        {
            if (_NodosHijos == null)
            {
                return false;
            }
            return (_NodosHijos.Count > 0);
        }
        public String Dibuja(String Indentacion = "")
        {
            String str = string.Empty;
            str = Indentacion + (this.TieneNodosHijos() ? "+ " : "- ") + _Id.ToString();
            Indentacion += "\t";
            foreach (var item in _NodosHijos)
            {
                str += "\n" + item.Dibuja(Indentacion);
            }
            return str;
        }
        public bool BuscaPantallaEnHijos()
        {
            if (ClsAdicional.Convert<Int16>(_Informacion["NumeroDePantalla"].ToString()) != 0)
            {
                return true;
            }
            if (this.TieneNodosHijos())
            {
                return this.NodosHijos.Exists(x => x.BuscaPantallaEnHijos());
            }
            else
            {
                return false;
            }
        }
        public String Menus(String ruta)
        {
            String str = string.Empty, rutaCadena = string.Empty;
            if (this.TieneNodosHijos() && _Id != 0)
            {
                rutaCadena = ruta;
                if (this.BuscaPantallaEnHijos())
                {
                    str = "\n" + "<li class=\"nav-item dropdown\">";
                    str += "\n" + "<a class=\"nav-link dropdown-toggle\" href=\"#\" id=\"navbarDropdownMenuLink\" role=\"button\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">" + _Informacion["NombreDeMenu"].ToString() + "</a>";
                    str += "\n" + "<div class=\"dropdown-menu\" aria-labelledby=\"navbarDropdownMenuLink\">";

                    foreach (var item in _NodosHijos)
                    {
                        str += "\n" + item.Menus(rutaCadena);
                    }
                    str += "\n" + "</div>";
                    str += "\n" + "</li>";
                }
            }
            else
            {
                if (_Id != 0)
                {
                    if (this.BuscaPantallaEnHijos())
                    {
                        rutaCadena = ruta + "/" + _Informacion["NombreInterno"].ToString();
                        str += "\n" + "<a class=\"dropdown-item\" href=\"" + rutaCadena + "\"> " + _Informacion["NombreDeMenu"].ToString() + "</a>";
                    }
                }
                else
                {
                    foreach (var item in _NodosHijos)
                    {
                        str += "\n" + item.Menus(rutaCadena);
                    }
                }
            }
            return str;
        }
    }
}
