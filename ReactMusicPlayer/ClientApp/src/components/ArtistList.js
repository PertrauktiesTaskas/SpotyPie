import React from "react";
import ArtistInfo from './ArtistInfo';
import {itemService} from "../Service";
import ArtistDefault from '../img/artist_default.png';

class ArtistList extends React.Component {
    constructor(props) {
        super(props);
        console.log(props);

        this.state = {
            show_artist_list: true,
            show_artist_info: false,
            selected_artist: "",
            artists: []
        };

        this.handleClick = this.handleClick.bind(this);
    }

    componentDidMount() {
        itemService.getArtists().then((data) => {
            console.log('Loading artists:', data);
            this.setState({artists: data});
        });
    }

    handleClick(event) {
        console.log("Artist item clicked", event.target.id);
        switch (event.target.id) {
            case "back_btn":
                this.setState({
                    show_artist_list: true,
                    show_artist_info: false,
                    selected_artist: ""
                });
                break;
            default:
                this.setState({
                    show_artist_list: false,
                    show_artist_info: true,
                    selected_artist: event.target.id
                });
        }
    }

    render() {

        function SingleArtist(props) {

            let artist_img = props.props.images.length > 0 ? props.props.images[0].url : ArtistDefault;

            return (
                <div className="artist__info" style={{borderBottom: "solid 1px"}}>

                    <div className="artist__info__art">

                        <img src={artist_img}
                             alt=""/>

                    </div>

                    <div className="artist__info__meta">

                        <div className="artist__name">{props.props.name}</div>


                        <div className="artist__actions">

                            <button id={props.props.id} className="button-light save"
                                    onClick={props.func}>View
                            </button>

                        </div>

                    </div>

                </div>
            );
        }

        let artists = this.state.artists.map(artist => <SingleArtist props={artist}
                                                                     func={this.handleClick.bind(this)}/>);

        if (this.state.show_artist_list) {
            return (
                <div style={{height: "100%"}}>

                    {artists}

                </div>

            );
        }
        else {
            return (<div style={{height: "100%"}}>
                    <div id="back_btn" className="back_btn"
                         onClick={this.handleClick.bind(this)}><i id="back_btn" onClick={this.handleClick.bind(this)}
                                                                  className="fas fa-arrow-left"/>
                    </div>
                    <ArtistInfo props={this.state.selected_artist} function={this.props.props}/>
                </div>
            );
        }
    }

}

export default ArtistList;