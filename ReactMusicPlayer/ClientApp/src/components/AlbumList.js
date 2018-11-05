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
            selected_album: ""
        };

        this.handleClick = this.handleClick.bind(this);
    }

    componentDidMount() {
        /*itemService.getAlbums().then((data) => {
            console.log('Loading album:', data);
        });*/
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
                this.setState({
                    show_album_list: false,
                    show_album_songs: true,
                    selected_album: event.target.id
                });
                break;
        }

    }

    render() {
        if (this.state.show_album_list) {
            return (
                <div style={{height: "100%"}}>

                    <div className="album__info" style={{borderBottom: "solid 1px"}}>

                        <div className="album__info__art">

                            <img
                                src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/whenDarkOut.jpg"
                                alt="When It's Dark Out"/>

                        </div>

                        <div className="album__info__meta">

                            <div className="album__artist">G-Eazy</div>

                            <div className="album__name">When It's Dark Out</div>

                            <div className="album__year">2015</div>

                            <div className="album__actions">

                                <button id="When It's Dark Out" className="button-light save"
                                        onClick={this.handleClick.bind(this)}>View
                                </button>

                            </div>

                        </div>

                    </div>

                </div>

            );
        }
        else {
            return (<div style={{padding: "10px"}}>
                    <button id="back_btn" className="button-light" style={{borderRadius: "25px"}}
                            onClick={this.handleClick.bind(this)}><i id="back_btn" onClick={this.handleClick.bind(this)}
                                                                     className="fas fa-arrow-left"/>
                    </button>
                    <div className="album_title">{this.state.selected_album}</div>
                    <AlbumSongs/>
                </div>
            );
        }
    }

}

export default AlbumList;