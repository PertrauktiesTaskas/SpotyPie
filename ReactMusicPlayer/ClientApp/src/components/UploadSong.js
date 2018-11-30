import React from "react";
import '../css/song_upload.css';
import {itemService} from "../Service";

class UploadSong extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            fileName: "",
            message: "",
            block_btn: true
        };

        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleSubmit(event) {
        console.log("Submit", event);
        event.preventDefault();

        const data = new FormData();
        data.append('file', this.uploadInput.files[0]);

        /*console.log("Upload file", this.uploadInput.files[0]);

        for (var pair of data.entries()) {
            console.log("Data", pair);
        }*/

        /*console.log("Data", data.entries());*/

        itemService.uploadSong(data).then(() => {
            this.setState({message: "Song successfully uploaded"});
        });
    }

    handleChange() {
        this.setState({message: ""});

        let input = document.getElementById("file-upload");
        let file = input.value.split("\\");
        let fileName = file[file.length - 1];
        this.setState({
            fileName: fileName,
            block_btn: false
        });
    }

    render() {

        let file = this.state.fileName != null ? <div className="file-name">{this.state.fileName}</div> : null;
        let success = this.state.message != null ? <div className="file-name">{this.state.message}</div> : null;
        let button = this.state.block_btn ?
            <input className="submit_btn" type="submit" value="Upload" disabled={true} style={{opacity: 0.5}}/> :
            <input className="submit_btn" type="submit" value="Upload"/>

        return (
            <div style={{height: "100%"}}>
                <form method="POST" className="upload_song_form" encType="multipart/form-data"
                      onSubmit={this.handleSubmit.bind(this)}>
                    <div className="upload_title">Upload a song</div>
                    {file}
                    <label className="custom-file-upload">
                        <input type="file" id="file-upload" onChange={this.handleChange.bind(this)} ref={(ref) => {
                            this.uploadInput = ref;
                        }}/>
                        Choose file
                    </label>
                    {button}
                    {success}
                </form>
            </div>

        );
    }

}

export default UploadSong;