import React from "react";

class FilesList extends React.Component {

    render() {
        return (
            <div  style={{height: "100%"}}>

                <div className="tracks">

                    <div className="tracks__heading">

                        <div className="tracks__heading__number">#</div>

                        <div className="tracks__heading__file">File</div>

                        <div className="tracks__heading__length" style={{marginRight: "5px"}}> FORMAT</div>

                    </div>

                    <div className="track">

                        <div className="track__number">1</div>

                        <div className="track__title">G-Eazy - Intro</div>

                        <div className="track__length" style={{marginRight: "15px"}}>FLAC</div>

                    </div>

                    <div className="track">

                        <div className="track__number">2</div>

                        <div className="track__title">G-Eazy - Random</div>

                        <div className="track__length" style={{marginRight: "15px"}}>FLAC</div>

                    </div>

                    <div className="track">

                        <div className="track__number">3</div>

                        <div className="track__title">G-Eazy feat. Bebe Rexha - Me, Myself & I</div>

                        <div className="track__length" style={{marginRight: "15px"}}>FLAC</div>

                    </div>

                    <div className="track">

                        <div className="track__number">4</div>

                        <div className="track__title">G-Eazy feat. Big Sean - One Of Them</div>

                        <div className="track__length" style={{marginRight: "15px"}}>FLAC</div>

                    </div>

                    <div className="track">

                        <div className="track__number">5</div>

                        <div className="track__title">G-Eazy feat. Chris Brown & Tory Lanez - Drifting</div>

                        <div className="track__length" style={{marginRight: "15px"}}>FLAC</div>

                    </div>

                </div>

            </div>

        );
    }

}

export default FilesList;