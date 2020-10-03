(function (windows, document) {
    let Agenda = (function () {
        let _Nucleo = function () {
            let Elementos_Agenda = {
                form: 'formAltaAgenda',
                Calendario: 'Calendario',
                AgregaEvento: 'AgregaEvento',
                FechaDeInicioDeEvento: 'fFechaDeInicioDeEvento',
                TodoElDia: 'fTodoElDia',
                FechaDeFinDeEvento: 'fFechaDeFinDeEvento',
                CveDeNotifica: 'fCveDeNotifica',
                Descripcion: 'fDescripcion'
            };
            let Configuracion = {
                Idioma: 'es',
                Tema: 'bootstrap',
                InitialView: 'dayGridMonth',
                WindowResize: true,
                HeaderToolbar: { left: 'prev,next today', center: 'title', right: 'dayGridMonth,timeGridWeek,timeGridDay' },
                FooterToolbar: { right: 'addEventButton' }
            };
            let URLAltaAgenda = '',
                URLGetAgenda = '',
                URLDeleteAgenda = '';
            let _URLAltaAgenda = function (URLAltaAgendaSet) {
                if (typeof (URLAltaAgendaSet) != 'undefined') {
                    URLAltaAgenda = URLAltaAgendaSet;
                } else {
                    return URLAltaAgenda;
                }
            },
            _URLGetAgenda = function (URLGetAgendaSet) {
                if (typeof (URLGetAgendaSet) != 'undefined') {
                    URLGetAgenda = URLGetAgendaSet;
                } else {
                    return URLGetAgenda;
                }
            },
            _URLDeleteAgenda = function (URLDeleteAgendaSet) {
                if (typeof (URLDeleteAgendaSet) != 'undefined') {
                    URLDeleteAgenda = URLDeleteAgendaSet;
                } else {
                    return URLDeleteAgenda;
                }
            };
            let _Inicio = function () {
                let calendarEl = document.getElementById(Elementos_Agenda.Calendario);
                let Calendario = new FullCalendar.Calendar(calendarEl, {
                    locale: Configuracion.Idioma,
                    themeSystem: Configuracion.Tema,
                    initialView: Configuracion.InitialView,
                    handleWindowResize: Configuracion.WindowResize,
                    headerToolbar: Configuracion.HeaderToolbar,
                    footerToolbar: Configuracion.FooterToolbar,
                    eventClick: function (calEvent) {
                        EventClick(calEvent);
                    },
                    customButtons: {
                        addEventButton: {
                            text: 'Agregar Evento',
                            click: function () {
                                AgregaEventoShowDialog(Calendario)
                            }
                        }
                    }
                });
                Calendario.render();
                AgregaEventosACalendario(Calendario);
            };
            let _ShowHideFechaFinDeEvento = function () {
                CambiaFecha();
            };
            function AgregaEventosACalendario(Calendario) {
                $.ajax({
                    type: "POST",
                    url: URLGetAgenda,
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var obj = $.parseJSON(data);
                        $(obj).each(function () {
                            Calendario.addEvent(this);
                        });
                    },
                    error: function () {
                        alertify.error("Ocurrio un error al realizar la accion Alta");
                    }
                });
            }
            function AgregaEventoShowDialog(Calendario) {
                CreaDialog(Elementos_Agenda.AgregaEvento, 'Agrega Evento', CreaBotonesDialog(Calendario), 500, 450);
                $('#' + Elementos_Agenda.AgregaEvento).dialog('open');
                $('.datetimepicker').datetimepicker();
                $('#' + Elementos_Agenda.FechaDeInicioDeEvento).blur();
                $('#' + Elementos_Agenda.AgregaEvento).on('dialogclose', function (event) {
                    $('#' + Elementos_Agenda.FechaDeInicioDeEvento).val('');
                    $('#' + Elementos_Agenda.FechaDeFinDeEvento).val('');
                    $('#' + Elementos_Agenda.TodoElDia).prop('checked', false);
                    CambiaFecha();
                    $('#' + Elementos_Agenda.CveDeNotifica).val('');
                    $('#' + Elementos_Agenda.Descripcion).val('');
                    $('.datetimepicker').datepicker("destroy");
                });
            }
            function CreaBotonesDialog(Calendario) {
                return {
                    'Cancelar': {
                        id: 'Cancelar',
                        text: 'Cancelar',
                        class: 'btn btn-danger',
                        click: function () {
                            $('#AgregaEvento').dialog('close');
                        }
                    },
                    'Guardar': {
                        id: 'Guardar',
                        text: 'Guardar',
                        class: 'btn btn-primary',
                        click: function () {
                            let Resultado = AltaAgenda();
                            if (Resultado.Resultado) {
                                if ($('#' + Elementos_Agenda.TodoElDia).is(':checked')) {
                                    Calendario.addEvent({
                                        id: Resultado.Adicional,
                                        title: $('#' + Elementos_Agenda.Descripcion).val(),
                                        start: $('#' + Elementos_Agenda.FechaDeInicioDeEvento).val(),
                                        allDay: true
                                    });
                                } else {
                                    Calendario.addEvent({
                                        id: Resultado.Adicional,
                                        title: $('#' + Elementos_Agenda.Descripcion).val(),
                                        start: $('#' + Elementos_Agenda.FechaDeInicioDeEvento).val(),
                                        end: $('#' + Elementos_Agenda.FechaDeFinDeEvento).val()
                                    });
                                }
                                $('#' + Elementos_Agenda.AgregaEvento).dialog('close');
                            }
                        }
                    }
                }
            }
            function CambiaFecha() {
                if ($('#' + Elementos_Agenda.TodoElDia).is(':checked')) {
                    $('#' + Elementos_Agenda.FechaDeFinDeEvento).parent().parent().hide();
                    $('#' + Elementos_Agenda.AgregaEvento).dialog({
                        height: 420
                    });
                } else {
                    $('#' + Elementos_Agenda.FechaDeFinDeEvento).parent().parent().show();
                    $('#' + Elementos_Agenda.AgregaEvento).dialog({
                        height: 450
                    });
                }
            }
            function EventClick(calEvent) {
                alertify.confirm('Eliminar Evento','¿Desea eleminar el Evento de la Agenda?', function () {
                    $.ajax({
                        type: "POST",
                        url: URLDeleteAgenda,
                        async: false,
                        data: { NumeroDeAgenda: calEvent.event.id },
                        success: function (data) {
                            if (data.Resultado) {
                                calEvent.event.remove();
                                alertify.success(data.Mensaje);
                            } else {
                                alertify.error(data.Mensaje);
                            }
                        },
                        error: function () {
                            alertify.error("Ocurrio un error al realizar la accion Baja");
                        }
                    });
                }, function () {

                });
            }
            function AltaAgenda() {
                let Resultado;
                let Registro = 'fFechaDeInicioDeEvento=' + $('#' + Elementos_Agenda.FechaDeInicioDeEvento).val() + '&';
                if ($('#' + Elementos_Agenda.TodoElDia).is(':checked')) {
                    Registro += 'fFechaDeFinDeEvento=' + $('#' + Elementos_Agenda.FechaDeInicioDeEvento).val() + '&';
                } else {
                    Registro += 'fFechaDeFinDeEvento=' + $('#' + Elementos_Agenda.FechaDeFinDeEvento).val() + '&';
                }
                Registro += 'fCveDeNotifica=' + $('#' + Elementos_Agenda.CveDeNotifica).val() + '&';
                Registro += 'fDescripcion=' + $('#' + Elementos_Agenda.Descripcion).val() + '&';
                $.ajax({
                    type: "POST",
                    url: URLAltaAgenda,
                    async: false,
                    data: Registro,
                    success: function (data) {
                        Resultado = data;
                        if (data.Resultado) {
                            alertify.success(data.Mensaje);
                        } else {
                            alertify.error(data.Mensaje);
                        }
                    },
                    error: function () {
                        alertify.error("Ocurrio un error al realizar la accion Alta");
                    }
                });
                return Resultado;
            }
            return {
                Configuracion: {
                    URLAltaAgenda: _URLAltaAgenda,
                    URLGetAgenda: _URLGetAgenda,
                    URLDeleteAgenda: _URLDeleteAgenda
                },
                Inicio: _Inicio,
                ShowHideFechaFinDeEvento: _ShowHideFechaFinDeEvento
            }
        }
        let _Constructor = function (ObjetoConfiguracion) {
            let NucleoConfigurado = _Nucleo();
            NucleoConfigurado.Configuracion.URLAltaAgenda(ObjetoConfiguracion.URLAltaAgenda);
            NucleoConfigurado.Configuracion.URLGetAgenda(ObjetoConfiguracion.URLGetAgenda);
            NucleoConfigurado.Configuracion.URLDeleteAgenda(ObjetoConfiguracion.URLDeleteAgenda);
            return NucleoConfigurado;
        }
        return {
            Nucleo: _Nucleo,
            Constructor: _Constructor
        }
    })();
    window.Objeto = Agenda;
})(window, document);