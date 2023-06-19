import Vuex from 'vuex'
import mutations from './mutations'
import actions from './actions'
import getter from './getter'

export default new Vuex.Store({
    state: {  
        selected: [],
        delete:[]
    },
    mutations: mutations,
    getters: getter,
    actions: actions
  })