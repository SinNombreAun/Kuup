﻿using Mod.Entity;
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
    public class ClsParametros : Interfaces.InterfazGen<ClsParametros>
    {
        ViParametro Parametro = new ViParametro();
        public byte CveTipo
        {
            get { return Parametro.PAR_CVE_TIPO; }
            set { Parametro.PAR_CVE_TIPO = value; }
        }
        public String NombreDeParametro
        {
            get { return Parametro.PAR_NOM_PARAMETRO; }
            set { Parametro.PAR_NOM_PARAMETRO = value; }
        }
        public String ValorDeParametro
        {
            get { return Parametro.PAR_VALOR_PARAMETRO; }
            set { Parametro.PAR_VALOR_PARAMETRO = value; }
        }
        public String Descripcion
        {
            get { return Parametro.PAR_DESCRIPCION; }
            set { Parametro.PAR_DESCRIPCION = value; }
        }
        public String TextoTipo
        {
            get { return Parametro.PAR_TXT_TIPO; }
            set { Parametro.PAR_TXT_TIPO = value; }
        }
        public bool Insert(bool Dependencia = false)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    Parametro Parametro = this.ToTable();
                    db.Parametro.Add(Parametro);
                    if (!Dependencia)
                    {
                        db.SaveChanges();
                    }
                    if ((from q in db.Parametro where q.PAR_NOM_PARAMETRO == Parametro.PAR_NOM_PARAMETRO select q).Count() != 0)
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
        public bool Delete(bool Dependencia = false)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    db.Parametro.Remove((from q in db.Parametro where q.PAR_NOM_PARAMETRO == Parametro.PAR_NOM_PARAMETRO select q).FirstOrDefault());
                    if (!Dependencia)
                    {
                        db.SaveChanges();
                    }
                    if ((from q in db.Parametro where q.PAR_NOM_PARAMETRO == Parametro.PAR_NOM_PARAMETRO select q).Count() != 0)
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
        public bool Update(bool Dependencia = false)
        {
            throw new NotImplementedException();
        }
        public Parametro ToTable()
        {
            Parametro Tabla = new Parametro();
            Tabla.PAR_CVE_TIPO = this.CveTipo;
            Tabla.PAR_NOM_PARAMETRO = this.NombreDeParametro;
            Tabla.PAR_VALOR_PARAMETRO = this.ValorDeParametro;
            Tabla.PAR_DESCRIPCION = this.Descripcion;
            return Tabla;
        }
        public static List<ClsParametros> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViParametro
                                select new ClsParametros()
                                {
                                    CveTipo = q.PAR_CVE_TIPO,
                                    NombreDeParametro = q.PAR_NOM_PARAMETRO,
                                    ValorDeParametro = q.PAR_VALOR_PARAMETRO,
                                    Descripcion = q.PAR_DESCRIPCION,
                                    TextoTipo = q.PAR_TXT_TIPO
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.Parametro
                                select new ClsParametros()
                                {
                                    CveTipo = q.PAR_CVE_TIPO,
                                    NombreDeParametro = q.PAR_NOM_PARAMETRO,
                                    ValorDeParametro = q.PAR_VALOR_PARAMETRO,
                                    Descripcion = q.PAR_DESCRIPCION
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsParametros>();
        }
    }
}
