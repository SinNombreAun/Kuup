using Mod.Entity;
using Negocio.Kuup.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Negocio.Kuup.Clases
{
    public class ClsConfiguraPaquetes : InterfazGen<ClsConfiguraPaquetes>
    {
        ViConfiguraPaquete ConfiguraPaquete = new ViConfiguraPaquete();
        public short NumeroDeProductoPadre
        {
            get { return ConfiguraPaquete.CNP_NUM_PRODUCTO_PADRE; }
            set { ConfiguraPaquete.CNP_NUM_PRODUCTO_PADRE = value; }
        }
        public short NumeroDeProductoHijo
        {
            get { return ConfiguraPaquete.CNP_NUM_PRODUCTO_HIJO; }
            set { ConfiguraPaquete.CNP_NUM_PRODUCTO_HIJO = value; }
        }
        public decimal PrecioDeProductoPadre
        {
            get { return ConfiguraPaquete.CNP_PRECIO_PRODUCTO_PADRE; }
            set { ConfiguraPaquete.CNP_PRECIO_PRODUCTO_PADRE = value; }
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
        public bool Insert(bool Dependencia = false)
        {
            try
            {
                using(DBKuupEntities db = new DBKuupEntities())
                {
                    ConfiguraPaquete ConfiguraPaquete = this.ToTable();
                    db.ConfiguraPaquete.Add(ConfiguraPaquete);
                    if (!Dependencia)
                    {
                        db.SaveChanges();
                    }
                    if ((from q in db.ConfiguraPaquete where q.CNP_NUM_PRODUCTO_PADRE == ConfiguraPaquete.CNP_NUM_PRODUCTO_PADRE && q.CNP_NUM_PRODUCTO_HIJO == ConfiguraPaquete.CNP_NUM_PRODUCTO_HIJO select q).Count() != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch(Exception e)
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
                    db.ConfiguraPaquete.Remove((from q in db.ConfiguraPaquete where q.CNP_NUM_PRODUCTO_PADRE == ConfiguraPaquete.CNP_NUM_PRODUCTO_PADRE && q.CNP_NUM_PRODUCTO_HIJO == ConfiguraPaquete.CNP_NUM_PRODUCTO_HIJO select q).FirstOrDefault());
                    if (!Dependencia)
                    {
                        db.SaveChanges();
                    }
                    if ((from q in db.ConfiguraPaquete where q.CNP_NUM_PRODUCTO_PADRE == ConfiguraPaquete.CNP_NUM_PRODUCTO_PADRE && q.CNP_NUM_PRODUCTO_HIJO == ConfiguraPaquete.CNP_NUM_PRODUCTO_HIJO select q).Count() != 0)
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
        public ConfiguraPaquete ToTable()
        {
            ConfiguraPaquete Tabla = new ConfiguraPaquete();
            Tabla.CNP_NUM_PRODUCTO_PADRE = this.NumeroDeProductoPadre;
            Tabla.CNP_NUM_PRODUCTO_HIJO = this.NumeroDeProductoHijo;
            Tabla.CNP_PRECIO_PRODUCTO_PADRE = this.PrecioDeProductoPadre;
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
                                    NumeroDeProductoHijo = q.CNP_NUM_PRODUCTO_HIJO,
                                    PrecioDeProductoPadre = q.CNP_PRECIO_PRODUCTO_PADRE,
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
                                    NumeroDeProductoHijo = q.CNP_NUM_PRODUCTO_HIJO,
                                    PrecioDeProductoPadre = q.CNP_PRECIO_PRODUCTO_PADRE,
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
