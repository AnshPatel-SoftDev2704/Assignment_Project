import { GETUSERROLES } from "./actionTypes";

export const getUserRole = (userRoles) => ({
    type:GETUSERROLES,
    payload:{userRoles}
})