//styling
document.getElementById("titleform").style.fontFamily = 'Tahoma, sans-serif';

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
        url: "/Accounts/Registering",
        type: 'POST',
        data: obj,
        dataType: 'json',
        success: function (data, x, xhr) {
            if (data.statusCode == 200) {
                Swal.fire({
                    title: "Welcome!",
                    text: "Register successful. You can Login now",
                    icon: "success",
                    button: "OK"
                });
                $('#formRegis')[0].reset();
            }
            console.log(data);
        },
        fail: function (data) {
            if (data.statusCode == 400) {
                Swal.fire({
                    title: "Oh no...",
                    text: "Register unsuccessful ...",
                    icon: "error",
                    button: "OK"
                });
            }
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
                    text: "Reset password unsuccessful ...",
                    icon: "error",
                    button: "OK"
                });
            }
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
            console.log(data);
        },
        fail: function (data) {
            Swal.fire({
                title: "Oh no...",
                text: "Change password unsuccessful ...",
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



