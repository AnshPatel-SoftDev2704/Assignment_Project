import { GETUSER } from "./actionsTypes"; 

const userReducer = (state = [],action) => {
    switch(action.type)
    {
        case GETUSER : 
        return [...state,action.payload.users];
        default:
            return state;
    }
}

export default userReducer;