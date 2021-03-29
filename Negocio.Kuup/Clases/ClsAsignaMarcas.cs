using Mod.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;

namespace Negocio.Kuup.Clases
{
    public class ClsAsignaMarcas : Interfaces.InterfazGen<ClsAsignaMarcas>
    {
        public DBKuupEntities db { get; set; }
        public short NumeroDePantallaKuup
        {
            get { return 24; }
        }
        ViAsignaMarca AsignaMarca = new ViAsignaMarca();
        public short NumeroDeMarca
        {
            get { return AsignaMarca.AMA_NUM_MARCA; }
            set { AsignaMarca.AMA_NUM_MARCA = value; }
        }
        public short NumeroDeTipoDeProducto
        {
            get { return AsignaMarca.AMA_NUM_TIPO_PRODUCTO; }
            set { AsignaMarca.AMA_NUM_TIPO_PRODUCTO = value; }
        }
        public String NombreDeMarca
        {
            get { return AsignaMarca.AMA_NOM_MARCA; }
            set { AsignaMarca.AMA_NOM_MARCA = value; }
        }
        public String NombreDeTipoDeProducto
        {
            get { return AsignaMarca.AMA_NOM_TIPO_PRODUCTO; }
            set { AsignaMarca.AMA_NOM_TIPO_PRODUCTO = value; }
        }
        public ClsAsignaMarcas() { }
        public ClsAsignaMarcas(AsignaMarca AsignaMarca)
        {
            NumeroDeMarca = AsignaMarca.AMA_NUM_MARCA;
            NumeroDeTipoDeProducto = AsignaMarca.AMA_NUM_TIPO_PRODUCTO;
        }
        public bool Existe()
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if ((from q in db.AsignaMarca where q.AMA_NUM_MARCA == AsignaMarca.AMA_NUM_MARCA && q.AMA_NUM_TIPO_PRODUCTO == AsignaMarca.AMA_NUM_TIPO_PRODUCTO select q).Count() != 0)
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
            AsignaMarca AsignaMarca = this.ToTable();
            db.AsignaMarca.Add(AsignaMarca);
            db.Entry(AsignaMarca).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.AsignaMarca where q.AMA_NUM_MARCA == AsignaMarca.AMA_NUM_MARCA && q.AMA_NUM_TIPO_PRODUCTO == AsignaMarca.AMA_NUM_TIPO_PRODUCTO select q).Count() != 0)
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
        public bool ToDelete(DBKuupEntities db)
        {
            db.AsignaMarca.Remove((from q in db.AsignaMarca where q.AMA_NUM_MARCA == AsignaMarca.AMA_NUM_MARCA && q.AMA_NUM_TIPO_PRODUCTO == AsignaMarca.AMA_NUM_TIPO_PRODUCTO select q).FirstOrDefault());
            db.SaveChanges();
            if ((from q in db.AsignaMarca where q.AMA_NUM_MARCA == AsignaMarca.AMA_NUM_MARCA && q.AMA_NUM_TIPO_PRODUCTO == AsignaMarca.AMA_NUM_TIPO_PRODUCTO select q).Count() != 0)
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
            AsignaMarca AsignaMarca = this.ToTable();
            db.AsignaMarca.Attach(AsignaMarca);
            db.Entry(AsignaMarca).State = EntityState.Modified;
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
        public AsignaMarca ToTable()
        {
            AsignaMarca Tabla = new AsignaMarca()
            {
                AMA_NUM_MARCA = this.NumeroDeMarca,
                AMA_NUM_TIPO_PRODUCTO = this.NumeroDeTipoDeProducto
            };
            return Tabla;
        }
        public static List<ClsAsignaMarcas> getList(String filtro = "", bool EsVista = true, List<short> listaMarcas = null,List<short> listaTipos = null)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        var Query = (from q in db.ViAsignaMarca
                                     select new ClsAsignaMarcas()
                                     {
                                         NumeroDeMarca = q.AMA_NUM_MARCA,
                                         NumeroDeTipoDeProducto = q.AMA_NUM_TIPO_PRODUCTO,
                                         NombreDeMarca = q.AMA_NOM_MARCA,
                                         NombreDeTipoDeProducto = q.AMA_NOM_TIPO_PRODUCTO
                                     }).AsQueryable();
                        if (!String.IsNullOrEmpty(filtro))
                        {
                            if (listaMarcas == null)
                            {
                                if (listaTipos == null)
                                {
                                    Query = Query.Where(filtro);
                                }
                                else
                                {
                                    Query = Query.Where("NumeroDeTipoDeProducto.Contains(@1)", listaMarcas);
                                }
                            }
                            else
                            {
                                if (listaTipos != null)
                                {
                                    Query = Query.Where("NumeroDeTipoDeProducto.Contains(@0) && NumeroDeMarca.Contains(@1)", listaTipos, listaMarcas);
                                }
                                else
                                {
                                    Query = Query.Where("NumeroDeMarca.Contains(@0)", listaMarcas);
                                }
                            }
                        }
                        return Query.ToList();
                    }
                    else
                    {
                        var Query = (from q in db.AsignaMarca
                                     select new ClsAsignaMarcas()
                                     {
                                         NumeroDeMarca = q.AMA_NUM_MARCA,
                                         NumeroDeTipoDeProducto = q.AMA_NUM_TIPO_PRODUCTO
                                     }).AsQueryable();
                        if (!String.IsNullOrEmpty(filtro))
                        {
                            if (listaMarcas == null)
                            {
                                if (listaTipos == null)
                                {
                                    Query = Query.Where(filtro);
                                }
                                else
                                {
                                    Query = Query.Where("NumeroDeTipoDeProducto.Contains(@1)", listaMarcas);
                                }
                            }
                            else
                            {
                                if (listaTipos != null)
                                {
                                    Query = Query.Where("NumeroDeTipoDeProducto.Contains(@0) && NumeroDeMarca.Contains(@1)", listaTipos, listaMarcas);
                                }
                                else
                                {
                                    Query = Query.Where("NumeroDeMarca.Contains(@0)", listaMarcas);
                                }
                            }
                        }
                        return Query.ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsAsignaMarcas>();
        }
        public static List<NoAsignadosPorOrigen_Result> NoAsignadosPorOrigen(short Registro, String Origen)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    return db.NoAsignadosPorOrigen(Registro, Origen).ToList();
                }
            }
            catch (Exception e)
            {

            }
            return new List<NoAsignadosPorOrigen_Result>();
        }
    }
}
