import axios from "axios";

const getAllUser = async (token) => {
    try {
        const response = await axios.get(`http://localhost:5195/api/User/getAllUser`, {
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });
        return response;
    } catch (error) {
        return error;
    }
};

const saveUser = async (token, data) => {
    try {
        const response = await axios.post(`http://localhost:5195/api/User/saveUser`, data, {
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });
        return response;
    } catch (error) {
        return error;
    }
};

const updateUser = async(token,data) => {
    try{
        const response = await axios.put(`http://localhost:5195/api/User/updateUser/${data.User_id}`,data,{
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

const deleteUser = async(token,user_id) =>
{
    try{
        const response = await axios.delete(`http://localhost:5195/api/User/deleteUser/${user_id}`,{
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
export { getAllUser, saveUser,updateUser, deleteUser };
