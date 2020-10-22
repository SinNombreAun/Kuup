using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentacion.Kuup.Models
{
    public class GeneralModel
    {
        public class FileUploadModelCsv
        {
            //[Required, FileExtensions(Extensions ="csv,txt", ErrorMessage = "El archivo a subir no cuenta con la extencion correcta CSV")]
            public HttpPostedFileBase Archivo { get; set; }
        }
    }
}