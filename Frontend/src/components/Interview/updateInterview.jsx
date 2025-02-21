import { useState, useEffect } from 'react';
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import { Label } from "@/components/ui/label";
import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogFooter,
} from "@/components/ui/dialog";
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select";
import { Badge } from "@/components/ui/badge";
import { X } from "lucide-react";
import { useSelector, useDispatch } from 'react-redux';
import { updateInterview, getAllInterviewer } from '@/services/Interview/api';
import { getAllInterviewType } from '@/services/Interview/api';
import { getAllInterviewStatus, saveInterviewer } from '@/services/Interview/api';
import { getAllUser } from '@/services/Users/api';

const UpdateInterview = ({ showEditDialog, setShowEditDialog, editInterview }) => {
    const [editFormData, setEditFormData] = useState({
        interview_id: editInterview?.interview_id,
        application_id: editInterview?.application_id,
        number_of_round: editInterview?.number_of_round || '',
        interview_Type_id: editInterview?.interview_Type_id || '',
        scheduled_at: editInterview?.scheduled_at ? new Date(editInterview.scheduled_at).toISOString().slice(0, 16) : '',
        interview_Status_id: editInterview?.interview_Status_id || '',
        special_Notes: editInterview?.special_Notes || '',
        interview_Meeting_Link: editInterview?.interview_Meeting_Link || ''
    });

    const [interviewTypes, setInterviewTypes] = useState([]);
    const [interviewStatuses, setInterviewStatuses] = useState([]);
    const [users, setUsers] = useState([]);
    const [assignedInterviewers, setAssignedInterviewers] = useState([]);
    const [updatedInterviewers,setUpdatedInterviewers] = useState([])
    const [selectedInterviewer, setSelectedInterviewer] = useState("");

    const data = useSelector((state) => state.Userdata);
    const dispatch = useDispatch();

    useEffect(() => {
        const fetchData = async () => {
            try {
                const typeResponse = await getAllInterviewType(data[0].token);
                const statusResponse = await getAllInterviewStatus(data[0].token);
                const usersResponse = await getAllUser(data[0].token);
                const interviewers = await getAllInterviewer(data[0].token)
                const filteredInterviewers = interviewers.data.filter(interviewer => interviewer.interview_id === editInterview.interview_id).map(interviewer => interviewer.user)
                setAssignedInterviewers(filteredInterviewers)
                setInterviewTypes(typeResponse.data);
                setInterviewStatuses(statusResponse.data);
                setUsers(usersResponse.data);
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        };
        fetchData();
    }, []);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setEditFormData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleAddInterviewer = () => {
        if (selectedInterviewer && !assignedInterviewers.find(user => user.user_id === parseInt(selectedInterviewer))) {
            const userToAdd = users.find(user => user.user_id === parseInt(selectedInterviewer));
            setAssignedInterviewers([...assignedInterviewers, userToAdd]);
            setUpdatedInterviewers([...updatedInterviewers,userToAdd])
        }
        setSelectedInterviewer("");
    };

    const handleRemoveInterviewer = (userId) => {
        setAssignedInterviewers(assignedInterviewers.filter(user => user.user_id !== userId));
        setUpdatedInterviewers(updatedInterviewers.filter(interviewer => user.user_id !== userId))
    };

    const handleUpdate = async () => {
        try {
             if(editFormData.number_of_round <= 0)
                toast.error("Enter Correct Interview Round Number")
            const response = await updateInterview(data[0].token,editFormData.interview_id,editFormData)
            updatedInterviewers.forEach(interviewer => {
                const result = saveInterviewer(data[0].token,response.data.interview_id,interviewer.user_id)
            });
            setShowEditDialog(false);
            setAssignedInterviewers([])
            setUpdatedInterviewers([])
        } catch (err) {
            console.error('Error updating interview:', err);
            setUpdatedInterviewers([])
        }
    };

    return (
        <Dialog open={showEditDialog} onOpenChange={setShowEditDialog}>
            <DialogContent className="max-w-md">
                <DialogHeader>
                    <DialogTitle>Edit Interview</DialogTitle>
                </DialogHeader>
                
                <div className="space-y-4">
                    <div className="space-y-2">
                        <Label>Round Number</Label>
                        <Input
                            type="number"
                            name="number_of_round"
                            value={editFormData.number_of_round}
                            onChange={handleInputChange}
                            required
                        />
                    </div>

                    <div className="space-y-2">
                        <Label>Interview Type</Label>
                        <Select
                            value={editFormData.interview_Type_id}
                            onValueChange={(value) => 
                                setEditFormData(prev => ({ ...prev, interview_Type_id: value }))
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
                            value={editFormData.scheduled_at}
                            onChange={handleInputChange}
                            required
                        />
                    </div>

                    <div className="space-y-2">
                        <Label>Interview Status</Label>
                        <Select
                            value={editFormData.interview_Status_id}
                            onValueChange={(value) => 
                                setEditFormData(prev => ({ ...prev, interview_Status_id: value }))
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
                            value={editFormData.interview_Meeting_Link}
                            onChange={handleInputChange}
                            placeholder="Enter meeting link"
                        />
                    </div>

                    <div className="space-y-2">
                        <Label>Special Notes</Label>
                        <Textarea
                            name="special_Notes"
                            value={editFormData.special_Notes}
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
                </div>

                <DialogFooter>
                    <Button variant="outline" onClick={() => setShowEditDialog(false)}>
                        Cancel
                    </Button>
                    <Button onClick={handleUpdate}>
                        Save Changes
                    </Button>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    );
};

export default UpdateInterview; 