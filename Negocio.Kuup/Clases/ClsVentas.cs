﻿using Mod.Entity;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations.Builders;
using System.IO.Pipes;
using System.Linq;
using System.Security.Permissions;

namespace Negocio.Kuup.Clases
{
    public class ClsVentas : Interfaces.InterfazGen<ClsVentas>
    {
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
        public bool Insert(bool Dependencia = false)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    Venta Venta = this.ToTable();
                    db.Venta.Add(Venta);
                    db.Entry(Venta).State = EntityState.Added;
                    if (!Dependencia)
                    {
                        db.SaveChanges();
                    }
                    if ((from q in db.Venta where q.VEN_FOLIO_OPERACION == Venta.VEN_FOLIO_OPERACION && q.VEN_NUM_PRODUCTO == Venta.VEN_NUM_PRODUCTO && q.VEN_CODIGO_BARRAS == Venta.VEN_CODIGO_BARRAS select q).Count() != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                ClsBitacora.GeneraBitacora(1, 1, "Insert", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;
            }
        }
        public bool Delete(bool Dependencia = false)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    db.Venta.Remove((from q in db.Venta where q.VEN_FOLIO_OPERACION == Venta.VEN_FOLIO_OPERACION && q.VEN_NUM_PRODUCTO == Venta.VEN_NUM_PRODUCTO && q.VEN_CODIGO_BARRAS == Venta.VEN_CODIGO_BARRAS select q).FirstOrDefault());
                    db.Entry(Venta).State = EntityState.Deleted;
                    if (!Dependencia)
                    {
                        db.SaveChanges();
                    }
                    if ((from q in db.Venta where q.VEN_FOLIO_OPERACION == Venta.VEN_FOLIO_OPERACION && q.VEN_NUM_PRODUCTO == Venta.VEN_NUM_PRODUCTO && q.VEN_CODIGO_BARRAS == Venta.VEN_CODIGO_BARRAS select q).Count() != 0)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                ClsBitacora.GeneraBitacora(1, 1, "Delete", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;
            }
        }
        public bool Update(bool Dependencia = false)
        {
            throw new NotImplementedException();
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
        public static List<ClsVentas> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViVenta
                                select new ClsVentas()
                                {
                                    FolioDeOperacion = q.VEN_FOLIO_OPERACION,
                                    NumeroDeProducto = q.VEN_NUM_PRODUCTO,
                                    CodigoDeBarras = q.VEN_CODIGO_BARRAS,
                                    CantidadDeProducto = q.VEN_CANT_PRODUCTO,
                                    PrecioUnitario = q.VEN_PRECIO_UNITARIO,
                                    ImporteDeProducto = q.VEN_IMPORTE_PRODUCTO,
                                    NombreDeProducto = q.VEN_NOM_PRODUCTO
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.Venta
                                select new ClsVentas()
                                {
                                    FolioDeOperacion = q.VEN_FOLIO_OPERACION,
                                    NumeroDeProducto = q.VEN_NUM_PRODUCTO,
                                    CodigoDeBarras = q.VEN_CODIGO_BARRAS,
                                    CantidadDeProducto = q.VEN_CANT_PRODUCTO,
                                    PrecioUnitario = q.VEN_PRECIO_UNITARIO,
                                    ImporteDeProducto = q.VEN_IMPORTE_PRODUCTO,
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsVentas>();
        }
    }
}