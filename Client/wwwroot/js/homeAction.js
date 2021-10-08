$("#btnSubmit").click(post => {
    var obj = DataInsert();
    console.log(obj);
    var email = $('#email').val();
    console.log(email);
    post.preventDefault();
    $.ajax({
        url: "/Accounts/Registering",
        type: 'POST',
        data: obj,
        dataType: 'json',
        success: function (data, x, xhr) {
            if (data.statusCode == 200) {
                //$('#insertRegis').modal('hide')
                Swal.fire({
                    title: "Welcome!",
                    text: "Register successful. You can Login now",
                    icon: "success",
                    button: "OK"
                });
               // $('#dataTable').DataTable().ajax.reload();
                Reset();
            }
            else {
                Swal.fire({
                    title: "Oh no...",
                    text: "Register unsuccessful ...",
                    icon: "error",
                    button: "OK"
                });
            }
            console.log(data);
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
            Reset();
        }
    });
})

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