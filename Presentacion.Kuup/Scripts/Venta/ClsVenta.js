(function (windows, document) {
    let Venta = (function () {
        let _Nucleo = function () {
            let Elementos_Venta = {
                VentasTotales: 'VentasTotales',
                Ventas: 'Ventas',
                FechaInicial: 'fFechaInicial',
                FechaFinal: 'fFechaFinal',
                Consulta: 'Consulta',
                ContVentas: 'ContVentas',
                Guardar: 'Guardar',
                GeneraReporte: 'GeneraReporte'
            };
            let Funcionalidad = '',
                UrlCargaGrid = '',
                UrlGeneraReporte = '',
                UrlDetalle = '',
                FechaInicialLimite = '',
                TablaVentasTotales = null,
                TablaVentas = null;
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
            let _UrlGeneraReporte = function (UrlGeneraReporteSet) {
                if (typeof (UrlGeneraReporteSet) != 'undefined') {
                    UrlGeneraReporte = UrlGeneraReporteSet;
                } else {
                    return UrlGeneraReporte;
                }
            }
            let _UrlDetalle = function (UrlDetalleSet) {
                if (typeof (UrlDetalleSet) != 'undefined') {
                    UrlDetalle = UrlDetalleSet;
                } else {
                    return UrlDetalle;
                }
            };
            let _FechaInicialLimite = function (FechaInicialLimiteSet) {
                if (typeof (FechaInicialLimiteSet) != 'undefined') {
                    FechaInicialLimite = FechaInicialLimiteSet;
                } else {
                    return FechaInicialLimite;
                }
            }
            let _Inicio = function () {
                OcultaCampos();
                AgregaEvento();
                EjecutaEventosIniciales();
                $('.datetimepicker').datepicker({
                    minDate: FechaInicialLimite
                });
                if (Funcionalidad == 'INDEX') {
                    GeneraGridDeVentaTotal();
                    GeneraGridDeVentaDetalle();
                }
            };
            function GeneraGridDeVentaTotal() {
                TablaVentasTotales = $('#' + Elementos_Venta.VentasTotales).DataTable({
                    "destroy": true,
                    "responsive": false,
                    "scrollY": 300,
                    "scrollX": true,
                    "scrollCollapse": false,
                    "lengthChange": false,
                    "pageLength": 10000000,
                    "columns": [
                        { "data": "FolioDeOperacion" },
                        { "data": "FechaDeOperacion" },
                        { "data": "NombreDeUsuario" },
                        { "data": "ImporteEntregado", "render": $.fn.dataTable.render.number(',', '.', 2, '$' )  },
                        { "data": "ImporteCambio", "render": $.fn.dataTable.render.number(',', '.', 2, '$' )  },
                        { "data": "ImporteNeto", "render": $.fn.dataTable.render.number(',', '.', 2, '$') }, 
                        { "data": "TextoDeEstatus" }
                    ],
                    "footerCallback": function (row, data, start, end, display) {
                        var api = this.api(), data;

                        // Remove the formatting to get integer data for summation
                        var intVal = function (i) {
                            return typeof i === 'string' ?
                                i.replace(/[\$,]/g, '') * 1 :
                                typeof i === 'number' ?
                                    i : 0;
                        };

                        // Total over all pages
                        total = api
                            .column(5)
                            .data()
                            .reduce(function (a, b) {
                                return intVal(a) + intVal(b);
                            }, 0);

                        // Total over this page
                        pageTotal = api
                            .column(5, { page: 'current' })
                            .data()
                            .reduce(function (a, b) {
                                return intVal(a) + intVal(b);
                            }, 0);

                        // Update footer
                        $(api.column(3).footer()).html(
                            CurrencyFormat(pageTotal, ',', '.', 2, '$')  + ' ( ' + CurrencyFormat(total,',', '.', 2, '$') + ' total)'
                        );
                    },
                    "language": LenguajeEN()
                });
                $('#' + Elementos_Venta.VentasTotales + ' tbody').on('dblclick', 'tr', function () {
                    if ($(this).hasClass('selected')) {
                        $(this).removeClass('selected');
                        $('#' + Elementos_Venta.ContVentas).hide();
                    } else {
                        TablaVentasTotales.$('tr.selected').removeClass('selected');
                        $(this).addClass('selected');
                        CargaDetalleDeVentaTotal(this);
                    }
                });
            }
            function GeneraGridDeVentaDetalle() {
                TablaVentas = $('#' + Elementos_Venta.Ventas).DataTable({
                    "destroy": true,
                    "responsive": false,
                    "scrollX": true,
                    "scrollCollapse": false,
                    "pageLength": 100,
                    "paging": false,
                    "searching": false,
                    "columns": [
                        { "data": "FolioDeOperacion"},
                        { "data": "NombreDeProducto" },
                        { "data": "CantidadDeProducto" },
                        { "data": "PrecioUnitario", "render": $.fn.dataTable.render.number(',', '.', 2, '$') },
                        { "data": "ImporteDeProducto", "render": $.fn.dataTable.render.number(',', '.', 2, '$') }
                    ],
                    "language": LenguajeEN()
                });
                $('#' + Elementos_Venta.ContVentas).hide();
            }
            function CargaDetalleDeVentaTotal(item) {
                $('#' + Elementos_Venta.ContVentas).show();
                $.ajax({
                    type: "POST",
                    url: UrlDetalle,
                    async: false,
                    data: { fFolioDeOperacion: item.childNodes[0].innerText },
                    success: function (data) {
                        TablaVentas.clear().draw();
                        if (data.data.length > 0) {
                            for (var i = 0; i < data.data.length; i++) {
                                TablaVentas.row.add({
                                    "FolioDeOperacion": data.data[i].FolioDeOperacion,
                                    "NombreDeProducto": data.data[i].NombreDeProducto,
                                    "CantidadDeProducto": data.data[i].CantidadDeProducto,
                                    "PrecioUnitario": data.data[i].PrecioUnitario,
                                    "ImporteDeProducto": data.data[i].ImporteDeProducto
                                }).draw();
                            }
                        }
                    },
                    error: function () {
                        alertify.error("Ocurrio un error al realizar la accion de Consulta");
                    }
                });
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
                        $('#' + Elementos_Venta.FechaInicial).change(function () {
                            $('#' + Elementos_Venta.FechaFinal).datepicker("destroy");
                            $('#' + Elementos_Venta.FechaFinal).datepicker({
                                minDate: $('#' + Elementos_Venta.FechaInicial).val()
                            });
                        });
                        $('#' + Elementos_Venta.Consulta).click(function () {
                            $.ajax({
                                type: "POST",
                                url: UrlCargaGrid,
                                //async: false,
                                data: { fFechaInicial: $('#' + Elementos_Venta.FechaInicial).val(), fFechaFinal: $('#' + Elementos_Venta.FechaFinal).val() },
                                success: function (data) {
                                    TablaVentasTotales.clear().draw();
                                    if (data.data.length > 0) {
                                        for (var i = 0; i < data.data.length; i++) {
                                            TablaVentasTotales.row.add({
                                                "FolioDeOperacion": data.data[i].FolioDeOperacion,
                                                "FechaDeOperacion": data.data[i].FechaDeOperacion,
                                                "NombreDeUsuario": data.data[i].NombreDeUsuario,
                                                "ImporteEntregado": data.data[i].ImporteEntregado,
                                                "ImporteCambio": data.data[i].ImporteCambio,
                                                "ImporteNeto": data.data[i].ImporteNeto,
                                                "TextoDeEstatus": data.data[i].TextoDeEstatus
                                            }).draw();
                                        }
                                    }
                                },
                                error: function () {
                                    alertify.error("Ocurrio un error al realizar la accion de Consulta");
                                }
                            });
                        });
                        $('#' + Elementos_Venta.GeneraReporte).click(function () {
                            let UrlGeneraReporteParam = "/?fFechaInicial=" + $('#' + Elementos_Venta.FechaInicial).val()
                                + "&fFechaFinal=" + $('#' + Elementos_Venta.FechaFinal).val() 
                                + "&Tipo=PDF" 
                            window.open(UrlGeneraReporte + UrlGeneraReporteParam, "_blank");
                        });
                        break;
                    case 'ALTA':
                        break;
                    case 'DETALLE':
                        break;
                }
            }
            return {
                Configuracion: {
                    Funcionalidad: _Funcionalidad,
                    UrlCargaGrid: _UrlCargaGrid,
                    UrlGeneraReporte: _UrlGeneraReporte,
                    UrlDetalle: _UrlDetalle,
                    FechaInicialLimite: _FechaInicialLimite
                },
                Inicio: _Inicio,
            }
        }
        let _Constructor = function (ObjetoConfiguracion) {
            let NucleoConfigurado = _Nucleo();
            NucleoConfigurado.Configuracion.Funcionalidad(ObjetoConfiguracion.Funcionalidad);
            NucleoConfigurado.Configuracion.UrlCargaGrid(ObjetoConfiguracion.UrlCargaGrid);
            NucleoConfigurado.Configuracion.UrlGeneraReporte(ObjetoConfiguracion.UrlGeneraReporte);
            NucleoConfigurado.Configuracion.UrlDetalle(ObjetoConfiguracion.UrlDetalle);
            NucleoConfigurado.Configuracion.FechaInicialLimite(ObjetoConfiguracion.FechaInicialLimite);
            return NucleoConfigurado;
        }
        return {
            Nucleo: _Nucleo,
            Constructor: _Constructor
        }
    })();
    window.Objeto = Venta;
})(window, document);