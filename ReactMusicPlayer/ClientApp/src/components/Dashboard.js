import React from 'react';
import '../css/main_styles.css';
import '../css/responsive.css';
import Chart from "chart.js/dist/Chart";
import {itemService} from "../Service";

class Dashboard extends React.Component {
    constructor(props) {
        super(props);
        this.state = {library_info: []};
    }

    async componentDidMount() {

        function Charts(cpuUsage, ramUsage, cpuTemp, storage) {

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
                        /*data: [0, 10, 5, 2, 20, 30, 45],*/
                        data: cpuUsage,
                    },
                        {
                            label: 'RAM usage',
                            borderColor: "#aaaaaa",
                            /*data: [1, 15, 20, 35, 50, 70, 10],*/
                            data: ramUsage,
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
                    },
                    animation: {
                        duration: 0
                    }
                }
            });
            let chart2 = new Chart(ctx2, {
                type: 'doughnut',
                data: {
                    labels: ["Used (" + storage + "%)", "Free (" + (100 - storage) + "%)"],
                    datasets: [{

                        data: [storage, 100 - storage],
                        backgroundColor: ['#107dac', 'transparent'],
                        borderColor: '#107dac'
                    }],

                },
                options: {
                    title: {
                        display: true,
                        text: 'Storage'
                    },
                    animation: {
                        duration: 0
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
                        /*data: [0, 10, 5, 2, 20, 30, 45],*/
                        data: cpuTemp,
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
                    },
                    animation: {
                        duration: 0
                    }
                }
            });
        }

        itemService.getLibraryInfo().then((data) => {
            this.setState({library_info: data});
        });

        let cpuUsage = [];
        let ramUsage = [];
        let cpuTemp = [];
        let storage = 0;

        let sys_info = await itemService.getSystemInfo();
        console.log("System info", sys_info);

        cpuUsage.push(sys_info.cU);
        ramUsage.push(sys_info.rU);
        cpuTemp.push(sys_info.cT);
        storage = sys_info.dU;

        let chart = Charts(cpuUsage, ramUsage, cpuTemp, storage);
        {
            chart
        }

        const interval = require('interval-promise')

        // Run a function infinite times with 10 seconds between each iteration
        interval(async () => {
            let sys_info = await itemService.getSystemInfo();
            console.log("System info", sys_info);

            if (cpuUsage.length > 6) {
                cpuUsage = cpuUsage.slice(1, cpuUsage.length);
                cpuUsage.push(sys_info.cU)
            }
            else {
                cpuUsage.push(sys_info.cU);
            }

            if (ramUsage.length > 6) {
                ramUsage = ramUsage.slice(1, ramUsage.length);
                ramUsage.push(sys_info.rU)
            }
            else {
                ramUsage.push(sys_info.rU);
            }

            if (cpuTemp.length > 6) {
                cpuTemp = cpuTemp.slice(1, cpuTemp.length);
                cpuTemp.push(sys_info.cT)
            }
            else {
                cpuTemp.push(sys_info.cT);
            }

            storage = sys_info.dU;

            let chart = Charts(cpuUsage, ramUsage, cpuTemp, storage);
            {
                chart
            }

        }, 5000, {})
    }

    render() {
        function AllSongsLength(props) {
            console.log("Songs length", props.props);

            let length = props.props;
            let minutes = Math.floor(length / 60);
            let hours = Math.floor(minutes / 60);
            let days = Math.floor(hours / 24);

            let finalDays = days;
            let finalHours = hours - (24 * days);
            let finalMinutes = minutes - (hours * 60);
            let finalSeconds = length - (minutes * 60);

            console.log("Days ", finalDays, " Hours ", finalHours, " Minutes ", finalMinutes, " Seconds ", finalSeconds);

            return (<div className="col-sm-4"><i
                className="far fa-clock"/> Total length: {finalDays}d {finalHours}h {finalMinutes}min {finalSeconds}s
            </div>);
        }

        return (
            <div style={{height: "100%"}}>

                <div className="col-sm-11 dashboard_section">
                    <div className="section_name">Songs info</div>
                    <AllSongsLength props={this.state.library_info.tL}/>
                    <div className="col-sm-4">
                        <i className="fas fa-music"/> Songs: {this.state.library_info.sC}
                    </div>
                    <div className="col-sm-4">
                        <i className="fas fa-user"/> Artists: {this.state.library_info.arC}
                    </div>
                    <div className="col-sm-4">
                        <i className="fas fa-compact-disc"/> Albums: {this.state.library_info.alC}
                    </div>
                    <div className="col-sm-4">
                        <i className="fas fa-list-ul"/> Playlists: {this.state.library_info.pC}
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