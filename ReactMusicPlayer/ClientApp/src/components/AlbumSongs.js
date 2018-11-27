import React from "react";

const AlbumSongs = ({props}) => {

    console.log("Album songs", props);

        return (
            <div>

                <div className="tracks">

                    <div className="album__tracks__heading">

                        <div className="album__tracks__heading__number">#</div>

                        <div className="album__tracks__heading__title">Song</div>

                        <div className="album__tracks__heading__length">

                            <i className="far fa-clock"/>

                        </div>

                    </div>

                    <div className="track">

                        <div className="track__number">1</div>

                        <div className="track__title">Intro</div>

                        <div className="track__explicit">

                            <span className="label">Explicit</span>

                        </div>

                        <div className="track__length"><i className="far fa-clock"/> 1:11</div>

                    </div>

                </div>

            </div>

        );


}

export default AlbumSongs;