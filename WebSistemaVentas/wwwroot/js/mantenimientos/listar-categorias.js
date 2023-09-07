var tblElementos;
const setCategoria = async (parametro) => {
    try {
        const responseRequest = await postAjax("/Mantenimientos/setCategoria", parametro);
        await messageToastUtil(responseRequest.responsemessage, responseRequest.messagetype);
        await listarCategoriasAsync();
    } catch (error) {
        await messageToastUtil(error, "ERROR");
    }
}
var listarCategoriasAsync = async function () {

    let column_array = [
        {value: "codigo", text: "COD.", visible: true},
        {value: "nombre", text: "Nombre", visible: true},
        {value: "descripcion", text: "Descripción", visible: true},
        {value: "estado", text: "Estado", visible: true},
        {value: "auditoria_creacion", text: "Creado", visible: true}
    ];

    let type_documents_array = [];
    let array_type_documents = [];

    var response = await getAjax("/Mantenimientos/GetCategorias");
    if (response.statusprocess) {
        type_documents_array = response.responsevalue;

        for (var i = 0; i < type_documents_array.length; i++) {
            array_type_documents.push({
                codigo: type_documents_array[i].codigo,
                nombre: type_documents_array[i].nombre,
                descripcion: type_documents_array[i].descripcion,
                estado: type_documents_array[i].estado,
                auditoria_creacion: type_documents_array[i].auditoria_creacion
            });
        }

        let parametrosTabla = {
            columnas_array: column_array,
            filas_array: array_type_documents,
            contenedor_padre: "tabla-elementos",
            tabla_id_cabecera: "tblhead-elementos",
            tabla_id_cuerpo: "tblbody-elementos",
            tabla_seleccionar_fila: false,
            boton_editar: true,
            boton_eliminar: true
        }

        tblDocumentos = construirTablaPhoenix1(parametrosTabla);
    }
}
const listar = async function () {
    const existingTable = document.getElementById('tabla');
    const arrayColumns = ['Código', 'Categoria', 'Descripción', 'Estado', 'Creado', 'Operación'];
    const arrayValues = ['codigo', 'nombre', 'descripcion', 'estado', 'auditoria_creacion'];
    existingTable.innerHTML = '';
    await buildTableBootstrap1("/Mantenimientos/getCategorias", arrayColumns, arrayValues, true, true, true, "/Mantenimientos/RegistrarCategoria");
};

document.addEventListener("DOMContentLoaded", async function () {
    await listarCategoriasAsync();
    
});

async function editarRegistroTabla(id) {
    const data = await getAjax(`/Mantenimientos/GetCategoriaByCodigo?codigo=${id}`);
    document.getElementById("lbl-id-cod").textContent = "COD.";
    document.getElementById("lbl-nombre-element").textContent = "Categoría";
    document.getElementById("lbl-texto-1").textContent = "Descripción";
    const checkboxElement = document.getElementById("switchestado");
    const txtIdElement = document.getElementById("id-or-cod");
    const txtNombreElement = document.getElementById("nombre-categoria");
    const txtAd = document.getElementById("texto-ad-1");

    txtIdElement.value = data.responsevalue.codigo;
    txtNombreElement.value = data.responsevalue.nombre;
    txtAd.value = data.responsevalue.descripcion;
    checkboxElement.checked = data.responsevalue.estado ? true : false;
    console.log(data.responsevalue);
}

document.getElementById("btnGuardarElement").addEventListener("click", async function () {
    let v_codigo = $("#id-or-cod").val();

    var parametro = {
        'p_opcion': "UPDATE",
        'p_codigo': v_codigo,
        'p_nombre_categoria': $("#nombre-categoria").val(),
        'p_descripcion': $("#texto-ad-1").val(),
        'p_estado': document.getElementById('switchestado').checked,
        'p_id_usuario': 0
    };
    await setCategoria(parametro);
    $('#edit-modal').modal('hide');
});

async function openWarningModal(id) {
    Swal.fire({
        title: '¿Está seguro de eliminar la categoría?',
        text: "¡Este cambio no se podrá revertir!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Eliminar!',
        cancelButtonText: 'Cancelar'
    }).then(async (result) => {
        if (result.isConfirmed) {
            var parametro = {
                'p_opcion': "DELETE",
                'p_codigo': id
            }
            await setCategoria(parametro);
            Swal.fire(
                'Eliminado!',
                'El registro ha sido eliminado.',
                'success'
            )
        }
    })
}

// function editarRegistroTabla(id) {
//     location.href = "/Mantenimientos/RegistrarCategoria?id=" + id;
// }
async function eliminarRegistroTabla(id) {
    var parametro = {
        'p_opcion': "DELETE",
        'p_codigo': id
    };

    var responseDialog = await swalDialog('¿Eliminar La Categoría?', 'El registro se eliminará permanente');

    if (responseDialog) {
        await setCategoria(parametro);
        await listarCategoriasAsync();
    }
}