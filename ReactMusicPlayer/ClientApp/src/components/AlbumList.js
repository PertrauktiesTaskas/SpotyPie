import React from "react";
import AlbumSongs from "./AlbumSongs";
import {itemService} from "../Service";

class AlbumList extends React.Component {
    constructor(props) {
        super(props);
        console.log(props);

        this.state = {
            show_album_list: true,
            show_album_songs: false,
            selected_album: "",
            albums: []
        };

        this.handleClick = this.handleClick.bind(this);
    }

    async componentDidMount() {
        let getAlbums = await itemService.getAlbums();
            this.setState({albums: getAlbums});
    }

    handleClick(event) {
        switch (event.target.id) {
            case "back_btn":
                this.setState({
                    show_album_list: true,
                    show_album_songs: false,
                    selected_album: ""
                });
                break;
            default:
                console.log("Clicked item", this.state.albums[event.target.id-1]);
                this.setState({
                    show_album_list: false,
                    show_album_songs: true,
                    selected_album: this.state.albums[event.target.id-1]
                });
                break;
        }

    }

    render() {
        function DisplayAlbum(props) {

            var dateFormat = require('dateformat');

            return (<div className="album__info" style={{borderBottom: "solid 1px"}}>
                    <div className="album__info__art">

                        <img
                            src={props.props.images[0].url}
                            alt=""/>

                    </div>

                    <div className="album__info__meta">

                        <div className="album__artist">{JSON.parse(props.props.artists)[0].Name}</div>

                        <div className="album__name">{props.props.name}</div>

                        <div className="album__year">{dateFormat(props.props.releaseDate, "yyyy")}</div>

                        <div className="album__actions">

                            <button id={props.props.id} className="button-light save"
                                    onClick={props.func}>View
                            </button>

                        </div>
                    </div>
                </div>
            );
        }

        let album = this.state.albums.map(album => <DisplayAlbum props={album} func={this.handleClick.bind(this)}/>);

        if (this.state.show_album_list) {
            return (
                <div style={{height: "100%"}}>


                    {album}

                </div>);
        }
        else {
            return (<div style={{padding: "10px"}}>
                    <button id="back_btn" className="button-light" style={{borderRadius: "25px"}}
                            onClick={this.handleClick.bind(this)}><i id="back_btn" onClick={this.handleClick.bind(this)}
                                                                     className="fas fa-arrow-left"/>
                    </button>
                    <div className="album_title">{this.state.selected_album.name}</div>
                    <AlbumSongs props={this.state.selected_album} function={this.props.props}/>
                </div>
            );
        }
    }

}

export default AlbumList;