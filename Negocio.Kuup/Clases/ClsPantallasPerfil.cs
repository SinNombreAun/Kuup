using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mod.Entity;

namespace Negocio.Kuup.Clases
{
    public class ClsPantallasPerfil : Interfaces.InterfazGen<ClsPantallasPerfil>
    {
        DBKuupEntities db { get; set; }
        ViPantallaPerfil PantallaPerfil = new ViPantallaPerfil();
        public short NumeroDePantalla
        {
            get { return PantallaPerfil.PAP_NUM_PANTALLA; }
            set { PantallaPerfil.PAP_NUM_PANTALLA = value; }
        }
        public byte NumeroDePerfil
        {
            get { return PantallaPerfil.PAP_NUM_PERFIL; }
            set { PantallaPerfil.PAP_NUM_PERFIL = value; }
        }
        public byte CveDeEstatus
        {
            get { return PantallaPerfil.PAP_CVE_ESTATUS; }
            set { PantallaPerfil.PAP_CVE_ESTATUS = value; }
        }
        public String NombreDePantalla
        {
            get { return PantallaPerfil.PAP_NOM_PANTALLA; }
            set { PantallaPerfil.PAP_NOM_PANTALLA = value; }
        }
        public String NombreDePantallaInt
        {
            get { return PantallaPerfil.PAP_NOM_PANTALLA_INT; }
            set { PantallaPerfil.PAP_NOM_PANTALLA_INT = value; }
        }
        public String NombreDePerfil
        {
            get { return PantallaPerfil.PAP_NOM_PERFIL; }
            set { PantallaPerfil.PAP_NOM_PERFIL = value; }
        }
        public String TextoDeEstatus
        {
            get { return PantallaPerfil.PAP_TXT_ESTATUS; }
            set { PantallaPerfil.PAP_TXT_ESTATUS = value; }
        }
        public ClsPantallasPerfil() { }
        public ClsPantallasPerfil(ViPantallaPerfil Registro)
        {
            NumeroDePantalla = Registro.PAP_NUM_PANTALLA;
            NumeroDePerfil = Registro.PAP_NUM_PERFIL;
            CveDeEstatus = Registro.PAP_CVE_ESTATUS;
            NombreDePantalla = Registro.PAP_NOM_PANTALLA;
            NombreDePantallaInt = Registro.PAP_NOM_PANTALLA_INT;
            NombreDePerfil = Registro.PAP_NOM_PERFIL;
            TextoDeEstatus = Registro.PAP_TXT_ESTATUS;
        }
        public ClsPantallasPerfil(PantallaPerfil Registro)
        {
            NumeroDePantalla = Registro.PAP_NUM_PANTALLA;
            NumeroDePerfil = Registro.PAP_NUM_PERFIL;
            CveDeEstatus = Registro.PAP_CVE_ESTATUS;
        }
        private bool ToInsert(DBKuupEntities db)
        {
            PantallaPerfil PantallaPerfil = this.ToTable();
            db.PantallaPerfil.Add(PantallaPerfil);
            db.Entry(PantallaPerfil).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.PantallaPerfil where q.PAP_NUM_PERFIL == PantallaPerfil.PAP_NUM_PERFIL && q.PAP_NUM_PANTALLA == PantallaPerfil.PAP_NUM_PANTALLA select q).Count() != 0)
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
            db.PantallaPerfil.Remove((from q in db.PantallaPerfil where q.PAP_NUM_PERFIL == PantallaPerfil.PAP_NUM_PERFIL && q.PAP_NUM_PANTALLA == PantallaPerfil.PAP_NUM_PANTALLA select q).FirstOrDefault());
            db.Entry(PantallaPerfil).State = EntityState.Deleted;
            db.SaveChanges();
            if ((from q in db.PantallaPerfil where q.PAP_NUM_PERFIL == PantallaPerfil.PAP_NUM_PERFIL && q.PAP_NUM_PANTALLA == PantallaPerfil.PAP_NUM_PANTALLA select q).Count() != 0)
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
            PantallaPerfil PantallaPerfil = this.ToTable();
            db.PantallaPerfil.Attach(PantallaPerfil);
            db.Entry(PantallaPerfil).State = EntityState.Modified;
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
        public PantallaPerfil ToTable()
        {
            PantallaPerfil Tabla = new PantallaPerfil();
            Tabla.PAP_NUM_PANTALLA = this.NumeroDePantalla;
            Tabla.PAP_NUM_PERFIL = this.NumeroDePerfil;
            Tabla.PAP_CVE_ESTATUS = this.CveDeEstatus;
            return Tabla;
        }
        public static List<ClsPantallasPerfil> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViPantallaPerfil
                                select new ClsPantallasPerfil()
                                {
                                    NumeroDePantalla = q.PAP_NUM_PANTALLA,
                                    NumeroDePerfil = q.PAP_NUM_PERFIL,
                                    CveDeEstatus = q.PAP_CVE_ESTATUS,
                                    NombreDePantalla = q.PAP_NOM_PANTALLA,
                                    NombreDePantallaInt = q.PAP_NOM_PANTALLA_INT,
                                    NombreDePerfil = q.PAP_NOM_PERFIL,
                                    TextoDeEstatus = q.PAP_TXT_ESTATUS
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.PantallaPerfil
                                select new ClsPantallasPerfil()
                                {
                                    NumeroDePantalla = q.PAP_NUM_PANTALLA,
                                    NumeroDePerfil = q.PAP_NUM_PERFIL,
                                    CveDeEstatus = q.PAP_CVE_ESTATUS,
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsPantallasPerfil>();
        }
    }
}
