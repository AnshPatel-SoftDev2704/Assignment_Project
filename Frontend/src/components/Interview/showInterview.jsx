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
    useEffect(() => {
        const fetchData = async () => {
            try{
            const response = await getAllInterview(data[0].token);
            const result = await getAllInterviewer(data[0].token)

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
                        <TableHead className="text-center">Interviewers</TableHead>
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
                                {interviewers.filter(interviewer => interviewer.interview_id === interview.interview_id).map(interviewer => interviewer.user.name).join(', ')}
                            </TableCell>
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
            
            {/* <Dialog open={showDetailsDialog} onOpenChange={setShowDetailsDialog}>
                <DialogContent className="max-w-2xl">
                <DialogHeader>
                    <DialogTitle>User Details</DialogTitle>
                </DialogHeader>
                {selectedInterview && selectedInterviewer && (
                    <div className='space-y-6'>
                        <div className='grid grid-cols-2 gap-6'>
                            <div className='space-y-4'>
                                <div>
                                    <h3 className='text-lg font-semibold mb-3'>Interview Information</h3>
                                    <div className='space-y-2'>
                                        <p><span className='font-medium'>Name: </span>{selectedUser.name}</p>
                                        <p><span className='font-medium'>Email: </span>{selectedUser.email}</p>
                                        <p><span className='font-medium'>Contact: </span>{selectedUser.contact}</p>
                                        <p><span className='font-medium'>Id: </span>{selectedUser.user_id}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                )}
                </DialogContent>
            </Dialog> */}

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