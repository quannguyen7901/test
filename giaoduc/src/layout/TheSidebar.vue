<template>
    <div class="sidebar">
        <router-link to="/overview" class="sidebar__item">
            <div>
                <MisaIcon icon="fa-light fa-chart-pie-simple" styles="icon--24"></MisaIcon>
                <div class="sidebar__text">{{ text[0] }}</div>
            </div>
            <div class="icon"></div>
        </router-link>
        <div @click="showNotification" class="sidebar__item">
            <div>
                <MisaIcon icon="fa-light fa-clipboard-check" styles="icon--24"></MisaIcon>
                <div class="sidebar__text">{{ text[1] }}</div>
            </div>
            <MisaIcon icon="fa-light fa-chevron-right" styles="sidebar--iconright"></MisaIcon>
        </div>
        <div @click="showNotification" class="sidebar__item">
            <div>
                <MisaIcon icon="fa-light fa-award" styles="icon--24"></MisaIcon>
                <div class="sidebar__text">{{ text[2] }}</div>
            </div>
            <MisaIcon icon="fa-light fa-chevron-right" styles="sidebar--iconright"></MisaIcon>
        </div>
        <div @click="showNotification" class="sidebar__item">
            <div>
                <MisaIcon icon="fa-light fa-file-pen" styles="icon--24"></MisaIcon>
                <div class="sidebar__text">{{ text[3] }}</div>
            </div>
            <MisaIcon icon="fa-light fa-chevron-right" styles="sidebar--iconright"></MisaIcon>
        </div>
        <div @click="showNotification" class="sidebar__item">
            <div>
                <MisaIcon icon="fa-light fa-user-group" styles="icon--24"></MisaIcon>
                <div class="sidebar__text">{{ text[4] }}</div>
            </div>
            <MisaIcon icon="fa-light fa-chevron-right" styles="sidebar--iconright"></MisaIcon>
        </div>
        <div @click="showNotification" class="sidebar__item">
            <div>
                <MisaIcon icon="fa-light fa-magnifying-glass"></MisaIcon>
                <div class="sidebar__text">{{ text[5] }}</div>
            </div>
            <MisaIcon icon="fa-light fa-chevron-right" styles="sidebar--iconright"></MisaIcon>
        </div>
        <div @click="showNotification" class="sidebar__item">
            <div>
                <MisaIcon icon="fa-light fa-file-lines"></MisaIcon>
                <div class="sidebar__text">{{ text[6] }}</div>
            </div>
            <div class="icon sidebar--iconright"></div>
        </div>
        <div style="background-color: #bbb;height: 2px;">
        </div>
        <router-link to="/emulatitle" class="sidebar__item">
            <div>
                <MisaIcon icon="fa-light fa-memo"></MisaIcon>
                <div class="sidebar__text">{{ text[7] }}</div>
            </div>
            <MisaIcon icon="fa-light fa-chevron-right" styles="sidebar--iconright"></MisaIcon>
        </router-link>
        <div @click="showNotification" class="sidebar__item">
            <div>
                <MisaIcon icon="fa-light fa-gear"></MisaIcon>
                <div class="sidebar__text">{{ text[8] }}</div>
            </div>
            <MisaIcon icon="fa-light fa-chevron-right" styles="sidebar--iconright"></MisaIcon>
        </div>
        <div  style="background-color: #bbb;height: 2px;">
        </div>
        <div @click="showNotification" class="sidebar__item">
            <div>
                <MisaIcon icon="fa-light fa-message-question"></MisaIcon>
                <div class="sidebar__text">{{ text[9] }}</div>
            </div>
            <MisaIcon icon="fa-light fa-chevron-right" styles="sidebar--iconright"></MisaIcon>
        </div>
        <div class="sidebar__item sidebar__collapse" @click="btnCollapseOnClick">
            <div>
                <MisaIcon icon="fa-duotone fa-chevrons-left"></MisaIcon>
                <div class="sidebar__text">{{ text[10] }}</div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "TheSiderbar",
    data() {
        return {
            text:this.$resource.VN.SideBar
        }
    },
    methods: {
        /**
         * Hàm hiện ra toast message thông báo
         * auther: Nguyễn Quân
         * created: 05/2023
         */
        showNotification() {
            this.$tnemtter.emit("Noti",this.$resource.VN.Notification.Warning,this.$resource.VN.ErrorText.ErrorNull)
        },
        /**
         * Đóng mở slidebar
         * auther: Nguyễn Quân
         * created: 05/2023
         */
        btnCollapseOnClick() {
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
                    document.querySelector(".sidebar__collapse .icon").innerHTML = '<i class="fa-duotone fa-chevrons-left"></i>';
                }
            } catch (error) {
                console.log(error);
            }
        },
    },
}
</script>

<style></style>