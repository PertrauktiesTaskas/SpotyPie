import React from "react";

class SongList extends React.Component {


    render() {
        return (
            <div style={{height: "100%"}}>

                <div className="tracks">

                    <div className="tracks__heading">

                        <div className="tracks__heading__number">#</div>

                        <div className="tracks__heading__artist">Artist</div>

                        <div className="tracks__heading__title">Song</div>

                        <div className="tracks__heading__album">Album</div>

                        <div className="tracks__heading__length">

                            <i className="far fa-clock"/>

                        </div>

                    </div>

                    <div className="track">

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

export default SongList;