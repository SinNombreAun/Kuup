using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Mod.Entity;

namespace Negocio.Kuup.Clases
{
    public class ClsClaves : Interfaces.InterfazGen<ClsClaves>
    {
        private ViClaves Clave = new ViClaves();
        public byte NumeroDeClave
        {
            get { return Clave.CVE_NUM_CLAVE; }
            set { Clave.CVE_NUM_CLAVE = value; }
        }
        public byte NumeroDeSecuencial
        {
            get { return Clave.CVE_NUM_SEC_CLAVE; }
            set { Clave.CVE_NUM_SEC_CLAVE = value; }
        }
        public String NombreDeClave 
        {
            get { return Clave.CVE_NOM_CLAVE; }
            set { Clave.CVE_NOM_CLAVE = value; }
        }
        public String Descripcion
        {
            get { return Clave.CVE_DESCRIPCION; }
            set { Clave.CVE_DESCRIPCION = value; }
        }
        public String AdicionalI
        {
            get { return Clave.CVE_DATO_ADICIONAL_I; }
            set { Clave.CVE_DATO_ADICIONAL_I = value; }
        }
        public String AdicionalII
        {
            get { return Clave.CVE_DATO_ADICIONAL_II; }
            set { Clave.CVE_DATO_ADICIONAL_II = value; }
        }
        public byte CveDeEstatus
        {
            get { return Clave.CVE_CVE_ESTATUS; }
            set { Clave.CVE_CVE_ESTATUS = value; }
        }
        public String TextoDeEstatus
        {
            get { return Clave.CVE_TXT_ESTATUS; }
            set { Clave.CVE_TXT_ESTATUS = value; }
        }
        public ClsClaves() { }
        public ClsClaves(byte NumeroDeClave, byte SecuencialDeClave, String NombreDeClave, String Descripcion, String DatoAdicionalI, String DatoAdicionalII, byte CveDeEstatus)
        {
            Clave.CVE_NUM_CLAVE = NumeroDeClave;
            Clave.CVE_NUM_SEC_CLAVE = SecuencialDeClave;
            Clave.CVE_NOM_CLAVE = NombreDeClave;
            Clave.CVE_DESCRIPCION = Descripcion;
            Clave.CVE_DATO_ADICIONAL_I = DatoAdicionalI;
            Clave.CVE_DATO_ADICIONAL_II = DatoAdicionalII;
            Clave.CVE_CVE_ESTATUS = CveDeEstatus;
        }
        public bool Insert(bool Dependencia = false)
        {
            try 
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    Claves Clave = this.ToTable();
                    db.Claves.Add(Clave);
                    if (!Dependencia)
                    {
                        db.SaveChanges();
                    }
                    if ((from q in db.Claves where q.CVE_NUM_CLAVE == Clave.CVE_NUM_CLAVE && q.CVE_NUM_SEC_CLAVE == Clave.CVE_NUM_SEC_CLAVE select q).Count() != 0){
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
        public bool Delete(bool Dependencia = false)
        {
            throw new NotImplementedException();
        }
        public bool Update(bool Dependencia = false)
        {
            throw new NotImplementedException();
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
