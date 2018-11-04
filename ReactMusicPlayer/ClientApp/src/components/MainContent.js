import React from 'react';
import '../css/main_styles.css';
import '../css/responsive.css';

const ContentMain = ({props}) => {
    return (
        <div style={{height: "100%"}}>

            <div className="col-sm-5 home_section">
                <div className="section_name"> Favorite Artist</div>
                <div className="top_artist">
                    <div className="col-sm-5">
                        <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/g_eazy_propic.jpg"
                             alt="G-Eazy"/>
                    </div>
                    <div className="col-sm-7">
                        <div className="top_artist_name">
                            <br/>G-Eazy<br/>
                        </div>
                    </div>
                </div>
            </div>

            <div className="col-sm-5 home_section">
                <div className="section_name"> Last played artist</div>
                <div className="top_artist">
                    <div className="col-sm-5">
                        <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/g_eazy_propic.jpg"
                             alt="G-Eazy"/>
                    </div>
                    <div className="col-sm-7">
                        <div className="top_artist_name">
                            <br/>G-Eazy<br/>
                        </div>
                    </div>
                </div>
            </div>

            <div className="col-sm-5 home_section">
                <div className="section_name"> Favorite Album</div>
                <div className="top_album">
                    <div className="col-sm-5">
                        <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/whenDarkOut.jpg"
                             alt="When It's Dark Out"/>
                    </div>
                    <div className="col-sm-7">
                        <div className="top_album_name">
                            <span>G-Eazy</span><br/>
                            When It's Dark Out<br/>
                            <span>2015</span>
                        </div>
                    </div>
                </div>
            </div>

            <div className="col-sm-5 home_section">
                <div className="section_name"> Last played album</div>
                <div className="top_album">
                    <div className="col-sm-5">
                        <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/whenDarkOut.jpg"
                             alt="When It's Dark Out"/>
                    </div>
                    <div className="col-sm-7">
                        <div className="top_album_name">
                            <span>G-Eazy</span><br/>
                            When It's Dark Out<br/>
                            <span>2015</span>
                        </div>
                    </div>
                </div>
            </div>

            <div className="col-sm-5 home_section">
                <div className="section_name"> Favorite Song</div>
                <div className="top_song">
                    <div className="col-sm-5">
                        <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/whenDarkOut.jpg"
                             alt="When It's Dark Out"/>
                    </div>
                    <div className="col-sm-7">
                        <div className="top_song_name">
                            <span>G-Eazy</span><br/>
                            Random<br/>
                            <span>When It's Dark Out</span>
                        </div>
                    </div>
                </div>
            </div>

            <div className="col-sm-5 home_section">
                <div className="section_name"> Last played song</div>
                <div className="top_song">
                    <div className="col-sm-5">
                        <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/whenDarkOut.jpg"
                             alt="When It's Dark Out"/>
                    </div>
                    <div className="col-sm-7">
                        <div className="top_song_name">
                            <span>G-Eazy</span><br/>
                            Random<br/>
                            <span>When It's Dark Out</span>
                        </div>
                    </div>
                </div>
            </div>


        </div>

    );
};

export default ContentMain;