import { combineReducers } from "redux";
import loginReducer from "./reducer";
import userReducer from "./Users/reducer";
import roleReducer from "./Roles/reducer";

const rootReducer = combineReducers({
    Userdata : loginReducer,
    Users : userReducer,
    Roles : roleReducer,
})

export default rootReducer;