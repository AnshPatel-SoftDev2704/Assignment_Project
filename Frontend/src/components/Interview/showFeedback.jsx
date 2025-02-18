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
import { Button } from "@/components/ui/button";
import { Pencil, Trash2 } from "lucide-react";
import UpdateFeedback from './updateFeedback';
import DeleteFeedback from './deleteFeedback';
import { toast } from 'react-toastify';

const DisplayFeedback = () => {
    const data = useSelector((state) => state.Userdata);
    const [feedbacks,setFeedbacks] = useState([])
    const [showEditDialog, setShowEditDialog] = useState(false);
    const [showDeleteDialog, setShowDeleteDialog] = useState(false);
    const [selectedFeedback, setSelectedFeedback] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try{
            const response = await getAllFeedback(data[0].token);
            if(response.status === 403)
                throw new Error("You are not allowed to Perform this Action")
            else if(response.status !== 200)
                throw new Error()
            setFeedbacks(response.data)
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

    return (
        <div className="rounded-md border">
            <Table>
                <TableHeader>
                    <TableRow>
                        <TableHead className="text-center">Interview ID</TableHead>
                        <TableHead className="text-center">User ID</TableHead>
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
                            <TableCell className="text-center">{feedback.interview_id}</TableCell>
                            <TableCell className="text-center">{feedback.user_id}</TableCell>
                            <TableCell className="text-center">{feedback.technology}</TableCell>
                            <TableCell className="text-center">{feedback.rating}</TableCell>
                            <TableCell className="text-center">{feedback.comments}</TableCell>
                            <TableCell className="text-center">
                                {new Date(feedback.submitted_date).toLocaleString()}
                            </TableCell>
                            <TableCell className="text-center">
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