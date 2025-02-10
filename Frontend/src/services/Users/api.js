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

export { getAllUser, saveUser };
