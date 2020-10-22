using Mod.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Negocio.Kuup.Clases
{
    public class ClsBitacora : Interfaces.InterfazGen<ClsBitacora>
    {
        ViBitacora Bitacora = new ViBitacora();
        public short NumeroDeBitacora
        {
            get { return Bitacora.BIT_NUM_BITACORA; }
            set { Bitacora.BIT_NUM_BITACORA = value; }
        }
        public short NumeroDePantalla
        {
            get { return Bitacora.BIT_NUM_PANTALLA; }
            set { Bitacora.BIT_NUM_PANTALLA = value; }
        }
        public byte NumeroDeFuncionalidad
        {
            get { return Bitacora.BIT_NUM_FUNCIONALIDAD; }
            set { Bitacora.BIT_NUM_FUNCIONALIDAD = value; }
        }
        public String NombreDeFuncionCodigo
        {
            get { return Bitacora.BIT_NOM_FUNCION_COD; }
            set { Bitacora.BIT_NOM_FUNCION_COD = value; }
        }
        public String Detalle
        {
            get { return Bitacora.BIT_DETALLE; }
            set { Bitacora.BIT_DETALLE = value; }
        }
        public DateTime FechaDeBitacora
        {
            get { return Bitacora.BIM_FECHA_BITACORA; }
            set { Bitacora.BIM_FECHA_BITACORA = value; }
        }
        public String NombreDePantalla
        {
            get { return Bitacora.BIT_NOM_PANTALLA; }
            set { Bitacora.BIT_NOM_PANTALLA = value; }
        }
        public String NombreDeFuncionalidad
        {
            get { return Bitacora.BIT_NOM_FUNCIONALIDAD; }
            set { Bitacora.BIT_NOM_FUNCIONALIDAD = value; }
        }
        public bool Insert()
        {
            try
            {
                using(DBKuupEntities db = new DBKuupEntities())
                {
                    Bitacora Bitacora = this.ToTable();
                    db.Bitacora.Add(Bitacora);
                    db.SaveChanges();
                    if ((from q in db.Bitacora where q.BIT_NUM_BITACORA == Bitacora.BIT_NUM_BITACORA && q.BIT_NUM_PANTALLA == Bitacora.BIT_NUM_PANTALLA && q.BIT_NUM_FUNCIONALIDAD == Bitacora.BIT_NUM_FUNCIONALIDAD select q).Count() != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch(Exception e)
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
                    db.Bitacora.Remove((from q in db.Bitacora where q.BIT_NUM_BITACORA == Bitacora.BIT_NUM_BITACORA && q.BIT_NUM_PANTALLA == Bitacora.BIT_NUM_PANTALLA && q.BIT_NUM_FUNCIONALIDAD == Bitacora.BIT_NUM_FUNCIONALIDAD select q).FirstOrDefault());
                    db.SaveChanges();
                    if ((from q in db.Bitacora where q.BIT_NUM_BITACORA == Bitacora.BIT_NUM_BITACORA && q.BIT_NUM_PANTALLA == Bitacora.BIT_NUM_PANTALLA && q.BIT_NUM_FUNCIONALIDAD == Bitacora.BIT_NUM_FUNCIONALIDAD select q).Count() != 0)
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
        public Bitacora ToTable()
        {
            Bitacora Tabla = new Bitacora();
            Tabla.BIT_NUM_BITACORA = this.NumeroDeBitacora;
            Tabla.BIT_NUM_PANTALLA = this.NumeroDePantalla;
            Tabla.BIT_NUM_FUNCIONALIDAD = this.NumeroDeFuncionalidad;
            Tabla.BIT_NOM_FUNCION_COD = this.NombreDeFuncionCodigo;
            Tabla.BIT_DETALLE = this.Detalle;
            Tabla.BIM_FECHA_BITACORA = this.FechaDeBitacora;
            return Tabla;
        }
        public static void GeneraBitacora(short NumeroDePantalla,byte NumeroDeFuncionalidad,String NombreDeFuncionCodigo,String Detalle)
        {
            ClsBitacora Bitacora = new ClsBitacora();
            Bitacora.NumeroDePantalla = NumeroDePantalla;
            Bitacora.NumeroDeFuncionalidad = NumeroDeFuncionalidad;
            Bitacora.NombreDeFuncionCodigo = NombreDeFuncionCodigo;
            Bitacora.Detalle = Detalle;
            Bitacora.FechaDeBitacora = DateTime.Now;
            try
            {
                Bitacora.Insert();
            }catch (Exception e)
            {

            }
        }
    }
}
