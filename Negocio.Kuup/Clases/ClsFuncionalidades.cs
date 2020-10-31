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
        public class ClsFuncionalidades : Interfaces.InterfazGen<ClsFuncionalidades>
    {
        public DBKuupEntities db { get; set; }
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
        public byte CveEstatus
        {
            get { return Funcionalidad.FUN_CVE_ESTATUS; }
            set { Funcionalidad.FUN_CVE_ESTATUS = value; }
        }
        public String TextoEstatus
        {
            get { return Funcionalidad.FUN_TXT_ESTATUS; }
            set { Funcionalidad.FUN_TXT_ESTATUS = value; }
        }
        private bool ToInsert(DBKuupEntities db)
        {
            Funcionalidad Funcionalidad = this.ToTable();
            db.Funcionalidad.Add(Funcionalidad);
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
        public Funcionalidad ToTable()
        {
            Funcionalidad Tabla = new Funcionalidad();
            Tabla.FUN_NUM_PANTALLA = this.NumeroDePantalla;
            Tabla.FUN_NUM_FUNCIONALIDAD = this.NumeroDeFuncionalidad;
            Tabla.FUN_NOM_FUNCIONALIDAD = this.NombreDeFuncionalidad;
            Tabla.FUN_CVE_ESTATUS = this.CveEstatus;
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
                                    CveEstatus = q.FUN_CVE_ESTATUS,
                                    TextoEstatus = q.FUN_TXT_ESTATUS
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
                                    CveEstatus = q.FUN_CVE_ESTATUS
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
