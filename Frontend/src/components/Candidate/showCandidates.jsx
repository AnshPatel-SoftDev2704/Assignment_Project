import React, { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from "@/components/ui/tooltip";
import { Button } from '@/components/ui/button';
import { Trash2 } from "lucide-react";
import { getAllCandidates, getFile } from '@/services/Candidate/api';
import DeleteCandidate from './deleteCandidate';

const ShowCandidates = () => {
    const [showEditDialog, setShowEditDialog] = useState(false);
    const [showDeleteDialog, setShowDeleteDialog] = useState(false);
    const [candidates, setCandidates] = useState([]);
    const dispatch = useDispatch();
    const data = useSelector((state) => state.Userdata);

    useEffect(() => {
        const fetchData = async () => {
            const response = await getAllCandidates(data[0].token);
            setCandidates(response.data);
            console.log(response.data);
        };
        fetchData();
    }, [showDeleteDialog, setShowDeleteDialog]);

    const handleDelete = (candidate) => {
        setShowDeleteDialog(true);
        setDeleteCandidate(candidate);
    };

    const handleOpenFile = async (filepath) => {
        try {
            const response = await getFile(data[0].token, filepath);
            if (response.status === 200) {
                window.open(`http://localhost:5195/api/Candidate_Details/GetFile?filepath=${encodeURIComponent(filepath)}`, '_blank');
            }
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <div className='rounded-md border'>
            <Table>
                <TableHeader>
                    <TableRow>
                        <TableHead className='text-center'>Name</TableHead>
                        <TableHead className='text-center'>Email</TableHead>
                        <TableHead className='text-center'>Phone</TableHead>
                        <TableHead className='text-center'>Experience</TableHead>
                        <TableHead className='text-center'>CV</TableHead>
                        <TableHead className='text-center'>Actions</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {candidates && candidates.map(candidate => (
                        <TableRow key={candidate.candidate_id}>
                            <TableCell className='text-center'>{candidate.candidate_name}</TableCell>
                            <TableCell className='text-center'>{candidate.candidate_email}</TableCell>
                            <TableCell className='text-center'>{candidate.phoneNo}</TableCell>
                            <TableCell className='text-center'>{candidate.candidate_Total_Work_Experience} years</TableCell>
                            <TableCell className="text-center">
                                <a onClick={() => handleOpenFile(candidate.cV_Path)} target="_blank" rel="noopener noreferrer" className="text-blue-500 hover:underline">
                                    See CV
                                </a>
                            </TableCell>
                            <TableCell className="text-center">
                                <TooltipProvider>
                                    <Tooltip>
                                        <TooltipTrigger asChild>
                                            <Button
                                                variant="ghost"
                                                size="icon"
                                                className="h-8 w-8 text-red-500 hover:text-red-600"
                                                onClick={() => handleDelete(candidate)}
                                            >
                                                <Trash2 className="h-4 w-4" />
                                            </Button>
                                        </TooltipTrigger>
                                        <TooltipContent>
                                            <p>Delete candidate</p>
                                        </TooltipContent>
                                    </Tooltip>
                                </TooltipProvider>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
            {showDeleteDialog && <DeleteCandidate showDeleteDialog={showDeleteDialog} setShowDeleteDialog={setShowDeleteDialog} deleteCandidateData={deleteCandidate} />}
        </div>
    );
};

export default ShowCandidates;