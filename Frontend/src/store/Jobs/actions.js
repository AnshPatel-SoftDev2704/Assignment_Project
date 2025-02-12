import { GETJOBS } from "./actionTypes";

export const getJobs = (jobs) => ({
    type : GETJOBS,
    payload : {jobs}
})