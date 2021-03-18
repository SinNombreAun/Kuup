using Mod.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;

namespace Negocio.Kuup.Clases
{
    public class ClsVentas : Interfaces.InterfazGen<ClsVentas>
    {
        public DBKuupEntities db  { get; set; }
        public short NumeroDePantallaKuup
        {
            get { return 22; }
        }
        ViVenta Venta = new ViVenta();
        public short FolioDeOperacion
        {
            get { return Venta.VEN_FOLIO_OPERACION; }
            set { Venta.VEN_FOLIO_OPERACION = value; }
        }
        public short NumeroDeProducto
        {
            get { return Venta.VEN_NUM_PRODUCTO; }
            set { Venta.VEN_NUM_PRODUCTO = value; }
        }
        public String CodigoDeBarras
        {
            get { return Venta.VEN_CODIGO_BARRAS; }
            set { Venta.VEN_CODIGO_BARRAS = value; }
        }
        public short CantidadDeProducto
        {
            get { return Venta.VEN_CANT_PRODUCTO; }
            set { Venta.VEN_CANT_PRODUCTO = value; }
        }
        public decimal PrecioUnitario
        {
            get { return Venta.VEN_PRECIO_UNITARIO; }
            set { Venta.VEN_PRECIO_UNITARIO = value; }
        }
        public decimal ImporteDeProducto
        {
            get { return Venta.VEN_IMPORTE_PRODUCTO; }
            set { Venta.VEN_IMPORTE_PRODUCTO = value; }
        }
        public String NombreDeProducto
        {
            get { return Venta.VEN_NOM_PRODUCTO; }
            set { Venta.VEN_NOM_PRODUCTO = value; }
        }
        public ClsVentas() { }
        private bool ToInsert(DBKuupEntities db)
        {
            Venta Venta = this.ToTable();
            db.Venta.Add(Venta);
            db.Entry(Venta).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.Venta where q.VEN_FOLIO_OPERACION == Venta.VEN_FOLIO_OPERACION && q.VEN_NUM_PRODUCTO == Venta.VEN_NUM_PRODUCTO && q.VEN_CODIGO_BARRAS == Venta.VEN_CODIGO_BARRAS select q).Count() != 0)
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
        private bool ToDelete(DBKuupEntities db)
        {
            db.Venta.Remove((from q in db.Venta where q.VEN_FOLIO_OPERACION == Venta.VEN_FOLIO_OPERACION && q.VEN_NUM_PRODUCTO == Venta.VEN_NUM_PRODUCTO && q.VEN_CODIGO_BARRAS == Venta.VEN_CODIGO_BARRAS select q).FirstOrDefault());
            db.SaveChanges();
            if ((from q in db.Venta where q.VEN_FOLIO_OPERACION == Venta.VEN_FOLIO_OPERACION && q.VEN_NUM_PRODUCTO == Venta.VEN_NUM_PRODUCTO && q.VEN_CODIGO_BARRAS == Venta.VEN_CODIGO_BARRAS select q).Count() != 0)
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
            Venta Venta = this.ToTable();
            db.Venta.Attach(Venta);
            db.Entry(Venta).State = EntityState.Modified;
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
                ClsBitacora.GeneraBitacora(NumeroDePantallaKuup, 1, "Delete", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;
            }
        }
        public Venta ToTable()
        {
            Venta Tabla = new Venta();
            Tabla.VEN_FOLIO_OPERACION = this.FolioDeOperacion;
            Tabla.VEN_NUM_PRODUCTO = this.NumeroDeProducto;
            Tabla.VEN_CODIGO_BARRAS = this.CodigoDeBarras;
            Tabla.VEN_CANT_PRODUCTO = this.CantidadDeProducto;
            Tabla.VEN_PRECIO_UNITARIO = this.PrecioUnitario;
            Tabla.VEN_IMPORTE_PRODUCTO = this.ImporteDeProducto;
            return Tabla;
        }
        public static List<ClsVentas> getList(String filtro = "", bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        var Query = (from q in db.ViVenta
                                select new ClsVentas()
                                {
                                    FolioDeOperacion = q.VEN_FOLIO_OPERACION,
                                    NumeroDeProducto = q.VEN_NUM_PRODUCTO,
                                    CodigoDeBarras = q.VEN_CODIGO_BARRAS,
                                    CantidadDeProducto = q.VEN_CANT_PRODUCTO,
                                    PrecioUnitario = q.VEN_PRECIO_UNITARIO,
                                    ImporteDeProducto = q.VEN_IMPORTE_PRODUCTO,
                                    NombreDeProducto = q.VEN_NOM_PRODUCTO
                                }).AsQueryable();
                        if (!String.IsNullOrEmpty(filtro))
                        {
                            Query = Query.Where(filtro);
                        }
                        return Query.ToList();
                    }
                    else
                    {
                        var Query = (from q in db.Venta
                                select new ClsVentas()
                                {
                                    FolioDeOperacion = q.VEN_FOLIO_OPERACION,
                                    NumeroDeProducto = q.VEN_NUM_PRODUCTO,
                                    CodigoDeBarras = q.VEN_CODIGO_BARRAS,
                                    CantidadDeProducto = q.VEN_CANT_PRODUCTO,
                                    PrecioUnitario = q.VEN_PRECIO_UNITARIO,
                                    ImporteDeProducto = q.VEN_IMPORTE_PRODUCTO,
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
            return new List<ClsVentas>();
        }
        public static List<VentasTotalesDetalle_Result> VentaDetalle(DateTime fechaInicio, DateTime fechaFin, short folioDeOperacion)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    return db.VentasTotalesDetalle(fechaInicio, fechaFin, folioDeOperacion).ToList();
                }
            }
            catch (Exception e)
            {

            }
            return new List<VentasTotalesDetalle_Result>();
        }
    }
}
