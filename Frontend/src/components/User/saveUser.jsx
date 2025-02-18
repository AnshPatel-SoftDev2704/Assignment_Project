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
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

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
            try{
            const response = await getAllRole(user[0].token)
            if(response.status === 403)
                throw new Error("You are Not Allowed to Peform this Action");
            setRoles(response.data)
            dispatch(getRole(response.data))
            }
            catch(error)
            {
                toast.error(error.message || "Failed to Fetch Details");
            }
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
    const validateUserData = () => {
        if (!userData.name.trim()) {
            toast.error("User name is required", { position: "top-right", autoClose: 3000 });
            return false;
        }
        if (!userData.email.trim()) {
            toast.error("Email is required", { position: "top-right", autoClose: 3000 });
            return false;
        }
        if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(userData.email)) {
            toast.error("Invalid email format", { position: "top-right", autoClose: 3000 });
            return false;
        }
        if (!userData.contact.trim()) {
            toast.error("Phone number is required", { position: "top-right", autoClose: 3000 });
            return false;
        }
        if (!/^\d{10}$/.test(userData.contact)) {
            toast.error("Invalid phone number format", { position: "top-right", autoClose: 3000 });
            return false;
        }
        if (!selectedRoles.length) {
            toast.error("Please select at least one Role", { position: "top-right", autoClose: 3000 });
            return false;
        }
        return true;
    };
    
    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (!validateUserData()) {
                throw new Error("Invalid user data");
            }
    
            const response = await saveUser(user[0].token, userData);
            const result = await getAllUser(user[0].token);
            
            if (result.status === 403) {
                throw new Error("You are not allowed to perform this action");
            }

            if(result.status === 200)
                toast.success("User added successfully", {
                    position: "top-right",
                    autoClose: 3000,
                    hideProgressBar: false,
                    closeOnClick: true,
                    pauseOnHover: true,
                    draggable: true,
                  });
    
            dispatch(getUser(result.data));
            const User_id = response.data.user_id;
            
            for (const role of selectedRoles) {
                await saveUserRoles(user[0].token, User_id, role.role_id);
            }
            setSelectedRole([])
            setUserData({
                name: '',
                email: '',
                contact: '',
                password: '',
            })
        } catch (error) {
            toast.error(error.message || "Failed to fetch details");
        }
    };
    

    return (
        <>
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
        <ToastContainer/>
        </>
    );
};

export default SaveUser;
