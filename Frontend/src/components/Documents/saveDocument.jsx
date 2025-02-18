import React, { useEffect, useState } from 'react';
import { Card, CardHeader, CardTitle, CardContent } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { getDocumentType, saveDocument } from '@/services/Document/api';
import { useSelector } from 'react-redux';
import { Label } from "@/components/ui/label";
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select";
import { getAllJobs } from '@/services/Jobs/api';
import { toast } from 'react-toastify';

const DocumentForm = () => {
    const [formData, setFormData] = useState({
        candidate_id: '',
        job_id: '',
        document_Type_id: '',
        Status: false,
        Approved_by: 0,
        Document_path: 'anything'
    });
    const [documentType, setDocumentType] = useState([]);
    const [jobs, setJobs] = useState([]);
    const [file, setFile] = useState(null);
    const [loading, setLoading] = useState(true);
    const data = useSelector((state) => state.Userdata);

    useEffect(() => {
        fetchData();
    }, []);

    const fetchData = async () => {
        try {
            setLoading(true);
            const [docTypes, jobsData] = await Promise.all([
                getDocumentType(data[0].token),
                getAllJobs(data[0].token)
            ]);

            if (!docTypes.data || !jobsData.data) {
                throw new Error('Failed to fetch data');
            }

            setDocumentType(docTypes.data);
            setJobs(jobsData.data);
        } catch (error) {
            toast.error(error.message || "Failed to fetch document types and jobs", {
                position: "top-right",
                autoClose: 3000
            });
        } finally {
            setLoading(false);
        }
    };

    const validateDocument = () => {
        if (!file) {
            toast.error("Please select a file", {
                position: "top-right",
                autoClose: 3000
            });
            return false;
        }

        if (!formData.document_Type_id) {
            toast.error("Please select document type", {
                position: "top-right",
                autoClose: 3000
            });
            return false;
        }

        if (!formData.job_id) {
            toast.error("Please select a job", {
                position: "top-right",
                autoClose: 3000
            });
            return false;
        }

        const maxSize = 5 * 1024 * 1024; 
        if (file.size > maxSize) {
            toast.error("File size should not exceed 5MB", {
                position: "top-right",
                autoClose: 3000
            });
            return false;
        }

        const allowedTypes = ['application/pdf', 'image/jpeg', 'image/png'];
        if (!allowedTypes.includes(file.type)) {
            toast.error("Only PDF, JPEG, and PNG files are allowed", {
                position: "top-right",
                autoClose: 3000
            });
            return false;
        }

        return true;
    };

    const handleFileChange = (e) => {
        const selectedFile = e.target.files[0];
        setFile(selectedFile);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (!validateDocument()) return;

            formData.candidate_id = data[0].user_id;
            formData.job_id = parseInt(formData.job_id);
            formData.document_Type_id = parseInt(formData.document_Type_id);

            const response = await saveDocument(data[0].token, formData, file);
            
            if (response.status === 200) {
                toast.success("Document uploaded successfully", {
                    position: "top-right",
                    autoClose: 3000
                });

                setFormData({
                    candidate_id: '',
                    job_id: '',
                    document_Type_id: '',
                    Status: false,
                    Approved_by: 0,
                    Document_path: 'anything'
                });
                setFile(null);
                
                const fileInput = document.querySelector('input[type="file"]');
                if (fileInput) fileInput.value = '';
            } else {
                throw new Error("Failed to upload document");
            }
        } catch (error) {
            console.error('Upload error:', error);
            toast.error(error.message || "Failed to upload document", {
                position: "top-right",
                autoClose: 3000
            });
        }
    };

    if (loading) {
        return <div className="flex justify-center items-center h-64">Loading...</div>;
    }

    return (
        <div className="max-w-2xl mx-auto">
            <Card>
                <CardHeader>
                    <CardTitle>Upload Document</CardTitle>
                </CardHeader>
                <CardContent>
                    <form className="space-y-4">
                        <div className="space-y-2">
                            <Label>Job</Label>
                            <Select
                                value={formData.job_id}
                                onValueChange={(value) => 
                                    setFormData(prev => ({ ...prev, job_id: value }))
                                }
                            >
                                <SelectTrigger>
                                    <SelectValue placeholder="Select job" />
                                </SelectTrigger>
                                <SelectContent>
                                    {jobs.map(job => (
                                        <SelectItem 
                                            key={job.job_id} 
                                            value={job.job_id.toString()}
                                        >
                                            {job.job_title}
                                        </SelectItem>
                                    ))}
                                </SelectContent>
                            </Select>
                        </div>

                        <div className="space-y-2">
                            <Label>Document Type</Label>
                            <Select
                                value={formData.document_Type_id}
                                onValueChange={(value) => 
                                    setFormData(prev => ({ ...prev, document_Type_id: value }))
                                }
                            >
                                <SelectTrigger>
                                    <SelectValue placeholder="Select document type" />
                                </SelectTrigger>
                                <SelectContent>
                                    {documentType.map(type => (
                                        <SelectItem 
                                            key={type.document_Type_id} 
                                            value={type.document_Type_id.toString()}
                                        >
                                            {type.document_name}
                                        </SelectItem>
                                    ))}
                                </SelectContent>
                            </Select>
                        </div>

                        <div className="space-y-2">
                            <Label>File</Label>
                            <Input
                                type="file"
                                onChange={handleFileChange}
                                accept=".pdf,.jpg,.jpeg,.png"
                            />
                        </div>

                        <Button onClick={handleSubmit} className="w-full">
                            Upload Document
                        </Button>
                    </form>
                </CardContent>
            </Card>
        </div>
    );
};

export default DocumentForm;