<template>
    <div class="daialog__cover">
        <div class="daialog">
            <div class="daialog__head">
                <div class="daialog__name">{{ title }}</div>
                <MisaIcon @click="hideDailog" styles="icon--16 daialog__exit" icon="fa-regular fa-x"></MisaIcon>
            </div>
            <div class="daialog__content">
                {{ content }}
            </div>
            <div class="daialog__button">
                <MisaButton @click="hideDailog" text="Hủy" styles="button--white"></MisaButton>
                <MisaButton @click="confirmDailog" :text="button" :styles="style"></MisaButton>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "MisaDailog",
    props: {
        //Text hiển thị của dailog
        title: {
            type: String,
            default: "",
        },
        //Dạng dailog
        type: {
            type: String,
            default: "delete"
        },
        //Text hiển thị trên nút chức năng
        button: {
            type: String,
            default: "Xóa"
        }
    },
    data() {
        return {
            style:"",
            content: this.$resource.VN.WarningText.Delete
        }
    },
    created() {
        //Cảnh báo khi đóng from
        if (this.type == "update") {
            this.style=""
            this.content= this.$resource.VN.WarningText.Exit
        }
        //Cảnh báo khi xóa bản ghi
        else{
            this.style="button--warning"
            this.content= this.$resource.VN.WarningText.Delete
        }
    },
    methods: {
        /**
        * Truyền hàm đóng form ra ngoài
        * Auther: Nguyễn Quân
        * Created:05/2023 
        */
        hideDailog() {
            this.$tnemtter.emit("hideDailog")
        },
        /**
        * Truyền hàm confirmDailog ra ngoài
        * Auther: Nguyễn Quân
        * Created:05/2023 
        */
        confirmDailog() {
            this.$tnemtter.emit("confirmDailog","ok")
            this.hideDailog()
        }
    },
}
</script>

<style></style>