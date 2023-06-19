<template>
    <div class="popup__head filter__head">
        <div class="popup__name">{{ title }}</div>
        <div class="popup__icon">
            <MisaIcon icon="fa-light fa-x" styles="icon--24 icon--close" @click="btnCloseClick"></MisaIcon>
        </div>
    </div>
</template>

<script>
export default {
    name: "MisaFilterBoxHeader",
    props: {
        title: {
            type: String,
            default: ""
        }
    },
    methods: {
        /**
         * Truyền hàm đóng from ra ngoài để có thể ẩn form
         * Auther: Nguyễn Quân
         * Created:05/2023 
         */
        btnCloseClick() {
            this.$tnemtter.emit("closefilter");
        }
    },
}
</script>

<style>
.filter__head {
    padding-top: 12px;
}
</style>