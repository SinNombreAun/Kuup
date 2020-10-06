using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ComponentModel;
using System.Web.Mvc;
using Mod.Entity;
using Newtonsoft.Json;

namespace Funciones.Kuup.Adicionales
{
    public static class ClsAdicional
    {
        [System.Diagnostics.DebuggerHidden()]
        public static T Convert<T>(this string input)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    if (input == "")
                    {
                        if (typeof(T) != typeof(String))
                        {
                            input = "0";
                        }
                    }
                    return (T)converter.ConvertFromString(input);
                }
                return default(T);
            }
            catch (NotSupportedException)
            {
                return default(T);
            }
        }
        public static class ClsGetHostNameAndIP
        {
            [System.Diagnostics.DebuggerHidden()]
            public static string GetIPv4Address(String sHostNameOrAddress)
            {
                try
                {
                    IPAddress[] aIPHostAddresses = Dns.GetHostAddresses(sHostNameOrAddress);

                    foreach (IPAddress ipHost in aIPHostAddresses)
                    {
                        if (ipHost.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            return ipHost.ToString();
                        }
                    }
                    foreach (IPAddress ipHost in aIPHostAddresses)
                    {
                        if (ipHost.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        {
                            IPHostEntry ihe = Dns.GetHostEntry(ipHost);
                            foreach (IPAddress ipEntry in ihe.AddressList)
                            {
                                if (ipEntry.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                {
                                    return ipEntry.ToString();
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                }
                return string.Empty;
            }
            [System.Diagnostics.DebuggerHidden()]
            public static string GetHostName(String UserHostAddres)
            {
                return System.Net.Dns.GetHostEntry(GetIPv4Address(UserHostAddres)).HostName;
            }
        }
        public class ClsResultado
        {
            private bool _Resultado;
            private String _Mensaje;
            private Object _Adicional;
            public bool Resultado
            {
                get { return _Resultado; }
                set { _Resultado = value; }
            }
            public String Mensaje
            {
                get { return _Mensaje; }
                set { _Mensaje = value; }
            }
            public Object Adicional
            {
                get { return _Adicional; }
                set { _Adicional = value; }
            }
            public ClsResultado() { }
            public ClsResultado(bool Resultado, String Mensaje)
            {
                _Resultado = Resultado;
                _Mensaje = Mensaje;
            }
            public ClsResultado(bool Resultado, String Mensaje, Object Adicional)
            {
                _Resultado = Resultado;
                _Mensaje = Mensaje;
                _Adicional = Adicional;
            }
            public string MensajeController()
            {
                String JsonResultado = JsonConvert.SerializeObject(this);
                return JsonResultado;
            }
        }
        public class ClsCargaCombo
        {
            public static List<SelectListItem> CargaComboClave(byte NumeroDeClave, String ValorPorDefecto)
            {
                byte ValorPorDefectoN = Convert<byte>(ValorPorDefecto);
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    return (from q in db.Claves where q.CVE_NUM_CLAVE == NumeroDeClave select new SelectListItem { Text = q.CVE_NUM_SEC_CLAVE.ToString() + " / " + q.CVE_NOM_CLAVE, Value = q.CVE_NUM_SEC_CLAVE.ToString(), Selected = q.CVE_NUM_SEC_CLAVE == ValorPorDefectoN }).ToList();
                }
            }
            public static List<SelectListItem> CargaComboProveedor(Nullable<byte> NumeroDeProveedor)
            {
                using(DBKuupEntities db = new DBKuupEntities())
                {
                    return (from q in db.Proveedor where q.PRV_NUM_PROVEEDOR == NumeroDeProveedor select new SelectListItem { Text = q.PRV_NUM_PROVEEDOR.ToString() + " / " + q.PRV_NOM_PROVEEDOR, Value = q.PRV_NUM_PROVEEDOR.ToString(), Selected = q.PRV_NUM_PROVEEDOR == NumeroDeProveedor }).ToList();
                }
            }
        }
    }
}
