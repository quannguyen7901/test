import {createRouter, createWebHistory} from 'vue-router'
import EmulationTitleHome from '@/views/emulationtitle/EmulationTitleHome.vue'
import OverViewHome from '@/views/overview/OverViewHome.vue'
import EmulationTitleForm from '@/views/emulationtitle/EmulationTitleForm.vue'


const routers=[
    {
        path:"/",component:EmulationTitleHome,name:"MainRouter"
    },
    {
        path:"/emulatitle",component:EmulationTitleHome,name:"EmulationTitleHomeRouter",
        children:[
            {
                path:":id",components:{default:EmulationTitleHome,EmulationTitleForm:EmulationTitleForm},name:"EmulationTitleDetalRouter",props:true
            },
            {
                path:"created",components:{default:EmulationTitleHome,EmulationTitleForm:EmulationTitleForm},name:"EmulationTitleCreateRouter"
            }
        ]
    },
    {
        path:"/overview",component:OverViewHome,name:"OverViewHomeRouter"
    },
]

const router=createRouter({
    history:createWebHistory(),
    routes:routers
})

export default router;