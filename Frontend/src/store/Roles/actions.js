import { GETROLE } from "./actionTypes";

export const getRole = (role) => ({
    type : GETROLE,
    payload : {role}
})