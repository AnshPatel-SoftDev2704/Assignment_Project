import { GETROLE } from "./actionTypes";

const roleReducer = (state = [],action) => {
    switch(action.type)
    {
        case GETROLE : 
        return [...state,action.payload.role]
        default:
        return state;
    }
}

export default roleReducer;