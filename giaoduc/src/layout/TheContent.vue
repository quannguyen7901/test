<template>
    <div class="content">
        <MisaToastMessage></MisaToastMessage>
        <router-view></router-view>
    </div>
</template>

<script>
import MisaToastMessage from '@/components/MisaToastMessage.vue'

export default {
    name: "TheContent",
    components: {
        MisaToastMessage,
    },
}
</script>

<style></style>