import { useEffect, useState } from 'react';
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select";
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Badge } from "@/components/ui/badge";
import { X } from "lucide-react";
import { useSelector,useDispatch } from 'react-redux';
import { getAllRole, getAllUser, saveUser, saveUserRoles } from '@/services/Users/api';
import { getRole } from '@/store/Roles/actions';
import { getUser } from '@/store/Users/actions';

const SaveUser = () => {
    const [userData, setUserData] = useState({
        name: '',
        email: '',
        contact: '',
        password: '',
    });
    const [selectedRoles,setSelectedRoles] = useState([])
    const [roles,setRoles] = useState([])
    const user = useSelector((state) => state.Userdata)
    const dispatch = useDispatch()

    useEffect(()=>{
        const fetchData = async () => {
            const response = await getAllRole(user.token)
            setRoles(response.data)
            dispatch(getRole(response.data))
        }
        fetchData()
    },[])
 
    const [selectedRole, setSelectedRole] = useState("");

    const handleAddRole = () => {
        if (!(selectedRole && selectedRoles.find(role => role.role_id === parseInt(selectedRole)))) {
            const roleToAdd = roles.find(role => role.role_id === parseInt(selectedRole));
            setSelectedRoles([...selectedRoles,roleToAdd])
        }
        setSelectedRole("");
    };

    const handleRemoveRole = (roleId) => {
        setSelectedRoles(selectedRoles.filter(role => role.role_id !== roleId))
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try{
            const response = await saveUser(user.token,userData)
            const result = await getAllUser(user.token)
            dispatch(getUser(result.data))
            const User_id = response.data.user_id
            selectedRoles.forEach(role => {
                const response = saveUserRoles(user.token,User_id,role.role_id)
            });
        }        
        catch(error)
        {
            console.error('Error Saving User',error)
        }
    };

    return (
        <Card>
            <CardHeader>
                <CardTitle>Add New User</CardTitle>
            </CardHeader>
            <CardContent>
                <form onSubmit={handleSubmit} className="space-y-4">
                    <Input
                        placeholder="Name"
                        value={userData.name}
                        onChange={(e) => setUserData({ ...userData, name: e.target.value })}
                        required
                    />
                    <Input
                        placeholder="Email"
                        type="email"
                        value={userData.email}
                        onChange={(e) => setUserData({ ...userData, email: e.target.value })}
                        required
                    />
                    <Input
                        placeholder="Phone Number"
                        value={userData.contact}
                        onChange={(e) => setUserData({ ...userData, contact: e.target.value })}
                        required
                    />
                        <Input
                            type="password"
                            placeholder="Password"
                            value={userData.password}
                            onChange={(e) => setUserData({ ...userData, password: e.target.value })}
                            required
                        />
                    
                    <div className="flex flex-wrap gap-2 mb-4">
                        {selectedRoles.map((role) => (
                            <Badge 
                                key={role.role_id} 
                                variant="secondary"
                                className="flex items-center gap-1"
                            >
                                {role.role_Name}
                                <X
                                    className="h-3 w-3 cursor-pointer"
                                    onClick={() => handleRemoveRole(role.role_id)}
                                />
                            </Badge>
                        ))}
                    </div>

                    <div className="flex gap-2">
                        <Select
                            value={selectedRole}
                            onValueChange={setSelectedRole}
                        >
                            <SelectTrigger className="w-full">
                                <SelectValue placeholder="Select role" />
                            </SelectTrigger>
                            <SelectContent>
                                {roles.filter(role => 
                                    !selectedRoles.find(r => r.role_id === role.role_id)
                                ).map((role) => (
                                    <SelectItem 
                                        key={role.role_id} 
                                        value={role.role_id.toString()}
                                    >
                                        {role.role_Name}
                                    </SelectItem>
                                ))}
                            </SelectContent>
                        </Select>
                        <Button 
                            type="button" 
                            variant="outline" 
                            onClick={handleAddRole}
                            disabled={!selectedRole}
                        >
                            Add Role
                        </Button>
                    </div>

                    <Button type="submit" className="w-full">
                        Add User
                    </Button>
                </form>
            </CardContent>
        </Card>
    );
};

export default SaveUser;
