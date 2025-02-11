import { GETSKILL } from "./actionTypes";

export const getSkill = (skill) => ({
    type : GETSKILL,
    payload : {skill}
})