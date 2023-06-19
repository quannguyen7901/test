<template>
    <div class="table__cover" @scroll="scrollFunction" ref="table">
        <MisaLoading v-if="isloading"></MisaLoading>
        <table>
            <MisaTableHead :datas="tabletitle"></MisaTableHead>
            <MisaTableBody v-if="hasdata" tablekey="EmulatitleId" :colum="colum" @showPopup="showPopup" :datas="emulations"
                :scroll="numscroll">
            </MisaTableBody>
        </table>
    </div>
    <div class="content__footer">
        <div class="content__footer--left">
            {{ $resource.VN.TextNormal.Count }} <span>{{ format1(num) }}</span> {{ $resource.VN.TextNormal.Record }}
        </div>
        <div class="content__footer--right">
            {{ $resource.VN.TextNormal.CountRecord }}
            <MisaListbox class="numberpage" v-model="pagesize" :datas="numberpage"></MisaListbox>
            <div><span> {{ start }} - {{ end }}</span> {{ $resource.VN.TextNormal.Record }} </div>
            <div class="icon icon--24" ref="pre" @click="prePage"><i class="fa-light fa-angle-left"></i></div>
            <div class="icon icon--24" ref="next" @click="nextPage"><i class="fa-light fa-angle-right"></i></div>
        </div>
    </div>
</template>

<script>
import MisaTableHead from '@/components/MisaTable.vue/MisaTableHead.vue';
import MisaTableBody from '@/components/MisaTable.vue/MisaTableBody.vue';
import MisaLoading from '@/components/MisaLoading.vue';

export default {
    name: "EmulationTitleTable",
    data() {
        return {
            numscroll: 0,
            isloading: false,
            tabletitle: this.$resource.VN.TextTable.Emulation,
            colum: this.$resource.VN.TextColum.Emulation,
            emulations: [],
            pageindex: 1,
            pagesize: 20,
            baseapi: this.$baseapi,
            hasdata: false,
            numberpage: [10, 20, 50, 100],
            start: 1,
            end: 0,
            emulationdelete:null,
        }
    },
    components: {
        MisaTableBody, MisaTableHead, MisaLoading
    },
    created() {
        //Hứng các sự kiện từ bên ngoài
        this.$tnemtter.on("confirmDailog", this.btnDelete)
        this.$tnemtter.on("delete", this.itemDelete)
        this.$tnemtter.on("search", this.search)
        this.$tnemtter.on("setStatusItem", this.setStatusItem)
        this.$tnemtter.on("reload", this.reLoadData)
        this.loadData()
        //Api lấy tổng số bản ghi
        this.$axios.get(`${this.baseapi}Emulatitles/Counts`)
            //Api chạy OK
            .then(async (res) => {
                this.total = await res.data
                this.num = this.total
            })
            //Api chạy không OK
            .catch(async (res) => {
                console.log(res)
            })
    },
    mounted() {
        //Ẩn nút trở về trang trước
        this.$refs.pre.classList.add("disable__div")
        //Set số bản ghi
        if (this.num < this.pagesize) {
            this.$refs.next.classList.add("disable__div")
        }
    },
    beforeUnmount() {
        //Bỏ hứng các sự kiện bên ngoài
        this.$tnemtter.off("delete")
        this.$tnemtter.off("confirmDailog")
        this.$tnemtter.off("search")
        this.$tnemtter.off("reload")
        this.$tnemtter.off("setStatusItem")
    },
    methods: {
        itemDelete(item){
            this.emulationdelete=item
        },
        btnDelete(item){
            let id=this.emulationdelete
            if(item=="ok" && id!=""){
                this.$axios.delete(`${this.baseapi}Emulatitles/${id}`)
                //Api chạy OK
                .then(async (res) => {
                    console.log(res)
                    this.reLoadData()
                })
                //Api chạy không OK
                .catch(async (res) => {
                    console.log(res)
                })
            }
        },
        /**
         * Load lại trang
         * auther: NgQuan
         * created: 05/2023
         */
        reLoadData() {
            //Api lấy tổng số bản ghi
            this.$axios.get(`${this.baseapi}Emulatitles/Counts`)
                //Api chạy OK
                .then(async (res) => {
                    this.total = await res.data
                    this.num = this.total
                    this.loadData()
                })
                //Api chạy không OK
                .catch(async (res) => {
                    console.log(res)
                })
        },
        /**
         * Xang trang tiếp theo
         * auther: NgQuan
         * created:05/2023
         */
        nextPage() {
            //Nếu số trang chưa max
            if (this.end != this.num) {
                //Số trang +1
                this.pageindex += 1
                //Nếu số trang sau khi cộng là max
                if ((this.pageindex) * this.pagesize > this.num) {
                    //Cập nhật số bản ghi
                    this.start = ((this.pageindex - 1) * this.pagesize) + 1
                    this.end = this.num
                    //Ẩn nút xang trang tiếp theo
                    this.$refs.next.classList.add("disable__div")
                }
                //Nếu số trang sau khi cộng chưa max
                else {
                    //Cập nhật số bản ghi
                    this.start = ((this.pageindex - 1) * this.pagesize) + 1
                    this.end = this.pageindex * this.pagesize
                }
                //Hiện nút về trang trước đó
                this.$refs.pre.classList.remove("disable__div")
                //Load dữ liệu của trang
                this.loadData()
            }
        },
        /**
         * Trở về trang trước đó
         * auther: NgQuan
         * created: 05/2023
         */
        prePage() {
            //Nêu số trang chưa min
            if (this.end != this.pagesize) {
                //Số trang -1
                this.pageindex -= 1
                //Nếu số trang sau khi trừ là min
                if ((this.pageindex) * this.pagesize == this.pagesize) {
                    //Cập nhật số bản ghi
                    this.start = ((this.pageindex - 1) * this.pagesize) + 1
                    this.end = (this.pageindex) * this.pagesize
                    //Ẩn nút về trang trước đó
                    this.$refs.pre.classList.add("disable__div")
                }
                //Nếu số trang sau khi trừ chưa min
                else {
                    //Cập nhật số bản ghi
                    this.start = ((this.pageindex - 1) * this.pagesize) + 1
                    this.end = this.pageindex * this.pagesize
                }
                //Hiện nút xang trang tiếp
                this.$refs.next.classList.remove("disable__div")
                //Load dữ liệu của trang
                this.loadData()
            }
        },
        /**
         * đổi trạng thái bản ghi
         * @param {id bản ghi} id 
         * @param {trạng thái bản ghi muốn đổi} status
         * auther: NgQuan
         * created: 05/2023 
         */
        setStatusItem(id, status) {
            //Api sửa trạng thái
            this.$axios.put(`${this.baseapi}Emulatitles/${id}/Status?status=${status}`)
                //Nếu Api chạy OK
                .then(() => {
                    //Load dữ liệu trang
                    this.loadData()
                    //Hiện thông báo
                    this.$tnemtter.emit("Noti", this.$resource.VN.Notification.Sucsess, this.$resource.VN.SucsessText.SucsessInsert)
                })
                //Nếu Api chạy ko OK
                .catch((res) => {
                    //Load dữ liệu trang
                    this.loadData()
                    //Hiện thông báo
                    this.$tnemtter.emit("Noti", this.$resource.VN.Notification.Error, res.response.data.Message)
                })
        },
        /**
         * Format số nguyên
         * @param {một số nguyên} n 
         * auther: NgQuan
         * created: 05/2023
         */
        format1(n) {
            //Nếu biến truyền vào là kiểu số
            if (typeof (n) == 'number') {
                //format biến truyền vào
                return n.toFixed(0).replace(/./g, function (c, i, a) {
                    return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c
                })
            }
            //Nếu biến truyền vào là kiểu khác số
            else {
                //trả về -1
                return -1
            }
        },
        /**
        * Load data
        * Auther: Nguyễn Quân
        * Created:05/2023 
        */
        loadData() {
            this.isloading = true
            this.hasdata = false
            //Api lấy bản ghi
            this.$axios.get(`${this.baseapi}Emulatitles?pageIndex=${this.pageindex}&pageSize=${this.pagesize}`)
                //Api chạy OK
                .then(async (res) => {
                    this.emulations = await res.data
                    this.isloading = false
                    this.hasdata = true
                })
                //Api chạy ko OK
                .catch(async (res) => {
                    console.log(await res)
                    this.$tnemtter.emit("Noti", this.$resource.VN.Notification.Error, res.response.data.Message)
                })
        },
        /**
         * Tìm kiếm bản ghi
         * @param {Ký tự tìm kiếm} i
         * auther: NgQuan
         * created: 05/2023 
         */
        search(i) {
            this.isloading = true
            this.hasdata = false
            //Api tìm kiếm bản ghi
            this.$axios.get(`${this.baseapi}Emulatitles?pageIndex=1&pageSize=${this.pagesize}&search=${i}`)
                //Api chạy OK
                .then(async (res) => {
                    this.emulations = await res.data
                    this.isloading = false
                    this.hasdata = true
                    this.num = res.data.length
                    if (this.num < this.pagesize) {
                        this.end = this.num
                    }
                    else {
                        this.end = this.pagesize
                        this.num = this.total
                    }
                })
                //Api chạy ko OK
                .catch(async (res) => {
                    console.log(await res)
                })
        },
        /**
        * Dịch chuyển function của bảng
        * Auther: Nguyễn Quân
        * Created:05/2023 
        */
        scrollFunction() {
            this.numscroll = this.$refs.table.scrollLeft
        },
        /**
        * Hiện popup
        * Auther: Nguyễn Quân
        * Created:05/2023 
        */
        showPopup(id) {
            this.$router.push(`/emulatitle/${id}`)
        }
    },
    watch: {
        //Quan sát sự thay đổi của pagesize
        pagesize(newvalue) {
            //Ẩn nút trở về trang trước
            this.$refs.pre.classList.add("disable__div")
            //reset số trang
            this.pageindex = 1
            //Load dữ liệu của trang
            this.loadData()
            this.start = 1
            //Nếu pagesize lớn hơn tổng số bản ghi
            if (Number(newvalue) > this.num) {
                //Cập nhật số bản ghi
                this.end = this.num
                //Ẩn nút xang trang tiếp
                this.$refs.next.classList.add("disable__div")
            }
            //Nếu pagesize nhỏ hơn tổng số bản ghi
            else {
                //Cập nhật số bản ghi
                this.end = this.format1(Number(newvalue))
                //Hiện nút xang trang tiếp
                this.$refs.next.classList.remove("disable__div")
            }
        }
    }
}
</script>

<style>
.disable__div {
    color: #cfcfcf;
    cursor: not-allowed;
}
</style>