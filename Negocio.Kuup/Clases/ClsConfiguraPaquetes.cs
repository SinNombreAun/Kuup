using Mod.Entity;
using Negocio.Kuup.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;

namespace Negocio.Kuup.Clases
{
    public class ClsConfiguraPaquetes : Interfaces.InterfazGen<ClsConfiguraPaquetes>
    {
        private DBKuupEntities _db = null;
        public DBKuupEntities db
        {
            get { return _db; }
            set { _db = value; }
        }
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
        public bool Existe(bool Dependencia = false)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if ((from q in db.ConfiguraPaquete where q.CNP_NUM_PRODUCTO_PADRE == ConfiguraPaquete.CNP_NUM_PRODUCTO_PADRE && q.CNP_NUM_PRODUCTO_HIJO == ConfiguraPaquete.CNP_NUM_PRODUCTO_HIJO select q).Count() != 0)
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
                if (_db == null)
                {
                    using (DBKuupEntities db = new DBKuupEntities())
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
                    _db.ConfiguraPaqueteAudit.Add(Audit);
                    _db.Entry(Audit).State = EntityState.Added;
                    _db.SaveChanges();
                    if ((from q in _db.ConfiguraPaqueteAudit where q.CNP_ID_AUDIT == Audit.CNP_ID_AUDIT && q.CNP_NUM_PRODUCTO_PADRE == Audit.CNP_NUM_PRODUCTO_PADRE && q.CNP_CODIGO_BARRAS_PADRE == Audit.CNP_CODIGO_BARRAS_PADRE && q.CNP_NUM_PRODUCTO_HIJO == Audit.CNP_NUM_PRODUCTO_HIJO && q.CNP_CODIGO_BARRAS_HIJO == Audit.CNP_CODIGO_BARRAS_HIJO select q).Count() != 0)
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
        public static List<ClsConfiguraPaquetes> getList(String filtro = "", bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        var Query = (from q in db.ViConfiguraPaquete
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
                                }).AsQueryable();
                        if (!String.IsNullOrEmpty(filtro))
                        {
                            Query = Query.Where(filtro);
                        }
                        return Query.ToList();
                    }
                    else
                    {
                        var Query = (from q in db.ConfiguraPaquete
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
            return new List<ClsConfiguraPaquetes>();
        }
        public Object DataTableConfiguraPaquete(Globales.ClsDataTables RequesDT)
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
            using(DBKuupEntities db = new DBKuupEntities())
            {
                var Query = (from q in db.ViConfiguraPaquete
                             select new ClsConfiguraPaquetes()
                             {
                                 CodigoDeBarrasPadre = q.CNP_CODIGO_BARRAS_PADRE,
                                 NombreDeProductoPadre = q.CNP_NOM_PRODUCTO_PADRE,
                                 PrecioDeProductoPadre = q.CNP_PRECIO_PRODUCTO_PADRE,
                                 CodigoDeBarrasHijo = q.CNP_CODIGO_BARRAS_HIJO,
                                 NombreDeProductoHijo = q.CNP_NOM_PRODUCTO_HIJO,
                                 PrecioDeProductoHijo = q.CNP_PRECIO_PRODUCTO_HIJO,
                                 ImporteTotal = q.CNP_IMPORTE_TOTAL,
                                 NumeroDeProductoPadre = q.CNP_NUM_PRODUCTO_PADRE,
                                 NumeroDeProductoHijo = q.CNP_NUM_PRODUCTO_HIJO
                             }).AsQueryable();
                String sql = String.Empty;
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[0][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("CodigoDeBarrasPadre.Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[0][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[1][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("NombreDeProductoPadre.Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[1][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[2][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("PrecioDeProductoPadre.ToString().Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[2][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[3][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("CodigoDeBarrasHijo.ToString().Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[3][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[4][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("NombreDeProductoHijo.Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[4][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[5][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("PrecioDeProductoHijo.ToString().Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[5][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[6][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("ImporteTotal.ToString().Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[6][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!String.IsNullOrEmpty(sql))
                {
                    Query = Query.Where(sql);
                }
                //if (!String.IsNullOrEmpty(RequesDT.searchValue))
                //{
                //    Query = Query.Where(x => x.CodigoDeBarrasPadre.Trim().ToUpper().Contains(RequesDT.searchValue.Trim().ToUpper()) ||
                //    x.NombreDeProductoPadre.Trim().ToUpper().Contains(RequesDT.searchValue.Trim().ToUpper()) ||
                //    x.CodigoDeBarrasHijo.Trim().ToUpper().Contains(RequesDT.searchValue.Trim().ToUpper()) ||
                //    x.NombreDeProductoHijo.Trim().ToUpper().Contains(RequesDT.searchValue.Trim().ToUpper()));
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
