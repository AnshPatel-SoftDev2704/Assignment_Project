import { GETSKILL } from "./actionTypes";

const skillReducer = (state = [], action) => {
    switch(action.type)
    {
        case GETSKILL : 
        return [...state,action.payload.skill]
        default: 
        return state;
    }
}

export default skillReducer;