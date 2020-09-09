

//  ROLE -- BEGIN ---->
function AddRole() {

    var role = $("#txtRole").val(); 

    $.ajax({
        url: "/Admin/AddRole",
        type: "POST",
        dataType: "json",
        data: {
            _role: role 
        },
        success: function (data) {
            if (data.Success)
                Swal.fire({
                    title: '',
                    text: data.Message,
                    icon: 'success',
                    confirmButtonText: 'Tamam'
                }).then(function () {
                    location.reload();
                });
            else
                Swal.fire({
                    title: 'Hata!',
                    text: data.Message,
                    icon: 'warning',
                    confirmButtonText: 'Tamam'
                });
        }
    });
}

function UpdateRole() {

    var role = $("#txtRole").val();
    var roleID = $("#hdnRoleId").val();
     
    $.ajax({
        url: "/Admin/EditRole/" + roleID,
        type: "POST",
        dataType: "json",
        data: {
            _role: role,
            _id: roleID
        },
        success: function (data) {
            if (data.Success)
                Swal.fire({
                    title: '',
                    text: data.Message,
                    icon: 'success',
                    confirmButtonText: 'Tamam'
                }).then(function () {
                    location.href="/Admin/AddRole";
                });
            else
                Swal.fire({
                    title: 'Hata!',
                    text: data.Message,
                    icon: 'warning',
                    confirmButtonText: 'Tamam'
                });
        }
    });
}

function DeleteRole() { 

    Swal.fire({
        title: 'Silme İşlemi',
        text: "Rol tanımı silinecektir. Emin misiniz?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Devam Et',
        cancelButtonText: "Iptal"
    }).then((result) => {
        if (result.value) {
            return true;
        }  
        else {
            return false;
        }
    });
}
//  ROLE -- END ----->


// USER -- BEGIN ---->
function UserAdd() {

    var username = $("#txtUserName").val();
    var password = $("#txtPassword").val();

    $.ajax({
        url: "/Admin/AddUser",
        type: "POST",
        dataType: "json",
        data: {
            _username: username,
            _password: password
        },
        success: function (data) {
            if (data.Success)
                Swal.fire({
                    title: '',
                    text: data.Message,
                    icon: 'success',
                    confirmButtonText: 'Tamam'
                }).then(function () {
                    location.reload();
                });
            else
                Swal.fire({
                    title: 'Hata!',
                    text: data.Message,
                    icon: 'warning',
                    confirmButtonText: 'Tamam'
                });
        }
    });
}
function UpdateUser() {

    var user = $("#txtUserName").val();
    var password = $("#txtPassword").val();
    var userID = $("#hdnUserId").val();

    $.ajax({
        url: "/Admin/EditUser/" + userID,
        type: "POST",
        dataType: "json",
        data: {
            _user: user,
            _password: password,
            _id: userID
        },
        success: function (data) {
            if (data.Success)
                Swal.fire({
                    title: '',
                    text: data.Message,
                    icon: 'success',
                    confirmButtonText: 'Tamam'
                }).then(function () {
                    location.href = "/Admin/AddUser";
                });
            else
                Swal.fire({
                    title: 'Hata!',
                    text: data.Message,
                    icon: 'warning',
                    confirmButtonText: 'Tamam'
                });
        }
    });
}
// USER -- END ----->

function Message(title,message,icon,confirmText) {
    Swal.fire({
        title: title,
        text: message,
        icon: icon,
        confirmButtonText: confirmText
    });
}