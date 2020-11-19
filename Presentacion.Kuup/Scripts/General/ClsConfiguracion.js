function BloqueaPantalla() {
    $.blockUI({
        menssage: '<h1><img src="~/Content/Imagenes/cargakuup.gif" /></h1>'
    });
}
function DesbloqueaPantalla() {
    $.unblockUI();
}