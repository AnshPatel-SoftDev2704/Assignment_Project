import { useState, useEffect } from 'react';
import { Button } from "@/components/ui/button";
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select";
import { Label } from "@/components/ui/label";
import { useSelector, useDispatch } from 'react-redux';
import { saveUserRole, getAllUserRoles } from '@/services/UserRoles/api';
import { getUserRole } from '@/store/UserRoles/actions';
import { getAllUser } from '@/services/Users/api';
import { getAllRole } from '@/services/Roles/api';

const CreateUserRole = () => {
    const [userRoleData, setUserRoleData] = useState({
        user_id: '',
        role_id: ''
    });

    const [users, setUsers] = useState([]);
    const [roles, setRoles] = useState([]);
    const data = useSelector((state) => state.Userdata);
    const dispatch = useDispatch();

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

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await saveUserRole(data[0].token, userRoleData);
            const result = await getAllUserRoles(data[0].token);
            dispatch(getUserRole(result.data));
            setUserRoleData({
                user_id: '',
                role_id: ''
            });
        } catch (error) {
            console.error("Error saving user role:", error);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-4 p-4">
            <div className="space-y-2">
                <Label htmlFor="user_id">Select User</Label>
                <Select
                    value={userRoleData.user_id}
                    onValueChange={(value) => setUserRoleData(prev => ({ ...prev, user_id: value }))}
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
                    value={userRoleData.role_id}
                    onValueChange={(value) => setUserRoleData(prev => ({ ...prev, role_id: value }))}
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

            <Button type="submit">Assign Role</Button>
        </form>
    );
};

export default CreateUserRole;
