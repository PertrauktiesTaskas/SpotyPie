import React from "react";
import {itemService} from "../Service";

class PlaylistSongs extends React.Component {

    constructor(props) {
        super(props);
        console.log(props);

        this.state = {songs: []};

    }

    componentDidMount() {
        itemService.getPlaylistSongs(this.props.props).then((data) => {
            console.log('Loading playlist songs:', data);
            this.setState({songs: data.items});
        });
    }

    render() {

        function SingleSong(props) {

            console.log("Playlist song", props);

            let explicit = props.props.explicit ?
                <div id={props.props.id - 1} className="track__explicit">
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

                    <div id={props.props.id - 1} className="track__number">{props.index + 1}</div>

                    <div id={props.props.id - 1} className="track__title"
                         style={{marginLeft: "110px"}}>{props.props.name}</div>

                    {/*<div className="track__title featured">

                        <span className="title">Drifting</span>
                        <span className="feature">Chris Brown</span>
                        <span className="feature">Tory Lanez</span>

                    </div>*/}

                    {explicit}

                    <div id={props.props.id - 1} className="track__length"><i className="far fa-clock"/>{disp_duration}
                    </div>

                </div>
            );
        }

        let songs = this.state.songs.map((song, index) => <SingleSong props={song} index={index} function={this.props.function}/>);

        return (
            <div>

                <div className="tracks">

                    <div className="album__tracks__heading">

                        <div className="album__tracks__heading__number">#</div>

                        <div className="album__tracks__heading__title">Song</div>

                        <div className="album__tracks__heading__length">

                            <i className="far fa-clock"/>

                        </div>

                    </div>

                    {songs}

                </div>

            </div>

        );
    }

}

export default PlaylistSongs;