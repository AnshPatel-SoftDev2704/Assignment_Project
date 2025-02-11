import axios from "axios";

const getAllSkill = async (token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Skill`,{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response;
    }
    catch(error)
    {
        return error;
    }
}

const saveSkill = async (token,data) => {
    try{
        const response = await axios.post(`http://localhost:5195/api/Skill`,data,{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response;
    }
    catch(error)
    {
        return error;
    }
}

const updateSkill = async (token,data) => {
    try{
        const response = await axios.put(`http://localhost:5195/api/Skill/${data.skill_id}`,data,{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response;
    }
    catch(error)
    {
        return error;
    }
}

const deleteSkill = async (token,skill_id) => {
    try{
        const response = await axios.delete(`http://localhost:5195/api/Skill/${skill_id}`,{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response;
    }
    catch(error)
    {
        return error;
    }
}

export {getAllSkill, saveSkill,updateSkill,deleteSkill};