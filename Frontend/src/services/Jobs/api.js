import axios from "axios";

const getAllJobs = async (token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Jobs`,{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response
    }
    catch(error)
    {
        return error
    }
}

const saveJobs = async (token,data) => {
    try{
        const response = await axios.post(`http://localhost:5195/api/Jobs`,data,{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response
    }
    catch(error)
    {
        return error;
    }
}

const saveRequiredSkill = async (token,Job_id,Skill_id) => {
    try{
        console.log(Job_id)
        console.log(Skill_id)
        const response = await axios.post(`http://localhost:5195/api/Jobs/SaveRequiredJobSkill`,{Job_id,Skill_id},{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response
    }
    catch(error)
    {
        return error;
    }
}

const savePreferredSkill = async (token,Job_id,Skill_id) => {
    try{
        const response = await axios.post(`http://localhost:5195/api/Jobs/SavePreferredJobSkill`,{Job_id,Skill_id},{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response
    }
    catch(error)
    {
        return error;
    }
}

const getAllJobStatus = async(token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Jobs/getAllJobStatus`,{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response
    }
    catch(error)
    {
        return error;
    }
}

const getAllRequiredSkill = async (token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Jobs/GetAllRequiredJobSkill`,{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response
    }
    catch(error)
    {
        return error;
    }
}

const getAllPreferredSkill = async (token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Jobs/GetAllPreferredJobSkill`,{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response
    }
    catch(error)
    {
        return error;
    }
}

const updateJob = async (token,data) => {
    try{
        const response = await axios.put(`http://localhost:5195/api/Jobs/${data.Job_id}`,data,{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response
    }
    catch(error)
    {
        return error;
    }
}

const deleteJob = async (token,Job_id) => {
    try{
        const response = await axios.delete(`http://localhost:5195/api/Jobs/${Job_id}`,{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response
    }
    catch(error)
    {
        return error;
    }
}

export {getAllJobs, saveJobs, savePreferredSkill, saveRequiredSkill, getAllJobStatus, getAllPreferredSkill, getAllRequiredSkill, updateJob, deleteJob}