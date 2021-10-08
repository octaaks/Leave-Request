
(function () {
    'use strict';
    window.addEventListener('load', function () {
        // Fetch all the forms we want to apply custom Bootstrap validation styles to
        var forms = document.getElementsByClassName('needs-validation');

        // Loop over them and prevent submission
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Something went wrong!',
                    })
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === true) {
                    event.preventDefault();

                    var obj = {

                        //"Id": 12,
                        //"Name": "Cumi Cumi",
                        //"Address": "Bandung, Indonesia",
                        //"Gender": 1,
                        //"Phone": "0812322126007",
                        //"BirthDate": "1994-05-11",
                        //"Salary": 1000000,
                        //"JoinDate": "2020-10-05",
                        //"ReligionId": 1,
                        //"JobId": 1,
                        //"Email": "ciciici7@gmail.com",
                        //"Password": "Qwerty007"

                        "Id": parseInt($('#inputId').val()),
                        "Name": $('#inputName').val(),
                        "Address": $('#inputAddress').val(),
                        "Gender": parseInt($('#inputGender').val()),
                        "Phone": $('#inputPhone').val(),
                        "BirthDate": moment($('#inputBirthDate').val()).format("YYYY-MM-DD"),
                        "Salary": parseInt($('#inputSalary').val()),
                        "JoinDate": moment($('#inputJoinDate').val()).format("YYYY-MM-DD"),
                        "ReligionId": parseInt($('#inputReligion').val()),
                        "JobId": parseInt($('#inputJob').val()),
                        "Email": $('#inputEmail').val(),
                        "Password": $('#inputPassword').val()

                        //"Id": parseInt($('#inputId').val()),
                        //"Name": $('#inputName').val(),
                        //"Address": $('#inputAddress').val(),
                        //"Gender": parseInt($('#inputGender').val()),
                        //"Phone": $('#inputPhone').val(),
                        //"BirthDate": $('#inputBirthDate').val(),
                        //"Salary": parseInt($('#inputSalary').val()),
                        //"JoinDate": $('#inputJoinDate').val(),
                        //"ReligionId": parseInt($('#inputReligion').val()),
                        //"JobId": parseInt($('#inputJob').val()),
                        //"Email": $('#inputEmail').val(),
                        //"Password": $('#inputPassword').val()
                    };

                    var data = JSON.stringify(obj);

                    register(data);
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();

function register(data) {
    console.log(data);

    $.ajax({
        url: "/Accounts/Register",
        type: 'POST',
        data: data,
        traditional: true,
        contentType: 'application/json',
        dataType: "json",
        success: function (data, x, xhr) {
            //if (data.statusCode == 200) {
                //$('#insertRegis').modal('toggle')

                $('#insertRegis').modal('hide');
                $('.modal-backdrop').remove();

               // $('#dataTable').DataTable().ajax.reload();
            Reset();

            Swal.fire({
                title: "Welcome!",
                text: "Register successful. You can Login now",
                icon: "success",
                button: "OK"
            });
            //}
            //else {
            //    Swal.fire({
            //        title: "Oh no...",
            //        text: "Register unsuccessful ...",
            //        icon: "error",
            //        button: "OK"
            //    });
            //}
            //console.log(data);
            /* $('#insertData').modal('hide');*/
        },
        fail: function (data) {
            Swal.fire({
                title: "Oh no...",
                text: "Register unsuccessful ...",
                icon: "error",
                button: "OK"
            });
            console.log(data.message)
        },
        error: function (xhr, status, error) {
            Swal.fire({
                title: "Oh no...",
                text: "Register unsuccessful ...",
                icon: "error",
                button: "OK"
            });
            console.log(error);
            //Reset();
        }
    });
};

//$("#btnSubmit").click(post => {

//    debugger;

//    var obj = DataInsert();
//    console.log(obj);
//    var email = $('#email').val();
//    console.log(email);
//    post.preventDefault();

//    $.ajax({
//        url: "/Accounts/Registering",
//        type: 'POST',
//        data: obj,
//        dataType: 'json',
//        success: function (data, x, xhr) {
//            if (data.statusCode == 200) {
//                //$('#insertRegis').modal('hide')
//                Swal.fire({
//                    title: "Welcome!",
//                    text: "Register successful. You can Login now",
//                    icon: "success",
//                    button: "OK"
//                });
//               // $('#dataTable').DataTable().ajax.reload();
//                Reset();
//            }
//            else {
//                Swal.fire({
//                    title: "Oh no...",
//                    text: "Register unsuccessful ...",
//                    icon: "error",
//                    button: "OK"
//                });
//            }
//            console.log(data);
//            /* $('#insertData').modal('hide');*/
//        },
//        fail: function (data) {
//            Swal.fire({
//                title: "Oh no...",
//                text: "Register unsuccessful ...",
//                icon: "error",
//                button: "OK"
//            });
//            console.log(data.message)
//        },
//        error: function (xhr, status, error) {
//            Swal.fire({
//                title: "Oh no...",
//                text: "Register unsuccessful ...",
//                icon: "error",
//                button: "OK"
//            });
//            console.log(error);
//            Reset();
//        }
//    });
//})

function DataInsert() {
    var obj = {
        "Id": parseInt($('#inputId').val()),
        "Name": $('#inputName').val(),
        "Address": $('#inputAddress').val(),
        "Gender": parseInt($('#inputGender').val()),
        "Phone": $('#inputPhone').val(),
        "BirthDate": $('#inputBirthDate').val(),
        "Salary": parseInt($('#inputSalary').val()),
        "JoinDate": $('#inputJoinDate').val(),
        "ReligionId": parseInt($('#inputReligion').val()),
        "JobId": parseInt($('#inputJob').val()),
        "Email": $('#inputEmail').val(),
        "Password": $('#inputPassword').val()
    };
    return obj;
}

function Reset() {
    $('#regisForm')[0].reset();
}