<template>
    <div class="header">
        <div class="header__item--left">
            <MisaIcon @click="showNoti" icon="fa-solid fa-grid" styles="icon--24 header__icon"></MisaIcon>
            <MisaIcon styles="icon--36 header__logo"></MisaIcon>
            <div class="header__text">
                <h1>{{ name }}</h1>
            </div>
        </div>
        <div class="header__item--right">
            <MisaIcon @click="showNoti" hastooltip="true" :tooltext="noti" class="tooltip--bottom" icon="fa-light fa-bell"
                styles="icon--24 header__icon"></MisaIcon>
            <MisaIcon @click="showNoti" hastooltip="true" :tooltext="help" class="tooltip--bottom"
                icon="fa-light fa-circle-question" styles="icon--24 header__icon"></MisaIcon>
            <MisaIcon @click="showNoti" hastooltip="true" :tooltext="doc" class="tooltip--bottom"
                icon="fa-light fa-book-open" styles="icon--24 header__icon"></MisaIcon>
            <MisaIcon @click="showNoti" hastooltip="true" :tooltext="set" class="tooltip--bottom" icon="fa-light fa-gear"
                styles="icon--24 header__icon"></MisaIcon>
            <MisaIcon @click="showNoti" styles="header__user"></MisaIcon>
        </div>
    </div>
</template>

<script>
export default {
    name: "TheHeader",
    data() {
        return {
            name: this.$resource.VN.LogoName,
            noti: this.$resource.VN.TextTooltip.Noti,
            help: this.$resource.VN.TextTooltip.Help,
            doc: this.$resource.VN.TextTooltip.Doc,
            set: this.$resource.VN.TextTooltip.Set,
        }
    },
    methods: {
         /**
       * Hàm hiện cảnh báo
       * Auther: Nguyễn Quân
       * Created:05/2023 
       */
        showNoti() {
            this.$tnemtter.emit("Noti",this.$resource.VN.Notification.Warning,this.$resource.VN.ErrorText.ErrorNull);
        }
    },
}
</script>

<style></style>