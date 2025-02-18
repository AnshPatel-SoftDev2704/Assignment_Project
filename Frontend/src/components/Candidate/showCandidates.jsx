import React, { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from "@/components/ui/tooltip";
import { Button } from '@/components/ui/button';
import { Save, Trash2 } from "lucide-react";
import { getAllCandidates, getFile } from '@/services/Candidate/api';
import DeleteCandidate from './deleteCandidate';
import { toast } from 'react-toastify';
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select";

const ShowCandidates = () => {
    const [showDeleteDialog, setShowDeleteDialog] = useState(false);
    const [deleteCandidate, setDeleteCandidate] = useState(null);
    const [candidates, setCandidates] = useState([]);
    const [loading, setLoading] = useState(true);
    const data = useSelector((state) => state.Userdata);

    useEffect(() => {
        fetchCandidates();
    }, []);

    const fetchCandidates = async () => {
        try {
            setLoading(true);
            const response = await getAllCandidates(data[0].token);
            if (!response.data) {
                throw new Error('No data received');
            }
            setCandidates(response.data);
        } catch (error) {
            toast.error(error.message || "Failed to fetch candidates", {
                position: "top-right",
                autoClose: 3000
            });
        } finally {
            setLoading(false);
        }
    };

    const handleDelete = (candidate) => {
        if (!candidate) {
            toast.error("Invalid candidate selection", {
                position: "top-right",
                autoClose: 3000
            });
            return;
        }
        setDeleteCandidate(candidate);
        setShowDeleteDialog(true);
    };

    const handleOpenFile = async (filepath) => {
        try {
            if (!filepath) {
                toast.error("No CV file available", {
                    position: "top-right",
                    autoClose: 3000
                });
                return;
            }

            const response = await getFile(data[0].token, filepath);
            console.log(filepath)
            if (response.status === 200) {
                window.open(`http://localhost:5195/api/Document_Submitted/GetFile?filepath=${encodeURI(filepath)}`, '_blank');
            } else {
                throw new Error("Failed to retrieve document");
            }
        } catch (error) {
            toast.error(error.message || "Failed to open CV file", {
                position: "top-right",
                autoClose: 3000
            });
        }
    };


    if (loading) {
        return <div className="flex justify-center items-center h-64">Loading candidates...</div>;
    }

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
                    {candidates.map((candidate) => (
                        <TableRow key={candidate.candidate_id}>
                            <TableCell className='text-center'>{candidate.candidate_name}</TableCell>
                            <TableCell className='text-center'>{candidate.candidate_email}</TableCell>
                            <TableCell className='text-center'>{candidate.phoneNo}</TableCell>
                            <TableCell className='text-center'>
                                {candidate.candidate_Total_Work_Experience} years
                            </TableCell>
                            <TableCell className='text-center'>
                                <Button
                                    variant="link"
                                    onClick={() => handleOpenFile(candidate.cV_Path)}
                                >
                                    View CV
                                </Button>
                            </TableCell>
                            <TableCell className='text-center'>
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

            {showDeleteDialog && (
                <DeleteCandidate
                    showDeleteDialog={showDeleteDialog}
                    setShowDeleteDialog={setShowDeleteDialog}
                    deleteCandidateData={deleteCandidate}
                    onDelete={fetchCandidates}
                />
            )}
        </div>
    );
};

export default ShowCandidates;