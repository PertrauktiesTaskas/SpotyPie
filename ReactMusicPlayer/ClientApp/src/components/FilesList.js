import React from "react";
import {itemService} from "../Service";

class FilesList extends React.Component {
    constructor(props) {
        super(props);
        console.log(props);

        this.state = {
            files: []
        };
    }

    componentDidMount() {
        itemService.getFileList().then((data) => {
            console.log("File list", data);
            this.setState({files: data});
        });
    }


    render() {

        function File(props) {

            var file = props.props.split("/");
            var fileName = file[file.length - 1];

            let finalName = fileName.split(".")[0];
            let format = fileName.split(".")[1];

            return (<div className="track">

                <div className="track__number">{props.index + 1}</div>

                <div className="track__title" style={{marginLeft: "110px"}}>{finalName}</div>

                <div className="track__length" style={{marginRight: "15px"}}>{format}</div>

            </div>);
        }

        let file = this.state.files.map((f, index) => <File props={f} index={index}/>);

        return (
            <div style={{height: "100%"}}>

                <div className="tracks">

                    <div className="tracks__heading">

                        <div className="tracks__heading__number">#</div>

                        <div className="tracks__heading__file">File</div>

                        <div className="tracks__heading__length" style={{marginRight: "5px"}}> FORMAT</div>

                    </div>


                    {file}


                </div>

            </div>

        );
    }

}

export default FilesList;