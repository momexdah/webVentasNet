async function postAjax(url, param) {
    let result;

    try {
        result = await $.ajax({
            url: url,
            type: 'POST',
            data: param
        });
        return result;
    } catch (error) {
        console.error(error);
        throw error;
    }
}

async function getAjax(url){
    let result;
    
    try {
        result = await $.ajax({
            url: url,
            type: 'GET'
        });
        return result;
    }catch (error){
        console.error(error);
        throw error;
    }
}