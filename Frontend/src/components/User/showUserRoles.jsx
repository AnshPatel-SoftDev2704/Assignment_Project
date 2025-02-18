import { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { getAllUserRoles } from '@/services/Users/api';
import { getUserRole } from '@/store/UserRoles/actions';
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table";
import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogFooter,
    DialogDescription,
  } from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Pencil, Trash2,Eye } from "lucide-react";
import { useState } from 'react';
import UpdateUserRole from './updateUserRoles';
import DeleteUserRole from './deleteUserRoles';
import { toast } from 'react-toastify';

const DisplayUserRole = () => {
    const dispatch = useDispatch();
    const data = useSelector((state) => state.Userdata);
    const [showEditDialog, setShowEditDialog] = useState(false);
    const [showDeleteDialog, setShowDeleteDialog] = useState(false);
    const [selectedUserRole, setSelectedUserRole] = useState(null);
    const [userRoles,setUserRoles] = useState([])
    const [showDetailsDialog,setShowDetailsDialog] = useState(false)
    useEffect(() => {
        fetchData();
    }, [showEditDialog,showDeleteDialog,setShowDeleteDialog,setShowEditDialog]);

    const fetchData = async () => {
        try{
            const response = await getAllUserRoles(data[0].token);
            
            if(response.status === 403)
                throw new Error("You are Not Allowed to Perform this Action")

            setUserRoles(response.data)
        }
        catch(error)
        {
            toast.error(error.message || "Failed to Fetch Details");
        }
    };

    const handleEdit = (userRole) => {
        setSelectedUserRole(userRole);
        setShowEditDialog(true);
    };

    const handleDelete = (userRole) => {
        setSelectedUserRole(userRole);
        setShowDeleteDialog(true);
    };

    const handleShowDetails = async (user) => {
        setShowDetailsDialog(true)
        setSelectedUserRole(user)
    }

    return (
        <div className="rounded-md border">
            <Table>
                <TableHeader>
                    <TableRow>
                        <TableHead className="text-center">User Name</TableHead>
                        <TableHead className="text-center">Role Name</TableHead>
                        <TableHead className="text-center">Created At</TableHead>
                        <TableHead className="text-center">Updated At</TableHead>
                        <TableHead className="text-center">Actions</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {userRoles && userRoles.map(userRole => (
                        <TableRow key={userRole.userRoleId}>
                            <TableCell className="text-center">{userRole.user.name}</TableCell>
                            <TableCell className="text-center">{userRole.role.role_Name}</TableCell>
                            <TableCell className="text-center">
                                {new Date(userRole.created_at).toLocaleDateString()}
                            </TableCell>
                            <TableCell className="text-center">
                                {new Date(userRole.updated_at).toLocaleDateString()}
                            </TableCell>
                            <TableCell className="text-center">
                                <Button
                                    variant="ghost"
                                    size="icon"
                                    className="h-8 w-8 mr-2"
                                    onClick={() => handleShowDetails(userRole)}
                                >
                                    <Eye className="h-4 w-4" />
                                </Button>
                                <Button
                                    variant="ghost"
                                    size="icon"
                                    onClick={() => handleEdit(userRole)}
                                >
                                    <Pencil className="h-4 w-4" />
                                </Button>
                                <Button
                                    variant="ghost"
                                    size="icon"
                                    className="text-red-500"
                                    onClick={() => handleDelete(userRole)}
                                >
                                    <Trash2 className="h-4 w-4" />
                                </Button>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
            <Dialog open={showDetailsDialog} onOpenChange={setShowDetailsDialog}>
                <DialogContent className="max-w-2xl">
                <DialogHeader>
                    <DialogTitle>User Details</DialogTitle>
                </DialogHeader>
                {selectedUserRole && (
                    <div className='space-y-6'>
                        <div className='grid grid-cols-2 gap-6'>
                            <div className='space-y-4'>
                                <div>
                                    <h3 className='text-lg font-semibold mb-3'>User Information</h3>
                                    <div className='space-y-2'>
                                        <p><span className='font-medium'>Name: </span>{selectedUserRole.user.name}</p>
                                        <p><span className='font-medium'>Email: </span>{selectedUserRole.user.email}</p>
                                        <p><span className='font-medium'>Contact: </span>{selectedUserRole.user.contact}</p>
                                        <p><span className='font-medium'>Id: </span>{selectedUserRole.user.user_id}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                )}
                </DialogContent>
            </Dialog>
            {showEditDialog && (
                <UpdateUserRole 
                    showEditDialog={showEditDialog}
                    setShowEditDialog={setShowEditDialog}
                    editUserRole={selectedUserRole}
                />
            )}

            {showDeleteDialog && (
                <DeleteUserRole 
                    showDeleteDialog={showDeleteDialog}
                    setShowDeleteDialog={setShowDeleteDialog}
                    deleteUserRoles={selectedUserRole}
                />
            )}
        </div>
    );
};

export default DisplayUserRole;