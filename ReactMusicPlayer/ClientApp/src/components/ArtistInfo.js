import React from 'react';
import '../css/main_styles.css';
import '../css/responsive.css';

class ArtistInfo extends React.Component {
    constructor(props) {
        super(props);
        console.log(props);

        this.state = {
            show_album_songs: false,
            selected_album: "",
            button_text: "view"
        };

        this.handleClick = this.handleClick.bind(this);
    }

    handleClick(event) {
        console.log("Item clicked", event.target.innerText)
        switch (event.target.innerText) {
            case "VIEW":
                this.setState({
                    show_album_songs: true,
                    selected_album: event.target.id,
                    button_text: "hide"
                });
                break;
            default:
                this.setState({
                    show_album_songs: false,
                    selected_album: "",
                    button_text: "view"
                });
                break;
        }

    }

    render() {

        function AlbumTracks(props) {
            return (
                <div className="album__tracks">

                    <div className="tracks">

                        <div className="artist__album__tracks__heading">

                            <div className="artist__album__tracks__heading__number">#</div>

                            <div className="artist__album__tracks__heading__title">Song</div>

                            <div className="artist__album__tracks__heading__length">

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

                        <div className="track">

                            <div className="track__number">2</div>

                            <div className="track__title">Random</div>

                            <div className="track__explicit">

                                <span className="label">Explicit</span>

                            </div>

                            <div className="track__length"><i className="far fa-clock"/> 3:00</div>

                        </div>

                        <div className="track">

                            <div className="track__number">3</div>

                            <div className="track__title featured">

                                <span className="title">Me, Myself & I</span>
                                <span className="feature">Bebe Rexha</span>

                            </div>

                            <div className="track__explicit">

                                <span className="label">Explicit</span>

                            </div>

                            <div className="track__length"><i className="far fa-clock"/> 4:11</div>


                        </div>

                        <div className="track">

                            <div className="track__number">4</div>

                            <div className="track__title featured">

                                <span className="title">One Of Them</span>
                                <span className="feature">Big Sean</span>

                            </div>

                            <div className="track__explicit">

                                <span className="label">Explicit</span>

                            </div>

                            <div className="track__length"><i className="far fa-clock"/> 3:20</div>

                        </div>

                        <div className="track">

                            <div className="track__number">5</div>

                            <div className="track__title featured">

                                <span className="title">Drifting</span>
                                <span className="feature">Chris Brown</span>
                                <span className="feature">Tory Lanez</span>

                            </div>

                            <div className="track__explicit">

                                <span className="label">Explicit</span>

                            </div>

                            <div className="track__length"><i className="far fa-clock"/> 4:33</div>

                        </div>

                        <div className="track">

                            <div className="track__number">6</div>

                            <div className="track__title featured">

                                <span className="title">Of All Things</span>
                                <span className="feature">Too $hort</span>

                            </div>

                            <div className="track__explicit">

                                <span className="label">Explicit</span>

                            </div>

                            <div className="track__length"><i className="far fa-clock"/> 3:34</div>

                        </div>

                        <div className="track">

                            <div className="track__number">7</div>

                            <div className="track__title featured">

                                <span className="title">Order More</span>
                                <span className="feature">Starrah</span>

                            </div>

                            <div className="track__explicit">

                                <span className="label">Explicit</span>

                            </div>

                            <div className="track__length"><i className="far fa-clock"/> 3:29</div>

                        </div>

                        <div className="track">

                            <div className="track__number">8</div>

                            <div className="track__title">

                                <span>Calm Down</span>

                            </div>

                            <div className="track__explicit">

                                <span className="label">Explicit</span>

                            </div>

                            <div className="track__length"><i className="far fa-clock"/> 2:07</div>

                        </div>

                        <div className="track">

                            <div className="track__number">9</div>

                            <div className="track__title featured">

                                <span className="title">Don't Let Me Go</span>
                                <span className="feature">Grace</span>

                            </div>

                            <div className="track__explicit">

                                <span className="label">Explicit</span>

                            </div>

                            <div className="track__length"><i className="far fa-clock"/> 3:11</div>

                        </div>

                        <div className="track">

                            <div className="track__number">10</div>

                            <div className="track__title">

                                <span>You Got Me</span>

                            </div>

                            <div className="track__explicit">

                                <span className="label">Explicit</span>

                            </div>

                            <div className="track__length"><i className="far fa-clock"/> 3:28</div>

                        </div>

                        <div className="track">

                            <div className="track__number">11</div>

                            <div className="track__title featured">

                                <span className="title">What If</span>
                                <span className="feature">Gizzle</span>

                            </div>

                            <div className="track__explicit">

                                <span className="label">Explicit</span>

                            </div>

                            <div className="track__length"><i className="far fa-clock"/> 4:13</div>

                        </div>

                        <div className="track">

                            <div className="track__number">12</div>

                            <div className="track__title">

                                <span>Sad Boy</span>

                            </div>

                            <div className="track__explicit">

                                <span className="label">Explicit</span>

                            </div>

                            <div className="track__length"><i className="far fa-clock"/> 3:23</div>

                        </div>

                        <div className="track">

                            <div className="track__number">13</div>

                            <div className="track__title featured">

                                <span className="title">Some Kind Of Drug</span>
                                <span className="feature">Marc E. Bassy</span>

                            </div>

                            <div className="track__explicit">

                                <span className="label">Explicit</span>

                            </div>

                            <div className="track__length"><i className="far fa-clock"/> 3:42</div>

                        </div>

                        <div className="track">

                            <div className="track__number">14</div>

                            <div className="track__title featured">

                                <span className="title">Think About You</span>
                                <span className="feature">Quin</span>

                            </div>

                            <div className="track__explicit">

                                <span className="label">Explicit</span>

                            </div>

                            <div className="track__length"><i className="far fa-clock"/> 2:59</div>


                        </div>

                        <div className="track">

                            <div className="track__number">15</div>

                            <div className="track__title featured">

                                <span className="title">Everything Will Be OK</span>
                                <span className="feature">Kehlani</span>

                            </div>

                            <div className="track__explicit">

                                <span className="label">Explicit</span>

                            </div>

                            <div className="track__length"><i className="far fa-clock"/> 5:11</div>

                        </div>

                        <div className="track">

                            <div className="track__number">16</div>

                            <div className="track__title featured">

                                <span className="title">For This</span>
                                <span className="feature">Iamnobodi</span>

                            </div>

                            <div className="track__explicit">

                                <span className="label">Explicit</span>

                            </div>

                            <div className="track__length"><i className="far fa-clock"/> 4:11</div>

                        </div>

                        <div className="track">

                            <div className="track__number">17</div>

                            <div className="track__title featured">

                                <span className="title">Nothing to Me</span>
                                <span className="feature">Keyshia Cole</span>
                                <span className="feature">E-40</span>

                            </div>

                            <div className="track__explicit">

                                <span className="label">Explicit</span>

                            </div>

                            <div className="track__length"><i className="far fa-clock"/> 5:30</div>

                        </div>

                    </div>

                </div>


            );
        }

        let album_songs = this.state.show_album_songs ? <AlbumTracks/> : null;

        return (
            <div style={{height: "100%"}}>

                <div className="artist">

                    <div className="artist__header">

                        <div className="artist__info">

                            <div className="profile__img">

                                <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/g_eazy_propic.jpg"
                                     alt="G-Eazy"/>

                            </div>

                            <div className="artist__info__meta">

                                <div className="artist__info__name">G-Eazy</div>

                            </div>

                        </div>

                        {/*<div className="artist__listeners">

                            <div className="artist__listeners__count">15,662,810</div>

                            <div className="artist__listeners__label">Monthly Listeners</div>

                        </div>*/}

                        <div className="artist__navigation">

                            <ul className="nav nav-tabs" role="tablist">

                                <li role="presentation" className="active">
                                    <a href="#artist-overview" aria-controls="artist-overview" role="tab"
                                       data-toggle="tab">Overview</a>
                                </li>

                                <li role="presentation">
                                    <a href="#related-artists" aria-controls="related-artists" role="tab"
                                       data-toggle="tab">Related
                                        Artists</a>
                                </li>

                            </ul>

                            <div className="artist__navigation__friends">

                                <a href="#">
                                    <img src="http://zblogged.com/wp-content/uploads/2015/11/17.jpg" alt=""/>
                                </a>

                                <a href="#">
                                    <img src="http://zblogged.com/wp-content/uploads/2015/11/5.png" alt=""/>
                                </a>

                                <a href="#">
                                    <img
                                        src="http://cdn.devilsworkshop.org/files/2013/01/enlarged-facebook-profile-picture.jpg"
                                        alt=""/>
                                </a>

                            </div>

                        </div>

                    </div>

                    <div className="artist__content">

                        <div className="tab-content">

                            <div role="tabpanel" className="tab-pane active" id="artist-overview">

                                <div className="overview">

                                    <div className="overview__artist">


                                        <div className="section-title">Latest Release</div>

                                        <div className="latest-release">

                                            <div className="latest-release__art">

                                                <img
                                                    src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/whenDarkOut.jpg"
                                                    alt="When It's Dark Out"/>

                                            </div>

                                            <div className="latest-release__song">

                                                <div className="latest-release__song__title">Drifting (Track Commentary)
                                                </div>

                                                <div className="latest-release__song__date">

                                                    <span className="day">4</span>

                                                    <span className="month">December</span>

                                                    <span className="year">2015</span>

                                                </div>

                                            </div>

                                        </div>

                                        <div className="section-title">Popular</div>

                                        <div className="tracks">

                                            <div className="track">

                                                <div className="track__art">

                                                    <img
                                                        src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/whenDarkOut.jpg"
                                                        alt="When It's Dark Out"/>

                                                </div>

                                                <div className="track__number">1</div>

                                                <div className="track__added">

                                                    <i className="ion-checkmark-round added"></i>

                                                </div>

                                                <div className="track__title">Me, Myself & I</div>

                                                <div className="track__explicit">

                                                    <span className="label">Explicit</span>

                                                </div>

                                                <div className="track__plays">147,544,165</div>

                                            </div>

                                            <div className="track">

                                                <div className="track__art">

                                                    <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/tth.jpg"
                                                         alt="These Things Happen"/>

                                                </div>

                                                <div className="track__number">2</div>

                                                <div className="track__added">

                                                    <i className="ion-plus not-added"></i>

                                                </div>

                                                <div className="track__title">I Mean It</div>

                                                <div className="track__explicit">

                                                    <span className="label">Explicit</span>

                                                </div>

                                                <div className="track__plays">74,568,782</div>

                                            </div>

                                            <div className="track">

                                                <div className="track__art">

                                                    <img
                                                        src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/whenDarkOut.jpg"
                                                        alt="When It's Dark Out"/>

                                                </div>

                                                <div className="track__number">3</div>

                                                <div className="track__added">

                                                    <i className="ion-checkmark-round added"></i>

                                                </div>

                                                <div className="track__title">Calm Down</div>

                                                <div className="track__explicit">

                                                    <span className="label">Explicit</span>

                                                </div>

                                                <div className="track__plays">13,737,506</div>

                                            </div>

                                            <div className="track">

                                                <div className="track__art">

                                                    <img
                                                        src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/whenDarkOut.jpg"
                                                        alt="When It's Dark Out"/>

                                                </div>

                                                <div className="track__number">4</div>

                                                <div className="track__added">

                                                    <i className="ion-plus not-added"></i>

                                                </div>

                                                <div className="track__title">Some Kind Of Drug</div>

                                                <div className="track__explicit">

                                                    <span className="label">Explicit</span>

                                                </div>

                                                <div className="track__plays">12,234,881</div>

                                            </div>

                                            <div className="track">

                                                <div className="track__art">

                                                    <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/tth.jpg"
                                                         alt="These Things Happen"/>

                                                </div>

                                                <div className="track__number">5</div>

                                                <div className="track__added">

                                                    <i className="ion-checkmark-round added"></i>

                                                </div>

                                                <div className="track__title">Let's Get Lost</div>

                                                <div className="track__explicit">

                                                    <span className="label">Explicit</span>

                                                </div>

                                                <div className="track__plays">40,882,954</div>

                                            </div>

                                        </div>

                                    </div>

                                    <div className="overview__related">

                                        <div className="section-title">Related Artists</div>

                                        <div className="related-artists">

                                            <a href="#" className="related-artist">
                  
                    <span className="related-artist__img">
                    
                      <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/hoodie.jpg" alt="Hoodie Allen"/>
                      
                    </span>

                                                <span className="related-artist__name">Hoodie Allen</span>

                                            </a>

                                            <a href="#" className="related-artist">
                  
                    <span className="related-artist__img">
                    
                      <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/mikestud.jpg" alt="Mike Stud"/>
                      
                    </span>

                                                <span className="related-artist__name">Mike Stud</span>

                                            </a>

                                            <a href="#" className="related-artist">
                  
                    <span className="related-artist__img">
                    
                      <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/drake.jpeg" alt="Drake"/>
                      
                    </span>

                                                <span className="related-artist__name">Drake</span>

                                            </a>

                                            <a href="#" className="related-artist">
                  
                    <span className="related-artist__img">
                    
                      <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/jcole.jpg" alt="J. Cole"/>
                      
                    </span>

                                                <span className="related-artist__name">J. Cole</span>

                                            </a>

                                            <a href="#" className="related-artist">
                  
                    <span className="related-artist__img">
                    
                      <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/bigsean.jpg" alt="Big Sean"/>
                      
                    </span>

                                                <span className="related-artist__name">Big Sean</span>

                                            </a>

                                            <a href="#" className="related-artist">
                  
                    <span className="related-artist__img">
                    
                      <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/wiz.jpeg" alt="Wiz Khalifa"/>
                      
                    </span>

                                                <span className="related-artist__name">Wiz Khalifa</span>

                                            </a>

                                            <a href="#" className="related-artist">
                  
                    <span className="related-artist__img">
                    
                      <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/yonas.jpg" alt="Yonas"/>
                      
                    </span>

                                                <span className="related-artist__name">Yonas</span>

                                            </a>

                                        </div>

                                    </div>

                                    <div className="overview__albums" style={{marginBottom: "20px"}}>

                                        <div className="overview__albums__head">

                                            <span className="section-title">Albums</span>


                                        </div>

                                        <div className="album">

                                            <div className="album__info">

                                                <div className="album__info__art">

                                                    <img
                                                        src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/whenDarkOut.jpg"
                                                        alt="When It's Dark Out"/>

                                                </div>

                                                <div className="album__info__meta">

                                                    <div className="album__year">2015</div>

                                                    <div className="album__name">When It's Dark Out</div>

                                                    <div className="album__actions">

                                                        <button id="When It's Dark Out" className="button-light save"
                                                                onClick={this.handleClick.bind(this)}>{this.state.button_text}
                                                        </button>

                                                    </div>

                                                </div>

                                            </div>

                                            {album_songs}

                                        </div>

                                    </div>

                                </div>

                            </div>

                            <div role="tabpanel" className="tab-pane" id="related-artists">

                                <div className="media-cards">

                                    <div className="media-card">

                                        <div className="media-card__image"
                                             style={{backgroundImage: "url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/hoodie.jpg)"}}>

                                            <i className="ion-ios-play"></i>

                                        </div>

                                        <a className="media-card__footer">Hoodie Allen</a>

                                    </div>

                                    <div className="media-card">

                                        <div className="media-card__image"
                                             style={{backgroundImage: "url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/mikestud_large.jpg)"}}>

                                            <i className="ion-ios-play"></i>

                                        </div>

                                        <a className="media-card__footer">Mike Stud</a>

                                    </div>

                                    <div className="media-card">

                                        <div className="media-card__image"
                                             style={{backgroundImage: "url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/drake_large.jpg)"}}>

                                            <i className="ion-ios-play"></i>

                                        </div>

                                        <a className="media-card__footer">Drake</a>

                                    </div>

                                    <div className="media-card">

                                        <div className="media-card__image"
                                             style={{backgroundImage: "url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/jcole_large.jpg)"}}>

                                            <i className="ion-ios-play"></i>

                                        </div>

                                        <a className="media-card__footer">J. Cole</a>

                                    </div>

                                    <div className="media-card">

                                        <div className="media-card__image"
                                             style={{backgroundImage: "url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/bigSean_large.jpg)"}}>

                                            <i className="ion-ios-play"></i>

                                        </div>

                                        <a className="media-card__footer">Big Sean</a>

                                    </div>

                                    <div className="media-card">

                                        <div className="media-card__image"
                                             style={{backgroundImage: "url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/wiz.jpeg)"}}>

                                            <i className="ion-ios-play"></i>

                                        </div>

                                        <a className="media-card__footer">Wiz Khalifa</a>

                                    </div>

                                    <div className="media-card">

                                        <div className="media-card__image"
                                             style={{backgroundImage: "url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/yonas.jpg)"}}>

                                            <i className="ion-ios-play"></i>

                                        </div>

                                        <a className="media-card__footer">Yonas</a>

                                    </div>

                                    <div className="media-card">

                                        <div className="media-card__image"
                                             style={{backgroundImage: "url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/7022/childish.jpg)"}}>

                                            <i className="ion-ios-play"></i>

                                        </div>

                                        <a className="media-card__footer">Childish Gambino</a>

                                    </div>

                                </div>

                            </div>

                        </div>

                    </div>

                </div>

            </div>

        );
    }
}

export default ArtistInfo;