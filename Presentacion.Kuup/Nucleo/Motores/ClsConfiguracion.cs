using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Kuup.Nucleo.Motores
{
    public static class ClsConfiguracion
    {
        private static readonly String _RutaCodigoDeBarras = "~/Imagenes/CodigoBarras";
        public static String RutaCodigoDeBarras
        {
            get { return _RutaCodigoDeBarras; }
        }
        private static readonly String _RutaDescarga = "~/App_Data/Download/";
        public static String RutaDescarga
        {
            get { return _RutaDescarga; }
        }
        private static readonly String _RutaUpload = "~/App_Data/Upload/";
        public static String RutaUpload
        {
            get { return _RutaUpload; }
        }
        private static readonly String _Tickets = "~/App_Data/Ticket/";
        public static String Tickets
        {
            get { return _Tickets; }
        }
    }
    public class PDFBuild<T> : System.Web.Mvc.Controller
    {
        private ControllerContext controllerContext { get; set; }
        private String vistaReporte { get; set; }
        private String nombreDeReporte { get; set; }
        private String ruta { get; set; }
        private TempDataDictionary temporalData{ get; set; }
        private T model { get; set; }
        public PDFBuild(ControllerContext _controllerContext, String _vistaReporte, String _nombreDeReporte,String _ruta, TempDataDictionary _temporalData, T _model)
        {
            controllerContext = _controllerContext;
            vistaReporte = _vistaReporte;
            nombreDeReporte = _nombreDeReporte;
            temporalData = _temporalData;
            model = _model;
            ruta = _ruta;
        }

        public ActionResult generaPDF()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                StringWriter stringWriter = new StringWriter();
                System.Web.HttpContext.Current.Response.Clear();
                ViewEngineResult viewEngineResult = ViewEngines.Engines.FindView(controllerContext, vistaReporte, null);
                ViewContext viewContext = new ViewContext(controllerContext, viewEngineResult.View, new ViewDataDictionary(model), temporalData, stringWriter);
                viewEngineResult.View.Render(viewContext, stringWriter);
                String HtmlString = stringWriter.ToString();

                List<string> cssFiles = new List<string>();
                cssFiles.Add("~/Content/Styles/Site/Site.css");
                cssFiles.Add("~/Content/Styles/Account/all.css");
                cssFiles.Add("~/Content/Librerias/JQuery.UI/jquery-ui.theme.css");
                cssFiles.Add("~/Content/Librerias/JQuery.UI/jquery-ui.structure.css");
                cssFiles.Add("~/Content/Styles/Site/ReportesPDF.css");

                Document Pdf = new Document(PageSize.A4);
                StringReader stringReader = new StringReader(HtmlString);
                PdfWriter write = PdfWriter.GetInstance(Pdf, stream);
                write.CloseStream = false;
                Pdf.Open();
                HtmlPipelineContext htmlPipelineContext = new HtmlPipelineContext(null);
                htmlPipelineContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());
                ICSSResolver iCSSResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);
                cssFiles.ForEach(x => iCSSResolver.AddCssFile(System.Web.HttpContext.Current.Server.MapPath(x), true));
                IPipeline pipeline = new CssResolverPipeline(iCSSResolver, new HtmlPipeline(htmlPipelineContext, new PdfWriterPipeline(Pdf,write)));
                XMLWorker xMLWorkerHelper = new XMLWorker(pipeline,true);
                XMLParser xMLParse = new XMLParser(xMLWorkerHelper);
                xMLParse.Parse(new MemoryStream(Encoding.UTF8.GetBytes(HtmlString)));
                Pdf.Close();
                using (FileStream fileStream = System.IO.File.Create(ruta + nombreDeReporte + ".pdf"))
                {
                    fileStream.Write(stream.ToArray(), 0, stream.ToArray().Length);
                }
                return File(ruta + nombreDeReporte + ".pdf", System.Net.Mime.MediaTypeNames.Application.Octet, nombreDeReporte + ".pdf");
            }
        }
    }
}