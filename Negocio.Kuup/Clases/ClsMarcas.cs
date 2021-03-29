using Mod.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;

namespace Negocio.Kuup.Clases
{
    public class ClsMarcas : Interfaces.InterfazGen<ClsMarcas>
    {
        public DBKuupEntities db { get; set; }
        public short NumeroDePantallaKuup
        {
            get { return 23; }
        }
        ViMarca Marca = new ViMarca();
        public short NumeroDeMarca
        {
            get { return Marca.MRA_NUM_MARCA; }
            set { Marca.MRA_NUM_MARCA = value; }
        }
        public String NombreDeMarca
        {
            get { return Marca.MRA_NOM_MARCA; }
            set { Marca.MRA_NOM_MARCA = value; }
        }
        public byte CveDeEstatus
        {
            get { return Marca.MRA_CVE_ESTATUS; }
            set { Marca.MRA_CVE_ESTATUS = value; }
        }
        public String TextoDeEstatus
        {
            get { return Marca.MRA_TXT_ESTATUS; }
            set { Marca.MRA_TXT_ESTATUS = value; }
        }
        public ClsMarcas() { }
        public ClsMarcas(Marca Marca)
        {
            NumeroDeMarca = Marca.MRA_NUM_MARCA;
            NombreDeMarca = Marca.MRA_NOM_MARCA;
            CveDeEstatus = Marca.MRA_CVE_ESTATUS;
        }
        public bool Existe()
        {
            try
            {
                using(DBKuupEntities db = new DBKuupEntities())
                {
                    if((from q in db.Marca where q.MRA_NUM_MARCA == Marca.MRA_NUM_MARCA && q.MRA_CVE_ESTATUS == 1 select q).Count() != 0)
                    {
                        return true;
                    }
                    if((from q in db.Marca where q.MRA_NOM_MARCA.Equals(Marca.MRA_NOM_MARCA) && q.MRA_CVE_ESTATUS == 1 select q).Count() != 0)
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
        private bool ToInsert(DBKuupEntities db)
        {
            Marca Marca = this.ToTable();
            db.Marca.Add(Marca);
            db.Entry(Marca).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.Marca where q.MRA_NUM_MARCA == Marca.MRA_NUM_MARCA && q.MRA_CVE_ESTATUS == 1 select q).Count() != 0)
            {
                return true;
            }
            return false;
        }
        public bool Insert()
        {
            try
            {
                if(db == null)
                {
                    using(db = new DBKuupEntities())
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
                MarcaAudit Audit = new MarcaAudit() {
                    MRA_ID_AUDIT = ObjAudit.IdAudit,
                    MRA_TERMINAL = ObjAudit.Terminal,
                    MRA_IP = ObjAudit.IP,
                    MRA_VERSION = ObjAudit.Version,
                    MRA_NOM_USUARIO = ObjAudit.NombreDeUsuario,
                    MRA_FECHA_BASE = DateTime.Now,
                    MRA_NOM_FUNCIONALIDAD = ObjAudit.NombreDeFuncionalidad,
                    MRA_NUM_MARCA = Marca.MRA_NUM_MARCA,
                    MRA_NOM_MARCA = Marca.MRA_NOM_MARCA,
                    MRA_CVE_ESTATUS = Marca.MRA_CVE_ESTATUS,
                    MRA_TXT_ESTATUS = Marca.MRA_TXT_ESTATUS
                };
                if(db == null)
                {
                    using(db = new DBKuupEntities())
                    {
                        db.MarcaAudit.Add(Audit);
                        db.Entry(Audit).State = EntityState.Added;
                        db.SaveChanges();
                        if ((from q in db.MarcaAudit where q.MRA_ID_AUDIT == Audit.MRA_ID_AUDIT && q.MRA_NUM_MARCA == Audit.MRA_NUM_MARCA select q).Count() != 0)
                        {
                            return true;
                        }
                        return false;
                    }
                }
                else
                {
                    db.MarcaAudit.Add(Audit);
                    db.Entry(Audit).State = EntityState.Added;
                    db.SaveChanges();
                    if ((from q in db.MarcaAudit where q.MRA_ID_AUDIT == Audit.MRA_ID_AUDIT && q.MRA_NUM_MARCA == Audit.MRA_NUM_MARCA select q).Count() != 0)
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
            db.Marca.Remove((from q in db.Marca where q.MRA_NUM_MARCA == Marca.MRA_NUM_MARCA select q).FirstOrDefault());
            db.SaveChanges();
            if((from q in db.Marca where q.MRA_NUM_MARCA == Marca.MRA_NUM_MARCA select q).Count() != 0)
            {
                return false;
            }
            return true;
        }
        public bool Delete()
        {
            try
            {
                if(db == null)
                {
                    using(DBKuupEntities db = new DBKuupEntities())
                    {
                        return ToDelete(db);
                    }
                }
                else
                {
                    return ToDelete(db);
                }
            }
            catch(Exception e)
            {
                ClsBitacora.GeneraBitacora(NumeroDePantallaKuup, 1, "Delete", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;
            }
        }
        private bool ToUpdate(DBKuupEntities db)
        {
            Marca Marca = this.ToTable();
            db.Marca.Attach(Marca);
            db.Entry(Marca).State = EntityState.Modified;
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
        public Marca ToTable()
        {
            Marca Tabla = new Marca()
            {
                MRA_NUM_MARCA = this.NumeroDeMarca,
                MRA_NOM_MARCA = this.NombreDeMarca,
                MRA_CVE_ESTATUS = this.CveDeEstatus
            };
            return Tabla;
        }
        public static List<ClsMarcas> getList(String filtro = "", bool EsVista = true,List<short> listaMarcas = null)
        {
            try
            {
                using(DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        var Query = (from q in db.ViMarca
                                     select new ClsMarcas()
                                     {
                                         NumeroDeMarca = q.MRA_NUM_MARCA,
                                         NombreDeMarca = q.MRA_NOM_MARCA,
                                         CveDeEstatus = q.MRA_CVE_ESTATUS,
                                         TextoDeEstatus = q.MRA_TXT_ESTATUS
                                     }).AsQueryable();
                        if (!String.IsNullOrEmpty(filtro))
                        {
                            if(listaMarcas == null)
                            {
                                Query = Query.Where(filtro);
                            }
                            else
                            {
                                Query = Query.Where("NumeroDeMarca.Contains(@0)", listaMarcas);
                            }
                        }
                        return Query.ToList();
                    }
                    else
                    {
                        var Query = (from q in db.ViMarca
                                     select new ClsMarcas()
                                     {
                                         NumeroDeMarca = q.MRA_NUM_MARCA,
                                         NombreDeMarca = q.MRA_NOM_MARCA,
                                         CveDeEstatus = q.MRA_CVE_ESTATUS
                                     }).AsQueryable();
                        if (!String.IsNullOrEmpty(filtro))
                        {
                            if (listaMarcas == null)
                            {
                                Query = Query.Where(filtro);
                            }
                            else
                            {
                                Query = Query.Where("NumeroDeMarca.Contains(@0)", listaMarcas);
                            }
                        }
                        return Query.ToList();
                    }
                }
            }
            catch(Exception e)
            {

            }
            return new List<ClsMarcas>();
        }
        public Object DataTableMarca(Globales.ClsDataTables RequesDT)
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
                var Query = (from q in db.ViMarca
                             select new ClsMarcas()
                             {
                                 NumeroDeMarca = q.MRA_NUM_MARCA,
                                 NombreDeMarca = q.MRA_NOM_MARCA,
                                 TextoDeEstatus = q.MRA_TXT_ESTATUS
                             }).AsQueryable();
                String sql = String.Empty;
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[0][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("NumeroDeMarca = {0}", RequesDT.Form.GetValues("columns[0][search][value]").FirstOrDefault().Trim().ToUpper());
                }
                if (!string.IsNullOrEmpty(RequesDT.Form.GetValues("columns[1][search][value]").FirstOrDefault()))
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        sql += " && ";
                    }
                    sql += String.Format("NombreDeMarca.Trim().ToUpper().Contains(\"{0}\")", RequesDT.Form.GetValues("columns[1][search][value]").FirstOrDefault().Trim().ToUpper());
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
