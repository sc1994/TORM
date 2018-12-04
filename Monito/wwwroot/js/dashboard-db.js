Vue.component('dashboard-db',
    function (resolve) {
        window.axios.get("/js/dashboard-db.html").then(function (res) {
            resolve({
                template: res.data,
                props: ["start", "end"],
                data() {
                    return {
                        date: [],
                        data: [],
                        dataTyps: []
                    }
                },
                mounted() {
                    var that = this;
                    window.axios.post(`Dashboard/GetDbData?start=${this.start}&end=${this.end}`, {
                    }).then(function (response) {
                        var myChart = window.echarts.init(document.getElementById('main-db'));

                        that.date = response.data.item3;
                        that.data = response.data.item2;

                        var option = {
                            tooltip: {
                                trigger: 'axis',
                                position: function (pt) {
                                    return [pt[0], '10%'];
                                }
                            },
                            title: {
                                left: 'center',
                                text: '各库的请求量'
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
                                boundaryGap: [0, '0%']
                            },
                            dataZoom: [{
                                type: 'inside',
                                start: response.data.item4[0],
                                end: response.data.item4[1]
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
                            series: that.data
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