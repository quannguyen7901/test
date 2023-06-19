import { createApp } from 'vue'
import App from './App.vue'
import router from './router/router'
import MisaButton from './components/MisaButton.vue'
import MisaTextBox from './components/MisaTextBox.vue'
import MisaIcon from './components/MisaIcon.vue'
import axios from 'axios'
import MisaTooltip from '@/components/MisaTooltip.vue'
import emitter from 'tiny-emitter/instance'
import MisaEnum from './js/base/enum'
import MisaCheckbox from './components/MisaCheckbox.vue'
import MisaResource from './js/base/resources'
import MisaCombobox from './components/MisaCombobox.vue'
import MisaListbox from './components/MisaListbox.vue'
import MisaRadioButton from './components/MisaRadioButton.vue'
import Vuex from 'vuex'
import BaseApi from './service/api'
import store from './storevuex/store'


const app = createApp(App);
app.use(router);
app.use(Vuex);
app.component("MisaButton",MisaButton);
app.component("MisaCombobox",MisaCombobox);
app.component("MisaListbox",MisaListbox);
app.component("MisaTextBox",MisaTextBox);
app.component("MisaIcon",MisaIcon);
app.component("MisaRadioButton",MisaRadioButton);
app.component("MisaTooltip",MisaTooltip);
app.component("MisaCheckbox",MisaCheckbox);
app.config.globalProperties.$axios=axios;
app.config.globalProperties.$enum=MisaEnum;
app.config.globalProperties.$store=store;
app.config.globalProperties.$baseapi=BaseApi;
app.config.globalProperties.$resource=MisaResource;
app.config.globalProperties.$tnemtter=emitter;

app.mount('#app');
