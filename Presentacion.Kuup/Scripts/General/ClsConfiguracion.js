$(document).ajaxStart(function () {
    $.blockUI({
        message: '<img src="/Content/Imagenes/BlockUI/cargakuup.gif" style="width: 60%" />'
    });
}).ajaxStop($.unblockUI);

function CreaFiltrosTabla(idTabla,table, itemSelect, itemSelectValue) {
    $('#' + idTabla + ' thead tr').clone(true).appendTo('#' + idTabla + ' thead');
    $('#' + idTabla + ' thead tr:eq(1) th').each(function (i) {
        if (this.id != 'boton') {
            if (itemSelect.indexOf(this.id) > -1) {
                var Elemento = itemSelectValue[itemSelect.indexOf(this.id)];
                $(this).html(Elemento);
                $('select', this).on('change', function () {
                    if (table.column(i).search() !== this.value) {
                        table.column(i).search(this.value).draw();
                    }
                });
            } else {
                var campo = $(this).text();
                $(this).html('<input type="text" style="width: 100%;" placeholder="Buscar ' + campo + '" />');
                $("input", this).on("keydown", function () {
                    let keycode = event.keyCode ? event.keyCode : event.which;
                    if (keycode == 13) {
                        if (table.column(i).search() !== this.value) {
                            table.column(i).search(this.value).draw();
                        }
                    }
                });
            }
        }
    });
}
function CurrencyFormat(d, thousands, decimal, precision, prefix, postfix) {
    if (typeof d !== 'number' && typeof d !== 'string') {
        return d;
    }

    var negative = d < 0 ? '-' : '';
    var flo = parseFloat(d);

    if (isNaN(flo)) {
        return htmlEscapeEntitiesGen(d);
    }

    flo = flo.toFixed(precision);
    d = Math.abs(flo);

    var intPart = parseInt(d, 10);
    var floatPart = precision ? decimal + (d - intPart).toFixed(precision).substring(2) : '';

    return negative + (prefix || '') + intPart.toString().replace(/\B(?=(\d{3})+(?!\d))/g, thousands) + floatPart + (postfix || '');
}
function htmlEscapeEntitiesGen(d) {
    return typeof d === 'string' ? d.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;') : d;
};

