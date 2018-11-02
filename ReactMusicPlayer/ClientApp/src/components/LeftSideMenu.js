import React from 'react';
import '../css/main_styles.css';
import '../css/responsive.css';

const SideMenuPanel = ({props}) => {
    return (

        <div className="content__left">

            <section className="navigation">

                <div className="navigation__list">
                    <div className="navigation__list__header">
                        <i className="fas fa-home"/> Home
                    </div>

                </div>

                <div className="navigation__list">
                    <div className="navigation__list__header" role="button" data-toggle="collapse"
                         aria-expanded="true" aria-controls="yourMusic">
                        Your Music
                    </div>
                    <div className="collapse in" id="yourMusic">
                        <a href="#" className="navigation__list__item">
                            <i className="fas fa-headphones"/>
                            <span>Songs</span>
                        </a>
                        <a href="#" className="navigation__list__item">
                            <i className="fas fa-compact-disc"/>
                            <span>Albums</span>
                        </a>
                        <a href="#" className="navigation__list__item">
                            <i className="fas fa-user"/>
                            <span>Artists</span>
                        </a>
                        <a href="#" className="navigation__list__item">
                            <i className="fas fa-file"/>
                            <span>Local Files</span>
                        </a>
                    </div>
                </div>

                <div className="navigation__list">
                    <div className="navigation__list__header" role="button" data-toggle="collapse"
                         aria-expanded="true" aria-controls="playlists">
                        Playlists
                    </div>
                    <div className="collapse in" id="playlists">
                        <a href="#" className="navigation__list__item">
                            <i className="fas fa-music"/>
                            <span>Doo Wop</span>
                        </a>
                        <a href="#" className="navigation__list__item">
                            <i className="fas fa-music"/>
                            <span>Pop Classics</span>
                        </a>
                    </div>
                </div>

            </section>

            <section className="playlist">
                <a href="#">
                    <i className="fas fa-plus"/>
                    New Playlist
                </a>
            </section>

            <section className="playing">
                <div className="playing__art">
                    <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/cputh.jpg" alt="Album Art"/>
                </div>
                <div className="playing__song">
                    <span className="playing__song__name"><i className="fas fa-music"/> Some Type of Love</span>
                    <span className="playing__song__artist"><i className="fas fa-user"/> Charlie Puth</span>
                    <span className="playing__song__album"><i className="fas fa-compact-disc"/> Album</span>
                </div>

            </section>

        </div>
    );
};

export default SideMenuPanel;