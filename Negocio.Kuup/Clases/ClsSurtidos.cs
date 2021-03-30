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
using System.Linq.Dynamic;

namespace Negocio.Kuup.Clases
{
    public class ClsSurtidos : Interfaces.InterfazGen<ClsSurtidos>
    {
        private DBKuupEntities _db = null;
        public DBKuupEntities db
        {
            get { return _db; }
            set { _db = value; }
        }
        public short NumeroDePantallaKuup
        {
            get { return 21; }
        }
        ViSurtido Surtido = new ViSurtido();
        public short FolioDeSurtido
        {
            get { return Surtido.SUR_FOLIO_SURTIDO; }
            set { Surtido.SUR_FOLIO_SURTIDO = value; }
        }
        public byte CveDeAplicaSurtido
        {
            get { return Surtido.SUR_CVE_APLICA_PROVEEDOR; }
            set { Surtido.SUR_CVE_APLICA_PROVEEDOR = value; }
        }
        public Nullable<byte> NumeroDeProveedor
        {
            get { return Surtido.SUR_NUM_PROVEEDOR; }
            set { Surtido.SUR_NUM_PROVEEDOR = value; }
        }
        public Nullable<short> NumeroDeUsuario
        {
            get { return Surtido.SUR_NUM_USUARIO; }
            set { Surtido.SUR_NUM_USUARIO = value; }
        }
        public short NumeroDeProducto
        {
            get { return Surtido.SUR_NUM_PRODUCTO; }
            set { Surtido.SUR_NUM_PRODUCTO = value; }
        }
        public String CodigoDeBarras
        {
            get { return Surtido.SUR_CODIGO_BARRAS; }
            set { Surtido.SUR_CODIGO_BARRAS = value; }
        }
        public short CantidadPrevia
        {
            get { return Surtido.SUR_CANT_PREVIA; }
            set { Surtido.SUR_CANT_PREVIA = value; }
        }
        public short CantidadNueva
        {
            get { return Surtido.SUR_CANT_NUEVA; }
            set { Surtido.SUR_CANT_NUEVA = value; }
        }
        public Nullable<decimal> PrecioUnitario
        {
            get { return Surtido.SUR_PRECIO_UNITARIO; }
            set { Surtido.SUR_PRECIO_UNITARIO = value; }
        }
        public Nullable<decimal> CostoTotal
        {
            get { return Surtido.SUR_COSTO_TOTAL; }
            set { Surtido.SUR_COSTO_TOTAL = value; }
        }
        public System.DateTime FechaDeSurtido
        {
            get { return Surtido.SUR_FECHA_SURTIDO; }
            set { Surtido.SUR_FECHA_SURTIDO = value; }
        }
        public byte CveDeEstatus
        {
            get { return Surtido.SUR_CVE_ESTATUS; }
            set { Surtido.SUR_CVE_ESTATUS = value; }
        }
        public String TextoDeAplicaProveedor
        {
            get { return Surtido.SUR_TXT_APLICA_MAYOREO; }
            set { Surtido.SUR_TXT_APLICA_MAYOREO = value; }
        }
        public String NombreDeProveedor
        {
            get { return Surtido.SUR_NOM_PROVEEDOR; }
            set { Surtido.SUR_NOM_PROVEEDOR = value; }
        }
        public String NombreDeUsuario
        {
            get { return Surtido.SUR_NOM_USUARIO; }
            set { Surtido.SUR_NOM_USUARIO = value; }
        }
        public String NombreDeProducto
        {
            get { return Surtido.SUR_NOM_PRODUCTO; }
            set { Surtido.SUR_NOM_PRODUCTO = value; }
        }
        public String TextoDeEstatus
        {
            get { return Surtido.SUR_TXT_ESTATUS; }
            set { Surtido.SUR_TXT_ESTATUS = value; }
        }
        public ClsSurtidos() { }
        private bool ToInsert(DBKuupEntities db)
        {
            Surtido Surtido = this.ToTable();
            db.Surtido.Add(Surtido);
            db.Entry(Surtido).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.Surtido where q.SUR_FOLIO_SURTIDO == Surtido.SUR_FOLIO_SURTIDO  select q).Count() != 0)
            {
                return true;
            }
            return false;
        }
        public bool Insert()
        {
            try
            {
                if (_db == null)
                {
                    using (DBKuupEntities db = new DBKuupEntities())
                    {
                        return ToInsert(db);
                    }
                }
                else
                {
                    return ToInsert(_db);
                }
            }
            catch (Exception e)
            {
                ClsBitacora.GeneraBitacora(NumeroDePantallaKuup, 1, "Insert", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;
            }
        }
        private bool ToDelete(DBKuupEntities db)
        {
            db.Surtido.Remove((from q in db.Surtido where q.SUR_FOLIO_SURTIDO == Surtido.SUR_FOLIO_SURTIDO select q).FirstOrDefault());
            db.SaveChanges();
            if ((from q in db.Surtido where q.SUR_FOLIO_SURTIDO == Surtido.SUR_FOLIO_SURTIDO  select q).Count() != 0)
            {
                return false;
            }
            return true;
        }
        public bool Delete()
        {
            try
            {
                if (_db == null)
                {
                    using (DBKuupEntities db = new DBKuupEntities())
                    {
                        return ToDelete(db);
                    }
                }
                else
                {
                    return ToDelete(_db);
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
            Surtido Surtido = this.ToTable();
            db.Surtido.Attach(Surtido);
            db.Entry(Surtido).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
        public bool Update()
        {
            try
            {
                if (_db == null)
                {
                    using (DBKuupEntities db = new DBKuupEntities())
                    {
                        return ToUpdate(db);
                    }
                }
                else
                {
                    return ToUpdate(_db);
                }
            }
            catch (Exception e)
            {
                ClsBitacora.GeneraBitacora(NumeroDePantallaKuup, 1, "Update", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;

            }
        }
        public Surtido ToTable()
        {
            Surtido Tabla = new Surtido();
            Tabla.SUR_FOLIO_SURTIDO = this.FolioDeSurtido;
            Tabla.SUR_CVE_APLICA_PROVEEDOR = this.CveDeAplicaSurtido;
            Tabla.SUR_NUM_PROVEEDOR = this.NumeroDeProveedor;
            Tabla.SUR_NUM_USUARIO = this.NumeroDeUsuario;
            Tabla.SUR_NUM_PRODUCTO = this.NumeroDeProducto;
            Tabla.SUR_CODIGO_BARRAS = this.CodigoDeBarras;
            Tabla.SUR_CANT_PREVIA = this.CantidadPrevia;
            Tabla.SUR_CANT_NUEVA = this.CantidadNueva;
            Tabla.SUR_PRECIO_UNITARIO = this.PrecioUnitario;
            Tabla.SUR_COSTO_TOTAL = this.CostoTotal;
            Tabla.SUR_FECHA_SURTIDO = this.FechaDeSurtido;
            Tabla.SUR_CVE_ESTATUS = this.CveDeEstatus;
            return Tabla;
        }
        public static List<ClsSurtidos> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViSurtido
                                select new ClsSurtidos()
                                {
                                    FolioDeSurtido = q.SUR_FOLIO_SURTIDO,
                                    CveDeAplicaSurtido = q.SUR_CVE_APLICA_PROVEEDOR,
                                    NumeroDeProveedor = q.SUR_NUM_PROVEEDOR,
                                    NumeroDeUsuario = q.SUR_NUM_USUARIO,
                                    NumeroDeProducto = q.SUR_NUM_PRODUCTO,
                                    CodigoDeBarras = q.SUR_CODIGO_BARRAS,
                                    CantidadPrevia = q.SUR_CANT_PREVIA,
                                    CantidadNueva = q.SUR_CANT_NUEVA,
                                    PrecioUnitario = q.SUR_PRECIO_UNITARIO,
                                    CostoTotal = q.SUR_COSTO_TOTAL,
                                    FechaDeSurtido = q.SUR_FECHA_SURTIDO,
                                    CveDeEstatus = q.SUR_CVE_ESTATUS,
                                    TextoDeAplicaProveedor = q.SUR_TXT_APLICA_MAYOREO,
                                    NombreDeProveedor = q.SUR_NOM_PROVEEDOR,
                                    NombreDeUsuario = q.SUR_NOM_USUARIO,
                                    NombreDeProducto = q.SUR_NOM_PRODUCTO,
                                    TextoDeEstatus = q.SUR_TXT_ESTATUS
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.Surtido
                                select new ClsSurtidos()
                                {
                                    FolioDeSurtido = q.SUR_FOLIO_SURTIDO,
                                    CveDeAplicaSurtido = q.SUR_CVE_APLICA_PROVEEDOR,
                                    NumeroDeProveedor = q.SUR_NUM_PROVEEDOR,
                                    NumeroDeUsuario = q.SUR_NUM_USUARIO,
                                    NumeroDeProducto = q.SUR_NUM_PRODUCTO,
                                    CodigoDeBarras = q.SUR_CODIGO_BARRAS,
                                    CantidadPrevia = q.SUR_CANT_PREVIA,
                                    CantidadNueva = q.SUR_CANT_NUEVA,
                                    PrecioUnitario = q.SUR_PRECIO_UNITARIO,
                                    CostoTotal = q.SUR_COSTO_TOTAL,
                                    FechaDeSurtido = q.SUR_FECHA_SURTIDO,
                                    CveDeEstatus = q.SUR_CVE_ESTATUS
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsSurtidos>();
        }
        public Object DataTableSurtido(Globales.ClsDataTables RequesDT)
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
                var Query = (from q in db.ViSurtido
                             select new ClsSurtidos
                             {
                                 FolioDeSurtido = q.SUR_FOLIO_SURTIDO,
                                 NombreDeProducto = q.SUR_NOM_PRODUCTO,
                                 NombreDeUsuario = (q.SUR_NOM_PROVEEDOR == null ? q.SUR_NOM_USUARIO : q.SUR_NOM_PROVEEDOR),
                                 CantidadNueva = q.SUR_CANT_NUEVA,
                                 FechaDeSurtido = q.SUR_FECHA_SURTIDO,
                                 TextoDeEstatus = q.SUR_TXT_ESTATUS
                             }).AsQueryable();
                String sql = String.Empty;
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[0][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("FolioDeSurtido.ToString().Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[0][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[1][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("NombreDeProducto.Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[1][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[2][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("NombreDeUsuario.Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[2][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[3][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("CantidadNueva.ToString().Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[3][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[4][search][value]").FirstOrDefault()))
                {
                    String Fecha = RequesDT.Form.GetValues("columns[4][search][value]").FirstOrDefault().Trim().ToUpper();
                    if (!String.IsNullOrEmpty(Fecha))
                    {
                        if (Fecha.Split('-').Length == 3)
                        {
                            if (!String.IsNullOrEmpty(sql))
                            {
                                sql += " && ";
                            }
                            sql += String.Format("FechaDeSurtido == Datetime({0},{1},{2})", Fecha.Split('-')[0], Fecha.Split('-')[1], Fecha.Split('-')[2]);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[5][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("TextoDeEstatus.Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[5][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!String.IsNullOrEmpty(sql))
                {
                    Query = Query.Where(sql);
                }
                //if (!String.IsNullOrEmpty(RequesDT.searchValue))
                //{
                //    Query = Query.Where(x => x.FolioDeSurtido.ToString().Contains(RequesDT.searchValue.Trim().ToUpper()) ||
                //        x.NombreDeProducto.Trim().ToUpper().Contains(RequesDT.searchValue.Trim().ToUpper()));
                //}
                if (!(string.IsNullOrEmpty(RequesDT.sortColumn) && string.IsNullOrEmpty(RequesDT.sortColumnDir)))
                {
                    Query = Query.OrderBy(RequesDT.sortColumn + " " + RequesDT.sortColumnDir);
                }
                RequesDT.recordsTotal = Query.Count();

                var ProductoTable = Query.Skip(RequesDT.skip).Take(RequesDT.pageSize).ToArray();
                

                RequesDT.DatosJson = new { RequesDT.draw, recordsFiltered = RequesDT.recordsTotal, RequesDT.recordsTotal, data = ProductoTable.Select(x => new
                {
                    x.FolioDeSurtido,
                    x.NombreDeProducto,
                    NombreDeQuienSurtio = x.NombreDeUsuario,
                    x.CantidadNueva,
                    FechaDeSurtido = x.FechaDeSurtido.ToString("yyyy-MM-dd"),
                    x.TextoDeEstatus
                })
            };
            }
            return RequesDT.DatosJson;
        }
    }
}
