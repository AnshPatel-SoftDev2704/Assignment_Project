import { useEffect, useState } from 'react';
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
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select";
import {
    Tooltip,
    TooltipContent,
    TooltipProvider,
    TooltipTrigger,
} from "@/components/ui/tooltip";
import { Button } from '../ui/button';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { getAllApplications, getAllApplicationStatuses, updateApplicationStatus } from '@/services/Candidate/api';
import { Save } from 'lucide-react';

const CandidateApplicationStatus = () => {
    const data = useSelector((state) => state.Userdata);
    const dispatch = useDispatch();

    const [applications, setApplications] = useState([]);
    const [applicationStatuses, setApplicationStatuses] = useState([]);
    const [updatedApplications, setUpdatedApplications] = useState({
        Candidate_id : '',
        Job_id : '',
        application_Status_id : '',
        applied_Date : ''
    });

    useEffect(() => {
        const fetchData = async () => {
            const response = await getAllApplicationStatuses(data[0].token);
            setApplicationStatuses(response.data);
            const apps = await getAllApplications(data[0].token);
            setApplications(apps.data);
        };
        fetchData();
    }, []);

    const handleStatusChange = async (app, newStatus) => {
            setUpdatedApplications({
                Candidate_id : app.candidate_id,
                Job_id : app.job_id,
                application_Status_id : newStatus,
                applied_Date : app.applied_Date
        });
    };

    const handleUpdate = async(app) => {
        try{
            const response = await updateApplicationStatus(data[0].token, app.candidate_Application_Status_id, updatedApplications);
            const apps = await getAllApplications(data[0].token);
            setApplications(apps.data);
            setUpdatedApplications({
                Candidate_id : '',
                Job_id : '',
                application_Status_id : '',
                applied_Date : ''})
        }
        catch(error){
            console.error("Error updating application status:", error);
        }
    }

    return (
        <Card>
            <CardHeader>
                <CardTitle>Candidate Applications</CardTitle>
            </CardHeader>
            <CardContent>
                <Table>
                    <TableHeader>
                        <TableRow>
                            <TableHead className='text-center'>Candidate</TableHead>
                            <TableHead className='text-center'>Job Title</TableHead>
                            <TableHead className='text-center'>Applied Date</TableHead>
                            <TableHead className='text-center'>Status</TableHead>
                            <TableHead className='text-center'>Actions</TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>
                        {applications.map((app) => (
                            <TableRow key={app.application_id}>
                                <TableCell className='text-center'>{app.candidate_Details.candidate_name}</TableCell>
                                <TableCell className='text-center'>{app.job.job_title}</TableCell>
                                <TableCell className='text-center'>
                                    {new Date(app.applied_Date).toLocaleDateString()}
                                </TableCell>
                                <TableCell className='text-center'>
                                    <Select
                                        value={updatedApplications[app.application_id] || app.application_Status_id}
                                        onValueChange={(value) => handleStatusChange(app, value)}
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
                        ))}
                    </TableBody>
                </Table>
            </CardContent>
        </Card>
    );
};

export default CandidateApplicationStatus;
