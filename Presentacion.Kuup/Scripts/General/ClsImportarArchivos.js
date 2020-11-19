let cargaImportar = true;
let Elementos_Impotar = {
    ImportarUploadObjetivo: 'ImportarUploadObjetivo',
    ImportarArchivoUpload: 'ImportarArchivoUpload',
    ImportarInformacionValidacion: 'ImportarInformacionValidacion',
    Archivo: 'Archivo'
};
function ImportarCargaYValida() {
    $('#' + Elementos_Impotar.ImportarInformacionValidacion).hide();
    let RutaYNombreDelArchivo = $('#' + Elementos_Impotar.Archivo).val();
    if (RutaYNombreDelArchivo.length > 0) {
        $('#' + Elementos_Impotar.ImportarArchivoUpload).submit();
    } else {
        $('#' + Elementos_Impotar.ImportarInformacionValidacion).addClass('text-danger');
        $('#' + Elementos_Impotar.ImportarInformacionValidacion).html('No haz seleccionado un archivo');
        $('#' + Elementos_Impotar.ImportarInformacionValidacion).show();
    }
}
function ValidarArchivo(UrlAccion, BotonCargaDatos) {
    let Resultado = ''
    if ($('#' + Elementos_Impotar.ImportarUploadObjetivo).contents().find('#Validador').length != 0) {
        Resultado = $('#' + Elementos_Impotar.ImportarUploadObjetivo).contents().find('#Validador')[0].innerHTML;
        if (Resultado == '') {
            let ArregloNombreDelArchivo = $('#' + Elementos_Impotar.Archivo).val().split('\\');
            let NombreDelArchivoAValidar = ArregloNombreDelArchivo[ArregloNombreDelArchivo.length - 1];
            $.ajax({
                type: "POST",
                url: UrlAccion,
                async: false,
                data: { NombreDelArchivo: NombreDelArchivoAValidar },
                success: function (data) {
                    CreaTabla(data.data);
                    if (data.Resultado.Resultado) {
                        $(BotonCargaDatos).show();
                    } else {
                        $(BotonCargaDatos).hide();
                        alertify.error(data.Resultado.Mensaje, 0)
                    }
                },
                error: function () {
                    alertify.error("Ocurrio un error al realizar la accion de carga de archivo");
                }
            });
        } else {
            $('#' + Elementos_Impotar.ImportarInformacionValidacion).addClass('text-danger');
            $('#' + Elementos_Impotar.ImportarInformacionValidacion).html(Resultado);
            $('#' + Elementos_Impotar.ImportarInformacionValidacion).show();
        }
    }
}
function LimpiaTabla(BotonCargaDatos) {
    $(BotonCargaDatos).hide();
    LimpiaTablaPantalla();
}