import axios from "axios";

const fetchUserNotification = async (token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Notification_Users`,{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response;
    }
    catch(error)
    {
        return
    }
}

const fetchCandidateNotification = async (token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Notification_Users/GetAllCandidateNotification`,{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response;
    }
    catch(error)
    {
        return
    }
}

const updateUserNotification = async (token,id,data) => {
    console.log(data)
    try{
        const response = await axios.put(`http://localhost:5195/api/Notification_Users/${id}`,data,{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response;
    }
    catch(error)
    {
        return
    }
}

const updateCandiateNotification = async (token,id,data) => {
    try{
        const response = await axios.put(`http://localhost:5195/api/Notification_Users/UpdateCandidateNotification/${id}`,data,{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response;
    }
    catch(error)
    {
        return
    }
}

export {fetchCandidateNotification,fetchUserNotification,updateCandiateNotification,updateUserNotification}