<template>
    <div @click="selectRadio">
        <input type="radio" ref="radio" :name="name" :checked="status" :value="val"> <label> {{ label }} </label>
    </div>
</template>

<script>
export default {
    name: "MisaRadioButton",
    props: {
        //Text label hiện bên cạnh radiobutton
        label: {
            type: String,
            default: ""
        },
        //Name của radiobutton
        name: {
            type: String,
            default: ""
        },
        //Trạng thái của radiobutton
        status:{
            type:Boolean,
            default:false
        },
        //Values của radiobutton
        val:{
            type:Number
        },
        //Giá trị của vmodel
        statusValue:{
            type:Number
        }
    },
    methods: {
        /**
         * Chọn radiobutton
         * auther: NgQuan
         * created: 05/2023
         */
        selectRadio() {
            this.$refs.radio.checked = true
            //Cập nhật giá trị của vmodel
            this.$emit("update:statusValue", this.val)
        }  
    },
}
</script>

<style></style>