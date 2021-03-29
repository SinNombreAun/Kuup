using Mod.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Negocio.Kuup.Clases
{
    public class ClsAgenda : Interfaces.InterfazGen<ClsAgenda>
    {
        public DBKuupEntities db { get; set; }
        public short NumeroDePantallaKuup
        {
            get { return 11; }
        }
        readonly ViAgenda Agenda = new ViAgenda();
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
        private bool ToInsert(DBKuupEntities db)
        {
            Agenda Agenda = this.ToTable();
            db.Agenda.Add(Agenda);
            db.Entry(Agenda).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.Agenda where q.AGN_NUM_AGENDA == Agenda.AGN_NUM_AGENDA select q).Count() != 0)
            {
                this.NumeroDeAgenda = Agenda.AGN_NUM_AGENDA;
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
            db.Agenda.Remove((from q in db.Agenda where q.AGN_NUM_AGENDA == Agenda.AGN_NUM_AGENDA select q).FirstOrDefault());
            db.SaveChanges();
            if ((from q in db.Agenda where q.AGN_NUM_AGENDA == Agenda.AGN_NUM_AGENDA select q).Count() != 0)
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
            Agenda Agenda = this.ToTable();
            db.Agenda.Attach(Agenda);
            db.Entry(Agenda).State = EntityState.Modified;
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
                ClsBitacora.GeneraBitacora(NumeroDePantallaKuup, 1, "Delete", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;

            }
        }
        public Agenda ToTable()
        {
            Agenda Tabla = new Agenda
            {
                AGN_NUM_AGENDA = this.NumeroDeAgenda,
                AGN_FECHA_ALTA = this.FechaDeAlta,
                AGN_FECHA_INI_EVENTO = this.FechaDeInicioDeEvento,
                AGN_FECHA_FIN_EVENTO = this.FechaDeFinDeEvento,
                AGN_NUM_USUARIO = this.NumeroDeUsuario,
                AGN_CVE_NOTIFICA = this.CveDeNotifica,
                AGN_DESCRIPCION = this.Descripcion,
                AGN_CVE_ESTATUS = this.CveDeEstatus
            };
            return Tabla;
        }
        public static List<ClsAgenda> GetList(bool EsVista = true)
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
                ClsBitacora.GeneraBitacora((new ClsAgenda()).NumeroDePantallaKuup, 2, "GetList", string.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
            }
            return new List<ClsAgenda>();
        }
    }
}
