import React from 'react';
import '../css/main_styles.css';
import '../css/responsive.css';
import Chart from 'chart.js/dist/Chart.js';


class SideMenuPanel2 extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    componentDidMount() {
        let ctx = document.getElementById('system').getContext('2d');
        let ctx2 = document.getElementById('storage').getContext('2d');
        let ctx3 = document.getElementById('temp').getContext('2d');
        let chart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: ["1", "2", "3", "4", "5", "6", "7"],
                datasets: [{
                    label: 'CPU usage',

                    borderColor: '#107dac',
                    data: [0, 10, 5, 2, 20, 30, 45],
                },
                    {
                        label: 'RAM usage',
                        borderColor: "#aaaaaa",
                        data: [1, 15, 20, 35, 50, 70, 10],
                    }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            fontColor: '#aaaaaa'
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            fontColor: '#181818'
                        }
                    }]
                },
                legend: {
                    labels: {
                        fontColor: '#aaaaaa'
                    }
                }
            }
        });
        let chart2 = new Chart(ctx2, {
            type: 'doughnut',
            data: {
                labels: ['used (60%)', 'free (40%)'],
                datasets: [{

                    data: [60, 40],
                    backgroundColor: ['#107dac', 'transparent'],
                    borderColor: '#107dac'
                }],

            },
            options: {
                title: {
                    display: true,
                    text: 'Storage'
                }
            }
        });
        let chart3 = new Chart(ctx3, {
            type: 'line',
            data: {
                labels: ["1", "2", "3", "4", "5", "6", "7"],
                datasets: [{
                    label: 'CPU temp',

                    borderColor: '#107dac',
                    data: [0, 10, 5, 2, 20, 30, 45],
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            fontColor: '#aaaaaa'
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            fontColor: '#181818'
                        }
                    }]
                },
                legend: {
                    labels: {
                        fontColor: '#aaaaaa'
                    }
                }
            }
        });
    }

    render() {


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

                        <canvas id="system" style={{height: "300px;", width: "300px;"}}/>
                        <canvas id="temp" style={{height: "300px;", width: "300px;"}}/>
                        <canvas id="storage" style={{height: "300px;", width: "300px;"}}/>
                    </div>
                </div>

            </div>
        );
    }
}

export default SideMenuPanel2;
