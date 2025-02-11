import { Button } from '@/components/ui/button';
import { useState } from 'react';
import { useSelector } from 'react-redux';
import ShowUser from './User/showUser';
import SaveUser from './User/saveUser';
import DisplayRole from './Role/showRole';
import CreateRole from './Role/addRole';
const Dashboard = () => {
    const [showUser, setShowUser] = useState(false);
    const [addUser,setAddUser] = useState(false);
    const [showRole,setShowRole] = useState(false);
    const [addRole,setAddRole] = useState(false)
    const data = useSelector((state) => state.Userdata);
    const handleShowUser = () => {
        setShowUser(prev => !prev);
    };
    const handleAddUser = () => {
        setAddUser(prev => !prev);
    };

    const handleShowRole = () => {
        setShowRole(prev => !prev)
    }

    const handleAddRole = () => [
        setAddRole(prev => !prev)
    ]
    return (
        <>
            <h1>Welcome {data[0].name}</h1>
            <Button onClick={handleShowUser}>Show Users</Button>
            <Button className = 'ml-2' onClick={handleAddUser}>Add User</Button>
            <Button className = 'ml-2' onClick={handleShowRole}>Show Roles</Button>
            <Button className = 'ml-2' onClick={handleAddRole}>Add Role</Button>
            {showUser && <ShowUser />}
            {addUser && <SaveUser/>}
            {showRole && <DisplayRole/>}
            {addRole && <CreateRole/>}
        </>
    );
};

export default Dashboard;
