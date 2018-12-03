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
                        that.dataTyps = response.data.item1;
                        that.data = response.data.item2;
                        that.date = response.data.item3;

                        var myChart = window.echarts.init(document.getElementById('main-db'));

                        var option = {
                            title: {
                                text: '每个库的请求量'
                            },
                            tooltip: {
                                trigger: 'axis',
                                axisPointer: {
                                    type: 'cross',
                                    label: {
                                        backgroundColor: '#6a7985'
                                    }
                                }
                            },
                            legend: {
                                data: that.dataTyps
                            },
                            toolbox: {
                                feature: {
                                    saveAsImage: {}
                                }
                            },
                            grid: {
                                left: '3%',
                                right: '4%',
                                bottom: '3%',
                                containLabel: true
                            },
                            xAxis: [
                                {
                                    type: 'category',
                                    boundaryGap: false,
                                    data: that.date
                                }
                            ],
                            yAxis: [
                                {
                                    type: 'value'
                                }
                            ],
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