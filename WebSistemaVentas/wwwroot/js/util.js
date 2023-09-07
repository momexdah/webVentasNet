iziToast.settings({
    position: 'bottomRight', // Posición de las notificaciones
    timeout: 3000, // Duración en milisegundos
    progressBar: true, // Muestra la barra de progreso
    pauseOnHover: true, // Pausa al pasar el mouse
    // ... otras opciones
});

async function messageToastUtil(textmessage, typemessage) {
    let icon = '';
    let backgroundColor = '';
    let progressBarColor = '';
    let transitionIn = '';
    let timeout = 5000;

    switch (typemessage) {
        case 'success':
            icon = 'fa fa-check-circle';
            backgroundColor = '#28a745';
            progressBarColor = '#ffffff';
            transitionIn = 'fadeInRight';
            break;
        case 'WARNING':
            icon = 'fa fa-exclamation-triangle';
            backgroundColor = '#ffc107';
            progressBarColor = '#ffffff';
            transitionIn = 'fadeInDown';
            break;
        case 'ERROR':
            icon = 'fa fa-times-circle';
            backgroundColor = '#dc3545';
            progressBarColor = '#ffffff';
            transitionIn = 'fadeInUp';
            break;
        default:
            icon = 'fa fa-info-circle';
            backgroundColor = '#007bff';
            progressBarColor = '#ffffff';
            transitionIn = 'fadeInUp';
            break;
    }

    iziToast.show({
        title: textmessage,
        message: typemessage,
        position: 'topRight',
        theme: 'dark',
        icon: icon,
        iconColor: '#ffffff',
        backgroundColor: backgroundColor,
        progressBarColor: progressBarColor,
        transitionIn: transitionIn,
        timeout: timeout
    });
}


function createEditButton(id) {
    const button = document.createElement('button');
    button.type = 'button';
    button.className = 'btn btn-info btn-sm text-light';
    button.style.marginLeft = '4px';
    button.dataset.toggle = 'modal';
    button.dataset.target = '#modalcrud';
    //button.innerHTML = '<i class="fa fa-edit"></i>';
    button.setAttribute("data-bs-toggle", "modal");
    button.setAttribute("data-bs-target", "#edit-modal");
    button.textContent = "Editar"
    button.onclick = () => editarElemento(id);
    return button;
}

function createDeleteButton(id) {
    const button = document.createElement('button');
    button.type = 'button';
    button.className = 'btn btn-danger btn-sm';
    button.style.marginLeft = '4px';
    //button.innerHTML = '<i class="fa fa-trash"></i>';
    button.textContent = "Eliminar"
    button.onclick = () => openWarningModal(id);
    return button;
}

async function buildTableBootstrap1(url, arrayColumns, arrayValues, btnAdd, btnEdit, btnDelete, urlRegiter) {
    const response = await fetch(url, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    });
    const dataList = await response.json();

    const table = document.createElement('table');
    table.className = 'table table-responsive table-hover table-sm fs--1 mb-0 overflow-hidden';
    table.style.width = '100%';

    const thead = document.createElement('thead');
    const theadRow = document.createElement('tr');

    for (const value of arrayColumns) {
        const th = document.createElement('th');
        th.className = 'sort ps-3 pe-1 align-middle white-space-nowrap';
        th.dataset.sort = 'id';
        th.style.minWidth = '4.5rem';
        th.style.textAlign = 'center';
        th.textContent = value;
        theadRow.appendChild(th);
    }

    thead.appendChild(theadRow);
    table.appendChild(thead);

    const tbody = document.createElement('tbody');
    tbody.className = 'list';

    for (const item of dataList.responsevalue) {
        const tr = document.createElement('tr');

        for (const field of arrayValues) {
            const td = document.createElement('td');
            td.style.textAlign = 'center';

            if (field === 'nombre') {
                td.classList.add('nombre');
            }

            if (field.toUpperCase() === 'ESTADO' && typeof item[field] === 'boolean') {
                const icon = document.createElement('span');
                const iconClass = item[field] ? 'fa fa-check text-success d-flex' : 'fa fa-regular fa-ban text-danger d-flex';
                icon.textContent = item[field] ? " Activo" : " Inactivo";
                icon.className = iconClass;
                td.appendChild(icon);
            } else {
                td.textContent = item[field];
            }

            tr.appendChild(td);
        }

        const tdButtons = document.createElement('td');
        tdButtons.className = 'd-flex';
        //tdButtons.style.textAlign = 'center';

        if (!item.flg_bloqueado) {
            if (btnEdit) {
                const editButton = createEditButton(item[arrayValues[0]]);
                tdButtons.appendChild(editButton);
            }
            if (btnDelete) {
                const deleteButton = createDeleteButton(item[arrayValues[0]]);
                tdButtons.appendChild(deleteButton);
            }
        }
        tr.appendChild(tdButtons);
        tbody.appendChild(tr);
    }

    table.appendChild(tbody);
    document.getElementById('tabla').appendChild(table);
}

function construirTablaPhoenix1(parametro) {

    let header = "";
    let row = "";
    let columns_values_array = [];
    let first_column = "";
    var tblList;

    if (parametro.filas_array != null) {
        if (parametro.filas_array.length > 0) {

            header = "<tr>";
            for (var i = 0; i < parametro.columnas_array.length; i++) {
                columns_values_array.push(parametro.columnas_array[i].value);
                let valor_texto = parametro.columnas_array[i].text;
                if (parametro.columnas_array[i].visible) {
                    header += "<th class='sort border-top ps-3' data-sort='" + parametro.columnas_array[i].value + "'>" + valor_texto.toUpperCase() + "</th>";
                } else {
                    header += "<th class='sort border-top ps-3' data-sort='" + parametro.columnas_array[i].value + "' style='display:none;'>" + valor_texto.toUpperCase() + "</th>";
                }
            }
            if (parametro.boton_editar || parametro.boton_eliminar) {
                header += "<th class='sort text-end align-middle pe-0 border-top' scope='col'>ACCION</th>";
            }
            header += "</tr>";
            document.getElementById(parametro.tabla_id_cabecera).innerHTML = header;

            for (var i = 0; i < parametro.filas_array.length; i++) {
                first_column = parametro.columnas_array[0].value;

                if (parametro.tabla_seleccionar_fila) {
                    row += "<tr class='fila-seleccionable'>";
                } else {
                    row += "<tr>";
                }

                for (var j = 0; j < parametro.columnas_array.length; j++) {
                    let valor_texto = "" + parametro.filas_array[i][parametro.columnas_array[j].value];
                    let nombre_columna = parametro.columnas_array[j].value;
                    if (parametro.columnas_array[j].visible) {
                        if (nombre_columna.toUpperCase() == "ESTADO" && (valor_texto.toUpperCase() == "TRUE" || valor_texto.toUpperCase() == "FALSE")) {
                            row += "<td class='align-middle ps-3 " + nombre_columna + "'>" +
                                "<span class='badge badge-phoenix fs--2 " + (valor_texto = "TRUE" ? "badge-phoenix-success" : "badge-phoenix-secondary") + "'>" +
                                (valor_texto = "TRUE" ? "ACTIVO" : "INACTIVO") +
                                "</span>" +
                                "</td>";
                        } else {
                            row += "<td class='align-middle ps-3 " + parametro.columnas_array[j].value + "'>" + valor_texto.toUpperCase() + "</td>";
                        }

                    } else {
                        row += "<td class='align-middle ps-3 " + parametro.columnas_array[j].value + "' style='display:none;'>" + valor_texto.toUpperCase() + "</td>";
                    }
                }

                if (parametro.boton_editar || parametro.boton_eliminar) {
                    row += "<td class='align-middle white-space-nowrap text-end pe-0'>" +
                        "<div class='font-sans-serif btn-reveal-trigger position-static'>" +
                        "<button class='btn btn-sm dropdown-toggle dropdown-caret-none transition-none btn-reveal fs--2' type='button' data-bs-toggle='dropdown' data-boundary='window' aria-haspopup='true' aria-expanded='false' data-bs-reference='parent'><span class='fas fa-ellipsis-h fs--2'></span></button>" +
                        "<div class='dropdown-menu dropdown-menu-end py-2'>";

                    // if (parametro.boton_editar) {
                    //     row += "<a class='dropdown-item' onclick='editarRegistroTabla(" + parametro.filas_array[i][first_column] + ")'>Editar</a>";
                    //     row += "<div class='dropdown-divider'></div>";
                    // }
                    if (parametro.boton_editar) {
                        var valor = parametro.filas_array[i][first_column];
                        var argumento = JSON.stringify(valor); // Convierte cualquier tipo de dato en una cadena JSON.
                        row += "<a class='dropdown-item' data-bs-toggle='modal' data-bs-target='#edit-modal' onclick='editarRegistroTabla(" + argumento + ")'>Editar</a>";
                        row += "<div class='dropdown-divider'></div>";

                    }

                    // if (parametro.boton_eliminar) {
                    //     row += "<a class='dropdown-item text-danger' onclick='eliminarRegistroTabla(" + parametro.filas_array[i][first_column] + ")'>Eliminar</a>";
                    // }
                    if (parametro.boton_eliminar) {
                        var valorEliminar = parametro.filas_array[i][first_column];
                        var argumentoEliminar = JSON.stringify(valorEliminar); // Convierte cualquier tipo de dato en una cadena JSON.
                        row += "<a class='dropdown-item text-danger' data-toggle='modal' data-target='#delete-modal' onclick='eliminarRegistroTabla(" + argumentoEliminar + ")'>Eliminar</a>";
                    }

                    row += "</div></div></td >";
                }
                row += "</tr>";
            }
            document.getElementById(parametro.tabla_id_cuerpo).innerHTML = row;

            let optionsTable = {
                "valueNames": columns_values_array,
                "page": 10,
                "pagination": true
            };

            const contenedor = document.getElementById(parametro.contenedor_padre);
            contenedor.setAttribute("data-list", JSON.stringify(optionsTable));
            tblList = new List(parametro.contenedor_padre, optionsTable);

            if (parametro.tabla_seleccionar_fila) {
                var row_first = $("#" + parametro.tabla_id_cuerpo + "").children('tr:first');
                $(row_first).addClass('selected').siblings().removeClass('selected');
                $(row_first).focus();
            }

        }
    }

    return tblList;
}

async function swalDialog(titulo, texto) {
    try {
        const alert = await Swal.fire({
            title: titulo,
            text: texto,
            icon: 'warning',
            showCancelButton: true,
            cancelButtonText: 'No, Cancelar',
            confirmButtonText: 'Si',
            confirmButtonColor: "#028824",
            customClass: {
                cancelButton: 'btn btn-danger',
                confirmButton: 'btn btn-success'
            },
        });
        return !!(alert.value && alert.value === true);

    } catch (e) {
        console.log('error:', e);
        return false;
    }
}



