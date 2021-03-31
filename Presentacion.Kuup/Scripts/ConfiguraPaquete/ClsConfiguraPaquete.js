(function (windows, document) {
    let ConfiguraPaquete = (function () {
        let _Nucleo = function () {
            let Elementos_ConfiguraPaquete = {
                CodigoONombreDeProducto: 'fCodigoONombreDeProducto',
                Tabla: 'Tabla',
                TablaFilter: "Tabla_filter",
                SelectTablas: ["TextoAviso", "TextoCorreoSurtido", "TextoDeEstatus"]
            };
            let Funcionalidad = '',
                UrlCargaGrid = '',
                UrlAutoCompleteProducto = '',
                CombosParaTabla = [];
            let _Funcionalidad = function (FuncionalidadSet) {
                if (typeof (FuncionalidadSet) != 'undefined') {
                    Funcionalidad = FuncionalidadSet;
                } else {
                    return Funcionalidad;
                }
            };
            let _UrlCargaGrid = function (UrlCargaGridSet) {
                if (typeof (UrlCargaGridSet) != 'undefined') {
                    UrlCargaGrid = UrlCargaGridSet;
                } else {
                    return UrlCargaGrid;
                }
            };
            let _UrlAutoCompleteProducto = function (UrlAutoCompleteProductoSet) {
                if (typeof (UrlAutoCompleteProductoSet) != 'undefined') {
                    UrlAutoCompleteProducto = UrlAutoCompleteProductoSet;
                } else {
                    return UrlAutoCompleteProducto;
                }
            };
            let _CombosParaTabla = function (CombosParaTablaSet) {
                if (typeof CombosParaTablaSet != "") {
                    CombosParaTabla = CombosParaTablaSet;
                } else {
                    return CombosParaTabla;
                }
            };
            let _Inicio = function () {
                OcultaCampos();
                AgregaEvento();
                EjecutaEventosIniciales();
                if (Funcionalidad == 'INDEX') {
                    GeneraGridDeConfiguraPaquete();
                }
            };
            function GeneraGridDeConfiguraPaquete() {
                $("#" + Elementos_ConfiguraPaquete.Tabla + " thead tr").clone(true).appendTo("#" + Elementos_ConfiguraPaquete.Tabla + " thead");
                $("#" + Elementos_ConfiguraPaquete.Tabla + " thead tr:eq(1) th").each(function (i) {
                    if (this.id != "boton") {
                        if (Elementos_ConfiguraPaquete.SelectTablas.indexOf(this.id) > -1) {
                            var Elemento = CombosParaTabla[Elementos_ConfiguraPaquete.SelectTablas.indexOf(this.id)];
                            $(this).html(Elemento);
                            $("select", this).on("change", function () {
                                if (table.column(i).search() !== this.value) {
                                    table.column(i).search(this.value).draw();
                                }
                            });
                        } else {
                            var title = $(this).text();
                            $(this).html('<input type="text" style="width: 100%;" placeholder="Buscar ' + title + '" />');
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
                var table = $('#' + Elementos_ConfiguraPaquete.Tabla).DataTable({
                    "processing": true,
                    "serverSide": true,
                    "ajax": {
                        "type": "POST",
                        "url": UrlCargaGrid,
                        "async": false,
                        "datatype": "json"
                    },
                    "destroy": true,
                    "responsive": false,
                    "scrollY": 500,
                    "scrollX": true,
                    "select": true,
                    "scrollCollapse": false,
                    "pageLength": 100,
                    "filter": true,
                    "responsivePriority": 1,
                    "orderCellsTop": true,
                    "fixedHeader": true,
                    "data": null,
                    "order": [[1, 'desc']],
                    "columns": [
                        { "data": "CodigoDeBarrasPadre" },
                        { "data": "NombreDeProductoPadre" },
                        { "data": "PrecioDeProductoPadre", "render": $.fn.dataTable.render.number(',', '.', 2, '$') },
                        { "data": "CodigoDeBarrasHijo" },
                        { "data": "NombreDeProductoHijo" },
                        { "data": "PrecioDeProductoHijo", "render": $.fn.dataTable.render.number(',', '.', 2, '$') },
                        { "data": "ImporteTotal", "render": $.fn.dataTable.render.number(',', '.', 2, '$') }
                    ],
                    "rowGroup": {
                        "dataSrc": 'NombreDeProductoPadre'
                    },
                    "language": LenguajeEN()
                });
                table.draw();
                $('#' + Elementos_ConfiguraPaquete.TablaFilter).hide();
            }
            function OcultaCampos() {
                switch (Funcionalidad) {
                    case 'ALTA':
                        break;
                    case 'IMPORTAR':
                        break;
                }
            }
            function EjecutaEventosIniciales() {
                switch (Funcionalidad) {
                    case 'ALTA':
                        break;
                    case 'DETALLE':
                        break;
                }
            }
            function AgregaEvento() {
                switch (Funcionalidad) {
                    case 'INDEX':
                        break;
                    case 'ALTA':
                        $('#' + Elementos_ConfiguraPaquete.CodigoONombreDeProducto).autocomplete({
                            minLength: 3,
                            source: function (request, response) {
                                $.ajax({
                                    url: UrlAutoCompleteProducto,
                                    type: "POST",
                                    dataType: "json",
                                    data: { Prefix: request.term },
                                    async: false,
                                    success: function (data) {
                                        try {
                                            response($.map(data, function (item) {
                                                return { label: item.NombreDeProducto, value: item.NombreDeProducto };
                                            }));
                                        } catch (e) {
                                            //Excepcion controlada 
                                        }
                                    }
                                })
                            },
                            messages: {
                                noResults: "", results: ""
                            }
                        });
                        break;
                    case 'DETALLE':
                        break;
                }
            }
            return {
                Configuracion: {
                    Funcionalidad: _Funcionalidad,
                    UrlCargaGrid: _UrlCargaGrid,
                    UrlAutoCompleteProducto: _UrlAutoCompleteProducto,
                    CombosParaTabla: _CombosParaTabla
                },
                Inicio: _Inicio,
            }
        }
        let _Constructor = function (ObjetoConfiguracion) {
            let NucleoConfigurado = _Nucleo();
            NucleoConfigurado.Configuracion.Funcionalidad(ObjetoConfiguracion.Funcionalidad);
            NucleoConfigurado.Configuracion.UrlCargaGrid(ObjetoConfiguracion.UrlCargaGrid);
            NucleoConfigurado.Configuracion.UrlAutoCompleteProducto(ObjetoConfiguracion.UrlAutoCompleteProducto);
            NucleoConfigurado.Configuracion.CombosParaTabla(ObjetoConfiguracion.CombosParaTabla);
            return NucleoConfigurado;
        }
        return {
            Nucleo: _Nucleo,
            Constructor: _Constructor
        }
    })();
    window.Objeto = ConfiguraPaquete;
})(window, document);