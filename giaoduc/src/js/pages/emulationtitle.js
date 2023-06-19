window.onload = function () {
    //hàm thao tác
    initEvents();
    //lấy dữ liệu cho table
}
/**
 * Hàm gán các sự kiện
 * auther: Nguyễn Quân
 */
function initEvents() {
    //gán sự kiện click cho button thêm
    document.querySelector("#btnadd").addEventListener("click", btnAddOnClick);
    //gán sự kiện click cho button close
    document.querySelector(".popup__icon .icon--close").addEventListener("click", btnCloseClick);
    //gán sự kiện chick chọn tất cả checkbox
    document.querySelector("table thead input").addEventListener("click", selectAll);
    //gán sự kiện click đóng mở sidebar
    document.querySelector(".sidebar__collapse").addEventListener("click", sideBarOpen);
    //Gán sự kiện hover cho bảng
    const table = document.querySelectorAll(" tbody tr");
    for (let i = 0; i < table.length; i++) {
        table[i].addEventListener("mouseenter", showFunction);
        table[i].addEventListener("mouseleave", hideFunction);
    }
    //Validate dữ liệu
    const input = document.querySelectorAll(".popup__cover .popup__content .popup__item .input");
    for (let i = 0; i < input.length; i++) {
        input[i].addEventListener("focusout", inputFunction);
        input[i].addEventListener("keyup", inputFunction);
    }
    //Gán sự kiện cho form
    const form=document.querySelector(".popup__cover .popup");
    form.lastElementChild.lastElementChild.addEventListener("keyup",focusFunction);
    form.lastElementChild.lastElementChild.addEventListener("click",btnCloseClick);
    //Gán sự kiện cho menu slidebar
    const sidebarItem=document.querySelectorAll(".sidebar__item");
    for(let i=0;i<sidebarItem.length;i++){
        sidebarItem[i].addEventListener("click",showNotification);
    }
}

/**
 * Hàm hiện form thêm
 * auther: Nguyễn Quân
 */
function btnAddOnClick() {
    document.querySelector(".popup__cover").style.display = "flex";
    //focus ô nhập liệu đầu tiên
    document.querySelector(".popup__cover .input").focus();
}

/**
 * Hàm hiện form thêm
 * auther: Nguyễn Quân
 */
function showNotification() {
    const noti=document.querySelector(".content__warning");
    noti.style.display="flex";
    setTimeout(hideNotification, 4000);
}

function hideNotification() {
    const noti=document.querySelector(".content__warning");
    noti.style.display="none";
}

/**
 * Hàm đóng popup
 * auther: Nguyễn Quân
 */
function btnCloseClick() {
    //Ẩn popup
    document.querySelector(".popup__cover").style.display = "none";
    const input = document.querySelectorAll(".popup__cover .popup__content .popup__item .input");
    //Ẩn các thhong báo lỗi của input
    for (let i = 0; i < input.length; i++) {
        input[i].classList.remove("input--error");
        if(input[i].nextElementSibling){
            input[i].parentElement.removeChild(input[i].nextElementSibling);
        }
    }
}

/**
 * Hàm chọn tất cả checkbox
 * auther: Nguyễn Quân
 */
function selectAll() {
    try {
        let check = document.querySelectorAll("table tbody input");
        //kiểm tra trạng thái checkbox đầu tiên
        if (document.querySelector("table thead input").checked) {
            //chọn tất cả checkbox
            for (let i = 0; i < check.length; i++) {
                check[i].checked = true;
            }
        }
        else {
            //bỏ chọn tất cả checkbox
            for (let i = 0; i < check.length; i++) {
                check[i].checked = false;
            }
        }
    } catch (error) {
        console.log(error);
    }
}

/**
 * Hàm hiện function của bảng
 * auther: Nguyễn Quân
 */
function showFunction() {
    this.lastElementChild.style.display = "flex";
}

/**
 * Hàm ẩn function của bảng
 * auther: Nguyễn Quân
 */
function hideFunction() {
    this.lastElementChild.style.display = "none";
}

/**
 * Hàm check input
 * auther: Nguyễn Quân
 */
function inputFunction(e) {
    try {
        //kiểm tra input có lỗi không
        if (!this.value && e.keyCode != 9) {
            //thêm viền đỏ
            this.classList.add("input--error");
            //Thêm thông báo lỗi
            if (this.nextElementSibling == null) {
                //Tạo thông báo
                const para = document.createElement("div");
                //Gán class thông báo
                para.classList.add("input__text--error");
                //Gán nội dung thông báo
                const node = document.createTextNode(
                    this.previousElementSibling.previousElementSibling.previousElementSibling.textContent + " không được để trống");
                para.appendChild(node);
                //Gán thông báo vào dưới input
                this.parentElement.appendChild(para);
            };
        }
        //Bỏ lỗi input
        else {
            //Bỏ viền đỏ
            this.classList.remove("input--error");
            //Bỏ thông báo lỗi dưới input
            if (this.nextElementSibling) {
                this.parentElement.removeChild(this.nextElementSibling);
            }
        }
    } catch (error) {
        console.log(error);
    }
}

/**
 * Hàm đóng mở sidebar
 * auther: Nguyễn Quân
 */
function sideBarOpen() {
    let icon = document.querySelectorAll(".sidebar--iconright");
    let text = document.querySelectorAll(".sidebar__text");
    let sidebar = document.querySelector(".sidebar");
    try {
        //Đóng slidebar
        if (sidebar.offsetWidth == 200) {
            //Thu nhỏ slidebar
            sidebar.style.width = "73px";
            //Ẩn các icon bên phải
            for (let i = 0; i < icon.length; i++) {
                icon[i].style.display = "none";
            }
            //Ẩn các text của slidebar
            for (let i = 0; i < text.length; i++) {
                text[i].style.display = "none";
            }
            //Đổi icon thu phóng
            document.querySelector(".sidebar__collapse .icon").innerHTML = '<i class="fa-duotone fa-chevrons-right"></i>';
        }
        //Mở slidebar
        else {
            //mở rộng slidebar
            sidebar.style.width = "200px";
            //Hiện các icon bên phải
            for (let i = 0; i < icon.length; i++) {
                icon[i].style.display = "block";
            }
            //Hiện text
            for (let i = 0; i < text.length; i++) {
                text[i].style.display = "block";
            }
        }
    } catch (error) {
        console.log(error);
    }
}

/**
 * Hàm giới hạn chỉ tab trong form
 */
function focusFunction(e){
    // const form=document.querySelector(".popup__cover .popup");

    if(e.keyCode==9){
        document.querySelector(".popup__cover .input").focus();
    }
}