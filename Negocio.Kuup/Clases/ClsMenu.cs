using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mod.Entity;

namespace Negocio.Kuup.Clases
{
    public class ClsMenu : Interfaces.InterfazGen<ClsMenu>
    {
        private DBKuupEntities db = null;
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
        public ClsMenu(DBKuupEntities _db)
        {
            db = _db;
        }
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
        private bool ToInsert(DBKuupEntities db)
        {
            Menu Menu = this.ToTable();
            db.Menu.Add(Menu);
            db.Entry(Menu).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.Menu where q.MEN_NUM_MENU == Menu.MEN_NUM_MENU && q.MEN_NUM_PADRE == Menu.MEN_NUM_PADRE && q.MEN_NUM_ORDEN == Menu.MEN_NUM_ORDEN select q).Count() != 0)
            {
                return true;
            }
            return false;
        }
        public bool Insert()
        {
            try
            {
                if (db == null)
                {
                    using (db = new DBKuupEntities())
                    {
                        return ToInsert(db);
                    }
                }
                else
                {
                    return ToInsert(db);
                }
            }
            catch (Exception e)
            {
                ClsBitacora.GeneraBitacora(1, 1, "Insert", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;
            }
        }
        private bool ToDelete(DBKuupEntities db)
        {
            db.Menu.Remove((from q in db.Menu where q.MEN_NUM_MENU == Menu.MEN_NUM_MENU && q.MEN_NUM_PADRE == Menu.MEN_NUM_PADRE && q.MEN_NUM_ORDEN == Menu.MEN_NUM_ORDEN select q).FirstOrDefault());
            db.Entry(Menu).State = EntityState.Deleted;
            db.SaveChanges();
            if ((from q in db.Menu where q.MEN_NUM_MENU == Menu.MEN_NUM_MENU && q.MEN_NUM_PADRE == Menu.MEN_NUM_PADRE && q.MEN_NUM_ORDEN == Menu.MEN_NUM_ORDEN select q).Count() != 0)
            {
                return false;
            }
            return true;
        }
        public bool Delete()
        {
            try
            {
                if (db == null)
                {
                    using (DBKuupEntities db = new DBKuupEntities())
                    {
                        return ToDelete(db);
                    }
                }
                else
                {
                    return ToDelete(db);
                }
            }
            catch (Exception e)
            {
                ClsBitacora.GeneraBitacora(1, 1, "Delete", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;
            }
        }
        private bool ToUpdate(DBKuupEntities db)
        {
            Menu Menu = this.ToTable();
            db.Menu.Attach(Menu);
            db.Entry(Menu).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
        public bool Update()
        {
            try
            {
                if (db == null)
                {
                    using (DBKuupEntities db = new DBKuupEntities())
                    {
                        return ToUpdate(db);
                    }
                }
                else
                {
                    return ToUpdate(db);
                }
            }
            catch (Exception e)
            {
                ClsBitacora.GeneraBitacora(1, 1, "Update", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;
            }
        }
        public Menu ToTable()
        {
            Menu Tabla = new Menu();
            Tabla.MEN_NUM_MENU = this.NumeroDeMenu;
            Tabla.MEN_NUM_PADRE = this.NumeroDeMenuPadre;
            Tabla.MEN_NUM_ORDEN = this.NumeroDeOrden;
            Tabla.MEN_NOM_MENU = this.NombreDeMenu;
            Tabla.MEN_NUM_PANTALLA = this.NumeroDePantalla;
            Tabla.MEN_CVE_ESTATUS = this.CveDeEstatus;
            return Tabla;
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
