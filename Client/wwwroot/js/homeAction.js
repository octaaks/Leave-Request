//styling
document.getElementById("titleform").style.fontFamily = 'Tahoma, sans-serif';

/*(function () {
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

                    register(obj);
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();
*/
/*
function register(inputdata) {
    console.log(inputdata);

    $.ajax({
        url: "/Accounts/Registering",
        type: 'POST',
        data: inputdata,
        //traditional: true,
        //contentType: 'application/json',
        dataType: "json",
        success: function (data, x, xhr) {

            console.log(data.status);

            //if (data.status == 200) {
                Swal.fire({
                    title: "Welcome!",
                    text: "Register successful. You can Login now",
                    icon: "success",
                    button: "OK"
                });
                $('#formRegis')[0].reset();
                $('#insertRegis').modal('hide');
                $('.modal-backdrop').remove();
            //}
            //else {
            //    Swal.fire({
            //        title: "Oh no...",
            //        text: "Register unsuccessful ...",
            //        icon: "error",
            //        button: "OK"
            //    });
            //}
            console.log(data);
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
*/
//posting data registration form to db
//$("#btnSubmit").click(post => {
//    var obj = DataInsert();
//    console.log(obj);
//    post.preventDefault();
//    $.ajax({
//        url: "/Accounts/Register",
//        type: 'POST',
//        data: data,
//        traditional: true,
//        contentType: 'application/json',
//        dataType: "json",
//        success: function (data, x, xhr) {
//            if (data.statusCode == 200) {
//                Swal.fire({
//                    title: "Welcome!",
//                    text: "Register successful. You can Login now",
//                    icon: "success",
//                    button: "OK"
//                });
//                Reset();
//                $('#insertRegis').modal('hide')
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
//            //Reset();
//        }
//    });
//});

//posting data registration form to db
$("#btnSubmit").click(post => {
    var obj = DataInsert();
    console.log(obj);
    //post.preventDefault();
    $.ajax({
        url: "/Accounts/Register",
        type: 'POST',
        data: obj,
        dataType: 'json',
        success: function (data, x, xhr) {
            if (data.statusCode == 200) {
                Swal.fire({
                    title: "Welcome!",
                    text: "Register successful. You can Login now",
                    icon: "success",
                    confirmButtonText: 'Ok!'
                });
                $('#formRegis')[0].reset();
                $('#insertRegis').modal('hide');
                $('.modal-backdrop').remove();
                }
            else if (data.statusCode == 400)
            {
                Swal.fire({
                    title: "Oh no...",
                    text: "Register unsuccessful ...",
                    icon: "error",
                    button: "OK"
                });
            }
        },
        fail: function (data) {
            Swal.fire({
                title: "Oh no...",
                text: "Register unsuccessful !",
                icon: "error",
                button: "OK"
            });
            console.log(data.message)
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
})

//get data value registration form
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

//send link for reset password by email
$("#resetButton").click(post => {
    var obj = {
        "Email": $('#email-for-reset').val()
    }
    post.preventDefault();
    $.ajax({
        url: "/Accounts/ForgotPass",
        type: 'POST',
        data: obj,
        dataType: 'json',
        success: function (data, x, xhr) {
            if (data.statusCode == 200) {
                Swal.fire({
                    title: "Ok!",
                    text: "Your new password has been sent to your email!",
                    icon: "success",
                    button: "OK"
                });
            $('#changepassform')[0].reset();
            }
            else {
                Swal.fire({
                    title: "Oh no...",
                    text: "Reset password unsuccessful. Check your input email",
                    icon: "error",
                    button: "OK"
                });
            }
            console.log(data.statusCode)
        },
        fail: function (data) {
            Swal.fire({
                title: "Oh no...",
                text: "Reset password  unsuccessful ...",
                icon: "error",
                button: "OK"
            });
            console.log(data.message)
        },
        error: function (xhr, status, error) {
            Swal.fire({
                title: "Oh no...",
                text: "Reset password  unsuccessful ...",
                icon: "error",
                button: "OK"
            });
            console.log(error);
        }
    });
})

//change password
$("#changeButton").click(post => {
    var obj = DataChangePass();
    console.log(obj);
    post.preventDefault();
    $.ajax({
        url: "/Accounts/ChangePass",
        type: 'POST',
        data: obj,
        dataType: 'json',
        success: function (data, x, xhr) {
            if (data.statusCode == 200) {
                Swal.fire({
                    title: "Successful!",
                    text: "You can login with your new password",
                    icon: "success",
                    button: "OK"
                });
                $('#changepassform')[0].reset();
            }
            else {
                Swal.fire({
                    title: "Oh no...",
                    text: "Change password unsuccessful ...",
                    icon: "error",
                    button: "OK"
                });
            }
            console.log(data.statusCode);
        },
        fail: function (data) {
            Swal.fire({
                title: "Oh no...",
                text: "Change password unsuccessful !",
                icon: "error",
                button: "OK"
            });
            console.log(data.message)
        },
        error: function (xhr, status, error) {
            Swal.fire({
                title: "Oh no...",
                text: "Change password unsuccessful ...",
                icon: "error",
                button: "OK"
            });
            console.log(error);
            Reset();
        }
    });
})

//get data value change pass form
function DataChangePass() {
    var obj = {
        "Email": $('#emailreset').val(),
        "OldPassword": $('#oldpassreset').val(),
        "NewPassword": $('#newpassreset').val(),
        "ConfirmNewPassword": $('#conewpassreset').val()
    };
    return obj;
}



