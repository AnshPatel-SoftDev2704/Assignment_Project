import React, { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { getAllDocumentSubmitted, getFile, updateSubmittedDocument } from '@/services/Document/api';
import { toast } from 'react-toastify';
import { Check, X } from "lucide-react";
import {
    Tooltip,
    TooltipContent,
    TooltipProvider,
    TooltipTrigger,
} from "@/components/ui/tooltip";

const ShowDocuments = () => {
    const [documents, setDocuments] = useState([]);
    const [loading, setLoading] = useState(true);
    const data = useSelector((state) => state.Userdata);

    useEffect(() => {
        validateAndFetchDocuments();
    }, []);

    const validateToken = () => {
        if (!data || !data[0]?.token) {
            toast.error("Authentication token is missing", {
                position: "top-right",
                autoClose: 3000,
                hideProgressBar: false,
            });
            return false;
        }
        return true;
    };

    const validateAndFetchDocuments = async () => {
        if (!validateToken()) return;
        await fetchDocuments();
    };

    const fetchDocuments = async () => {
        try {
            setLoading(true);
            const response = await getAllDocumentSubmitted(data[0].token);
            if (!response.data || response.data.length === 0) {
                throw (response)
            }
            setDocuments(response.data);
        } catch (error) {
            if(error.status === 403)
            {
                toast.error("You don't have Access for this Action", {
                position: "top-right",
                autoClose: 3000,
                hideProgressBar: false,
                });
            }
            else
            {
                toast.error(error.message || "Failed to fetch documents", {
                position: "top-right",
                autoClose: 3000,
                hideProgressBar: false,
                });
            }
        } finally {
            setLoading(false);
        }
    };

    const handleOpenFile = async (filepath) => {
        try {
            if (!validateToken()) return;

            if (!filepath) {
                toast.error("Document path is missing", {
                    position: "top-right",
                    autoClose: 3000,
                    hideProgressBar: false,
                });
                return;
            }

            const response = await getFile(data[0].token, filepath);
            if (response.status === 200) {
                window.open(`http://localhost:5195/api/Document_Submitted/GetFile?filepath=${encodeURI(filepath)}`, '_blank');
            } else {
                throw new Error("Failed to retrieve document");
            }
        } catch (error) {
            toast.error(error.message || "Failed to open document", {
                position: "top-right",
                autoClose: 3000,
                hideProgressBar: false,
            });
        }
    };

    const handleApprove = async (documentId, status) => {
        try {
            if (!validateToken()) return;

            if (!documentId) {
                toast.error("Invalid document selection", {
                    position: "top-right",
                    autoClose: 3000,
                    hideProgressBar: false,
                });
                return;
            }

            const response = await updateSubmittedDocument(data[0].token, documentId, {
                Status: status,
                Approved_by: data[0].user_id
            });
            console.log(response.data)
            if (response.status === 200) {
                toast.success(`Document ${status ? 'approved' : 'rejected'} successfully`, {
                    position: "top-right",
                    autoClose: 3000,
                    hideProgressBar: false,
                });
                await fetchDocuments();
            } else {
                throw new Error(`Failed to ${status ? 'approve' : 'reject'} document`);
            }
        } catch (error) {
            toast.error(error.message || `Failed to ${status ? 'approve' : 'reject'} document`, {
                position: "top-right",
                autoClose: 3000,
                hideProgressBar: false,
            });
        }
    };

    if (loading) {
        return (
            <div className="flex justify-center items-center h-64">
                <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-gray-900"></div>
                <span className="ml-2">Loading documents...</span>
            </div>
        );
    }

    return (
        <Card>
            <CardHeader>
                <CardTitle>Submitted Documents</CardTitle>
            </CardHeader>
            <CardContent>
                {documents.length === 0 ? (
                    <div className="text-center py-4 text-gray-500">
                        No documents found
                    </div>
                ) : (
                    <Table>
                        <TableHeader>
                            <TableRow>
                                <TableHead className="text-center">Candidate</TableHead>
                                <TableHead className="text-center">Job</TableHead>
                                <TableHead className="text-center">Document Type</TableHead>
                                <TableHead className="text-center">Status</TableHead>
                                <TableHead className="text-center">Document</TableHead>
                                <TableHead className="text-center">Actions</TableHead>
                            </TableRow>
                        </TableHeader>
                        <TableBody>
                            {documents.map((doc) => (
                                <TableRow key={doc.document_id}>
                                    <TableCell className="text-center">
                                        {doc.candidate?.candidate_name || 'N/A'}
                                    </TableCell>
                                    <TableCell className="text-center">
                                        {doc.job?.job_title || 'N/A'}
                                    </TableCell>
                                    <TableCell className="text-center">
                                        {doc.document_Type?.document_name || 'N/A'}
                                    </TableCell>
                                    <TableCell className="text-center">
                                        <span className={`px-2 py-1 rounded-full text-sm ${
                                            doc.status ? 'bg-green-100 text-green-800' : 'bg-yellow-100 text-yellow-800'
                                        }`}>
                                            {doc.status ? 'Approved' : 'Rejected'}
                                        </span>
                                    </TableCell>
                                    <TableCell className="text-center">
                                        <Button
                                            variant="link"
                                            onClick={() => handleOpenFile(doc.document_path)}
                                            disabled={!doc.document_path}
                                        >
                                            View Document
                                        </Button>
                                    </TableCell>
                                    <TableCell className="text-center">
                                        <div className="flex justify-center space-x-2">
                                            {doc.created_at === doc.updated_at && (
                                                <>
                                                    <TooltipProvider>
                                                        <Tooltip>
                                                            <TooltipTrigger asChild>
                                                                <Button
                                                                    variant="ghost"
                                                                    size="icon"
                                                                    className="h-8 w-8 text-green-500 hover:text-green-600"
                                                                    onClick={() => handleApprove(doc.document_Submitted_id, true)}
                                                                >
                                                                    <Check className="h-4 w-4" />
                                                                </Button>
                                                            </TooltipTrigger>
                                                            <TooltipContent>
                                                                <p>Approve Document</p>
                                                            </TooltipContent>
                                                        </Tooltip>
                                                    </TooltipProvider>

                                                    <TooltipProvider>
                                                        <Tooltip>
                                                            <TooltipTrigger asChild>
                                                                <Button
                                                                    variant="ghost"
                                                                    size="icon"
                                                                    className="h-8 w-8 text-red-500 hover:text-red-600"
                                                                    onClick={() => handleApprove(doc.document_Submitted_id, false)}
                                                                >
                                                                    <X className="h-4 w-4" />
                                                                </Button>
                                                            </TooltipTrigger>
                                                            <TooltipContent>
                                                                <p>Reject Document</p>
                                                            </TooltipContent>
                                                        </Tooltip>
                                                    </TooltipProvider>
                                                </>
                                            )}
                                        </div>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                )}
            </CardContent>
        </Card>
    );
};

export default ShowDocuments;