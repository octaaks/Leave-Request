// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var val = $("#idvalue").data("value");
console.log(val);

$(document).ready(function () {
    $.ajax({
        url: "/Employee/GetEmployeeById/" + val
    }).done((result) => {
        console.log(result);
        document.getElementById('namevalue').value = result.name;
        document.getElementById('username').innerHTML = result.name;
        document.getElementById('addressvalue').value = result.address;
        document.getElementById('gendervalue').value = result.gender;
        document.getElementById('phonevalue').value = "+62"+result.phone;
        document.getElementById('bdvalue').value = (result.birthDate).toString().substring(0,10);
        document.getElementById('salaryvalue').value = result.salary;
        document.getElementById('jdvalue').value = (result.joinDate).toString().substring(0, 10);
        document.getElementById('leavevalue').value = result.totalLeave;

        $.ajax({
            url: "/Jobs/GetJob/" + result.jobId
        }).done((result) => {
            console.log(result)
            document.getElementById('jobvalue').value = result.title;
        });

        $.ajax({
            url: "/Religions/GetReligion/" + result.religionId
        }).done((result) => {
            document.getElementById('religionvalue').value = result.name;
        });
    })
});