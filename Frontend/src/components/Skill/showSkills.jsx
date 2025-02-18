import { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { getAllSkill } from '@/services/Skills/api';
import { getSkill } from '@/store/Skills/actions';
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
import UpdateSkill from './updateSkills';
import DeleteSkill from './deleteSkills';

const DisplaySkill = () => {
    const dispatch = useDispatch();
    const data = useSelector((state) => state.Userdata);
    const [showEditDialog, setShowEditDialog] = useState(false);
    const [showDeleteDialog, setShowDeleteDialog] = useState(false);
    const [selectedSkill, setSelectedSkill] = useState(null);
    const [skills,setSkills] = useState([])

    useEffect(() => {
        const fetchData = async () => {
            const response = await getAllSkill(data[0].token);
            setSkills(response.data)
        };
        fetchData();
    }, [showDeleteDialog,showEditDialog,setShowDeleteDialog,setShowEditDialog]);

    const handleEdit = (skill) => {
        setSelectedSkill(skill);
        setShowEditDialog(true);
    };

    const handleDelete = (skill) => {
        setSelectedSkill(skill);
        setShowDeleteDialog(true);
    };

    return (
        <div className="rounded-md border">
            <Table>
                <TableHeader>
                    <TableRow>
                        <TableHead className="text-center">Skill Name</TableHead>
                        <TableHead className="text-center">Description</TableHead>
                        <TableHead className="text-center">Created At</TableHead>
                        <TableHead className="text-center">Updated At</TableHead>
                        <TableHead className="text-center">Actions</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {skills && skills.map(skill => (
                        <TableRow key={skill.skill_id}>
                            <TableCell className="text-center">{skill.skill_name}</TableCell>
                            <TableCell className="text-center">{skill.skill_description}</TableCell>
                            <TableCell className="text-center">
                                {new Date(skill.created_at).toLocaleDateString()}
                            </TableCell>
                            <TableCell className="text-center">
                                {new Date(skill.updated_at).toLocaleDateString()}
                            </TableCell>
                            <TableCell className="text-center">
                                <Button
                                    variant="ghost"
                                    size="icon"
                                    onClick={() => handleEdit(skill)}
                                >
                                    <Pencil className="h-4 w-4" />
                                </Button>
                                <Button
                                    variant="ghost"
                                    size="icon"
                                    className="text-red-500"
                                    onClick={() => handleDelete(skill)}
                                >
                                    <Trash2 className="h-4 w-4" />
                                </Button>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>

            {showEditDialog && (
                <UpdateSkill 
                    showEditDialog={showEditDialog}
                    setShowEditDialog={setShowEditDialog}
                    editSkill={selectedSkill}
                />
            )}

            {showDeleteDialog && (
                <DeleteSkill 
                    showDeleteDialog={showDeleteDialog}
                    setShowDeleteDialog={setShowDeleteDialog}
                    deleteSkills={selectedSkill}
                />
            )}
        </div>
    );
};

export default DisplaySkill;