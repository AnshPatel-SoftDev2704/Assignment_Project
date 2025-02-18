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
import { updateSkill, getAllSkill } from '@/services/Skills/api';
import { getSkill } from '@/store/Skills/actions';
import { toast, ToastContainer } from 'react-toastify';

const UpdateSkill = ({ showEditDialog, setShowEditDialog, editSkill }) => {
    const data = useSelector((state) => state.Userdata);
    const dispatch = useDispatch();
    const [editFormData, setEditFormData] = useState({
        skill_id: editSkill?.skill_id || '',
        skill_name: editSkill?.skill_name || '',
        skill_description: editSkill?.skill_description || ''
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
            if (!editFormData.skill_name.trim() || !editFormData.skill_description.trim()) {
                toast.error("Both fields are required", {
                    position: "top-right",
                    autoClose: 3000
                });
                return;
            }

            const response = await updateSkill(data[0].token, editFormData);
            
            if(response.status === 403)
                throw new Error("You Don't have Permission to Perform this Action")
            
            const result = await getAllSkill(data[0].token);
            dispatch(getSkill(result.data));
            setShowEditDialog(false);
            toast.success("Skill updated successfully", {
                position: "top-right",
                autoClose: 3000
            });
        } catch (err) {
            console.error('Error updating skill:', err);
            toast.error("Failed to update skill", {
                position: "top-right",
                autoClose: 3000
            });
        }
    };

    return (
        <Dialog open={showEditDialog} onOpenChange={setShowEditDialog}>
            <DialogContent className="max-w-md">
                <DialogHeader>
                    <DialogTitle>Edit Skill</DialogTitle>
                </DialogHeader>
                <div className="space-y-4">
                    <div className="space-y-2">
                        <Label htmlFor="skill_name">Skill Name</Label>
                        <Input
                            id="skill_name"
                            name="skill_name"
                            value={editFormData.skill_name}
                            onChange={handleInputChange}
                        />
                    </div>

                    <div className="space-y-2">
                        <Label htmlFor="skill_description">Skill Description</Label>
                        <Input
                            id="skill_description"
                            name="skill_description"
                            value={editFormData.skill_description}
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
            <ToastContainer/>
            </DialogContent>
        </Dialog>
    );
};

export default UpdateSkill;