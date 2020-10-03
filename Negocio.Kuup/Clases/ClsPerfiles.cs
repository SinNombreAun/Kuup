using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mod.Entity;

namespace Negocio.Kuup.Clases
{
    public class ClsPerfiles : Interfaces.InterfazGen<ClsPerfiles>
    {
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
        public bool Insert()
        {
            throw new NotImplementedException();
        }
        public bool Delete()
        {
            throw new NotImplementedException();
        }
        public bool Update()
        {
            throw new NotImplementedException();
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
