using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Mod.Entity;

namespace Negocio.Kuup.Clases
{
    public class ClsClaves : Interfaces.InterfazGen<ClsClaves>
    {
        DBKuupEntities db { get; set; }
        private ViClaves Claves = new ViClaves();
        public byte NumeroDeClave
        {
            get { return Claves.CVE_NUM_CLAVE; }
            set { Claves.CVE_NUM_CLAVE = value; }
        }
        public byte NumeroDeSecuencial
        {
            get { return Claves.CVE_NUM_SEC_CLAVE; }
            set { Claves.CVE_NUM_SEC_CLAVE = value; }
        }
        public String NombreDeClave 
        {
            get { return Claves.CVE_NOM_CLAVE; }
            set { Claves.CVE_NOM_CLAVE = value; }
        }
        public String Descripcion
        {
            get { return Claves.CVE_DESCRIPCION; }
            set { Claves.CVE_DESCRIPCION = value; }
        }
        public String AdicionalI
        {
            get { return Claves.CVE_DATO_ADICIONAL_I; }
            set { Claves.CVE_DATO_ADICIONAL_I = value; }
        }
        public String AdicionalII
        {
            get { return Claves.CVE_DATO_ADICIONAL_II; }
            set { Claves.CVE_DATO_ADICIONAL_II = value; }
        }
        public byte CveDeEstatus
        {
            get { return Claves.CVE_CVE_ESTATUS; }
            set { Claves.CVE_CVE_ESTATUS = value; }
        }
        public String TextoDeEstatus
        {
            get { return Claves.CVE_TXT_ESTATUS; }
            set { Claves.CVE_TXT_ESTATUS = value; }
        }
        public ClsClaves() { }
        public ClsClaves(byte NumeroDeClave, byte SecuencialDeClave, String NombreDeClave, String Descripcion, String DatoAdicionalI, String DatoAdicionalII, byte CveDeEstatus)
        {
            Claves.CVE_NUM_CLAVE = NumeroDeClave;
            Claves.CVE_NUM_SEC_CLAVE = SecuencialDeClave;
            Claves.CVE_NOM_CLAVE = NombreDeClave;
            Claves.CVE_DESCRIPCION = Descripcion;
            Claves.CVE_DATO_ADICIONAL_I = DatoAdicionalI;
            Claves.CVE_DATO_ADICIONAL_II = DatoAdicionalII;
            Claves.CVE_CVE_ESTATUS = CveDeEstatus;
        }
        private bool ToInsert(DBKuupEntities db)
        {
            Claves Claves = this.ToTable();
            db.Claves.Add(Claves);
            db.Entry(Claves).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.Claves where q.CVE_NUM_CLAVE == Claves.CVE_NUM_CLAVE && q.CVE_NUM_SEC_CLAVE == Claves.CVE_NUM_SEC_CLAVE select q).Count() != 0)
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
        public bool ToDelete(DBKuupEntities db)
        {
            db.Claves.Remove((from q in db.Claves where q.CVE_NUM_CLAVE == Claves.CVE_NUM_CLAVE && q.CVE_NUM_SEC_CLAVE == Claves.CVE_NUM_SEC_CLAVE select q).FirstOrDefault());
            db.Entry(Claves).State = EntityState.Deleted;
            db.SaveChanges();
            if ((from q in db.Claves where q.CVE_NUM_CLAVE == Claves.CVE_NUM_CLAVE && q.CVE_NUM_SEC_CLAVE == Claves.CVE_NUM_SEC_CLAVE select q).Count() != 0)
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
            Claves Claves = this.ToTable();
            db.Claves.Attach(Claves);
            db.Entry(Claves).State = EntityState.Modified;
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
        public Claves ToTable()
        {
            Claves Tabla = new Claves();
            Tabla.CVE_NUM_CLAVE = this.NumeroDeClave;
            Tabla.CVE_NUM_SEC_CLAVE = this.NumeroDeSecuencial;
            Tabla.CVE_NOM_CLAVE = this.NombreDeClave;
            Tabla.CVE_DESCRIPCION = this.Descripcion;
            Tabla.CVE_DATO_ADICIONAL_I = this.AdicionalI;
            Tabla.CVE_DATO_ADICIONAL_II = this.AdicionalII;
            Tabla.CVE_CVE_ESTATUS = this.CveDeEstatus;
            return Tabla;
        }
        public static List<ClsClaves> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViClaves
                                select new ClsClaves()
                                {
                                    NumeroDeClave = q.CVE_NUM_CLAVE,
                                    NumeroDeSecuencial = q.CVE_NUM_SEC_CLAVE,
                                    NombreDeClave = q.CVE_NOM_CLAVE,
                                    Descripcion = q.CVE_DESCRIPCION,
                                    AdicionalI = q.CVE_DATO_ADICIONAL_I,
                                    AdicionalII = q.CVE_DATO_ADICIONAL_II,
                                    CveDeEstatus = q.CVE_CVE_ESTATUS,
                                    TextoDeEstatus = q.CVE_TXT_ESTATUS
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.Claves
                                select new ClsClaves()
                                {
                                    NumeroDeClave = q.CVE_NUM_CLAVE,
                                    NumeroDeSecuencial = q.CVE_NUM_SEC_CLAVE,
                                    NombreDeClave = q.CVE_NOM_CLAVE,
                                    Descripcion = q.CVE_DESCRIPCION,
                                    AdicionalI = q.CVE_DATO_ADICIONAL_I,
                                    AdicionalII = q.CVE_DATO_ADICIONAL_II,
                                    CveDeEstatus = q.CVE_CVE_ESTATUS
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsClaves>();
        }
    }
}
