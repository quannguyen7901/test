export default {
    /**
     * Lưu một giá trị mới vào state
     * @param {State lưu trữ} state 
     * @param {Giá trị muốn lưu} newSelect 
     * auther: NgQuan
     * created: 05/2023
     */
    addSelected (state, newSelect) {
        if (newSelect.length && !state.selected.includes(newSelect)) {
            state.selected.push(newSelect)
        }
        else{
            state.selected=state.selected.filter(item => item !== newSelect);
        }
    },
     /**
     * Lưu một giá trị mới vào state
     * @param {State lưu trữ} state 
     * @param {Giá trị muốn lưu} newSelect 
     * auther: NgQuan
     * created: 05/2023
     */
       addDelete (state, newSelect) {
        if (newSelect.length && !state.delete.includes(newSelect)) {
            state.delete.push(newSelect)
        }
        else{
            state.delete=state.delete.filter(item => item !== newSelect);
        }
    },
    /**
     * Xóa toàn bộ dữ liệu state lưu trữ
     * @param {State lưu trữ} state 
     */
    clearSelected(state){
        state.selected=[]
    }
}