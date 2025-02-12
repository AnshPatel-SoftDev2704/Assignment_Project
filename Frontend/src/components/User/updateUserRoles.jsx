import { useState, useEffect } from 'react';
import { Button } from "@/components/ui/button";
import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogFooter,
} from "@/components/ui/dialog";
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select";
import { Label } from "@/components/ui/label";
import { useSelector, useDispatch } from 'react-redux';
import { updateUserRole, getAllUserRoles } from '@/services/Users/api';
import { getUserRole } from '@/store/UserRoles/actions';
import { getAllUser } from '@/services/Users/api';
import { getAllRole } from '@/services/Users/api';

const UpdateUserRole = ({ showEditDialog, setShowEditDialog, editUserRole }) => {
    const data = useSelector((state) => state.Userdata);
    const dispatch = useDispatch();
    const [users, setUsers] = useState([]);
    const [roles, setRoles] = useState([]);
    const [editFormData, setEditFormData] = useState({
        userRoleId: editUserRole?.userRoleId || '',
        user_id: editUserRole?.user_id || '',
        role_id: editUserRole?.role_id || ''
    });

    useEffect(() => {
        const fetchData = async () => {
            try {
                const usersResponse = await getAllUser(data[0].token);
                const rolesResponse = await getAllRole(data[0].token);
                setUsers(usersResponse.data);
                setRoles(rolesResponse.data);
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        };
        fetchData();
    }, []);

    const handleUpdate = async () => {
        try {
            await updateUserRole(data[0].token, editFormData);
            const result = await getAllUserRoles(data[0].token);
            dispatch(getUserRole(result.data));
            setShowEditDialog(false);
        } catch (err) {
            console.error('Error updating user role:', err);
        }
    };

    return (
        <Dialog open={showEditDialog} onOpenChange={setShowEditDialog}>
            <DialogContent className="max-w-md">
                <DialogHeader>
                    <DialogTitle>Edit User Role</DialogTitle>
                </DialogHeader>
                <div className="space-y-4">
                    <div className="space-y-2">
                        <Label htmlFor="user_id">Select User</Label>
                        <Select
                            value={editFormData.user_id.toString()}
                            onValueChange={(value) => setEditFormData(prev => ({ ...prev, user_id: value }))}
                        >
                            <SelectTrigger>
                                <SelectValue placeholder="Select a user" />
                            </SelectTrigger>
                            <SelectContent>
                                {users.map(user => (
                                    <SelectItem key={user.user_id} value={user.user_id.toString()}>
                                        {user.name}
                                    </SelectItem>
                                ))}
                            </SelectContent>
                        </Select>
                    </div>

                    <div className="space-y-2">
                        <Label htmlFor="role_id">Select Role</Label>
                        <Select
                            value={editFormData.role_id.toString()}
                            onValueChange={(value) => setEditFormData(prev => ({ ...prev, role_id: value }))}
                        >
                            <SelectTrigger>
                                <SelectValue placeholder="Select a role" />
                            </SelectTrigger>
                            <SelectContent>
                                {roles.map(role => (
                                    <SelectItem key={role.role_id} value={role.role_id.toString()}>
                                        {role.role_Name}
                                    </SelectItem>
                                ))}
                            </SelectContent>
                        </Select>
                    </div>
                </div>
                <DialogFooter>
                    <Button variant="outline" onClick={() => setShowEditDialog(false)}>
                        Cancel
                    </Button>
                    <Button onClick={handleUpdate}>
                        Save Changes
                    </Button>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    );
};

export default UpdateUserRole;