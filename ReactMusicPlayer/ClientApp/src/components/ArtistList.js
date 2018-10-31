import React from "react";
import ArtistInfo from './ArtistInfo';

class ArtistList extends React.Component {
    constructor(props) {
        super(props);
        console.log(props);

        this.state = {
            show_artist_list: true,
            show_artist_info: false,
            selected_artist: ""
        };

        this.handleClick = this.handleClick.bind(this);
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
        if (this.state.show_artist_list) {
            return (
                <div style={{height: "100%"}}>

                    <div className="artist__info" style={{borderBottom: "solid 1px"}}>

                        <div className="artist__info__art">

                            <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/g_eazy_propic.jpg"
                                 alt="G-Eazy"/>

                        </div>

                        <div className="artist__info__meta">

                            <div className="artist__name">G-Eazy</div>


                            <div className="artist__actions">

                                <button id="G-Eazy" className="button-light save"
                                        onClick={this.handleClick.bind(this)}>View
                                </button>

                            </div>

                        </div>

                    </div>

                </div>

            );
        }
        else {
            return (<div style={{height: "100%"}}>
                    <div id="back_btn" className="back_btn"
                            onClick={this.handleClick.bind(this)}><i id="back_btn" onClick={this.handleClick.bind(this)}
                                                                     className="fas fa-arrow-left"/>
                    </div>
                    <ArtistInfo/>
                </div>
            );
        }
    }

}

export default ArtistList;