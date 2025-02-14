import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogFooter,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { useSelector, useDispatch } from 'react-redux';
import { deleteFeedback, getAllFeedback } from '@/services/Interview/api';

const DeleteFeedback = ({ showDeleteDialog, setShowDeleteDialog, deleteFeedbacks }) => {
    const data = useSelector((state) => state.Userdata);
    const dispatch = useDispatch();

    const handleDelete = async () => {
        try {
            console.log(deleteFeedbacks)
            await deleteFeedback(data[0].token, deleteFeedbacks.feedback_id);
            setShowDeleteDialog(false);
        } catch (err) {
            console.error('Error deleting feedback:', err);
        }
    };

    return (
        <Dialog open={showDeleteDialog} onOpenChange={setShowDeleteDialog}>
            <DialogContent className="bg-white text-black p-0 overflow-hidden">
                <DialogHeader className="pt-8 px-6">
                    <DialogTitle className="text-2xl text-center font-bold">
                        Delete Feedback
                    </DialogTitle>
                </DialogHeader>

                <DialogFooter className="bg-gray-100 px-6 py-4">
                    <Button onClick={() => setShowDeleteDialog(false)} variant="ghost">
                        Cancel
                    </Button>
                    <Button variant="primary" onClick={handleDelete} className="bg-red-500 hover:bg-red-600 text-white">
                        Delete Feedback
                    </Button>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    );
};

export default DeleteFeedback;
