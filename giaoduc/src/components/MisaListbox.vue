<template>
     <OnClickOutside @trigger="() => this.hasdata = false">
        <div class="combobox" ref="combo">
            <label for="">{{ label }}</label><span v-if="hasspan">*</span>
            <div class="combobox__field" style="display: flex;">
                <input type="text" placeholder="" :value="values" class="combobox__input">
                <button class="combobox__button" @click="showData"><i
                    class="icon fa-light fa-chevron-down icon--16"></i></button>
                </div>
                <div v-show="hasdata" class="combobox__data">
                    <a @click="selectItem(index)" ref="mitem" v-for="(item, index) in datas" :key="index" value="item"
                    class="combobox__item">{{ item }}</a>
                </div>
            </div>
    </OnClickOutside>
</template>
  
<script>
import { OnClickOutside } from '@vueuse/components'
export default {
    name: "MisaListbox",
    props: {
        //Text label hiển thị của listbox
        label: {
            type: String,
            default: ""
        }, 
        //Ẩn hiện dấu * bắt buộc nhập
        hasspan: {
            type: Boolean,
            default: false
        },
        //Dữ liệu sổ ra của listbox
        datas: {
            type: [],
            default: ""
        },
        //Dữ liệu của vmodel
        modelValue:{
            type:String,
            default:null
        }
    },
    data() {
        return {
            data: this.datas,
            hasdata: false,
            values: "",
        }
    },
    components:{OnClickOutside},
    mounted() {
        //Nếu chưa chọn dữ liệu
        if(this.modelValue==null){
            //chọn dữ liệu đầu tiên
            this.selectItem(0)
        }
        //Nếu đã chọn dữ liệu
        else{
            //tìm dữ liệu trùng khớp
            for (let j = 0; j < this.data.length; j++) {
                let table = this.$refs.mitem[j]
                //Tìm phần tử trong bảng
                if(table.innerText==this.modelValue){
                    //chọn dữ liệu
                    this.selectItem(j)
                    break
                }
            }
        }
    },
    methods: {
        /**
         * Update dữ liệu của vmodel
         * @param {dữ liệu} a 
         * auther: NgQuan
         * created: 05/2023
         */
        update(a) {
            this.$emit("update:modelValue", a)
        },
        /**
         * Chọn một phần tử trong bảng
         * auther:NgQuan
         * @param {Vị trí của phần tử trong bảng} i 
         * create:05/2023
         */
        selectItem(i) {
            //tìm vị trí trùng khớp
            for (let j = 0; j < this.data.length; j++) {
                let table = this.$refs.mitem[j]
                //Tìm phần tử trong bảng
                if (i == j) {
                    //Nếu phần tử đó chưa được chọn
                    if (!table.classList.contains("combobox__item--select")) {
                        this.values = table.innerText
                        //Chọn phần tử đó
                        table.classList.add("combobox__item--select")
                    }
                }
                else {
                    //Nếu không phải phần tử được chọn
                    if (table.classList.contains("combobox__item--select")) {
                        //Bỏ chọn phần tử
                        table.classList.remove("combobox__item--select")
                    }
                }
                this.update(this.values)
                this.hasdata = false
            }
        },
        /**
         * Hiện dữ liệu xổ ra của listbox
         * auther: NgQuan
         * created: 05/2023
         */
        showData() {
            //Nếu dữ liệu đang đóng
            if (this.hasdata == false) {
                //Mở
                this.hasdata = true
            }
            //Nếu dữ liệu đang mở
            else {
                //Đóng
                this.hasdata = false
            }
        }
    },
}
</script>
  
<style></style>