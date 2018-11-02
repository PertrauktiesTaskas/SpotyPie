import React from 'react';
import '../css/main_styles.css';
import '../css/responsive.css';
import Chart from "chart.js/dist/Chart";

class Dashboard extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    componentDidMount() {
        var ctx = document.getElementById('system').getContext('2d');
        var ctx2 = document.getElementById('storage').getContext('2d');
        var ctx3 = document.getElementById('temp').getContext('2d');
        var chart = new Chart(ctx, {
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
                        borderColor: "red",
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
                            fontColor: '#aaaaaa'
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
        var chart2 = new Chart(ctx2, {
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
        var chart3 = new Chart(ctx3, {
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
                            fontColor: '#aaaaaa'
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
            <div style={{height: "100%"}}>

                <div className="col-sm-11 dashboard_section">
                    <div className="section_name">Songs info</div>
                    <div className="col-sm-4">
                        <div><i className="far fa-clock"/> Total length: 2h 15min</div>
                    </div>
                    <div className="col-sm-4">
                        <div><i className="fas fa-music"/> Songs: 15</div>
                    </div>
                    <div className="col-sm-4">
                        <div><i className="fas fa-user-alt"/> Artists: 5</div>
                    </div>
                    <div className="col-sm-4">
                        <div><i className="fas fa-compact-disc"/> Albums: 3</div>
                    </div>
                    <div className="col-sm-4">
                        <div><i className="fas fa-list-ul"/> Playlists: 3</div>
                    </div>
                </div>

                <div className="col-sm-11 dashboard_section">
                    <div className="section_name">System info</div>
                    <div className="col-sm-4">
                        <canvas id="system" className="dashboard_graph" style={{height: "200px;", width: "200px;"}}/>
                    </div>
                    <div className="col-sm-4">
                        <canvas id="temp" className="dashboard_graph" style={{height: "200px;", width: "200px;"}}/>
                    </div>
                    <div className="col-sm-4">
                        <canvas id="storage" className="dashboard_graph" style={{height: "200px;", width: "200px;"}}/>
                    </div>
                </div>


            </div>

        );
    }
}

export default Dashboard;