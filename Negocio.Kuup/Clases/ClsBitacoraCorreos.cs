using Mod.Entity;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations.Builders;
using System.IO.Pipes;
using System.Linq;
using System.Security.Permissions;
namespace Negocio.Kuup.Clases
{
    public class ClsBitacoraCorreos : Interfaces.InterfazGen<ClsBitacoraCorreos>
    {
        public DBKuupEntities db { get; set; }
        ViBitacoraCorreo BitacoraCorreo = new ViBitacoraCorreo();
        public short NumeroDePantalla
        {
            get { return BitacoraCorreo.BIM_NUM_PANTALLA; }
            set { BitacoraCorreo.BIM_NUM_PANTALLA = value; }
        }
        public String Correo
        {
            get { return BitacoraCorreo.BIM_CORREO; }
            set { BitacoraCorreo.BIM_CORREO = value; }
        }
        public String Asunto
        {
            get { return BitacoraCorreo.BIM_ASUNTO; }
            set { BitacoraCorreo.BIM_ASUNTO = value; }
        }
        public String Mensaje
        {
            get { return BitacoraCorreo.BIM_MENSAJE; }
            set { BitacoraCorreo.BIM_MENSAJE = value; }
        }
        public System.DateTime FechaDeEnvio
        {
            get { return BitacoraCorreo.BIM_FECHA_ENVIO; }
            set { BitacoraCorreo.BIM_FECHA_ENVIO = value; }
        }
        public System.DateTime FechaDeReenvio
        {
            get { return BitacoraCorreo.BIM_FECHA_REENVIO; }
            set { BitacoraCorreo.BIM_FECHA_REENVIO = value; }
        }
        public String MensajeDeError
        {
            get { return BitacoraCorreo.BIM_MESAJE_ERROR; }
            set { BitacoraCorreo.BIM_MESAJE_ERROR = value; }
        }
        public Byte CveDeEstatus
        {
            get { return BitacoraCorreo.BIM_CVE_ESTATUS; }
            set { BitacoraCorreo.BIM_CVE_ESTATUS = value; }
        }
        public String NombreDePantalla
        {
            get { return BitacoraCorreo.BIM_NOM_PANTALLA; }
            set { BitacoraCorreo.BIM_NOM_PANTALLA = value; }
        }
        public String TextoDeEstatus
        {
            get { return BitacoraCorreo.BIM_TXT_ESTATUS; }
            set { BitacoraCorreo.BIM_TXT_ESTATUS = value; }
        }
        public ClsBitacoraCorreos() { }
        private bool ToInsert(DBKuupEntities db)
        {
            BitacoraCorreo BitacoraCorreo = this.ToTable();
            db.BitacoraCorreo.Add(BitacoraCorreo);
            db.Entry(BitacoraCorreo).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.BitacoraCorreo where q.BIM_NUM_PANTALLA == BitacoraCorreo.BIM_NUM_PANTALLA select q).Count() != 0)
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
            db.BitacoraCorreo.Remove((from q in db.BitacoraCorreo where q.BIM_NUM_PANTALLA == BitacoraCorreo.BIM_NUM_PANTALLA select q).FirstOrDefault());
            db.SaveChanges();
            if ((from q in db.BitacoraCorreo where q.BIM_NUM_PANTALLA == BitacoraCorreo.BIM_NUM_PANTALLA select q).Count() != 0)
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
            BitacoraCorreo BitacoraCorreo = this.ToTable();
            db.BitacoraCorreo.Attach(BitacoraCorreo);
            db.Entry(BitacoraCorreo).State = EntityState.Modified;
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
        public BitacoraCorreo ToTable()
        {
            BitacoraCorreo Tabla = new BitacoraCorreo();
            Tabla.BIM_NUM_PANTALLA = this.NumeroDePantalla;
            Tabla.BIM_CORREO = this.Correo;
            Tabla.BIM_ASUNTO = this.Asunto;
            Tabla.BIM_MENSAJE = this.Mensaje;
            Tabla.BIM_FECHA_ENVIO = this.FechaDeEnvio;
            Tabla.BIM_FECHA_REENVIO = this.FechaDeReenvio;
            Tabla.BIM_MESAJE_ERROR = this.MensajeDeError;
            Tabla.BIM_CVE_ESTATUS = this.CveDeEstatus;
            return Tabla;
        }

        public static List<ClsBitacoraCorreos> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViBitacoraCorreo
                                select new ClsBitacoraCorreos()
                                {
                                    NumeroDePantalla = q.BIM_NUM_PANTALLA,
                                    Correo = q.BIM_CORREO,
                                    Asunto = q.BIM_ASUNTO,
                                    Mensaje = q.BIM_MENSAJE,
                                    FechaDeEnvio = q.BIM_FECHA_ENVIO,
                                    FechaDeReenvio = q.BIM_FECHA_REENVIO,
                                    MensajeDeError = q.BIM_MESAJE_ERROR,
                                    CveDeEstatus = q.BIM_CVE_ESTATUS,
                                    NombreDePantalla = q.BIM_NOM_PANTALLA,
                                    TextoDeEstatus = q.BIM_TXT_ESTATUS
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.BitacoraCorreo
                                select new ClsBitacoraCorreos()
                                {
                                    NumeroDePantalla = q.BIM_NUM_PANTALLA,
                                    Correo = q.BIM_CORREO,
                                    Asunto = q.BIM_ASUNTO,
                                    Mensaje = q.BIM_MENSAJE,
                                    FechaDeEnvio = q.BIM_FECHA_ENVIO,
                                    FechaDeReenvio = q.BIM_FECHA_REENVIO,
                                    MensajeDeError = q.BIM_MESAJE_ERROR,
                                    CveDeEstatus = q.BIM_CVE_ESTATUS
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsBitacoraCorreos>();
        }
    }
}
