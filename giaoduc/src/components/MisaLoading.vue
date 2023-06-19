<template>
    <div class="loading">
        <div class="icon icon--64"></div>
    </div>
</template>

<script>
export default {
    name: "MisaLoading"
}
</script>

<style>
.loading {
    position: absolute;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    background: #00000075;
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 10;
    color: var(--color-primary);
}

.loading div {
    border: 3px solid var(--color-primary);
    border-radius: 50%;
    border-top: 3px solid #fff;
    /* Safari */
    animation: spin 2s linear infinite;
}

@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}
</style>