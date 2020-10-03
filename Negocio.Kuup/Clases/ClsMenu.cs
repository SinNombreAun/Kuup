using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mod.Entity;

namespace Negocio.Kuup.Clases
{
    public class ClsMenu : Interfaces.InterfazGen<ClsMenu>
    {
        ViMenu Menu = new ViMenu();
        public short NumeroDeMenu
        {
            get { return Menu.MEN_NUM_MENU; }
            set { Menu.MEN_NUM_MENU = value; }
        }
        public short NumeroDeMenuPadre
        {
            get { return Menu.MEN_NUM_PADRE; }
            set { Menu.MEN_NUM_PADRE = value; }
        }
        public byte NumeroDeOrden
        {
            get { return Menu.MEN_NUM_ORDEN; }
            set { Menu.MEN_NUM_ORDEN = value; }
        }
        public String NombreDeMenu
        {
            get { return Menu.MEN_NOM_MENU; }
            set { Menu.MEN_NOM_MENU = value; }
        }
        public short NumeroDePantalla
        {
            get { return Menu.MEN_NUM_PANTALLA; }
            set { Menu.MEN_NUM_PANTALLA = value; }
        }
        public byte CveDeEstatus
        {
            get { return Menu.MEN_CVE_ESTATUS; }
            set { Menu.MEN_CVE_ESTATUS = value; }
        }
        public String NombreDePantalla
        {
            get { return Menu.MEN_NOM_PANTALLA; }
            set { Menu.MEN_NOM_PANTALLA = value; }
        }
        public String NombreDePantallaInt
        {
            get { return Menu.MEN_NOM_PANTALLA_INT; }
            set { Menu.MEN_NOM_PANTALLA_INT = value; }
        }
        public String TextoDeEstatus
        {
            get { return Menu.MEN_TXT_ESTATUS; }
            set { Menu.MEN_TXT_ESTATUS = value; }
        }
        public ClsMenu() { }
        public ClsMenu(ViMenu Registro) {
            NumeroDeMenu = Registro.MEN_NUM_MENU;
            NumeroDeMenuPadre = Registro.MEN_NUM_PADRE;
            NumeroDeOrden = Registro.MEN_NUM_ORDEN;
            NombreDeMenu = Registro.MEN_NOM_MENU;
            NumeroDePantalla = Registro.MEN_NUM_PANTALLA;
            CveDeEstatus = Registro.MEN_CVE_ESTATUS;
            NombreDePantalla = Registro.MEN_NOM_PANTALLA;
            NombreDePantallaInt = Registro.MEN_NOM_PANTALLA_INT;
            TextoDeEstatus = Registro.MEN_TXT_ESTATUS;
        }
        public ClsMenu(Menu Registro)
        {
            NumeroDeMenu = Registro.MEN_NUM_MENU;
            NumeroDeMenuPadre = Registro.MEN_NUM_PADRE;
            NumeroDeOrden = Registro.MEN_NUM_ORDEN;
            NombreDeMenu = Registro.MEN_NOM_MENU;
            NumeroDePantalla = Registro.MEN_NUM_PANTALLA;
            CveDeEstatus = Registro.MEN_CVE_ESTATUS;
        }
        public bool Insert()
        {
            throw new NotImplementedException();
        }
        public bool Delete()
        {
            throw new NotImplementedException();
        }
        public bool Update()
        {
            throw new NotImplementedException();
        }
        public static List<ClsMenu> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViMenu
                                select new ClsMenu()
                                {
                                    NumeroDeMenu = q.MEN_NUM_MENU,
                                    NumeroDeMenuPadre = q.MEN_NUM_PADRE,
                                    NumeroDeOrden = q.MEN_NUM_ORDEN,
                                    NombreDeMenu = q.MEN_NOM_MENU,
                                    NumeroDePantalla = q.MEN_NUM_PANTALLA,
                                    CveDeEstatus = q.MEN_CVE_ESTATUS,
                                    NombreDePantalla = q.MEN_NOM_PANTALLA,
                                    NombreDePantallaInt = q.MEN_NOM_PANTALLA_INT,
                                    TextoDeEstatus = q.MEN_TXT_ESTATUS
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.Menu
                                select new ClsMenu()
                                {
                                    NumeroDeMenu = q.MEN_NUM_MENU,
                                    NumeroDeMenuPadre = q.MEN_NUM_PADRE,
                                    NumeroDeOrden = q.MEN_NUM_ORDEN,
                                    NombreDeMenu = q.MEN_NOM_MENU,
                                    NumeroDePantalla = q.MEN_NUM_PANTALLA,
                                    CveDeEstatus = q.MEN_CVE_ESTATUS
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsMenu>();
        }
    }
}
