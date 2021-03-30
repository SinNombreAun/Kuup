using Mod.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Negocio.Kuup.Clases
{
    public class ClsFuncionesPerfiles : Interfaces.InterfazGen<ClsFuncionesPerfiles>
    {
        private DBKuupEntities _db = null;
        public DBKuupEntities db
        {
            get { return _db; }
            set { _db = value; }
        }
        public short NumeroDePantallaKuup
        {
            get { return 9; }
        }
        ViFuncionPerfil FuncionPerfil = new ViFuncionPerfil();
        public short NumeroDePantalla
        {
            get { return FuncionPerfil.FUP_NUM_PANTALLA; }
            set { FuncionPerfil.FUP_NUM_PANTALLA = value; }
        }
        public byte NumeroDeFuncionalidad
        {
            get { return FuncionPerfil.FUP_NUM_FUNCIONALIDAD; }
            set { FuncionPerfil.FUP_NUM_FUNCIONALIDAD = value; }
        }
        public byte NumeroDePerfil
        {
            get { return FuncionPerfil.FUP_NUM_PERFIL; }
            set { FuncionPerfil.FUP_NUM_PERFIL = value; }
        }
        public byte CveDeEstatus
        {
            get { return FuncionPerfil.FUP_CVE_ESTATUS; }
            set { FuncionPerfil.FUP_CVE_ESTATUS = value; }
        }
        public String NombreDePantalla
        {
            get { return FuncionPerfil.FUP_NOM_PANTALLA; }
            set { FuncionPerfil.FUP_NOM_PANTALLA = value; }
        }
        public String NombreDeFuncionalidad
        {
            get { return FuncionPerfil.FUP_NOM_FUNCIONALIDAD; }
            set { FuncionPerfil.FUP_NOM_FUNCIONALIDAD = value; }
        }
        public String NombreDePerfil
        {
            get { return FuncionPerfil.FUP_NOM_PERFIL; }
            set { FuncionPerfil.FUP_NOM_PERFIL = value; }
        }
        public String TextoDeEstatus
        {
            get { return FuncionPerfil.FUP_TXT_ESTATUS; }
            set { FuncionPerfil.FUP_TXT_ESTATUS = value; }
        }
        public ClsFuncionesPerfiles() { }
        private bool ToInsert(DBKuupEntities db)
        {
            FuncionPerfil FuncionPerfil = this.ToTable();
            db.FuncionPerfil.Add(FuncionPerfil);
            db.Entry(FuncionPerfil).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.FuncionPerfil where q.FUP_NUM_PANTALLA == FuncionPerfil.FUP_NUM_PANTALLA && q.FUP_NUM_FUNCIONALIDAD == FuncionPerfil.FUP_NUM_FUNCIONALIDAD && q.FUP_NUM_PERFIL == FuncionPerfil.FUP_NUM_PERFIL select q).Count() != 0)
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
            db.FuncionPerfil.Remove((from q in db.FuncionPerfil where q.FUP_NUM_PANTALLA == FuncionPerfil.FUP_NUM_PANTALLA && q.FUP_NUM_FUNCIONALIDAD == FuncionPerfil.FUP_NUM_FUNCIONALIDAD && q.FUP_NUM_PERFIL == FuncionPerfil.FUP_NUM_PERFIL select q).FirstOrDefault());
            db.SaveChanges();
            if ((from q in db.FuncionPerfil where q.FUP_NUM_PANTALLA == FuncionPerfil.FUP_NUM_PANTALLA && q.FUP_NUM_FUNCIONALIDAD == FuncionPerfil.FUP_NUM_FUNCIONALIDAD && q.FUP_NUM_PERFIL == FuncionPerfil.FUP_NUM_PERFIL select q).Count() != 0)
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
            FuncionPerfil FuncionPerfil = this.ToTable();
            db.FuncionPerfil.Attach(FuncionPerfil);
            db.Entry(FuncionPerfil).State = EntityState.Modified;
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
        public FuncionPerfil ToTable()
        {
            FuncionPerfil Tabla = new FuncionPerfil();
            Tabla.FUP_NUM_PANTALLA = this.NumeroDePantalla;
            Tabla.FUP_NUM_FUNCIONALIDAD = this.NumeroDeFuncionalidad;
            Tabla.FUP_NUM_PERFIL = this.NumeroDePerfil;
            Tabla.FUP_CVE_ESTATUS = this.CveDeEstatus;
            return Tabla;
        }
        public static List<ClsFuncionesPerfiles> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViFuncionPerfil
                                select new ClsFuncionesPerfiles()
                                {
                                    NumeroDePantalla = q.FUP_NUM_PANTALLA,
                                    NumeroDeFuncionalidad = q.FUP_NUM_FUNCIONALIDAD,
                                    NumeroDePerfil = q.FUP_NUM_PERFIL,
                                    CveDeEstatus = q.FUP_CVE_ESTATUS,
                                    NombreDePantalla = q.FUP_NOM_PANTALLA,
                                    NombreDeFuncionalidad = q.FUP_NOM_FUNCIONALIDAD,
                                    NombreDePerfil = q.FUP_NOM_PERFIL,
                                    TextoDeEstatus = q.FUP_TXT_ESTATUS,
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.FuncionPerfil
                                select new ClsFuncionesPerfiles()
                                {
                                    NumeroDePantalla = q.FUP_NUM_PANTALLA,
                                    NumeroDeFuncionalidad = q.FUP_NUM_FUNCIONALIDAD,
                                    NumeroDePerfil = q.FUP_NUM_PERFIL,
                                    CveDeEstatus = q.FUP_CVE_ESTATUS,
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsFuncionesPerfiles>();
        }
    }
}
