import App from "@/App";
import axios from "axios";

const getDocumentType = async (token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Document_Submitted/GetAllDocumentType`,{
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

const saveDocument = async (token,data,file) => {
    let formData = new FormData();

    Object.keys(data).forEach(key => {
        formData.append(key, data[key]);
    });

    if (file) {
        formData.append('file', file);
    }

    try{
        const response = await axios.post(`http://localhost:5195/api/Document_Submitted`,formData,{
            headers: {
                'Authorization': `Bearer ${token}`,
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

const getAllDocumentSubmitted = async (token) => {
    try{
        const response = await axios.get(`http://localhost:5195/api/Document_Submitted`,{
            headers: {
                'Authorization': `Bearer ${token}`,
            }
        })
        return response
    }
    catch(error)
    {
        return error
    }
}

const updateSubmittedDocument = async (token,id,data) => {
    console.log(data)
    try{
        const response = await axios.put(`http://localhost:5195/api/Document_Submitted/UpdateStatus/${id}/${data.Approved_by}`,data.Status,{
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

const deleteSubmittedDocument = async (token,id) => {
    try{
        const response = await axios.delete(`http://localhost:5195/api/Document_Submitted/${id}`,{
            headers: {
                'Authorization': `Bearer ${token}`,
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

const getFile = async (token, filepath) => {
    try {
        const response = await axios.get(`http://localhost:5195/api/Document_Submitted/GetFile`, {
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

export {getFile,deleteSubmittedDocument,updateSubmittedDocument,getAllDocumentSubmitted,saveDocument,getDocumentType}