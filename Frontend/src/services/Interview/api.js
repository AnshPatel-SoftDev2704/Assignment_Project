import axios from "axios";

const getAllInterviewType = async (token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Interview/GetAllInterviewType`,{
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

const getAllInterviewStatus = async (token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Interview/GetAllInterviewStatus`,{
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

const saveInterviewer = async (token,interview_id,user_id) => {
    try{
        const response = await axios.post(`http://localhost:5195/api/Interview/SaveInterviewer`,{interview_id,user_id},{
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

const saveInterview = async(token,data) => {
    try{
        const response = await axios.post(`http://localhost:5195/api/Interview`,data,{
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

const updateInterview = async(token,id,data) => {
    try{
        const response = await axios.put(`http://localhost:5195/api/Interview/${id}`,data,{
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

const getAllInterview = async (token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Interview`,{
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

const getAllInterviewer = async (token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Interview/GetAllInterviewer`,{
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

const deleteInterview = async (token,id) => {
    try{
        const response = await axios.delete(`http://localhost:5195/api/Interview/${id}`,{
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

const saveFeedback = async (token,data) => {
    try{
        const response = await axios.post(`http://localhost:5195/api/Interview/SaveFeedback`,data,{
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

const getAllFeedback = async (token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Interview/GetAllFeedback`,{
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        })
        return response
    }
    catch(error)
    {
        if (error.response) {
            console.error('Server Error:', {
                status: error.response.status,
                data: error.response.data
            });
            throw new Error(`Server error: ${error.response.data}`);
        } else if (error.request) {
            console.error('Network Error:', error.request);
            throw new Error('Network error: Unable to reach the server');
        } else {
            console.error('Request Error:', error.message);
            throw error;
        }
    }
}

const updateFeedback = async (token,id,data) => {
    try{
        const response = await axios.put(`http://localhost:5195/api/Interview/UpdateFeedback/${id}`,data,{
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

const deleteFeedback = async (token,id) => {
    try{
        const response = await axios.delete(`http://localhost:5195/api/Interview/DeleteFeedback/${id}`,{
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
export {deleteFeedback,updateFeedback,getAllFeedback,saveFeedback,deleteInterview,getAllInterviewer,getAllInterview,saveInterview,saveInterviewer,getAllInterviewStatus,getAllInterviewType,updateInterview,}