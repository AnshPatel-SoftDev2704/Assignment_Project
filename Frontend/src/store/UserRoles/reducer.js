import { GETUSERROLES } from "./actionTypes";

const userRolesReducer = (state = [],action) => {
    switch(action.type)
    {
        case GETUSERROLES :
            return [...state,action.payload.userRoles]
        default:
            return state
    }
}

export default userRolesReducer;