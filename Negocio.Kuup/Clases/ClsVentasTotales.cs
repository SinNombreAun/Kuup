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

namespace Negocio.Kuup.Clases
{
    public class ClsVentasTotales : Interfaces.InterfazGen<ClsVentasTotales>
    {
        DBKuupEntities _db = null;
        public DBKuupEntities db
        {
            get { return _db; }
            set { _db = value; }
        }
        ViVentaTotal VentaTotal = new ViVentaTotal();
        public short FolioDeOperacion
        {
            get { return VentaTotal.VET_FOLIO_OPERACION; }
            set { VentaTotal.VET_FOLIO_OPERACION = value; }
        }
        public System.DateTime FechaDeOperacion
        {
            get { return VentaTotal.VET_FECHA_OPERACION; }
            set { VentaTotal.VET_FECHA_OPERACION = value; }
        }
        public short NumeroDeUsuario
        {
            get { return VentaTotal.VET_NUM_USUARIO; }
            set { VentaTotal.VET_NUM_USUARIO = value; }
        }
        public String NombreDeCliente
        {
            get { return VentaTotal.VET_NOM_CLIENTE; }
            set { VentaTotal.VET_NOM_CLIENTE = value; }
        }
        public decimal ImporteBruto
        {
            get { return VentaTotal.VET_IMPORTE_BRUTO; }
            set { VentaTotal.VET_IMPORTE_BRUTO = value; }
        }
        public Nullable<decimal> IVA
        {
            get { return VentaTotal.VET_IVA; }
            set { VentaTotal.VET_IVA = value; }
        }
        public byte CveAplicaDescuento
        {
            get { return VentaTotal.VET_CVE_APLICADESCUENTO; }
            set { VentaTotal.VET_CVE_APLICADESCUENTO = value; }
        }
        public String Porcentaje
        {
            get { return VentaTotal.VET_PORCENTAJE; }
            set { VentaTotal.VET_PORCENTAJE = value; }
        }
        public decimal ImporteNeto
        {
            get { return VentaTotal.VET_IMPORTE_NETO; }
            set { VentaTotal.VET_IMPORTE_NETO = value; }
        }
        public byte CveEstatus
        {
            get { return VentaTotal.VET_CVE_ESTATUS; }
            set { VentaTotal.VET_CVE_ESTATUS = value; }
        }
        public String AplicaDescuento
        {
            get { return VentaTotal.VET_TXT_APLICADESCUENTO; }
            set { VentaTotal.VET_TXT_APLICADESCUENTO = value; }
        }
        public String TxtEstatus
        {
            get { return VentaTotal.VET_TXT_ESTATUS; }
            set { VentaTotal.VET_TXT_ESTATUS = value; }
        }
        private bool ToInsert(DBKuupEntities db)
        {
            VentaTotal VentaTotal = this.ToTable();
            db.VentaTotal.Add(VentaTotal);
            db.Entry(VentaTotal).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.VentaTotal where q.VET_FOLIO_OPERACION == VentaTotal.VET_FOLIO_OPERACION select q).Count() != 0)
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
                ClsBitacora.GeneraBitacora(1, 1, "Insert", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;
            }
        }
        private bool ToDelete(DBKuupEntities db)
        {
            db.VentaTotal.Remove((from q in db.VentaTotal where q.VET_FOLIO_OPERACION == VentaTotal.VET_FOLIO_OPERACION select q).FirstOrDefault());
            db.Entry(VentaTotal).State = EntityState.Deleted;
            db.SaveChanges();
            if ((from q in db.VentaTotal where q.VET_FOLIO_OPERACION == VentaTotal.VET_FOLIO_OPERACION select q).Count() != 0)
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
            VentaTotal VentaTotal = this.ToTable();
            db.VentaTotal.Attach(VentaTotal);
            db.Entry(VentaTotal).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
        public bool Update()
        {
            try
            {
                if(db == null)
                {
                    using(DBKuupEntities db = new DBKuupEntities())
                    {
                        return ToUpdate(db);
                    }
                }
                else
                {
                    return ToUpdate(db);
                }
            }
            catch(Exception e)
            {
                ClsBitacora.GeneraBitacora(1, 1, "Delete", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;

            }
        }
        public VentaTotal ToTable()
        {
            VentaTotal Tabla = new VentaTotal();
            Tabla.VET_FOLIO_OPERACION = this.FolioDeOperacion;
            Tabla.VET_FECHA_OPERACION = this.FechaDeOperacion;
            Tabla.VET_NUM_USUARIO = this.NumeroDeUsuario;
            Tabla.VET_NOM_CLIENTE = this.NombreDeCliente;
            Tabla.VET_IMPORTE_BRUTO = this.ImporteBruto;
            Tabla.VET_IVA = this.IVA;
            Tabla.VET_CVE_APLICADESCUENTO = this.CveAplicaDescuento;
            Tabla.VET_PORCENTAJE = this.Porcentaje;
            Tabla.VET_IMPORTE_NETO = this.ImporteNeto;
            Tabla.VET_CVE_ESTATUS = this.CveEstatus;
            return Tabla;
        }
        public static List<ClsVentasTotales> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViVentaTotal
                                select new ClsVentasTotales()
                                {
                                    FolioDeOperacion = q.VET_FOLIO_OPERACION,
                                    FechaDeOperacion = q.VET_FECHA_OPERACION,
                                    NumeroDeUsuario = q.VET_NUM_USUARIO,
                                    NombreDeCliente = q.VET_NOM_CLIENTE,
                                    ImporteBruto = q.VET_IMPORTE_BRUTO,
                                    IVA = q.VET_IVA,
                                    CveAplicaDescuento = q.VET_CVE_APLICADESCUENTO,
                                    Porcentaje = q.VET_PORCENTAJE,
                                    ImporteNeto = q.VET_IMPORTE_NETO,
                                    CveEstatus = q.VET_CVE_ESTATUS,
                                    AplicaDescuento = q.VET_TXT_APLICADESCUENTO,
                                    TxtEstatus = q.VET_TXT_ESTATUS
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.VentaTotal
                                select new ClsVentasTotales()
                                {
                                    FolioDeOperacion = q.VET_FOLIO_OPERACION,
                                    FechaDeOperacion = q.VET_FECHA_OPERACION,
                                    NumeroDeUsuario = q.VET_NUM_USUARIO,
                                    NombreDeCliente = q.VET_NOM_CLIENTE,
                                    ImporteBruto = q.VET_IMPORTE_BRUTO,
                                    IVA = q.VET_IVA,
                                    CveAplicaDescuento = q.VET_CVE_APLICADESCUENTO,
                                    Porcentaje = q.VET_PORCENTAJE,
                                    ImporteNeto = q.VET_IMPORTE_NETO,
                                    CveEstatus = q.VET_CVE_ESTATUS
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsVentasTotales>();
        }
    }
}
