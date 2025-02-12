import { combineReducers } from "redux";
import loginReducer from "./reducer";
import userReducer from "./Users/reducer";
import roleReducer from "./Roles/reducer";
import userRolesReducer from "./UserRoles/reducer";
import skillReducer from "./Skills/reducer";
import jobReducer from "./Jobs/reducer";

const rootReducer = combineReducers({
    Userdata : loginReducer,
    Users : userReducer,
    Roles : roleReducer,
    UserRoles : userRolesReducer,
    Skills : skillReducer,
    Jobs : jobReducer
})

export default rootReducer;