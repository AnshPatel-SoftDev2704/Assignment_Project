import axios from "axios";

const getCandidateDetailById = async (token,id) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Candidate_Details/${id}`,{
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

const getAllCandidateSkill = async (token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Candidate_Details/GetAllCandidateSkills`,{
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

const updateCandidate = async (token, data, user_id, file) => {
    let formData = new FormData();
    data.Role_id = 6;

    Object.keys(data).forEach(key => {
        formData.append(key, data[key]);
    });

    if (file) {
        formData.append('file', file);
    }

    try {
        const response = await axios.put(
            `http://localhost:5195/api/Candidate_Details/${user_id}`,
            formData,
            {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            }
        );
        return response;
    } catch (error) {
        console.error("Error updating candidate:", error);
        return error;
    }
};

const getAllCandidates = async (token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Candidate_Details`,{
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

const saveCandidateSkill = async (token, data,user_id) => {
    try {
        if (!token || !data) {
            throw new Error('Missing required parameters: token, data, or candidate_id');
        }

        const payload = {
            Candidate_id: user_id,
            Skill_id: data.skill_id,
            Total_Skill_Work_experience: data.experience
        };

        const response = await axios.post(
            'http://localhost:5195/api/Candidate_Details/SaveCandidateSkill',
            payload,
            {
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            }
        );

        return response.data;
    } catch (error) {
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
};

const getAllApplications = async (token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Candidate_Details/GetAllCandidateApplicationStatus`,{
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

const getAllApplicationStatuses = async (token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Candidate_Details/GetAllApplicationStatus`,{
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

const saveCandidateDetails = async(token,data) => {
    try{
        const response = await axios.post(`http://localhost:5195/api/Candidate_Details`,data,{
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

const deleteCandidate = async (token,candidate_id) => {
    try{
        const response = await axios.delete(`http://localhost:5195/api/Candidate_Details/${candidate_id}`,{
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

const updateApplicationStatus = async (token,id,data) => {
    try{
        const response = await axios.put(`http://localhost:5195/api/Candidate_Details/UpdateCandidateApplicationStatus/${id}`,data,{
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

const saveCandidateApplicationStatus = async (token, candidate_id, job_id, application_Status_id, todayDate) => {
    try {
        const response = await axios.post(
            `http://localhost:5195/api/Candidate_Details/SaveCandidateApplicationStatus`,
            { candidate_id, job_id, application_Status_id, Applied_Date: todayDate },
            {
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            }
        );
        return response;
    } catch (error) {
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
};

const getFile = async (token, filepath) => {
    try {
        const response = await axios.get(`http://localhost:5195/api/Candidate_Details/GetFile`, {
            params: { filepath },
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


export {getFile,saveCandidateApplicationStatus,updateApplicationStatus, deleteCandidate, getCandidateDetailById,getAllCandidateSkill,updateCandidate,saveCandidateSkill, getAllApplications, getAllApplicationStatuses, saveCandidateDetails, getAllCandidates};