Vue.component('dashboard-explain',
    function (resolve) {
        window.axios.get("/js/dashboard/explain.html").then(function (res) {
            resolve({
                template: res.data,
                props: ["start", "end"],
                data() {
                    return {
                        date: [],
                        data: []
                    }
                },
                mounted() {
                    window.axios.post(`Dashboard/GetExplainData?start=${this.start}&end=${this.end}`, {
                    }).then(function (response) {
                        window.console.log(response);
                        var myChart = window.echarts.init(document.getElementById('main-explain'));

                        var option = {
                            title: {
                                text: '解析表达式耗时/ms'
                            },
                            legend: {
                                data: response.data.dataType,
                                align: 'left'
                            },
                            toolbox: {
                                // y: 'bottom',
                                feature: {
                                    magicType: {
                                        type: ['stack', 'tiled']
                                    },
                                    dataView: {},
                                    saveAsImage: {
                                        pixelRatio: 2
                                    }
                                }
                            },
                            tooltip: {},
                            xAxis: {
                                data: response.data.dataType,
                                silent: false,
                                splitLine: {
                                    show: false
                                }
                            },
                            yAxis: {
                            },
                            series: response.data.data,
                            animationEasing: 'elasticOut',
                            animationDelayUpdate: function (idx) {
                                return idx * 5;
                            }
                        };
                        myChart.setOption(option);
                    }).catch(function (error) {
                        console.log(error);
                    });

                }
            });
        });
    });