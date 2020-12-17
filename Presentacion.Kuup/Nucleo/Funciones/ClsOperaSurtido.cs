using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Funciones.Kuup.Adicionales;
using Funciones.Kuup.CodigoDeBarras;
using Mod.Entity;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Nucleo.Motores;

namespace Presentacion.Kuup.Nucleo.Funciones
{
    public class ClsOperaSurtido
    {
        public bool ManejaProveedor
        {
            get { return validaProveedor(); }
        }
        private bool validaProveedor()
        {
            using (DBKuupEntities db = new DBKuupEntities())
            {
                return (from q in db.Parametro where q.PAR_NOM_PARAMETRO == "ManejaProveedor" select q.PAR_VALOR_PARAMETRO).FirstOrDefault() == "SI";
            }
        }
        public ClsAdicional.ClsResultado InsertSurtidos(String JsonSurtido)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            if (!String.IsNullOrEmpty(JsonSurtido))
            {
                List<ClsSurtidos> Surtidos = ClsAdicional.Deserializar<List<ClsSurtidos>>(JsonSurtido);
                if(Surtidos == null)
                {
                    Surtidos = new List<ClsSurtidos>();
                }
                if(Surtidos.Count() != 0)
                {
                    using (DBKuupEntities db = new DBKuupEntities())
                    {
                        using(var Transaccion = db.Database.BeginTransaction())
                        {
                            try
                            {
                                ClsSequence Sequence = new ClsSequence(db.Database);
                                var Productos = ClsProductos.getList().Where(x => Surtidos.Exists(y => y.NumeroDeProducto == x.NumeroDeProducto)).ToList();
                                var ProductosAudit = (from q in db.ProductoAudit select q.PRO_NUM_PRODUCTO).Distinct().ToList();
                                short FolioDeSurtido = Sequence.SQ_FolioSurtido();
                                foreach (var Surtido in Surtidos)
                                {
                                    Surtido.db = db;
                                    Surtido.FolioDeSurtido = FolioDeSurtido;
                                    Surtido.FechaDeSurtido = DateTime.Now;
                                    if (!ManejaProveedor)
                                    {
                                         Surtido.NumeroDeUsuario = MoSesion.NumeroDeUsuario;
                                    }
                                    Surtido.CveDeEstatus = (byte)ClsEnumerables.CveDeEstatusGeneral.ACTIVO;
                                    if (!Surtido.Insert())
                                    {
                                        Resultado.Resultado = false;
                                        break;
                                    }
                                    else
                                    {
                                        if(Productos.Exists(x => x.NumeroDeProducto == Surtido.NumeroDeProducto && x.CodigoDeBarras == Surtido.CodigoDeBarras))
                                        {
                                            var Producto = Productos.Find(x => x.NumeroDeProducto == Surtido.NumeroDeProducto && x.CodigoDeBarras == Surtido.CodigoDeBarras);
                                            if (!ProductosAudit.Exists(x => x == Producto.NumeroDeProducto))
                                            {
                                                ClsAudit Audit = Clases.ClsAuditInsert.RegistraAudit(Sequence.SQ_FolioAudit(), "ALTA");
                                                Producto.InsertAudit(Audit);
                                            }
                                            Producto.CantidadDeProductoUltima = Producto.CantidadDeProductoTotal;
                                            Producto.CantidadDeProductoNueva = Surtido.CantidadNueva;
                                            Producto.CantidadDeProductoTotal = (short)(Surtido.CantidadPrevia + Surtido.CantidadNueva);
                                            if (!Producto.Update())
                                            {
                                                Resultado.Resultado = false;
                                                break;
                                            }
                                            else
                                            {
                                                ClsAudit AuditUp = Clases.ClsAuditInsert.RegistraAudit(Sequence.SQ_FolioAudit(), "SURTIDO");
                                                Producto.InsertAudit(AuditUp);
                                            }
                                        }
                                    }
                                }
                                if (Resultado.Resultado)
                                {
                                    Transaccion.Commit();
                                    Resultado.Mensaje = "Surtido dado de alta correctamente";
                                }
                                else
                                {
                                    Transaccion.Rollback();
                                    Resultado.Mensaje = "Ocurrio un problema al registrar el surtido";
                                }
                            }
                            catch(Exception e)
                            {
                                Transaccion.Rollback();
                                Resultado.Resultado = false;
                                Resultado.Mensaje = Recursos.Textos.Bitacora_TextoTryCatchGenerico;
                                ClsBitacora.GeneraBitacora(1, 1, "InsertSurtidos", String.Format(Recursos.Textos.Bitacora_TextoDeError, e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                            }
                        }
                    }
                }
                else
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "El objeto de surtido esta vacio";
                }
            }
            return Resultado;
        }
    }
}