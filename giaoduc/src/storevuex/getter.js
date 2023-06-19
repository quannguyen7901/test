export default {
    getLength: (state) => {
        return state.selected.length 
      },
    Done: (state) => {
        return state.toDos.filter(m => m.completed == true).length
    },
    allTasks: (state) =>  {
        return state.toDos.length
    }, 
    count: (state) => {
        return state.count
    }
}