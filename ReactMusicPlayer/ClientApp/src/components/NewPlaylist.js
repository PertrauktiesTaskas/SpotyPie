import React from "react";
import Formsy from "formsy-react";
import FormInput from './FormInput';
import FormCheckbox from "./FormCheckbox";

class NewPlaylist extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            canSubmit: false,
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.disableButton = this.disableButton.bind(this);
        this.enableButton = this.enableButton.bind(this);
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
                </Formsy>


                <div className="tracks">

                    <div className="tracks__heading" style={{marginLeft: "20px"}}>

                        <div className="tracks__heading__number">#</div>

                        <div className="tracks__heading__artist">Artist</div>

                        <div className="tracks__heading__title">Song</div>

                        <div className="tracks__heading__album">Album</div>

                        <div className="tracks__heading__length">

                            <i className="far fa-clock"/>

                        </div>

                    </div>

                    <div className="track">

                        <Formsy>
                            <FormCheckbox
                                name="add_to_playlist"
                            />
                        </Formsy>

                        <div className="track__number">1</div>

                        <div className="track__artist">G-Eazy</div>

                        <div className="track__title">Intro</div>

                        <div className="track__album">When It's Dark Out</div>

                        <div className="track__explicit" style={{marginLeft: "5%"}}>

                            <span className="label">Explicit</span>

                        </div>

                        <div className="track__length">1:11</div>

                    </div>

                    <div className="track">

                        <Formsy>
                            <FormCheckbox
                                name="add_to_playlist"
                            />
                        </Formsy>

                        <div className="track__number">2</div>

                        <div className="track__artist">G-Eazy</div>

                        <div className="track__title">Random</div>

                        <div className="track__album">When It's Dark Out</div>

                        <div className="track__explicit" style={{marginLeft: "5%"}}>

                            <span className="label">Explicit</span>

                        </div>

                        <div className="track__length">3:00</div>

                    </div>

                    <div className="track">

                        <Formsy>
                            <FormCheckbox
                                name="add_to_playlist"
                            />
                        </Formsy>

                        <div className="track__number">3</div>

                        <div className="track__artist">G-Eazy</div>

                        <div className="track__title featured">

                            <span className="title">Me, Myself & I</span>
                            <span className="feature">Bebe Rexha</span>

                        </div>

                        <div className="track__album">When It's Dark Out</div>

                        <div className="track__explicit" style={{marginLeft: "5%"}}>

                            <span className="label">Explicit</span>

                        </div>

                        <div className="track__length">4:11</div>

                    </div>

                    <div className="track">

                        <Formsy>
                            <FormCheckbox
                                name="add_to_playlist"
                            />
                        </Formsy>

                        <div className="track__number">4</div>

                        <div className="track__artist">G-Eazy</div>

                        <div className="track__title featured">

                            <span className="title">One Of Them</span>
                            <span className="feature">Big Sean</span>

                        </div>

                        <div className="track__album">When It's Dark Out</div>

                        <div className="track__explicit" style={{marginLeft: "5%"}}>

                            <span className="label">Explicit</span>

                        </div>

                        <div className="track__length">3:20</div>

                    </div>

                    <div className="track">

                        <Formsy>
                            <FormCheckbox
                                name="add_to_playlist"
                            />
                        </Formsy>

                        <div className="track__number">5</div>

                        <div className="track__artist">G-Eazy</div>

                        <div className="track__title featured">

                            <span className="title">Drifting</span>
                            <span className="feature">Chris Brown</span>
                            <span className="feature">Tory Lanez</span>

                        </div>

                        <div className="track__album">When It's Dark Out</div>

                        <div className="track__explicit" style={{marginLeft: "5%"}}>

                            <span className="label">Explicit</span>

                        </div>

                        <div className="track__length">4:33</div>

                    </div>

                </div>

            </div>

        );
    }

}

export default NewPlaylist;