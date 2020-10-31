using Mod.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Negocio.Kuup.Clases
{
    public class ClsProductos : Interfaces.InterfazGen<ClsProductos>
    {
        public DBKuupEntities db { get; set; }
        ViProducto Producto = new ViProducto();
        public short NumeroDeProducto
        {
            get { return Producto.PRO_NUM_PRODUCTO; }
            set { Producto.PRO_NUM_PRODUCTO = value; }
        }
        public String CodigoDeBarras
        {
            get { return Producto.PRO_CODIGO_BARRAS; }
            set { Producto.PRO_CODIGO_BARRAS = value; }
        }
        public DateTime FechaDeRegistro
        {
            get { return Producto.PRO_FECHA_REGISTRO; }
            set { Producto.PRO_FECHA_REGISTRO = value; }
        }
        public short CantidadDeProductoUltima
        {
            get { return Producto.PRO_CANT_PRODUCTO_ULTIMA; }
            set { Producto.PRO_CANT_PRODUCTO_ULTIMA = value; }
        }
        public short CantidadDeProductoNueva
        {
            get { return Producto.PRO_CANT_PRODUCTO_NUEVA; }
            set { Producto.PRO_CANT_PRODUCTO_NUEVA = value; }
        }
        public short CantidadDeProductoTotal
        {
            get { return Producto.PRO_CANT_PRODUCTO_TOTAL; }
            set { Producto.PRO_CANT_PRODUCTO_TOTAL = value; }
        }
        public String NombreDeProducto
        {
            get { return Producto.PRO_NOM_PRODUCTO; }
            set { Producto.PRO_NOM_PRODUCTO = value; }
        }
        public String Descripcion
        {
            get { return Producto.PRO_DESCRIPCION; }
            set { Producto.PRO_DESCRIPCION = value; }
        }
        public byte CveAviso
        {
            get { return Producto.PRO_CVE_AVISO; }
            set { Producto.PRO_CVE_AVISO = value; }
        }
        public byte CveCorreoSurtido
        {
            get { return Producto.PRO_CVE_CORREO_SURTIDO; }
            set { Producto.PRO_CVE_CORREO_SURTIDO = value; }
        }
        public short CantidadMinima
        {
            get { return Producto.PRO_CAT_MINIMA; }
            set { Producto.PRO_CAT_MINIMA = value; }
        }
        public Nullable<byte> NumeroDeProveedor
        {
            get { return Producto.PRO_NUM_PROVEEDOR; }
            set { Producto.PRO_NUM_PROVEEDOR = value; }
        }
        public decimal PrecioUnitario
        {
            get { return Producto.PRO_PRECIO_UNITARIO; }
            set { Producto.PRO_PRECIO_UNITARIO = value; }
        }
        public byte CveAplicaMayoreo
        {
            get { return Producto.PRO_CVE_APLICA_MAYOREO; }
            set { Producto.PRO_CVE_APLICA_MAYOREO = value; }
        }
        public Nullable<short> CantidadMinimaMayoreo
        {
            get { return Producto.PRO_CAT_MINIMA_MAYOREO; }
            set { Producto.PRO_CAT_MINIMA_MAYOREO = value; }
        }
        public Nullable<decimal> PrecioMayoreo
        {
            get { return Producto.PRO_PRECIO_MAYOREO; }
            set { Producto.PRO_PRECIO_MAYOREO = value; }
        }
        public byte CveEstatus
        {
            get { return Producto.PRO_CVE_ESTATUS; }
            set { Producto.PRO_CVE_ESTATUS = value; }
        }
        public String TextoAviso
        {
            get { return Producto.PRO_TXT_AVISO; }
            set { Producto.PRO_TXT_AVISO = value; }
        }
        public String TextoCorreoSurtido
        {
            get { return Producto.PRO_TXT_CORREO_SURTIDO; }
            set { Producto.PRO_TXT_CORREO_SURTIDO = value; }
        }
        public String NombreDeProveedor
        {
            get { return Producto.PRO_NOM_PROVEEDOR; }
            set { Producto.PRO_NOM_PROVEEDOR = value; }
        }
        public String TextoAplicaMayoreo
        {
            get { return Producto.PRO_TXT_APLICA_MAYOREO; }
            set { Producto.PRO_TXT_APLICA_MAYOREO = value; }
        }
        public String TextoEstatus
        {
            get { return Producto.PRO_TXT_ESTATUS; }
            set { Producto.PRO_TXT_ESTATUS = value; }
        }
        public ClsProductos() { }
        public ClsProductos(Producto Producto)
        {
            NumeroDeProducto = Producto.PRO_NUM_PRODUCTO;
            CodigoDeBarras = Producto.PRO_CODIGO_BARRAS;
            FechaDeRegistro = Producto.PRO_FECHA_REGISTRO;
            CantidadDeProductoUltima = Producto.PRO_CANT_PRODUCTO_ULTIMA;
            CantidadDeProductoNueva = Producto.PRO_CANT_PRODUCTO_NUEVA;
            CantidadDeProductoTotal = Producto.PRO_CANT_PRODUCTO_TOTAL;
            NombreDeProducto = Producto.PRO_NOM_PRODUCTO;
            Descripcion = Producto.PRO_DESCRIPCION;
            CveAviso = Producto.PRO_CVE_AVISO;
            CveCorreoSurtido = Producto.PRO_CVE_CORREO_SURTIDO;
            CantidadMinima = Producto.PRO_CAT_MINIMA;
            NumeroDeProveedor = Producto.PRO_NUM_PROVEEDOR;
            PrecioUnitario = Producto.PRO_PRECIO_UNITARIO;
            CveAplicaMayoreo = Producto.PRO_CVE_APLICA_MAYOREO;
            CantidadMinimaMayoreo = Producto.PRO_CAT_MINIMA_MAYOREO;
            PrecioMayoreo = Producto.PRO_PRECIO_MAYOREO;
            CveEstatus = Producto.PRO_CVE_ESTATUS;
        }
        public bool Existe(bool Dependencia = false)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if ((from q in db.Producto where q.PRO_NUM_PRODUCTO == Producto.PRO_NUM_PRODUCTO select q).Count() != 0)
                    {
                        return true;
                    }
                    else if ((from q in db.Producto where q.PRO_NOM_PRODUCTO.Trim() == Producto.PRO_NOM_PRODUCTO.Trim() select q).Count() != 0)
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
        private bool ToInsert(DBKuupEntities db)
        {
            Producto Producto = this.ToTable();
            db.Producto.Add(Producto);
            db.Entry(Producto).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.Producto where q.PRO_NUM_PRODUCTO == Producto.PRO_NUM_PRODUCTO select q).Count() != 0)
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
            db.Producto.Remove((from q in db.Producto where q.PRO_NUM_PRODUCTO == Producto.PRO_NUM_PRODUCTO select q).FirstOrDefault());
            db.Entry(Producto).State = EntityState.Deleted;
            db.SaveChanges();
            if ((from q in db.Producto where q.PRO_NUM_PRODUCTO == Producto.PRO_NUM_PRODUCTO select q).Count() != 0)
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
            Producto Producto = this.ToTable();
            db.Producto.Attach(Producto);
            db.Entry(Producto).State = EntityState.Modified;
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
        public Producto ToTable()
        {
            Producto Tabla = new Producto();
            Tabla.PRO_NUM_PRODUCTO = this.NumeroDeProducto;
            Tabla.PRO_CODIGO_BARRAS = this.CodigoDeBarras;
            Tabla.PRO_FECHA_REGISTRO = this.FechaDeRegistro;
            Tabla.PRO_CANT_PRODUCTO_ULTIMA = this.CantidadDeProductoUltima;
            Tabla.PRO_CANT_PRODUCTO_NUEVA = this.CantidadDeProductoNueva;
            Tabla.PRO_CANT_PRODUCTO_TOTAL = this.CantidadDeProductoTotal;
            Tabla.PRO_NOM_PRODUCTO = this.NombreDeProducto;
            Tabla.PRO_DESCRIPCION = this.Descripcion;
            Tabla.PRO_CVE_AVISO = this.CveAviso;
            Tabla.PRO_CVE_CORREO_SURTIDO = this.CveCorreoSurtido;
            Tabla.PRO_CAT_MINIMA = this.CantidadMinima;
            Tabla.PRO_NUM_PROVEEDOR = this.NumeroDeProveedor;
            Tabla.PRO_PRECIO_UNITARIO = this.PrecioUnitario;
            Tabla.PRO_CVE_APLICA_MAYOREO = this.CveAplicaMayoreo;
            Tabla.PRO_CAT_MINIMA_MAYOREO = this.CantidadMinimaMayoreo;
            Tabla.PRO_PRECIO_MAYOREO = this.PrecioMayoreo;
            Tabla.PRO_CVE_ESTATUS = this.CveEstatus;
            return Tabla;
        }
        public static List<ClsProductos> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViProducto
                                select new ClsProductos()
                                {
                                    NumeroDeProducto = q.PRO_NUM_PRODUCTO,
                                    CodigoDeBarras = q.PRO_CODIGO_BARRAS,
                                    FechaDeRegistro = q.PRO_FECHA_REGISTRO,
                                    CantidadDeProductoUltima = q.PRO_CANT_PRODUCTO_ULTIMA,
                                    CantidadDeProductoNueva = q.PRO_CANT_PRODUCTO_NUEVA,
                                    CantidadDeProductoTotal = q.PRO_CANT_PRODUCTO_TOTAL,
                                    NombreDeProducto = q.PRO_NOM_PRODUCTO,
                                    Descripcion = q.PRO_DESCRIPCION,
                                    CveAviso = q.PRO_CVE_AVISO,
                                    CveCorreoSurtido = q.PRO_CVE_CORREO_SURTIDO,
                                    CantidadMinima = q.PRO_CAT_MINIMA,
                                    NumeroDeProveedor = q.PRO_NUM_PROVEEDOR,
                                    PrecioUnitario = q.PRO_PRECIO_UNITARIO,
                                    CveAplicaMayoreo = q.PRO_CVE_APLICA_MAYOREO,
                                    CantidadMinimaMayoreo = q.PRO_CAT_MINIMA_MAYOREO,
                                    PrecioMayoreo = q.PRO_PRECIO_MAYOREO,
                                    CveEstatus = q.PRO_CVE_ESTATUS,
                                    TextoAviso = q.PRO_TXT_AVISO,
                                    TextoCorreoSurtido = q.PRO_TXT_CORREO_SURTIDO,
                                    TextoAplicaMayoreo = q.PRO_TXT_APLICA_MAYOREO,
                                    TextoEstatus = q.PRO_TXT_ESTATUS
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.Producto
                                select new ClsProductos()
                                {
                                    NumeroDeProducto = q.PRO_NUM_PRODUCTO,
                                    CodigoDeBarras = q.PRO_CODIGO_BARRAS,
                                    FechaDeRegistro = q.PRO_FECHA_REGISTRO,
                                    CantidadDeProductoUltima = q.PRO_CANT_PRODUCTO_ULTIMA,
                                    CantidadDeProductoNueva = q.PRO_CANT_PRODUCTO_NUEVA,
                                    CantidadDeProductoTotal = q.PRO_CANT_PRODUCTO_TOTAL,
                                    NombreDeProducto = q.PRO_NOM_PRODUCTO,
                                    Descripcion = q.PRO_DESCRIPCION,
                                    CveAviso = q.PRO_CVE_AVISO,
                                    CveCorreoSurtido = q.PRO_CVE_CORREO_SURTIDO,
                                    CantidadMinima = q.PRO_CAT_MINIMA,
                                    NumeroDeProveedor = q.PRO_NUM_PROVEEDOR,
                                    PrecioUnitario = q.PRO_PRECIO_UNITARIO,
                                    CveAplicaMayoreo = q.PRO_CVE_APLICA_MAYOREO,
                                    CantidadMinimaMayoreo = q.PRO_CAT_MINIMA_MAYOREO,
                                    PrecioMayoreo = q.PRO_PRECIO_MAYOREO,
                                    CveEstatus = q.PRO_CVE_ESTATUS
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsProductos>();
        }
    }
}
