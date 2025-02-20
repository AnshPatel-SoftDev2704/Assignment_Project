import { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { getAllInterview, getAllInterviewer } from '@/services/Interview/api';
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table";
import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogFooter,
    DialogDescription,
  } from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Pencil, Trash2, Eye } from "lucide-react";
import UpdateInterview from './updateInterview';
import DeleteInterview from './deleteInterview';
import { toast } from 'react-toastify';
import { getAllRequiredSkill,getAllPreferredSkill } from '@/services/Jobs/api';

const DisplayInterview = () => {
    const dispatch = useDispatch();
    const data = useSelector((state) => state.Userdata);
    const [interviews,setInterviews] = useState([])
    const [interviewers,setInterviewers] = useState([])
    const [showEditDialog, setShowEditDialog] = useState(false);
    const [showDeleteDialog, setShowDeleteDialog] = useState(false);
    const [showDetailsDialog,setShowDetailsDialog] = useState(false)
    const [selectedInterview, setSelectedInterview] = useState(null);
    const [selectedInterviewer,setSelectedInterviewer] = useState([])
    const [requiredSkills,setRequiredSkills] = useState([])
    const [preferredSkills,setPreferredSkills] = useState([])
    useEffect(() => {
        const fetchData = async () => {
            try{
            const response = await getAllInterview(data[0].token);
            const result = await getAllInterviewer(data[0].token)
            const requiredSkill = await getAllRequiredSkill(data[0].token)
            const preferredSkill = await getAllPreferredSkill(data[0].token)
            setRequiredSkills(requiredSkill.data)
            setPreferredSkills(preferredSkill.data)

            if(response.status === 403 || result.status === 403)
                throw new Error("You are Not Allowed to Perform this Action")

            setInterviews(response.data)
            setInterviewers(result.data)
            }
            catch(error)
            {
                toast.error(error.message || "Failed to Fetch Details");
            }
        };
        fetchData();
    }, [showEditDialog,setShowEditDialog,showDeleteDialog,setShowDeleteDialog]);

    const handleEdit = (interview) => {
        setSelectedInterview(interview);
        setShowEditDialog(true);
    };

    const handleDelete = (interview) => {
        setSelectedInterview(interview);
        setShowDeleteDialog(true);
    }; 

    const handleShowDetails = (interview) => {
        setSelectedInterview(interview)
        const response = interviewers.filter(interviewer => interviewer.interview_id === interview.interview_id)
        setSelectedInterviewer(response)
        console.log(interview)
        console.log(response)
        setShowDetailsDialog(true)
    }


    return (
        <div className="rounded-md border">
            <Table>
                <TableHeader>
                    <TableRow>
                        <TableHead className="text-center">Round</TableHead>
                        <TableHead className="text-center">Type</TableHead>
                        <TableHead className="text-center">Schedule</TableHead>
                        <TableHead className="text-center">Status</TableHead>
                        <TableHead className="text-center">Meeting Link</TableHead>
                        <TableHead className="text-center">Notes</TableHead>
                            <TableHead className="text-center">Actions</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {interviews && interviews.map(interview => (
                        <TableRow key={interview.interview_id}>
                            <TableCell className="text-center">{interview.number_of_round}</TableCell>
                            <TableCell className="text-center">{interview.interview_Type.interview_Type_name}</TableCell>
                            <TableCell className="text-center">
                                {new Date(interview.scheduled_at).toLocaleString()}
                            </TableCell>
                            <TableCell className="text-center">{interview.interview_Status.interview_Status_name}</TableCell>
                            <TableCell className="text-center">
                                <a href={interview.interview_Meeting_Link} target="_blank" rel="noopener noreferrer" className="text-blue-500 hover:underline">
                                    Join Meeting
                                </a>
                            </TableCell>
                            <TableCell className="text-center">{interview.special_Notes}</TableCell>
                            <TableCell className="text-center">
                                <Button
                                    variant="ghost"
                                    size="icon"
                                    className="h-8 w-8 mr-2"
                                    onClick={() => handleShowDetails(interview)}
                                >
                                    <Eye className="h-4 w-4" />
                                </Button>
                                <Button
                                    variant="ghost"
                                    size="icon"
                                    onClick={() => handleEdit(interview)}
                                >
                                    <Pencil className="h-4 w-4" />
                                </Button>
                                <Button
                                    variant="ghost"
                                    size="icon"
                                    className="text-red-500"
                                    onClick={() => handleDelete(interview)}
                                >
                                    <Trash2 className="h-4 w-4" />
                                </Button>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
            
            <Dialog open={showDetailsDialog} onOpenChange={setShowDetailsDialog}>
                <DialogContent className="overflow-auto max-w-3xl">
                    <DialogHeader>
                        <DialogTitle>Interview Details</DialogTitle>
                    </DialogHeader>
                    {selectedInterview && selectedInterviewer && (
                        <div className='space-y-6 overflow-y-auto max-h-96'>
                            <div className='grid grid-cols-2 gap-6'>
                                <div className='space-y-4'>
                                    <div>
                                        <h3 className='text-lg font-semibold mb-3'>Interview Information</h3>
                                        <div className='space-y-2'>
                                            <p><span className='font-medium'>Interview Type: </span>{selectedInterview.interview_Type.interview_Type_name}</p>
                                            <p><span className='font-medium'>Interview Status: </span>{selectedInterview?.interview_Status?.interview_Status_name}</p>
                                            <p><span className='font-medium'>Round Number: </span>{selectedInterview?.number_of_round}</p>
                                            <p><span className='font-medium'>Scheduled at: </span>{new Date(selectedInterview?.scheduled_at).toLocaleDateString()}</p>
                                            <p><span className='font-medium'>Special Notes: </span>{selectedInterview?.special_Notes}</p>
                                        </div>
                                    </div>
                                    <br/>
                                    <div>
                                        <h3 className="font-semibold mb-2">Interviewer:</h3>
                                        <ul className="list-disc list-inside space-y-2">
                                            {interviewers.filter(interviewer => interviewer.interview_id === selectedInterview.interview_id).map(interviewer => (<li>{interviewer.user.name}</li>))}
                                        </ul>
                                    </div>
                                    <br/>
                                    <div>
                                        <h3 className='text-lg font-semibold mb-3'>Job Information</h3>
                                        <div className='space-y-2'>
                                            <p><span className='font-medium'>Title: </span>{selectedInterview?.candidate_Application_Status.job?.job_title}</p>
                                            <p><span className='font-medium'>Description: </span>{selectedInterview?.candidate_Application_Status.job?.job_description}</p>
                                            <p><span className='font-medium'>Id: </span>{selectedInterview?.candidate_Application_Status.job?.job_id}</p>
                                            <p><span className='font-medium'>Closed Reason: </span>{selectedInterview?.candidate_Application_Status.job?.job_Closed_reason}</p>
                                            <p><span className='font-medium'>Selected Candidate Id: </span>{selectedInterview?.candidate_Application_Status.job?.job_Selected_Candidate_id}</p>
                                        </div>
                                    </div>
                                    <div className='space-y-4'>
                                        <div>
                                            <h3 className="font-semibold mb-2">Required Skills:</h3>
                                            <ul className="list-disc list-inside space-y-2">
                                                {requiredSkills?.map(skill => skill.job_id === selectedInterview?.candidate_Application_Status.job?.job_id && (<li>{skill.skill?.skill_name}</li>))}
                                            </ul>
                                        </div>
                                        <div>
                                            <h3 className="font-semibold mb-2">Preferred Skills:</h3>
                                            <ul className="list-disc list-inside space-y-2">
                                                {preferredSkills?.map(skill => skill.job_id === selectedInterview?.candidate_Application_Status.job?.job_id && (<li>{skill.skill?.skill_name}</li>))}
                                            </ul>
                                        </div>
                                    </div>
                                    <br/>
                                    <div className='space-y-4'>
                                        <h3 className='text-lg font-semibold mb-3'>Candidate Information:</h3>
                                        <p><span className='font-medium'>Candidate Name: </span>{selectedInterview?.candidate_Application_Status.candidate_Details?.candidate_name}</p>
                                        <p><span className='font-medium'>Candidate Total Work Experience: </span>{selectedInterview?.candidate_Application_Status.candidate_Details?.candidate_Total_Work_Experience}</p>
                                        <p><span className='font-medium'>Candidate Email: </span>{selectedInterview?.candidate_Application_Status.candidate_Details?.candidate_email}</p>
                                        <p><span className='font-medium'>Candidate Contact: </span>{selectedInterview?.candidate_Application_Status.candidate_Details?.phoneNo}</p>
                                        <p><span className='font-medium'>Candidate Application Date: </span>{new Date(selectedInterview?.candidate_Application_Status?.applied_Date).toLocaleDateString()}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    )}
                </DialogContent>
            </Dialog>


            {showEditDialog && (
                <UpdateInterview 
                    showEditDialog={showEditDialog}
                    setShowEditDialog={setShowEditDialog}
                    editInterview={selectedInterview}
                />
            )}

            {showDeleteDialog && (
                <DeleteInterview 
                    showDeleteDialog={showDeleteDialog}
                    setShowDeleteDialog={setShowDeleteDialog}
                    deleteInterviews={selectedInterview}
                />
            )}
        </div>
    );
};

export default DisplayInterview;