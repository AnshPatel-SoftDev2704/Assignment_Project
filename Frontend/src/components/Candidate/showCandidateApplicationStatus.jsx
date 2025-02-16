import React, { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from "@/components/ui/tooltip";
import { Button } from '@/components/ui/button';
import { Save, Trash2 } from "lucide-react";
import { getAllCandidates, getFile, updateApplicationStatus } from '@/services/Candidate/api';
import DeleteCandidate from './deleteCandidate';

const ShowCandidates = () => {
    const [showDeleteDialog, setShowDeleteDialog] = useState(false);
    const [candidates, setCandidates] = useState([]);
    const [updatedApplications, setUpdatedApplications] = useState({});
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

    const handleStatusChange = (applicationId, newStatus) => {
        setUpdatedApplications(prevState => ({
            ...prevState,
            [applicationId]: newStatus
        }));
    };

    const handleSave = async (application) => {
        try {
            const newStatus = updatedApplications[application.application_id];
            if (newStatus) {
                await updateApplicationStatus(data[0].token, application.application_id, newStatus);
                // Optionally, refresh data or provide feedback to the user
            }
        } catch (error) {
            console.error("Error saving status:", error);
        }
    };

    return (
        <div className='rounded-md border'>
            <Table>
                <TableHeader>
                    <TableRow>
                        <TableHead className='text-center'>Candidate</TableHead>
                        <TableHead className='text-center'>Job Title</TableHead>
                        <TableHead className='text-center'>Applied Date</TableHead>
                        <TableHead className='text-center'>Status</TableHead>
                        <TableHead className='text-center'>See CV</TableHead>
                        <TableHead className='text-center'>Actions</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {candidates && candidates.map(app => (
                        <TableRow key={app.application_id}>
                            <TableCell className='text-center'>{app.candidate_Details.candidate_name}</TableCell>
                            <TableCell className='text-center'>{app.job.job_title}</TableCell>
                            <TableCell className='text-center'>
                                {new Date(app.applied_Date).toLocaleDateString()}
                            </TableCell>
                            <TableCell className='text-center'>
                                <div className="select-container">
                                    <Select
                                        value={updatedApplications[app.application_id] || app.application_Status_id}
                                        onValueChange={(value) => handleStatusChange(app.application_id, value)}
                                    >
                                        <SelectTrigger className="w-[200px]">
                                            <SelectValue placeholder="Select status" />
                                        </SelectTrigger>
                                        <SelectContent>
                                            {applicationStatuses.map((status) => (
                                                <SelectItem
                                                    key={status.application_Status_id}
                                                    value={status.application_Status_id}
                                                >
                                                    {status.application_Status_Name}
                                                </SelectItem>
                                            ))}
                                        </SelectContent>
                                    </Select>
                                </div>
                            </TableCell>
                            <TableCell className="text-center">
                                <a onClick={() => handleOpenFile(app.candidate_Details.cV_Path)} target="_blank" rel="noopener noreferrer" className="text-blue-500 hover:underline">
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
                                                onClick={() => handleDelete(app)}
                                            >
                                                <Trash2 className="h-4 w-4" />
                                            </Button>
                                        </TooltipTrigger>
                                        <TooltipContent>
                                            <p>Delete candidate</p>
                                        </TooltipContent>
                                    </Tooltip>
                                    <Tooltip>
                                        <TooltipTrigger asChild>
                                            <Button
                                                variant="ghost"
                                                size="icon"
                                                className="h-8 w-8 text-blue-500 hover:text-blue-600"
                                                onClick={() => handleSave(app)}
                                            >
                                                <Save className="h-4 w-4" />
                                            </Button>
                                        </TooltipTrigger>
                                        <TooltipContent>
                                            <p>Save status</p>
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
