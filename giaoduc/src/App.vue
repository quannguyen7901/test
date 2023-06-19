<template>
  <TheHeader></TheHeader>
  <TheSidebar></TheSidebar>
  <TheContent></TheContent>
  <!-- <MisaLoading></MisaLoading> -->
</template>

<script>
import TheHeader from "./layout/TheHeader.vue";
import TheSidebar from "./layout/TheSidebar.vue";
import TheContent from "./layout/TheContent.vue";
// import MisaLoading from "./components/MisaLoading.vue";

export default {
  name: 'App',
  components: {
    TheHeader, TheSidebar, TheContent
  },
  mounted() {
    /**
        * Chèn file js của font
        * Auther: NgQuan
        * Created:05/2023 
        */
    const plugin = document.createElement("script");
    plugin.setAttribute(
      "src",
      "http://127.0.0.1:8887/all.min.js"
    );
    plugin.async = true;
    document.body.appendChild(plugin);
  },
}
</script>

<style>
@import url(./css/main.css);
@import url(./assets/font/fontawesome-pro-6.1.1-web/css/all.min.css);
</style>
