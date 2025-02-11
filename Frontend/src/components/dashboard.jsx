import { Button } from '@/components/ui/button';
import { useState } from 'react';
import { useSelector } from 'react-redux';
import ShowUser from './User/showUser';
import SaveUser from './User/saveUser';
import DisplayRole from './Role/showRole';
import CreateRole from './Role/addRole';
import DisplayUserRole from './UserRoles/showUserRoles';
import CreateUserRole from './UserRoles/addUserRoles';
import DisplaySkill from './Skill/showSkills';
import CreateSkill from './Skill/addSkills';
const Dashboard = () => {
    const [showUser, setShowUser] = useState(false);
    const [addUser,setAddUser] = useState(false);
    const [showRole,setShowRole] = useState(false);
    const [addRole,setAddRole] = useState(false)
    const [showUserRole,setShowUserRole] = useState(false)
    const [addUserRole,setAddUserRole] = useState(false)
    const [showSkill,setShowSkill] = useState(false)
    const [addSkill,setAddSkill] = useState(false)
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

    const handleAddRole = () => {
        setAddRole(prev => !prev)
}  

    const handleShowUserRole = () => {
        setShowUserRole(prev => !prev)
    }

    const handleAddUserRole = () => {
        setAddUserRole(prev => !prev)
    }

    const handleShowSkill = () => {
        setShowSkill(prev => !prev)
    }

    const handleAddSkill = () => {
        setAddSkill(prev => !prev)
    }
    return (
        <>
            <h1>Welcome {data[0].name}</h1>
            <Button onClick={handleShowUser}>Show Users</Button>
            <Button className = 'ml-2' onClick={handleAddUser}>Add User</Button>
            <Button className = 'ml-2' onClick={handleShowRole}>Show Roles</Button>
            <Button className = 'ml-2' onClick={handleAddRole}>Add Role</Button>
            <Button className = 'ml-2' onClick={handleShowUserRole}>Show UserRoles</Button>
            <Button className = 'ml-2' onClick={handleAddUserRole}>Add UserRoles</Button>
            <Button className = 'ml-2' onClick={handleShowSkill}>Show Skill</Button>
            <Button className = 'ml-2' onClick={handleAddSkill}>Add Skill</Button>
            {showUser && <ShowUser />}
            {addUser && <SaveUser/>}
            {showRole && <DisplayRole/>}
            {addRole && <CreateRole/>}
            {showUserRole && <DisplayUserRole/>}
            {addUserRole && <CreateUserRole/>}
            {showSkill && <DisplaySkill/>}
            {addSkill && <CreateSkill/>}
        </>
    );
};

export default Dashboard;
