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
        public class ClsFuncionesPerfiles : Interfaces.InterfazGen<ClsFuncionesPerfiles>
    {
        ViFuncionPerfil FupFuncionPerfil = new ViFuncionPerfil();
        public short FupNumeroDePantalla
        {
            get { return FupFuncionPerfil.FUP_NUM_PANTALLA; }
            set { FupFuncionPerfil.FUP_NUM_PANTALLA = value; }
        }
        public byte FupNumeroDeFuncionalidad
        {
            get { return FupFuncionPerfil.FUP_NUM_FUNCIONALIDAD; }
            set { FupFuncionPerfil.FUP_NUM_FUNCIONALIDAD = value; }
        }
        public byte FupNumeroDePerfil
        {
            get { return FupFuncionPerfil.FUP_NUM_PERFIL; }
            set { FupFuncionPerfil.FUP_NUM_PERFIL = value; }
        }
        public byte FupCveEstatus
        {
            get { return FupFuncionPerfil.FUP_CVE_ESTATUS; }
            set { FupFuncionPerfil.FUP_CVE_ESTATUS = value; }
        }
        public short FupNombreDePantalla
        {
            get { return FupFuncionPerfil.FUP_NOM_PANTALLA; }
            set { FupFuncionPerfil.FUP_NOM_PANTALLA = value; }
        }
        public string FupNombreDeFuncionalidad
        {
            get { return FupFuncionPerfil.FUP_NOM_FUNCIONALIDAD; }
            set { FupFuncionPerfil.FUP_NOM_FUNCIONALIDAD = value; }
        }
        public string FupNombreDePerfil
        {
            get { return FupFuncionPerfil.FUP_NOM_PERFIL; }
            set { FupFuncionPerfil.FUP_NOM_PERFIL = value; }
        }
        public string FupTxtEstatus
        {
            get { return FupFuncionPerfil.FUP_TXT_ESTATUS; }
            set { FupFuncionPerfil.FUP_TXT_ESTATUS = value; }
        }
        public bool Insert()
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    FuncionPerfil FupFuncionPerfil = this.ToTable();
                    db.FupFuncionPerfil.Add(FupFuncionPerfil);
                    db.SaveChanges();
                    if ((from q in db.FuncionPerfil where q.FUP_NUM_PANTALLA == FupFuncionPerfil.FUP_NUM_PANTALLA && q.FUP_NUM_FUNCIONALIDAD == FupFuncionPerfil.FUP_NUM_FUNCIONALIDAD && q.FUP_NUM_PERFIL == FupFuncionPerfil.FUP_NUM_PERFIL select q).Count() != 0)
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
                    db.FupFuncionPerfil.Remove((from q in db.FuncionPerfil where q.FUP_NUM_PANTALLA == FupFuncionPerfil.FUP_NUM_PANTALLA && q.FUP_NUM_FUNCIONALIDAD == FupFuncionPerfil.FUP_NUM_FUNCIONALIDAD && q.FUP_NUM_PERFIL == FupFuncionPerfil.FUP_NUM_PERFIL select q).FirstOrDefault());
                    db.SaveChanges();
                    if ((from q in db.FuncionPerfil where q.FUP_NUM_PANTALLA == FupFuncionPerfil.FUP_NUM_PANTALLA && q.FUP_NUM_FUNCIONALIDAD == FupFuncionPerfil.FUP_NUM_FUNCIONALIDAD && q.FUP_NUM_PERFIL == FupFuncionPerfil.FUP_NUM_PERFIL select q).Count() != 0)
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
        public FuncionPerfil ToTable()
        {
            FuncionPerfil Tabla = new FuncionPerfil();
            Tabla.FUP_NUM_PANTALLA = this.FupNumeroDePantalla;
            Tabla.FUP_NUM_FUNCIONALIDAD = this.FupNumeroDeFuncionalidad;
            Tabla.FUP_NUM_PERFIL = this.FupNumeroDePerfil;
            Tabla.FUP_CVE_ESTATUS = this.FupCveEstatus;
            return Tabla;
        }
        public static List<ClsFuncionesPerfiles> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViFuncionPerfil
                                select new ClsFuncionesPerfiles()
                                {
                                    FupNumeroDePantalla = q.FUP_NUM_PANTALLA,
                                    FupNumeroDeFuncionalidad = q.FUP_NUM_FUNCIONALIDAD,
                                    FupNumeroDePerfil = q.FUP_NUM_PERFIL,
                                    FupCveEstatus = q.FUP_CVE_ESTATUS,
                                    FupNombreDePantalla = q.FUP_NOM_PANTALLA,
                                    FupNombreDeFuncionalidad = q.FUP_NOM_FUNCIONALIDAD,
                                    FupNombreDePerfil = q.FUP_NOM_PERFIL,
                                    FupTxtEstatus = q.FUP_TXT_ESTATUS,
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.FupFuncionPerfil
                                select new ClsFuncionesPerfiles()
                                {
                                    FupNumeroDePantalla = q.FUP_NUM_PANTALLA,
                                    FupNumeroDeFuncionalidad = q.FUP_NUM_FUNCIONALIDAD,
                                    FupNombreDeFuncionalidad = q.FUP_NUM_PERFIL,
                                    FupCveEstatus = q.FUP_CVE_ESTATUS,
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsFuncionesPerfiles>();
        }
    }
}
