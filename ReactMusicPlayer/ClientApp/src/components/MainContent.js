import React from 'react';
import '../css/main_styles.css';
import '../css/responsive.css';
import {itemService} from "../Service";
import ArtistDefault from '../img/artist_default.png';

class ContentMain extends React.Component {
    constructor(props) {
        super(props);
        console.log(props);

        this.state = {
            artists: null,
            recent_album: null,
            recent_artist: null,
            recent_song: null,
            favorite_album: null,
            favorite_artist: null,
            favorite_song: null
        };
    }

    componentDidMount() {

        itemService.getArtists().then((data) => {
            this.setState({artists: data});

            itemService.getMostPopularAlbum().then((data) => {
                this.setState({favorite_album: data[0]});
                this.state.artists.map((artist) => {
                    if (artist.name === JSON.parse(data[0].artists)[0].Name) {
                        itemService.getArtistInfo(artist.id).then((data) => {
                            this.setState({favorite_artist: data});
                        })
                    }
                });
            });

            itemService.getRecentAlbum().then((data) => {
                this.setState({recent_album: data[0]});
                this.state.artists.map((artist) => {
                    if (artist.name === JSON.parse(data[0].artists)[0].Name) {
                        itemService.getArtistInfo(artist.id).then((data) => {
                            this.setState({recent_artist: data});
                        })
                    }
                });
            });
        });

        itemService.getPopRecentSong().then((data) => {
            this.setState({
                recent_song: data.recent,
                favorite_song: data.popular
            });
        })
    }

    render() {

        function FavoriteAlbum(props) {
            var dateFormat = require('dateformat');

            if (props.props != null) {
                return (
                    <div className="top_album">
                        <div className="col-sm-5">
                            <img src={props.props.images[0].url}
                                 alt=""/>
                        </div>
                        <div className="col-sm-7">
                            <div className="top_album_name">
                                <span>{JSON.parse(props.props.artists)[0].Name}</span><br/>
                                {props.props.name}<br/>
                                <span>{dateFormat(props.props.releaseDate, "yyyy")}</span>
                            </div>
                        </div>
                    </div>
                );
            }
            else {
                return null;
            }
        }

        function LastAlbum(props) {
            var dateFormat = require('dateformat');

            if (props.props != null) {
                return (
                    <div className="top_album">
                        <div className="col-sm-5">
                            <img src={props.props.images[0].url}
                                 alt=""/>
                        </div>
                        <div className="col-sm-7">
                            <div className="top_album_name">
                                <span>{JSON.parse(props.props.artists)[0].Name}</span><br/>
                                {props.props.name}<br/>
                                <span>{dateFormat(props.props.releaseDate, "yyyy")}</span>
                            </div>
                        </div>
                    </div>
                );
            }
            else {
                return null;
            }
        }

        function FavoriteArtist(props) {
            if (props.props != null) {

                let img = props.props.images.length > 0 ? props.props.images[0].url : ArtistDefault;

                return (
                    <div className="top_artist">
                        <div className="col-sm-5">
                            <img src={img}
                                 alt=""/>
                        </div>
                        <div className="col-sm-7">
                            <div className="top_artist_name">
                                <br/>{props.props.name}<br/>
                            </div>
                        </div>
                    </div>);
            }
            else {
                return null;
            }
        }

        function LastArtist(props) {
            if (props.props != null) {

                let img = props.props.images.length > 0 ? props.props.images[0].url : ArtistDefault;

                return (
                    <div className="top_artist">
                        <div className="col-sm-5">
                            <img src={img}
                                 alt=""/>
                        </div>
                        <div className="col-sm-7">
                            <div className="top_artist_name">
                                <br/>{props.props.name}<br/>
                            </div>
                        </div>
                    </div>);
            }
            else {
                return null;
            }
        }

        function FavoriteSong(props) {
            if (props.props != null) {
                return (<div className="top_song">
                    <div className="col-sm-5">
                        <img src={props.props.imageUrl}
                             alt=""/>
                    </div>
                    <div className="col-sm-7">
                        <div className="top_song_name">
                            <span>{props.props.artist}</span><br/>
                            {props.props.name}<br/>
                            <span>{props.props.albumName}</span>
                        </div>
                    </div>
                </div>);
            }
            else {
                return null;
            }
        }

        function LastSong(props) {
            if (props.props != null) {
                return (<div className="top_song">
                    <div className="col-sm-5">
                        <img src={props.props.imageUrl}
                             alt=""/>
                    </div>
                    <div className="col-sm-7">
                        <div className="top_song_name">
                            <span>{props.props.artist}</span><br/>
                            {props.props.name}<br/>
                            <span>{props.props.albumName}</span>
                        </div>
                    </div>
                </div>);
            }
            else {
                return null;
            }
        }

        return (
            <div style={{height: "100%"}}>

                <div className="col-sm-5 home_section">
                    <div className="section_name"> Favorite Artist</div>
                    <FavoriteArtist props={this.state.favorite_artist}/>
                </div>

                <div className="col-sm-5 home_section">
                    <div className="section_name"> Last played artist</div>
                    <LastArtist props={this.state.recent_artist}/>
                </div>

                <div className="col-sm-5 home_section">
                    <div className="section_name"> Favorite Album</div>
                    <FavoriteAlbum props={this.state.favorite_album}/>
                </div>

                <div className="col-sm-5 home_section">
                    <div className="section_name"> Last played album</div>
                    <LastAlbum props={this.state.recent_album}/>
                </div>

                <div className="col-sm-5 home_section">
                    <div className="section_name"> Favorite Song</div>
                    <FavoriteSong props={this.state.favorite_song}/>
                </div>

                <div className="col-sm-5 home_section">
                    <div className="section_name"> Last played song</div>
                    <LastSong props={this.state.recent_song}/>
                </div>


            </div>

        );
    }
}

export default ContentMain;