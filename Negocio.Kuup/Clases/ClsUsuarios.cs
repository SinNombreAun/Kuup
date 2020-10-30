using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mod.Entity;

namespace Negocio.Kuup.Clases
{
    public class ClsUsuarios : Interfaces.InterfazGen<ClsUsuarios>
    {
        ViUsuario Usuario = new ViUsuario();
        public short NumeroDeUsuario
        {
            get { return Usuario.USU_NUM_USUARIO; }
            set { Usuario.USU_NUM_USUARIO = value; }
        }
        public String NombreDeUsuario
        {
            get { return Usuario.USU_NOM_USUARIO; }
            set { Usuario.USU_NOM_USUARIO = value; }
        }
        public String NombreDePersona
        {
            get { return Usuario.USU_NOM_PERS; }
            set { Usuario.USU_NOM_PERS = value; }
        }
        public String ApellidoPaterno
        {
            get { return Usuario.USU_APP_PERS; }
            set { Usuario.USU_APP_PERS = value; }
        }
        public String ApellidoMaterno
        {
            get { return Usuario.USU_APM_PERS; }
            set { Usuario.USU_APM_PERS = value; }
        }
        public String CorreoDeUsuario
        {
            get { return Usuario.USU_CORREO; }
            set { Usuario.USU_CORREO = value; }
        }
        public String PasswordUsuario
        {
            get { return Usuario.USU_PASSWORD; }
            set { Usuario.USU_PASSWORD = value; }
        }
        public DateTime FechaDeRegistro
        {
            get { return Usuario.USU_FECHA_REGISTRO; }
            set { Usuario.USU_FECHA_REGISTRO = value; }
        }
        public DateTime? FechaDeCancelacion
        {
            get { return Usuario.USU_FECHA_CANCELACION; }
            set { Usuario.USU_FECHA_CANCELACION = value; }
        }
        public byte CveDeEstatus
        {
            get { return Usuario.USU_CVE_ESTATUS; }
            set { Usuario.USU_CVE_ESTATUS = value; }
        }
        public String TextoDeEstatus
        {
            get { return Usuario.USU_TXT_ESTATUS; }
            set { Usuario.USU_TXT_ESTATUS = value; }
        }
        public ClsUsuarios() { }
        public ClsUsuarios(Usuario Registro)
        {
            NumeroDeUsuario = Registro.USU_NUM_USUARIO;
            NombreDeUsuario = Registro.USU_NOM_USUARIO;
            NombreDePersona = Registro.USU_NOM_PERS;
            ApellidoPaterno = Registro.USU_APP_PERS;
            ApellidoMaterno = Registro.USU_APM_PERS;
            CorreoDeUsuario = Registro.USU_CORREO;
            PasswordUsuario = Registro.USU_PASSWORD;
            FechaDeRegistro = Registro.USU_FECHA_REGISTRO;
            FechaDeCancelacion = Registro.USU_FECHA_CANCELACION;
            CveDeEstatus = Registro.USU_CVE_ESTATUS;
        }
        public ClsUsuarios(ViUsuario Registro)
        {
            NumeroDeUsuario = Registro.USU_NUM_USUARIO;
            NombreDeUsuario = Registro.USU_NOM_USUARIO;
            NombreDePersona = Registro.USU_NOM_PERS;
            ApellidoPaterno = Registro.USU_APP_PERS;
            ApellidoMaterno = Registro.USU_APM_PERS;
            CorreoDeUsuario = Registro.USU_CORREO;
            PasswordUsuario = Registro.USU_PASSWORD;
            FechaDeRegistro = Registro.USU_FECHA_REGISTRO;
            FechaDeCancelacion = Registro.USU_FECHA_CANCELACION;
            CveDeEstatus = Registro.USU_CVE_ESTATUS;
            TextoDeEstatus = Registro.USU_TXT_ESTATUS;
        }
        public bool Insert(bool Dependencia = false)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    Usuario Usuario = this.ToTable();
                    db.Usuario.Add(Usuario);
                    if (!Dependencia)
                    {
                        db.SaveChanges();
                    }
                    if ((from q in db.Usuario where q.USU_NUM_USUARIO == Usuario.USU_NUM_USUARIO select q).Count() != 0)
                    {
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
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    db.Usuario.Remove((from q in db.Usuario where q.USU_NUM_USUARIO == Usuario.USU_NUM_USUARIO select q).FirstOrDefault());
                    if (!Dependencia)
                    {
                        db.SaveChanges();
                    }
                    if ((from q in db.Usuario where q.USU_NUM_USUARIO == Usuario.USU_NUM_USUARIO select q).Count() != 0)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool Update(bool Dependencia = false)
        {
            throw new NotImplementedException();
        }
        public Usuario ToTable()
        {
            Usuario Tabla = new Usuario();
            Tabla.USU_NUM_USUARIO = Usuario.USU_NUM_USUARIO;
            Tabla.USU_NOM_USUARIO = Usuario.USU_NOM_USUARIO;
            Tabla.USU_NOM_PERS = Usuario.USU_NOM_PERS;
            Tabla.USU_APP_PERS = Usuario.USU_APP_PERS;
            Tabla.USU_APM_PERS = Usuario.USU_APM_PERS;
            Tabla.USU_CORREO = Usuario.USU_CORREO;
            Tabla.USU_PASSWORD = Usuario.USU_PASSWORD;
            Tabla.USU_FECHA_REGISTRO = Usuario.USU_FECHA_REGISTRO;
            Tabla.USU_FECHA_CANCELACION = Usuario.USU_FECHA_CANCELACION;
            Tabla.USU_CVE_ESTATUS = Usuario.USU_CVE_ESTATUS;
            return Tabla;
        }
        public static List<ClsUsuarios> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViUsuario
                                select new ClsUsuarios()
                                {
                                    NumeroDeUsuario = q.USU_NUM_USUARIO,
                                    NombreDeUsuario = q.USU_NOM_USUARIO,
                                    NombreDePersona = q.USU_NOM_PERS,
                                    ApellidoPaterno = q.USU_APP_PERS,
                                    ApellidoMaterno = q.USU_APM_PERS,
                                    CorreoDeUsuario = q.USU_CORREO,
                                    PasswordUsuario = q.USU_PASSWORD,
                                    FechaDeRegistro = q.USU_FECHA_REGISTRO,
                                    FechaDeCancelacion = q.USU_FECHA_CANCELACION,
                                    CveDeEstatus = q.USU_CVE_ESTATUS,
                                    TextoDeEstatus = q.USU_TXT_ESTATUS
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.Usuario
                                select new ClsUsuarios()
                                {
                                    NumeroDeUsuario = q.USU_NUM_USUARIO,
                                    NombreDeUsuario = q.USU_NOM_USUARIO,
                                    NombreDePersona = q.USU_NOM_PERS,
                                    ApellidoPaterno = q.USU_APP_PERS,
                                    ApellidoMaterno = q.USU_APM_PERS,
                                    CorreoDeUsuario = q.USU_CORREO,
                                    PasswordUsuario = q.USU_PASSWORD,
                                    FechaDeRegistro = q.USU_FECHA_REGISTRO,
                                    FechaDeCancelacion = q.USU_FECHA_CANCELACION,
                                    CveDeEstatus = q.USU_CVE_ESTATUS
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsUsuarios>();
        }
    }
}
