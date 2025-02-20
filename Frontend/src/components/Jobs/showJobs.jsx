import { getJobs } from '@/store/Jobs/actions';
import { useSelector, useDispatch } from 'react-redux';
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table";
import {
    Tooltip,
    TooltipContent,
    TooltipProvider,
    TooltipTrigger,
} from "@/components/ui/tooltip";
import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogFooter,
    DialogDescription,
  } from "@/components/ui/dialog";
import { Pencil, Trash2, Eye} from "lucide-react";
import { useEffect, useState } from 'react';
import { Button } from '@/components/ui/button';
import DeleteJob from './deleteJobs';
import UpdateJob from './updateJobs';
import { getAllJobs, getAllPreferredSkill, getAllRequiredSkill } from '@/services/Jobs/api';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const ShowJobs = () => {
    const [showEditDialog, setShowEditDialog] = useState(false);
    const [showDeleteDialog, setShowDeleteDialog] = useState(false);
    const [editJob, setEditJob] = useState({});
    const [deletejob, setDeleteJob] = useState({});
    const [showDetailsDialog,setShowDetailsDialog] = useState(false)
    const [selectedJob,setSelectedJob] = useState({})
    const [requiredSkills,setRequiredSkills] = useState([])
    const [preferredSkills,setPreferredSkills] = useState([])
    const dispatch = useDispatch();
    const data = useSelector((state) => state.Userdata);
    const [jobs,setJobs] = useState([])

    useEffect(() => {
        const fetchData = async () => {
            try {
                if (!data?.[0]?.token) {
                    toast.error('Authentication required to fetch jobs');
                    return;
                }

                const response = await getAllJobs(data[0].token);
                const requiredSkill = await getAllRequiredSkill(data[0].token)
                const preferredSkill = await getAllPreferredSkill(data[0].token)
                setRequiredSkills(requiredSkill.data)
                setPreferredSkills(preferredSkill.data)
                if (response.data) {
                    setJobs(response.data)
                    toast.success({
                        render: 'Jobs loaded successfully',
                        isLoading: false,
                        autoClose: 2000
                    });
                }
            } catch (error) {
                toast.error(error.response?.data?.message || 'Failed to fetch jobs');
            }
        };
        fetchData();
    }, [showEditDialog,showDeleteDialog,setShowDeleteDialog,setShowEditDialog]);

    const handleEdit = async (job) => {
        if (!job?.job_id) {
            toast.error('Invalid job selected for editing');
            return;
        }
        setShowEditDialog(true);
        setEditJob(job);
        toast.info(`Editing job: ${job.job_title}`);
    }

    const handleDelete = async (job) => {
        if (!job?.job_id) {
            toast.error('Invalid job selected for deletion');
            return;
        }
        setShowDeleteDialog(true);
        setDeleteJob(job);
        toast.warning(`Please confirm deletion of: ${job.job_title}`);
    }

    const handleShowDetails = async (job) => {
        setShowDetailsDialog(true)
        setSelectedJob(job)
    }


    return (
        <>
            <div className='rounded-md border'>
                <Table>
                    <TableHeader>
                        <TableRow>
                            <TableHead className='text-center'>Title</TableHead>
                            <TableHead className='text-center'>Description</TableHead>
                            <TableHead className='text-center'>Status</TableHead>
                            <TableHead className='text-center'>Created By</TableHead>
                            <TableHead className='text-center'>Created_at</TableHead>
                            <TableHead className='text-center'>Updated_at</TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>
                        {jobs && jobs.map(job => (
                            <TableRow key={job.job_id}>
                                <TableCell className='text-center'>{job.job_title}</TableCell>
                                <TableCell className='text-center'>{job.job_description}</TableCell>
                                <TableCell className='text-center'>{job.job_Status.job_Status_name}</TableCell>
                                <TableCell className='text-center'>{job.user.name}</TableCell>
                                <TableCell className='text-center'>{new Date(job.created_at).toLocaleDateString()}</TableCell>
                                <TableCell className='text-center'>{new Date(job.updated_at).toLocaleDateString()}</TableCell>
                                <TableCell className="text-center">
                                    <TooltipProvider>
                                        <Tooltip>
                                            <TooltipTrigger asChild>
                                            <Button
                                                variant="ghost"
                                                size="icon"
                                                className="h-8 w-8 mr-2"
                                                onClick={() => handleShowDetails(job)}
                                            >
                                                <Eye className="h-4 w-4" />
                                            </Button>
                                            </TooltipTrigger>
                                            <TooltipContent>
                                            <p>View details</p>
                                            </TooltipContent>
                                        </Tooltip>
                                        <Tooltip>
                                            <TooltipTrigger asChild>
                                                <Button
                                                    variant="ghost"
                                                    size="icon"
                                                    className="h-8 w-8"
                                                    onClick={() => handleEdit(job)}
                                                >
                                                    <Pencil className="h-4 w-4" />
                                                </Button>
                                            </TooltipTrigger>
                                            <TooltipContent>
                                                <p>Edit job</p>
                                            </TooltipContent>
                                        </Tooltip>
                                        <Tooltip>
                                            <TooltipTrigger asChild>
                                                <Button
                                                    variant="ghost"
                                                    size="icon"
                                                    className="h-8 w-8 text-red-500 hover:text-red-600"
                                                    onClick={() => handleDelete(job)}
                                                >
                                                    <Trash2 className="h-4 w-4" />
                                                </Button>
                                            </TooltipTrigger>
                                            <TooltipContent>
                                                <p>Delete job</p>
                                            </TooltipContent>
                                        </Tooltip>
                                    </TooltipProvider>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </div>
            <Dialog open={showDetailsDialog} onOpenChange={setShowDetailsDialog}>
                <DialogContent className="max-w-2xl">
                <DialogHeader>
                    <DialogTitle>Job Details</DialogTitle>
                </DialogHeader>
                {selectedJob && (
                    <div className='space-y-6'>
                        <div className='grid grid-cols-2 gap-6'>
                            <div className='space-y-4'>
                                <div>
                                    <h3 className='text-lg font-semibold mb-3'>Job Information</h3>
                                    <div className='space-y-2'>
                                        <p><span className='font-medium'>Title: </span>{selectedJob.job_title}</p>
                                        <p><span className='font-medium'>Decription: </span>{selectedJob.job_description}</p>
                                        <p><span className='font-medium'>Id: </span>{selectedJob.job_id}</p>
                                        <p><span className='font-medium'>Closed Reason: </span>{selectedJob.job_Closed_reason}</p>
                                        <p><span className='font-medium'>Selected Candidate Id: </span>{selectedJob.job_Selected_Candidate_id}</p>
                                    </div>
                                </div>
                                <div className='space-y-4'>
                                    <div>
                                    <h3 className="font-semibold mb-2">Required Skills:</h3>
                                    <ul className="list-disc list-inside space-y-2">
                                        {requiredSkills.map(skill => skill.job_id === selectedJob.job_id && (<li>{skill.skill.skill_name}</li>))}
                                    </ul>
                                    </div>
                                    <div>
                                    <h3 className="font-semibold mb-2">Preferred Skills:</h3>
                                    <ul className="list-disc list-inside space-y-2">
                                        {preferredSkills.map(skill =>skill.job_id === selectedJob.job_id && (<li>{skill.skill.skill_name}</li>))}
                                    </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                )}
                </DialogContent>
            </Dialog>
            {showEditDialog && <UpdateJob showEditDialog={showEditDialog} setShowEditDialog={setShowEditDialog} editJob={editJob} />}
            {showDeleteDialog && <DeleteJob showDeleteDialog={showDeleteDialog} setShowDeleteDialog={setShowDeleteDialog} deleteJobs={deletejob} />}
        </>
    );
}

export default ShowJobs;