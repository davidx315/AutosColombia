function MostrarTablaConsultaVehiculo() {

    $('#tblConsultaVehiculo').DataTable({
        "bDestroy": true,
        "processing": true,
        "serverSide": true,
        "language": {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar MENU registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del START al END de un total de TOTAL registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de MAX registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            },
            buttons: {
                copyTitle: 'Copiado al portapapeles',
                copySuccess: {
                    _: 'Copiados %d items al portapapeles',
                    1: 'Copiados 1 item al portapapeles'
                }
            }
        },
        search: {
            return: true,
        },
        "ajax": {
            type: "POST",
            url: "/Vehiculos/GeneraDatosTablaConsultasVehiculos",
            contentType: 'application/json',
            dataType: "json",
            data: function (d) {

                var dataObjt = {
                    Placa: $("#PlacaVehiculo").val(),
                    NumeroDocumento: $("#NumeroDocumento").val(),
                }
                d["customFilter"] = JSON.stringify(dataObjt);
                return JSON.stringify(d);
            }
        },
        columns: [
            {
                data: 'Modelo_Vehiculo',
                title: 'Modelo Vehiculo',
                sortable: true
            },
            {
                data: 'Placa',
                title: 'Placa',
                sortable: true
            },
            {
                data: 'Documento_Usuario',
                title: 'Documento Usuario',
                sortable: false
            },
            {
                data: 'NumeroCelda',
                title: 'Numero Celda',
                sortable: false
            },
            {
                title: 'Editar Reintento',
                render:
                    function (data, type, row, meta) {
                        return '<button class="fa fa-edit" style="color:#F26821; font-size:14pt; background-color:transparent;" onclick="CargarModalEditarReintentosBiometria(\'' + row.Documento_Usuario + '\')"></button>'
                    },
                sortable: false
            }
        ]
    });

}

function CargarModalEditarReintentosBiometria(numeroDocumento) {

    $.ajax({
        url: "/Vehiculos/EditarVehiculo",
        dataType: "html",
        type: "GET",
        data: { numeroDocumento: numeroDocumento },
        async: false, // La petición es síncrona
        cache: false, // No queremos usar la caché del navegador
        success: function (data) {

            $("#TituloFormularioModal").empty();
            $("#TituloFormularioModal").append("Editar Reintentos Biometria");
            $('#FormModal').html("");
            $('#FormModal').html(data);
            $("#FormularioModal").modal({ backdrop: 'static', keyboard: false });
        },
        error: function (data) {
            showMessage("Ha ocurrido un error en la aplicación, por favor intentelo nuevamente", "Error");
        }
    });
}

function LimpiarDatosFiltroReintentoBiometria() {

    $("#TipoDocumento").val("");
    $("#NumeroDocumento").val("");
}

function EditarVehiculo() {
    var dataString = new FormData();

    var Tipo_Documento = $("#Tipo_Documento").val()
    var Numero_Identificacion = $("#Numero_Identificacion").val()
    var ID_transaccion = $("#ID_transaccion").val()
    var Cant_Intentos = $("#Cant_Intentos").val()

    dataString.append("Tipo_Documento", Tipo_Documento);
    dataString.append("Numero_Identificacion", Numero_Identificacion);
    dataString.append("ID_transaccion", ID_transaccion);
    dataString.append("Cant_Intentos", Cant_Intentos);

    $.ajax({
        url: "/Biometria/EditarVehiculo",
        type: "POST",
        contentType: false,
        processData: false,
        data: dataString,
        success: function (data) {

            showMessage(data.Codigo + " - " + data.Mensaje, data.Tipo, function () { location.href = location.href });
        },
        error: function (data) {
            showMessage("Ha ocurrido un error en la aplicación, por favor intentelo nuevamente", "Error");
        }
    });
}