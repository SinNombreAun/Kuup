using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mod.Entity;

namespace Negocio.Kuup.Clases
{
    class ClsClaves : Interfaces.InterfazGen<ClsClaves>
    {
        private Claves Clave = new Claves();
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
        public bool Insert()
        {
            try 
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    db.Claves.Add(Clave);
                    db.SaveChanges();
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
        public bool Delete()
        {
            throw new NotImplementedException();
        }
        public bool Update()
        {
            throw new NotImplementedException();
        }
    }
}
