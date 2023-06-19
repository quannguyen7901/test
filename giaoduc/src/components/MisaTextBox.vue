<template>
    <div>
        <label v-if="haslabel">{{ label }}</label><span v-if="hasspan">*</span>
    </div>
    <input type="text" :value="modelValue" ref="txtfist" v-if="!hasicon" @focusout="inputFunction"
        @input="update($event.target.value)" class="input input--normal" :placeholder="'Nhập ' + label">
    <input @input="e => search(e.target.value)" type="text" v-model="datas" v-if="hasicon"
        class="input icon input__icon--left input--normal" :placeholder="'Nhập ' + label">
</template>

<script>
// import { ref } from 'vue';
export default {
    name: "MisaTextBox",
    props: {
        //Dữ liệu của vmodel
        modelValue: {
            type: String,
            default: ""
        },
        //Textbox có icon hoặc không
        hasicon: {
            type: Boolean,
            default: false
        },
        //Textbox có label ở trên hay không
        haslabel: {
            type: Boolean,
            default: false
        },
        //Ẩn hiện * bắt buộc nhập
        hasspan: {
            type: Boolean,
            default: false
        },
        //Text label hiện trên text box
        label: {
            type: String,
            default: ""
        },
        //Textbõ có được focus khi tạo ra hay không
        focus: {
            type: Boolean,
            default: false
        },
    },
    data() {
        return {
            timeout:null,
        }
    },
    created() {
        //Hứng sự kiện bên ngoài
        this.$tnemtter.on("tabOrder", this.tabOrder)
        //Focus textbox
        this.focucsInput()
    },
    updated() {
        //Check null cho textbox
        this.inputFunction()
    },
    beforeUnmount() {
        //Bỏ hứng sự kiện
        this.$tnemtter.off("tabOrder")
    },
    methods: {
        fun(val) {
            console.log('value', val)
        },
        /**
         * Tìm kiếm
         * @param {dữ liệu tìm kiếm} val 
         */
        search(value) {
            if (this.timeout) {
                    clearTimeout(this.timeout); // clear any pre-existing timer
                } // get the current context
                this.timeout = setTimeout(() => {
                this.$emit("search",value)
                }, 300);
        },
        /**
        * Tùy chọn focus input
        * Auther: Nguyễn Quân
        * Created:05/2023 
        */
        focucsInput() {
            if (this.focus) {
                this.$nextTick(() => {
                    this.$refs.txtfist.focus();
                })
            }
        },
        /**
        * Tab trong form
        * Auther: Nguyễn Quân
        * Created:05/2023 
        */
        tabOrder() {
            this.focucsInput()
        },
        /**
        * Thay đổi dữ liệu khi cập nhật data
        * Auther: Nguyễn Quân
        * Created:05/2023 
        */
        update(a) {
            this.$emit("update:modelValue", a)
        },
        /**
        * Validate dữ liệu trong form
        * Auther: Nguyễn Quân
        * Created:05/2023 
        */
        inputFunction() {
            const input = this.$refs.txtfist
            // kiểm tra input có lỗi không
            if (input.value == "") {
                //thêm viền đỏ
                input.classList.add("input--error");
                //Thêm thông báo lỗi
                if (input.nextElementSibling == null) {
                    //Tạo thông báo
                    const para = document.createElement("div");
                    //Gán class thông báo
                    para.classList.add("input__text--error");
                    //Gán nội dung thông báo
                    const node = document.createTextNode(
                        this.label + this.$resource.VN.ErrorText.TextNull);
                    para.appendChild(node);
                    //Gán thông báo vào dưới input
                    input.parentElement.appendChild(para);
                }
            }
            else {
                input.classList.remove("input--error");
                //Bỏ thông báo lỗi dưới input
                if (input.nextElementSibling) {
                    input.parentElement.removeChild(input.nextElementSibling);
                }
            }
        }
    },
}
</script>

<style></style>