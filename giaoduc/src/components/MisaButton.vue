<template>
    <button :class="'tooltip tooltip--top button ' + styles">
        <MisaIcon v-if="hasicon" :icon="icon"></MisaIcon>
        <div class="button__text">{{ text }}</div>
        <MisaTooltip v-if="hastooltip" :tooltext="tooltext"></MisaTooltip>
    </button>
</template>

<script>
export default {
    name: "MisaButton",
    props: {
        //Có hoặc không có icon
        hasicon: {
            type:Boolean,
            default:false,
        },
        //Text hiển thị của tooltip
        tooltext:{
            type:String,
            default:""
        },
        //Có hoặc không có tooltip
        hastooltip:{
            type:Boolean,
            default:false,
        },
        //Icon hiển thị, đang dùng fontawesome.com
        icon: {
            type: String,
            default: ""
        },
        //Class của button
        styles: {
            type: String,
            default: "button--normal"
        },
        //Text hiển thị của button
        text: {
            style: String,
            default: ""
        }
    }
}
</script>

<style></style>