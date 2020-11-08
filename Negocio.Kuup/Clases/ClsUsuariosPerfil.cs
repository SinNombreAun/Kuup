using Mod.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Negocio.Kuup.Clases
{
    public class ClsUsuariosPerfil : Interfaces.InterfazGen<ClsUsuariosPerfil>
    {
        public DBKuupEntities db { get; set; }
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
        private bool ToInsert(DBKuupEntities db)
        {
            UsuarioPerfil UsuarioPerfil = this.ToTable();
            db.UsuarioPerfil.Add(UsuarioPerfil);
            db.Entry(UsuarioPerfil).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.UsuarioPerfil where q.USP_NUM_USUARIO == UsuarioPerfil.USP_NUM_USUARIO && q.USP_NUM_PERFIL == UsuarioPerfil.USP_NUM_PERFIL select q).Count() != 0)
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
            db.UsuarioPerfil.Remove((from q in db.UsuarioPerfil where q.USP_NUM_USUARIO == UsuarioPerfil.USP_NUM_USUARIO && q.USP_NUM_PERFIL == UsuarioPerfil.USP_NUM_PERFIL select q).FirstOrDefault());
            db.SaveChanges();
            if((from q in db.UsuarioPerfil where q.USP_NUM_USUARIO == UsuarioPerfil.USP_NUM_USUARIO && q.USP_NUM_PERFIL == UsuarioPerfil.USP_NUM_PERFIL select q).Count() != 0)
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
            UsuarioPerfil UsuarioPerfil = this.ToTable();
            db.UsuarioPerfil.Attach(UsuarioPerfil);
            db.Entry(UsuarioPerfil).State = EntityState.Modified;
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
        public UsuarioPerfil ToTable()
        {
            UsuarioPerfil Tabla = new UsuarioPerfil();
            Tabla.USP_NUM_PERFIL = this.NumeroDePerfil;
            Tabla.USP_NUM_USUARIO = this.NumeroDeUsuario;
            Tabla.USP_CVE_ESTATUS = this.CveDeEstatus;
            return Tabla;
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
