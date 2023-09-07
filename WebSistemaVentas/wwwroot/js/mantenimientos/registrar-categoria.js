const setCategoria = async (parametro) => {
    try {
        const responseRequest = await postAjax("/Mantenimientos/setCategoria", parametro);
        await messageToastUtil(responseRequest.responsemessage, responseRequest.messagetype);

    } catch (error) {
        await messageToastUtil(error, "ERROR");
    }
}

document.addEventListener("DOMContentLoaded", async function () {

    $("#btnguardar").click(async function () {
        let v_codigo = $("#txtcodigo").val();

        var parametro = {
            'p_opcion': "INSERT",
            'p_codigo': v_codigo,
            'p_nombre_categoria': $("#txtnombreCategoria").val(),
            'p_descripcion': $("#txtDescripcion").val(),
            'p_estado': document.getElementById('switchestado').checked,
            'p_id_usuario': 0
        };
        await setCategoria(parametro);
        location.href = '/Mantenimientos/ListarCategorias';
    });
});