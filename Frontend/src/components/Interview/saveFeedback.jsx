import { useState, useEffect } from 'react';
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import { Label } from "@/components/ui/label";
import {
    Card,
    CardHeader,
    CardFooter,
    CardContent,
} from "@/components/ui/card";
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
import { getAllInterview,saveFeedback } from '@/services/Interview/api';
import { getAllUser } from '@/services/Users/api';
import { toast } from 'react-toastify';
import { getAllPreferredSkill, getAllRequiredSkill } from '@/services/Jobs/api';

const AddFeedback = () => {
    const [feedbackData, setFeedbackData] = useState({
        interview_id: '',
        user_id: '',
        technology: '',
        rating: '',
        comments: '',
        submitted_date: '',
    });

    const [interviews, setInterviews] = useState([]);
    const [users, setUsers] = useState([]);
    const [skills,setSkills] = useState([])
    const [requiredSkills,setRequiredSkills] = useState([])
    const [preferredSkills,setPreferredSkills] = useState([])

    const data = useSelector((state) => state.Userdata);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const interviewResponse = await getAllInterview(data[0].token);
                const userResponse = await getAllUser(data[0].token);
                const requiredSkill = await getAllRequiredSkill(data[0].token)
                const preferredSkill = await getAllPreferredSkill(data[0].token)
                
                if(interviewResponse.status === 403 ||userResponse.status === 403)
                    throw new Error("You are Not Allowed to Perform this Action")
                setRequiredSkills(requiredSkill.data)
                setPreferredSkills(preferredSkill.data)
                setInterviews(interviewResponse.data);
                setUsers(userResponse.data);
            } catch (error) {
                toast.error(error.message || "Failed to Fetch Details");
            }
        };
        fetchData();
    }, []);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFeedbackData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleSkillChange = async () => {
        setSkills([])
        let job_id = interviews.filter(interview => interview.interview_id === parseInt(feedbackData.interview_id)).map(interview => interview.candidate_Application_Status.job.job_id)
        const required = requiredSkills.filter(skill => skill.job_id === job_id[0]).map(skill => skill.skill);
        const preferred = preferredSkills.filter(skill => skill.job_id === job_id[0]).map(skill => skill.skill)
        setSkills([...required,...preferred])
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            feedbackData.interview_id = parseInt(feedbackData.interview_id)
            feedbackData.user_id = parseInt(data[0].user_id)
            feedbackData.technology = parseInt(feedbackData.technology)
            feedbackData.rating = parseInt(feedbackData.rating)
            feedbackData.submitted_date = new Date().toISOString();
            const result = await saveFeedback(data[0].token, feedbackData);
            if(result.status === 403)
                throw new Error("You are Not allowed to Perform this action")
            setFeedbackData({
                interview_id: '',
                user_id: '',
                technology: '',
                rating: '',
                comments: '',
                submitted_date: '',
            });
        } catch (error) {
            toast.error(error.message || "Failed to Fetch Details");
        }
    };

    return (
        <Card className="max-w-xl mx-auto p-6 shadow-md bg-white">
            <CardHeader>
                <h2 className="text-xl font-semibold">Add Feedback</h2>
            </CardHeader>
            <CardContent>
                <form onSubmit={handleSubmit} className="space-y-4">
                    <div className="space-y-2">
                        <Label>Interview</Label>
                        <Select
                            value={feedbackData.interview_id}
                            onValueChange={(value) =>{
                                handleSkillChange()
                                setFeedbackData(prev => ({ ...prev, interview_id: value }))
                            }}
                        >
                            <SelectTrigger>
                                <SelectValue placeholder="Select Interview" />
                            </SelectTrigger>
                            <SelectContent>
                                {interviews.map(interview => (
                                    <SelectItem 
                                        key={interview.interview_id} 
                                        value={interview.interview_id.toString()}
                                    >
                                        Name : {interview.candidate_Application_Status.candidate_Details.candidate_name}<br/> 
                                        Interview Type : {interview.interview_Type.interview_Type_name}<br/>
                                        Round {interview.number_of_round} - {new Date(interview.scheduled_at).toLocaleString()}
                                    </SelectItem>
                                ))}
                            </SelectContent>
                        </Select>
                    </div>

                    <div className="space-y-2">
                        <Label>Technology</Label>
                        <Select
                            value={feedbackData.technology}
                            onValueChange={(value) => setFeedbackData({...feedbackData,technology:value})}
                        >
                            <SelectTrigger className="w-full">
                            <SelectValue placeholder="Select required skill" />
                            </SelectTrigger>
                            <SelectContent>
                            {skills.map(skill => 
                                <SelectItem 
                                key={skill.skill_id} 
                                value={skill.skill_id.toString()}
                                >
                                {skill.skill_name}
                                </SelectItem>
                            )}
                            </SelectContent>
                        </Select>
                    </div>

                    <div className="space-y-2">
                        <Label>Rating</Label>
                        <Input
                            type="number"
                            name="rating"
                            value={feedbackData.rating}
                            onChange={handleInputChange}
                            min="1"
                            max="5"
                            required
                        />
                    </div>

                    <div className="space-y-2">
                        <Label>Comments</Label>
                        <Textarea
                            name="comments"
                            value={feedbackData.comments}
                            onChange={handleInputChange}
                            placeholder="Enter your comments"
                        />
                    </div>
                </form>
            </CardContent>
            <CardFooter>
                <Button onClick={handleSubmit} className="w-full">
                    Submit Feedback
                </Button>
            </CardFooter>
        </Card>
    );
};

export default AddFeedback;