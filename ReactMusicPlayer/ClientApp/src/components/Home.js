import React from 'react';
import '../css/main_styles.css';
import '../css/responsive.css';
import HeaderBar from "./Header";
import MainContent from "./MainContent";
import SideMenuPanel2 from "./RightSideMenu";
import MusicPlayer from "./Player";
import SongList from "./SongList";
import AlbumList from "./AlbumList";
import ArtistList from "./ArtistList";
import FilesList from "./FilesList";
import NewPlaylist from "./NewPlaylist";
import Dashboard from "./Dashboard";
import UploadSong from "./UploadSong";
import {itemService} from "../Service";
import DefaultAlbum from '../img/default-album-artwork.png';

class HomePage extends React.Component {
    constructor(props) {
        super(props);
        console.log(props);

        this.state = {
            show_main_page: true,
            show_song_list: false,
            show_album_list: false,
            show_artist_list: false,
            show_files_list: false,
            show_playlist_add: false,
            show_dashboard: false,
            upload_song: false,
            playing_song_id: "",
            playing_song: "",
            playing_song_album: "",
            show_dashboard_btn: false
        };

        this.handleClick = this.handleClick.bind(this);
        this.handlePlay = this.handlePlay.bind(this);
    }

    componentDidMount() {
        this.updateDimensions();
        window.addEventListener("resize", this.updateDimensions.bind(this));
    }

    updateDimensions() {
        if (window.innerWidth <= 1400) {
            this.setState({show_dashboard_btn: true});
        } else {
            this.setState({show_dashboard_btn: false});
        }
    }

    handleClick(event) {
        console.log("Menu clicked", event.target.id);

        switch (event.target.id) {
            case "home":
                this.setState({
                    show_main_page: true,
                    show_song_list: false,
                    show_album_list: false,
                    show_artist_list: false,
                    show_files_list: false,
                    show_playlist_add: false,
                    show_dashboard: false,
                    upload_song: false
                });
                break;
            case "songs":
                this.setState({
                    show_main_page: false,
                    show_song_list: true,
                    show_album_list: false,
                    show_artist_list: false,
                    show_files_list: false,
                    show_playlist_add: false,
                    show_dashboard: false,
                    upload_song: false
                });
                break;
            case "albums":
                this.setState({
                    show_main_page: false,
                    show_song_list: false,
                    show_album_list: true,
                    show_artist_list: false,
                    show_files_list: false,
                    show_playlist_add: false,
                    show_dashboard: false,
                    upload_song: false
                });
                break;
            case "artists":
                this.setState({
                    show_main_page: false,
                    show_song_list: false,
                    show_album_list: false,
                    show_artist_list: true,
                    show_files_list: false,
                    show_playlist_add: false,
                    show_dashboard: false,
                    upload_song: false
                });
                break;
            case "files":
                this.setState({
                    show_main_page: false,
                    show_song_list: false,
                    show_album_list: false,
                    show_artist_list: false,
                    show_files_list: true,
                    show_playlist_add: false,
                    show_dashboard: false,
                    upload_song: false
                });
                break;
            case "playlist":
                this.setState({
                    show_main_page: false,
                    show_song_list: false,
                    show_album_list: false,
                    show_artist_list: false,
                    show_files_list: false,
                    show_playlist_add: true,
                    show_dashboard: false,
                    upload_song: false
                });
                break;
            case "dashboard":
                this.setState({
                    show_main_page: false,
                    show_song_list: false,
                    show_album_list: false,
                    show_artist_list: false,
                    show_files_list: false,
                    show_playlist_add: false,
                    show_dashboard: true,
                    upload_song: false
                });
                break;
            case "upload":
                this.setState({
                    show_main_page: false,
                    show_song_list: false,
                    show_album_list: false,
                    show_artist_list: false,
                    show_files_list: false,
                    show_playlist_add: false,
                    show_dashboard: false,
                    upload_song: true
                });
                break;
            default:
                this.setState({
                    show_main_page: true,
                    show_song_list: false,
                    show_album_list: false,
                    show_artist_list: false,
                    show_files_list: false,
                    show_playlist_add: false,
                    show_dashboard: false,
                    upload_song: false
                });
        }
    }

    handlePlay(event) {

        let id = parseInt(event.target.id) + 1;

        this.setState({playing_song_id: id});

        console.log("Playing song id", id);

        if (id != null) {
            itemService.getSong(id).then((data) => {
                console.log("Playing song info:", data);
                this.setState({playing_song: data});
            });

            itemService.getSongAlbum(id).then((data) => {
                console.log("Playing song album:", data);
                this.setState({playing_song_album: data});
            })
        }
    }


    render() {
        let show_window = function (props, func) {
            console.log("Props", props);
            if (props.show_main_page) {
                return <MainContent/>;
            }
            else if (props.show_song_list) {
                return <SongList props={func}/>;
            }
            else if (props.show_album_list) {
                return <AlbumList props={func}/>;
            }
            else if (props.show_artist_list) {
                return <ArtistList props={func}/>;
            }
            else if (props.show_files_list) {
                return <FilesList/>;
            }
            else if (props.show_playlist_add) {
                return <NewPlaylist/>;
            }
            else if (props.show_dashboard) {
                return <Dashboard/>;
            }
            else if (props.upload_song) {
                return <UploadSong/>;
            }
        };

        function PlayingSongInfo(props) {

            let display_album_art = props.props2.images != null ? props.props2.images[0].url : {DefaultAlbum};

            if (props.props) {
                return (<section className="playing">
                    <div className="playing__art">
                        <img src={display_album_art} alt=""/>
                    </div>
                    <div className="playing__song">
                        <span className="playing__song__name"><i className="fas fa-music"/> {props.props.name}</span>
                        <span className="playing__song__artist"><i
                            className="fas fa-user"/> {JSON.parse(props.props.artists)[0].Name}</span>
                        <span className="playing__song__album"><i className="fas fa-compact-disc"/> {props.props2.name}</span>
                    </div>

                </section>);
            }
            else {
                return (<section className="playing">
                    <div className="playing__art">
                        <img src={DefaultAlbum} alt=""/>
                    </div>
                    <div className="playing__song">
                        <span className="playing__song__name"><i className="fas fa-music"/> </span>
                        <span className="playing__song__artist"><i className="fas fa-user"/> </span>
                        <span className="playing__song__album"><i className="fas fa-compact-disc"/> </span>
                    </div>

                </section>);
            }
        }

        let show_dash_btn = this.state.show_dashboard_btn ?
            <a href="#" className="navigation__list__item" onClick={this.handleClick.bind(this)}>
                <i className=" fas fa-tachometer-alt"/>
                <span id="dashboard">Dashboard</span>
            </a> : null;

        return (<div style={{height: "100%"}}>
            <HeaderBar/>
            <section className="content">

                {/*Left side menu*/}
                <div className="content__left">

                    <section className="navigation">

                        <div className="navigation__list">
                            <div className="navigation__list__header">
                                <a href="#" className="navigation__list__item" onClick={this.handleClick.bind(this)}>
                                    <i className="fas fa-home"/>
                                    <span id="home">Home</span>
                                </a>
                                {show_dash_btn}
                                <a href="#" className="navigation__list__item" onClick={this.handleClick.bind(this)}>
                                    <i className="fas fa-upload"/>
                                    <span id="upload">Upload song</span>
                                </a>
                            </div>

                        </div>

                        <div className="navigation__list">
                            <div className="navigation__list__header" role="button" data-toggle="collapse"
                                 aria-expanded="true" aria-controls="yourMusic">
                                Your Music
                            </div>
                            <div className="collapse in" id="yourMusic">
                                <a id="songs" href="#" className="navigation__list__item"
                                   onClick={this.handleClick.bind(this)}>
                                    <i className="fas fa-headphones"/>
                                    <span id="songs">Songs</span>
                                </a>
                                <a id="albums" href="#" className="navigation__list__item"
                                   onClick={this.handleClick.bind(this)}>
                                    <i className="fas fa-compact-disc"/>
                                    <span id="albums">Albums</span>
                                </a>
                                <a id="artists" href="#" className="navigation__list__item"
                                   onClick={this.handleClick.bind(this)}>
                                    <i className="fas fa-user"/>
                                    <span id="artists">Artists</span>
                                </a>
                                <a id="files" href="#" className="navigation__list__item"
                                   onClick={this.handleClick.bind(this)}>
                                    <i className="fas fa-file"/>
                                    <span id="files">Local Files</span>
                                </a>
                            </div>
                        </div>

                        <div className="navigation__list">
                            <div className="navigation__list__header" role="button" data-toggle="collapse"
                                 aria-expanded="true" aria-controls="playlists">
                                Playlists
                            </div>
                            <div className="collapse in">
                                <a href="#" className="navigation__list__item">
                                    <i className="fas fa-music"/>
                                    <span>Doo Wop</span>
                                </a>
                                <a href="#" className="navigation__list__item">
                                    <i className="fas fa-music"/>
                                    <span>Pop Classics</span>
                                </a>
                                <a id="playlist" href="#" className="navigation__list__item"
                                   onClick={this.handleClick.bind(this)}>
                                    <i className="fas fa-plus"/>
                                    <span id="playlist">New Playlist</span>
                                </a>
                            </div>
                        </div>

                    </section>

                    <PlayingSongInfo props={this.state.playing_song} props2={this.state.playing_song_album}/>

                </div>

                <div className="content__middle">
                    {show_window(this.state, this.handlePlay.bind(this))}
                </div>

                {/*Right side panel*/}
                <SideMenuPanel2/>
            </section>
            <MusicPlayer props={this.state.playing_song_id}/>
        </div>);
    }
}

export default HomePage;

