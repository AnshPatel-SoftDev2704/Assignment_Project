import axios from 'axios'
const auth = async (username,password) =>
{
    let response = {};
    try{
    response = await axios.post(`http://localhost:5195/api/Auth/login/${username}`,password,{
        headers: {
            'Content-Type': 'application/json'
          }
    })}
    catch(error)
    {
        return error
    };
    try{
    const user = await axios.get(`http://localhost:5195/api/User/${username}`,{
        headers: {
            'Authorization': `Bearer ${response.data}`,
            'Content-Type': 'application/json'
          }
    });
    user.data.token = response.data;
    return user;
}
catch(error)
{
    return error
}
}

export default auth;