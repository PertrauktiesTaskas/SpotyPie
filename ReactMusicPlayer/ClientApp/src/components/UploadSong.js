import React from "react";
import Formsy from "formsy-react";
import FormInput from './FormInput';
import FormCheckbox from "./FormCheckbox";

class UploadSong extends React.Component {
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


            </div>

        );
    }

}

export default UploadSong;