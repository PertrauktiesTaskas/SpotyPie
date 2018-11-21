const apiEnd = 'https://localhost:9876/api';

export const itemService = {
    getAlbums,
    getArtists,
    getSongs
};

function getAlbums() {
    const requestOptions = {
        method: 'GET'
    };
    return fetch(apiEnd + '/album/Albums', requestOptions)
        .then(handleResponse);
}

function getArtists(){
    const requestOptions = {
        method: 'GET'
    };
    return fetch(apiEnd + '/Artists', requestOptions)
        .then(handleResponse);
}

function getSongs(){
    const requestOptions = {
        method: 'GET'
    };
    return fetch(apiEnd + '/Songs', requestOptions)
        .then(handleResponse);
}

function handleResponse(response) {
    return response.text().then(data => {
            if (response.ok) {
                let parsed = data && JSON.parse(data);
                return parsed
            }
            return undefined;
        },
        error => {
            console.log(error, 'handleResponse error');
            return error;
        });
}