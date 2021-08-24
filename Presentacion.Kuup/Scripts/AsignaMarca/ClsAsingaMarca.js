(function (windows, document) {
    let AsingaMarca = (function () {
        let _Nucleo = function () {
            let Elementos_AsingaMarca = {
                TipoDeProductoMarca: "fTipoDeProductoMarca",
                TablaTipoOMarca: "TablaTipoOMarca",
                TablaTipoOMarcaNoAsignados: "TablaTipoOMarcaNoAsignados",
                Asignados: "Asignados",
                NoAsignados: "NoAsignados",
                BtnOrigen: "BtnOrigen",
                BtnAsignados: "BtnAsignados",
                BtnNoAsignados: "BtnNoAsignados"
            };
            let Origen = "",
                TablaS = null,
                TablaSNoAsigandos = null,
                UrlAutoCompleteMarcaOTipProducto = "",
                Url = "",
                UrlCargaAsignados = "",
                UrlCargaNoAsignados = "",
                TipoDeProductoMarca = "",
                UrlAgregaElemento = "",
                UrlRemueveElemento = "";
            let _Origen = function (OrigenSet) {
                if (typeof (OrigenSet) != "undefined") {
                    Origen = OrigenSet;
                } else {
                    return Origen;
                }
            };
            let _UrlAutoCompleteMarcaOTipProducto = function (UrlAutoCompleteMarcaOTipProductoSet) {
                if (typeof (UrlAutoCompleteMarcaOTipProductoSet) != 'undefined') {
                    UrlAutoCompleteMarcaOTipProducto = UrlAutoCompleteMarcaOTipProductoSet;
                } else {
                    return UrlAutoCompleteMarcaOTipProducto;
                }
            };
            let _Url = function (UrlSet) {
                if (typeof (UrlSet) != 'undefined') {
                    Url = UrlSet;
                } else {
                    return Url;
                }
            };
            let _UrlCargaAsignados = function (UrlCargaAsignadosSet) {
                if (typeof (UrlCargaAsignadosSet) != "undefined") {
                    UrlCargaAsignados = UrlCargaAsignadosSet;
                } else {
                    return UrlCargaAsignados;
                }
            };
            let _UrlCargaNoAsignados = function (UrlCargaNoAsignadosSet) {
                if (typeof (UrlCargaNoAsignadosSet) != 'undefined') {
                    UrlCargaNoAsignados = UrlCargaNoAsignadosSet;
                } else {
                    return UrlCargaNoAsignados;
                }
            };
            let _TipoDeProductoMarca = function (TipoDeProductoMarcaSet) {
                if (typeof (TipoDeProductoMarcaSet) != 'undefined') {
                    TipoDeProductoMarca = TipoDeProductoMarcaSet;
                } else {
                    return TipoDeProductoMarca;
                }
            };
            let _UrlAgregaElemento = function (UrlAgregaElementoSet) {
                if (typeof (UrlAgregaElementoSet) != 'undefined') {
                    UrlAgregaElemento = UrlAgregaElementoSet;
                } else {
                    return UrlAgregaElemento;
                }
            };
            let _UrlRemueveElemento = function (UrlRemueveElementoSet) {
                if (typeof (UrlRemueveElementoSet) != 'undefined') {
                    UrlRemueveElemento = UrlRemueveElementoSet;
                } else {
                    return UrlRemueveElemento;
                }
            };
            let _Inicio = function () {
                $('#' + Elementos_AsingaMarca.TipoDeProductoMarca).val(TipoDeProductoMarca);
                if (TipoDeProductoMarca != '') {
                    ConsultaPorTipoOMarca();
                } 
                OcultaCampos();
                AgregaEvento();
                EjecutaEventosIniciales();
                CargaAsingados(Origen);
                CargaNoAsingados(Origen);
            };
            function CargaAsingados(Origen) {
                TablaS = $('#' + Elementos_AsingaMarca.TablaTipoOMarca).DataTable({
                    "destroy": true,
                    "responsive": false,
                    "scrollY": 300,
                    "scrollX": true,
                    "scrollCollapse": false,
                    "columns": [
                        { "data": "NumeroMarcaOTipo" },
                        { "data": "MarcaOTipo" },
                        { "defaultContent": "<button type='button' class='btnRemover btn btn-danger'><i class='far fa-trash-alt'></i></button>" }
                    ],
                    "paging": false,
                    "searching": false,
                    "language": LenguajeEN()
                });
                RemoverProducto('#' + Elementos_AsingaMarca.TablaTipoOMarca + ' tbody', TablaS);
                $('#' + Elementos_AsingaMarca.Asignados).show();
            }
            function CargaNoAsingados(Origen) {
                TablaSNoAsigandos = $('#' + Elementos_AsingaMarca.TablaTipoOMarcaNoAsignados).DataTable({
                    "destroy": true,
                    "responsive": false,
                    "scrollY": 300,
                    "scrollX": true,
                    "scrollCollapse": false,
                    "columns": [
                        { "data": "NumeroMarcaOTipo" },
                        { "data": "MarcaOTipo" },
                        { "defaultContent": "<button type='button' class='btnAgregar btn btn-danger'><i class='fa fa-plus'></i></button>" }
                    ],
                    "paging": false,
                    "searching": false,
                    "language": LenguajeEN()
                });
                AgregaProducto('#' + Elementos_AsingaMarca.TablaTipoOMarcaNoAsignados + ' tbody', TablaSNoAsigandos);
                $('#' + Elementos_AsingaMarca.NoAsignados).hide();
            }
            var AgregaProducto = function (tbody, table) {
                $(tbody).on('click', 'button.btnAgregar', function () {
                    debugger
                    let data = table.row($(this).parents('tr')).data();
                    if (typeof (data) == 'undefined') {
                        data = table.row($(this).parents('li')).data();
                    }
                    if ($('#' + Elementos_AsingaMarca.TipoDeProductoMarca).val() != '') {
                        if ($('#' + Elementos_AsingaMarca.TipoDeProductoMarca).val().includes('/')) {
                            alertify.confirm("¿Desea agregar el sigiente elemento " + data.MarcaOTipo + " a " + $('#' + Elementos_AsingaMarca.TipoDeProductoMarca).val().split('/')[1].trim() + "?",
                                function () {
                                    $.ajax({
                                        url: UrlAgregaElemento,
                                        type: "POST",
                                        dataType: "json",
                                        data: { Origen: Origen, NumeroDeElementoPadre: $('#' + Elementos_AsingaMarca.TipoDeProductoMarca).val().split('/')[0].trim(), NumeroElemento: data.NumeroMarcaOTipo },
                                        async: false,
                                        success: function (data) {
                                            if (data.Resultado) {
                                                table.row(this).remove().draw();
                                            } else {
                                                alertify.error(data.Mensaje);
                                            }
                                        }
                                    });
                                },
                                function () {

                                }
                            ).set('labels', { ok: 'Si, Asignar', cancel: 'No, Asignar' });

                        } else {
                            alertify.error('El valor sobre el cual se asignará el registro no es válido');
                        }
                    } else {
                        alertify.error('No sé a seleccionado ningún registro sobre el cual asignar el elemento');
                    }
                });
            };
            var RemoverProducto = function (tbody, table) {
                $(tbody).on('click', 'button.btnRemover', function () {
                    debugger
                    let data = table.row($(this).parents('tr')).data();
                    if (typeof (data) == 'undefined') {
                        data = table.row($(this).parents('li')).data();
                    }
                    if ($('#' + Elementos_AsingaMarca.TipoDeProductoMarca).val() != '') {
                        if ($('#' + Elementos_AsingaMarca.TipoDeProductoMarca).val().includes('/')) {
                            alertify.confirm("¿Desea remover el sigiente elemento " + data.MarcaOTipo + " de " + $('#' + Elementos_AsingaMarca.TipoDeProductoMarca).val().split('/')[1].trim() + "?",
                                function () {
                                    $.ajax({
                                        url: UrlRemueveElemento,
                                        type: "POST",
                                        dataType: "json",
                                        data: { Origen: Origen, NumeroDeElementoPadre: $('#' + Elementos_AsingaMarca.TipoDeProductoMarca).val().split('/')[0].trim(), NumeroElemento: data.NumeroMarcaOTipo },
                                        async: false,
                                        success: function (data) {
                                            if (data.Resultado) {
                                                table.row(this).remove().draw();
                                            } else {
                                                alertify.error(data.Mensaje);
                                            }
                                        }
                                    });
                                },
                                function () {

                                }
                            ).set('labels', { ok: 'Si, Remover', cancel: 'No, Remover' });
 
                        } else {
                            alertify.error('El valor sobre el cual se removerá  el registro no es válido');
                        }
                    } else {
                        alertify.error('No sé a seleccionado ningún registro sobre el cual remover el elemento');
                    }
                });
            };
            function OcultaCampos() {
            }
            function EjecutaEventosIniciales() {
            }
            function AgregaEvento() {
                $('#' + Elementos_AsingaMarca.BtnOrigen).click(function () {
                    let RegistroVal = '';
                    if ($('#' + Elementos_AsingaMarca.TipoDeProductoMarca).val() != '') {
                        RegistroVal = $('#' + Elementos_AsingaMarca.TipoDeProductoMarca).val();
                    }
                    location.href = Url + "?Origen=" + (Origen == "Tipo" ? "Marca" : "Tipo") + "&Registro=" + RegistroVal;
                });
                $('#' + Elementos_AsingaMarca.TipoDeProductoMarca).autocomplete({
                    minLength: 3,
                    source: function (request, response) {
                        $.ajax({
                            url: UrlAutoCompleteMarcaOTipProducto,
                            type: "POST",
                            dataType: "json",
                            data: { Prefix: request.term, Origen: Origen },
                            async: false,
                            success: function (data) {
                                try {
                                    response($.map(data, function (item) {
                                        return { label: item.NomTipoMarca, value: item.NumTipoMarca + " / " + item.NomTipoMarca };
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
                $('#' + Elementos_AsingaMarca.TipoDeProductoMarca).keydown(function (event) {
                    let keycode = (event.keyCode ? event.keyCode : event.which);
                    if (keycode == 13) {
                        ConsultaPorTipoOMarca();
                    }
                });
                $('#' + Elementos_AsingaMarca.BtnNoAsignados).click(function () {
                    $('#' + Elementos_AsingaMarca.NoAsignados).show();
                    $('#' + Elementos_AsingaMarca.Asignados).hide();
                    if ($('#' + Elementos_AsingaMarca.TipoDeProductoMarca).val() != '') {
                        ConsultaPorTipoOMarcaNo();
                    } else {
                        alertify.error("No se a seleccionado ningun registro para realizar la consulta");
                    }
                });
                $('#' + Elementos_AsingaMarca.BtnAsignados).click(function () {
                    $('#' + Elementos_AsingaMarca.Asignados).show();
                    $('#' + Elementos_AsingaMarca.NoAsignados).hide();
                    if ($('#' + Elementos_AsingaMarca.TipoDeProductoMarca).val() != '') {
                        ConsultaPorTipoOMarca();
                    } else {
                        alertify.error("No se a seleccionado ningun registro para realizar la consulta");
                    }

                });
            }
            function ConsultaPorTipoOMarca() {
                $.ajax({
                    type: "POST",
                    url: UrlCargaAsignados,
                    async: false,
                    data: { TipoDeProductoMarca: $('#' + Elementos_AsingaMarca.TipoDeProductoMarca).val(), Origen: Origen },
                    success: function (data) {
                        if (data.Resultado.Resultado) {
                            TablaS.clear().draw();
                            for (var i = 0; i < data.data.length; i++) {
                                TablaS.row.add({
                                    "NumeroMarcaOTipo": data.data[i].NumeroMarcaOTipo,
                                    "MarcaOTipo": data.data[i].MarcaOTipo
                                }).draw();
                            }
                        } else {
                            alertify.error(data.Resultado.Mensaje);
                        }
                    },
                    error: function () {
                        alertify.error("Ocurrio un error al realizar la consulta de Tipo o Marca asignados");
                    }
                });
            }
            function ConsultaPorTipoOMarcaNo() {
                $.ajax({
                    type: "POST",
                    url: UrlCargaNoAsignados,
                    async: false,
                    data: { TipoDeProductoMarca: $('#' + Elementos_AsingaMarca.TipoDeProductoMarca).val(), Origen: Origen },
                    success: function (data) {
                        if (data.Resultado.Resultado) {
                            TablaSNoAsigandos.clear().draw();
                            TablaSNoAsigandos.rows.add(data.data).draw();
                        } else {
                            alertify.error(data.Resultado.Mensaje);
                        }
                    },
                    error: function () {
                        alertify.error("Ocurrio un error al realizar la consulta de Tipo o Marca asignados");
                    }
                });
            }
            return {
                Configuracion: {
                    Origen: _Origen,
                    UrlAutoCompleteMarcaOTipProducto: _UrlAutoCompleteMarcaOTipProducto,
                    UrlCargaAsignados: _UrlCargaAsignados,
                    UrlCargaNoAsignados: _UrlCargaNoAsignados,
                    Url: _Url,
                    TipoDeProductoMarca: _TipoDeProductoMarca,
                    UrlAgregaElemento: _UrlAgregaElemento, 
                    UrlRemueveElemento: _UrlRemueveElemento
                },
                Inicio: _Inicio
            };
        };
        let _Constructor = function (ObjetoConfiguracion) {
            let NucleoConfigurado = _Nucleo();
            NucleoConfigurado.Configuracion.Origen(ObjetoConfiguracion.Origen);
            NucleoConfigurado.Configuracion.UrlAutoCompleteMarcaOTipProducto(ObjetoConfiguracion.UrlAutoCompleteMarcaOTipProducto);
            NucleoConfigurado.Configuracion.UrlCargaAsignados(ObjetoConfiguracion.UrlCargaAsignados);
            NucleoConfigurado.Configuracion.UrlCargaNoAsignados(ObjetoConfiguracion.UrlCargaNoAsignados);
            NucleoConfigurado.Configuracion.Url(ObjetoConfiguracion.Url);
            NucleoConfigurado.Configuracion.TipoDeProductoMarca(ObjetoConfiguracion.TipoDeProductoMarca);
            NucleoConfigurado.Configuracion.UrlAgregaElemento(ObjetoConfiguracion.UrlAgregaElemento);
            NucleoConfigurado.Configuracion.UrlRemueveElemento(ObjetoConfiguracion.UrlRemueveElemento);
            return NucleoConfigurado;
        };
        return {
            Nucleo: _Nucleo,
            Constructor: _Constructor,
        };
    })();
    window.Objeto = AsingaMarca;
})(window, document);
