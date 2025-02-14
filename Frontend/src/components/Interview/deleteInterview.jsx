import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogDescription,
    DialogFooter,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { useSelector, useDispatch } from 'react-redux';
import { deleteInterview } from '@/services/Interview/api';

const DeleteInterview = ({ showDeleteDialog, setShowDeleteDialog, deleteInterviews }) => {
    const data = useSelector((state) => state.Userdata);
    const dispatch = useDispatch();

    const handleDelete = async () => {
        try {
            await deleteInterview(data[0].token, deleteInterviews.interview_id);
            setShowDeleteDialog(false);
        } catch (err) {
            console.error('Error deleting interview:', err);
        }
    };

    return (
        <Dialog open={showDeleteDialog} onOpenChange={setShowDeleteDialog}>
            <DialogContent className="bg-white text-black p-0 overflow-hidden">
                <DialogHeader className="pt-8 px-6">
                    <DialogTitle className="text-2xl text-center font-bold">
                        Delete Interview
                    </DialogTitle>
                    <DialogDescription className="text-center text-zinc-500">
                        Are you sure you want to delete this interview? <br />
                        Round {deleteInterviews?.number_of_round} interview scheduled for{' '}
                        <span className="text-indigo-500 font-semibold">
                            {new Date(deleteInterviews?.scheduled_at).toLocaleString()}
                        </span>
                        <br />
                        This action cannot be undone.
                    </DialogDescription>
                </DialogHeader>

                <DialogFooter className="bg-gray-100 px-6 py-4">
                    <div className="flex items-center justify-between w-full">
                        <Button
                            onClick={() => setShowDeleteDialog(false)}
                            variant="ghost"
                        >
                            Cancel
                        </Button>
                        <Button
                            variant="primary"
                            onClick={handleDelete}
                            className="bg-red-500 hover:bg-red-600 text-white"
                        >
                            Delete Interview
                        </Button>
                    </div>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    );
};

export default DeleteInterview;