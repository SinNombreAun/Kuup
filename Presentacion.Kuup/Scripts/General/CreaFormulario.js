/**
 * Crea popup de para HTML de forma dinámica.
 * @function CreaDialogo
 * @param {string} ContenedorDialog: id de div contenedor del formulario en HTML.
 * @param {string} Titulo: Titulo del formulario en HTML.
 * @param {Object} Botones: Objeto de botones dinámicos para el formulario.
 * @param {number} Width: Tamaño del formulario ancho.
 * @param {number} Height: Tamaño del formulario largo.
 */
function CreaDialog(ContenedorDialog, Titulo, Botones, Width, Height) {
    $("#" + ContenedorDialog).dialog({
        autoOpen: false,
        bgiframe: true,
        modal: true,
        title: Titulo,
        width: Width,
        height: Height,
        resizable: false,
        open: function () { }
    });
    $("#" + ContenedorDialog).dialog('option', 'buttons', Botones);
}
/**
 * Genera string de HTML el cual será agregado al popup.
 * @function CreaHTML
 * @param {Object} Elemento: Objeto dinámico para los elementos que serán agregados al formulario.
 * @param {string} PropiedadesAdicionales: propiedades adicionales como estilos.
 * @returns {string} HTML
 */
function CreaHTML(Elemento, PropiedadesAdicionales) {
    var HTML = "";
    HTML += '<div class="field" ' + PropiedadesAdicionales + '>';
    for (var i = 0; i < Elemento.length; i++) {
        switch (Elemento[i].Tipo.toLowerCase()) {
            case "label":
                HTML += SpanLabel(Elemento[i]);
                break;
            case "textbox":
                HTML += SpanTextBox(Elemento[i]);
                break;
            case "checkbox":
                HTML += SpanCheckBox(Elemento[i]);
                break;
            case "select":
                HTML += SpanSelect(Elemento[i]);
                break;
            case "selectlistitem":
                HTML += SpanSelectListItem(Elemento[i]);
                break;
            case "textarea":
                HTML += SpanTextArea(Elemento[i]);
        }
    }
    HTML += '</div>';
    return HTML;
}
/**
 * Genera label para el formulario
 * @function SpanLabel
 * @param {Object} Elemento: Objeto dinámico para los elementos que serán agregados al formulario.
 * @returns {string} HTML
 */
function SpanLabel(Elemento) {
    var HTML = "";
    HTML += '<span class="display-field" ' + Elemento.AddParamSpan + '>';
    HTML += '<label ' + Elemento.AddParamElement + ' for="' + Elemento.Id + '">' + Elemento.Valor + '</label>';
    if (typeof (Elemento.Requerida) != "undefined") {
        if (Elemento.Requerida) {
            HTML += '<a class="captura-requerida">*</a>'
        }
    }
    HTML += '</span>';
    return HTML;
}
/**
 * Genera Textbox para el formulario
 * @function SpanTextBox
 * @param {Object} Elemento: Objeto dinámico para los elementos que serán agregados al formulario.
 * @returns {string} HTML
 */
function SpanTextBox(Elemento) {
    var HTML = "";
    HTML += '<span class="editor-field" ' + Elemento.AddParamSpan + '>';
    if (typeof (Elemento.AgregaTooltip) != "undefined") {
        if (Elemento.AgregaTooltip) {
            HTML += '<a class="tooltip" id="' + Elemento.Id + '_tooltip" style="margin-right: 6px;" title="' + Elemento.TituloTooltip + '">?</a>'
        }
    }
    HTML += '<input ' + Elemento.AddParamElement + ' class="' + Elemento.Class + '" type="text" id="' + Elemento.Id + '" value="' + Elemento.Valor + '">';
    HTML += '<span class="field-validation-valid" data-valmsg-for= "' + Elemento.Id + '" data-valmsg-replace="true"></span>';
    HTML += '</span>';
    return HTML;
}
/**
 * Genera checkbox para el formulario
 * @function SpanCheckBox
 * @param {Object} Elemento: Objeto dinámico para los elementos que serán agregados al formulario.
 * @returns {string} HTML
 */
function SpanCheckBox(Elemento) {
    var HTML = "";
    HTML += '<span class="editor-field" ' + Elemento.AddParamSpan + '>';
    if (typeof (Elemento.AgregaTooltip) != "undefined") {
        if (Elemento.AgregaTooltip) {
            HTML += '<a class="tooltip" id="' + Elemento.Id + '_tooltip" style="margin-right: 6px;" title="' + Elemento.TituloTooltip + '">?</a>'
        }
    }
    HTML += '<input ' + Elemento.AddParamElement + ' type="checkbox" id="' + Elemento.Id + '" value="' + Elemento.Valor + '">';
    HTML += '<span class="field-validation-valid" data-valmsg-for= "' + Elemento.Id + '" data-valmsg-replace="true"></span>';
    HTML += '</span>';
    return HTML;
}
/**
 * Genera select para el formulario
 * @function SpanSelect
 * @param {Object} Elemento: Objeto dinámico para los elementos que serán agregados al formulario.
 * @returns {string} HTML
 */
function SpanSelect(Elemento) {
    var HTML = "";
    HTML += '<span class="editor-field" ' + Elemento.AddParamSpan + '>';
    if (typeof (Elemento.AgregaTooltip) != "undefined") {
        if (Elemento.AgregaTooltip) {
            HTML += '<a class="tooltip" id="' + Elemento.Id + '_tooltip" style="margin-right: 6px;" title="' + Elemento.TituloTooltip + '">?</a>'
        }
    }
    HTML += '<select id="' + Elemento.Id + '" name="' + Elemento.Id + '" class="' + Elemento.Class + '" ' + Elemento.AddParamElement + '>';
    if (Elemento.Seleccione) {
        HTML += '<option value="0">--SELECCIONE--</option>'
    }
    if (typeof (Elemento.Componentes) != "undefined") {
        for (var i = 0; i < Elemento.Componentes.options.length; i++) {
            if (Elemento.Valor.toUpperCase() == Elemento.Componentes.options[i].Name.toUpperCase()) {
                HTML += '<option value="' + Elemento.Componentes.options[i].Id + '" selected>' + Elemento.Componentes.options[i].Name.toUpperCase() + '</option>';
            } else {
                HTML += '<option value="' + Elemento.Componentes.options[i].Id + '">' + Elemento.Componentes.options[i].Name.toUpperCase() + '</option>';
            }
        }
    }
    HTML += '</select>';
    HTML += '<span class="field-validation-valid" data-valmsg-for= "' + Elemento.Id + '" data-valmsg-replace="true"></span>';
    HTML += '</span>';
    return HTML;
}
/**
 * Genera selectListItem para el formulario
 * @function SpanSelect
 * @param {Object} Elemento: Objeto dinámico para los elementos que serán agregados al formulario con estructura de SelectListItem.
 * @returns {string} HTML
 */
function SpanSelectListItem(Elemento) {
    var HTML = "";
    HTML += '<span class="editor-field" ' + Elemento.AddParamSpan + '>';
    if (typeof (Elemento.AgregaTooltip) != "undefined") {
        if (Elemento.AgregaTooltip) {
            HTML += '<a class="tooltip" id="' + Elemento.Id + '_tooltip" style="margin-right: 6px;" title="' + Elemento.TituloTooltip + '">?</a>'
        }
    }
    HTML += '<select id="' + Elemento.Id + '" name="' + Elemento.Id + '" class="' + Elemento.Class + '" ' + Elemento.AddParamElement + '>';
    if (Elemento.Seleccione) {
        HTML += '<option value="0">--SELECCIONE--</option>'
    } 
    if (typeof (Elemento.Componentes) != "undefined") {
        for (var i = 0; i < Elemento.Componentes.options.length; i++) {
            if (Elemento.Componentes.options[i].Selected) {
                HTML += '<option value="' + Elemento.Componentes.options[i].Value + '" selected>' + Elemento.Componentes.options[i].Text.toUpperCase() + '</option>';
            } else {
                HTML += '<option value="' + Elemento.Componentes.options[i].Value + '">' + Elemento.Componentes.options[i].Text.toUpperCase() + '</option>';
            }
        }
    }
    HTML += '</select>';
    HTML += '<span class="field-validation-valid" data-valmsg-for= "' + Elemento.Id + '" data-valmsg-replace="true"></span>';
    HTML += '</span>';
    return HTML;
}
function SpanTextArea(Elemento) {
    var HTML = "";
    HTML += '<span class="editor-field" ' + Elemento.AddParamSpan + '>';
    if (typeof (Elemento.AgregaTooltip) != "undefined") {
        if (Elemento.AgregaTooltip) {
            HTML += '<a class="tooltip" id="' + Elemento.Id + '_tooltip" style="margin-right: 6px;" title="' + Elemento.TituloTooltip + '">?</a>'
        }
    }
    let AutoSize = 'cols="65"'
    if (typeof (Elemento.AutoSize) != "undefined") {
        if (Elemento.AutoSize) {
            AutoSize = 'cols="65" onkeydown="AutoSizeCreaFormulario(\'' + Elemento.Id + '\')"';
        }
    }
    HTML += '<textarea data-val="true" data-val-required="" id="' + Elemento.Id + '" name="' + Elemento.Id + '" rows="5" ' + AutoSize + '>' + Elemento.Valor + '</textarea>'
    HTML += '<span class="field-validation-valid" data-valmsg-for= "' + Elemento.Id + '" data-valmsg-replace="true"></span>';
    HTML += '</span>';
    return HTML;
}
/**
 * Genera títulos para el formulario.
 * @function TitulosParaFormulario
 * @param {Object} Elemento: Objeto dinámico para los elementos que serán agregados al formulario.
 * @param {string} PropiedadesAdicionales: propiedades adicionales como estilos.
 * @returns {string} HTML
 */
function TitulosParaFormulario(Elemento, PropiedadesAdicionales) {
    var HTML = "";
    HTML += '<div class="field" ' + PropiedadesAdicionales + '>';
    for (var i = 0; i < Elemento.length; i++) {
        HTML += SpanLabel(Elemento[i]);
    }
    HTML += '</div>';
    return HTML;
}

function AutoSizeCreaFormulario(id) {
    let elemento = $("#" + id);
    setTimeout(function () {
        elemento.css = 'height:auto; padding:0';
        elemento.css = 'height:' + elemento.scrollHeight + 'px';
    }, 0);
}