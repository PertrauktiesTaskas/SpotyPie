import React from 'react';
import '../css/main_styles.css';
import '../css/responsive.css';
import '../css/slider.css';
import Slider from "rc-slider";
import ReactPlayer from 'react-player'

class MusicPlayer extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            playing: false,
            volume: 1,
            muted_volume: 1,
            muted: false,
            played: "0:00",
            duration: "0:00",
            played_percentage: 0,
            playbackRate: 1.0,
            loop: false,
        }

        this.handleClick = this.handleClick.bind(this);
        this.handleVolume = this.handleVolume.bind(this);
        this.handleMute = this.handleMute.bind(this);
        this.setLoop = this.setLoop.bind(this);
        this.handleProgress = this.handleProgress.bind(this);
    }

    handleClick(event) {
        switch (event.target.id) {
            case "play/pause":
                console.log("Seconds played", this.player.getCurrentTime());
                this.setState({
                    playing: !this.state.playing
                });
                break;
            case "forward":
                this.player.seekTo(this.player.getCurrentTime() + 5);
                let minutes = Math.floor(this.player.getCurrentTime() / 60);
                let seconds = this.player.getCurrentTime() - 60 * minutes;
                if (seconds < 10) {
                    this.setState({
                        played: minutes + ":0" + Math.round(seconds),
                        played_percentage: this.player.getCurrentTime() / this.player.getDuration() * 100
                    });
                }
                else {
                    this.setState({
                        played: minutes + ":" + Math.round(seconds),
                        played_percentage: this.player.getCurrentTime() / this.player.getDuration() * 100
                    });
                }
                break;
            case "backward":
                this.player.seekTo(this.player.getCurrentTime() - 5);
                minutes = Math.floor(this.player.getCurrentTime() / 60);
                seconds = this.player.getCurrentTime() - 60 * minutes;
                if (seconds < 10) {
                    this.setState({
                        played: minutes + ":0" + Math.round(seconds),
                        played_percentage: this.player.getCurrentTime() / this.player.getDuration() * 100
                    });
                }
                else {
                    this.setState({
                        played: minutes + ":" + Math.round(seconds),
                        played_percentage: this.player.getCurrentTime() / this.player.getDuration() * 100
                    });
                }
                break;
            default:
                break;
        }

    }

    handleVolume(value) {
        console.log("Volume is:", value * 0.01);
        if (this.state.muted && value * 0.01 > 0) {
            this.setState({
                muted: false,
                volume: value * 0.01
            });
        }
        else if (value * 0.01 === 0) {
            this.setState({
                muted: true,
                volume: value * 0.01
            });
        }
        else if (value * 0.01 > 0) {
            this.setState({
                muted: false,
                volume: value * 0.01
            });
        }
    }

    handleMute() {
        if (this.state.muted) {
            console.log("Unmuting");
            this.setState({
                muted: false,
                volume: this.state.muted_volume
            });
        }
        else {
            console.log("Muting");
            this.setState({
                muted: true,
                muted_volume: this.state.volume,
                volume: 0
            });
        }
    }

    setLoop() {
        this.setState({loop: !this.state.loop});
    }

    SetSongInfo() {
        let length = Math.round(this.player.getDuration());
        let minutes = Math.round(length / 60);
        let seconds = length - 60 * minutes;
        console.log("Minutes", minutes);
        console.log("Seconds", seconds);
        if (seconds < 10) {
            this.setState({
                duration: minutes + ":0" + seconds
            });
        }
        else {
            this.setState({
                duration: minutes + ":" + seconds
            });
        }
    }

    handleProgress() {
        let minutes = Math.floor(this.player.getCurrentTime() / 60);
        let seconds = this.player.getCurrentTime() - 60 * minutes;
        if (seconds < 10) {
            this.setState({
                played: minutes + ":0" + Math.round(seconds),
                played_percentage: this.player.getCurrentTime() / this.player.getDuration() * 100
            });
        }
        else {
            this.setState({
                played: minutes + ":" + Math.round(seconds),
                played_percentage: this.player.getCurrentTime() / this.player.getDuration() * 100
            });
        }
    }

    handleSeek(value) {
        console.log("Seeking to", this.player.getDuration() / 100 * value);
        this.player.seekTo(this.player.getDuration() / 100 * value);
    }

    ref = player => {
        this.player = player
    }

    render() {

        let volume_icon = function (props, action) {
            if (props.volume >= 0.5) {
                return <i className="fas fa-volume-up" onClick={action}/>;
            }
            else if (props.volume < 0.5 && props.volume > 0) {
                return <i className="fas fa-volume-down" onClick={action}/>;
            }
            else if (props.volume === 0) {
                return <i className="fas fa-volume-off" onClick={action}/>;
            }

        };

        let playing_song = this.state.playing ?
            <a><i id="play/pause" className="fas fa-pause" onClick={this.handleClick.bind(this)}/></a> :
            <a><i id="play/pause" className="fas fa-play" onClick={this.handleClick.bind(this)}/></a>;

        let loop_enabled = this.state.loop ?
            <a className="control"><i className="fas fa-sync-alt" style={{color: "#107dac"}}
                                      onClick={this.setLoop.bind(this)}/></a> :
            <a className="control"><i className="fas fa-sync-alt"
                                      onClick={this.setLoop.bind(this)}/></a>;

        return (
            <div className="player_content">
                <section className="current-track">
                    <div className="current-track__actions">
                        <a><i id="backward" className="fas fa-backward" onClick={this.handleClick.bind(this)}/></a>
                        {playing_song}
                        <a><i id="forward" className="fas fa-forward" onClick={this.handleClick.bind(this)}/></a>
                    </div>

                    <div className="current-track__progress">
                        <div className="current-track__progress__start">{this.state.played}</div>
                        <div className="current-track__progress__bar">
                            <Slider style={{width: "100%"}}
                                    min={0}
                                    max={100}
                                    defaultValue={0}
                                    value={this.state.played_percentage}
                                    onChange={this.handleSeek.bind(this)}/>
                        </div>
                        <div className="current-track__progress__finish">{this.state.duration}</div>
                    </div>

                    <div className="current-track__options">

                        <span className="controls">
                           {loop_enabled}
                            <span href="#"
                                  className="control volume">{volume_icon(this.state, this.handleMute.bind(this))}<Slider
                                min={0}
                                max={100}
                                defaultValue={(this.state.volume * 100)}
                                value={this.state.volume * 100}
                                onChange={this.handleVolume.bind(this)}/></span>
                        </span>
                    </div>

                    <ReactPlayer url='http://spotypie.deveim.com/api/stream/play/542'
                                 ref={this.ref}
                                 playing={this.state.playing}
                                 controls={true}
                                 className="music_player"
                                 volume={this.state.volume}
                                 loop={this.state.loop}
                                 muted={this.state.muted}
                                 onStart={this.SetSongInfo.bind(this)}
                                 onProgress={this.handleProgress.bind(this)}/>

                </section>
            </div>);
    }
}

export default MusicPlayer;