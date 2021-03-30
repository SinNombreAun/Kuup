using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mod.Entity;

namespace Negocio.Kuup.Clases
{
    public class ClsIPRegistradas : Interfaces.InterfazGen<ClsIPRegistradas>
    {
        private DBKuupEntities _db = null;
        public DBKuupEntities db
        {
            get { return _db; }
            set { _db = value; }
        }
        public short NumeroDePantallaKuup
        {
            get { return 15; }
        }
        ViIPRegistradas IPRegistradas = new ViIPRegistradas();
        public short NumeroDeUsuario
        {
            get { return IPRegistradas.IPR_NUM_USUARIO; }
            set { IPRegistradas.IPR_NUM_USUARIO = value; }
        }
        public String Terminal
        {
            get { return IPRegistradas.IPR_TERMINAL; }
            set { IPRegistradas.IPR_TERMINAL = value; }
        }
        public String IP
        {
            get { return IPRegistradas.IPR_IP; }
            set { IPRegistradas.IPR_IP = value; }
        }
        public byte CveTipoDeAcceso
        {
            get { return IPRegistradas.IPR_CVE_TIPOACCESO; }
            set { IPRegistradas.IPR_CVE_TIPOACCESO = value; }
        }
        public String NombreDeUsuario
        {
            get { return IPRegistradas.IPR_NOM_USUARIO; }
            set { IPRegistradas.IPR_NOM_USUARIO = value; }
        }
        public String TextoTipoDeAccedo
        {
            get { return IPRegistradas.IPR_TXT_TIPOACCESO; }
            set { IPRegistradas.IPR_TXT_TIPOACCESO = value; }
        }
        public ClsIPRegistradas() { }
        public ClsIPRegistradas(IPRegistradas Registro)
        {
            NumeroDeUsuario = Registro.IPR_NUM_USUARIO;
            Terminal = Registro.IPR_TERMINAL;
            IP = Registro.IPR_IP;
            CveTipoDeAcceso = Registro.IPR_CVE_TIPOACCESO;
        }
        public ClsIPRegistradas(ViIPRegistradas Registro)
        {
            NumeroDeUsuario = Registro.IPR_NUM_USUARIO;
            Terminal = Registro.IPR_TERMINAL;
            IP = Registro.IPR_IP;
            CveTipoDeAcceso = Registro.IPR_CVE_TIPOACCESO;
            NombreDeUsuario = Registro.IPR_NOM_USUARIO;
            TextoTipoDeAccedo = Registro.IPR_TXT_TIPOACCESO;
        }
        private bool ToInsert(DBKuupEntities db)
        {
            IPRegistradas IPRegistradas = this.ToTable();
            db.IPRegistradas.Add(IPRegistradas);
            db.Entry(IPRegistradas).State = EntityState.Added;
            db.SaveChanges();
            if ((from q in db.IPRegistradas where q.IPR_NUM_USUARIO == IPRegistradas.IPR_NUM_USUARIO && q.IPR_TERMINAL == IPRegistradas.IPR_TERMINAL && q.IPR_IP == IPRegistradas.IPR_IP && q.IPR_CVE_TIPOACCESO == IPRegistradas.IPR_CVE_TIPOACCESO select q).Count() != 0)
            {
                return true;
            }
            return false;
        }
        public bool Insert()
        {
            try
            {
                if (_db == null)
                {
                    using (DBKuupEntities db = new DBKuupEntities())
                    {
                        return ToInsert(db);
                    }
                }
                else
                {
                    return ToInsert(_db);
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
            db.IPRegistradas.Remove((from q in db.IPRegistradas where q.IPR_NUM_USUARIO == IPRegistradas.IPR_NUM_USUARIO && q.IPR_TERMINAL == IPRegistradas.IPR_TERMINAL && q.IPR_IP == IPRegistradas.IPR_IP && q.IPR_CVE_TIPOACCESO == IPRegistradas.IPR_CVE_TIPOACCESO select q).FirstOrDefault());
            db.SaveChanges();
            if ((from q in db.IPRegistradas where q.IPR_NUM_USUARIO == IPRegistradas.IPR_NUM_USUARIO && q.IPR_TERMINAL == IPRegistradas.IPR_TERMINAL && q.IPR_IP == IPRegistradas.IPR_IP && q.IPR_CVE_TIPOACCESO == IPRegistradas.IPR_CVE_TIPOACCESO select q).Count() != 0)
            {
                return false;
            }
            return true;
        }
        public bool Delete()
        {
            try
            {
                if (_db == null)
                {
                    using (DBKuupEntities db = new DBKuupEntities())
                    {
                        return ToDelete(db);
                    }
                }
                else
                {
                    return ToDelete(_db);
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
            IPRegistradas IPRegistradas = this.ToTable();
            db.IPRegistradas.Attach(IPRegistradas);
            db.Entry(IPRegistradas).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
        public bool Update()
        {
            try
            {
                if (_db == null)
                {
                    using (DBKuupEntities db = new DBKuupEntities())
                    {
                        return ToUpdate(db);
                    }
                }
                else
                {
                    return ToUpdate(_db);
                }
            }
            catch (Exception e)
            {
                ClsBitacora.GeneraBitacora(NumeroDePantallaKuup, 1, "Update", String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                return false;
            }
        }
        public IPRegistradas ToTable()
        {
            IPRegistradas Tabla = new IPRegistradas();
            Tabla.IPR_NUM_USUARIO = this.NumeroDeUsuario;
            Tabla.IPR_TERMINAL = this.Terminal;
            Tabla.IPR_IP = this.IP;
            Tabla.IPR_CVE_TIPOACCESO = this.CveTipoDeAcceso;
            return Tabla;
        }
        public static List<ClsIPRegistradas> getList(bool EsVista = true)
        {
            try
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    if (EsVista)
                    {
                        return (from q in db.ViIPRegistradas
                                select new ClsIPRegistradas()
                                {
                                    NumeroDeUsuario = q.IPR_NUM_USUARIO,
                                    Terminal = q.IPR_TERMINAL,
                                    IP = q.IPR_IP,
                                    CveTipoDeAcceso = q.IPR_CVE_TIPOACCESO,
                                    NombreDeUsuario = q.IPR_NOM_USUARIO,
                                    TextoTipoDeAccedo = q.IPR_TXT_TIPOACCESO
                                }).ToList();
                    }
                    else
                    {
                        return (from q in db.IPRegistradas
                                select new ClsIPRegistradas()
                                {
                                    NumeroDeUsuario = q.IPR_NUM_USUARIO,
                                    Terminal = q.IPR_TERMINAL,
                                    IP = q.IPR_IP,
                                    CveTipoDeAcceso = q.IPR_CVE_TIPOACCESO
                                }).ToList();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return new List<ClsIPRegistradas>();
        }
    }
}
