import { GETJOBS } from "./actionTypes";

const jobReducer = (state = [], action) => {
    switch(action.type)
    {
        case GETJOBS : 
        return [...state,action.payload.jobs]
        default:
            return state
    }
}
export default jobReducer