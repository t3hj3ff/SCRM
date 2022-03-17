// ============================================================== 
// Bar chart option
// ============================================================== 
//var myChart = echarts.init(document.getElementById('bar-chart'));

//// specify chart configuration item and data
//option = {
//    tooltip : {
//        trigger: 'axis'
//    },
//    legend: {
//        data:['Site A','Site B']
//    },
//    toolbox: {
//        show : true,
//        feature : {

//            magicType : {show: true, type: ['line', 'bar']},
//            restore : {show: true},
//            saveAsImage : {show: true}
//        }
//    },
//    color: ["#55ce63", "#009efb"],
//    calculable : true,
//    xAxis : [
//        {
//            type : 'category',
//            data : ['Jan','Feb','Mar','Apr','May','Jun','July','Aug','Sept','Oct','Nov','Dec']
//        }
//    ],
//    yAxis : [
//        {
//            type : 'value'
//        }
//    ],
//    series : [
//        {
//            name:'Site A',
//            type:'bar',
//            data:[2.0, 4.9, 7.0, 23.2, 25.6, 76.7, 135.6, 162.2, 32.6, 20.0, 6.4, 3.3],
//            markPoint : {
//                data : [
//                    {type : 'max', name: 'Max'},
//                    {type : 'min', name: 'Min'}
//                ]
//            },
//            markLine : {
//                data : [
//                    {type : 'average', name: 'Average'}
//                ]
//            }
//        },
//        {
//            name:'Site B',
//            type:'bar',
//            data:[2.6, 5.9, 9.0, 26.4, 28.7, 70.7, 175.6, 182.2, 48.7, 18.8, 6.0, 2.3],
//            markPoint : {
//                data : [
//                    {name : 'The highest year', value : 182.2, xAxis: 7, yAxis: 183, symbolSize:18},
//                    {name : 'Year minimum', value : 2.3, xAxis: 11, yAxis: 3}
//                ]
//            },
//            markLine : {
//                data : [
//                    {type : 'average', name : 'Average'}
//                ]
//            }
//        }
//    ]
//};


//// use configuration item and data specified to show chart
//myChart.setOption(option, true), $(function() {
//            function resize() {
//                setTimeout(function() {
//                    myChart.resize()
//                }, 100)
//            }
//            $(window).on("resize", resize), $(".sidebartoggler").on("click", resize)
//        });

// ============================================================== 
// Line chart
// ============================================================== 
//var dom = document.getElementById("main");
//var mytempChart = echarts.init(dom);
//var app = {};
//option = null;
//option = {
//    tooltip: {
//        trigger: 'axis'
//    },
//    legend: {
//        data: ['გაყიდვები', 'მომხმარებლები']
//    },
//    //toolbox: {
//    //    show: true,
//    //    //feature: {
//    //    //    mark: { show: true },
//    //    //    dataView: { show: true },
//    //    //    magicType: { show: true, type: ['line', 'bar'] },
//    //    //    restore: { show: true },
//    //    //    saveAsImage: { show: true }
//    //    //}
//    //},
//    xAxis: [
//        {
//            type: 'category',
//            axisLine: { show: false },
//            position: 'bottom',
//            boundaryGap: true,
//            //axisLine: {
//            //    show: true,
//            //    lineStyle: {
//            //        color: 'green',
//            //        type: 'solid',
//            //        width: 2
//            //    }
//            //},
//            //axisTick: {
//            //    show: true,
//            //    length: 10,
//            //    lineStyle: {
//            //        color: 'red',
//            //        type: 'solid',
//            //        width: 2
//            //    }
//            //},
//            axisLabel: {
//                show: true,
//                interval: 'auto',
//                //rotate: 30,
//                margin: 8,
//                formatter: '{value} ვიზიტი',
//                //textStyle: {
//                //    color: 'black',
//                //    fontFamily: 'sans-serif',
//                //    fontSize: 15,
//                //    fontStyle: 'italic',
//                //    fontWeight: 'bold'
//                //}
//            },
//            splitLine: {
//                show: true,
//                lineStyle: {
//                    color: '#483d8b',
//                    type: 'dotted',
//                    width: 1
//                }
//            },

//            data: [
//                '0', '1', '2', '3', '4', '5', '>5'
//            ]
//        },
//        {
//            axisLine: { show: false },
//            type: 'category',
//            data: ['', '', '', '', '', '', '']
//        }
//    ],
//    yAxis: [
//        {
//            axisLine: { show: false },
//            type: 'value',
//            show: true,
//            position: 'left',
//            //min: 0,
//            // max: 20,
//            splitNumber: 10,
//            //    boundaryGap: [0,0.1],
//            //axisLine: {
//            //    show: true,
//            //    lineStyle: {
//            //        color: 'red',
//            //        type: 'dashed',
//            //        width: 2
//            //    }
//            //},
//            //axisTick: {
//            //    show: true,
//            //    length: 10,
//            //    lineStyle: {
//            //        color: 'green',
//            //        type: 'solid',
//            //        width: 2
//            //    }
//            //},
//            axisLabel: {
//                show: true,
//                interval: 'auto',
//                rotate: 0,
//                margin: 18,

//            },
//            splitLine: {
//                show: true,
//                lineStyle: {
//                    type: 'dotted',
//                    width: 1
//                }
//            }
//        },
//        {
//            axisLine: { show: false },
//            type: 'value',
//            splitNumber: 10,
//            axisLabel: {
//                formatter: function (value) {
//                    // Function formatter
//                    return value
//                }
//            },
//            splitLine: {
//                show: false
//            }
//        }
//    ],
//    series: [
//        {
//            name: 'გაყიდვები',
//            type: 'line',
//            data: [0, 240128, 197145, 187350, 168687, 158060, 1613543],
//            xAxisIndex: 0,
//            yAxisIndex: 1,
//            itemStyle: {
//                normal: {
//                    color: '#24d2b5'//, areaStyle: { type: 'default' }
//                }
//            },
//        },
//        {
//            name: 'მომხმარებლები',
//            type: 'bar',
//            xAxisIndex: 1,
//            yAxisIndex: 0,
//            data: [78635, 22804, 10121, 6334, 4381, 3274, 15076],
//            //stack: 'sum',
//            barCategoryGap: '50%',
//            itemStyle: {
//                normal: {
//                    color: '#5B9BD5',
//                    label: {
//                        color: 'black',
//                        show: true,
//                        position: 'top',

//                        textStyle: {
//                            color: '#000'
//                        }
//                    }
//                },
//                label: { show: true, position: 'top' },

//            },
//            //markPoint: {
//            //    symbol:'',
//            //    data: [
//            //        { type: 'sum', name: '2' }
//            //    ]
//            //},
//        },
//        {
//            name: 'საშ. ბრუნვა',
//            type: 'scatter',
//            data: [0, 9, 8, 7, 28.7, 20, 7],
//            xAxisIndex: 0,
//            yAxisIndex: 0

//        },
//        {
//            name: 'მომხმარებლების წილი',
//            type: 'scatter',
//            data: [10, 2, 47, 10.4, 47, 58, 6],
//            xAxisIndex: 0,
//            yAxisIndex: 0,
//            tooltip: {
               
//            }
//        },
//        {
//            name: 'გაყიდვების წილი',
//            type: 'scatter',
//            data: [25.0, 12.2, 13.3, 14.5, 10.3, 18, 120],
//            xAxisIndex: 0,
//            yAxisIndex: 0,
//            tooltip: {
               
//            }
//        }
//    ]
//};

//if (option && typeof option === "object") {
//    mytempChart.setOption(option, true), $(function () {
//        function resize() {
//            setTimeout(function () {
//                mytempChart.resize()
//            }, 100)
//        }
//        $(window).on("resize", resize), $(".sidebartoggler").on("click", resize)
//    });
//}

//var dom = document.getElementById("visit_turnovers");
//var visit_turnovers = echarts.init(dom);
//option = null;
//option = {
//    tooltip: {
//        trigger: 'axis'
//    },
//    legend: {
//        data: ['ვიზიტი', 'ბრუნვა']
//    },
//    xAxis: [
//        {
//            type: 'category',
//            axisLine: { show: false },
//            position: 'bottom',
//            boundaryGap: true,
//            axisLabel: {
//                show: true,
//                interval: 'auto',
//                margin: 8,
//                formatter: '{value}',
//            },
//            splitLine: {
//                show: true,
//                lineStyle: {
//                    color: '#483d8b',
//                    type: 'dotted',
//                    width: 1
//                }
//            },

//            data: [
//                1,
//                2,
//                3,
//                4,
//                5,
//                6,
//                7,
//                8,
//                9,
//                10,
//                11,
//                12,
//                13,
//                14,
//                15,
//                16,
//                17,
//                18,
//                19,
//                20,
//                21,
//                22,
//                23,
//                24,
//                25,
//                26,
//                27,
//                28,
//                29,
//                30
//            ]
//        },
//        {
//            axisLine: { show: false },
//            type: 'category',
//            data: ['', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '']
//        }
//    ],
//    yAxis: [
//        {
//            axisLine: { show: false },
//            type: 'value',
//            show: true,
//            position: 'left',
//            splitNumber: 10,
//            axisLabel: {
//                show: true,
//                interval: 'auto',
//                rotate: 0,
//                margin: 18,

//            }
//        },
//        {
//            axisLine: { show: false },
//            type: 'value',
//            splitNumber: 10,
//            axisLabel: {
//                formatter: function (value) {
//                    return value
//                }
//            },
//            splitLine: {
//                show: false
//            }
//        }
//    ],
//    series: [
//        {
//            name: 'ბრუნვა',
//            type: 'line',
//            smooth: true,
//            data: [347954.716,
//                351638.737,
//                295306.634,
//                370610.67,
//                318961.516,
//                310254.63,
//                283048.039,
//                337822.774,
//                310076.3,
//                262803.858,
//                290188.667,
//                339941.61,
//                90799.323,
//                204880.44,
//                299233.258,
//                294396.875,
//                281188.525,
//                322349.223,
//                289676.88,
//                271529.055,
//                279301.171,
//                299475.142,
//                392602.77,
//                293957.73,
//                338626.909,
//                339641.67,
//                284771.516,
//                287326.455,
//                333811.839,
//                310208.96,
//                314886.378],
//            itemStyle: {
//                normal: {
//                    color: '#5B9BD5'
//                },
//                formatter: '{value}'
//            }
//        },
//        {
//            name: 'ვიზიტი',
//            type: 'line',
//            smooth: true,
//            xAxisIndex: 0,
//            yAxisIndex: 1,
//            data: [18860,
//                16519,
//                14812,
//                17650,
//                17870,
//                18286,
//                17180,
//                18780,
//                15501,
//                13658,
//                16315,
//                17765,
//                4219,
//                11510,
//                16575,
//                16569,
//                15693,
//                17577,
//                14839,
//                13864,
//                16689,
//                17394,
//                18075,
//                17094,
//                18457,
//                15941,
//                14021,
//                16796,
//                17316,
//                17788,
//                17732],

//            itemStyle: {
//                normal: {
//                    color: '#24d2b5'//, areaStyle: { type: 'default' }
//                }
//            },
//        }
//    ]
//};

//if (option && typeof option === "object") {
//    visit_turnovers.setOption(option, true), $(function () {
//        function resize() {
//            setTimeout(function () {
//                visit_turnovers.resize()
//            }, 100)
//        }
//        $(window).on("resize", resize), $(".sidebartoggler").on("click", resize)
//    });
//}


//Age

//var dom = document.getElementById("ages");
//var ages = echarts.init(dom);
//option = null;

//var dataStyle = {
//    normal: {
//        label: { show: false },
//        labelLine: { show: false }
//    }
//};
//var placeHolderStyle = {
//    normal: {
//        color: 'rgba(0,0,0,0)',
//        label: { show: false },
//        labelLine: { show: false }
//    },
//    emphasis: {
//        color: 'rgba(0,0,0,0)'
//    }
//};
//option = {
//    title: {
//        text: 'ასაკი',
//        //subtext: 'მომხმარებლის',
//        sublink: 'http://e.weibo.com/1341556070/AhQXtjbqh',
//        x: 'center',
//        y: 'center',
//        itemGap: 20,
//        //textStyle: {
//        //    color: 'rgba(30,144,255,0.8)',
//        //    fontFamily: '微软雅黑',
//        //    fontSize: 35,
//        //    fontWeight: 'bolder'
//        //}
//    },
//    tooltip: {
//        show: true,
//        formatter: "{a} <br/>{b} : {c} ({d}%)"
//    },
//    legend: {
//        orient: 'vertical',
//        x: document.getElementById('main').offsetWidth / 2,
//        y: 45,
//        itemGap: 12,
//        data: ['18', '35', '50']
//    },
//    toolbox: {
//        show: false,
//        feature: {
//            mark: { show: true },
//            dataView: { show: true, readOnly: false },
//            restore: { show: true },
//            saveAsImage: { show: true }
//        }
//    },
//    series: [
//        {
//            name: '1',
//            type: 'pie',
//            clockWise: false,
//            radius: [125, 150],
//            itemStyle: dataStyle,
//            data: [
//                {
//                    value: 100,
//                    name: '18'
//                },
//                {
//                    value: 0,
//                    name: 'invisible',
//                    itemStyle: placeHolderStyle
//                }
//            ]
//        },
//        {
//            name: '2',
//            type: 'pie',
//            clockWise: false,
//            radius: [100, 125],
//            itemStyle: dataStyle,
//            data: [
//                {
//                    value: 35,
//                    name: '35'
//                },
//                {
//                    value: 0,
//                    name: 'invisible',
//                    itemStyle: placeHolderStyle
//                }
//            ]
//        },
//        {
//            name: '3',
//            type: 'pie',
//            clockWise: false,
//            radius: [75, 100],
//            itemStyle: dataStyle,
//            data: [
//                {
//                    value: 50,
//                    name: '50'
//                },
//                {
//                    value: 50,
//                    name: 'invisible',
//                    itemStyle: placeHolderStyle
//                }
//            ]
//        }
//    ]
//};


//if (option && typeof option === "object") {
//    ages.setOption(option, true), $(function () {
//        function resize() {
//            setTimeout(function () {
//                ages.resize()
//            }, 100)
//        }
//        $(window).on("resize", resize), $(".sidebartoggler").on("click", resize)
//    });
//}


//var dom = document.getElementById("main_2");
//var main_2_chart = echarts.init(dom);
//option = null;
//option = {

//    tooltip: {
//        trigger: 'axis'
//    },
//    legend: {
//        data: ['max temp', 'min temp']
//    },
//    toolbox: {
//        show: true,
//        feature: {
//            magicType: { show: true, type: ['line', 'bar'] },
//            restore: { show: true },
//            saveAsImage: { show: true }
//        }
//    },
//    calculable: true,
//    xAxis: [
//        {
//            type: 'category',

//            boundaryGap: false,
//            data: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday']
//        }
//    ],
//    yAxis: [
//        {
//            type: 'value',
//            axisLabel: {
//                formatter: '{value} °C'
//            }
//        }
//    ],

//    series: [
//        {
//            name: 'max temp',
//            type: 'line',
//            color: ['#000'],
//            data: [11, 11, 15, 13, 12, 13, 10],
//            markPoint: {
//                data: [
//                    { type: 'max', name: 'Max' },
//                    { type: 'min', name: 'Min' }
//                ]
//            },
//            itemStyle: {
//                normal: {
//                    lineStyle: {
//                        shadowColor: 'rgba(0,0,0,0.3)',
//                        shadowBlur: 10,
//                        shadowOffsetX: 8,
//                        shadowOffsetY: 8
//                    }
//                }
//            },
//            markLine: {
//                data: [
//                    { type: 'average', name: 'Average' }
//                ]
//            }
//        },
//        {
//            name: 'min temp',
//            type: 'line',
//            data: [1, -2, 2, 5, 3, 2, 0],
//            markPoint: {
//                data: [
//                    { name: 'Week minimum', value: -2, xAxis: 1, yAxis: -1.5 }
//                ]
//            },
//            itemStyle: {
//                normal: {
//                    lineStyle: {
//                        shadowColor: 'rgba(0,0,0,0.3)',
//                        shadowBlur: 10,
//                        shadowOffsetX: 8,
//                        shadowOffsetY: 8
//                    }
//                }
//            },
//            markLine: {
//                data: [
//                    { type: 'average', name: 'Average' }
//                ]
//            }
//        }
//    ]
//};

//if (option && typeof option === "object") {
//    main_2_chart.setOption(option, true), $(function () {
//        function resize() {
//            setTimeout(function () {
//                main_2_chart.resize()
//            }, 100)
//        }
//        $(window).on("resize", resize), $(".sidebartoggler").on("click", resize)
//    });
//};



//// ============================================================== 
//// Pie chart option
//// ============================================================== 
//var pieChart = echarts.init(document.getElementById('pie-chart'));

//// specify chart configuration item and data
//option = {

//    tooltip : {
//        trigger: 'item',
//        formatter: "{a} <br/>{b} : {c} ({d}%)"
//    },
//    legend: {
//        x : 'center',
//        y : 'bottom',
//        data:['rose1','rose2','rose3','rose4','rose5','rose6','rose7','rose8']
//    },
//    toolbox: {
//        show : true,
//        feature : {

//            dataView : {show: true, readOnly: false},
//            magicType : {
//                show: true, 
//                type: ['pie', 'funnel']
//            },
//            restore : {show: true},
//            saveAsImage : {show: true}
//        }
//    },
//    color: ["#f62d51", "#dddddd","#ffbc34", "#7460ee","#009efb", "#2f3d4a","#90a4ae", "#55ce63"],
//    calculable : true,
//    series : [
//        {
//            name:'Radius mode',
//            type:'pie',
//            radius : [20, 110],
//            center : ['25%', 200],
//            roseType : 'radius',
//            width: '40%',       // for funnel
//            max: 40,            // for funnel
//            itemStyle : {
//                normal : {
//                    label : {
//                        show : false
//                    },
//                    labelLine : {
//                        show : false
//                    }
//                },
//                emphasis : {
//                    label : {
//                        show : true
//                    },
//                    labelLine : {
//                        show : true
//                    }
//                }
//            },
//            data:[
//                {value:10, name:'rose1'},
//                {value:5, name:'rose2'},
//                {value:15, name:'rose3'},
//                {value:25, name:'rose4'},
//                {value:20, name:'rose5'},
//                {value:35, name:'rose6'},
//                {value:30, name:'rose7'},
//                {value:40, name:'rose8'}
//            ]
//        },
//        {
//            name:'Area mode',
//            type:'pie',
//            radius : [30, 110],
//            center : ['75%', 200],
//            roseType : 'area',
//            x: '50%',               // for funnel
//            max: 40,                // for funnel
//            sort : 'ascending',     // for funnel
//            data:[
//                {value:10, name:'rose1'},
//                {value:5, name:'rose2'},
//                {value:15, name:'rose3'},
//                {value:25, name:'rose4'},
//                {value:20, name:'rose5'},
//                {value:35, name:'rose6'},
//                {value:30, name:'rose7'},
//                {value:40, name:'rose8'}
//            ]
//        }
//    ]
//};



//// use configuration item and data specified to show chart
//pieChart.setOption(option, true), $(function() {
//            function resize() {
//                setTimeout(function() {
//                    pieChart.resize()
//                }, 100)
//            }
//            $(window).on("resize", resize), $(".sidebartoggler").on("click", resize)
//        });

//// ============================================================== 
//// Radar chart option
//// ============================================================== 
//var radarChart = echarts.init(document.getElementById('radar-chart'));

// specify chart configuration item and data

//option = {

//    tooltip : {
//        trigger: 'axis'
//    },
//    legend: {
//        orient : 'vertical',
//        x : 'right',
//        y : 'bottom',
//        data:['Allocated Budget','Actual Spending']
//    },
//    toolbox: {
//        show : true,
//        feature : {
//            dataView : {show: true, readOnly: false},
//            restore : {show: true},
//            saveAsImage : {show: true}
//        }
//    },
//    polar : [
//       {
//           indicator : [
//               { text: 'sales', max: 6000},
//               { text: 'Administration', max: 16000},
//               { text: 'Information Techology', max: 30000},
//               { text: 'Customer Support', max: 38000},
//               { text: 'Development', max: 52000},
//               { text: 'Marketing', max: 25000}
//            ]
//        }
//    ],
//    color: ["#55ce63", "#009efb"],
//    calculable : true,
//    series : [
//        {
//            name: 'Budget vs spending',
//            type: 'radar',
//            data : [
//                {
//                    value : [4300, 10000, 28000, 35000, 50000, 19000],
//                    name : 'Allocated Budget'
//                },
//                 {
//                    value : [5000, 14000, 28000, 31000, 42000, 21000],
//                    name : 'Actual Spending'
//                }
//            ]
//        }
//    ]
//};




//// use configuration item and data specified to show chart
//radarChart.setOption(option, true), $(function() {
//            function resize() {
//                setTimeout(function() {
//                    radarChart.resize()
//                }, 100)
//            }
//            $(window).on("resize", resize), $(".sidebartoggler").on("click", resize)
//        });

//// ============================================================== 
//// doughnut chart option
//// ============================================================== 
//var doughnutChart = echarts.init(document.getElementById('doughnut-chart'));

//// specify chart configuration item and data

//option = {
//    tooltip : {
//        trigger: 'item',
//        formatter: "{a} <br/>{b} : {c} ({d}%)"
//    },
//    legend: {
//        orient : 'vertical',
//        x : 'left',
//        data:['Item A','Item B','Item C','Item D','Item E']
//    },
//    toolbox: {
//        show : true,
//        feature : {
//            dataView : {show: true, readOnly: false},
//            magicType : {
//                show: true, 
//                type: ['pie', 'funnel'],
//                option: {
//                    funnel: {
//                        x: '25%',
//                        width: '50%',
//                        funnelAlign: 'center',
//                        max: 1548
//                    }
//                }
//            },
//            restore : {show: true},
//            saveAsImage : {show: true}
//        }
//    },
//    color: ["#f62d51", "#009efb", "#55ce63", "#ffbc34", "#2f3d4a"],
//    calculable : true,
//    series : [
//        {
//            name:'Source',
//            type:'pie',
//            radius : ['80%', '90%'],
//            itemStyle : {
//                normal : {
//                    label : {
//                        show : false
//                    },
//                    labelLine : {
//                        show : false
//                    }
//                },
//                emphasis : {
//                    label : {
//                        show : true,
//                        position : 'center',
//                        textStyle : {
//                            fontSize : '30',
//                            fontWeight : 'bold'
//                        }
//                    }
//                }
//            },
//            data:[
//                {value:335, name:'Item A'},
//                {value:310, name:'Item B'},
//                {value:234, name:'Item C'},
//                {value:135, name:'Item D'},
//                {value:1548, name:'Item E'}
//            ]
//        }
//    ]
//};



//// use configuration item and data specified to show chart
//doughnutChart.setOption(option, true), $(function() {
//            function resize() {
//                setTimeout(function() {
//                    doughnutChart.resize()
//                }, 100)
//            }
//            $(window).on("resize", resize), $(".sidebartoggler").on("click", resize)
//        });

//// ============================================================== 
//// Gauge chart option
//// ============================================================== 
//var gaugeChart = echarts.init(document.getElementById('gauge-chart'));

//// specify chart configuration item and data
//option = {
//    tooltip : {
//        formatter: "{a} <br/>{b} : {c}%"
//    },
//    toolbox: {
//        show : true,
//        feature : {
//            restore : {show: true},
//            saveAsImage : {show: true}
//        }
//    },

//    series : [
//        {
//            name:'Speed',
//            type:'gauge',
//            detail : {formatter:'{value}%'},
//            data:[{value: 50, name: 'Speed'}],
//            axisLine: {            // 坐标轴线
//                lineStyle: {       // 属性lineStyle控制线条样式
//                    color: [[0.2, '#55ce63'],[0.8, '#009efb'],[1, '#f62d51']], 

//                }
//            },

//        }
//    ]
//};
//timeTicket = setInterval(function (){
//    option.series[0].data[0].value = (Math.random()*100).toFixed(2) - 0;
//    gaugeChart.setOption(option, true);
//},2000);


//// use configuration item and data specified to show chart
//gaugeChart.setOption(option, true), $(function() {
//            function resize() {
//                setTimeout(function() {
//                    gaugeChart.resize()
//                }, 100)
//            }
//            $(window).on("resize", resize), $(".sidebartoggler").on("click", resize)
//        });

//// ============================================================== 
//// Radar chart option
//// ============================================================== 
//var gauge2Chart = echarts.init(document.getElementById('gauge2-chart'));

//// specify chart configuration item and data
//option = {
//    tooltip : {
//        formatter: "{a} <br/>{b} : {c}%"
//    },
//    toolbox: {
//        show : true,
//        feature : {
//            restore : {show: true},
//            saveAsImage : {show: true}
//        }
//    },
//    series : [
//        {
//            name:'Market',
//            type:'gauge',
//            splitNumber: 10,       // 分割段数，默认为5
//            axisLine: {            // 坐标轴线
//                lineStyle: {       // 属性lineStyle控制线条样式
//                    color: [[0.2, '#55ce63'],[0.8, '#009efb'],[1, '#f62d51']], 
//                    width: 8
//                }
//            },
//            axisTick: {            // 坐标轴小标记
//                splitNumber: 10,   // 每份split细分多少段
//                length :12,        // 属性length控制线长
//                lineStyle: {       // 属性lineStyle控制线条样式
//                    color: 'auto'
//                }
//            },
//            axisLabel: {           // 坐标轴文本标签，详见axis.axisLabel
//                textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
//                    color: 'auto'
//                }
//            },
//            splitLine: {           // 分隔线
//                show: true,        // 默认显示，属性show控制显示与否
//                length :30,         // 属性length控制线长
//                lineStyle: {       // 属性lineStyle（详见lineStyle）控制线条样式
//                    color: 'auto'
//                }
//            },
//            pointer : {
//                width : 5
//            },
//            title : {
//                show : true,
//                offsetCenter: [0, '-40%'],       // x, y，单位px
//                textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
//                    fontWeight: 'bolder'
//                }
//            },
//            detail : {
//                formatter:'{value}%',
//                textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
//                    color: 'auto',
//                    fontWeight: 'bolder'
//                }
//            },
//            data:[{value: 50, name: 'Rate'}]
//        }
//    ]
//};

//clearInterval(timeTicket);
//timeTicket = setInterval(function (){
//    option.series[0].data[0].value = (Math.random()*100).toFixed(2) - 0;
//    gauge2Chart.setOption(option,true);
//},2000)


//// use configuration item and data specified to show chart
//gauge2Chart.setOption(option, true), $(function() {
//            function resize() {
//                setTimeout(function() {
//                    gauge2Chart.resize()
//                }, 100)
//            }
//            $(window).on("resize", resize), $(".sidebartoggler").on("click", resize)
//        });