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
import { deleteSkill, getAllSkill } from '@/services/Skills/api';
import { getSkill } from '@/store/Skills/actions';
import { toast } from 'react-toastify';

const DeleteSkill = ({ showDeleteDialog, setShowDeleteDialog, deleteSkills }) => {
    const data = useSelector((state) => state.Userdata);
    const dispatch = useDispatch();

    const handleDelete = async () => {
        try {
            if (!deleteSkills?.skill_id) {
                toast.error("Invalid skill selection", {
                    position: "top-right",
                    autoClose: 3000
                });
                return;
            }

            await deleteSkill(data[0].token, deleteSkills.skill_id);
            const result = await getAllSkill(data[0].token);
            
            if(result.status === 403)
                throw new Error("You Don't have Permission to Perform this Action")

            dispatch(getSkill(result.data));
            toast.success("Skill deleted successfully", {
                position: "top-right",
                autoClose: 3000
            });
            setShowDeleteDialog(false);
        } catch (error) {
            toast.error("Failed to delete skill", {
                position: "top-right",
                autoClose: 3000
            });
        }
    };

    return (
        <Dialog open={showDeleteDialog} onOpenChange={setShowDeleteDialog}>
            <DialogContent className="bg-white text-black p-0 overflow-hidden">
                <DialogHeader className="pt-8 px-6">
                    <DialogTitle className="text-2xl text-center font-bold">
                        Delete Skill
                    </DialogTitle>
                    <DialogDescription className="text-center text-zinc-500">
                        Are you sure you want to delete this skill? <br />
                        <span className="text-indigo-500 font-semibold">{deleteSkills?.skill_name}</span> will be permanently deleted.
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
                        >
                            Confirm
                        </Button>
                    </div>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    );
};

export default DeleteSkill;