import React from 'react';
import '../css/main_styles.css';
import '../css/responsive.css';
import Spotipie from '../img/spotipie_blue.png';

const HeaderBar = ({props}) => {
    return (<div>
        <section className="header">
            <div className="page-flows">
                <img className="logo" src={Spotipie} alt=""/>
                <div href="/" className="page_title">SpotiPie</div>
            </div>

            {/*<div className="user">

                <div className="user__inbox">
                    <i className="fas fa-download"/>
                </div>

            </div>*/}

        </section>
    </div>);
}

export default HeaderBar;