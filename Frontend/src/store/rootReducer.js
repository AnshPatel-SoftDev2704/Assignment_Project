import { combineReducers } from "redux";
import loginReducer from "./reducer";
import userReducer from "./Users/reducer";

const rootReducer = combineReducers({
    Userdata : loginReducer,
    Users : userReducer,
})

export default rootReducer;