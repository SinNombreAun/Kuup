using Mod.Entity;
using Negocio.Kuup.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Negocio.Kuup.Clases
{
    public class ClsConfiguraPaquetes : Interfaces.InterfazGen<ClsConfiguraPaquetes>
    {
        public DBKuupEntities db { get; set; }
        public short NumeroDePantallaKuup
        {
            get { return 7; }
        }
        ViConfiguraPaquete ConfiguraPaquete = new ViConfiguraPaquete();
        public short NumeroDeProductoPadre
        {
            get { return ConfiguraPaquete.CNP_NUM_PRODUCTO_PADRE; }
            set { ConfiguraPaquete.CNP_NUM_PRODUCTO_PADRE = value; }
        }
        public String CodigoDeBarrasPadre
        {
            get { return ConfiguraPaquete.CNP_CODIGO_BARRAS_PADRE; }
            set { ConfiguraPaquete.CNP_CODIGO_BARRAS_PADRE = value; }
        }
        public short NumeroDeProductoHijo
        {
            get { return ConfiguraPaquete.CNP_NUM_PRODUCTO_HIJO; }
            set { ConfiguraPaquete.CNP_NUM_PRODUCTO_HIJO = value; }
        }
        public String CodigoDeBarrasHijo
        {
            get { return ConfiguraPaquete.CNP_CODIGO_BARRAS_HIJO; }
            set { ConfiguraPaquete.CNP_CODIGO_BARRAS_HIJO = value; }
        }
        public decimal PrecioDeProductoPadre
        {
            get { return ConfiguraPaquete.CNP_PRECIO_PRODUCTO_PADRE; }
            set { ConfiguraPaquete.CNP_PRECIO_PRODUCTO_PADRE = value; }
        }
        public byte CantidadASalir
        {
            get { return ConfiguraPaquete.CNP_CANTIDAD_A_SALIR; }
            set { ConfiguraPaquete.CNP_CANTIDAD_A_SALIR = value; }
        }
        public Nullable<decimal> PrecioDeProductoHijo
        {
            get { return ConfiguraPaquete.CNP_PRECIO_PRODUCTO_HIJO; }
            set { ConfiguraPaquete.CNP_PRECIO_PRODUCTO_HIJO = value; }
        }
        public decimal ImporteTotal
        {
            get { return ConfiguraPaquete.CNP_IMPORTE_TOTAL; }
            set { ConfiguraPaquete.CNP_IMPORTE_TOTAL = value; }
        }
        public String NombreDeProductoPadre
        {
            get { return ConfiguraPaquete.CNP_NOM_PRODUCTO_PADRE; }
            set { ConfiguraPaquete.CNP_NOM_PRODUCTO_PADRE = value; }
        }
        public String NombreDeProductoHijo
        {
            get { return ConfiguraPaquete.CNP_NOM_PRODUCTO_HIJO; }
            set { ConfiguraPaquete.CNP_NOM_PRODUCTO_HIJO = value; }
        }
        public ClsConfiguraPaquetes() { }
        private bool ToInsert(DBKuupEntities db)
        {
            ConfiguraPaquete ConfiguraPaquete = this.ToTable();
            db.ConfiguraPaquete.Add(ConfiguraPaquete);
            db.Entry(ConfiguraPaquete).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.ConfiguraPaquete where q.CNP_NUM_PRODUCTO_PADRE == ConfiguraPaquete.CNP_NUM_PRODUCTO_PADRE && q.CNP_CODIGO_BARRAS_PADRE == ConfiguraPaquete.CNP_CODIGO_BARRAS_PADRE && q.CNP_NUM_PRODUCTO_HIJO == ConfiguraPaquete.CNP_NUM_PRODUCTO_HIJO && q.CNP_CODIGO_BARRAS_HIJO == ConfiguraPaquete.CNP_CODIGO_BARRAS_HIJO select q).Count() != 0)
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
                ClsBitacora.GeneraBitacora(NumeroDePantallaKuup, 1, "Insert", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;
            }
        }
        public bool InsertAudit(ClsAudit ObjAudit)
        {
            ConfiguraPaqueteAudit Audit = new ConfiguraPaqueteAudit();
            Audit.CNP_ID_AUDIT = ObjAudit.IdAudit;
            Audit.CNP_TERMINAL = ObjAudit.Terminal;
            Audit.CNP_IP = ObjAudit.IP;
            Audit.CNP_VERSION = ObjAudit.Version;
            Audit.CNP_NOM_USUARIO = ObjAudit.NombreDeUsuario;
            Audit.CNP_FECHA_BASE = DateTime.Now;
            Audit.CNP_NOM_FUNCIONALIDAD = ObjAudit.NombreDeFuncionalidad;
            Audit.CNP_NUM_PRODUCTO_PADRE = ConfiguraPaquete.CNP_NUM_PRODUCTO_PADRE;
            Audit.CNP_CODIGO_BARRAS_PADRE = ConfiguraPaquete.CNP_CODIGO_BARRAS_PADRE;
            Audit.CNP_NUM_PRODUCTO_HIJO = ConfiguraPaquete.CNP_NUM_PRODUCTO_HIJO;
            Audit.CNP_CODIGO_BARRAS_HIJO = ConfiguraPaquete.CNP_CODIGO_BARRAS_HIJO;
            Audit.CNP_PRECIO_PRODUCTO_PADRE = ConfiguraPaquete.CNP_PRECIO_PRODUCTO_PADRE;
            Audit.CNP_CANTIDAD_A_SALIR = ConfiguraPaquete.CNP_CANTIDAD_A_SALIR;
            Audit.CNP_PRECIO_PRODUCTO_HIJO = ConfiguraPaquete.CNP_PRECIO_PRODUCTO_HIJO;
            Audit.CNP_IMPORTE_TOTAL = ConfiguraPaquete.CNP_IMPORTE_TOTAL;
            Audit.CNP_NOM_PRODUCTO_PADRE = ConfiguraPaquete.CNP_NOM_PRODUCTO_PADRE;
            Audit.CNP_NOM_PRODUCTO_HIJO = ConfiguraPaquete.CNP_NOM_PRODUCTO_HIJO;
            try
            {
                if (db == null)
                {
                    using (db = new DBKuupEntities())
                    {
                        db.ConfiguraPaqueteAudit.Add(Audit);
                        db.Entry(Audit).State = EntityState.Added;
                        db.SaveChanges();
                        if ((from q in db.ConfiguraPaqueteAudit where q.CNP_ID_AUDIT == Audit.CNP_ID_AUDIT && q.CNP_NUM_PRODUCTO_PADRE == Audit.CNP_NUM_PRODUCTO_PADRE && q.CNP_CODIGO_BARRAS_PADRE == Audit.CNP_CODIGO_BARRAS_PADRE && q.CNP_NUM_PRODUCTO_HIJO == Audit.CNP_NUM_PRODUCTO_HIJO && q.CNP_CODIGO_BARRAS_HIJO == Audit.CNP_CODIGO_BARRAS_HIJO select q).Count() != 0)
                        {
                            return true;
                        }
                        return false;
                    }
                }
                else
                {
                    db.ConfiguraPaqueteAudit.Add(Audit);
                    db.Entry(Audit).State = EntityState.Added;
                    db.SaveChanges();
                    if ((from q in db.ConfiguraPaqueteAudit where q.CNP_ID_AUDIT == Audit.CNP_ID_AUDIT && q.CNP_NUM_PRODUCTO_PADRE == Audit.CNP_NUM_PRODUCTO_PADRE && q.CNP_CODIGO_BARRAS_PADRE == Audit.CNP_CODIGO_BARRAS_PADRE && q.CNP_NUM_PRODUCTO_HIJO == Audit.CNP_NUM_PRODUCTO_HIJO && q.CNP_CODIGO_BARRAS_HIJO == Audit.CNP_CODIGO_BARRAS_HIJO select q).Count() != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                ClsBitacora.GeneraBitacora(NumeroDePantallaKuup, 1, "InsertAudit", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;
            }
        }
        private bool ToDelete(DBKuupEntities db)
        {
            db.ConfiguraPaquete.Remove((from q in db.ConfiguraPaquete where q.CNP_NUM_PRODUCTO_PADRE == ConfiguraPaquete.CNP_NUM_PRODUCTO_PADRE && q.CNP_CODIGO_BARRAS_PADRE == ConfiguraPaquete.CNP_CODIGO_BARRAS_PADRE && q.CNP_NUM_PRODUCTO_HIJO == ConfiguraPaquete.CNP_NUM_PRODUCTO_HIJO && q.CNP_CODIGO_BARRAS_HIJO == ConfiguraPaquete.CNP_CODIGO_BARRAS_HIJO select q).FirstOrDefault());
            db.SaveChanges();
            if ((from q in db.ConfiguraPaquete where q.CNP_NUM_PRODUCTO_PADRE == ConfiguraPaquete.CNP_NUM_PRODUCTO_PADRE && q.CNP_CODIGO_BARRAS_PADRE == ConfiguraPaquete.CNP_CODIGO_BARRAS_PADRE && q.CNP_NUM_PRODUCTO_HIJO == ConfiguraPaquete.CNP_NUM_PRODUCTO_HIJO && q.CNP_CODIGO_BARRAS_HIJO == ConfiguraPaquete.CNP_CODIGO_BARRAS_HIJO select q).Count() != 0)
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
                ClsBitacora.GeneraBitacora(NumeroDePantallaKuup, 1, "Delete", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;
            }
        }
        private bool ToUpdate(DBKuupEntities db)
        {
            ConfiguraPaquete ConfiguraPaquete = this.ToTable();
            db.ConfiguraPaquete.Attach(ConfiguraPaquete);
            db.Entry(ConfiguraPaquete).State = EntityState.Modified;
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
                ClsBitacora.GeneraBitacora(NumeroDePantallaKuup, 1, "Update", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;

            }
        }
        public ConfiguraPaquete ToTable()
        {
            ConfiguraPaquete Tabla = new ConfiguraPaquete();
            Tabla.CNP_NUM_PRODUCTO_PADRE = this.NumeroDeProductoPadre;
            Tabla.CNP_CODIGO_BARRAS_PADRE = this.CodigoDeBarrasPadre;
            Tabla.CNP_NUM_PRODUCTO_HIJO = this.NumeroDeProductoHijo;
            Tabla.CNP_CODIGO_BARRAS_HIJO = this.CodigoDeBarrasHijo;
            Tabla.CNP_PRECIO_PRODUCTO_PADRE = this.PrecioDeProductoPadre;
            Tabla.CNP_CANTIDAD_A_SALIR = this.CantidadASalir;
            Tabla.CNP_PRECIO_PRODUCTO_HIJO = this.PrecioDeProductoHijo;
            Tabla.CNP_IMPORTE_TOTAL = this.ImporteTotal;
            return Tabla;
        }
        public static List<ClsConfiguraPaquetes> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViConfiguraPaquete
                                select new ClsConfiguraPaquetes()
                                {
                                    NumeroDeProductoPadre = q.CNP_NUM_PRODUCTO_PADRE,
                                    CodigoDeBarrasPadre = q.CNP_CODIGO_BARRAS_PADRE,
                                    NumeroDeProductoHijo = q.CNP_NUM_PRODUCTO_HIJO,
                                    CodigoDeBarrasHijo = q.CNP_CODIGO_BARRAS_HIJO,
                                    PrecioDeProductoPadre = q.CNP_PRECIO_PRODUCTO_PADRE,
                                    CantidadASalir = q.CNP_CANTIDAD_A_SALIR,
                                    PrecioDeProductoHijo = q.CNP_PRECIO_PRODUCTO_HIJO,
                                    ImporteTotal = q.CNP_IMPORTE_TOTAL,
                                    NombreDeProductoPadre = q.CNP_NOM_PRODUCTO_PADRE,
                                    NombreDeProductoHijo = q.CNP_NOM_PRODUCTO_HIJO
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.ConfiguraPaquete
                                select new ClsConfiguraPaquetes()
                                {
                                    NumeroDeProductoPadre = q.CNP_NUM_PRODUCTO_PADRE,
                                    CodigoDeBarrasPadre = q.CNP_CODIGO_BARRAS_PADRE,
                                    NumeroDeProductoHijo = q.CNP_NUM_PRODUCTO_HIJO,
                                    CodigoDeBarrasHijo = q.CNP_CODIGO_BARRAS_HIJO,
                                    PrecioDeProductoPadre = q.CNP_PRECIO_PRODUCTO_PADRE,
                                    CantidadASalir = q.CNP_CANTIDAD_A_SALIR,
                                    PrecioDeProductoHijo = q.CNP_PRECIO_PRODUCTO_HIJO,
                                    ImporteTotal = q.CNP_IMPORTE_TOTAL
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsConfiguraPaquetes>();
        }
    }
}
