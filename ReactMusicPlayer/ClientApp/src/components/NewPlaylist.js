import React from "react";
import Formsy from "formsy-react";
import FormInput from './FormInput';
import FormCheckbox from "./FormCheckbox";
import {itemService} from "../Service";

class NewPlaylist extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            canSubmit: false,
            songs: []
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.disableButton = this.disableButton.bind(this);
        this.enableButton = this.enableButton.bind(this);
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

    disableButton() {
        this.setState({canSubmit: false});
    }

    enableButton() {
        this.setState({canSubmit: true});
    }

    handleSubmit(model) {
        var json = JSON.stringify(model);
        console.log(json);

        this.setState({});
    }

    render() {

        function SingleSong(props) {

            let duration = props.props.DurationMs / 1000;
            let minutes = Math.floor(duration / 60);
            let seconds = Math.floor(duration - minutes * 60);


            let disp_duration = seconds >= 10 ?
                <div className="track__length"><i className="far fa-clock"/> {minutes + ':' + seconds}</div> :
                <div className="track__length"><i className="far fa-clock"/> {minutes + ':0' + seconds}</div>;

            return (<div className="track">

              {/* <FormCheckbox
                    name={props.index.toString()}
                />*/}
                <input type="checkbox" name="song" value={props.index}/>
                <div className="track__number">{props.index + 1}</div>

                <div className="track__artist">{props.props.Artist}</div>

                <div className="track__title">{props.props.Name}</div>

                <div className="track__length">{disp_duration}</div>

            </div>);
        }

        let songs = this.state.songs.map((song, index) => <SingleSong props={song} index={index}/>);

        return (
            <div style={{height: "100%"}}>

                <Formsy className={"w-100"} onValidSubmit={this.handleSubmit}
                        onValid={this.enableButton} onInvalid={this.disableButton} style={{paddingTop: "10px"}}>
                    <label className="playlist_name_input" htmlFor={"name"}>Playlist name:</label>
                    <FormInput
                        name="name"
                        validations="isExisty"
                        validationError="*Enter playlist's name"
                        required
                    />
                    <button type="submit" disabled={!this.state.canSubmit}
                            id="add_btn" className="button-dark">
                        Save
                    </button>


                    <div className="tracks">

                        <div className="tracks__heading" style={{marginLeft: "20px"}}>

                            <div className="tracks__heading__number">#</div>

                            <div className="tracks__heading__artist">Artist</div>

                            <div className="tracks__heading__title" style={{
                                marginLeft: "125px",
                                width: "45.5%"
                            }}>Song</div>



                            <div className="tracks__heading__length" style={{marginRight: "20px"}}>

                                <i className="far fa-clock"/>

                            </div>

                        </div>

                        {songs}

                    </div>

                </Formsy>

            </div>

        );
    }

}

export default NewPlaylist;