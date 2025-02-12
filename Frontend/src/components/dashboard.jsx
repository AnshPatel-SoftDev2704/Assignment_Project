import { Button } from '@/components/ui/button';
import { useState } from 'react';
import { useSelector } from 'react-redux';
import ShowUser from './User/showUser';
import SaveUser from './User/saveUser';
import DisplayUserRole from './User/showUserRoles';
import DisplaySkill from './Skill/showSkills';
import CreateSkill from './Skill/addSkills';
import ShowJobs from './Jobs/showJobs';
import CreateJob from './Jobs/saveJobs';
const Dashboard = () => {
    const [showUser, setShowUser] = useState(false);
    const [addUser,setAddUser] = useState(false);
    const [showUserRole,setShowUserRole] = useState(false)
    const [showSkill,setShowSkill] = useState(false)
    const [addSkill,setAddSkill] = useState(false)
    const [showJobs,setShowJobs] = useState(false)
    const [addJobs,setAddJobs] = useState(false)
    const data = useSelector((state) => state.Userdata);
    const handleShowUser = () => {
        setShowUser(prev => !prev);
    };
    const handleAddUser = () => {
        setAddUser(prev => !prev);
    };

    const handleShowUserRole = () => {
        setShowUserRole(prev => !prev)
    }

    const handleShowSkill = () => {
        setShowSkill(prev => !prev)
    }

    const handleAddSkill = () => {
        setAddSkill(prev => !prev)
    }

    const handleShowJobs = () => {
        setShowJobs(prev => !prev)
    }
    
    const handleAddJobs = () => {
        setAddJobs(prev => !prev)
    }
    return (
        <>
            <h1>Welcome {data[0].name}</h1>
            <Button onClick={handleShowUser}>Show Users</Button>
            <Button className = 'ml-2' onClick={handleAddUser}>Add User</Button>
            <Button className = 'ml-2' onClick={handleShowUserRole}>Show UserRoles</Button>
            <Button className = 'ml-2' onClick={handleShowSkill}>Show Skill</Button>
            <Button className = 'ml-2' onClick={handleShowJobs}>Show Jobs</Button>
            <Button className = 'ml-2' onClick={handleAddJobs}>Add Jobs</Button>
            {showUser && <ShowUser />}
            {addUser && <SaveUser/>}
            {showUserRole && <DisplayUserRole/>}
            {showSkill && <DisplaySkill/>}
            {addSkill && <CreateSkill/>}
            {showJobs && <ShowJobs/>}
            {addJobs && <CreateJob/>}
        </>
    );
};

export default Dashboard;
