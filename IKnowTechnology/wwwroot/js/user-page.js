const api = 'https://localhost:44339/'

function GetUserCard() {
    $.ajax({
        url: api + "User/GetUserCard",
        type: "GET",
        success: function (response) {
            console.log(response)
            $('#user-card').html(response);
        }
    })
}

let p_id = "";
function GetUserInfo() {
    $('#modal-edit-profile').modal('show');
    var myUrl = api + "User/GetUserInfo";

    $.ajax({
        url: myUrl,
        type: "GET",
        success: function (response) {
            console.log(response);
            p_id = response.id
            $('#output').attr('src', response.imagePath)
            $('#profilePic').val(response.imagePath);   
            $('#firstName').val(response.firstName);
            $('#lastName').val(response.lastName);
        }
    })
}


function UpdateMyInfo() {
    var myUrl = api + "User/UpdateUser";

    var formData = new FormData();
    formData.append("id", p_id)
    formData.append("ImagePath", $('#profile-picture').attr('src'))    
    formData.append("newPhoto", document.querySelector('#newPhoto').files[0])
    formData.append("firstName", $('#firstName').val())
    formData.append("lastName", $('#lastName').val())

    $.ajax({
        url: myUrl,
        data: formData,
        type: "POST",
        processData: false,
        contentType: false,
        success: function (response) {
            if (response.result == true) {
                GetUserCard();
                toastr.success("Bilgileriniz Güncellendi");
                $('#modal-edit-profile').modal('hide')
                // deploy olduğunda burası cacheden dolayı güncellenmiyor. o yüzden manuel olarak güncelliyoruz.
                $('#profile-picture').attr('src', $('#output').attr('src'));
            }
            else {
                const errList = response.reduce((acc, val) => `${acc}<li>${val}</li>`, "")
                toastr.warning("Bilgiler Güncellenirken Hata Oluştu" + "<br>" + errList)
            }
        },
        error: () => toastr.error("Bilinmeyen bir hata oluştu.")
    })


}