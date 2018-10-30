import { withFormsy } from 'formsy-react';
import React from 'react';

class FormInput extends React.Component {
    constructor(props) {
        super(props);
        this.changeValue = this.changeValue.bind(this);
    }

    changeValue(event) {
        this.props.setValue(event.currentTarget.value);
    }

    render() {
        // An error message is returned only if the component is invalid
        const errorMessage = this.props.getErrorMessage();

        return (
            <div style={{display: "inline-block"}}>
                <input
                    className="playlist_form_name input_field"
                    onChange={this.changeValue}
                    type="text"
                    value={this.props.getValue() || ''}
                />
                <span style={{color: 'red'}}>{errorMessage}</span>
            </div>
        );
    }
}

export default withFormsy(FormInput);