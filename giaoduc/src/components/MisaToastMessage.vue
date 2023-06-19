<template>
    <div ref="noti" class="toast content__warning">
        <div v-if="title == $resource.VN.Notification.Warning" class="toast__icon toast__icon--warning">
            <MisaIcon icon="fa-sharp fa-light fa-exclamation fa-rotate-180" styles="icon--16"></MisaIcon>
        </div>
        <div v-if="title == $resource.VN.Notification.Sucsess" class="toast__icon toast__icon--success">
            <MisaIcon icon="fa-sharp fa-light fa-check" styles="icon--16"></MisaIcon>
        </div>
        <div v-if="title == $resource.VN.Notification.Error" class="toast__icon toast__icon--error">
            <MisaIcon icon="fa-light fa-triangle-exclamation" styles="icon--16"></MisaIcon>
        </div>
        <div class="toast__text">{{ content }}</div>
    </div>
</template>

<script>
export default {
    name: "MisaToastMessage",
    data() {
        return {
            title: "",
            content: ""
        }
    },
    created() {
        //Hứng sự kiện
        this.$tnemtter.on("Noti", this.showNotification)
    },
    beforeUnmount() {
        //Bỏ hứng sự kiện
        this.$tnemtter.off("Noti")
    },
    methods: {
       /**
        * Hiện toast message thông báo
        * @param {Loại thông báo} a 
        * @param {Nội dung thông báo} b 
        * auther: Nguyễn Quân
        * created: 05/2023
        */
        showNotification(a, b) {
            const noti = this.$refs.noti;
            this.title = a;
            this.content = b;
            let id = null;
            let pos = 0;
            let opa = 0;
            noti.style.display = "flex";
            clearInterval(id);
            //Lặp lại sau mỗi 5 mili giây
            id = setInterval(frame, 5);
            //animation xuất hiện của message
            function frame() {
                //Nếu đến vị trí giữa màn hình thì dừng lại
                if (pos == 50.0) {
                    clearInterval(id);
                    // noti.style.display="none";
                }
                //Nếu chưa đến vị trí giữa màn hình thì dịch chuyển thêm
                else {
                    opa += 0.005;
                    pos += 0.25;
                    noti.style.right = pos + "%";
                    noti.style.opacity = opa;
                }
            }
            //Đóng thông báo sau 3s
            setTimeout(this.hideNotification, 3000);
        },
        /**
        * Hàm ẩn toast message thông báo
        * auther: Nguyễn Quân
        * created: 05/2023
        */
        hideNotification() {
            const noti = this.$refs.noti;
            let id = null;
            let pos = 50;
            let opa = 1;
            clearInterval(id);
            //Lặp lại sau mỗi 5 mili giây
            id = setInterval(frame, 5);
            //animation ẩn đi của message
            function frame() {
                //Nếu đến vị trí cuối màn hình thì dừng lại và ẩn đi
                if (pos == 0) {
                    clearInterval(id);
                    noti.style.display = "none";
                }
                //Nếu chưa đến vị trí cuối màn hình thì dịch chuyển thêm
                else {
                    opa -= 0.005;
                    pos -= 0.25;
                    noti.style.right = pos + "%";
                    noti.style.opacity = opa;
                }
            }
        },
    }
}
</script>

<style></style>