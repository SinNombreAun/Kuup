using System;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Nucleo.Motores;

namespace Presentacion.Kuup.Nucleo.Clases
{
    public static class ClsAuditInsert
    {
        public static ClsAudit RegistraAudit(short IdAudit, String NombreDeFuncionalidad)
        {
            return new ClsAudit()
            {
                IdAudit = IdAudit,
                Terminal = MoSesion.TerminalDeUsuario,
                IP = MoSesion.IPDeUsuario,
                Version = MvcApplication.ObjVersion.NumeroDeVersionCorta,
                NombreDeUsuario = MoSesion.NombreDeUsuario,
                NombreDeFuncionalidad = NombreDeFuncionalidad
            };
        }
    }
}