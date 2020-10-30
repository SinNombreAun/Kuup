using Mod.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Negocio.Kuup.Clases
{
    public class ClsAgenda : Interfaces.InterfazGen<ClsAgenda>
    {
        ViAgenda Agenda = new ViAgenda();
        public short NumeroDeAgenda
        {
            get { return Agenda.AGN_NUM_AGENDA; }
            set { Agenda.AGN_NUM_AGENDA = value; }
        }
        public DateTime FechaDeAlta
        {
            get { return Agenda.AGN_FECHA_ALTA; }
            set { Agenda.AGN_FECHA_ALTA = value; }
        }
        public DateTime FechaDeInicioDeEvento
        {
            get { return Agenda.AGN_FECHA_INI_EVENTO; }
            set { Agenda.AGN_FECHA_INI_EVENTO = value; }
        }
        public DateTime FechaDeFinDeEvento
        {
            get { return Agenda.AGN_FECHA_FIN_EVENTO; }
            set { Agenda.AGN_FECHA_FIN_EVENTO = value; }
        }
        public short NumeroDeUsuario
        {
            get { return Agenda.AGN_NUM_USUARIO; }
            set { Agenda.AGN_NUM_USUARIO = value; }
        }
        public byte CveDeNotifica
        {
            get { return Agenda.AGN_CVE_NOTIFICA; }
            set { Agenda.AGN_CVE_NOTIFICA = value; }
        }
        public String Descripcion
        {
            get { return Agenda.AGN_DESCRIPCION; }
            set { Agenda.AGN_DESCRIPCION = value; }
        }
        public byte CveDeEstatus
        {
            get { return Agenda.AGN_CVE_ESTATUS; }
            set { Agenda.AGN_CVE_ESTATUS = value; }
        }
        public String NombreDeUsuario
        {
            get { return Agenda.AGN_NOM_USUARIO; }
            set { Agenda.AGN_NOM_USUARIO = value; }
        }
        public String TextoDeNotifica
        {
            get { return Agenda.AGN_TXT_NOTIFICA; }
            set { Agenda.AGN_TXT_NOTIFICA = value; }
        }
        public String TextoDeEstatus
        {
            get { return Agenda.AGN_TXT_ESTATUS; }
            set { Agenda.AGN_TXT_ESTATUS = value; }
        }
        public ClsAgenda() { }
        public ClsAgenda(Agenda Registro)
        {
            NumeroDeAgenda = Registro.AGN_NUM_AGENDA;
            FechaDeAlta = Registro.AGN_FECHA_ALTA;
            FechaDeInicioDeEvento = Registro.AGN_FECHA_INI_EVENTO;
            FechaDeFinDeEvento = Registro.AGN_FECHA_FIN_EVENTO;
            NumeroDeUsuario = Registro.AGN_NUM_USUARIO;
            CveDeNotifica = Registro.AGN_CVE_NOTIFICA;
            Descripcion = Registro.AGN_DESCRIPCION;
            CveDeEstatus = Registro.AGN_CVE_ESTATUS;
        }
        public ClsAgenda(ViAgenda Registro)
        {
            NumeroDeAgenda = Registro.AGN_NUM_AGENDA;
            FechaDeAlta = Registro.AGN_FECHA_ALTA;
            FechaDeInicioDeEvento = Registro.AGN_FECHA_INI_EVENTO;
            FechaDeFinDeEvento = Registro.AGN_FECHA_FIN_EVENTO;
            NumeroDeUsuario = Registro.AGN_NUM_USUARIO;
            CveDeNotifica = Registro.AGN_CVE_NOTIFICA;
            Descripcion = Registro.AGN_DESCRIPCION;
            CveDeEstatus = Registro.AGN_CVE_ESTATUS;
            NombreDeUsuario = Registro.AGN_NOM_USUARIO;
            TextoDeNotifica = Registro.AGN_TXT_NOTIFICA;
            TextoDeEstatus = Registro.AGN_TXT_ESTATUS;
        }
        public bool Insert(bool Dependencia = false)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    Agenda Agenda = this.ToTable();
                    db.Agenda.Add(Agenda);
                    if (!Dependencia)
                    {
                        db.SaveChanges();
                    }
                    if ((from q in db.Agenda where q.AGN_NUM_AGENDA == Agenda.AGN_NUM_AGENDA select q).Count() != 0)
                    {
                        this.NumeroDeAgenda = Agenda.AGN_NUM_AGENDA;
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
                    db.Agenda.Remove((from q in db.Agenda where q.AGN_NUM_AGENDA == Agenda.AGN_NUM_AGENDA select q).FirstOrDefault());
                    if (!Dependencia)
                    {
                        db.SaveChanges();
                    }
                    if ((from q in db.Agenda where q.AGN_NUM_AGENDA == Agenda.AGN_NUM_AGENDA select q).Count() != 0)
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
        public Agenda ToTable()
        {
            Agenda Tabla = new Agenda();
            Tabla.AGN_NUM_AGENDA = this.NumeroDeAgenda;
            Tabla.AGN_FECHA_ALTA = this.FechaDeAlta;
            Tabla.AGN_FECHA_INI_EVENTO = this.FechaDeInicioDeEvento;
            Tabla.AGN_FECHA_FIN_EVENTO = this.FechaDeFinDeEvento;
            Tabla.AGN_NUM_USUARIO = this.NumeroDeUsuario;
            Tabla.AGN_CVE_NOTIFICA = this.CveDeNotifica;
            Tabla.AGN_DESCRIPCION = this.Descripcion;
            Tabla.AGN_CVE_ESTATUS = this.CveDeEstatus;
            return Tabla;
        }
        public static List<ClsAgenda> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViAgenda
                                select new ClsAgenda()
                                {
                                    NumeroDeAgenda = q.AGN_NUM_AGENDA,
                                    FechaDeAlta = q.AGN_FECHA_ALTA,
                                    FechaDeInicioDeEvento = q.AGN_FECHA_INI_EVENTO,
                                    FechaDeFinDeEvento = q.AGN_FECHA_FIN_EVENTO,
                                    NumeroDeUsuario = q.AGN_NUM_USUARIO,
                                    CveDeNotifica = q.AGN_CVE_NOTIFICA,
                                    Descripcion = q.AGN_DESCRIPCION,
                                    CveDeEstatus = q.AGN_CVE_ESTATUS,
                                    NombreDeUsuario = q.AGN_NOM_USUARIO,
                                    TextoDeNotifica = q.AGN_TXT_NOTIFICA,
                                    TextoDeEstatus = q.AGN_TXT_ESTATUS
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.Agenda
                                select new ClsAgenda()
                                {
                                    NumeroDeAgenda = q.AGN_NUM_AGENDA,
                                    FechaDeAlta = q.AGN_FECHA_ALTA,
                                    FechaDeInicioDeEvento = q.AGN_FECHA_INI_EVENTO,
                                    FechaDeFinDeEvento = q.AGN_FECHA_FIN_EVENTO,
                                    NumeroDeUsuario = q.AGN_NUM_USUARIO,
                                    CveDeNotifica = q.AGN_CVE_NOTIFICA,
                                    Descripcion = q.AGN_DESCRIPCION,
                                    CveDeEstatus = q.AGN_CVE_ESTATUS
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsAgenda>();
        }
    }
}
