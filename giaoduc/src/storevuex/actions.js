export default {
    addSelected ({commit}, newSelect) {
        commit('addSelected', newSelect)
    },
    addDelete ({commit}, newSelect) {
        commit('addDelete', newSelect)
    },
    clearSelected ({commit}) {
        commit('clearSelected')
    },
    doneEdit ({commit}, item) {
        commit('doneEdit', item)
    }
}