<template>
    <div :class="'tooltip icon ' + styles">
        <i :class="icon"></i>
        <MisaTooltip v-if="hastooltip" :tooltext="tooltext"></MisaTooltip>
    </div>
</template>

<script>
export default {
    name: "MisaIcon",
    props: {
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
            default: "",
        },
        //Class của icon
        styles: {
            type: String,
            default: "",
        }
    },
}
</script>

<style></style>