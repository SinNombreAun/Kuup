using Mod.Entity;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations.Builders;
using System.IO.Pipes;
using System.Linq;
using System.Security.Permissions;

namespace Negocio.Kuup.Clases
{
    public class ClsSurtidos : Interfaces.InterfazGen<ClsSurtidos>
    {
        ViSurtido Surtido = new ViSurtido();
        public short FolioDeSurtido
        {
            get { return Surtido.SUR_FOLIO_SURTIDO; }
            set { Surtido.SUR_FOLIO_SURTIDO = value; }
        }
        public byte NumeroDeProveedor
        {
            get { return Surtido.SUR_NUM_PROVEEDOR; }
            set { Surtido.SUR_NUM_PROVEEDOR = value; }
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
        public short CantNueva
        {
            get { return Surtido.SUR_CANT_NUEVA; }
            set { Surtido.SUR_CANT_NUEVA = value; }
        }
        public decimal PrecioUnitario
        {
            get { return Surtido.SUR_PRECIO_UNITARIO; }
            set { Surtido.SUR_PRECIO_UNITARIO = value; }
        }
        public decimal CostoTotal
        {
            get { return Surtido.SUR_COSTO_TOTAL; }
            set { Surtido.SUR_COSTO_TOTAL = value; }
        }
        public System.DateTime FechaDeSurtido
        {
            get { return Surtido.SUR_FECHA_SURTIDO; }
            set { Surtido.SUR_FECHA_SURTIDO = value; }
        }
        public byte CveEstatus
        {
            get { return Surtido.SUR_CVE_ESTATUS; }
            set { Surtido.SUR_CVE_ESTATUS = value; }
        }
        public String NombreDeProveedor
        {
            get { return Surtido.SUR_NOM_PROVEEDOR; }
            set { Surtido.SUR_NOM_PROVEEDOR = value; }
        }
        public String NombreDeProducto
        {
            get { return Surtido.SUR_NOM_PRODUCTO; }
            set { Surtido.SUR_NOM_PRODUCTO = value; }
        }
        public String TxtEstatus
        {
            get { return Surtido.SUR_TXT_ESTATUS; }
            set { Surtido.SUR_TXT_ESTATUS = value; }
        }
        public bool Insert(bool Dependencia = false)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    Surtido Surtido = this.ToTable();
                    db.Surtido.Add(Surtido);
                    if (!Dependencia)
                    {
                        db.SaveChanges();
                    }
                    if ((from q in db.Surtido where q.SUR_FOLIO_SURTIDO == Surtido.SUR_FOLIO_SURTIDO && q.SUR_NUM_PROVEEDOR == Surtido.SUR_NUM_PROVEEDOR && q.SUR_NUM_PRODUCTO == Surtido.SUR_NUM_PRODUCTO select q).Count() != 0)
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
        public bool Delete(bool Dependencia = false)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    db.Surtido.Remove((from q in db.Surtido where q.SUR_FOLIO_SURTIDO == Surtido.SUR_FOLIO_SURTIDO && q.SUR_NUM_PROVEEDOR == Surtido.SUR_NUM_PROVEEDOR && q.SUR_NUM_PRODUCTO == Surtido.SUR_NUM_PRODUCTO select q).FirstOrDefault());
                    if (!Dependencia)
                    {
                        db.SaveChanges();
                    }
                    if ((from q in db.Surtido where q.SUR_FOLIO_SURTIDO == Surtido.SUR_FOLIO_SURTIDO && q.SUR_NUM_PROVEEDOR == Surtido.SUR_NUM_PROVEEDOR && q.SUR_NUM_PRODUCTO == Surtido.SUR_NUM_PRODUCTO select q).Count() != 0)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool Update(bool Dependencia = false)
        {
            throw new NotImplementedException();
        }
        public Surtido ToTable()
        {
            Surtido Tabla = new Surtido();
            Tabla.SUR_FOLIO_SURTIDO = this.FolioDeSurtido;
            Tabla.SUR_NUM_PROVEEDOR = this.NumeroDeProveedor;
            Tabla.SUR_NUM_PRODUCTO = this.NumeroDeProducto;
            Tabla.SUR_CODIGO_BARRAS = this.CodigoDeBarras;
            Tabla.SUR_CANT_NUEVA = this.CantNueva;
            Tabla.SUR_PRECIO_UNITARIO = this.PrecioUnitario;
            Tabla.SUR_COSTO_TOTAL = this.CostoTotal;
            Tabla.SUR_FECHA_SURTIDO = this.FechaDeSurtido;
            Tabla.SUR_CVE_ESTATUS = this.CveEstatus;
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
                                    NumeroDeProveedor = q.SUR_NUM_PROVEEDOR,
                                    NumeroDeProducto = q.SUR_NUM_PRODUCTO,
                                    CodigoDeBarras = q.SUR_CODIGO_BARRAS,
                                    CantNueva = q.SUR_CANT_NUEVA,
                                    PrecioUnitario = q.SUR_PRECIO_UNITARIO,
                                    CostoTotal = q.SUR_COSTO_TOTAL,
                                    FechaDeSurtido = q.SUR_FECHA_SURTIDO,
                                    CveEstatus = q.SUR_CVE_ESTATUS,
                                    NombreDeProveedor = q.SUR_NOM_PROVEEDOR,
                                    NombreDeProducto = q.SUR_NOM_PRODUCTO,
                                    TxtEstatus = q.SUR_TXT_ESTATUS
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.Surtido
                                select new ClsSurtidos()
                                {
                                    FolioDeSurtido = q.SUR_FOLIO_SURTIDO,
                                    NumeroDeProveedor = q.SUR_NUM_PROVEEDOR,
                                    NumeroDeProducto = q.SUR_NUM_PRODUCTO,
                                    CodigoDeBarras = q.SUR_CODIGO_BARRAS,
                                    CantNueva = q.SUR_CANT_NUEVA,
                                    PrecioUnitario = q.SUR_PRECIO_UNITARIO,
                                    CostoTotal = q.SUR_COSTO_TOTAL,
                                    FechaDeSurtido = q.SUR_FECHA_SURTIDO,
                                    CveEstatus = q.SUR_CVE_ESTATUS
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsSurtidos>();
        }
    }
}
