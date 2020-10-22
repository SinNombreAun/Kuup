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
        public class ClsFuncionalidades : Interfaces.InterfazGen<ClsFuncionalidades>
    {
        ViFuncionalidad Funcionalidad = new ViFuncionalidad();
        public short NumeroDePantalla
        {
            get { return Funcionalidad.FUN_NUM_PANTALLA; }
            set { Funcionalidad.FUN_NUM_PANTALLA = value; }
        }
        public byte NumeroDeFuncionalidad
        {
            get { return Funcionalidad.FUN_NUM_FUNCIONALIDAD; }
            set { Funcionalidad.FUN_NUM_FUNCIONALIDAD = value; }
        }
        public String NombreDeFuncionalidad
        {
            get { return Funcionalidad.FUN_NOM_FUNCIONALIDAD; }
            set { Funcionalidad.FUN_NOM_FUNCIONALIDAD = value; }
        }
        public byte CveEstatus
        {
            get { return Funcionalidad.FUN_CVE_ESTATUS; }
            set { Funcionalidad.FUN_CVE_ESTATUS = value; }
        }
        public String TextoEstatus
        {
            get { return Funcionalidad.FUN_TXT_ESTATUS; }
            set { Funcionalidad.FUN_TXT_ESTATUS = value; }
        }
        public bool Insert()
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    Funcionalidad Funcionalidad = this.ToTable();
                    db.Funcionalidad.Add(Funcionalidad);
                    db.SaveChanges();
                    if ((from q in db.Funcionalidad where q.FUN_NUM_PANTALLA == Funcionalidad.FUN_NUM_PANTALLA && q.FUN_NUM_FUNCIONALIDAD == Funcionalidad.FUN_NUM_FUNCIONALIDAD select q).Count() != 0)
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
                    db.Funcionalidad.Add((from q in db.Funcionalidad where q.FUN_NUM_PANTALLA == Funcionalidad.FUN_NUM_PANTALLA && q.FUN_NUM_FUNCIONALIDAD == Funcionalidad.FUN_NUM_FUNCIONALIDAD select q).FirstOrDefault());
                    db.SaveChanges();
                    if ((from q in db.Funcionalidad where q.FUN_NUM_PANTALLA == Funcionalidad.FUN_NUM_PANTALLA && q.FUN_NUM_FUNCIONALIDAD == Funcionalidad.FUN_NUM_FUNCIONALIDAD  select q).Count() != 0)
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
        public Funcionalidad ToTable()
        {
            Funcionalidad Tabla = new Funcionalidad();
            Tabla.FUN_NUM_PANTALLA = this.NumeroDePantalla;
            Tabla.FUN_NUM_FUNCIONALIDAD = this.NumeroDeFuncionalidad;
            Tabla.FUN_NOM_FUNCIONALIDAD = this.NombreDeFuncionalidad;
            Tabla.FUN_CVE_ESTATUS = this.CveEstatus;
            return Tabla;
        }
        public static List<ClsFuncionalidades> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViFuncionalidad
                                select new ClsFuncionalidades()
                                {
                                    NumeroDePantalla = q.FUN_NUM_PANTALLA,
                                    NumeroDeFuncionalidad = q.FUN_NUM_FUNCIONALIDAD,
                                    NombreDeFuncionalidad = q.FUN_NOM_FUNCIONALIDAD,
                                    CveEstatus = q.FUN_CVE_ESTATUS,
                                    TextoEstatus = q.FUN_TXT_ESTATUS
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.Funcionalidad
                                select new ClsFuncionalidades()
                                {
                                    NumeroDePantalla = q.FUN_NUM_PANTALLA,
                                    NumeroDeFuncionalidad = q.FUN_NUM_FUNCIONALIDAD,
                                    NombreDeFuncionalidad = q.FUN_NOM_FUNCIONALIDAD,
                                    CveEstatus = q.FUN_CVE_ESTATUS
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsFuncionalidades>();
        }
    }
}
