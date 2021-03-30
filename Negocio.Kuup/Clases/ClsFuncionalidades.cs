using Mod.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Negocio.Kuup.Clases
{
    public class ClsFuncionalidades : Interfaces.InterfazGen<ClsFuncionalidades>
    {
        private DBKuupEntities _db = null;
        public DBKuupEntities db
        {
            get { return _db; }
            set { _db = value; }
        }
        public short NumeroDePantallaKuup
        {
            get { return 8; }
        }
        ViFuncionalidad Funcionalidad = new ViFuncionalidad();
        public short NumeroDePantalla
        {
            get { return Funcionalidad.FUN_NUM_PANTALLA; }
            set { Funcionalidad.FUN_NUM_PANTALLA = value; }
        }
        public byte NumeroDeFuncionalidad
        {
            get { return Funcionalidad.FUN_NUM_FUNCIONALIDAD; }
            set { Funcionalidad.FUN_NUM_FUNCIONALIDAD = value; }
        }
        public String NombreDeFuncionalidad
        {
            get { return Funcionalidad.FUN_NOM_FUNCIONALIDAD; }
            set { Funcionalidad.FUN_NOM_FUNCIONALIDAD = value; }
        }
        public byte CveDeEstatus
        {
            get { return Funcionalidad.FUN_CVE_ESTATUS; }
            set { Funcionalidad.FUN_CVE_ESTATUS = value; }
        }
        public String NombreDePantalla
        {
            get { return Funcionalidad.FUN_NOM_PANTALLA; }
            set { Funcionalidad.FUN_NOM_PANTALLA = value; }
        }
        public String TextoDeEstatus
        {
            get { return Funcionalidad.FUN_TXT_ESTATUS; }
            set { Funcionalidad.FUN_TXT_ESTATUS = value; }
        }
        public ClsFuncionalidades() { }
        private bool ToInsert(DBKuupEntities db)
        {
            Funcionalidad Funcionalidad = this.ToTable();
            db.Funcionalidad.Add(Funcionalidad);
            db.Entry(Funcionalidad).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.Funcionalidad where q.FUN_NUM_PANTALLA == Funcionalidad.FUN_NUM_PANTALLA && q.FUN_NUM_FUNCIONALIDAD == Funcionalidad.FUN_NUM_FUNCIONALIDAD select q).Count() != 0)
            {
                return true;
            }
            return false;
        }
        public bool Insert()
        {
            try
            {
                if (_db == null)
                {
                    using (DBKuupEntities db = new DBKuupEntities())
                    {
                        return ToInsert(db);
                    }
                }
                else
                {
                    return ToInsert(_db);
                }
            }
            catch (Exception e)
            {
                ClsBitacora.GeneraBitacora(NumeroDePantallaKuup, 1, "Insert", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;
            }
        }
        private bool ToDelete(DBKuupEntities db)
        {
            db.Funcionalidad.Add((from q in db.Funcionalidad where q.FUN_NUM_PANTALLA == Funcionalidad.FUN_NUM_PANTALLA && q.FUN_NUM_FUNCIONALIDAD == Funcionalidad.FUN_NUM_FUNCIONALIDAD select q).FirstOrDefault());
            db.SaveChanges();
            if ((from q in db.Funcionalidad where q.FUN_NUM_PANTALLA == Funcionalidad.FUN_NUM_PANTALLA && q.FUN_NUM_FUNCIONALIDAD == Funcionalidad.FUN_NUM_FUNCIONALIDAD select q).Count() != 0)
            {
                return false;
            }
            return true;
        }
        public bool Delete()
        {
            try
            {
                if (_db == null)
                {
                    using (DBKuupEntities db = new DBKuupEntities())
                    {
                        return ToDelete(db);
                    }
                }
                else
                {
                    return ToDelete(_db);
                }
            }
            catch (Exception e)
            {
                ClsBitacora.GeneraBitacora(NumeroDePantallaKuup, 1, "Delete", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;
            }
        }
        private bool ToUpdate(DBKuupEntities db)
        {
            Funcionalidad Funcionalidad = this.ToTable();
            db.Funcionalidad.Attach(Funcionalidad);
            db.Entry(Funcionalidad).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
        public bool Update()
        {
            try
            {
                if (_db == null)
                {
                    using (DBKuupEntities db = new DBKuupEntities())
                    {
                        return ToUpdate(db);
                    }
                }
                else
                {
                    return ToUpdate(_db);
                }
            }
            catch (Exception e)
            {
                ClsBitacora.GeneraBitacora(NumeroDePantallaKuup, 1, "Update", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;

            }
        }
        public Funcionalidad ToTable()
        {
            Funcionalidad Tabla = new Funcionalidad();
            Tabla.FUN_NUM_PANTALLA = this.NumeroDePantalla;
            Tabla.FUN_NUM_FUNCIONALIDAD = this.NumeroDeFuncionalidad;
            Tabla.FUN_NOM_FUNCIONALIDAD = this.NombreDeFuncionalidad;
            Tabla.FUN_CVE_ESTATUS = this.CveDeEstatus;
            return Tabla;
        }
        public static List<ClsFuncionalidades> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViFuncionalidad
                                select new ClsFuncionalidades()
                                {
                                    NumeroDePantalla = q.FUN_NUM_PANTALLA,
                                    NumeroDeFuncionalidad = q.FUN_NUM_FUNCIONALIDAD,
                                    NombreDeFuncionalidad = q.FUN_NOM_FUNCIONALIDAD,
                                    CveDeEstatus = q.FUN_CVE_ESTATUS,
                                    NombreDePantalla = q.FUN_NOM_PANTALLA,
                                    TextoDeEstatus = q.FUN_TXT_ESTATUS
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.Funcionalidad
                                select new ClsFuncionalidades()
                                {
                                    NumeroDePantalla = q.FUN_NUM_PANTALLA,
                                    NumeroDeFuncionalidad = q.FUN_NUM_FUNCIONALIDAD,
                                    NombreDeFuncionalidad = q.FUN_NOM_FUNCIONALIDAD,
                                    CveDeEstatus = q.FUN_CVE_ESTATUS
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsFuncionalidades>();
        }
    }
}
