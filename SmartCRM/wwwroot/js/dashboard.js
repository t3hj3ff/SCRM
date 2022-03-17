
function InitialzieVisitTurnovers(data) {
    var dom = document.getElementById("visit_turnovers");
    var visit_turnovers = echarts.init(dom);
    // var days = Array(data.length).fill().map((x, i) => i + 1);
    var days_empty_string = Array(data.length).fill().map((x, i) => '');

    var turnover_columns = data.map(function (item) {
        return Math.round(item.turnover);
    });

    var visits_columns = data.map(function (item) {
        return item.visit;
    });

    var days;
    var days = data.map(function (item) {
        return item.day;
    });

    var visits_sum_turnover_number = 0;
    var visits_sum_turnover = data.forEach(function (item) {
        visits_sum_turnover_number += parseFloat(item.turnover);
    });

    var visits_sum_visit_number = 0;
    var visits_sum_visit = data.forEach(function (item) {
        visits_sum_visit_number += parseFloat(item.visit);
    });

    document.getElementById("visits_total").innerHTML = Math.round(visits_sum_visit_number).toLocaleString();
    document.getElementById("sales_total").innerHTML = Math.round(visits_sum_turnover_number).toLocaleString();



    if (turnover_columns.length == 0) {
        document.getElementById("visit_turnovers").style.display = "none";
        document.getElementById("visit_turnovers_empty").style.display = "block";
    };

    option = null;
    option = {
        title: {
        },
        tooltip: {
            trigger: 'axis',
            formatter: function (params) {
                var _data = '';
                var _data_header = '';
                for (var i = 0; i < params.length; i++) {
                    _data_header = params[0].name + '<br/>';
                    _data += params[i].seriesName + ': ' + params[i].value.toLocaleString() + '<br/>';
                }
                return _data_header + _data;
            }
        },
        legend: {
            data: ['ვიზიტები', 'გაყიდვები']
        },
        xAxis: [
            {
                type: 'category',
                axisLine: { show: false },
                position: 'bottom',
                boundaryGap: true,
                axisLabel: {
                    show: true,
                    interval: 'auto',
                    margin: 8,

                },
                splitLine: {
                    show: true,
                    lineStyle: {
                        color: '#483d8b',
                        type: 'dotted',
                        width: 1
                    }
                },

                data: days
            },
            {
                axisLine: { show: false },
                type: 'category',
                data: days_empty_string
            }
        ],
        yAxis: [
            {
                type: 'value',
                name: 'გაყიდვები',
                show: true,
                position: 'left',
                splitNumber: 10,
                nameTextStyle: {
                    align: 'right'
                },
                axisLabel: {
                    show: true,
                    interval: 'auto',
                    rotate: 0,
                    margin: 18,

                }
            },
            {

                type: 'value',
                name: 'ვიზიტები',
                splitNumber: 10,
                nameTextStyle: {
                    align: 'left'
                },
                axisLabel: {

                },
                splitLine: {
                    show: false
                }
            }
        ],
        series: [
            {
                name: 'გაყიდვები',
                type: 'line',
                smooth: true,
                data: turnover_columns,
                itemStyle: {
                    normal: {
                        color: '#5B9BD5'
                    },
                    formatter: '{value}'
                }
            },
            {
                name: 'ვიზიტები',
                type: 'line',
                smooth: true,
                xAxisIndex: 0,
                yAxisIndex: 1,
                data: visits_columns,

                itemStyle: {
                    normal: {
                        color: '#24d2b5'//, areaStyle: { type: 'default' }
                    }
                },
            },
            {
                name: 'საშუალო ჩეკი',
                data: data.map(function (item) {
                    return item.avgAmount;
                }),
                type: 'scatter'
            }
        ]
    };

    if (option && typeof option === "object") {
        visit_turnovers.setOption(option, true), $(function () {
            function resize() {
                setTimeout(function () {
                    visit_turnovers.resize()
                }, 100)
            }
            $(window).on("resize", resize), $(".sidebartoggler").on("click", resize)
        });
    }



}

function InitialzieAges(data) {

    var ages_columns = data.map(function (item) {
        return [item.description, item.count];
    });

    var chart = c3.generate({
        tooltip: {
            format: {
                value: function (value, ratio, id, index) { return value; }
            }
        },
        bindto: '#ages',
        data: {
            columns:
                ages_columns,
            type: 'donut'
        },
        legend: {
            show: true,
            position: 'right'
        },
        donut: {
            label: {
                threshold: 0
            },
            title: "ასაკი",
            //width: 35,
        },
        color: {
            pattern: ['#ffb74d', '#cbd036', '#5eeaea', '#24d2b5', '#6772e5', '#20aee3', '#E5CF0D', '#c4c4c4']
        }
    });
}

function InitializeGender(data) {
    var option = null;

    var gender_columns = data.map(function (item) {
        return [item.description, item.count];
    });

    var chart = c3.generate({
        tooltip: {
            format: {
                value: function (value, ratio, id, index) { return value; }
            }
        },
        bindto: '#gender_board',
        data: {
            columns:
                gender_columns,

            type: 'donut'
        },
        legend: {
            show: true,
            position: 'right'
        },
        donut: {
            label: {
                threshold: 0
            },
            title: "სქესი",
            //width: 35
        },
        color: {
            pattern: ['#6772e5', '#20aee3', '#c4c4c4']
        }
    });
}


function InitialzieDashboard(result) {

    document.getElementById("c_active").innerHTML = result.customer.activeClients.toLocaleString();
    document.getElementById("c_average").innerHTML = result.customer.averageAmount.toLocaleString();
    document.getElementById("c_passive").innerHTML = result.customer.inActiveClients.toLocaleString();
    document.getElementById("c_new").innerHTML = result.customer.newClients.toLocaleString();
    document.getElementById("c_total").innerHTML = result.customer.totalClients.toLocaleString();

    document.getElementById("c_sms_left").innerHTML = result.sms.smsRemaining;
    document.getElementById("c_point_left").innerHTML = result.sms.pointRemaining;

    InitialzieAges(result.customerAges);

    var option = null;

    InitializeGender(result.customerGenders);

    var chart = c3.generate({
        bindto: '#rem_sms',
        data: {
            columns: [
                ['სულ', result.sms.smsQuantity],
                ['დარჩენილი', result.sms.smsRemaining],
            ],

            type: 'donut'
        },
        donut: {
            label: {
                show: false
            },
            title: "",
            width: 10,

        },
        size: {
            height: 140
        },
        legend: {
            hide: true
        },
        color: {
            pattern: ['#5c66d1', '#ffffff']
        }
    });

    var chart = c3.generate({
        bindto: '#left_point',
        data: {
            columns: [
                ['სულ', result.sms.pointQuantity],
                ['დარჩენილი', result.sms.pointRemaining],
            ],

            type: 'donut'
        },
        donut: {
            label: {
                show: false
            },
            title: "",
            width: 10,

        },
        size: {
            height: 140
        },
        legend: {
            hide: true
        },
        color: {
            pattern: ['#1592c1', '#ffffff']
        }
    });

    var dom = document.getElementById("main");
    var mytempChart = echarts.init(dom);
    var app = {};
    option = null;

    var days = Array(result.vizitorRanges.length).fill().map((x, i) => i + 1);
    var days_empty_string = Array(result.vizitorRanges.length).fill().map((x, i) => '');
    var vizitor_amount_columns = result.vizitorRanges.map(function (item) {
        return item.amount;
    });

    var vizitor_desc_columns = result.vizitorRanges.map(function (item) {
        return item.description;
    });

    var count_columns = result.vizitorRanges.map(function (item) {
        return item.count;
    });

    var vizitor_amountperc = result.vizitorRanges.map(function (item) {
        return item.amountPerc;
    });

    var vizitor_CntPerc = result.vizitorRanges.map(function (item) {
        return item.visitorCntPerc;
    });

    var vizitor_averageAmount = result.vizitorRanges.map(function (item) {
        return item.averageAmount;
    });

    option = {
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: ['გაყიდვები', 'მომხმარებლები']
        },
        xAxis: [
            {
                type: 'category',
                axisLine: { show: false },
                position: 'bottom',
                boundaryGap: true,
                axisLabel: {
                    show: true,
                    interval: 'auto',
                    margin: 8,
                    formatter: '{value}',
                },
                splitLine: {
                    show: true,
                    lineStyle: {
                        color: '#483d8b',
                        type: 'dotted',
                        width: 1
                    }
                },

                data: vizitor_desc_columns
            },
            {
                axisLine: { show: false },
                type: 'category',
                data: days_empty_string
            }
        ],
        yAxis: [
            {
                axisLine: { show: true },
                name: 'მომხმარებელი',
                //nameTextStyle: { align: 'right'},
                type: 'value',
                show: true,
                position: 'left',
                splitNumber: 10,
                axisLabel: {
                    show: true,
                    interval: 'auto',
                    rotate: 0,
                    margin: 18,

                },
                splitLine: {
                    show: true,
                    lineStyle: {
                        type: 'dotted',
                        width: 1
                    }
                }
            },
            {
                axisLine: { show: true },
                name: 'გაყიდვები',
                //nameTextStyle: { align: 'left' },
                type: 'value',
                splitNumber: 10,
                axisLabel: {

                },
                splitLine: {
                    show: false
                }

            }
        ],
        tooltip: {
            trigger: 'axis',
            formatter: function (items, i, k, m) {
                var _data_text = '';
                var _header_text = '';

                for (var i = 0; i < items.length; i++) {
                    _header_text = '<b>' + items[i][1] + '</b><br/>';
                    _data_text += items[i][0] + ': ' + items[i].data.toLocaleString() + '<br/>';
                }
                //'{b}<br/>{a0}: {c0}<br/>{a1}: {c1}<br/>{a2}: {c2}<br/>{a3}: {c3}% <br> {a4}: {c4}%'
                return _header_text + _data_text;
            }
        },
        series: [
            {
                name: 'გაყიდვები',
                type: 'line',
                data: vizitor_amount_columns,
                xAxisIndex: 0,
                yAxisIndex: 1,
                itemStyle: {
                    normal: {
                        color: '#24d2b5'
                    }
                },
            },
            {
                name: 'მომხმარებლები',
                type: 'bar',
                data: count_columns,
                xAxisIndex: 0,
                yAxisIndex: 0,

                barCategoryGap: '50%',
                itemStyle: {
                    normal: {
                        color: '#5B9BD5',
                        label: {
                            color: 'black',
                            show: true,
                            position: 'top',

                            textStyle: {
                                color: '#000'
                            }
                        }
                    },
                    label: { show: true, position: 'top' },

                },
            },
            {
                name: 'საშ. დანახარჯი',
                type: 'scatter',
                data: vizitor_averageAmount,
                xAxisIndex: 0,
                yAxisIndex: 0,
            },
            {
                name: 'წილი მომხმარებლებში',
                type: 'scatter',
                data: vizitor_CntPerc,
                xAxisIndex: 0,
                yAxisIndex: 0,
            },
            {
                name: 'წილი გაყიდვებში',
                type: 'scatter',
                data: vizitor_amountperc,
                xAxisIndex: 0,
                yAxisIndex: 0,
            }

        ]

    };

    if (option && typeof option === "object") {
        mytempChart.setOption(option, true), $(function () {
            function resize() {
                setTimeout(function () {
                    mytempChart.resize()
                }, 100)
            }
            $(window).on("resize", resize), $(".sidebartoggler").on("click", resize)
        });
    }

    if (vizitor_desc_columns.length == 0) {
        document.getElementById("main").style.display = "none";
        document.getElementById("main_empty").style.display = "block";
    };

    InitialzieVisitTurnovers(result.visitTurnovers);



}
