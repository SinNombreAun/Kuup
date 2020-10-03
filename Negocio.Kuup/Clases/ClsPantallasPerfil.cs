using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mod.Entity;

namespace Negocio.Kuup.Clases
{
    public class ClsPantallasPerfil : Interfaces.InterfazGen<ClsPantallasPerfil>
    {
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
