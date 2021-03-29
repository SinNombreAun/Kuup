using Funciones.Kuup.Adicionales;
using Mod.Entity;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Models;
using System;

namespace Presentacion.Kuup.Nucleo.Funciones
{
    public class ClsOperaMarca : Models.MarcaModel
    {
        public ClsAdicional.ClsResultado Insert(ref MarcaModel Marca)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            using(DBKuupEntities db = new DBKuupEntities())
            {
                ClsSequence Sequence = new ClsSequence(db.Database);
                Marca.NumeroDeMarca = Sequence.SQ_Marca();
                Marca.NombreDeMarca = Marca.NombreDeMarca.ToUpper().Trim();
                if (!Marca.Existe())
                {
                    if (!Marca.Insert())
                    {
                        Resultado.Resultado = false;
                        Resultado.Mensaje = "Ocurrio un error al dar de alta la Marca";
                    }
                    else
                    {
                        ClsAudit Audit = Nucleo.Clases.ClsAuditInsert.RegistraAudit(Sequence.SQ_FolioAudit(), "ALTA");
                        Marca.InsertAudit(Audit);
                    }
                }
                else
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "La marca a registrar " + Marca.NombreDeMarca + " ya existe en el sistema";
                }
            }
            return Resultado;
        }
        public ClsAdicional.ClsResultado AsignaMarcaATipo(short NumeroDeTipo, short NumeroDeMarca)
        {
            ClsAdicional.ClsResultado resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            ClsAsignaMarcas AsignaMarca = new ClsAsignaMarcas()
            {
                NumeroDeMarca = NumeroDeMarca,
                NumeroDeTipoDeProducto = NumeroDeTipo
            };
            if (!AsignaMarca.Insert())
            {
                resultado.Resultado = false;
                resultado.Mensaje = "No fue posible agregar la Marca al Tipo";
            }
            return resultado;
        }
        public ClsAdicional.ClsResultado RemueveMarcaDeTipo(short NumeroDeTipo, short NumeroDeMarca)
        {
            ClsAdicional.ClsResultado resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            ClsAsignaMarcas AsignaMarca = new ClsAsignaMarcas()
            {
                NumeroDeMarca = NumeroDeMarca,
                NumeroDeTipoDeProducto = NumeroDeTipo
            };
            if (!AsignaMarca.Delete())
            {
                resultado.Resultado = false;
                resultado.Mensaje = "No fue posible remover la Marca del Tipo";
            }
            return resultado;
        }
    }
}