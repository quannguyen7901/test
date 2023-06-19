
  <template>
    <div class="popup__cover">
        <div class="popup">
            <MisaPopupHeader :title="title" :hasquestion="hasquestion"></MisaPopupHeader>
            <slot></slot>
            <MisaPopupButton :save="save" :twobutton="twobutton"></MisaPopupButton>
        </div>
    </div>
</template>

<script>
import MisaPopupHeader from '@/components/MisaPopup.vue/MisaPopupHeader.vue';
import MisaPopupButton from '@/components/MisaPopup.vue/MisaPopupButton.vue';

export default {
    name: "EmulationTitleFrom",
    props: {
        //Text title của popup
        title:{
            type:String,
            default:""
        },
        //Ẩn hiện icon question
        hasquestion:{
            type:Boolean,
            default:true
        },
        //Hiện 2 hoặc 3 nút trên popup
        twobutton:{
            type:Boolean,
            default:false
        },
        //Text trên button chức năng
        save:{
            type:String,
            default:""
        }
    },
    components: {
        MisaPopupButton, MisaPopupHeader
    },
}
</script>

<style>

</style>