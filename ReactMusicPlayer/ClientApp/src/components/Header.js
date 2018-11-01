import React from 'react';
import '../css/main_styles.css';
import '../css/responsive.css';

const HeaderBar = ({props}) => {
    return (<div>
        <section className="header">
            <div className="page-flows">
                <a href="/" className="page_title">SpotiPie</a>
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