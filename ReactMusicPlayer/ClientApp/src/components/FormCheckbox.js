import { withFormsy } from 'formsy-react';
import React from 'react';

class FormInput extends React.Component {
    constructor(props) {
        super(props);
        this.changeValue = this.changeValue.bind(this);
    }

    changeValue(event) {
        this.props.setValue(event.currentTarget.checked);
    }

    render() {
        // An error message is returned only if the component is invalid
        const errorMessage = this.props.getErrorMessage();
        let checkbox = this.props.getValue() ?
            <i id="chk_subscribe_icon" className="checkbox-checkmark"></i> :
            <i id="chk_subscribe_icon" className="checkbox-checkmark" style={{background: "silver"}}></i>;
        return (
            <div>
                <label className="checkbox-container" style={{marginBottom: 0}}>
                    <input name="subscribe" type="checkbox" className="check_add_playlist"
                           checked={this.props.getValue()} onChange={this.changeValue}/>
                    {this.props.labelName}
                    {checkbox}
                </label>
                <span style={{color: 'red'}}>{errorMessage}</span>
            </div>
        );
    }
}

export default withFormsy(FormInput);