import React from 'react';
import '../css/main_styles.css';
import HeaderBar from "./Header";
import MainContent from "./MainContent";
import SideMenuPanel2 from "./RightSideMenu";
import MusicPlayer from "./Player";
import SongList from "./SongList";
import AlbumList from "./AlbumList";
import ArtistList from "./ArtistList";
import FilesList from "./FilesList";
import NewPlaylist from "./NewPlaylist";

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
            show_playlist_add: false
        };

        this.handleClick = this.handleClick.bind(this);
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
                    show_playlist_add: false
                });
                break;
            case "songs":
                this.setState({
                    show_main_page: false,
                    show_song_list: true,
                    show_album_list: false,
                    show_artist_list: false,
                    show_files_list: false,
                    show_playlist_add: false
                });
                break;
            case "albums":
                this.setState({
                    show_main_page: false,
                    show_song_list: false,
                    show_album_list: true,
                    show_artist_list: false,
                    show_files_list: false,
                    show_playlist_add: false
                });
                break;
            case "artists":
                this.setState({
                    show_main_page: false,
                    show_song_list: false,
                    show_album_list: false,
                    show_artist_list: true,
                    show_files_list: false,
                    show_playlist_add: false
                });
                break;
            case "files":
                this.setState({
                    show_main_page: false,
                    show_song_list: false,
                    show_album_list: false,
                    show_artist_list: false,
                    show_files_list: true,
                    show_playlist_add: false
                });
                break;
            case "playlist":
                this.setState({
                    show_main_page: false,
                    show_song_list: false,
                    show_album_list: false,
                    show_artist_list: false,
                    show_files_list: false,
                    show_playlist_add: true
                });
                break;
            default:
                this.setState({
                    show_main_page: true,
                    show_song_list: false,
                    show_album_list: false,
                    show_artist_list: false,
                    show_files_list: false,
                    show_playlist_add: false
                });
        }
    }

    render() {
        let show_window = function (props) {
            console.log("Props", props);
            if (props.show_main_page) {
                return <MainContent/>;
            }
            else if (props.show_song_list) {
                return <SongList/>;
            }
            else if (props.show_album_list) {
                return <AlbumList/>;
            }
            else if (props.show_artist_list) {
                return <ArtistList/>;
            }
            else if (props.show_files_list) {
                return <FilesList/>;
            }
            else if (props.show_playlist_add) {
                return <NewPlaylist/>;
            }
        };


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
                            </div>

                        </div>

                        <div className="navigation__list">
                            <div className="navigation__list__header" role="button" data-toggle="collapse"
                                 href="#yourMusic"
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
                                 href="#playlists"
                                 aria-expanded="true" aria-controls="playlists">
                                Playlists
                            </div>
                            <div className="collapse in" id="playlists">
                                <a href="#" className="navigation__list__item">
                                    <i className="fas fa-music"/>
                                    <span>Doo Wop</span>
                                </a>
                                <a href="#" className="navigation__list__item">
                                    <i className="fas fa-music"/>
                                    <span>Pop Classics</span>
                                </a>
                            </div>
                        </div>

                    </section>

                    <section id="playlist" className="playlist" onClick={this.handleClick.bind(this)}>
                        <a href="#" id="playlist">
                            <i className="fas fa-plus"/>
                            New Playlist
                        </a>
                    </section>

                    <section className="playing">
                        <div className="playing__art">
                            <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/cputh.jpg" alt="Album Art"/>
                        </div>
                        <div className="playing__song">
                            <span className="playing__song__name"><i className="fas fa-music"/> Some Type of Love</span>
                            <span className="playing__song__artist"><i className="fas fa-user"/> Charlie Puth</span>
                            <span className="playing__song__album"><i className="fas fa-compact-disc"/> Album</span>
                        </div>
                        <div className="playing__add">
                            <i className="ion-checkmark"></i>
                        </div>

                    </section>

                </div>

                <div className="content__middle">
                    {show_window(this.state)}
                </div>

                {/*Right side panel*/}
                <SideMenuPanel2/>
            </section>
            <MusicPlayer/>
        </div>);
    }
}

export default HomePage;

