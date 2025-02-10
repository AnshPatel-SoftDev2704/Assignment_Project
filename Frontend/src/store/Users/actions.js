import { GETUSER } from "./actionsTypes";

export const getUser = (users) => ({
    type : GETUSER,
    payload:{users}
})