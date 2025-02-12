import { useState, useEffect } from 'react';
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
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Badge } from "@/components/ui/badge";
import { X } from "lucide-react";
import { updateUser, getAllUser, getAllUserRoles, saveUserRoles} from '@/services/Users/api';
import { useSelector, useDispatch } from 'react-redux';
import { getUser } from '@/store/Users/actions';
import { getRole } from '@/store/Roles/actions';

const UpdateUser = ({ 
  showEditDialog, 
  setShowEditDialog, 
  editUser,
}) => {
  const user = useSelector((state) => state.Userdata);

  const [editFormData, setEditFormData] = useState({
    User_id: parseInt(editUser?.user_id),
    name: editUser?.name || '',
    email: editUser?.email || '',
    contact: editUser?.contact || '',
    password: '',
  });

  const [selectedRole, setSelectedRole] = useState("");

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setEditFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const [selectedRoles,setSelectedRoles] = useState([])
  const [updatedRoles,setUpdatedRoles] = useState([])
  const dispatch = useDispatch()
  const roles = useSelector((state) => state.Roles)
  useEffect(()=>{
      const fetchData = async () => {
          let response = await getAllUserRoles(user[0].token)
          response = response.data.filter(userroles => userroles.user_id === editUser.user_id)
          const filteredRoles = response
          .filter(item => item.role.role_id > 0)
          .map(item => item.role);          
          setSelectedRoles(filteredRoles)
      }
      fetchData()
  },[])


  const handleAddRole = () => {
      if (!(selectedRole && selectedRoles.find(role => role.role_id === parseInt(selectedRole)))) {
          const roleToAdd = roles[0].find(role => role.role_id === parseInt(selectedRole));
          setSelectedRoles([...selectedRoles,roleToAdd])
          setUpdatedRoles([...updatedRoles,roleToAdd])
      }
      setSelectedRole("");
  };

  const handleRemoveRole = (roleId) => {
      setSelectedRoles(selectedRoles.filter(role => role.role_id !== roleId))
      setUpdatedRoles(updatedRoles.filter(role => role.role_id !== roleId))
  };

  const handleUpdate = async () => {
    try {
      const updateData = {
        ...editFormData,
        password: editFormData.password === '' ? editUser.password : editFormData.password,
      };

      await updateUser(user[0].token, updateData);
      const result = await getAllUser(user[0].token);
      dispatch(getUser(result.data));
      const User_id = editUser.user_id
      updatedRoles.forEach(role => {
          const response = saveUserRoles(user[0].token,User_id,role.role_id)
      });
      setShowEditDialog(false);
    } catch (err) {
      console.error('Error updating user:', err);
    }
  };

  return (
    <Dialog open={showEditDialog} onOpenChange={setShowEditDialog}>
      <DialogContent className="max-w-md">
        <DialogHeader>
          <DialogTitle>Edit User</DialogTitle>
        </DialogHeader>
        
        <div className="space-y-4">
          <div className="space-y-2">
            <Label htmlFor="name">Name</Label>
            <Input
              id="name"
              name="name"
              value={editFormData.name}
              onChange={handleInputChange}
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="email">Email</Label>
            <Input
              id="email"
              name="email"
              value={editFormData.email}
              onChange={handleInputChange}
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="contact">Contact</Label>
            <Input
              id="contact"
              name="contact"
              value={editFormData.contact}
              onChange={handleInputChange}
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="password">Password (leave empty to keep current)</Label>
            <Input
              id="password"
              name="password"
              type="password"
              value={editFormData.password}
              onChange={handleInputChange}
            />
          </div>

          <div className="space-y-2">
            <Label>User Roles</Label>
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
                {roles[0].filter(role => 
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

export default UpdateUser;