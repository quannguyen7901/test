<template>
    <div class="content__item">
        <h1>{{ title }}</h1>
    </div>
    <div class="content__item">
        <div class="content__item--left">
            <div>
                <MisaTextBox @search="search" hasicon="true" label="mã hoặc tên danh hiệu..."></MisaTextBox>
            </div>
            <MisaButton :text="filter" styles="button--white button__filter" hasicon="true" icon="fa-light fa-filter"
                @click="filterOpen"></MisaButton>
        </div>
        <OnClickOutside id="popup__filter" @trigger="() => this.filteropen = false">
            <div v-if="filteropen" class="popup" style="width:360px">
                <i class="fa-solid fa-triangle popup__triangle"></i>
                <MisaFilterBoxHeader :title="filterfrom"></MisaFilterBoxHeader>
                <div class="popup__content">
                    <div class="popup__input">
                        <div class="popup__item">
                            <MisaListbox :datas="data" :label="object"></MisaListbox>
                        </div>
                        <div class="popup__item">
                            <MisaListbox :datas="cap" :label="level"></MisaListbox>
                        </div>
                        <div class="popup__item">
                            <MisaListbox :datas="loai" :label="type"></MisaListbox>
                        </div>
                        <div class="popup__item" style="margin-bottom: 10px;">
                            <MisaListbox :datas="tt" :label="status"></MisaListbox>
                        </div>
                    </div>
                </div>
                <MisaFilterBoxButton @closefilter="filterClose"></MisaFilterBoxButton>
            </div>
        </OnClickOutside>
        <div class="content__item--right">
            <MisaButton v-if="!hasselect" :text="add" styles="button--normal" hasicon="true" icon="fa-regular fa-plus"
                @click="btnAddOnClick()"></MisaButton>
            <div class="content__function" v-else>
                <label>Đã chọn <span>{{ getLength }}</span></label>
                <a @click="$store.dispatch('clearSelected')">Bỏ chọn</a>
                <MisaButton @click="setStatus(1)" :text="$resource.VN.TextFunction.Use" styles="button--link"></MisaButton>
                <MisaButton @click="setStatus(0)" :text="$resource.VN.TextFunction.UnUse" styles="button--white">
                </MisaButton>
                <MisaButton :text="$resource.VN.TextFunction.Delete" styles="button--red"></MisaButton>
            </div>
        </div>
    </div>
</template>

<script>
import MisaTextBox from '@/components/MisaTextBox.vue';
import MisaFilterBoxHeader from '@/components/MisaFilterBox.vue/MisaFilterBoxHeader.vue';
import MisaFilterBoxButton from '@/components/MisaFilterBox.vue/MisaFilterBoxButton.vue';
import { OnClickOutside } from '@vueuse/components';

export default {
    name: "EmulationTitleHeader",
    data() {
        return {
            data: ["Tất cả", "Cá nhân", "Tập thể", "Cá nhân, tập thể"],
            tt: ["Tất cả", "Sử dụng", "Ngưng sử dụng"],
            cap: ["Tất cả", "Cấp Nhà nước", "Cấp Tỉnh/tương đương", "Cấp Huyện/tương đương", "Cấp Xã/tương đương"],
            loai: ["Tất cả", "Thường xuyên", "Theo đợt"],
            filteropen: false,
            add: this.$resource.VN.TextButton.Add,
            filter: this.$resource.VN.TextButton.Filter,
            title: this.$resource.VN.TextTitle.Emulation,
            filterfrom: this.$resource.VN.TextFrom.Filter,
            object: this.$resource.VN.TextLabel.Object,
            type: this.$resource.VN.TextLabel.Type,
            level: this.$resource.VN.TextLabel.Level,
            status: this.$resource.VN.TextLabel.Status,
            hasselect: false,
            baseapi: this.$baseapi,
        }
    },
    components: {
        MisaTextBox, MisaFilterBoxButton, MisaFilterBoxHeader, OnClickOutside
    },
    created() {
        //Hứng sự kiện từ bên ngoài
        this.$tnemtter.on("closefilter", this.filterClose)
        this.$tnemtter.on("addOnClick", this.btnAddOnClick)
    },
    beforeUnmount() {
        //Bỏ hứng sự kiện
        this.$tnemtter.off("addOnClick")
        this.$tnemtter.off("closefilter")
    },
    methods: {
        /**
         * Set trạng thái cho nhiều bản ghi
         * @param {trạng thái} i 
         * auther: NgQuan
         * created:05/2023
         */
        setStatus(i) {
            //Api thay đổi trạng thái của nhiều bản ghi
            this.$axios.put(`${this.baseapi}Emulatitles/Status?status=${i}`, this.$store.state.selected)
                //Api chạy OK
                .then(() => {
                    //Clear stae vuex
                    this.$store.dispatch('clearSelected')
                    //Hiện thông báo
                    this.$tnemtter.emit("Noti", this.$resource.VN.Notification.Sucsess, this.$resource.VN.SucsessText.SucsessUpdate)
                    //Reload lại trang
                    this.$tnemtter.emit("reload")
                })
                //Api chạy ko OK
                .catch(() => {
                    //Hiện thông báo
                    this.$tnemtter.emit("Noti", this.$resource.VN.Notification.Error, this.$resource.VN.ErrorText.Reload)
                })
        },
        /**
         * Tìm kiếm dữ liệu
         * Auther: NgQuan
         * Created:05/2023 
         */
        search(i) {
            this.$tnemtter.emit('search', i)
        },
        /**
         * Hiện from thêm danh hiệu
         * Auther: NgQuan
         * Created:05/2023 
         */
        btnAddOnClick() {
            this.$router.push("/emulatitle/created");
        },
        /**
         * Hiện tùy chọn lọc dữ liệu
         * Auther: NgQuan
         * Created:05/2023 
         */
        filterOpen() {
            this.filteropen = true
        },
        /**
        * Ẩn tùy chọn lọc dữ liệu
        * Auther: NgQuan
        * Created:05/2023 
        */
        filterClose() {
            //Nếu tùy chọn đang đóng
            if (!this.filteropen == true) {
                //Mở tùy chọn
                this.filteropen = true
            }
            //Nếu tùy chọn đang mở 
            else {
                //Đóng tùy chọn
                this.filteropen = false
            }
        }
    },
    watch: {
        //Quan sát chiều dài của state
        getLength(e) {
            //Nếu state !=null
            if (e > 0) {
                //Hiện tùy chọn
                this.hasselect = true
            }
            //Nếu state =null
            else {
                //Ản tùy chọn
                this.hasselect = false
            }
        }
    },
    computed: {
        //Lấy chiều dài của state
        getLength() {
            return this.$store.getters.getLength
        },
    },
}
</script>

<style>
.content__function {
    display: flex;
    align-items: center;
    gap: 8px;
}

.content__function a {
    cursor: pointer;
    margin-left: 12px;
    margin-right: 12px;
    color: var(--color-primary);
}

.content__function a:hover {
    text-decoration: underline;
}
</style>