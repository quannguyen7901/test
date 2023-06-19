<template>
    <div class="popup__button">
        <MisaButton @click="btnApplyOnClick" :text="apply" styles="button--noral"></MisaButton>
        <MisaButton @click="btnCloseOnClick" :text="cancel" styles="button--white"></MisaButton>
    </div>
</template>

<script>
export default {
    name: "MisaFilterBoxButton",
    props: {
    },
    data() {
        return {
            apply:this.$resource.VN.TextButton.Apply,
            cancel:this.$resource.VN.TextButton.Cancel,
        }
    },
    methods: {
         /**
         * Lọc dữ liệu - Chưa chạy :)
         * Auther: Nguyễn Quân
         * Created:05/2023 
         */
         btnApplyOnClick() {
            this.$tnemtter.emit("Noti", this.$resource.VN.Notification.Error,"Chưa chạy đâu")
        },
         /**
         * Truyền hàm đóng from lên cha
         * Auther: Nguyễn Quân
         * Created:05/2023 
         */
        btnCloseOnClick() {
            this.$tnemtter.emit("closefilter");
        }
    }
}
</script>

<style></style>