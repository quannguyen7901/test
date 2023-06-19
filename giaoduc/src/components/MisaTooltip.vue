<template>
    <span class="tooltiptext">{{ tooltext }}</span>
</template>

<script>
export default {
    name: "MisaTooltip",
    props: {
        //Text sẽ hiện trên tooltip
        tooltext: {
            type: String,
            default: ""
        }
    }
}
</script>

<style></style>