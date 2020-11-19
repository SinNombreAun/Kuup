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
    public class ClsCodigosDeBarras : Interfaces.InterfazGen<ClsCodigosDeBarras>
    {
        public DBKuupEntities db { get; set; }
        public short NumeroDePantallaKuup
        {
            get { return 14; }
        }
        ViCodigoDeBarras CodigoDeBarrasE = new ViCodigoDeBarras();
        public String CodigoDeBarras
        {
            get { return CodigoDeBarrasE.COB_CODIGO_BARRAS; }
            set { CodigoDeBarrasE.COB_CODIGO_BARRAS = value; }
        }
        public short NumeroDeProducto
        {
            get { return CodigoDeBarrasE.COB_NUM_PRODUCTO; }
            set { CodigoDeBarrasE.COB_NUM_PRODUCTO = value; }
        }
        public String RutaDeArchivo
        {
            get { return CodigoDeBarrasE.COB_RUTA_ARCHIVO; }
            set { CodigoDeBarrasE.COB_RUTA_ARCHIVO = value; }
        }
        public DateTime FechaDeGeneracion
        {
            get { return CodigoDeBarrasE.COB_FECHA_GENERACION; }
            set { CodigoDeBarrasE.COB_FECHA_GENERACION = value; }
        }
        public byte CveDeEstatus
        {
            get { return CodigoDeBarrasE.COB_CVE_ESTATUS; }
            set { CodigoDeBarrasE.COB_CVE_ESTATUS = value; }
        }
        public String NombreDeProducto
        {
            get { return CodigoDeBarrasE.COB_NOM_PRODUCTO; }
            set { CodigoDeBarrasE.COB_NOM_PRODUCTO = value; }
        }
        public String TextoDeEstatus
        {
            get { return CodigoDeBarrasE.COB_TXT_ESTATUS; }
            set { CodigoDeBarrasE.COB_TXT_ESTATUS = value; }
        }
        public ClsCodigosDeBarras() { }
        private bool ToInsert(DBKuupEntities db)
        {
            CodigoDeBarras CodigoDeBarrasE = this.ToTable();
            db.CodigoDeBarras.Add(CodigoDeBarrasE);
            db.Entry(CodigoDeBarras).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.CodigoDeBarras where q.COB_NUM_PRODUCTO == CodigoDeBarrasE.COB_NUM_PRODUCTO select q).Count() != 0)
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
            db.CodigoDeBarras.Remove((from q in db.CodigoDeBarras where q.COB_NUM_PRODUCTO == CodigoDeBarrasE.COB_NUM_PRODUCTO select q).FirstOrDefault());
            db.SaveChanges();
            if ((from q in db.CodigoDeBarras where q.COB_NUM_PRODUCTO == CodigoDeBarrasE.COB_NUM_PRODUCTO select q).Count() != 0)
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
            CodigoDeBarras CodigoDeBarras = this.ToTable();
            db.CodigoDeBarras.Attach(CodigoDeBarras);
            db.Entry(CodigoDeBarras).State = EntityState.Modified;
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
        public CodigoDeBarras ToTable()
        {
            CodigoDeBarras Tabla = new CodigoDeBarras();
            Tabla.COB_CODIGO_BARRAS = this.CodigoDeBarras;
            Tabla.COB_NUM_PRODUCTO = this.NumeroDeProducto;
            Tabla.COB_RUTA_ARCHIVO = this.RutaDeArchivo;
            Tabla.COB_FECHA_GENERACION = this.FechaDeGeneracion;
            Tabla.COB_CVE_ESTATUS = this.CveDeEstatus;
            return Tabla;
        }
        public static List<ClsCodigosDeBarras> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViCodigoDeBarras
                                select new ClsCodigosDeBarras()
                                {
                                    CodigoDeBarras = q.COB_CODIGO_BARRAS,
                                    NumeroDeProducto = q.COB_NUM_PRODUCTO,
                                    RutaDeArchivo = q.COB_RUTA_ARCHIVO,
                                    FechaDeGeneracion = q.COB_FECHA_GENERACION,
                                    CveDeEstatus = q.COB_CVE_ESTATUS,
                                    NombreDeProducto = q.COB_NOM_PRODUCTO,
                                    TextoDeEstatus = q.COB_TXT_ESTATUS
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.CodigoDeBarras
                                select new ClsCodigosDeBarras()
                                {
                                    CodigoDeBarras = q.COB_CODIGO_BARRAS,
                                    NumeroDeProducto = q.COB_NUM_PRODUCTO,
                                    RutaDeArchivo = q.COB_RUTA_ARCHIVO,
                                    FechaDeGeneracion = q.COB_FECHA_GENERACION,
                                    CveDeEstatus = q.COB_CVE_ESTATUS
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsCodigosDeBarras>();
        }
    }
}
