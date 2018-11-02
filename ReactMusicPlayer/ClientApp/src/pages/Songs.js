import HeaderBar from "../components/Header";
import SideMenuPanel from "../components/LeftSideMenu";
import MainContent from "../components/MainContent";
import SideMenuPanel2 from "../components/RightSideMenu";
import MusicPlayer from "../components/Player";
import React from "react";
import SongList from "../components/SongList";


const Songs = ({props}) => {
    return (<div style={{height: "100%"}}>
        <HeaderBar/>
        <section className="content">
            <SideMenuPanel/>
            <SongList/>
            <SideMenuPanel2/>
        </section>
        <MusicPlayer/>
    </div>);
};

export default Songs;