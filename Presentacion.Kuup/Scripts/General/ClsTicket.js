const RUTA_API = "http://localhost:8000"
const $impresoraSeleccionada = document.querySelector("#impresoraSeleccionada");

const establecerImpresoraComoPredeterminada = nombreImpresora => {
    Impresora.setImpresora(nombreImpresora).then(respuesta => {
        refrescarNombreDeImpresoraSeleccionada();
        if (respuesta) { } else { }
    });
};

const refrescarNombreDeImpresoraSeleccionada = () => {
    Impresora.getImpresora().then(nombreImpresora => {
        $impresoraSeleccionada.textContent = nombreImpresora;
    });
}
function ImprimirTicket(ObjetoTicket) {
    establecerImpresoraComoPredeterminada('POS-58');
    let impresora = new Impresora(RUTA_API);
    impresora.setFontSize(1, 1);
    impresora.setEmphasize(0);
    impresora.setAlign("center");
    impresora.write(ObjetoTicket.Empresa + '\n');
    if (ObjetoTicket.Dir != '') {
        impresora.setAlign("left");
        impresora.write('Dir: ' + ObjetoTicket.Dir + '\n');
    }
    if (ObjetoTicket.Tel != '') {
        impresora.setAlign("left");
        impresora.write('Tel: ' + ObjetoTicket.Tel + '\n');
    }
    impresora.setAlign("left");
    impresora.write("Folio de Venta: " + ObjetoTicket.FolioDeVenta + '\n');
    impresora.write("Le Atendio: " + ObjetoTicket.Atiende + '\n');
    impresora.setAlign("center");
    impresora.write(ObjetoTicket.Fecha + '\n');
    for (var i = 0; i < ObjetoTicket.ListaProducto.length; i++) {
        impresora.setAlign("left");
        impresora.write(ObjetoTicket.ListaProducto[i].NombreDeProducto + '\n');
        impresora.write('Precio: ' + ObjetoTicket.ListaProducto[i].PrecioUnitario +
            ' Cant: ' + ObjetoTicket.ListaProducto[i].CantidadDeProducto +
            ' Importe: $' + ObjetoTicket.ListaProducto[i].ImporteDeProducto +'\n');
    }
    impresora.setAlign("right");
    impresora.write('Importe Total:     $' + ObjetoTicket.ImporteTotal + '\n');
    impresora.write('Importe Entregado: $' + ObjetoTicket.ImporteEntregado + '\n');
    impresora.write('Importe Cambio:    $' + ObjetoTicket.Cambio + '\n');
    if (ObjetoTicket.TextoFinal != '') {
        impresora.write('\n' + ObjetoTicket.TextoFinal);
    }
    impresora.cut();
    impresora.cutPartial();
    impresora.end().then(valor => {
        alertify.success("Ticket impreso");
    });
}
refrescarNombreDeImpresoraSeleccionada();
