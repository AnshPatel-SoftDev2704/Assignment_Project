import { useState, useEffect } from 'react';
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import { Label } from "@/components/ui/label";
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select";
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Badge } from "@/components/ui/badge";
import { X } from "lucide-react";
import { useSelector, useDispatch } from 'react-redux';
import { saveInterview, saveInterviewer } from '@/services/Interview/api';
import { getAllInterviewType } from '@/services/Interview/api';
import { getAllInterviewStatus } from '@/services/Interview/api';
import { getAllUser } from '@/services/Users/api';
import { getAllApplications } from '@/services/Candidate/api';

const CreateInterview = () => {
    const [interviewData, setInterviewData] = useState({
        application_id: '',
        number_of_round: '',
        interview_Type_id: '',
        scheduled_at: '',
        interview_Status_id: '',
        special_Notes: '',
        interview_Meeting_Link: ''
    });

    const [interviewers, setInterviewers] = useState([]);
    const [selectedInterviewer, setSelectedInterviewer] = useState("");
    const [assignedInterviewers, setAssignedInterviewers] = useState([]);
    const [interviewTypes, setInterviewTypes] = useState([]);
    const [interviewStatuses, setInterviewStatuses] = useState([]);
    const [users, setUsers] = useState([]);
    const [applications,setApplications] = useState([])
    const data = useSelector((state) => state.Userdata);
    const dispatch = useDispatch();

    useEffect(() => {
        const fetchData = async () => {
            try {
                const typeResponse = await getAllInterviewType(data[0].token);
                const statusResponse = await getAllInterviewStatus(data[0].token);
                const usersResponse = await getAllUser(data[0].token);
                const applications = await getAllApplications(data[0].token)
                setInterviewTypes(typeResponse.data);
                setInterviewStatuses(statusResponse.data);
                setUsers(usersResponse.data);
                setApplications(applications.data)
                console.log(applications.data)
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        };
        fetchData();
    }, []);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setInterviewData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleAddInterviewer = () => {
        if (selectedInterviewer && !assignedInterviewers.find(user => user.user_id === parseInt(selectedInterviewer))) {
            const userToAdd = users.find(user => user.user_id === parseInt(selectedInterviewer));
            setAssignedInterviewers([...assignedInterviewers, userToAdd]);
        }
        setSelectedInterviewer("");
    };

    const handleRemoveInterviewer = (userId) => {
        setAssignedInterviewers(assignedInterviewers.filter(user => user.user_id !== userId));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            interviewData.application_id = parseInt(interviewData.application_id)
            interviewData.interview_Status_id = parseInt(interviewData.interview_Status_id)
            interviewData.interview_Type_id = parseInt(interviewData.interview_Type_id)
            interviewData.number_of_round = parseInt(interviewData.number_of_round)
            const response = await saveInterview(data[0].token,interviewData)
            console.log(response)
            assignedInterviewers.forEach(async interviewer => {
                const result = await saveInterviewer(data[0].token,response.data.interview_id,interviewer.user_id)
            });
            setInterviewData({
                application_id: '',
                number_of_round: '',
                interview_Type_id: '',
                scheduled_at: '',
                interview_Status_id: '',
                special_Notes: '',
                interview_Meeting_Link: ''
            });
            setAssignedInterviewers([]);
        } catch (error) {
            console.error("Error creating interview:", error);
        }
    };

    return (
        <Card>
            <CardHeader>
                <CardTitle>Schedule New Interview</CardTitle>
            </CardHeader>
            <CardContent>
                <form onSubmit={handleSubmit} className="space-y-4">
                <div className="space-y-2">
                        <Label>Candidate Application</Label>
                        <Select
                            value={interviewData.application_id}
                            onValueChange={(value) => 
                                setInterviewData(prev => ({ ...prev, application_id: value }))
                            }
                        >
                            <SelectTrigger>
                                <SelectValue placeholder="Select interview type" />
                            </SelectTrigger>
                            <SelectContent>
                                {applications.map(app => (
                                    <SelectItem 
                                        key={app.candidate_Application_Status_id} 
                                        value={app.candidate_Application_Status_id.toString()}
                                    >
                                        {app.candidate_Details.candidate_name}
                                    </SelectItem>
                                ))}
                            </SelectContent>
                        </Select>
                    </div>
                    <div className="space-y-2">
                        <Label>Round Number</Label>
                        <Input
                            type="number"
                            name="number_of_round"
                            value={interviewData.number_of_round}
                            onChange={handleInputChange}
                            required
                        />
                    </div>

                    <div className="space-y-2">
                        <Label>Interview Type</Label>
                        <Select
                            value={interviewData.interview_Type_id}
                            onValueChange={(value) => 
                                setInterviewData(prev => ({ ...prev, interview_Type_id: value }))
                            }
                        >
                            <SelectTrigger>
                                <SelectValue placeholder="Select interview type" />
                            </SelectTrigger>
                            <SelectContent>
                                {interviewTypes.map(type => (
                                    <SelectItem 
                                        key={type.interview_Type_id} 
                                        value={type.interview_Type_id.toString()}
                                    >
                                        {type.interview_Type_name}
                                    </SelectItem>
                                ))}
                            </SelectContent>
                        </Select>
                    </div>

                    <div className="space-y-2">
                        <Label>Schedule Date & Time</Label>
                        <Input
                            type="datetime-local"
                            name="scheduled_at"
                            value={interviewData.scheduled_at}
                            onChange={handleInputChange}
                            required
                        />
                    </div>

                    <div className="space-y-2">
                        <Label>Interview Status</Label>
                        <Select
                            value={interviewData.interview_Status_id}
                            onValueChange={(value) => 
                                setInterviewData(prev => ({ ...prev, interview_Status_id: value }))
                            }
                        >
                            <SelectTrigger>
                                <SelectValue placeholder="Select status" />
                            </SelectTrigger>
                            <SelectContent>
                                {interviewStatuses.map(status => (
                                    <SelectItem 
                                        key={status.interview_Status_id} 
                                        value={status.interview_Status_id.toString()}
                                    >
                                        {status.interview_Status_name}
                                    </SelectItem>
                                ))}
                            </SelectContent>
                        </Select>
                    </div>

                    <div className="space-y-2">
                        <Label>Meeting Link</Label>
                        <Input
                            name="interview_Meeting_Link"
                            value={interviewData.interview_Meeting_Link}
                            onChange={handleInputChange}
                            placeholder="Enter meeting link"
                        />
                    </div>

                    <div className="space-y-2">
                        <Label>Special Notes</Label>
                        <Textarea
                            name="special_Notes"
                            value={interviewData.special_Notes}
                            onChange={handleInputChange}
                            placeholder="Enter any special notes"
                        />
                    </div>

                    <div className="space-y-2">
                        <Label>Interviewers</Label>
                        <div className="flex flex-wrap gap-2 mb-2">
                            {assignedInterviewers.map((user) => (
                                <Badge 
                                    key={user.user_id} 
                                    variant="secondary"
                                    className="flex items-center gap-1"
                                >
                                    {user.name}
                                    <X
                                        className="h-3 w-3 cursor-pointer"
                                        onClick={() => handleRemoveInterviewer(user.user_id)}
                                    />
                                </Badge>
                            ))}
                        </div>
                        <div className="flex gap-2">
                            <Select
                                value={selectedInterviewer}
                                onValueChange={setSelectedInterviewer}
                            >
                                <SelectTrigger className="w-full">
                                    <SelectValue placeholder="Select interviewer" />
                                </SelectTrigger>
                                <SelectContent>
                                    {users.filter(user => 
                                        !assignedInterviewers.find(u => u.user_id === user.user_id)
                                    ).map((user) => (
                                        <SelectItem 
                                            key={user.user_id} 
                                            value={user.user_id.toString()}
                                        >
                                            {user.name}
                                        </SelectItem>
                                    ))}
                                </SelectContent>
                            </Select>
                            <Button 
                                type="button" 
                                variant="outline" 
                                onClick={handleAddInterviewer}
                                disabled={!selectedInterviewer}
                            >
                                Add
                            </Button>
                        </div>
                    </div>

                    <Button type="submit" className="w-full">
                        Schedule Interview
                    </Button>
                </form>
            </CardContent>
        </Card>
    );
};

export default CreateInterview;