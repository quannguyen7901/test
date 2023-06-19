<template>
    <tbody>

        <tr v-for="item in datas" :key="item[tablekey]" @dblclick="onClickDetail(item[tablekey])"
            @click="selectTr(item[tablekey])" @mouseenter="showFunction(item[tablekey])"
            @mouseleave="hideFunction(item[tablekey])" ref="table">
            <td>
                <MisaCheckbox v-model="item.isChecked"></MisaCheckbox>
            </td>
            <td v-for="it in colum" :key="it">
                <label v-if="it.type == 'string'">{{ item[it.value] }}</label>
                <label v-else>{{ this.$enum[it.value][item[it.value]] }}</label>
            </td>
            <td v-if="item.Status == 1">
                <div class="table__status table__status--active"></div><label for="">{{ $resource.VN.TextFunction.Use
                }}</label>
            </td>
            <td v-else>
                <div class="table__status"></div><label for="">{{ $resource.VN.TextFunction.UnUse }}</label>
            </td>
            <div ref="functions" class="table__function">
                <div>
                    <MisaIcon @click="onClickDetail(item[tablekey])" hastooltip="true" :tooltext="edit" class="tooltip--top"
                        styles="icon--16" icon="fa-light fa-pen"></MisaIcon>
                </div>
                <div @click="showFunctionText(item[tablekey])">
                    <MisaIcon styles="icon--20" hastooltip="true" :tooltext="more" class="tooltip--top"
                        icon="fa-light fa-ellipsis"></MisaIcon>
                    <div ref="functions__text" class="table__function--text">
                        <div v-if="item.Status==0" @click="setStatusItem(item[tablekey],1)" class="text">{{ use }}</div>
                        <div v-else class="text text--disable">{{ use }}</div>
                        <div  v-if="item.Status==1" ref="notuse" @click="setStatusItem(item[tablekey],0)" class="text">{{ unuse }}</div>
                        <div v-else class="text text--disable">{{ unuse }}</div>
                        <div class="red text" @click="btnDelete(item[tablekey])">{{ deleted }}</div>
                    </div>
                </div>
            </div>
        </tr>
        <!-- <MisaTableBodyTr :colum="colum" :scroll="scroll" @show="onClickDetail(item)" v-for="item in datas" :key="item" @dblclick="onClickDetail(item)" :datas="item"></MisaTableBodyTr> -->
    </tbody>
</template>

<script>
// import { keys } from '@/assets/font/fontawesome-pro-6.1.1-web/js/v4-shims'
// import router from '@/router/router'
// import MisaTableBodyTr from './MisaTableBodyTr.vue';

export default {
    name: "MisaTableBody",
    props: {
        //Dữ liệu của table
        datas: {
            type: [],
            default: ""
        },
        //Giá trị thanh scroll của bảng
        scroll: {
            type: Number,
            default: 0
        },
        //Tên bảng và dữ liệu trong cơ sở dữ liệu
        colum: {
            type: [],
            default:[{type:"string",value:"Test"}]
        },
        //Khóa chính của bảng trong cơ sở dữ liệu
        tablekey: {
            type: String,
            default: ""
        }
    },
    data() {
        return {
            hasfuntion: false,
            more: this.$resource.VN.TextTooltip.More,
            edit: this.$resource.VN.TextTooltip.Edit,
            use: this.$resource.VN.TextFunction.Use,
            unuse: this.$resource.VN.TextFunction.UnUse,
            deleted: this.$resource.VN.TextFunction.Delete,
            status: "",
            selected:"",
            modelTest: false,
        }
    },

    watch: {
        datas: {
            handler(){
                const value = this.datas.some(data => data.isChecked)
                console.log(value)
            },
            deep: true
        }
 
    },  
    mounted() {
        //Thêm sự kiện phím tắt
        document.addEventListener("keydown", this.keyShort);
        //Chọn phần tử đầu tiên
        this.selectTr(0)
    },

    beforeUnmount() {
        //Bỏ sự kiện phím tắt
        document.removeEventListener("keydown", this.keyShort);
    },
    methods: {
        /**
         * Thêm dữ liệu vào state vuex
         * @param {dữ liệu} i 
         * auther: NgQuan
         * created:05/2023
         */
        add(i) {
            this.$store.dispatch('addSelected', i)
            this.selected=i
        },
        /**
         * Phím tắt
         * @param {PHÍM} e
         * auther: NgQuan
         * created:05/2023 
         */
        keyShort(e) {
            //Nếu nhấn phím cùng với phím Ctrl
            if (e.ctrlKey && e.keyCode != this.$enum.KeyCode.Ctrl) {
                //Nhưng sự kiện của chrome
                if (e.keyCode == this.$enum.KeyCode.NumberOne) {
                    e.preventDefault();
                    this.$tnemtter.emit('addOnClick')
                }
                else if (e.keyCode == this.$enum.KeyCode.KeyE) {
                    e.preventDefault();
                    this.openEdit()
                }
            }
        },
        /**
         * Tìm phần tử theo id đã có
         * @param {id bản ghi} id 
         * @returns{vị trí của bản ghi}
         * auther: NgQuan 05/2023
         */
        searchItemId(id) {
            for (let j = 0; j < this.datas.length; j++) {
                if (this.datas[j][this.tablekey] == id) {
                    return j;
                }
            }
            return -1;
        },
        /**
        * Tìm phần tử theo class
        * @param {Class muốn tìm } classname 
        * @returns{vị trí của bản ghi}
        * auther: NgQuan 05/2023
        */
        searchItemClass(classname) {
            for (let j = 0; j < this.datas.length; j++) {
                if (this.$refs.table[j].classList.contains(classname)) {
                    return j
                }
            }
            return -1
        },
        /**
         * Mở form sửa cho phần tử đã chọn
         * auther:NgQuan 05/2023
         */
        openEdit() {
            let j = this.searchItemClass("table__tr--select")
            this.onClickDetail(this.datas[j][this.tablekey])
        },
        /**
         * Chọn phần tử tiếp theo trong bảng
         * auther:NgQuan
         * create:05/2023
         */
        selectTrNext() {
            let j = this.searchItemClass("table__tr--select")
            if (j + 1 < this.datas.length) {
                //Chọn phần tử tiếp theo và thoát
                this.selectTr(j + 1)
            }
            //(Là phần tử cuối)
            else {
                //Chọn phần tử đầu tiên
                this.selectTr(0)
            }
        },
        /**
         * Chọn phần tử trước đó trong bảng
         * auther:NgQuan
         * create:05/2023
         */
        selectTrPre() {
            let j = this.searchItemClass("table__tr--select")
            if (j == 0) {
                //Chọn phần tử cuối cùng
                this.selectTr(this.datas.length - 1)
            }
            else {
                //Chọn phần tử trước đó
                this.selectTr(j - 1)
            }
        },
        /**
         * Chọn một phần tử trong bảng
         * auther:NgQuan
         * @param {Vị trí của phần tử trong bảng} i 
         * create:05/2023
         */
        selectTr(i) {
            if (typeof i != 'number') {
                i = this.searchItemId(i)
            }
            for (let j = 0; j < this.datas.length; j++) {
                let table = this.$refs.table[j].classList
                //Tìm phần tử trong bảng
                if (i == j) {
                    //Nếu phần tử đó chưa được chọn
                    if (!table.contains("table__tr--select")) {
                        //Chọn phần tử đó
                        table.add("table__tr--select")
                    }
                }
                else {
                    //Nếu không phải phần tử được chọn
                    if (table.contains("table__tr--select")) {
                        //Bỏ chọn phần tử
                        table.remove("table__tr--select")
                    }

                }
            }
        },
       
        /**
        * Hàm hiện text function con của bảng
        * Auther: Nguyễn Quân
        * Created:05/2023 
        */
        showFunctionText(id) {
            let i = this.searchItemId(id)
            const functions = this.$refs.functions__text[i]
            //Nếu text function chưa hiện thì hiện ra
            if (functions.style.display == "none") {
                functions.style.display = "block"
            }
            //Nếu text function hiện rồi thì ẩn đi
            else {
                functions.style.display = "none"
            }
        },
        /**
        * Hàm hiện function của bảng
        * auther: Nguyễn Quân
        */
        showFunction(id) {
            let i = this.searchItemId(id)
            const functions = this.$refs.functions[i]
            functions.style.left = 'calc(100% + ' + this.scroll + 'px )'
            functions.style.display = "flex"
        },
        /**
        * Truyền hàm hiện dailog cảnh báo
        * auther: Nguyễn Quân
        */
        btnDelete(item) {
            this.$tnemtter.emit("showDailog","delete")
            this.$tnemtter.emit("delete",item)
        },
        /**
         * Hàm ẩn function của bảng
         * auther: Nguyễn Quân
         */
        hideFunction(id) {
            let i = this.searchItemId(id)
            const functionss = this.$refs.functions__text[i]
            const functions = this.$refs.functions[i]
            functions.style.display = "none"
            functionss.style.display = "none"
        },
        /**
         * Truyền dữ liệu lên from
         * Auther: Nguyễn Quân
         * Created:05/2023 
         */
        onClickDetail(id) {
            this.$emit("showPopup", id)
        },
        /**
         * Đổi mã bản ghi
         * @param {Mã bản ghi} id 
         * @param {trạng thái} status 
         */
        setStatusItem(id,status){
            this.$tnemtter.emit("setStatusItem",id,status)
        },
    }
}
</script>

<style>
.red {
    color: #ff0000;
}
</style>