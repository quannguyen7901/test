<template>
    <MisaLoading v-if="isloading"></MisaLoading>
    <MisaPopupVue v-if="popup" :title="title" :twobutton="twobutton" :save="save">
        <div class="popup__content">
            <div class="popup__input">
                <div class="popup__item">
                    <MisaTextBox :focus="focus" v-model="emulation.EmulatitleName" haslabel="true" hasspan="true"
                        :label="name">
                    </MisaTextBox>
                </div>
                <div class="popup__item popup__item--50">
                    <div class="popup__item--left">
                        <MisaTextBox v-model="emulation.EmulatitleCode" haslabel="true" hasspan="true" :label="code">
                        </MisaTextBox>
                    </div>
                    <div class="popup__item--right">
                        <label>{{ object }} </label><span>*</span>
                        <div class="popup__checkbox" @click="checkObject" ref="object">
                            <MisaCheckbox v-model="incheck" :text="individual"></MisaCheckbox>
                            <MisaCheckbox v-model="cocheck" :text="collective"></MisaCheckbox>
                        </div>
                    </div>
                </div>
                <div class="popup__item popup__item--50">
                    <div class="popup__item--left">
                        <MisaCombobox v-if="combo" :combodata="rewardlevels" v-model:combo-value="emulation.RewardLevelId"
                            :nametable="table" hasspan="true" :label="level">
                        </MisaCombobox>
                    </div>
                    <div class="popup__item--right">
                        <label>{{ type }} </label><span>*</span>
                        <div class="popup__checkbox" @click="checkType" ref="type">
                            <MisaCheckbox v-model="frcheck" :text="frequent"></MisaCheckbox>
                            <MisaCheckbox v-model="bacheck" :text="batched"></MisaCheckbox>
                        </div>
                    </div>
                </div>
                <label>{{ note }}</label><br />
                <textarea name="" id="" placeholder="Nhập ghi chú"></textarea>
                <div v-if="title == this.$resource.VN.TextFrom.Update">
                    <label>Trạng thái</label><br />
                    <div class="popup__radio" @click="checkStatus">
                        <MisaRadioButton v-model:status="use" v-model:status-value="status" class="popup__input--radio"
                            name="trangthai" label="Sử dụng" :val="this.$enum.StatusCode.Use"></MisaRadioButton>
                        <MisaRadioButton v-model:status="unuse" v-model:status-value="status" class="popup__input--radio"
                            name="trangthai" label="Ngưng sử dụng" :val="this.$enum.StatusCode.UnUse"></MisaRadioButton>
                    </div>
                </div>
            </div>
        </div>
    </MisaPopupVue>
</template>

<script>
import MisaPopupVue from '@/components/MisaPopup.vue/MisaPopup.vue'
import MisaLoading from '@/components/MisaLoading.vue';
export default {
    name: "EmulationTitleFrom",
    props: {
        id: {
            type: String,
            default: ""
        },
    },
    data() {
        return {
            baseapi: this.$baseapi,
            emulation: {},
            title: this.$resource.VN.TextFrom.Add,
            table: "RewardLevel",
            focus: false,
            name: this.$resource.VN.TextLabel.Name,
            object: this.$resource.VN.TextLabel.Object,
            type: this.$resource.VN.TextLabel.Type,
            level: this.$resource.VN.TextLabel.Level,
            code: this.$resource.VN.TextLabel.Code,
            note: this.$resource.VN.TextLabel.Note,
            batched: this.$resource.VN.TextCheckbox.Batched,
            frequent: this.$resource.VN.TextCheckbox.Frequent,
            collective: this.$resource.VN.TextCheckbox.Collective,
            individual: this.$resource.VN.TextCheckbox.Individual,
            bacheck: false,
            frcheck: false,
            cocheck: false,
            incheck: false,
            combo: false,
            popup: false,
            twobutton: false,
            save: "",
            use: "",
            unuse: "",
            status: ""
        }
    },
    components: {
        MisaPopupVue, MisaLoading
    },
    created() {
        //Hứng sự kiện ở bên ngoài
        this.$tnemtter.on("confirmDailog", this.btnCloseClick)
        this.$tnemtter.on("btnSaveOnClick", this.saveClick)
        //Api lấy danh sách cấp khen thưởng
        this.$axios.get(`${this.baseapi}RewardLevels`)
            //Api chạy OK
            .then(async(res) => {
                this.rewardlevels = await res.data
                this.combo = true
            })
            //Api chạy ko OK
            .catch(async (res)=>{
                console.log(await res)
            })
        /**
        * Tùy chọn mở from
        * Auther: NgQuan
        * Created:05/2023 
        */
        if (this.id != "") {
            //Nếu truyền dữ liệu thì đổi tên from thành sửa
            this.title = this.$resource.VN.TextFrom.Update
            //Api lấy bản ghi
            this.$axios.get(`${this.baseapi}Emulatitles/${this.id}`)
                //Api chạy OK
                .then(async (res) => {
                    this.emulation = await res.data
                    //Sử lý đối tượng khen thưởng
                    this.emulatittleObject(this.emulation.EmulatitleObject)
                    //Sử lý loại khen thưởng
                    this.emulatittleType(this.emulation.EmulatitleType)
                    //Sử lý trạng thái khen thưởng
                    this.emulatittleStatus(this.emulation.Status)
                    this.save = this.$resource.VN.TextButton.SaveChange
                    this.twobutton = false
                    this.popup = true
                    this.focus = true
                })
                //Api chạy ko OK
                .catch(async (res) => {
                    //Hiện thông báo
                    this.$tnemtter.emit("Noti", this.$resource.VN.Notification.Error,await res.response.data.Message)
                    this.$router.push(`/emulatitle`)
                })
        }
        else {
            this.emulation.EmulatitleObject=this.$enum.EmulatitleCheckbox.Individual
            this.emulation.EmulatitleType=this.$enum.EmulatitleCheckbox.Frequent
            this.frcheck = true
            this.incheck = true
            this.focus = true
            this.popup = true
            this.twobutton = true
            this.save = this.$resource.VN.TextButton.Save
        }
    },
    beforeUnmount() {
        //Bỏ hứng sự kiện
        this.$tnemtter.off("confirmDailog")
        this.$tnemtter.off("btnSaveOnClick")
    },
    methods: {
        /**
         * Cập nhật status bản ghi
         * auther: NgQuan
         * created: 05/2023
         */
        checkStatus() {
            this.emulation.Status = this.status
        },
        /**
         * Đóng form
         * @param {*} e
         * auther: NgQuan
         * created: 05/2023 
         */
        btnCloseClick(e) {
            if (e == "ok") {
                this.$router.push(`/emulatitle`)
            }
        },
        /**
         * Hàm xử lý dữ liệu trạng thái khen thưởng
         * @param {Dữ liệu của đối tượng khen thưởng} i 
         * auther: NgQuan
         * created: 05/2023
         */
        emulatittleStatus(i) {
            //Nếu trạng thái là sử dụng
            if (i == this.$enum.StatusCode.Use) {
                this.use = true
                this.unuse = false
            }
            //Nếu trạng thái là ngưng sử dụng
            else {
                this.use = false
                this.unuse = true
            }
            this.status = i
        },
        /**
         * Hàm xử lý dữ liệu đối tượng khen thưởng
         * @param {Dữ liệu của đối tượng khen thưởng} i 
         * auther: NgQuan
         * created: 05/2023
         */
        emulatittleObject(i) {
            //Nếu đối tượng khen thưởng là cá nhân
            if (i == this.$enum.EmulatitleCheckbox.Individual) {
                this.incheck = true
            }
            //Nếu đối tượng khen thưởng là tập thể
            else if (i == this.$enum.EmulatitleCheckbox.Collective) {
                this.cocheck = true
            }
            //Nếu đối tượng khen thưởng là cả hai
            else {
                this.incheck = true
                this.cocheck = true
            }
        },
        /**
        * Hàm xử lý dữ liệu loại khen thưởng
        * @param {Dữ liệu của loại khen thưởng} i 
        * auther: NgQuan
        * created: 05/2023
        */
        emulatittleType(i) {
            //Nếu loại khen thưởng là thường xuyên
            if (i == this.$enum.EmulatitleCheckbox.Frequent) {
                this.frcheck = true
            }
            //Nếu loại khen thưởng là theo đợt
            else if (i == this.$enum.EmulatitleCheckbox.Batched) {
                this.bacheck = true
            }
            //Nếu loại khen thưởng là cả hai
            else {
                this.frcheck = true
                this.bacheck = true
            }
        },
        /**
      * Hàm check dữ liệu đối tượng khen thưởng
      * Auther: NgQuan
      * Created:05/2023 
      */
        checkObject() {
            const object = this.$refs.object
            // kiểm tra input có lỗi không
            if (this.cocheck == false && this.incheck == false) {
                const para = document.createElement("div");
                //Gán class thông báo
                para.classList.add("input__text--error");
                //Gán nội dung thông báo
                const node = document.createTextNode(
                    this.object + this.$resource.VN.ErrorText.TextNull);
                para.appendChild(node);
                //Gán thông báo vào dưới input
                object.parentElement.appendChild(para);
            }
            else {
                //Gán lại dữ liệu theo điều kiện
                if (this.cocheck == true && this.incheck == false) {
                    this.emulation.EmulatitleObject = this.$enum.EmulatitleCheckbox.Collective
                }
                else if (this.incheck == true && this.cocheck == false) {
                    this.emulation.EmulatitleObject = this.$enum.EmulatitleCheckbox.Individual
                }
                else {
                    this.emulation.EmulatitleObject = 3
                }
                //Bỏ thông báo lỗi dưới input
                if (object.nextElementSibling) {
                    object.parentElement.removeChild(object.nextElementSibling);
                }
            }
        },
        /**
        * Hàm check dữ liệu loại khen thưởng
        * Auther: NgQuan
        * Created:05/2023 
        */
        checkType() {
            const object = this.$refs.type
            // kiểm tra input có lỗi không
            if (this.frcheck == false && this.bacheck == false) {
                const para = document.createElement("div");
                //Gán class thông báo
                para.classList.add("input__text--error");
                //Gán nội dung thông báo
                const node = document.createTextNode(
                    this.object + this.$resource.VN.ErrorText.TextNull);
                para.appendChild(node);
                //Gán thông báo vào dưới input
                object.parentElement.appendChild(para);
            }
            else {
                //Gán lại dữ liệu theo điều kiện
                if (this.frcheck == true && this.bacheck == false) {
                    this.emulation.EmulatitleType = this.$enum.EmulatitleCheckbox.Frequent
                }
                else if (this.bacheck == true && this.frcheck == false) {
                    this.emulation.EmulatitleType = this.$enum.EmulatitleCheckbox.Batched
                }
                else {
                    this.emulation.EmulatitleType = 3
                }
                //Bỏ thông báo lỗi dưới input
                if (object.nextElementSibling) {
                    object.parentElement.removeChild(object.nextElementSibling);
                }
            }
        },
        /**
        * Hàm ấn nút lưu
        * Auther: NgQuan
        * Created:05/2023 
        */
        saveClick() {
            console.log(this.emulation)
            //Nếu form là sửa
            if (this.id != "") {
                //Api lưu bản ghi
                this.$axios.put(`${this.baseapi}Emulatitles/${this.id}`, this.emulation)
                    //Api chạy OK
                    .then(async (res) => {
                        this.emulation = await res.data
                        this.$router.push(`/emulatitle`)
                        this.$tnemtter.emit("reload")
                        this.popup = false
                        //Hiện thông báo
                        this.$tnemtter.emit("Noti", this.$resource.VN.Notification.Sucsess, this.$resource.VN.SucsessText.SucsessUpdate)
                    })
                    //Api chạy ko OK
                    .catch(async(res) => {
                        //Hiện thông báo
                        this.$tnemtter.emit("Noti", this.$resource.VN.Notification.Error,await res.response.data.Message)
                    })
            }
            //Nếu from là thêm
            else {
                //Api chạy OK
                this.$axios.post(`${this.baseapi}Emulatitles`, this.emulation)
                    .then(async(res) => {
                        this.emulation = await res.data
                        this.$router.push(`/emulatitle`)
                        this.$tnemtter.emit("reload")
                        this.popup = false
                        //Hiện thông báo
                        this.$tnemtter.emit("Noti", this.$resource.VN.Notification.Sucsess, this.$resource.VN.SucsessText.SucsessInsert)
                    })
                    .catch(async(res) => {
                        //Hiện thông báo
                        this.$tnemtter.emit("Noti", this.$resource.VN.Notification.Error,await res.response.data.Message)
                    })
            }
        },
    },
    watch: {
        //Quan sát tên danh hiệu
        'emulation.EmulatitleName': function (e) {
            let code = e.split(' ').map(word => word.charAt(0))
            //Nếu nhập tên danh hiệu
            if (code != null && this.title == this.$resource.VN.TextFrom.Add) {
                //Cập nhật mã bản ghi
                this.emulation.EmulatitleCode = code.join('').toUpperCase()
            }
            //Nếu tên danh hiệu là trống
            else if (this.title == this.$resource.VN.TextFrom.Add) {
                //Clear mã bản ghi
                this.emulation.EmulatitleCode = null
            }
        }
    },
}
</script>

<style></style>