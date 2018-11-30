import React from "react";
import '../css/settings.css';
import {itemService} from "../Service";

class UploadSong extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            quality: "FLAC",
            path: ""
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(event) {
        console.log("Change:", event.target.value);

        if (event.target.name === "quality") {
            console.log("Changing quality to: ", event.target.value);
            this.setState({quality: event.target.value});
        }
        else if (event.target.name === "song_path") {
            console.log("Changing path: ", event.target.innerText);
            this.setState({path: event.target.innerText});
        }
    }

    handleSubmit(event) {
        console.log("Submit", event.target);

        event.preventDefault();
    }

    render() {

        let quality_select = this.state.quality === "FLAC" ?
            <div className="settings_option"><label>Song quality:</label>
                <input type="radio" name="quality" value="FLAC" checked onClick={this.handleChange.bind(this)}/><span>>320kbps (FLAC)</span>
                <input type="radio" name="quality" value="mp3" onClick={this.handleChange.bind(this)}/><span>320kbps (MP3)</span>
            </div> :
            <div className="settings_option"><label>Song quality:</label>
                <input type="radio" name="quality" value="FLAC" onClick={this.handleChange.bind(this)}/><span>>320kbps (FLAC)</span>
                <input type="radio" name="quality" value="mp3" checked onClick={this.handleChange.bind(this)}/><span>320kbps (MP3)</span>
            </div>


        return (
            <div style={{height: "100%"}}>
                <form method="POST" className="settings_form"
                      onSubmit={this.handleSubmit.bind(this)}>
                    <div className="settings_title">System settings</div>
                    <div className="settings_option"><label> Music files path: </label>
                        <input type="text" name="song_path" onChange={this.handleChange.bind(this)}/></div>
                    {quality_select}
                    <div className="settings_option">
                        <input className="submit_btn" type="submit" value="Save"/>
                    </div>
                </form>
            </div>

        );
    }

}

export default UploadSong;