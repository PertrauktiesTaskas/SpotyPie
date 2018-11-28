import React from 'react';
import '../css/main_styles.css';
import '../css/responsive.css';
import {itemService} from "../Service";
import ArtistDefault from "../img/artist_default.png";

class ArtistInfo extends React.Component {
    constructor(props) {
        super(props);
        console.log(props);

        this.state = {
            show_album_songs: false,
            selected_album_id: "",
            selected_album_tracks: [],
            artist_info: "",
            artist_images: [],
            artist_top_tracks: [],
            related_artists: [],
            artist_albums: [],
            top_track_albums: []
        };

        this.handleClick = this.handleClick.bind(this);
    }

    async componentDidMount() {

        function removeDuplicateRelated(arr) {

            if (arr != null) {

                let unique_ids = [];
                let unique_artists = [];
                for (let i = 0; i < arr.length; i++) {
                    if (unique_ids.indexOf(arr[i].id) === -1) {

                        unique_ids.push(arr[i].id);
                        unique_artists.push(arr[i]);
                    }
                }
                return unique_artists;
            }
            else {
                return null;
            }
        }

        let artist_information = await itemService.getArtistInfo(this.props.props);
        let artist_img = artist_information.images[0].url;

        let artist_top_tracks = [];
        let top_track_albums = [];

        await itemService.getArtistTopTracks(this.props.props).then((data) => {
            console.log('Loading artist top tracks:', data.slice(0, 5));

            artist_top_tracks = data.slice(0, 5);

            let top_tracks = data.slice(0, 5);
            let top_tracks_albums = [];

            top_tracks.map((track) => {
                itemService.getSongAlbum(track.id).then((data) => {
                    top_tracks_albums.push(data.images[0].url);
                });
            });

            console.log("Top tracks albums", top_tracks_albums);
            top_track_albums = top_tracks_albums;
        });

        let albums = [];

        itemService.getArtistAlbums(this.props.props).then((data) => {
            console.log('Loading artist albums:', data);

            albums = data.albums;
        });

        let related_artists_arr = [];

        itemService.getRelatedArtists(this.props.props).then((data) => {
            console.log('Loading related artists:', data);

            let related = removeDuplicateRelated(data);

            related_artists_arr = related;
            this.setState({
                artist_info: artist_information,
                artist_images: artist_img,
                artist_top_tracks: artist_top_tracks,
                related_artists: related_artists_arr,
                artist_albums: albums,
                top_track_albums: top_track_albums
            });
        });


    }

    handleClick(event) {
        switch (event.target.innerText) {
            case "VIEW":
                console.log("Selecting album", event.target.id);
                this.setState({
                    show_album_songs: true,
                    selected_album_id: event.target.id
                });

                itemService.getAlbumSongs(event.target.id).then((data) => {
                    console.log("Loading selected album songs: ", data.songs);

                    this.setState({
                        selected_album_tracks: data.songs
                    });
                });

                break;
            default:
                this.setState({
                    show_album_songs: false,
                    selected_album_id: "",
                    selected_album_tracks: []
                });
                break;
        }

    }

    render() {

        function TopTracks(props) {

            return (<div className="track">

                <div className="track__art">

                    <img
                        src={props.props2[props.index]}
                        alt=""/>

                </div>

                <div className="track__number">{props.index + 1}</div>

                <div className="track__title">{props.props.name}</div>

                {/*  <div className="track__explicit">

                    <span className="label">Explicit</span>

                </div>*/}


            </div>);
        }

        function Albums(props) {

            let show_tracks = props.props.id == props.selected ?
                <AlbumTracks props={props.props.id} tracks={props.tracks} function={props.function}/> : null;

            let button_text = props.props.id == props.selected ? "Hide" : "View";

            var dateFormat = require('dateformat');

            return (<div>
                    <div className="album__info">

                        <div className="album__info__art">

                            <img
                                src={props.props.images[0].url}
                                alt=""/>

                        </div>

                        <div className="album__info__meta">

                            <div className="album__year">{dateFormat(props.props.releaseDate, "yyyy")}</div>

                            <div className="album__name">{props.props.name}</div>

                            <div className="album__actions">

                                <button id={props.props.id} className="button-light save"
                                        onClick={props.func}>{button_text}
                                </button>

                            </div>

                        </div>

                    </div>

                    {show_tracks}</div>
            );
        }

        function AlbumTracks(props) {


            if (props.props != null && props.tracks != null) {


                let tracks = props.tracks.map((track) => <AlbumSingleTrack props={track} function={props.function}/>);


                return (
                    <div className="album__tracks">

                        <div className="tracks">

                            <div className="artist__album__tracks__heading">

                                <div className="artist__album__tracks__heading__number">#</div>

                                <div className="artist__album__tracks__heading__title">Song</div>

                                <div className="artist__album__tracks__heading__length">

                                    <i className="far fa-clock"/>

                                </div>

                            </div>

                            {tracks}


                        </div>

                    </div>


                );
            }
            else {
                return null;
            }
        }

        function AlbumSingleTrack(props) {

            let explicit = props.props.explicit ? <div id={props.props.id - 1} className="track__explicit">

                <span className="label">Explicit</span>

            </div> : null;

            let duration = props.props.durationMs / 1000;
            let minutes = Math.floor(duration / 60);
            let seconds = Math.floor(duration - minutes * 60);

            let disp_duration = seconds >= 10 ?
                <div className="track__length"><i className="far fa-clock"/> {minutes + ':' + seconds}</div> :
                <div className="track__length"><i className="far fa-clock"/> {minutes + ':0' + seconds}</div>;

            return (
                <div id={props.props.id - 1} className="track" onClick={props.function}>

                    <div id={props.props.id - 1} className="track__number">{props.props.trackNumber}</div>

                    <div id={props.props.id - 1} className="track__title"
                         style={{marginLeft: "100px"}}>{props.props.name}</div>

                    {explicit}

                    <div id={props.props.id - 1} className="track__length"><i
                        className="far fa-clock"/>{disp_duration}</div>

                </div>);
        }

        function RelatedArtists(props) {

            let artist_img = props.props.images.length > 0 ? props.props.images[0].url : ArtistDefault;

            return (
                <a href="#" className="related-artist">

                    <span className="related-artist__img">

                      <img src={artist_img} alt=""/>

                    </span>

                    <span className="related-artist__name">{props.props.name}</span>

                </a>
            );
        }

        let album_songs = this.state.show_album_songs ? <AlbumTracks function={this.props.function}/> : null;

        let artist_img = this.state.artist_images != null ? this.state.artist_images : ArtistDefault;

        let top_tracks = this.state.artist_top_tracks.map((top_track, index) => <TopTracks props={top_track}
                                                                                           props2={this.state.top_track_albums}
                                                                                           index={index}/>);

        let related_artists = this.state.related_artists != null ? this.state.related_artists.map(rel_artist =>
            <RelatedArtists props={rel_artist}/>) : null;

        let albums = this.state.artist_albums.map(album => <Albums props={album} func={this.handleClick.bind(this)}
                                                                   selected={this.state.selected_album_id}
                                                                   tracks={this.state.selected_album_tracks}
                                                                   function={this.props.function}/>);

        return (
            <div style={{height: "100%"}}>

                <div className="artist">

                    <div className="artist__header" style={{backgroundImage: "url(" + artist_img + ")"}}>

                        <div className="artist__info">

                            <div className="profile__img">

                                <img src={artist_img}
                                     alt=""/>

                            </div>

                            <div className="artist__info__meta">

                                <div className="artist__info__name">{this.state.artist_info.name}</div>

                            </div>

                        </div>

                        <div className="artist__navigation">

                            <ul className="nav nav-tabs" role="tablist">

                                <li role="presentation" className="active">
                                    <a href="#" aria-controls="artist-overview" role="tab"
                                       data-toggle="tab">Overview</a>
                                </li>

                                <li role="presentation">
                                    <a href="#" aria-controls="related-artists" role="tab"
                                       data-toggle="tab">Related
                                        Artists</a>
                                </li>

                            </ul>

                            <div className="artist__navigation__friends">

                                <a href="#">
                                    <img src="http://zblogged.com/wp-content/uploads/2015/11/17.jpg" alt=""/>
                                </a>

                                <a href="#">
                                    <img src="http://zblogged.com/wp-content/uploads/2015/11/5.png" alt=""/>
                                </a>

                                <a href="#">
                                    <img
                                        src="http://cdn.devilsworkshop.org/files/2013/01/enlarged-facebook-profile-picture.jpg"
                                        alt=""/>
                                </a>

                            </div>

                        </div>

                    </div>

                    <div className="artist__content">

                        <div className="tab-content">

                            <div role="tabpanel" className="tab-pane active" id="artist-overview">

                                <div className="overview">

                                    <div className="overview__artist">


                                        {/*<div className="section-title">Latest Release</div>*/}

                                        {/*<div className="latest-release">

                                            <div className="latest-release__art">

                                                <img
                                                    src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/whenDarkOut.jpg"
                                                    alt="When It's Dark Out"/>

                                            </div>

                                            <div className="latest-release__song">

                                                <div className="latest-release__song__title">Drifting (Track Commentary)
                                                </div>

                                                <div className="latest-release__song__date">

                                                    <span className="day">4</span>

                                                    <span className="month">December</span>

                                                    <span className="year">2015</span>

                                                </div>

                                            </div>

                                        </div>*/}

                                        <div className="section-title">Top 5 songs</div>

                                        <div className="tracks">

                                            {top_tracks}

                                        </div>

                                    </div>

                                    <div className="overview__related">

                                        <div className="section-title">Related Artists</div>

                                        <div className="related-artists">

                                            {related_artists}

                                        </div>

                                    </div>

                                    <div className="overview__albums" style={{marginBottom: "20px"}}>

                                        <div className="overview__albums__head">

                                            <span className="section-title">Albums</span>


                                        </div>

                                        <div className="album">

                                            {albums}

                                        </div>

                                    </div>

                                </div>

                            </div>

                        </div>

                    </div>

                </div>

            </div>

        );
    }
}

export default ArtistInfo;