import { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { getAllFeedback } from '@/services/Interview/api';
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
import UpdateFeedback from './updateFeedback';
import DeleteFeedback from './deleteFeedback';
import { toast } from 'react-toastify';

const DisplayFeedback = () => {
    const data = useSelector((state) => state.Userdata);
    const [feedbacks,setFeedbacks] = useState([])
    const [showEditDialog, setShowEditDialog] = useState(false);
    const [showDeleteDialog, setShowDeleteDialog] = useState(false);
    const [selectedFeedback, setSelectedFeedback] = useState(null);
    const [showDetailsDialog,setShowDetailsDialog] = useState(false)

    useEffect(() => {
        const fetchData = async () => {
            try{
            const response = await getAllFeedback(data[0].token);
            if(response.status === 403)
                throw new Error("You are not allowed to Perform this Action")
            else if(response.status !== 200)
                throw new Error()
            setFeedbacks(response.data)
            console.log(response.data)
            }
            catch(error)
            {
                toast.error(error.message || "Failed to Fetch Details");
            }
        };
        fetchData();
    }, [showEditDialog,setShowDeleteDialog,showDeleteDialog,setShowEditDialog]);

    const handleEdit = (feedback) => {
        setSelectedFeedback(feedback);
        setShowEditDialog(true);
    };

    const handleDelete = (feedback) => {
        setSelectedFeedback(feedback);
        setShowDeleteDialog(true);
    };

    const handleDetailsDialog = (feedback) => {
        setSelectedFeedback(feedback)
        setShowDetailsDialog(true);
    }

    return (
        <div className="rounded-md border">
            <Table>
                <TableHeader>
                    <TableRow>
                        <TableHead className="text-center">Candidate Name</TableHead>
                        <TableHead className="text-center">Interviewer Name</TableHead>
                        <TableHead className="text-center">Interview Type</TableHead>
                        <TableHead className="text-center">Round</TableHead>
                        <TableHead className="text-center">Technology</TableHead>
                        <TableHead className="text-center">Rating</TableHead>
                        <TableHead className="text-center">Comments</TableHead>
                        <TableHead className="text-center">Submitted Date</TableHead>
                        <TableHead className="text-center">Actions</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {feedbacks && feedbacks.map(feedback => (
                        <TableRow key={feedback.feedback_id}>
                            <TableCell className="text-center">{feedback.interview.candidate_Application_Status.candidate_Details.candidate_name}</TableCell>
                            <TableCell className="text-center">{feedback.user.name}</TableCell>
                            <TableCell className="text-center">{feedback.interview.interview_Type.interview_Type_name}</TableCell>
                            <TableCell className="text-center">{feedback.interview.number_of_round}</TableCell>
                            <TableCell className="text-center">{feedback.skill.skill_name}</TableCell>
                            <TableCell className="text-center">{feedback.rating}</TableCell>
                            <TableCell className="text-center">{feedback.comments}</TableCell>
                            <TableCell className="text-center">
                                {new Date(feedback.submitted_date).toLocaleString()}
                            </TableCell>
                            <TableCell className="text-center">
                            <Button variant="ghost" size="icon" onClick={() => handleDetailsDialog(feedback)}>
                                    <Eye className="h-4 w-4" />
                                </Button>
                                <Button variant="ghost" size="icon" onClick={() => handleEdit(feedback)}>
                                    <Pencil className="h-4 w-4" />
                                </Button>
                                <Button variant="ghost" size="icon" className="text-red-500" onClick={() => handleDelete(feedback)}>
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
                        <DialogTitle>Feedback Details</DialogTitle>
                    </DialogHeader>
                    {selectedFeedback && (
                        <div className='space-y-6 overflow-y-auto max-h-96'>
                            <div className='grid grid-cols-2 gap-6'>
                                <div className='space-y-4'>
                                    <div>
                                        <h3 className='text-lg font-semibold mb-3'>Feedback Information</h3>
                                            <p><span className='font-medium'>Techonology: </span>{selectedFeedback.skill.skill_name}</p>
                                            <p><span className='font-medium'>Interview Type: </span>{selectedFeedback.interview.interview_Type.interview_Type_name}</p>
                                            <p><span className='font-medium'>Round: </span>{selectedFeedback.interview.number_of_round}</p>
                                            <p><span className='font-medium'>Rating: </span>{selectedFeedback?.rating}</p>
                                            <p><span className='font-medium'>Comments: </span>{selectedFeedback?.Comments}</p>
                                            <p><span className='font-medium'>Submitted Date: </span>{new Date(selectedFeedback?.submitted_date).toLocaleDateString()}</p>
                                    </div>
                                    <div>
                                        <h3 className="font-semibold mb-2">Interviewer:</h3>
                                        <p><span className='font-medium'>Name: </span>{selectedFeedback.user.name}</p>
                                        <p><span className='font-medium'>Email: </span>{selectedFeedback.user.email}</p>
                                        <p><span className='font-medium'>Contact: </span>{selectedFeedback.user.contact}</p>
                                    </div>
                                    <div>
                                        <h3 className='text-lg font-semibold mb-3'>Job Information</h3>
                                            <p><span className='font-medium'>Title: </span>{selectedFeedback?.interview.candidate_Application_Status.job?.job_title}</p>
                                            <p><span className='font-medium'>Description: </span>{selectedFeedback?.interview.candidate_Application_Status.job?.job_description}</p>
                                            <p><span className='font-medium'>Id: </span>{selectedFeedback?.interview.candidate_Application_Status.job?.job_id}</p>
                                            <p><span className='font-medium'>Closed Reason: </span>{selectedFeedback?.interview.candidate_Application_Status.job?.job_Closed_reason}</p>
                                            <p><span className='font-medium'>Selected Candidate Id: </span>{selectedFeedback?.interview.candidate_Application_Status.job?.job_Selected_Candidate_id}</p>
                                        </div>
                                    </div>
                                    <br/>
                                    <div>
                                        <h3 className='text-lg font-semibold mb-3'>Candidate Information:</h3>
                                        <p><span className='font-medium'>Candidate Name: </span>{selectedFeedback?.interview.candidate_Application_Status.candidate_Details?.candidate_name}</p>
                                        <p><span className='font-medium'>Candidate Total Work Experience: </span>{selectedFeedback?.interview.candidate_Application_Status.candidate_Details?.candidate_Total_Work_Experience}</p>
                                        <p><span className='font-medium'>Candidate Email: </span>{selectedFeedback?.interview.candidate_Application_Status.candidate_Details?.candidate_email}</p>
                                        <p><span className='font-medium'>Candidate Contact: </span>{selectedFeedback?.interview.candidate_Application_Status.candidate_Details?.phoneNo}</p>
                                        <p><span className='font-medium'>Candidate Application Date: </span>{new Date(selectedFeedback.interview.candidate_Application_Status?.applied_Date).toLocaleDateString()}</p>
                                    </div>
                                </div>
                            </div>
                    )}
                </DialogContent>
            </Dialog>

            {showEditDialog && (
                <UpdateFeedback 
                    showEditDialog={showEditDialog}
                    setShowEditDialog={setShowEditDialog}
                    editFeedback={selectedFeedback}
                />
            )}

            {showDeleteDialog && (
                <DeleteFeedback 
                    showDeleteDialog={showDeleteDialog}
                    setShowDeleteDialog={setShowDeleteDialog}
                    deleteFeedbacks={selectedFeedback}
                />
            )}
        </div>
    );
};

export default DisplayFeedback;