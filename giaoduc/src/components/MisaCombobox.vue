<template>
    <OnClickOutside @trigger="() => this.hasdata = false">
        <div class="combobox" ref="combo">
            <label for="">{{ label }}</label><span v-if="hasspan">*</span>
            <div class="combobox__field">
                <input type="text" placeholder="" :value="values" class="combobox__input">
                <button class="combobox__button" @click="showData" tabindex="-1"><i
                        class="icon fa-light fa-chevron-down icon--16"></i></button>
            </div>
            <div v-show="hasdata" class="combobox__data">
                <li ref="mitem" v-for="(item, index) in combodata" :key="index" @click="selectItem(index)"
                    class="combobox__item">{{ item[nametable + "Name"] }}</li>
            </div>
        </div>
    </OnClickOutside>
</template>

<script>

import { OnClickOutside } from '@vueuse/components'

export default {
    name: "MisaCombocox",
    props: {
        //Tên bảng trong cơ sở dữ liệu
        nametable: {
            type: String,
            default: ""
        },
        //Text label hiển thị của combobox
        label: {
            type: String,
            default: ""
        },
        //Ẩn, hiện dấu * bắt buộc nhập
        hasspan: {
            type: Boolean,
            default: false
        },
        //Dữ liệu của combobox (Đang lấy bằng api)
        combodata: {
            type: []
        },
        //Values của combobox vmodel
        comboValue: {
            type: String,
            default: ""
        }
    },
    data() {
        return {
            hasdata: false,
            values: "",
            id: "",
        };
    },
    components: { OnClickOutside },
    mounted() {
        //Add sự kiện nhấn phím
        this.$el.addEventListener("keydown", this.keyShort);
        //Nếu chưa chọn dữ liệu
        if (this.comboValue == "") {
            //chọn dữ liệu đầu tiên
            this.selectItem(0);
        }
        //Nếu đã chọn dữ liệu
        else {
            //tìm dữ liệu trùng khớp
            for (let j = 0; j < this.combodata.length; j++) {
                //Tìm phần tử trong bảng
                if (this.combodata[j][this.nametable + "Id"] == this.comboValue) {
                    //Chọn dữ liệu
                    this.selectItem(j);
                }
            }
        }
    },
    beforeUnmount() {
        this.$el.removeEventListener("keydown", this.keyShort);
    },
    methods: {
        /**
 * Cài đặt phím tắt
 * Auther: Nguyễn Quân
 * Created:05/2023 
 */
        keyShort(e) {
            if (e.keyCode == this.$enum.KeyCode.ArrowDown) {
                // this.$tnemtter.emit('selectTrNext')
                this.selectNextItem()
                this.hasdata = true
            }
            else if (e.keyCode == this.$enum.KeyCode.ArrowUp) {
                // this.$tnemtter.emit('selectTrPre')
                this.selectPreItem()
                this.hasdata = true
            }
        },
        /**
        * Tìm phần tử theo class
        * @param {Class muốn tìm } classname 
        * @returns{vị trí của bản ghi}
        * auther: NgQuan 05/2023
        */
        searchItemClass(classname) {
            for (let j = 0; j < this.combodata.length; j++) {
                if (this.$refs.mitem[j].classList.contains(classname)) {
                    return j
                }
            }
            return -1
        },
        /**
        * Thay đổi dữ liệu khi cập nhật data
        * Auther: Nguyễn Quân
        * Created:05/2023
        */
        update(a) {
            this.$emit("update:comboValue", a);
        },
        /**
         * Chọn một phần tử trong bảng
         * auther:NgQuan 05/2023
         */
        selectNextItem() {
            let i = this.searchItemClass("combobox__item--select")
            if (i + 1 < this.combodata.length) {
                this.selectItem(i + 1)
            }
            else {
                this.selectItem(0)
            }
        },
        /**
         * Chọn một phần tử trong bảng
         * auther:NgQuan 05/2023
         */
        selectPreItem() {
            let i = this.searchItemClass("combobox__item--select")
            if (i - 1 >= 0) {
                this.selectItem(i - 1)
            }
            else {
                this.selectItem(this.combodata.length - 1)
            }
        },
        /**
         * Chọn một phần tử trong bảng
         * auther:NgQuan
         * @param {Vị trí của phần tử trong bảng} i
         * create:05/2023
         */
        selectItem(i) {
            for (let j = 0; j < this.combodata.length; j++) {
                let table = this.$refs.mitem[j];
                //Tìm phần tử trong bảng
                if (i == j) {
                    //Nếu phần tử đó chưa được chọn
                    if (!table.classList.contains("combobox__item--select")) {
                        this.values = table.innerText;
                        this.id = this.combodata[i][this.nametable + 'Id'];
                        //Chọn phần tử đó
                        table.classList.add("combobox__item--select");
                    }
                }
                else {
                    //Nếu không phải phần tử được chọn
                    if (table.classList.contains("combobox__item--select")) {
                        //Bỏ chọn phần tử
                        table.classList.remove("combobox__item--select");
                    }
                }
                this.hasdata = false;
                this.update(this.id);
            }
        },
        /**
         * Ẩn hiện data
         * auther: NgQuan
         * created: 05/2023
         */
        showData() {
            if (this.hasdata == false) {
                this.hasdata = true;
            }
            else {
                this.hasdata = false;
            }
        }
    },
}
</script>

<style></style>