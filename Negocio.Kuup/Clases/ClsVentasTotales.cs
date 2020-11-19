using Mod.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;

namespace Negocio.Kuup.Clases
{
    public class ClsVentasTotales : Interfaces.InterfazGen<ClsVentasTotales>
    {
        public DBKuupEntities db { get; set; }
        public short NumeroDePantallaKuup
        {
            get { return 1; }
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
        public decimal ImporteEntregado
        {
            get { return VentaTotal.VET_IMPORTE_ENTREGADO; }
            set { VentaTotal.VET_IMPORTE_ENTREGADO = value; }
        }
        public decimal ImporteCambio
        {
            get { return VentaTotal.VET_IMPORTE_CAMBIO; }
            set { VentaTotal.VET_IMPORTE_CAMBIO = value; }
        }
        public byte CveDeEstatus
        {
            get { return VentaTotal.VET_CVE_ESTATUS; }
            set { VentaTotal.VET_CVE_ESTATUS = value; }
        }
        public String NombreDeUsuario
        {
            get { return VentaTotal.VET_NOM_USUARIO; }
            set { VentaTotal.VET_NOM_USUARIO = value; }
        }
        public String TextoDeAplicaDescuento
        {
            get { return VentaTotal.VET_TXT_APLICADESCUENTO; }
            set { VentaTotal.VET_TXT_APLICADESCUENTO = value; }
        }
        public String TextoDeEstatus
        {
            get { return VentaTotal.VET_TXT_ESTATUS; }
            set { VentaTotal.VET_TXT_ESTATUS = value; }
        }
        public ClsVentasTotales() { }
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
            db.VentaTotal.Remove((from q in db.VentaTotal where q.VET_FOLIO_OPERACION == VentaTotal.VET_FOLIO_OPERACION select q).FirstOrDefault());
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
            Tabla.VET_IMPORTE_ENTREGADO = this.ImporteEntregado;
            Tabla.VET_IMPORTE_CAMBIO = this.ImporteCambio;
            Tabla.VET_CVE_ESTATUS = this.CveDeEstatus;
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
                                    ImporteEntregado = q.VET_IMPORTE_ENTREGADO,
                                    ImporteCambio = q.VET_IMPORTE_CAMBIO,
                                    CveDeEstatus = q.VET_CVE_ESTATUS,
                                    NombreDeUsuario = q.VET_NOM_USUARIO,
                                    TextoDeAplicaDescuento = q.VET_TXT_APLICADESCUENTO,
                                    TextoDeEstatus = q.VET_TXT_ESTATUS
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
                                    ImporteEntregado = q.VET_IMPORTE_ENTREGADO,
                                    ImporteCambio = q.VET_IMPORTE_CAMBIO,
                                    CveDeEstatus = q.VET_CVE_ESTATUS
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
