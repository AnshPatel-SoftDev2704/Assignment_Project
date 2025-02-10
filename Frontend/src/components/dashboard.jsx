import { Button } from '@/components/ui/button';
import { useState } from 'react';
import { useSelector } from 'react-redux';
import ShowUser from './showUser';
import SaveUser from './saveUser';
const Dashboard = () => {
    const [showUser, setShowUser] = useState(false);
    const [addUser,setAddUser] = useState(false)
    const data = useSelector((state) => state.Userdata);
    const handleShowUser = () => {
        setShowUser(prev => !prev);
    };
    const handleAddUser = () => {
        setAddUser(prev => !prev);
    };
    return (
        <>
            <h1>Welcome {data[0].name}</h1>
            <Button onClick={handleShowUser}>Show Users</Button>
            <Button onClick={handleAddUser}>Add User</Button>
            {showUser && <ShowUser />}
            {addUser && <SaveUser/>}
        </>
    );
};

export default Dashboard;
