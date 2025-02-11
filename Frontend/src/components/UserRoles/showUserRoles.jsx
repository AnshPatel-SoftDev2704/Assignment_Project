import { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { getAllUserRoles } from '@/services/UserRoles/api';
import { getUserRole } from '@/store/UserRoles/actions';
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table";
import { Button } from "@/components/ui/button";
import { Pencil, Trash2 } from "lucide-react";
import { useState } from 'react';
import UpdateUserRole from './updateUserRoles';
import DeleteUserRole from './deleteUserRoles';

const DisplayUserRole = () => {
    const dispatch = useDispatch();
    const data = useSelector((state) => state.Userdata);
    const userRoles = useSelector((state) => state.UserRoles);
    const [showEditDialog, setShowEditDialog] = useState(false);
    const [showDeleteDialog, setShowDeleteDialog] = useState(false);
    const [selectedUserRole, setSelectedUserRole] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            const response = await getAllUserRoles(data[0].token);
            dispatch(getUserRole(response.data));
        };
        fetchData();
    }, []);

    const handleEdit = (userRole) => {
        setSelectedUserRole(userRole);
        setShowEditDialog(true);
    };

    const handleDelete = (userRole) => {
        setSelectedUserRole(userRole);
        setShowDeleteDialog(true);
    };

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
                    {userRoles[0] && userRoles[0].map(userRole => (
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
