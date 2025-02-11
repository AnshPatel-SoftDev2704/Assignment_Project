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
import { Pencil, Trash2 } from "lucide-react";
import {deleteUser, getAllUser} from '@/services/Users/api';
import { useEffect, useState } from 'react';
import { Button } from '@/components/ui/button';
import UpdateUser from './updateUser';
import DeleteUser from './deleteUser';

const ShowUser = () => {
    const [showEditDialog,setShowEditDialog] = useState(false);
    const [showDeleteDialog,setShowDeleteDialog] = useState(false)
    const [editUser,setEditUser] = useState({})
    const [deleteUserData,setDeleteUserData] = useState({})
    const dispatch = useDispatch();
    const data = useSelector((state) => state.Userdata);
    const users = useSelector((state) => state.Users);
    useEffect(() => {
        const fetchData = async () => {
            const response = await getAllUser(data[0].token);
            dispatch(getUser(response.data));
        };
        fetchData();
    }, []);


    const handleEdit = async (user) => {
        setShowEditDialog(true);
        setEditUser(user);
    }

    const handleDelete = async (user) => {
        setShowDeleteDialog(true)
        setDeleteUserData(user)
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
                        {users[0] && users[0].map(user => (
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
            {showEditDialog && <UpdateUser showEditDialog={showEditDialog} setShowEditDialog={setShowEditDialog} editUser={editUser}/>}
            {showDeleteDialog && <DeleteUser showDeleteDialog = {showDeleteDialog} setShowDeleteDialog={setShowDeleteDialog} deleteUserData = {deleteUserData}/>}
        </>
    );
}

export default ShowUser;