const apiEnd = 'http://spotypie.deveim.com/api';

export const itemService = {
    getAlbums,
    getAlbumSongs,
    getArtists,
    getArtistInfo,
    getArtistTopTracks,
    getRelatedArtists,
    getArtistAlbums,
    getSongs,
    getSong,
    getSongAlbum,
    getSystemInfo
};

async function getAlbums() {
    const requestOptions = {
        method: 'GET'
    };

    let result = await fetch(apiEnd + '/album/Albums', requestOptions);

    return result.text().then(data => {
            if (result.ok) {
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

function getAlbumSongs(id) {
    const requestOptions = {
        method: 'GET'
    };
    return fetch(apiEnd + '/album/' + id + '/tracks', requestOptions)
        .then(handleResponse);
}

function getArtists() {
    const requestOptions = {
        method: 'GET'
    };
    return fetch(apiEnd + '/artist/Artists', requestOptions)
        .then(handleResponse);
}

async function getArtistInfo(id) {
    const requestOptions = {
        method: 'GET'
    };

    let result = await fetch(apiEnd + '/artist/' + id, requestOptions);

    return result.text().then(data => {
            if (result.ok) {
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

async function getArtistTopTracks(id) {
    const requestOptions = {
        method: 'GET'
    };

    let result = await fetch(apiEnd + '/artist/' + id + "/top-tracks", requestOptions);
    return result.text().then(data => {
            if (result.ok) {
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

async function getRelatedArtists(id) {
    const requestOptions = {
        method: 'GET'
    };

    let result = await fetch(apiEnd + '/artist/Related/' + id, requestOptions)

    return result.text().then(data => {
            if (result.ok) {
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

async function getArtistAlbums(id) {
    const requestOptions = {
        method: 'GET'
    };

    let result = await fetch(apiEnd + '/artist/' + id + "/albums", requestOptions);

    return result.text().then(data => {
            if (result.ok) {
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

function getSongs() {
    const requestOptions = {
        method: 'GET'
    };
    return fetch(apiEnd + '/Songs', requestOptions)
        .then(handleResponse);
}

function getSong(id) {
    const requestOptions = {
        method: 'GET'
    };
    return fetch(apiEnd + '/Songs/' + id, requestOptions)
        .then(handleResponse);
}

async function getSongAlbum(id) {
    const requestOptions = {
        method: 'GET'
    };

    let result = await fetch(apiEnd + '/Songs/GetSongAlbum/' + id, requestOptions);

    return result.text().then(data => {
            if (result.ok) {
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

async function getSystemInfo(){
    const requestOptions = {
        method: 'GET'
    };
    let result = await fetch(apiEnd + '/info', requestOptions);

    return result.text().then(data => {
            if (result.ok) {
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