using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mod.Entity;

namespace Negocio.Kuup.Clases
{
    public class ClsUsuariosPerfil : Interfaces.InterfazGen<ClsUsuariosPerfil>
    {
        ViUsuarioPerfil UsuarioPerfil = new ViUsuarioPerfil();
        public byte NumeroDePerfil
        {
            get { return UsuarioPerfil.USP_NUM_PERFIL; }
            set { UsuarioPerfil.USP_NUM_PERFIL = value; }
        }
        public short NumeroDeUsuario
        {
            get { return UsuarioPerfil.USP_NUM_USUARIO; }
            set { UsuarioPerfil.USP_NUM_USUARIO = value; }
        }
        public byte CveDeEstatus
        {
            get { return UsuarioPerfil.USP_CVE_ESTATUS; }
            set { UsuarioPerfil.USP_CVE_ESTATUS = value; }
        }
        public String NombreDePerfil
        {
            get { return UsuarioPerfil.USP_NOM_PERFIL; }
            set { UsuarioPerfil.USP_NOM_PERFIL = value; }
        }
        public String NombreDeUsuario
        {
            get { return UsuarioPerfil.USP_NOM_USUARIO; }
            set { UsuarioPerfil.USP_NOM_USUARIO = value; }
        }
        public String CorreoDeUsuario
        {
            get { return UsuarioPerfil.USP_CORREO; }
            set { UsuarioPerfil.USP_CORREO = value; }
        }
        public String TextoDeEstatus
        {
            get { return UsuarioPerfil.USP_TXT_ESTATUS; }
            set { UsuarioPerfil.USP_TXT_ESTATUS = value; }
        }
        public ClsUsuariosPerfil() { }
        public ClsUsuariosPerfil(UsuarioPerfil Registro)
        {
            NumeroDePerfil = Registro.USP_NUM_PERFIL;
            NumeroDeUsuario = Registro.USP_NUM_USUARIO;
            CveDeEstatus = Registro.USP_CVE_ESTATUS;
        }
        public ClsUsuariosPerfil(ViUsuarioPerfil Registro)
        {
            NumeroDePerfil = Registro.USP_NUM_PERFIL;
            NumeroDeUsuario = Registro.USP_NUM_USUARIO;
            CveDeEstatus = Registro.USP_CVE_ESTATUS;
            NombreDePerfil = Registro.USP_NOM_PERFIL;
            NombreDeUsuario = Registro.USP_NOM_USUARIO;
            CorreoDeUsuario = Registro.USP_CORREO;
            TextoDeEstatus = Registro.USP_TXT_ESTATUS;
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
        public static List<ClsUsuariosPerfil> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViUsuarioPerfil
                                select new ClsUsuariosPerfil()
                                {
                                    NumeroDePerfil = q.USP_NUM_PERFIL,
                                    NumeroDeUsuario = q.USP_NUM_USUARIO,
                                    CveDeEstatus = q.USP_CVE_ESTATUS,
                                    NombreDePerfil = q.USP_NOM_PERFIL,
                                    NombreDeUsuario = q.USP_NOM_USUARIO,
                                    CorreoDeUsuario = q.USP_CORREO,
                                    TextoDeEstatus = q.USP_TXT_ESTATUS
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.UsuarioPerfil
                                select new ClsUsuariosPerfil()
                                {
                                    NumeroDePerfil = q.USP_NUM_PERFIL,
                                    NumeroDeUsuario = q.USP_NUM_USUARIO,
                                    CveDeEstatus = q.USP_CVE_ESTATUS
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsUsuariosPerfil>();
        }
    }
}
