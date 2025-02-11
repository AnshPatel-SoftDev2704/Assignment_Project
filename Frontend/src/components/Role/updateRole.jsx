import { useState } from 'react';
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogFooter,
} from "@/components/ui/dialog";
import { useSelector, useDispatch } from 'react-redux';
import { updateRole, getAllRole } from '@/services/Roles/api';
import { getRole } from '@/store/Roles/actions';

const UpdateRole = ({ showEditDialog, setShowEditDialog, editRole }) => {
    const data = useSelector((state) => state.Userdata);
    const dispatch = useDispatch();
    const [editFormData, setEditFormData] = useState({
        role_id: parseInt(editRole?.role_id),
        role_Name: editRole?.role_Name || '',
        role_Description: editRole?.role_Description || ''
    });

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setEditFormData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleUpdate = async () => {
        try {
            await updateRole(data[0].token, editFormData);
            const result = await getAllRole(data[0].token);
            dispatch(getRole(result.data));
            setShowEditDialog(false);
        } catch (err) {
            console.error('Error updating role:', err);
        }
    };

    return (
        <Dialog open={showEditDialog} onOpenChange={setShowEditDialog}>
            <DialogContent className="max-w-md">
                <DialogHeader>
                    <DialogTitle>Edit Role</DialogTitle>
                </DialogHeader>
                <div className="space-y-4">
                    <div className="space-y-2">
                        <Label htmlFor="role_Name">Role Name</Label>
                        <Input
                            id="role_Name"
                            name="role_Name"
                            value={editFormData.role_Name}
                            onChange={handleInputChange}
                        />
                    </div>

                    <div className="space-y-2">
                        <Label htmlFor="role_Description">Role Description</Label>
                        <Input
                            id="role_Description"
                            name="role_Description"
                            value={editFormData.role_Description}
                            onChange={handleInputChange}
                        />
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

export default UpdateRole;
