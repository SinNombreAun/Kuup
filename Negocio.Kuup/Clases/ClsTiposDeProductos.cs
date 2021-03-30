using Mod.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;

namespace Negocio.Kuup.Clases
{
    public class ClsTiposDeProductos : Interfaces.InterfazGen<ClsTiposDeProductos>
    {
        private DBKuupEntities _db = null;
        public DBKuupEntities db
        {
            get { return _db; }
            set { _db = value; }
        }
        public short NumeroDePantallaKuup
        {
            get { return 25; }
        }
        ViTipoProducto TipoProducto = new ViTipoProducto();
        public short NumeroDeTipoDeProducto
        {
            get { return TipoProducto.TPO_NUM_TIPO_PRODUCTO; }
            set { TipoProducto.TPO_NUM_TIPO_PRODUCTO = value; }
        }
        public String NombreDeTipoDeProducto
        {
            get { return TipoProducto.TPO_NOM_TIPO_PRODUCTO; }
            set { TipoProducto.TPO_NOM_TIPO_PRODUCTO = value; }
        }
        public byte CveDeEstatus
        {
            get { return TipoProducto.TPO_CVE_ESTATUS; }
            set { TipoProducto.TPO_CVE_ESTATUS = value; }
        }
        public String TextoDeEstatus
        {
            get { return TipoProducto.TPO_TXT_ESTATUS; }
            set { TipoProducto.TPO_TXT_ESTATUS = value; }
        }
        public ClsTiposDeProductos() { }
        public ClsTiposDeProductos(TipoProducto TipoProducto)
        {
            NumeroDeTipoDeProducto = TipoProducto.TPO_NUM_TIPO_PRODUCTO;
            NombreDeTipoDeProducto = TipoProducto.TPO_NOM_TIPO_PRODUCTO;
            CveDeEstatus = TipoProducto.TPO_CVE_ESTATUS;
        }
        public bool Existe()
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if ((from q in db.TipoProducto where q.TPO_NUM_TIPO_PRODUCTO == TipoProducto.TPO_NUM_TIPO_PRODUCTO && q.TPO_CVE_ESTATUS == 1 select q).Count() != 0)
                    {
                        return true;
                    }
                    if ((from q in db.TipoProducto where q.TPO_NOM_TIPO_PRODUCTO.Equals(TipoProducto.TPO_NOM_TIPO_PRODUCTO) && q.TPO_CVE_ESTATUS == 1 select q).Count() != 0)
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
            TipoProducto TipoProducto = this.ToTable();
            db.TipoProducto.Add(TipoProducto);
            db.Entry(TipoProducto).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.TipoProducto where q.TPO_NUM_TIPO_PRODUCTO == TipoProducto.TPO_NUM_TIPO_PRODUCTO && q.TPO_CVE_ESTATUS == 1 select q).Count() != 0)
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
            try
            {
                TipoProductoAudit Audit = new TipoProductoAudit()
                {
                    TPO_ID_AUDIT = ObjAudit.IdAudit,
                    TPO_TERMINAL = ObjAudit.Terminal,
                    TPO_IP = ObjAudit.IP,
                    TPO_VERSION = ObjAudit.Version,
                    TPO_NOM_USUARIO = ObjAudit.NombreDeUsuario,
                    TPO_FECHA_BASE = DateTime.Now,
                    TPO_NOM_FUNCIONALIDAD = ObjAudit.NombreDeFuncionalidad,
                    TPO_NUM_TIPO_PRODUCTO = TipoProducto.TPO_NUM_TIPO_PRODUCTO,
                    TPO_NOM_TIPO_PRODUCTO = TipoProducto.TPO_NOM_TIPO_PRODUCTO,
                    TPO_CVE_ESTATUS = TipoProducto.TPO_CVE_ESTATUS,
                    TPO_TXT_ESTATUS = TipoProducto.TPO_TXT_ESTATUS
                };
                if (_db == null)
                {
                    using (DBKuupEntities db = new DBKuupEntities())
                    {
                        db.TipoProductoAudit.Add(Audit);
                        db.Entry(Audit).State = EntityState.Added;
                        db.SaveChanges();
                        if ((from q in db.TipoProductoAudit where q.TPO_ID_AUDIT == Audit.TPO_ID_AUDIT && q.TPO_NUM_TIPO_PRODUCTO == Audit.TPO_NUM_TIPO_PRODUCTO select q).Count() != 0)
                        {
                            return true;
                        }
                        return false;
                    }
                }
                else
                {
                    _db.TipoProductoAudit.Add(Audit);
                    _db.Entry(Audit).State = EntityState.Added;
                    _db.SaveChanges();
                    if ((from q in _db.TipoProductoAudit where q.TPO_ID_AUDIT == Audit.TPO_ID_AUDIT && q.TPO_NUM_TIPO_PRODUCTO == Audit.TPO_NUM_TIPO_PRODUCTO select q).Count() != 0)
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
        public bool ToDelete(DBKuupEntities db)
        {
            db.TipoProducto.Remove((from q in db.TipoProducto where q.TPO_NUM_TIPO_PRODUCTO == TipoProducto.TPO_NUM_TIPO_PRODUCTO select q).FirstOrDefault());
            db.SaveChanges();
            if ((from q in db.TipoProducto where q.TPO_NUM_TIPO_PRODUCTO == TipoProducto.TPO_NUM_TIPO_PRODUCTO select q).Count() != 0)
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
            TipoProducto TipoProducto = this.ToTable();
            db.TipoProducto.Attach(TipoProducto);
            db.Entry(TipoProducto).State = EntityState.Modified;
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
        public TipoProducto ToTable()
        {
            TipoProducto Tabla = new TipoProducto()
            {
                TPO_NUM_TIPO_PRODUCTO = this.NumeroDeTipoDeProducto,
                TPO_NOM_TIPO_PRODUCTO = this.NombreDeTipoDeProducto,
                TPO_CVE_ESTATUS = this.CveDeEstatus
            };
            return Tabla;
        }
        public static List<ClsTiposDeProductos> getList(String filtro = "", bool EsVista = true, List<short> listaTipoProductos = null)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        var Query = (from q in db.ViTipoProducto
                                     select new ClsTiposDeProductos()
                                     {
                                         NumeroDeTipoDeProducto = q.TPO_NUM_TIPO_PRODUCTO,
                                         NombreDeTipoDeProducto = q.TPO_NOM_TIPO_PRODUCTO,
                                         CveDeEstatus = q.TPO_CVE_ESTATUS,
                                         TextoDeEstatus = q.TPO_TXT_ESTATUS
                                     }).AsQueryable();
                        if (!String.IsNullOrEmpty(filtro))
                        {
                            if (listaTipoProductos == null)
                            {
                                Query = Query.Where(filtro);
                            }
                            else
                            {
                                Query = Query.Where("NumeroDeTipoProducto.Contains(@0)", listaTipoProductos);
                            }
                        }
                        return Query.ToList();
                    }
                    else
                    {
                        var Query = (from q in db.ViTipoProducto
                                     select new ClsTiposDeProductos()
                                     {
                                         NumeroDeTipoDeProducto = q.TPO_NUM_TIPO_PRODUCTO,
                                         NombreDeTipoDeProducto = q.TPO_NOM_TIPO_PRODUCTO,
                                         CveDeEstatus = q.TPO_CVE_ESTATUS
                                     }).AsQueryable();
                        if (!String.IsNullOrEmpty(filtro))
                        {
                            if (listaTipoProductos == null)
                            {
                                Query = Query.Where(filtro);
                            }
                            else
                            {
                                Query = Query.Where("NumeroDeTipoProducto.Contains(@0)", listaTipoProductos);
                            }
                        }
                        return Query.ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsTiposDeProductos>();
        }
        public Object DataTableTipoProducto(Globales.ClsDataTables RequesDT)
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
                var Query = (from q in db.ViTipoProducto
                             select new ClsTiposDeProductos()
                             {
                                 NumeroDeTipoDeProducto = q.TPO_NUM_TIPO_PRODUCTO,
                                 NombreDeTipoDeProducto = q.TPO_NOM_TIPO_PRODUCTO,
                                 TextoDeEstatus = q.TPO_TXT_ESTATUS
                             }).AsQueryable();
                String sql = String.Empty;
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[0][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("NumeroDeTipoDeProducto = {0}", RequesDT.Form.GetValues("columns[0][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[1][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("NombreDeTipoDeProducto.Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[1][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[2][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("TextoDeEstatus.Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[2][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!String.IsNullOrEmpty(sql))
                {
                    Query = Query.Where(sql);
                }
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
