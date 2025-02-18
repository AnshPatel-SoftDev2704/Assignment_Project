import { getUser } from '@/store/Users/actions';
import { useSelector, useDispatch } from 'react-redux';
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table";
import {
    Tooltip,
    TooltipContent,
    TooltipProvider,
    TooltipTrigger,
  } from "@/components/ui/tooltip";
  import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogFooter,
    DialogDescription,
  } from "@/components/ui/dialog";
import { Pencil, Trash2, Eye} from "lucide-react";
import {deleteUser, getAllRole, getAllUser} from '@/services/Users/api';
import { useEffect, useState } from 'react';
import { Button } from '@/components/ui/button';
import UpdateUser from './updateUser';
import DeleteUser from './deleteUser';
import { getRole } from '@/store/Roles/actions';
import { toast } from 'react-toastify';

const ShowUser = () => {
    const [showEditDialog,setShowEditDialog] = useState(false);
    const [showDeleteDialog,setShowDeleteDialog] = useState(false)
    const [showDetailsDialog,setShowDetailsDialog] = useState(false)
    const [selectedUser,setSelectedUser] = useState({})
    const [editUser,setEditUser] = useState({})
    const [deleteUserData,setDeleteUserData] = useState({})
    const [users,setUsers] = useState([])
    const dispatch = useDispatch();
    const data = useSelector((state) => state.Userdata);
    useEffect(() => {
        fetchData();
    }, [showDeleteDialog,showEditDialog,setShowDeleteDialog,setShowEditDialog]);

    const fetchData = async () => {
        try{
        const response = await getAllUser(data[0].token);
        console.log(response.data[0])
        if(response.status === 403)
            throw new Error("You are not allowed to Perform this Action")
        setUsers(response.data)
        }
        catch(error)
        {
            toast.error(error.message || "Failed to Fetch Details");
        }
    };

    const handleEdit = async (user) => {
        setShowEditDialog(true);
        setEditUser(user);
    }

    const handleDelete = async (user) => {
        setShowDeleteDialog(true)
        setDeleteUserData(user)
    }

    const handleShowDetails = async (user) => {
        setShowDetailsDialog(true)
        setSelectedUser(user)
    }

    return (
        <>
            <div className='rounded-md border'>
                <Table>
                    <TableHeader>
                        <TableRow>
                            <TableHead className='text-center'>Name</TableHead>
                            <TableHead className='text-center'>Contact</TableHead>
                            <TableHead className='text-center'>Email</TableHead>
                            <TableHead className='text-center'>Created_at</TableHead>
                            <TableHead className='text-center'>Updated_at</TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>
                        {users && users.map(user => (
                            <TableRow key={user.user_id}>
                                <TableCell className='text-center'>{user.name}</TableCell>
                                <TableCell className='text-center'>{user.contact}</TableCell>
                                <TableCell className='text-center'>{user.email}</TableCell>
                                <TableCell className='text-center'>{user.created_at}</TableCell>
                                <TableCell className='text-center'>{user.updated_at}</TableCell>
                                <TableCell className="text-center">
                                    <TooltipProvider>
                                        <Tooltip>
                                            <TooltipTrigger asChild>
                                            <Button
                                                variant="ghost"
                                                size="icon"
                                                className="h-8 w-8 mr-2"
                                                onClick={() => handleShowDetails(user)}
                                            >
                                                <Eye className="h-4 w-4" />
                                            </Button>
                                            </TooltipTrigger>
                                            <TooltipContent>
                                            <p>View details</p>
                                            </TooltipContent>
                                        </Tooltip>
                                        <Tooltip>
                                            <TooltipTrigger asChild>
                                                <Button
                                                    variant="ghost"
                                                    size="icon"
                                                    className="h-8 w-8"
                                                    onClick={() => handleEdit(user)}
                                                >
                                                    <Pencil className="h-4 w-4" />
                                                </Button>
                                            </TooltipTrigger>
                                            <TooltipContent>
                                                <p>Edit user</p>
                                            </TooltipContent>
                                        </Tooltip>
                                        <Tooltip>
                                            <TooltipTrigger asChild>
                                                <Button
                                                    variant="ghost"
                                                    size="icon"
                                                    className="h-8 w-8 text-red-500 hover:text-red-600"
                                                    onClick={() => handleDelete(user)}
                                                >
                                                    <Trash2 className="h-4 w-4" />
                                                </Button>
                                            </TooltipTrigger>
                                            <TooltipContent>
                                                <p>Delete user</p>
                                            </TooltipContent>
                                        </Tooltip>
                                    </TooltipProvider>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </div>
            <Dialog open={showDetailsDialog} onOpenChange={setShowDetailsDialog}>
                <DialogContent className="max-w-2xl">
                <DialogHeader>
                    <DialogTitle>User Details</DialogTitle>
                </DialogHeader>
                {selectedUser && (
                    <div className='space-y-6'>
                        <div className='grid grid-cols-2 gap-6'>
                            <div className='space-y-4'>
                                <div>
                                    <h3 className='text-lg font-semibold mb-3'>User Information</h3>
                                    <div className='space-y-2'>
                                        <p><span className='font-medium'>Name: </span>{selectedUser.name}</p>
                                        <p><span className='font-medium'>Email: </span>{selectedUser.email}</p>
                                        <p><span className='font-medium'>Contact: </span>{selectedUser.contact}</p>
                                        <p><span className='font-medium'>Id: </span>{selectedUser.user_id}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                )}
                </DialogContent>
            </Dialog>
            {showEditDialog && <UpdateUser showEditDialog={showEditDialog} setShowEditDialog={setShowEditDialog} editUser={editUser}/>}
            {showDeleteDialog && <DeleteUser showDeleteDialog = {showDeleteDialog} setShowDeleteDialog={setShowDeleteDialog} deleteUserData = {deleteUserData}/>}
        </>
    );
}

export default ShowUser;