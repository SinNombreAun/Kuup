﻿(function (windows, document) {
    let Marca = (function () {
        let _Nucleo = function () {
            let Elementos_Marca = {
                NumeroDeMarca: "fNumeroDeMarca",
                NombreDeMarca: "fNombreDeMarca",
                CveDeEstatus: "fCveDeEstatus",
                AsignaMarcas: "fAsignaMarcas",
                Guardar: "Guardar",
                Tabla: "Tabla",
                TablaFilter: "Tabla_filter",
                Cancelar: "Cancelar",
                Edita: "Edita",
                SelectTablas: ["TextoDeEstatus"]
            };
            let Funcionalidad = "",
                UrlCargaGrid = "",
                UrlDetalle = "",
                UrlDeBaja = "",
                CombosParaTabla = [];
            let _Funcionalidad = function (FuncionalidadSet) {
                if (typeof FuncionalidadSet != "undefined") {
                    Funcionalidad = FuncionalidadSet;
                } else {
                    return Funcionalidad;
                }
            };
            let _UrlCargaGrid = function (UrlCargaGridSet) {
                if (typeof UrlCargaGridSet != "undefined") {
                    UrlCargaGrid = UrlCargaGridSet;
                } else {
                    return UrlCargaGrid;
                }
            };
            let _UrlDetalle = function (UrlDetalleSet) {
                if (typeof UrlDetalleSet != "undefined") {
                    UrlDetalle = UrlDetalleSet;
                } else {
                    return UrlDetalle;
                }
            };
            let _UrlDeBaja = function (UrlDeBajaSet) {
                if (typeof (UrlDeBajaSet) != "undefined") {
                    UrlDeBaja = UrlDeBajaSet;
                } else {
                    return UrlDeBaja;
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
                if (Funcionalidad == "INDEX") {
                    GeneraGridDeMarca();
                }
            };
            function GeneraGridDeMarca() {
                $("#" + Elementos_Marca.Tabla + " thead tr").clone(true).appendTo("#" + Elementos_Marca.Tabla + " thead");
                $("#" + Elementos_Marca.Tabla + " thead tr:eq(1) th").each(function (i) {
                    if (this.id != "boton") {
                        if (Elementos_Marca.SelectTablas.indexOf(this.id) > -1) {
                            var Elemento = CombosParaTabla[Elementos_Marca.SelectTablas.indexOf(this.id)];
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
                var table = $("#" + Elementos_Marca.Tabla).DataTable({
                    "processing": true,
                    "serverSide": true,
                    "ajax": {
                        "type": "POST",
                        "url": UrlCargaGrid,
                        "async": false,
                        "datatype": "json",
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
                    "fixedColumns": {
                        "leftColumns": 1,
                        "rightColumns": 1,
                    },
                    "columnDefs": [{ "targets": [0], "visible": true }],
                    "data": null,
                    "order": [[2, "asc"]],
                    "searching": true,
                    "columns": [
                        { "data": "NumeroDeMarca" },
                        { "data": "NombreDeMarca" },
                        { "data": "TextoDeEstatus" },
                        { "defaultContent": "<button type='button' class='detalle btn btn-info'>Detalle</button>" }
                    ],
                    "language": LenguajeEN(),
                });
                table.draw();
                $('#' + Elementos_Marca.TablaFilter).hide();
                DetalleRegistro("#" + Elementos_Marca.Tabla + " tbody", table);
            }
            var DetalleRegistro = function (tbody, table) {
                $(tbody).on("click", "button.detalle", function () {
                    var data = table.row($(this).parents("tr")).data();
                    if (typeof data == "undefined") {
                        data = table.row($(this).parents("li")).data();
                    }
                    window.location.href = UrlDetalle + "?NumeroDeElemento=" + data.NumeroDeMarca;
                });
            };
            function OcultaCampos() {
                switch (Funcionalidad) {
                    case "ALTA":
                        break;
                    case "EDITA":
                        break;
                    case "IMPORTAR":
                        break;
                }
            }
            function EjecutaEventosIniciales() {
                switch (Funcionalidad) {
                    case "ALTA":
                        break;
                    case "EDITA":
                        break;
                    case "DETALLE":
                        break;
                }
            }
            function AgregaEvento() {
                switch (Funcionalidad) {
                    case "ALTA":
                        $('#' + Elementos_Marca.Guardar).click(function () {
                            alertify.confirm("Asignar Marca", "Es necesario asignar la Marca a un tipo de producto, ¿Deseas asignar la Marca ahora?, si lo deseas podrás hacer esta asignación desde la pantalla Asigna Marcas o Tipo de Producto",
                                function () {
                                    $("#" + Elementos_Marca.AsignaMarcas).val(1);
                                    $("form").submit();
                                },
                                function () {
                                    $("#" + Elementos_Marca.AsignaMarcas).val(2);
                                    $("form").submit();
                                    alertify.warning("Proceso de Alta sin asignacion de tipo de producto podras hacerlo desde la pantalla de Asigna Marcas o Tipo de Producto");
                                }
                            ).set("labels", { ok: "Si, Asignar Marca", cancel: "No, Asignar Marca" });
                        });
                        break;
                    case "EDITA":
                        $('#' + Elementos_Marca.Guardar).click(function () {
                            $("form").submit();
                        });
                        break;
                    case "DETALLE":
                        $("#" + Elementos_Marca.Cancelar).click(function () {
                            $.ajax({
                                type: "POST",
                                url: UrlDeBaja,
                                data: { NumeroDeMarca: $("#" + Elementos_Marca.NumeroDeMarca).val() },
                                success: function (data) {
                                    if (data.Resultado) {
                                        alertify.success(data.Mensaje);
                                    } else {
                                        alertify.error(data.Mensaje);
                                    }
                                },
                                error: function () {
                                    alertify.error(
                                        "Ocurrio un error al realizar la accion de Cancelar"
                                    );
                                },
                            });
                        });
                        break;
                }
            }
            return {
                Configuracion: {
                    Funcionalidad: _Funcionalidad,
                    UrlCargaGrid: _UrlCargaGrid,
                    UrlDetalle: _UrlDetalle,
                    UrlDeBaja: _UrlDeBaja,
                    CombosParaTabla: _CombosParaTabla
                },
                Inicio: _Inicio
            };
        };
        let _Constructor = function (ObjetoConfiguracion) {
            let NucleoConfigurado = _Nucleo();
            NucleoConfigurado.Configuracion.Funcionalidad(ObjetoConfiguracion.Funcionalidad);
            NucleoConfigurado.Configuracion.UrlCargaGrid(ObjetoConfiguracion.UrlCargaGrid);
            NucleoConfigurado.Configuracion.UrlDetalle(ObjetoConfiguracion.UrlDetalle);
            NucleoConfigurado.Configuracion.UrlDeBaja(ObjetoConfiguracion.UrlDeBaja);
            NucleoConfigurado.Configuracion.CombosParaTabla(ObjetoConfiguracion.CombosParaTabla);
            return NucleoConfigurado;
        };
        return {
            Nucleo: _Nucleo,
            Constructor: _Constructor,
        };
    })();
    window.Objeto = Marca;
})(window, document);