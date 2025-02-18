import axios from 'axios';

const auth = async (username, password) => {
    try {
        const loginResponse = await axios.post(
            `http://localhost:5195/api/Auth/login/${username}`,
            password,
            {
                headers: {
                    'Content-Type': 'application/json'
                }
            }
        );
        const token = loginResponse.data;
        try {
            const userResponse = await axios.get(
                `http://localhost:5195/api/User/${username}`,
                {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    }
                }
            );
            
            if (userResponse.data) {
                return {
                    user_id: userResponse.data.user_id,
                    name: userResponse.data.name,
                    email: userResponse.data.email,
                    token: token,
                };
            }
        } catch (userError) {
            console.log('User not found, checking candidate...');
        }

        const candidateResponse = await axios.get(
            `http://localhost:5195/api/Candidate_Details/getCandidateByName/${username}`,
            {
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            }
        );

        return {
            user_id: candidateResponse.data.candidate_id,
            name: candidateResponse.data.candidate_name,
            email: candidateResponse.data.candidate_email,
            token: token,
            role:"Candidate"
        };

    } catch (error) {
        console.error('Auth error:', error);
        throw error;
    }
};

export default auth;