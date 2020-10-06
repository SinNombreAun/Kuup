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
        ViFuncionalidad Funcionalidadd = new ViFuncionalidad();
        public short NumeroDePantalla
        {
            get { return Funcionalidadd.FUN_NUM_PANTALLA; }
            set { Funcionalidadd.FUN_NUM_PANTALLA = value; }
        }
        public byte NumeroDeFuncionalidad
        {
            get { return Funcionalidadd.FUN_NUM_FUNCIONALIDAD; }
            set { Funcionalidadd.FUN_NUM_FUNCIONALIDAD = value; }
        }
        public string NombreDeFuncionalidad
        {
            get { return Funcionalidadd.FUN_NOM_FUNCIONALIDAD; }
            set { Funcionalidadd.FUN_NOM_FUNCIONALIDAD = value; }
        }
        public byte CveEstatus
        {
            get { return Funcionalidadd.FUN_CVE_ESTATUS; }
            set { Funcionalidadd.FUN_CVE_ESTATUS = value; }
        }
        public string TextoEstatis
        {
            get { return Funcionalidadd.FUN_TXT_ESTATIS; }
            set { Funcionalidadd.FUN_TXT_ESTATIS = value; }
        }
        public bool Insert()
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    Funcionalidad Funcionalidadd = this.ToTable();
                    db.Funcionalidadd.Add(Funcionalidadd);
                    db.SaveChanges();
                    if ((from q in db.Funcionalidad where q.FUN_NUM_PANTALLA == Funcionalidadd.FUN_NUM_PANTALLA && q.FUN_NUM_FUNCIONALIDAD == Funcionalidadd.FUN_NUM_FUNCIONALIDAD select q).Count() != 0)
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
                    db.Funcionalidadd.Add(Funcionalidadd);
                    db.SaveChanges();
                    if ((from q in db.Funcionalidad where q.FUN_NUM_PANTALLA == Funcionalidadd.FUN_NUM_PANTALLA && q.FUN_NUM_FUNCIONALIDAD == Funcionalidadd.FUN_NUM_FUNCIONALIDAD && q.FUN_NUM_PERFIL == Funcionalidadd.FUN_NUM_PERFIL select q).Count() != 0)
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
                                    TextoEstatis = q.FUN_TXT_ESTATIS
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
