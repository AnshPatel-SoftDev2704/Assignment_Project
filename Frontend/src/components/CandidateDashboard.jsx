import { Button } from '@/components/ui/button';
import { useState } from 'react';
import { useSelector } from 'react-redux';
import CandidateProfile from './Candidate/editCandidateProfile';
import CandidateApplicationStatus from './Candidate/showCandidateApplicationStatus';
import JobListings from './Candidate/showJobsToCandidate';
const CandidateDashboard = () => {
    const [editCandidate, setEditCandidate] = useState(false);
    const data = useSelector((state) => state.Userdata);
    const handleEditCandidate = () => {
        setEditCandidate(prev => !prev);
    };
    return (
        <>
            <h1>Welcome {data[0].name}</h1>
            <Button onClick={handleEditCandidate}>Edit Profile</Button>
            {editCandidate && <CandidateProfile/>}
            <JobListings/>
        </>
    );
};

export default CandidateDashboard;
