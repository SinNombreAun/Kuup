using Mod.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;

namespace Negocio.Kuup.Clases
{
    public class ClsProductos : Interfaces.InterfazGen<ClsProductos>
    {
        public DBKuupEntities db { get; set; }
        public short NumeroDePantallaKuup
        {
            get { return 5; }
        }
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
        public byte CveDeEstatus
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
        public String TextoDeEstatus
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
            CveDeEstatus = Producto.PRO_CVE_ESTATUS;
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
                    else if ((from q in db.Producto where q.PRO_NOM_PRODUCTO.Trim() == Producto.PRO_NOM_PRODUCTO.Trim() && q.PRO_CVE_ESTATUS == 1 select q).Count() != 0)
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
                ClsBitacora.GeneraBitacora(NumeroDePantallaKuup, 1, "Insert", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;
            }
        }
        public bool InsertAudit(ClsAudit ObjAudit)
        {
            try
            {
                ProductoAudit Audit = new ProductoAudit();
                Audit.PRO_ID_AUDIT = ObjAudit.IdAudit;
                Audit.PRO_TERMINAL = ObjAudit.Terminal;
                Audit.PRO_IP = ObjAudit.IP;
                Audit.PRO_VERSION = ObjAudit.Version;
                Audit.PRO_NOM_USUARIO = ObjAudit.NombreDeUsuario;
                Audit.PRO_FECHA_BASE = DateTime.Now;
                Audit.PRO_NOM_FUNCIONALIDAD = ObjAudit.NombreDeFuncionalidad;
                Audit.PRO_NUM_PRODUCTO = Producto.PRO_NUM_PRODUCTO;
                Audit.PRO_CODIGO_BARRAS = Producto.PRO_CODIGO_BARRAS;
                Audit.PRO_FECHA_REGISTRO = Producto.PRO_FECHA_REGISTRO;
                Audit.PRO_CANT_PRODUCTO_ULTIMA = Producto.PRO_CANT_PRODUCTO_ULTIMA;
                Audit.PRO_CANT_PRODUCTO_NUEVA = Producto.PRO_CANT_PRODUCTO_NUEVA;
                Audit.PRO_CANT_PRODUCTO_TOTAL = Producto.PRO_CANT_PRODUCTO_TOTAL;
                Audit.PRO_NOM_PRODUCTO = Producto.PRO_NOM_PRODUCTO;
                Audit.PRO_DESCRIPCION = Producto.PRO_DESCRIPCION;
                Audit.PRO_CVE_AVISO = Producto.PRO_CVE_AVISO;
                Audit.PRO_CVE_CORREO_SURTIDO = Producto.PRO_CVE_CORREO_SURTIDO;
                Audit.PRO_CAT_MINIMA = Producto.PRO_CAT_MINIMA;
                Audit.PRO_NUM_PROVEEDOR = Producto.PRO_NUM_PROVEEDOR;
                Audit.PRO_PRECIO_UNITARIO = Producto.PRO_PRECIO_UNITARIO;
                Audit.PRO_CVE_ESTATUS = Producto.PRO_CVE_ESTATUS;
                Audit.PRO_TXT_AVISO = Producto.PRO_TXT_AVISO;
                Audit.PRO_TXT_CORREO_SURTIDO = Producto.PRO_TXT_CORREO_SURTIDO;
                Audit.PRO_NOM_PROVEEDOR = Producto.PRO_NOM_PROVEEDOR;
                Audit.PRO_TXT_ESTATUS = Producto.PRO_TXT_ESTATUS;
                if (db == null)
                {
                    using (db = new DBKuupEntities())
                    {
                        db.ProductoAudit.Add(Audit);
                        db.Entry(Audit).State = EntityState.Added;
                        db.SaveChanges();
                        if ((from q in db.ProductoAudit where q.PRO_ID_AUDIT == Audit.PRO_ID_AUDIT && q.PRO_NUM_PRODUCTO == Audit.PRO_NUM_PRODUCTO select q).Count() != 0)
                        {
                            return true;
                        }
                        return false;
                    }
                }
                else
                {
                    db.ProductoAudit.Add(Audit);
                    db.Entry(Audit).State = EntityState.Added;
                    db.SaveChanges();
                    if ((from q in db.ProductoAudit where q.PRO_ID_AUDIT == Audit.PRO_ID_AUDIT && q.PRO_NUM_PRODUCTO == Audit.PRO_NUM_PRODUCTO select q).Count() != 0)
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
            db.Producto.Remove((from q in db.Producto where q.PRO_NUM_PRODUCTO == Producto.PRO_NUM_PRODUCTO select q).FirstOrDefault());
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
                ClsBitacora.GeneraBitacora(NumeroDePantallaKuup, 1, "Delete", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
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
                ClsBitacora.GeneraBitacora(NumeroDePantallaKuup, 1, "Update", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
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
            Tabla.PRO_CVE_ESTATUS = this.CveDeEstatus;
            return Tabla;
        }
        public static List<ClsProductos> getList(String filtro = "", bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        var Query = (from q in db.ViProducto
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
                                         CveDeEstatus = q.PRO_CVE_ESTATUS,
                                         TextoAviso = q.PRO_TXT_AVISO,
                                         TextoCorreoSurtido = q.PRO_TXT_CORREO_SURTIDO,
                                         TextoDeEstatus = q.PRO_TXT_ESTATUS
                                     }).AsQueryable();
                        if (!String.IsNullOrEmpty(filtro))
                        {
                            Query = Query.Where(filtro);
                        }
                        return Query.ToList();
                    }
                    else
                    {
                        var Query = (from q in db.Producto
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
                                         CveDeEstatus = q.PRO_CVE_ESTATUS
                                     }).AsQueryable();
                        if (!String.IsNullOrEmpty(filtro))
                        {
                            Query = Query.Where(filtro);
                        }
                        return Query.ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsProductos>();
        }
        public Object DataTableProducto(Globales.ClsDataTables RequesDT)
        {
            RequesDT.draw = RequesDT.Form.GetValues("draw").FirstOrDefault();
            RequesDT.start = RequesDT.Form.GetValues("start").FirstOrDefault();
            RequesDT.length = RequesDT.Form.GetValues("length").FirstOrDefault();
            RequesDT.sortColumn = RequesDT.Form.GetValues("columns[" + RequesDT.Form.GetValues("order[0][column]").FirstOrDefault() + "][data]").FirstOrDefault();
            RequesDT.sortColumnDir = RequesDT.Form.GetValues("order[0][dir]").FirstOrDefault();
            RequesDT.searchValue = RequesDT.Form.GetValues("search[value]").FirstOrDefault();

            RequesDT.pageSize = RequesDT.length != null ? Convert.ToInt32(RequesDT.length) : 0;
            RequesDT.skip = RequesDT.start != null ? Convert.ToInt32(RequesDT.start) : 0;
            RequesDT.recordsTotal = 0;
            using (DBKuupEntities db = new DBKuupEntities())
            {
                var Query = (from q in db.ViProducto
                             select new ClsProductos()
                             {
                                 NumeroDeProducto = q.PRO_NUM_PRODUCTO,
                                 CodigoDeBarras = q.PRO_CODIGO_BARRAS,
                                 NombreDeProducto = q.PRO_NOM_PRODUCTO,
                                 CantidadDeProductoTotal = q.PRO_CANT_PRODUCTO_TOTAL,
                                 PrecioUnitario = q.PRO_PRECIO_UNITARIO,
                                 TextoAviso = q.PRO_TXT_AVISO,
                                 TextoCorreoSurtido = q.PRO_TXT_CORREO_SURTIDO,
                                 TextoDeEstatus = q.PRO_TXT_ESTATUS
                             }).AsQueryable();
                String sql = String.Empty;
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[0][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("NumeroDeProducto.ToString().Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[0][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[1][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("CodigoDeBarras.Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[1][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[2][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("NombreDeProducto.Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[2][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[3][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("CantidadDeProductoTotal.ToString().Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[3][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[4][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("PrecioUnitario.ToString().Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[4][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[5][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("TextoAviso.Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[5][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[6][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("TextoCorreoSurtido.Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[6][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[7][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("TextoDeEstatus.Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[7][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!String.IsNullOrEmpty(sql))
                {
                    Query = Query.Where(sql);
                }
                //if (!String.IsNullOrEmpty(RequesDT.searchValue))
                //{
                //    Query = Query.Where(x => x.NombreDeProducto.Trim().ToUpper().Contains(RequesDT.searchValue.Trim().ToUpper()) ||
                //    x.CodigoDeBarras.Trim().ToUpper().Contains(RequesDT.searchValue.Trim().ToUpper()));
                //}
                if (!(string.IsNullOrEmpty(RequesDT.sortColumn) && string.IsNullOrEmpty(RequesDT.sortColumnDir)))
                {
                    Query = Query.OrderBy(RequesDT.sortColumn + " " + RequesDT.sortColumnDir);
                }
                RequesDT.recordsTotal = Query.Count();

                var ProductoTable = Query.Skip(RequesDT.skip).Take(RequesDT.pageSize).ToArray();

                RequesDT.DatosJson = new { RequesDT.draw, recordsFiltered = RequesDT.recordsTotal, RequesDT.recordsTotal, data = ProductoTable };
            }
            return RequesDT.DatosJson;
        }
    }
}
