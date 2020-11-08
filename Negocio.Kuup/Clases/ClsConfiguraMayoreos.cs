using Mod.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Negocio.Kuup.Clases
{
    public class ClsConfiguraMayoreos : Interfaces.InterfazGen<ClsBitacora>
    {
        public DBKuupEntities db { get; set; }
        ViConfiguraMayoreo ConfiguraMayoreo = new ViConfiguraMayoreo();
        public short NumeroDeMayoreo
        {
            get { return ConfiguraMayoreo.COM_NUM_MAYOREO; }
            set { ConfiguraMayoreo.COM_NUM_MAYOREO = value; }
        }
        public short NumeroDeProducto
        {
            get { return ConfiguraMayoreo.COM_NUM_PRODUCTO; }
            set { ConfiguraMayoreo.COM_NUM_PRODUCTO = value; }
        }
        public String CodigoDeBarras
        {
            get { return ConfiguraMayoreo.COM_CODIGO_BARRAS; }
            set { ConfiguraMayoreo.COM_CODIGO_BARRAS = value; }
        }
        public byte CveDeAplicaPaquetes
        {
            get { return ConfiguraMayoreo.COM_CVE_APLICA_PAQUETES; }
            set { ConfiguraMayoreo.COM_CVE_APLICA_PAQUETES = value; }
        }
        public byte CantidadMinima
        {
            get { return ConfiguraMayoreo.COM_CANTIDAD_MINIMA; }
            set { ConfiguraMayoreo.COM_CANTIDAD_MINIMA = value; }
        }
        public Nullable<byte> CantidadMaxima
        {
            get { return ConfiguraMayoreo.COM_CANTIDAD_MAXIMA; }
            set { ConfiguraMayoreo.COM_CANTIDAD_MAXIMA = value; }
        }
        public decimal PrecioDeMayoreo
        {
            get { return ConfiguraMayoreo.COM_PRECIO_MAYOREO; }
            set { ConfiguraMayoreo.COM_PRECIO_MAYOREO = value; }
        }
        public String NombreDeProducto
        {
            get { return ConfiguraMayoreo.COM_NOM_PRODUCTO; }
            set { ConfiguraMayoreo.COM_NOM_PRODUCTO = value; }
        }
        public ClsConfiguraMayoreos() { }
        public ClsConfiguraMayoreos(ViConfiguraMayoreo Elemento)
        {
            NumeroDeMayoreo = Elemento.COM_NUM_MAYOREO;
            NumeroDeProducto = Elemento.COM_NUM_PRODUCTO;
            CodigoDeBarras = Elemento.COM_CODIGO_BARRAS;
            CveDeAplicaPaquetes = Elemento.COM_CVE_APLICA_PAQUETES;
            CantidadMinima = Elemento.COM_CANTIDAD_MINIMA;
            CantidadMaxima = Elemento.COM_CANTIDAD_MAXIMA;
            PrecioDeMayoreo = Elemento.COM_PRECIO_MAYOREO;
            NombreDeProducto = Elemento.COM_NOM_PRODUCTO;
        }
        public ClsConfiguraMayoreos(ConfiguraMayoreo Elemento)
        {
            NumeroDeMayoreo = Elemento.COM_NUM_MAYOREO;
            NumeroDeProducto = Elemento.COM_NUM_PRODUCTO;
            CodigoDeBarras = Elemento.COM_CODIGO_BARRAS;
            CveDeAplicaPaquetes = Elemento.COM_CVE_APLICA_PAQUETES;
            CantidadMinima = Elemento.COM_CANTIDAD_MINIMA;
            CantidadMaxima = Elemento.COM_CANTIDAD_MAXIMA;
            PrecioDeMayoreo = Elemento.COM_PRECIO_MAYOREO;
        }
        private bool ToInsert(DBKuupEntities db)
        {
            ConfiguraMayoreo ConfiguraMayoreo = this.ToTable();
            db.ConfiguraMayoreo.Add(ConfiguraMayoreo);
            db.Entry(ConfiguraMayoreo).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.ConfiguraMayoreo where q.COM_NUM_MAYOREO == ConfiguraMayoreo.COM_NUM_MAYOREO && q.COM_NUM_PRODUCTO == ConfiguraMayoreo.COM_NUM_PRODUCTO && q.COM_CODIGO_BARRAS == ConfiguraMayoreo.COM_CODIGO_BARRAS select q).Count() != 0)
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
            db.ConfiguraMayoreo.Remove((from q in db.ConfiguraMayoreo where q.COM_NUM_MAYOREO == ConfiguraMayoreo.COM_NUM_MAYOREO && q.COM_NUM_PRODUCTO == ConfiguraMayoreo.COM_NUM_PRODUCTO && q.COM_CODIGO_BARRAS == ConfiguraMayoreo.COM_CODIGO_BARRAS select q).FirstOrDefault());
            db.SaveChanges();
            if ((from q in db.ConfiguraMayoreo where q.COM_NUM_MAYOREO == ConfiguraMayoreo.COM_NUM_MAYOREO && q.COM_NUM_PRODUCTO == ConfiguraMayoreo.COM_NUM_PRODUCTO && q.COM_CODIGO_BARRAS == ConfiguraMayoreo.COM_CODIGO_BARRAS select q).Count() != 0)
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
            ConfiguraMayoreo ConfiguraMayoreo = this.ToTable();
            db.ConfiguraMayoreo.Attach(ConfiguraMayoreo);
            db.Entry(ConfiguraMayoreo).State = EntityState.Modified;
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
        public ConfiguraMayoreo ToTable()
        {
            ConfiguraMayoreo Tabla = new ConfiguraMayoreo();
            Tabla.COM_NUM_MAYOREO = this.NumeroDeMayoreo;
            Tabla.COM_NUM_PRODUCTO = this.NumeroDeProducto;
            Tabla.COM_CODIGO_BARRAS = this.CodigoDeBarras;
            Tabla.COM_CVE_APLICA_PAQUETES = this.CveDeAplicaPaquetes;
            Tabla.COM_CANTIDAD_MINIMA = this.CantidadMinima;
            Tabla.COM_CANTIDAD_MAXIMA = this.CantidadMaxima;
            Tabla.COM_PRECIO_MAYOREO = this.PrecioDeMayoreo;
            return Tabla;
        }
        public static List<ClsConfiguraMayoreos> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViConfiguraMayoreo
                                select new ClsConfiguraMayoreos()
                                {
                                    NumeroDeMayoreo = q.COM_NUM_MAYOREO,
                                    NumeroDeProducto = q.COM_NUM_PRODUCTO,
                                    CodigoDeBarras = q.COM_CODIGO_BARRAS,
                                    CveDeAplicaPaquetes = q.COM_CVE_APLICA_PAQUETES,
                                    CantidadMinima = q.COM_CANTIDAD_MINIMA,
                                    CantidadMaxima = q.COM_CANTIDAD_MAXIMA,
                                    PrecioDeMayoreo = q.COM_PRECIO_MAYOREO,
                                    NombreDeProducto = q.COM_NOM_PRODUCTO
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.ConfiguraMayoreo
                                select new ClsConfiguraMayoreos()
                                {
                                    NumeroDeMayoreo = q.COM_NUM_MAYOREO,
                                    NumeroDeProducto = q.COM_NUM_PRODUCTO,
                                    CodigoDeBarras = q.COM_CODIGO_BARRAS,
                                    CveDeAplicaPaquetes = q.COM_CVE_APLICA_PAQUETES,
                                    CantidadMinima = q.COM_CANTIDAD_MINIMA,
                                    CantidadMaxima = q.COM_CANTIDAD_MAXIMA,
                                    PrecioDeMayoreo = q.COM_PRECIO_MAYOREO,
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsConfiguraMayoreos>();
        }
    }
}
