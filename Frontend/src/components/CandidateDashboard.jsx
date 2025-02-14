import { Button } from '@/components/ui/button';
import { useState } from 'react';
import { useSelector } from 'react-redux';
import CandidateProfile from './Candidate/editCandidateProfile';
import JobListings from './Candidate/showJobsToCandidate';
const CandidateDashboard = () => {
    const [editCandidate, setEditCandidate] = useState(false);
    const [showOpenJobs,setShowOpenJobs] = useState(false)
    const data = useSelector((state) => state.Userdata);
    const handleEditCandidate = () => {
        setEditCandidate(prev => !prev);
    };

    const handleShowOpenJobs = () =>{
        setShowOpenJobs(prev => !prev)
    }
    return (
        <>
            <h1>Welcome {data[0].name}</h1>
            <Button onClick={handleEditCandidate}>Edit Profile</Button>
            <Button onClick={handleShowOpenJobs}>Show Open Jobs</Button>
            {editCandidate && <CandidateProfile/>}
            {showOpenJobs && <JobListings/>}
        </>
    );
};

export default CandidateDashboard;
