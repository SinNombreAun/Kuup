using Mod.Entity;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations.Builders;
using System.IO.Pipes;
using System.Linq;
using System.Security.Permissions;
namespace Negocio.Kuup.Clases
{
    public class ClsPantallas : Interfaces.InterfazGen<ClsPantallas>
    {
        ViPantalla Pantalla = new ViPantalla();

        public short NumeroDePantalla
        {
            get { return Pantalla.PAN_NUM_PANTALLA; }
            set { Pantalla.PAN_NUM_PANTALLA = value; }
        }
        public String NombreDePantalla
        {
            get { return Pantalla.PAN_NOM_PANTALLA; }
            set { Pantalla.PAN_NOM_PANTALLA = value; }
        }
        public String NombreDePantallaInt
        {
            get { return Pantalla.PAN_NOM_PANTALLA_INT; }
            set { Pantalla.PAN_NOM_PANTALLA_INT = value; }
        }
        public String Descripcion
        {
            get { return Pantalla.PAN_DESCRIPCION; }
            set { Pantalla.PAN_DESCRIPCION = value; }
        }
        public Byte CveManejoInterno
        {
            get { return Pantalla.PAN_CVE_MANEJO_INTERNO; }
            set { Pantalla.PAN_CVE_MANEJO_INTERNO = value; }
        }
        public String Llave
        {
            get { return Pantalla.PAN_LLAVE; }
            set { Pantalla.PAN_LLAVE = value; }
        }
        public Byte CveEstatus
        {
            get { return Pantalla.PAN_CVE_ESTATUS; }
            set { Pantalla.PAN_CVE_ESTATUS = value; }
        }
        public String TextoManejoInterno
        {
            get { return Pantalla.PAN_TXT_MANEJO_INTERNO; }
            set { Pantalla.PAN_TXT_MANEJO_INTERNO = value; }
        }
        public String TextoEstatus
        {
            get { return Pantalla.PAN_TXT_ESTATUS; }
            set { Pantalla.PAN_TXT_ESTATUS = value; }
        }
        public bool Insert()
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    Pantalla Pantalla = this.ToTable();
                    db.Pantalla.Add(Pantalla);
                    db.SaveChanges();
                    if ((from q in db.Pantalla where q.PAN_NUM_PANTALLA == Pantalla.PAN_NUM_PANTALLA select q).Count() != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool Delete()
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    db.Pantalla.Remove((from q in db.Pantalla where q.PAN_NUM_PANTALLA == Pantalla.PAN_NUM_PANTALLA select q).FirstOrDefault());
                    db.SaveChanges();
                    if ((from q in db.Pantalla where q.PAN_NUM_PANTALLA == Pantalla.PAN_NUM_PANTALLA select q).Count() != 0)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool Update()
        {
            throw new NotImplementedException();
        }
        public Pantalla ToTable()
        {
            Pantalla Tabla = new Pantalla();
            Tabla.PAN_NUM_PANTALLA = this.NumeroDePantalla;
            Tabla.PAN_NOM_PANTALLA = this.NombreDePantalla;
            Tabla.PAN_NOM_PANTALLA_INT = this.NombreDePantallaInt;
            Tabla.PAN_DESCRIPCION = this.Descripcion;
            Tabla.PAN_CVE_MANEJO_INTERNO = this.CveManejoInterno;
            Tabla.PAN_LLAVE = this.Llave;
            Tabla.PAN_CVE_ESTATUS = this.CveEstatus;
        }
        public static List<ClsPantallas> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViPantalla
                                select new ClsPantallas()
                                {
                                    NumeroDePantalla = q.PAN_NUM_PANTALLA,
                                    NombreDePantalla = q.PAN_NOM_PANTALLA,
                                    NombreDePantallaInt = q.PAN_NOM_PANTALLA_INT,
                                    Descripcion = q.PAN_DESCRIPCION,
                                    CveManejoInterno = q.PAN_CVE_MANEJO_INTERNO,
                                    Llave = q.PAN_LLAVE,
                                    CveEstatus = q.PAN_CVE_ESTATUS,
                                    TextoManejoInterno = q.PAN_TXT_MANEJO_INTERNO,
                                    TextoEstatus = q.PAN_TXT_ESTATUS
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.Pantalla
                                select new ClsPantallas()
                                {
                                    NumeroDePantalla = q.PAN_NUM_PANTALLA,
                                    NombreDePantalla = q.PAN_NOM_PANTALLA,
                                    NombreDePantallaInt = q.PAN_NOM_PANTALLA_INT,
                                    Descripcion = q.PAN_DESCRIPCION,
                                    CveManejoInterno = q.PAN_CVE_MANEJO_INTERNO,
                                    Llave = q.PAN_LLAVE,
                                    CveEstatus = q.PAN_CVE_ESTATUS
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsPantallas>();
        }
    }
}
