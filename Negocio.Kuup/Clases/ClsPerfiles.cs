using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mod.Entity;

namespace Negocio.Kuup.Clases
{
    public class ClsPerfiles : Interfaces.InterfazGen<ClsPerfiles>
    {
        public DBKuupEntities db { get; set; }
        ViPerfil Perfil = new ViPerfil();
        public byte NumeroDePerfil
        {
            get { return Perfil.PER_NUM_PERFIL; }
            set { Perfil.PER_NUM_PERFIL = value; }
        }
        public String NombreDePerfil
        {
            get { return Perfil.PER_NOM_PERFIL; }
            set { Perfil.PER_NOM_PERFIL = value; }
        }
        public byte CveTipoDePerfil
        {
            get { return Perfil.PER_CVE_TIPO_PERFIL; }
            set { Perfil.PER_CVE_TIPO_PERFIL = value; }
        }
        public byte CveDeEstatus
        {
            get { return Perfil.PER_CVE_ESTATUS; }
            set { Perfil.PER_CVE_ESTATUS = value; }
        }
        public String TextoTipoDePerfil
        {
            get { return Perfil.PER_TXT_TIPO_PERFIL; }
            set { Perfil.PER_TXT_TIPO_PERFIL = value; }
        }
        public String TextoDeEstatus
        {
            get { return Perfil.PER_TXT_ESTATUS; }
            set { Perfil.PER_TXT_ESTATUS = value; }
        }
        public ClsPerfiles() { }
        public ClsPerfiles(Perfil Registro)
        {
            NumeroDePerfil = Registro.PER_NUM_PERFIL;
            NombreDePerfil = Registro.PER_NOM_PERFIL;
            CveTipoDePerfil = Registro.PER_CVE_TIPO_PERFIL;
            CveDeEstatus = Registro.PER_CVE_ESTATUS;
        }
        public ClsPerfiles(ViPerfil Registro)
        {
            NumeroDePerfil = Registro.PER_NUM_PERFIL;
            NombreDePerfil = Registro.PER_NOM_PERFIL;
            CveTipoDePerfil = Registro.PER_CVE_TIPO_PERFIL;
            CveDeEstatus = Registro.PER_CVE_ESTATUS;
            TextoTipoDePerfil = Registro.PER_TXT_TIPO_PERFIL;
            TextoDeEstatus = Registro.PER_TXT_ESTATUS;
        }
        private bool ToInsert(DBKuupEntities db)
        {
            Perfil Perfil = this.ToTable();
            db.Perfil.Add(Perfil);
            db.Entry(Perfil).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.Perfil where q.PER_NUM_PERFIL == Perfil.PER_NUM_PERFIL select q).Count() != 0)
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
            db.Perfil.Remove((from q in db.Perfil where q.PER_NUM_PERFIL == Perfil.PER_NUM_PERFIL select q).FirstOrDefault());
            db.SaveChanges();
            if ((from q in db.Perfil where q.PER_NUM_PERFIL == Perfil.PER_NUM_PERFIL select q).Count() != 0)
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
            Perfil Perfil = this.ToTable();
            db.Perfil.Attach(Perfil);
            db.Entry(Perfil).State = EntityState.Modified;
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
                ClsBitacora.GeneraBitacora(1, 1, "Delete", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;
            }
        }
        public Perfil ToTable()
        {
            Perfil Tabla = new Perfil();
            Tabla.PER_NUM_PERFIL = this.NumeroDePerfil;
            Tabla.PER_NOM_PERFIL = this.NombreDePerfil;
            Tabla.PER_CVE_TIPO_PERFIL = this.CveTipoDePerfil;
            Tabla.PER_CVE_ESTATUS = this.CveDeEstatus;
            return Tabla;
        }
        public static List<ClsPerfiles> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViPerfil
                                select new ClsPerfiles()
                                {
                                    NumeroDePerfil = q.PER_NUM_PERFIL,
                                    NombreDePerfil = q.PER_NOM_PERFIL,
                                    CveTipoDePerfil = q.PER_CVE_TIPO_PERFIL,
                                    CveDeEstatus = q.PER_CVE_ESTATUS,
                                    TextoTipoDePerfil = q.PER_TXT_TIPO_PERFIL,
                                    TextoDeEstatus = q.PER_TXT_ESTATUS
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.Perfil
                                select new ClsPerfiles()
                                {
                                    NumeroDePerfil = q.PER_NUM_PERFIL,
                                    NombreDePerfil = q.PER_NOM_PERFIL,
                                    CveTipoDePerfil = q.PER_CVE_TIPO_PERFIL,
                                    CveDeEstatus = q.PER_CVE_ESTATUS
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsPerfiles>();
        }
    }
}
