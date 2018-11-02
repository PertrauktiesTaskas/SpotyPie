import React from 'react';
import '../css/main_styles.css';
import '../css/responsive.css';

const SideMenuPanel2 = ({props}) => {
    return (
        <div className="content__right">

            <div className="dashboard">

                <div className="dashboard_items">

                    <span className="dashboard_item">
                        <i className="far fa-clock"/> Total length: 2h 15min
                    </span>

                    <span className="dashboard_item">
                        <i className="fas fa-music"/> Songs: 15
                    </span>

                    <span className="dashboard_item">
                        <i className="fas fa-user"/> Artists: 5
                    </span>

                    <span className="dashboard_item">
                        <i className="fas fa-compact-disc"/> Albums: 3
                    </span>

                    <span className="dashboard_item">
                        <i className="fas fa-list-ul"/> Playlists: 2
                    </span>

                </div>
            </div>

        </div>
    );
}

export default SideMenuPanel2;
