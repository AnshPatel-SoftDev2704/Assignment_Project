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
import { Pencil, Trash2 } from "lucide-react";
import { useEffect, useState } from 'react';
import { Button } from '@/components/ui/button';
import DeleteJob from './deleteJobs';
import UpdateJob from './updateJobs';
import { getAllJobs } from '@/services/Jobs/api';

const ShowJobs = () => {
    const [showEditDialog,setShowEditDialog] = useState(false);
    const [showDeleteDialog,setShowDeleteDialog] = useState(false)
    const [editJob,setEditJob] = useState({})
    const [deletejob,setDeleteJob] = useState({})
    const dispatch = useDispatch();
    const data = useSelector((state) => state.Userdata);
    const jobs = useSelector((state) => state.Jobs);
    useEffect(() => {
        const fetchData = async () => {
            const response = await getAllJobs(data[0].token);
            dispatch(getJobs(response.data))
        };
        fetchData();
    }, []);


    const handleEdit = async (job) => {
        setShowEditDialog(true);
        setEditJob(job);
    }

    const handleDelete = async (job) => {
        setShowDeleteDialog(true)
        setDeleteJob(job)
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
                        {jobs[0]&& jobs[0].map(job => (
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
                                                    className="h-8 w-8"
                                                    onClick={() => handleEdit(job)}
                                                >
                                                    <Pencil className="h-4 w-4" />
                                                </Button>
                                            </TooltipTrigger>
                                            <TooltipContent>
                                                <p>Edit user</p>
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
                                                <p>Delete user</p>
                                            </TooltipContent>
                                        </Tooltip>
                                    </TooltipProvider>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </div>
            {showEditDialog && <UpdateJob showEditDialog={showEditDialog} setShowEditDialog={setShowEditDialog} editJob={editJob}/>}
            {showDeleteDialog && <DeleteJob showDeleteDialog = {showDeleteDialog} setShowDeleteDialog={setShowDeleteDialog} deleteJobs = {deletejob}/>}
        </>
    );
}

export default ShowJobs;