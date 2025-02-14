import React, { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from "@/components/ui/tooltip";
import { Button } from '@/components/ui/button';
import { Save, Trash2 } from "lucide-react";
import { getAllCandidates, getFile, updateApplicationStatus } from '@/services/Candidate/api';
import { getAllDocumentSubmitted, updateSubmittedDocument } from '@/services/Document/api';
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select";
import DeleteDocument from './deleteDocument';
const ShowDocuments = () => {
    const [showDeleteDialog, setShowDeleteDialog] = useState(false);
    const [documents, setDocuments] = useState([]);
    const [deleteDocument,setDeleteDocument] = useState()
    const [updatedDocumentStatus, setUpdatedDocumentStatus] = useState([])
    const data = useSelector((state) => state.Userdata);

    useEffect(() => {
        const fetchData = async () => {
            const response = await getAllDocumentSubmitted(data[0].token)
            setDocuments(response.data)
            console.log(response.data)
        };
        fetchData();
    }, [showDeleteDialog, setShowDeleteDialog]);

    const handleDelete = (document) => {
        setShowDeleteDialog(true);
        setDeleteDocument(document);
    };

    const handleOpenFile = async (filepath) => {
        try {
            const response = await getFile(data[0].token, filepath);
            if (response.status === 200) {
                window.open(`http://localhost:5195/api/Document_Submitted/GetFile?filepath=${encodeURIComponent(filepath)}`, '_blank');
            }
        } catch (error) {
            console.error(error);
        }
    };

    const handleStatusChange = (documentId, newStatus) => {
        setUpdatedDocumentStatus(prevState => ({
            ...prevState,
            [documentId]: newStatus
        }));
    };

    const handleSave = async (document) => {
        try {
            const newStatus = updatedDocumentStatus[document.document_Submitted_id];
            if (newStatus) {
                await updateSubmittedDocument(data[0].token, document.document_Submitted_id, newStatus,data[0].user_id);
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
                        <TableHead className='text-center'>See Document</TableHead>
                        <TableHead className='text-center'>Actions</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {documents && documents.map(document => (
                        <TableRow key={document.document_Submitted_id}>
                            <TableCell className='text-center'>{document.candidate.candidate_name}</TableCell>
                            <TableCell className='text-center'>{document.job.job_title}</TableCell>
                            <TableCell className='text-center'>
                                {new Date(document.submitted_date).toLocaleDateString()}
                            </TableCell>
                            <TableCell className='text-center'>
                                <div className="flex justify-center items-center">
                                    <Select
                                        value={updatedDocumentStatus[document.document_Submitted_id]}
                                        onValueChange={(value) => handleStatusChange(document.document_Submitted_id, value)}
                                    >
                                        <SelectTrigger className="w-[200px]">
                                            <SelectValue placeholder="Select status" />
                                        </SelectTrigger>
                                        <SelectContent>
                                            <SelectItem
                                                key="true"
                                                value={true}
                                            >
                                                Accepted
                                            </SelectItem>
                                            <SelectItem
                                                key="false"
                                                value={false}
                                            >
                                                Rejected
                                            </SelectItem>
                                        </SelectContent>
                                    </Select>
                                </div>
                            </TableCell>
                            <TableCell className="text-center">
                                <a onClick={() => handleOpenFile(document.candidate.cV_Path)} target="_blank" rel="noopener noreferrer" className="text-blue-500 hover:underline">
                                    See Document
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
                                                onClick={() => handleDelete(document)}
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
                                                onClick={() => handleSave(document)}
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
            {showDeleteDialog && <DeleteDocument showDeleteDialog={showDeleteDialog} setShowDeleteDialog={setShowDeleteDialog} deleteDocument={deleteDocument} />}
        </div>
    );
};

export default ShowDocuments;
