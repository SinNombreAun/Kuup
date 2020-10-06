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
    public class ClsProveedores : Interfaces.InterfazGen<ClsProveedores>
    {
        ViProveedor Proveedor = new ViProveedor();
        public byte NumeroDeProveedor
        {
            get { return Proveedor.PRV_NUM_PROVEEDOR; }
            set { Proveedor.PRV_NUM_PROVEEDOR = value; }
        }
        public String NombreDeProveedor
        {
            get { return Proveedor.PRV_NOM_PROVEEDOR; }
            set { Proveedor.PRV_NOM_PROVEEDOR = value; }
        }
        public DateTime FechaDeRegistro
        {
            get { return Proveedor.PRV_FECHA_REGISTRO; }
            set { Proveedor.PRV_FECHA_REGISTRO = value; }
        }
        public byte CveSurtidoPorCorreo
        {
            get { return Proveedor.PRV_CVE_SURTIDO_POR_CORREO; }
            set { Proveedor.PRV_CVE_SURTIDO_POR_CORREO = value; }
        }
        public String Correo
        {
            get { return Proveedor.PRV_CORREO; }
            set { Proveedor.PRV_CORREO = value; }
        }
        public String Asunto
        {
            get { return Proveedor.PRV_ASUNTO; }
            set { Proveedor.PRV_ASUNTO = value; }
        }
        public String Mensaje
        {
            get { return Proveedor.PRV_MENSAJE; }
            set { Proveedor.PRV_MENSAJE = value; }
        }
        public byte CveEstatus
        {
            get { return Proveedor.PRV_CVE_ESTATUS; }
            set { Proveedor.PRV_CVE_ESTATUS = value; }
        }
        public String TextoSurtidoPorCorreo
        {
            get { return Proveedor.PRV_TXT_SURTIDO_POR_CORREO; }
            set { Proveedor.PRV_TXT_SURTIDO_POR_CORREO = value; }
        }
        public String TextoEstatus
        {
            get { return Proveedor.PRV_TXT_ESTATUS; }
            set { Proveedor.PRV_TXT_ESTATUS = value; }
        }
        public bool Insert()
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    Proveedor Proveedor = this.ToTable();
                    db.Proveedor.Add(Proveedor);
                    db.SaveChanges();
                    if ((from q in db.Proveedor where q.PRV_NUM_PROVEEDOR == Proveedor.PRV_NUM_PROVEEDOR select q).Count() != 0)
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
        public bool Delete()
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    db.Proveedor.Remove((from q in db.Proveedor where q.PRV_NUM_PROVEEDOR == Proveedor.PRV_NUM_PROVEEDOR select q).FirstOrDefault());
                    db.SaveChanges();
                    if ((from q in db.Proveedor where q.PRV_NUM_PROVEEDOR == Proveedor.PRV_NUM_PROVEEDOR select q).Count() != 0)
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
        public bool Update()
        {
            throw new NotImplementedException();
        }
        public Proveedor ToTable()
        {
            Proveedor Tabla = new Proveedor();
            Tabla.PRV_NUM_PROVEEDOR = this.NumeroDeProveedor;
            Tabla.PRV_NOM_PROVEEDOR = this.NombreDeProveedor;
            Tabla.PRV_FECHA_REGISTRO = this.FechaDeRegistro;
            Tabla.PRV_CVE_SURTIDO_POR_CORREO = this.CveSurtidoPorCorreo;
            Tabla.PRV_CORREO = this.Correo;
            Tabla.PRV_ASUNTO = this.Asunto;
            Tabla.PRV_MENSAJE = this.Mensaje;
            Tabla.PRV_CVE_ESTATUS = this.CveEstatus;
            return Tabla;
        }
        public static List<ClsProveedores> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViProveedor
                                select new ClsProveedores()
                                {
                                    NumeroDeProveedor = q.PRV_NUM_PROVEEDOR,
                                    NombreDeProveedor = q.PRV_NOM_PROVEEDOR,
                                    FechaDeRegistro = q.PRV_FECHA_REGISTRO,
                                    CveSurtidoPorCorreo = q.PRV_CVE_SURTIDO_POR_CORREO,
                                    Correo = q.PRV_CORREO,
                                    Asunto = q.PRV_ASUNTO,
                                    Mensaje = q.PRV_MENSAJE,
                                    CveEstatus = q.PRV_CVE_ESTATUS,
                                    TextoSurtidoPorCorreo = q.PRV_TXT_SURTIDO_POR_CORREO,
                                    TextoEstatus = q.PRV_TXT_ESTATUS
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.Proveedor
                                select new ClsProveedores()
                                {
                                    NumeroDeProveedor = q.PRV_NUM_PROVEEDOR,
                                    NombreDeProveedor = q.PRV_NOM_PROVEEDOR,
                                    FechaDeRegistro = q.PRV_FECHA_REGISTRO,
                                    CveSurtidoPorCorreo = q.PRV_CVE_SURTIDO_POR_CORREO,
                                    Correo = q.PRV_CORREO,
                                    Asunto = q.PRV_ASUNTO,
                                    Mensaje = q.PRV_MENSAJE,
                                    CveEstatus = q.PRV_CVE_ESTATUS
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsProveedores>();
        }
    }
}
