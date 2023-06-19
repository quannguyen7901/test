<template>
    <div class="popup__head">
        <div class="popup__name">{{ title }}</div>
        <div class="popup__icon">
            <MisaIcon class="tooltip--top" hastooltip="true" :tooltext="help" icon="fa-light fa-circle-question" styles="icon--24"></MisaIcon>
            <MisaIcon class="tooltip--top" hastooltip="true" :tooltext="close" @click="btnCloseClick" icon="fa-light fa-x" styles="icon--24 icon--close"></MisaIcon>
        </div>
    </div>
</template>

<script>
export default {
    name: "MisaPopupHeader",
    props: {
        //Text hiện thị tên của popup
        title: {
            type: String,
            default: ""
        },
        //Ẩn hiện icon question
        hasquestion:{
            type:Boolean,
            default:false
        }
    },
    data() {
        return {
            help:this.$resource.VN.TextTooltip.Help,
            close:this.$resource.VN.TextTooltip.Close,
        }
    },
    methods: {
        /**
         * Truyền hàm đóng from lên cha
         * Auther: Nguyễn Quân
         * Created:05/2023 
         */
        btnCloseClick(){
            this.$tnemtter.emit("showDailog","update")
            //Nếu confirmDailog trả về ok thì đóng form
        }
    },
}
</script>

<style></style>