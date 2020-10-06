using Funciones.Kuup.Adicionales;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funciones.Kuup.CodigoDeBarras
{
    public static class MoCodigoDeBarras
    {
        public static ClsAdicional.ClsResultado GeneraCodigoDeBarras(short Secuencial, String TextoExtra)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado();
            Resultado.Mensaje = ArmaNumeroDeBarras(Secuencial, TextoExtra);
            Resultado.Adicional = GeneraImagen(Resultado.Mensaje, true);
            return Resultado;
        }
        private static Image GeneraImagen(String CodigoDeBarras, bool IncluirTexto = false)
        {
            BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode(CodigoDeBarras);
            Codigo.IncludeLabel = false;
            return Codigo.Encode(BarcodeLib.TYPE.CODE128, CodigoDeBarras, Color.Black,Color.White);
        }
        private static String ArmaNumeroDeBarras(short Secuencial, String TextoExtra) => DateTime.Now.ToString("yyyyMMdd") + Secuencial.ToString().PadLeft(5, '0') + TextoExtra;
    }
}
