// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//var myId = $('#@(ViewBag.Id)').val();
//var myValue = document.getElementById("@(ViewBag.Id)").value;
var val = $("#idvalue").data("value");
console.log(val);

//my leave history chart
$(document).ready(function () {
    $.ajax({
        url: "/leaverequests/getemployeeleaverequests/" + val,
        type: "GET"
    }).done((result) => {
        console.log(result);
        var annualLeave = result.filter(data => data.leaveTypeId === 1 && data.statusId === 2).length;
        var marriageLeave = result.filter(data => data.leaveTypeId === 2 && data.statusId === 2).length;
        var maternityLeave = result.filter(data => data.leaveTypeId === 3 && data.statusId === 2).length;
        var medicalLeave = result.filter(data => data.leaveTypeId === 4 && data.statusId === 2).length;
        var deathLeave = result.filter(data => data.leaveTypeId === 5 && data.statusId === 2).length;
        var options = {
            series: [{
                data: [annualLeave, marriageLeave, maternityLeave, medicalLeave, deathLeave]
            }],
            labels: ["Annual Leave", "Marriage Leave", "Maternity Leave", "Medical Leave", "Death Leave"],
            chart: {
                //width: 500,
                type: 'bar',
            },
            plotOptions: {
                bar: {
                    borderRadius: 5,
                    //columnWidth: '35%',
                    //barHeight: '70%',
                    dataLabels: {
                        position: 'top', // top, center, bottom
                    }
                }
            },
            xaxis: {
                categories: ["Annual Leave", "Marriage Leave", "Maternity Leave", "Medical Leave", "Death Leave"],
                position: 'bottom',
                labels: {
                    style: {
                        fontSize: '15px'
                    }
                },
                axisBorder: {
                    show: true
                },
                axisTicks: {
                    show: true
                },
                crosshairs: {
                    fill: {
                        type: 'gradient',
                        gradient: {
                            colorFrom: ' #c8f6fb',
                            colorTo: '#e4e2ec',
                            stops: [0, 100],
                            opacityFrom: 0.4,
                            opacityTo: 0.5,
                        }
                    }
                },
                tooltip: {
                    enabled: false,
                }
            },
            yaxis: {
                axisBorder: {
                    show: true
                },
                axisTicks: {
                    show: true,
                },
                labels: {
                    show: true,
                    style: {
                        fontSize: '15px'
                    },
                    formatter: function (val) {
                        return val;
                    }
                }
            }
        };
        var chart = new ApexCharts(document.querySelector("#chartminelv"), options);
        chart.render();
    })
});

//employee leave history chart
$(document).ready(function () {
    $.ajax({
        url: "/leaverequests/GetLeaveRequests/"+parseInt('@ViewData["Id"]'),
        type: "GET"
    }).done((result) => {
        console.log(result);
        var annualLeave = result.filter(data => data.leaveTypeId === 1 && data.statusId === 2).length;
        var marriageLeave = result.filter(data => data.leaveTypeId === 2 && data.statusId === 2).length;
        var maternityLeave = result.filter(data => data.leaveTypeId === 3 && data.statusId === 2).length;
        var medicalLeave = result.filter(data => data.leaveTypeId === 4 && data.statusId === 2).length;
        var deathLeave = result.filter(data => data.leaveTypeId === 5 && data.statusId === 2).length;
        console.log(deathLeave);
        var options = {
            series: [annualLeave, marriageLeave, maternityLeave, medicalLeave, deathLeave],
            chart: {
                //width: 500,
                type: 'pie',
            },
            labels: ["Annual Leave", "Marriage Leave", "Maternity Leave", "Medical Leave", "Death Leave"],
            responsive: [{
                breakpoint: 480,
                options: {
                    chart: {
                        //width: 200
                    },
                    legend: {
                        position: 'bottom'
                    }
                }
            }]
        };
        var chart = new ApexCharts(document.querySelector("#chartemplolv"), options);
        chart.render();
    })
});