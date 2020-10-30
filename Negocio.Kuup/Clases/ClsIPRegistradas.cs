using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mod.Entity;

namespace Negocio.Kuup.Clases
{
    public class ClsIPRegistradas : Interfaces.InterfazGen<ClsIPRegistradas>
    {
        ViIPRegistradas IPRegistrada = new ViIPRegistradas();
        public short NumeroDeUsuario
        {
            get { return IPRegistrada.IPR_NUM_USUARIO; }
            set { IPRegistrada.IPR_NUM_USUARIO = value; }
        }
        public String Terminal
        {
            get { return IPRegistrada.IPR_TERMINAL; }
            set { IPRegistrada.IPR_TERMINAL = value; }
        }
        public String IP
        {
            get { return IPRegistrada.IPR_IP; }
            set { IPRegistrada.IPR_IP = value; }
        }
        public byte CveTipoDeAcceso
        {
            get { return IPRegistrada.IPR_CVE_TIPOACCESO; }
            set { IPRegistrada.IPR_CVE_TIPOACCESO = value; }
        }
        public String NombreDeUsuario
        {
            get { return IPRegistrada.IPR_NOM_USUARIO; }
            set { IPRegistrada.IPR_NOM_USUARIO = value; }
        }
        public String TextoTipoDeAccedo
        {
            get { return IPRegistrada.IPR_TXT_TIPOACCESO; }
            set { IPRegistrada.IPR_TXT_TIPOACCESO = value; }
        }
        public ClsIPRegistradas() { }
        public ClsIPRegistradas(IPRegistradas Registro)
        {
            NumeroDeUsuario = Registro.IPR_NUM_USUARIO;
            Terminal = Registro.IPR_TERMINAL;
            IP = Registro.IPR_IP;
            CveTipoDeAcceso = Registro.IPR_CVE_TIPOACCESO;
        }
        public ClsIPRegistradas(ViIPRegistradas Registro)
        {
            NumeroDeUsuario = Registro.IPR_NUM_USUARIO;
            Terminal = Registro.IPR_TERMINAL;
            IP = Registro.IPR_IP;
            CveTipoDeAcceso = Registro.IPR_CVE_TIPOACCESO;
            NombreDeUsuario = Registro.IPR_NOM_USUARIO;
            TextoTipoDeAccedo = Registro.IPR_TXT_TIPOACCESO;
        }
        public bool Insert(bool Dependencia = false)
        {
            throw new NotImplementedException();
        }
        public bool Delete(bool Dependencia = false)
        {
            throw new NotImplementedException();
        }
        public bool Update(bool Dependencia = false)
        {
            throw new NotImplementedException();
        }
        public static List<ClsIPRegistradas> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViIPRegistradas
                                select new ClsIPRegistradas()
                                {
                                    NumeroDeUsuario = q.IPR_NUM_USUARIO,
                                    Terminal = q.IPR_TERMINAL,
                                    IP = q.IPR_IP,
                                    CveTipoDeAcceso = q.IPR_CVE_TIPOACCESO,
                                    NombreDeUsuario = q.IPR_NOM_USUARIO,
                                    TextoTipoDeAccedo = q.IPR_TXT_TIPOACCESO
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.IPRegistradas
                                select new ClsIPRegistradas()
                                {
                                    NumeroDeUsuario = q.IPR_NUM_USUARIO,
                                    Terminal = q.IPR_TERMINAL,
                                    IP = q.IPR_IP,
                                    CveTipoDeAcceso = q.IPR_CVE_TIPOACCESO
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsIPRegistradas>();
        }
    }
}
