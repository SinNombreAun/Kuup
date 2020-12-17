using BarcodeLib;
using Funciones.Kuup.Adicionales;
using Mod.Entity;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Razor.Generator;

namespace Funciones.Kuup.CodigoDeBarras
{
    public class MoCodigoDeBarras
    {
        private String _CodigoBarras;
        private String _RutaDeImagen;
        private ClsAdicional.ClsResultado _Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
        private Image _Imagen;
        public String CodigoBarras
        {
            get { return _CodigoBarras; }
            set { _CodigoBarras = value; }
        }
        public String RutaDeImagen
        {
            get { return _RutaDeImagen; }
            set { _RutaDeImagen = value; }
        }
        public ClsAdicional.ClsResultado Resultado
        {
            get { return _Resultado; }
            set { _Resultado = value; }
        }
        public Image Imagen
        {
            get { return _Imagen; }
            set { _Imagen = value; }
        }
        public MoCodigoDeBarras() { }
        public MoCodigoDeBarras GeneraCodigoDeBarras(short Secuencial, String TextoExtra,String CodigoPrevio = "")
        {
            try
            {
                if (String.IsNullOrEmpty(CodigoPrevio))
                {
                    CodigoBarras = ArmaNumeroDeBarras(Secuencial, TextoExtra);
                }
                else
                {
                    CodigoBarras = CodigoPrevio;
                }
                Imagen = GeneraImagen(CodigoBarras, true);
                if (Resultado.Resultado)
                {
                    using (DBKuupEntities db = new DBKuupEntities())
                    {
                        if ((from q in db.CodigoDeBarras where q.COB_CODIGO_BARRAS == CodigoBarras && q.COB_NUM_PRODUCTO == Secuencial select q).Count() == 0)
                        {
                            Mod.Entity.CodigoDeBarras codigoDeBarras = new Mod.Entity.CodigoDeBarras();
                            codigoDeBarras.COB_FECHA_GENERACION = DateTime.Now;
                            codigoDeBarras.COB_NUM_PRODUCTO = Secuencial;
                            codigoDeBarras.COB_CODIGO_BARRAS = CodigoBarras;
                            codigoDeBarras.COB_RUTA_ARCHIVO = ClsAdicional.ClsArchivos.RutaCodigoDeBarras + CodigoBarras + ".jpg";
                            codigoDeBarras.COB_CVE_ESTATUS = 1;
                            db.CodigoDeBarras.Add(codigoDeBarras);
                            db.SaveChanges();
                            if ((from q in db.CodigoDeBarras where q.COB_CODIGO_BARRAS == codigoDeBarras.COB_CODIGO_BARRAS && q.COB_NUM_PRODUCTO == codigoDeBarras.COB_NUM_PRODUCTO select q).Count() == 0)
                            {
                                Resultado.Resultado = false;
                                Resultado.Mensaje = "No fue posible gaurdar el registro de Codigo de Barras nuevo";
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Resultado.Resultado = false;
            }
            return this;
        }
        private Image GeneraImagen(String CodigoDeBarras, bool IncluirTexto = false)
        {
            RutaDeImagen = HostingEnvironment.MapPath(ClsAdicional.ClsArchivos.RutaCodigoDeBarras) + CodigoDeBarras + ".jpg";
            BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode(CodigoDeBarras);
            Codigo.IncludeLabel = IncluirTexto;
            Imagen = Codigo.Encode(BarcodeLib.TYPE.CODE128, CodigoDeBarras, Color.Black, Color.White);
            if (ClsAdicional.ClsArchivos.FileExists(HostingEnvironment.MapPath(ClsAdicional.ClsArchivos.RutaCodigoDeBarras), true, true))
            {
                Codigo.SaveImage(RutaDeImagen, SaveTypes.JPG);
                if (!ClsAdicional.ClsArchivos.FileExists(RutaDeImagen, false))
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "No fue posible encontrar la ruta de guardado de imagen del Codigo de Barras";
                    RutaDeImagen = String.Empty;
                }
            }
            else
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "No fue posible encontrar la ruta de guardado de imagen del Codigo de Barras";
                RutaDeImagen = String.Empty;
            }
            return Imagen;
        }
        public static String ArmaNumeroDeBarras(short Secuencial, String TextoExtra) => DateTime.Now.ToString("yyyyMMdd") + Secuencial.ToString().PadLeft(5, '0') + TextoExtra;
    }
}
