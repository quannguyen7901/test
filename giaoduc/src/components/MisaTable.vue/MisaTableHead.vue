<template>
    <thead>
        <tr>
            <th><MisaCheckbox></MisaCheckbox></th>
            <th v-for="(item, index) in datas" :key="index">{{ item }}</th>
        </tr>
    </thead>
</template>

<script>
export default {
    name: "MisaTableHead",
    props:{
        //Tên hiển thị của các cột
        datas:{
            type:[]
        }
    },
}
</script>

<style></style>