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
import { updateUser } from '@/services/Users/api';
import { useSelector, useDispatch } from 'react-redux';
import { getUser } from '@/store/Users/actions';
import { getAllUser } from '@/services/Users/api';

const UpdateUser = ({showEditDialog, setShowEditDialog, editUser }) => {
  const data = useSelector((state) => state.Userdata);
  const dispatch = useDispatch();
  const [editFormData, setEditFormData] = useState({
    User_id: parseInt(editUser?.user_id),
    name: editUser?.name || '',
    email: editUser?.email || '',
    contact: editUser?.contact || '',
    password: ''
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
      if(editFormData.password === '')
        editFormData.password = editUser.password;

      await updateUser(data[0].token, editFormData);
      const result = await getAllUser(data[0].token);
      dispatch(getUser(result.data));
      setShowEditDialog(false);
    } catch (err) {
      console.error('Error updating user:', err);
    }
  };

  return (
    <Dialog open = {showEditDialog} onOpenChange={setShowEditDialog}>
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