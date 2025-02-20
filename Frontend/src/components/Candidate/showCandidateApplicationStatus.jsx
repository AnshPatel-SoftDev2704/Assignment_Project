import React, { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from "@/components/ui/tooltip";
import { Button } from '@/components/ui/button';
import { Save } from "lucide-react";
import { getAllApplications, getFile, updateApplicationStatus, getAllApplicationStatuses } from '@/services/Candidate/api';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select";
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Input } from "@/components/ui/input";

const CandidateApplicationStatus = () => {
    const [showDeleteDialog, setShowDeleteDialog] = useState(false);
    const [candidates, setCandidates] = useState([]);
    const [updatedApplications, setUpdatedApplications] = useState({});
    const dispatch = useDispatch();
    const data = useSelector((state) => state.Userdata);
    const [loading, setLoading] = useState(true);
    const [applicationStatuses, setApplicationStatuses] = useState([]);
    const [deleteCandidate, setDeleteCandidate] = useState(null);
    const [filters, setFilters] = useState({
        candidateName: '',
        jobTitle: '',
        appliedDate: '',
        status: 'ALL'
    });

    useEffect(() => {
        const fetchApplications = async () => {
            try {
                setLoading(true);
                const [statusesResponse, appsResponse] = await Promise.all([
                    getAllApplicationStatuses(data[0].token),
                    getAllApplications(data[0].token)
                ]);

                if (!statusesResponse.data || !appsResponse.data) {
                    throw new Error('Failed to fetch data');
                }

                setApplicationStatuses(statusesResponse.data);
                setCandidates(appsResponse.data);
            } catch (error) {
                toast.error(error.message || "Failed to fetch application data", {
                    position: "top-right",
                    autoClose: 3000
                });
            } finally {
                setLoading(false);
            }
        };
        fetchApplications();
    }, []);

    const handleDelete = (candidate) => {
        setShowDeleteDialog(true);
        setDeleteCandidate(candidate);
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
            if (response.status === 200) {
                window.open(
                    `http://localhost:5195/api/Document_Submitted/GetFile?filepath=${encodeURI(filepath)}`,
                    '_blank'
                );
            } else {
                throw new Error('Failed to fetch file');
            }
        } catch (error) {
            toast.error(error.message || "Failed to open CV file", {
                position: "top-right",
                autoClose: 3000
            });
        }
    };

    const handleStatusChange = async (app, newStatus) => {
        try {
            setUpdatedApplications({
                Candidate_id: app.candidate_id,
                Job_id: app.job_id,
                application_Status_id: newStatus,
                applied_Date: app.applied_Date
            });
        } catch (error) {
            toast.error("Failed to update status", {
                position: "top-right",
                autoClose: 3000
            });
        }
    };

    const handleUpdate = async (app) => {
        try {
            if (!app || !updatedApplications.application_Status_id) {
                toast.error("Please select a status before updating", {
                    position: "top-right",
                    autoClose: 3000
                });
                return;
            }

            if (app.application_Status_id === updatedApplications.application_Status_id) {
                toast.info("No changes to update", {
                    position: "top-right",
                    autoClose: 3000
                });
                return;
            }

            const response = await updateApplicationStatus(
                data[0].token,
                app.candidate_Application_Status_id,
                updatedApplications
            );

            if (response.status === 403)
                throw new Error("You Don't have Permission to Perform this Action");

            if (response.status === 200) {
                toast.success("Application status updated successfully", {
                    position: "top-right",
                    autoClose: 3000
                });

                const response = await getAllApplications(data[0].token);
                setCandidates(response.data);

                setUpdatedApplications({
                    Candidate_id: '',
                    Job_id: '',
                    application_Status_id: '',
                    applied_Date: ''
                });
            } else {
                throw new Error("Failed to update status");
            }
        } catch (error) {
            toast.error(error.message || "Error updating application status", {
                position: "top-right",
                autoClose: 3000
            });
        }
    };

    const handleFilterChange = (filterName, value) => {
        setFilters(prevFilters => ({
            ...prevFilters,
            [filterName]: value
        }));
    };

    const filteredCandidates = candidates.filter(app => {
        const candidateNameMatch = app.candidate_Details.candidate_name.toLowerCase()
            .includes(filters.candidateName.toLowerCase());
        const jobTitleMatch = app.job.job_title.toLowerCase()
            .includes(filters.jobTitle.toLowerCase());
        const appliedDateMatch = filters.appliedDate === '' || 
            new Date(app.applied_Date).toLocaleDateString().includes(filters.appliedDate);
        const statusMatch = filters.status === 'ALL' || 
            app.application_Status_id.toString() === filters.status;

        return candidateNameMatch && jobTitleMatch && appliedDateMatch && statusMatch;
    });

    if (loading) {
        return <div className="flex justify-center items-center h-64">Loading applications...</div>;
    }

    return (
        <Card>
            <CardHeader>
                <CardTitle>Candidate Applications</CardTitle>
            </CardHeader>
            <CardContent>
                <div className="grid grid-cols-4 gap-4 mb-6">
                    <Input
                        placeholder="Filter by candidate name"
                        value={filters.candidateName}
                        onChange={(e) => handleFilterChange('candidateName', e.target.value)}
                    />
                    <Input
                        placeholder="Filter by job title"
                        value={filters.jobTitle}
                        onChange={(e) => handleFilterChange('jobTitle', e.target.value)}
                    />
                    <Input
                        placeholder="Filter by applied date"
                        value={filters.appliedDate}
                        onChange={(e) => handleFilterChange('appliedDate', e.target.value)}
                    />
                    <Select
                        value={filters.status}
                        onValueChange={(value) => handleFilterChange('status', value)}
                    >
                        <SelectTrigger>
                            <SelectValue placeholder="Filter by status" />
                        </SelectTrigger>
                        <SelectContent>
                            <SelectItem value="ALL">All Statuses</SelectItem>
                            {applicationStatuses.map((status) => (
                                <SelectItem
                                    key={status.application_Status_id}
                                    value={status.application_Status_id.toString()}
                                >
                                    {status.application_Status_Name}
                                </SelectItem>
                            ))}
                        </SelectContent>
                    </Select>
                </div>

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
                        {filteredCandidates.length === 0 ? (
                            <TableRow>
                                <TableCell colSpan="6" className="text-center text-gray-500">
                                    No applications match the current filters.
                                </TableCell>
                            </TableRow>
                        ) : (
                            filteredCandidates.map((app) => (
                                <TableRow key={app.candidate_Application_Status_id}>
                                    <TableCell className='text-center'>{app.candidate_Details.candidate_name}</TableCell>
                                    <TableCell className='text-center'>{app.job.job_title}</TableCell>
                                    <TableCell className='text-center'>
                                        {new Date(app.applied_Date).toLocaleDateString()}
                                    </TableCell>
                                    <TableCell className='text-center'>
                                        <div className='flex justify-center'>
                                        <Select
                                            value={updatedApplications[app.application_id] || app.application_Status_id.toString()}
                                            onValueChange={(value) => handleStatusChange(app, value)}
                                        >
                                            <SelectTrigger className="w-[200px]">
                                                <SelectValue placeholder="Select status" />
                                            </SelectTrigger>
                                            <SelectContent>
                                                {applicationStatuses.map((status) => (
                                                    <SelectItem
                                                        key={status.application_Status_id}
                                                        value={status.application_Status_id.toString()}
                                                    >
                                                        {status.application_Status_Name}
                                                    </SelectItem>
                                                ))}
                                            </SelectContent>
                                        </Select>
                                        </div>
                                    </TableCell>
                                    <TableCell className="text-center">
                                        <a 
                                            onClick={() => handleOpenFile(app.candidate_Details.cV_Path)} 
                                            target="_blank" 
                                            rel="noopener noreferrer" 
                                            className="text-blue-500 hover:underline cursor-pointer"
                                        >
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
                                                        className="h-8 w-8 text-blue-500 hover:text-blue-600"
                                                        onClick={() => handleUpdate(app)}
                                                    >
                                                        <Save className="h-4 w-4" />
                                                    </Button>
                                                </TooltipTrigger>
                                                <TooltipContent>
                                                    <p>Update Candidate Application Status</p>
                                                </TooltipContent>
                                            </Tooltip>
                                        </TooltipProvider>
                                    </TableCell>
                                </TableRow>
                            ))
                        )}
                    </TableBody>
                </Table>
            </CardContent>
            <ToastContainer />
        </Card>
    );
};

export default CandidateApplicationStatus;