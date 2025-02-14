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
import CreateCandidate from './Candidate/addCandidate';
import ShowCandidates from './Candidate/showCandidates';
import CandidateApplicationStatus from './Candidate/showCandidateApplicationStatus';
import CreateInterview from './Interview/addInterview';
import DisplayInterview from './Interview/showInterview';
import AddFeedback from './Interview/saveFeedback';
import DisplayFeedback from './Interview/showFeedback';
import ShowDocuments from './Documents/showSubmittedDocument';
const Dashboard = () => {
    const [showUser, setShowUser] = useState(false);
    const [addUser,setAddUser] = useState(false);
    const [showUserRole,setShowUserRole] = useState(false)
    const [showSkill,setShowSkill] = useState(false)
    const [addSkill,setAddSkill] = useState(false)
    const [showJobs,setShowJobs] = useState(false)
    const [addJobs,setAddJobs] = useState(false)
    const [addCandidate,setAddCandidate] = useState(false)
    const [showCandidates,setShowCandidates] = useState(false)
    const [showCandidateApplicationStatus,setShowCandidateApplicationStatus] = useState(false)
    const [addInterview,setAddInterview] = useState(false)
    const [showInterview,setShowInterview] = useState(false)
    const [addFeedback,setAddFeedback] = useState(false)
    const [showFeedback,setShowFeedback] = useState(false)
    const [showDocument,setShowDocument] = useState(false)
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

    const handleAddCandidate = () => {
        setAddCandidate(prev => !prev)
    }
    
    const handleShowCandidates = () => {
        setShowCandidates(prev => !prev)
    }

    const handleShowCandidateApplicationStatus = () => {
        setShowCandidateApplicationStatus(prev => !prev)
    }

    const handleAddInterview = () => {
        setAddInterview(prev => !prev)
    }

    const handleShowInterview = () => {
        setShowInterview(prev => !prev)
    }

    const handleAddFeedback = () =>{
        setAddFeedback(prev => !prev)
    }

    const handleShowFeedback = () => {
        setShowFeedback(prev => !prev)
    }

    const handleShowDocument = () => {
        setShowDocument(prev => !prev)
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
            <Button className = 'ml-2' onClick={handleAddCandidate}>Add Candidate</Button>
            <Button className = 'ml-2' onClick={handleShowCandidates}>Show Candidate</Button>
            <Button className = 'ml-2' onClick={handleShowCandidateApplicationStatus}>Show Candidate Application Status</Button>
            <Button className = 'mt-2' onClick={handleAddInterview}>Add Interview</Button>
            <Button className = 'mt-2 ml-2' onClick={handleShowInterview}>Show Interview</Button>
            <Button className = 'mt-2 ml-2' onClick={handleAddFeedback}>Add Feedback</Button>
            <Button className = 'mt-2 ml-2' onClick={handleShowFeedback}>Show Feedback</Button>
            <Button className = 'mt-2 ml-2' onClick={handleShowDocument}>Show Documents</Button>
            {showUser && <ShowUser />}
            {addUser && <SaveUser/>}
            {showUserRole && <DisplayUserRole/>}
            {showSkill && <DisplaySkill/>}
            {addSkill && <CreateSkill/>}
            {showJobs && <ShowJobs/>}
            {addJobs && <CreateJob/>}
            {addCandidate && <CreateCandidate/>}
            {showCandidates && <ShowCandidates/>}
            {showCandidateApplicationStatus && <CandidateApplicationStatus/>}
            {addInterview && <CreateInterview/>}
            {showInterview && <DisplayInterview/>} 
            {addFeedback && <AddFeedback/>}
            {showFeedback && <DisplayFeedback/>}
            {showDocument && <ShowDocuments/>}
        </>
    );
};

export default Dashboard;
