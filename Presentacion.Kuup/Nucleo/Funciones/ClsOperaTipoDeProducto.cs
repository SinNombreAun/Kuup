using Funciones.Kuup.Adicionales;
using Mod.Entity;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Models;
using System;

namespace Presentacion.Kuup.Nucleo.Funciones
{
    public class ClsOperaTipoDeProducto : Models.TipoDeProductoModel
    {
        public ClsAdicional.ClsResultado Insert(ref TipoDeProductoModel TipoDeProducto)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            using (DBKuupEntities db = new DBKuupEntities())
            {
                ClsSequence Sequence = new ClsSequence(db.Database);
                TipoDeProducto.NumeroDeTipoDeProducto = Sequence.SQ_TipoProducto();
                TipoDeProducto.NombreDeTipoDeProducto = TipoDeProducto.NombreDeTipoDeProducto.ToUpper().Trim();
                if (!TipoDeProducto.Existe())
                {
                    if (!TipoDeProducto.Insert())
                    {
                        Resultado.Resultado = false;
                        Resultado.Mensaje = "Ocurrio un error al dar de alta el tipo de producto";
                    }
                    else
                    {
                        ClsAudit Audit = Nucleo.Clases.ClsAuditInsert.RegistraAudit(Sequence.SQ_FolioAudit(), "ALTA");
                        TipoDeProducto.InsertAudit(Audit);
                    }
                }
                else
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "El tipo de producto a registrar " + TipoDeProducto.NombreDeTipoDeProducto + " ya existe en el sistema";
                }
            }
            return Resultado;
        }
        public ClsAdicional.ClsResultado AsignaTipoAMarca(short NumeroDeMarca, short NumeroDeTipo)
        {
            ClsAdicional.ClsResultado resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            ClsAsignaMarcas AsignaTipo = new ClsAsignaMarcas()
            {
                NumeroDeMarca = NumeroDeMarca,
                NumeroDeTipoDeProducto = NumeroDeTipo
            };
            if (!AsignaTipo.Insert())
            {
                resultado.Resultado = false;
                resultado.Mensaje = "No fue posible agregar el Tipo a la Marca";
            }
            return resultado;
        }
        public ClsAdicional.ClsResultado RemueveTipoDeMarca(short NumeroDeMarca, short NumeroDeTipo)
        {
            ClsAdicional.ClsResultado resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            ClsAsignaMarcas AsignaTipo = new ClsAsignaMarcas()
            {
                NumeroDeMarca = NumeroDeMarca,
                NumeroDeTipoDeProducto = NumeroDeTipo
            };
            if (!AsignaTipo.Delete())
            {
                resultado.Resultado = false;
                resultado.Mensaje = "No fue posible remover el Tipo de la Marca";
            }
            return resultado;
        }
    }
}