import { useState } from 'react';
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { useSelector, useDispatch } from 'react-redux';
import { saveRole, getAllRole } from '@/services/Roles/api';
import { getRole } from '@/store/Roles/actions';

const CreateRole = () => {
    const [roleData, setRoleData] = useState({
        role_Name: '',
        role_Description: ''
    });

    const data = useSelector((state) => state.Userdata);
    const dispatch = useDispatch();

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setRoleData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await saveRole(data[0].token, roleData);
            const result = await getAllRole(data[0].token);
            dispatch(getRole(result.data));
            setRoleData({
                role_Name: '',
                role_Description: ''
            });
        } catch (error) {
            console.error("Error saving role:", error);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-4 p-4">
            <div className="space-y-2">
                <Label htmlFor="role_Name">Role Name</Label>
                <Input
                    id="role_Name"
                    name="role_Name"
                    value={roleData.role_Name}
                    onChange={handleInputChange}
                    required
                />
            </div>

            <div className="space-y-2">
                <Label htmlFor="role_Description">Role Description</Label>
                <Input
                    id="role_Description"
                    name="role_Description"
                    value={roleData.role_Description}
                    onChange={handleInputChange}
                    required
                />
            </div>

            <Button type="submit">Create Role</Button>
        </form>
    );
};

export default CreateRole
