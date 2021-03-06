const apiEnd = 'http://spotypie.pertrauktiestaskas.lt/api';
/*const apiEnd = 'https://localhost:9876/api';*/

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
    getSystemInfo,
    updateSongPlayCount,
    getLibraryInfo,
    uploadSong,
    getFileList,
    getPlaylists,
    getPlaylistSongs,
    getRecentAlbum,
    getMostPopularAlbum,
    getPopRecentSong
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

async function getSystemInfo() {
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

function getLibraryInfo() {
    const requestOptions = {
        method: 'GET'
    };
    return fetch(apiEnd + '/info/library', requestOptions)
        .then(handleResponse);
}

function getFileList() {
    const requestOptions = {
        method: 'GET'
    };
    return fetch(apiEnd + '/info/list', requestOptions)
        .then(handleResponse);
}

function updateSongPlayCount(id) {
    const requestOptions = {
        method: 'GET'
    };
    return fetch(apiEnd + '/Songs/' + id + "/Update", requestOptions)
        .then(handleResponse);
}

function getPlaylists(){
    const requestOptions = {
        method: 'GET'
    };
    return fetch(apiEnd + '/playlist/playlists', requestOptions)
        .then(handleResponse);
}

function getPlaylistSongs(id){

    const requestOptions = {
        method: 'GET'
    };
    return fetch(apiEnd + '/playlist/' + id + '/tracks', requestOptions)
        .then(handleResponse);
}

function uploadSong(data) {
    const requestOptions = {
        method: 'POST',
        /*        headers: { 'Content-Type': 'multipart/form-data'},*/
        body: data
    };

    return fetch(apiEnd + "/upload", requestOptions);
}

function getRecentAlbum(){
    const requestOptions = {
        method: 'GET'
    };
    return fetch(apiEnd + '/album/Recent', requestOptions)
        .then(handleResponse);
}

function getMostPopularAlbum(){
    const requestOptions = {
        method: 'GET'
    };
    return fetch(apiEnd + '/album/Popular', requestOptions)
        .then(handleResponse);
}

function getPopRecentSong(){
    const requestOptions = {
        method: 'GET'
    };
    return fetch(apiEnd + '/Songs/PopularRecent', requestOptions)
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