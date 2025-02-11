import axios from "axios";

const getAllRole = async (token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Role`,{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response;
    }
    catch(error){
        return error;
    }
}

const saveRole = async (token,data) => {
    try{
        const response = await axios.post(`http://localhost:5195/api/Role`,data,{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response;
    }
    catch(error){
        return error;
    }
}

const updateRole = async(token,data) => {
    try{
    const response = await axios.put(`http://localhost:5195/api/Role/${data.role_id}`,data,{
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

const deleteRole = async (token,role_id) => {
    try{
        const response = await axios.delete(`http://localhost:5195/api/Role/${role_id}`,{
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

export {getAllRole,saveRole, updateRole, deleteRole};