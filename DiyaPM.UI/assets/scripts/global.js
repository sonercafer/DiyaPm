
//$(document).ready(function () {
//    $('select[id="ddl_Roles"]').change();
//});

//$('#ddl_Roles').on('change', function () {
//    var role_id = $("#ddl_Roles").val();
//    var data = { id: role_id };
//    $.get('/Admin/UsersInRoles', data, function (responseData) {
//        location.href = "/Admin/UsersInRoles/" + role_id;
//        $("#ddl_Roles").val(role_id);
//    });

//});

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
                    location.href = "/Admin/AddRole";
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

// USERS IN ROLES -- BEGIN ---->
function UsersInRoles() { // ADD UsersInRoles
    var role_id = $("#ddl_Roles").val();
    var user_id = $("#ddl_Users").val();
     
    if (role_id !== "") {
        $.ajax({
            url: "/Admin/UsersInRoles/",
            type: "POST",
            dataType: "json",
            data: {
                _roleid: role_id,
                _userid: user_id
            },
            success: function (data) {
                if (data.Success)
                    Swal.fire({
                        title: '',
                        text: data.Message,
                        icon: 'success',
                        confirmButtonText: 'Tamam'
                    }).then(function () {
                        GetUsersInRoles();
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
}

function GetUsersInRoles() {
    var role_id = $("#ddl_Roles").val();
    if (role_id !== "") {
        var tbl_Users = $('#tbl_Users').DataTable({
            paging: true,
            searching: false,
            destroy: true,
            info: false,
            lengthChange: false,
            ajax: {
                url: "/Admin/GetUsersInRoles/" + role_id
            },
            datasrc: 'data',
            columns: [
                { "data": "id", "visible": false },
                { "data": "username", "width": "90%" },
                {
                    data: "id", render: function (data, type, row, meta) {
                        return type === 'display' ?
                            '<a class="btn btn-danger btn-sm" href="javascript:;" onclick="UsersInRoleDel(' + data + ')" >Çıkar</a>' :
                            data;
                    }
                }
            ]
        });
    }
    else {
        $('#tbl_Users').dataTable().fnClearTable();
    }
}

function UsersInRoleDel(id) { // DELETE UsersInRoles 

    $.ajax({
        url: "/Admin/UsersInRoleDel/",
        type: "POST",
        dataType: "json",
        data: {
            id: id
        },
        success: function (data) {
            if (data.Success)
                Swal.fire({
                    title: '',
                    text: data.Message,
                    icon: 'success',
                    confirmButtonText: 'Tamam'
                }).then(function () {
                    GetUsersInRoles();
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
// USERS IN ROLES -- END ---->


// MENUS IN ROLES -- BEGIN ---->
function MenusInRoles() { // ADD UsersInRoles
    var role_id = $("#ddl_Roles").val();
    var menu_id = $("#ddl_Menus").val();

    if (role_id !== "") {
        $.ajax({
            url: "/Admin/MenusInRoles/",
            type: "POST",
            dataType: "json",
            data: {
                _roleid: role_id,
                _menuid: menu_id
            },
            success: function (data) {
                if (data.Success)
                    Swal.fire({
                        title: '',
                        text: data.Message,
                        icon: 'success',
                        confirmButtonText: 'Tamam'
                    }).then(function () {
                        GetMenusInRoles();
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
}

function GetMenusInRoles() {
    var role_id = $("#ddl_Roles").val();
    if (role_id !== "") {
        var tbl_Menus = $('#tbl_Menus').DataTable({
            info: false,
            lengthChange: false,
            ajax: {
                url: "/Admin/GetMenusInRoles/" + role_id
            },
            datasrc: 'data',
            columns: [
                { "data": "id", "visible": false },
                { "data": "menuname", "width": "90%" },
                {
                    data: "id", render: function (data, type, row, meta) {
                        return type === 'display' ?
                            '<a class="btn btn-danger btn-sm" href="javascript:;" onclick="MenusInRoleDel(' + data + ')" >Çıkar</a>' :
                            data;
                    }
                }
            ],
            "language": {
                "search": "Menü Ara : "
            },
            "oLanguage": {
                "sZeroRecords": "Aradığınız menü bulunamadı.."
            }
        });
    }
    else {
        $('#tbl_Menus').dataTable().fnClearTable();
    }
}

function MenusInRoleDel(id) { // DELETE MenusInRoleDel 

    $.ajax({
        url: "/Admin/MenusInRoleDel/",
        type: "POST",
        dataType: "json",
        data: {
            id: id
        },
        success: function (data) {
            if (data.Success)
                Swal.fire({
                    title: '',
                    text: data.Message,
                    icon: 'success',
                    confirmButtonText: 'Tamam'
                }).then(function () {
                    GetMenusInRoles();
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
// MENUS IN ROLES -- END ---->

function Message(title, message, icon, confirmText) {
    Swal.fire({
        title: title,
        text: message,
        icon: icon,
        confirmButtonText: confirmText
    });
}