Vue.component('dashboard-all',
    function (resolve) {
        window.axios.get("/js/dashboard/all.html").then(function (res) {
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
                    var that = this;
                    window.axios.post(`Dashboard/GetAllData?start=${this.start}&end=${this.end}`, {
                    }).then(function (response) {
                        var myChart = window.echarts.init(document.getElementById('main-all'));

                        that.date = response.data.date;
                        that.data = response.data.data;

                        var option = {
                            tooltip: {
                                trigger: 'axis',
                                position: function (pt) {
                                    return [pt[0], '10%'];
                                }
                            },
                            title: {
                                left: 'center',
                                text: `总请求量/粒度:${response.data.particle}s`
                            },
                            toolbox: {
                                feature: {
                                    dataZoom: {
                                        yAxisIndex: 'none'
                                    },
                                    restore: {},
                                    saveAsImage: {}
                                }
                            },
                            xAxis: {
                                type: 'category',
                                boundaryGap: false,
                                data: that.date
                            },
                            yAxis: {
                                type: 'value',
                                boundaryGap: [0, '10%']
                            },
                            dataZoom: [{
                                type: 'inside',
                                start: 0,
                                end: 100
                            }, {
                                start: 0,
                                end: 10,
                                handleIcon: 'M10.7,11.9v-1.3H9.3v1.3c-4.9,0.3-8.8,4.4-8.8,9.4c0,5,3.9,9.1,8.8,9.4v1.3h1.3v-1.3c4.9-0.3,8.8-4.4,8.8-9.4C19.5,16.3,15.6,12.2,10.7,11.9z M13.3,24.4H6.7V23h6.6V24.4z M13.3,19.6H6.7v-1.4h6.6V19.6z',
                                handleSize: '70%',
                                handleStyle: {
                                    color: '#fff',
                                    shadowBlur: 3,
                                    shadowColor: 'rgba(0, 0, 0, 0.3)',
                                    shadowOffsetX: 2,
                                    shadowOffsetY: 2
                                }
                            }],
                            series: [
                                {
                                    name: '请求量',
                                    type: 'line',
                                    smooth: true,
                                    symbol: 'none',
                                    sampling: 'average',
                                    itemStyle: {
                                        color: '#2962ff'
                                    },
                                    areaStyle: {
                                        color: new window.echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                            offset: 0,
                                            color: "#2962ff"
                                        }, {
                                            offset: 1,
                                            color: '#4fc3f7'
                                        }])
                                    },
                                    data: that.data
                                }
                            ]
                        };
                        // 使用刚指定的配置项和数据显示图表。
                        myChart.setOption(option);
                    }).catch(function (error) {
                        console.log(error);
                    });
                }
            });
        });
    });