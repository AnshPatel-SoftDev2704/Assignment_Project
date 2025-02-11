import { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { getAllRole } from '@/services/Roles/api';
import { getRole } from '@/store/Roles/actions';
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
import UpdateRole from './updateRole';
import DeleteRole from './deleteRole';

const DisplayRole = () => {
    const dispatch = useDispatch();
    const data = useSelector((state) => state.Userdata);
    const roles = useSelector((state) => state.Roles);
    const [showEditDialog, setShowEditDialog] = useState(false);
    const [showDeleteDialog, setShowDeleteDialog] = useState(false);
    const [selectedRole, setSelectedRole] = useState(null);
    useEffect(() => {
        const fetchData = async () => {
            const response = await getAllRole(data[0].token);
            dispatch(getRole(response.data));
        };
        fetchData();
    }, []);

    const handleEdit = (role) => {
        setSelectedRole(role);
        setShowEditDialog(true);
    };

    const handleDelete = (role) => {
        setSelectedRole(role);
        setShowDeleteDialog(true);
    };

    return (
        <div className="rounded-md border">
            <Table>
                <TableHeader>
                    <TableRow>
                        <TableHead className="text-center">Role Name</TableHead>
                        <TableHead className="text-center">Description</TableHead>
                        <TableHead className="text-center">Created At</TableHead>
                        <TableHead className="text-center">Updated At</TableHead>
                        <TableHead className="text-center">Actions</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {roles[0] && roles[0].map(role => (
                        <TableRow key={role.role_id}>
                            <TableCell className="text-center">{role.role_Name}</TableCell>
                            <TableCell className="text-center">{role.role_Description}</TableCell>
                            <TableCell className="text-center">{new Date(role.created_at).toLocaleDateString()}</TableCell>
                            <TableCell className="text-center">{new Date(role.updated_at).toLocaleDateString()}</TableCell>
                            <TableCell className="text-center">
                                <Button
                                    variant="ghost"
                                    size="icon"
                                    onClick={() => handleEdit(role)}
                                >
                                    <Pencil className="h-4 w-4" />
                                </Button>
                                <Button
                                    variant="ghost"
                                    size="icon"
                                    className="text-red-500"
                                    onClick={() => handleDelete(role)}
                                >
                                    <Trash2 className="h-4 w-4" />
                                </Button>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>

            {showEditDialog && (
                <UpdateRole 
                    showEditDialog={showEditDialog}
                    setShowEditDialog={setShowEditDialog}
                    editRole={selectedRole}
                />
            )}

            {showDeleteDialog && (
                <DeleteRole 
                    showDeleteDialog={showDeleteDialog}
                    setShowDeleteDialog={setShowDeleteDialog}
                    deleteRoles={selectedRole}
                />
            )}
        </div>
    );
};

export default DisplayRole;
