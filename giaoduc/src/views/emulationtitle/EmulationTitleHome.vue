<template>
    <div class="main">
        <EmulationTitleHeader></EmulationTitleHeader>
        <EmulationTitleTable></EmulationTitleTable>
        <router-view name="EmulationTitleForm"></router-view>
    </div>
    <MisaDailog v-if="showdailog" :type="type" :title="title" :button="button"></MisaDailog>
</template>

<script>
// import EmulationTitleForm from './EmulationTitleForm.vue';
import EmulationTitleTable from './EmulationTitleTable.vue';
import EmulationTitleHeader from './EmulationTitleHeader.vue';
import MisaDailog from '@/components/MisaDailog.vue';

export default {
    name: "EmulationTitleHome",
    data() {
        return {
            title: this.$resource.VN.TextDailog.Delete,
            button: this.$resource.VN.TextButton.Delete,
            showdailog: false,
        }
    },
    components: {
        EmulationTitleTable, EmulationTitleHeader, MisaDailog, 
    },
    created() {
        //Hứng sự kiện từ bê ngoài
        this.$tnemtter.on("showDailog", this.showDailog)
        this.$tnemtter.on("hideDailog", () => { this.showdailog = false })
    },
    beforeUnmount() {
        //Bỏ hứng sự kiện 
        this.$tnemtter.off("showDailog")
        this.$tnemtter.off("hideDailog")
    },
    methods: {
        /**
         * Hiện dailog cảnh báo khi thao tác
         * Auther: Nguyễn Quân
         * Created:05/2023 
         */
        showDailog(e) {
            //Khi đóng popup thì hiện dailog cảnh báo
            if (e == "update") {
                this.title = this.$resource.VN.TextDailog.Noti
                this.type = "update"
                this.showdailog = true
                this.button = this.$resource.VN.TextButton.Confirm
            }
            //Khi xóa thì hiện dailog xóa
            else if (e=="delete") {
                this.title = this.$resource.VN.TextDailog.Delete
                this.type = "delete"
                this.showdailog = true
                this.button = this.$resource.VN.TextButton.Delete
            }
        }
    },
}
</script>

<style>
.main {
    height: calc(100vh - 56px - 48px);
    width: 100%;
    flex: 1;
    display: flex;
    flex-direction: column;
}
</style>