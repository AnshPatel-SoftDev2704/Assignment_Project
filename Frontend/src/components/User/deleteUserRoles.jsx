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
import { deleteUserRole, getAllUserRoles } from '@/services/Users/api';
import { getUserRole } from '@/store/UserRoles/actions';

const DeleteUserRole = ({ showDeleteDialog, setShowDeleteDialog, deleteUserRoles }) => {
    const data = useSelector((state) => state.Userdata);
    const dispatch = useDispatch();
    const handleDelete = async () => {
        try {
            console.log(deleteUserRoles.userRoleId)
            await deleteUserRole(data[0].token, deleteUserRoles.userRoleId);
            const result = await getAllUserRoles(data[0].token);
            dispatch(getUserRole(result.data));
            setShowDeleteDialog(false);
        } catch (err) {
            console.error('Error deleting user role:', err);
        }
    };

    return (
        <Dialog open={showDeleteDialog} onOpenChange={setShowDeleteDialog}>
            <DialogContent className="bg-white text-black p-0 overflow-hidden">
                <DialogHeader className="pt-8 px-6">
                    <DialogTitle className="text-2xl text-center font-bold">
                        Delete User Role
                    </DialogTitle>
                    <DialogDescription className="text-center text-zinc-500">
                        Are you sure you want to delete this user role assignment? <br />
                        <span className="text-indigo-500 font-semibold">
                            {deleteUserRoles?.user.name} - {deleteUserRoles?.role.role_Name}
                        </span> will be permanently deleted.
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

export default DeleteUserRole;