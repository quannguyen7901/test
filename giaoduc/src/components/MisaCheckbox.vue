<template>
    <div class="div__checkbox" @click="check">
        <input ref="checkbox" :checked="this.modelValue" type="checkbox">
        <label v-if="text != ''" >{{ text }}</label>
    </div>
</template>

<script>
export default {
    name: "MisaCheckbox",
    props: {
        //Text label hiển thị cạnh checkbox
        text: {
            type: String,
            default: ""
        },
        //Value của vmodel
        modelValue: {
            type: Boolean,
            default: false
        }
    },
    methods: {
        /**
       * Hàm check box
       * Auther: Nguyễn Quân
       * Created:05/2023 
       */
        check() {
            this.$emit('update:modelValue', !this.modelValue)
            // if (!this.$refs.checkbox.checked) {
            //     this.$refs.checkbox.checked = true;
            // }
            // else {
            //     this.$refs.checkbox.checked = false;
            // }
        }
    },
}
</script>

<style>
.div__checkbox {
    height: 16px;
}

div input {
    cursor: pointer;
}

div label {
    cursor: pointer;
}
</style>