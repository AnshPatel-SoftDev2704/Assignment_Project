import axios from "axios";

const getAllUserRoles = async (token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/UserRoles`,{
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

const saveUserRole = async (token,data) => {
    try{
        const response = await axios.post(`http://localhost:5195/api/UserRoles`,data,{
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

const updateUserRole = async (token,data) => {
    try{
        console.log(data)
        const response = await axios.put(`http://localhost:5195/api/UserRoles/${data.userRoleId}`,data,{
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

const deleteUserRole = async (token, userRoleId) => {
    try{
        const response = await axios.delete(`http://localhost:5195/api/UserRoles/${userRoleId}`,{
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

export {getAllUserRoles, saveUserRole, updateUserRole, deleteUserRole};