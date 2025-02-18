import React from 'react';
import {
    AlertDialog,
    AlertDialogAction,
    AlertDialogCancel,
    AlertDialogContent,
    AlertDialogDescription,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogTitle,
} from "@/components/ui/alert-dialog";
import { useSelector } from 'react-redux';
import { deleteCandidate } from '@/services/Candidate/api';
import { toast } from 'react-toastify';

const DeleteCandidate = ({ showDeleteDialog, setShowDeleteDialog, deleteCandidateData, onDelete }) => {
    const data = useSelector((state) => state.Userdata);

    const handleDelete = async () => {
        try {
            if (!deleteCandidateData?.candidate_id) {
                toast.error("Invalid candidate data", {
                    position: "top-right",
                    autoClose: 3000
                });
                return;
            }
            await deleteCandidate(
                data[0].token,
                deleteCandidateData.candidate_id
            );

            if(response.status === 403)
            throw new Error("You Don't have Permission to Perform this Action")
        
            toast.success("Candidate deleted successfully", {
                position: "top-right",
                autoClose: 3000
            });
            
            setShowDeleteDialog(false);
            if (onDelete) onDelete();
        } catch (error) {
            toast.error(error.message || "Failed to delete candidate", {
                position: "top-right",
                autoClose: 3000
            });
        }
    };

    return (
        <AlertDialog open={showDeleteDialog} onOpenChange={setShowDeleteDialog}>
            <AlertDialogContent>
                <AlertDialogHeader>
                    <AlertDialogTitle>Are you absolutely sure?</AlertDialogTitle>
                    <AlertDialogDescription>
                        This action cannot be undone. This will permanently delete the
                        candidate's data and remove their records from our servers.
                    </AlertDialogDescription>
                </AlertDialogHeader>
                <AlertDialogFooter>
                    <AlertDialogCancel onClick={() => setShowDeleteDialog(false)}>
                        Cancel
                    </AlertDialogCancel>
                    <AlertDialogAction onClick={handleDelete}>
                        Delete
                    </AlertDialogAction>
                </AlertDialogFooter>
            </AlertDialogContent>
        </AlertDialog>
    );
};

export default DeleteCandidate;