<template>
    <div class="popup__button">
        <MisaButton @click="btnSaveOnClick" hastooltip="true" :tooltext="savetool" :text="save" styles="button--noral"></MisaButton>
        <MisaButton v-if="twobutton" hastooltip="true" :tooltext="saveastool" :text="saveas" styles="button--link"></MisaButton>
        <MisaButton  @click="btnCloseOnClick" @keydown.tab.prevent="tabOrder" :text="cancel" styles="button--white"></MisaButton>
    </div>
</template>

<script>
export default {
    name: "MisaPopupButton",
    props:{
        //Popup có 2 hoặc 3 nút
        twobutton:{
            type:Boolean,
            default:true
        },
        //Text button chức năng
        save:{
            type:String,
            default:""
        }
    },
    data() {
        return {
            saveas:this.$resource.VN.TextButton.SaveAs,
            cancel:this.$resource.VN.TextButton.Cancel,
            savetool:this.$resource.VN.TextTooltip.Save,
            saveastool:this.$resource.VN.TextTooltip.SaveAs,
        }
    },
    methods:{
        /**
         * Lưu dữ liệu - Chưa chạy
         * Auther: Nguyễn Quân
         * Created:05/2023 
         */
        btnSaveOnClick(){
            // this.$router.push(`/`)
            this.$tnemtter.emit("btnSaveOnClick")
        },
        /**
         * Tab trong form
         * Auther: Nguyễn Quân
         * Created:05/2023 
         */
         tabOrder(){
            // this.$router.push(`/`)
            this.$tnemtter.emit("tabOrder")
        },
        /**
         * Truyền hàm đóng from lên cha
         * Auther: Nguyễn Quân
         * Created:05/2023 
         */
        btnCloseOnClick(){
            this.$tnemtter.emit("showDailog","update")
        }
    }
}
</script>

<style></style>