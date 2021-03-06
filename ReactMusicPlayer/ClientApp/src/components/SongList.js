import React from "react";
import {itemService} from "../Service";

class SongList extends React.Component {
    constructor(props) {
        super(props);
        console.log(props);

        this.state = {
            songs: [],
            songCount: 0
        };

    }

    componentDidMount() {
        itemService.getSongs().then((data) => {
            console.log('Loading songs:', data);
            this.setState({
                songs: data,
                songCount: data.length
            });
        });
    }

    render() {

        function SingleSong(props) {

            let explicit = props.props.explicit ?
                <div className="track__explicit">
                    <span className="label">Explicit</span>
                </div> : null;

            let duration = props.props.DurationMs / 1000;
            let minutes = Math.floor(duration / 60);
            let seconds = Math.floor(duration - minutes * 60);


            let disp_duration = seconds >= 10 ?
                <div className="track__length"><i className="far fa-clock"/> {minutes + ':' + seconds}</div> :
                <div className="track__length"><i className="far fa-clock"/> {minutes + ':0' + seconds}</div>;

            return (
                <div id={parseInt(props.index)} className="track" onClick={props.function}>

                    <div id={parseInt(props.index)} className="track__number">{props.index + 1}</div>

                    <div id={parseInt(props.index)} className="track__artist">{props.props.Artist}</div>

                    <div id={parseInt(props.index)} className="track__title">{props.props.Name}</div>

                    <div id={parseInt(props.index)} className="track__length"><i
                        className="far fa-clock"/>{disp_duration}</div>

                </div>
            );
        }

        let songs = this.state.songs.map((song, index) => <SingleSong props={song} index={index}
                                                                      function={this.props.props}/>)

        return (
            <div style={{height: "100%"}}>

                <div className="tracks">

                    <div className="tracks__heading">

                        <div className="tracks__heading__number">#</div>

                        <div className="tracks__heading__artist">Artist</div>

                        <div className="tracks__heading__title">Song</div>

                        <div className="tracks__heading__length" style={{marginRight: "20px"}}>

                            <i className="far fa-clock"/>

                        </div>

                    </div>

                    {songs}

                </div>

            </div>

        );
    }

}

export default SongList;