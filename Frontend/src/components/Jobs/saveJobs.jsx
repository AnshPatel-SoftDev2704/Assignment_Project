import { useEffect, useState } from 'react';
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
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
import { getAllJobStatus, saveJobs, savePreferredSkill, saveRequiredSkill } from '@/services/Jobs/api';
import { getAllSkill } from '@/services/Skills/api';
import { toast } from 'react-toastify';

const CreateJob = () => {
  const data = useSelector((state) => state.Userdata);
  const dispatch = useDispatch();

  const [jobData, setJobData] = useState({
    Job_title: '',
    Job_description: '',
    Job_Status_id: '',
    Job_Closed_reason: '',
    Job_Selected_Candidate_id: 0,
    Created_by: data[0]?.user_id || 0
  });
  const [jobStatuses, setJobStatuses] = useState([]);
  const [skills, setSkills] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const jobStatus = await getAllJobStatus(data[0].token);
      setJobStatuses(jobStatus.data);
      const skill = await getAllSkill(data[0].token);
      setSkills(skill.data);
    };
    fetchData();
  }, []);

  const [required_skills, setRequired_Skills] = useState([]);
  const [preferred_skills, setPreferred_Skills] = useState([]);

  const [selectedRequiredSkill, setSelectedRequiredSkill] = useState("");
  const [selectedPreferredSkill, setSelectedPreferredSkill] = useState("");

  const handleAddRequiredSkill = () => {
    if (selectedRequiredSkill && !required_skills.find(skill => skill.skill_id === parseInt(selectedRequiredSkill))) {
      const skillToAdd = skills.find(skill => skill.skill_id === parseInt(selectedRequiredSkill));
      setRequired_Skills([...required_skills, skillToAdd]);
    }
    setSelectedRequiredSkill("");
  };

  const handleAddPreferredSkill = () => {
    if (selectedPreferredSkill && !preferred_skills.find(skill => skill.skill_id === parseInt(selectedPreferredSkill))) {
      const skillToAdd = skills.find(skill => skill.skill_id === parseInt(selectedPreferredSkill));
      setPreferred_Skills([...preferred_skills, skillToAdd]);
    }
    setSelectedPreferredSkill("");
  };

  const handleRemoveRequiredSkill = (skillId) => {
    setRequired_Skills(required_skills.filter(skill => skill.skill_id !== skillId));
  };

  const handleRemovePreferredSkill = (skillId) => {
    setPreferred_Skills(preferred_skills.filter(skill => skill.skill_id !== skillId));
  };

  const validateJobData = () => {
    if (!jobData.Job_title.trim()) {
      toast.error("Job title is required");
      return false;
    }
    if (!jobData.Job_description.trim()) {
      toast.error("Job description is required");
      return false;
    }
    if (!jobData.Job_Status_id) {
      toast.error("Job status is required");
      return false;
    } 
    if (!jobData.Created_by) {
      toast.error("Created by is required");
      return false;
    }
    return true;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (!validateJobData()) return;

      const result = await saveJobs(data[0].token, jobData);

      if(result.status === 403)
        throw new Error("You Don't have Permission to Perform this Action")
      
      required_skills.forEach(skill => {
        const response = saveRequiredSkill(data[0].token, result.data.job_id, skill.skill_id)
      });

      preferred_skills.forEach(skill => {
        const response = savePreferredSkill(data[0].token, result.data.job_id, skill.skill_id)
      });
      setJobData({
        Job_title: '',
        Job_description: '',
        Job_Status_id: '',
        Job_Closed_reason: '',
        Job_Selected_Candidate_id: 0,
        Created_by: data[0]?.user_id || 0,
      });
      setRequired_Skills([])
      setPreferred_Skills([])
      toast.success("Job posted successfully");
    } catch (error) {
      toast.error(error.message || "Failed to create job posting");
    }
  };

  return (
    <Card>
      <CardHeader>
        <CardTitle>Create New Job</CardTitle>
      </CardHeader>
      <CardContent>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div className="space-y-2">
            <Input
              placeholder="Job Title"
              value={jobData.Job_title}
              onChange={(e) => setJobData({ ...jobData, Job_title: e.target.value })}
            />
          </div>

          <div className="space-y-2">
            <Textarea
              placeholder="Job Description"
              value={jobData.Job_description}
              onChange={(e) => setJobData({ ...jobData, Job_description: e.target.value })}
              className="min-h-[100px]"
            />
          </div>

          <div className="space-y-2">
            <Select
              value={jobData.Job_Status_id.toString()}
              onValueChange={(value) => setJobData({ ...jobData, Job_Status_id: parseInt(value) })}
            >
              <SelectTrigger>
                <SelectValue placeholder="Select Status" />
              </SelectTrigger>
              <SelectContent>
                {jobStatuses.map((status) => status.job_Status_id == 1 && (
                  <SelectItem 
                    key={status.job_Status_id} 
                    value={status.job_Status_id.toString()}
                  >
                    {status.job_Status_name}
                  </SelectItem>
                ))}
              </SelectContent>
            </Select>
          </div>

          <div className="space-y-2">
            <h3 className="text-sm font-medium">Required Skills</h3>
            <div className="flex flex-wrap gap-2 mb-2">
              {required_skills.map((skill) => (
                <Badge 
                  key={skill.skill_id} 
                  variant="secondary"
                  className="flex items-center gap-1"
                >
                  {skill.skill_name}
                  <X
                    className="h-3 w-3 cursor-pointer"
                    onClick={() => handleRemoveRequiredSkill(skill.skill_id)}
                  />
                </Badge>
              ))}
            </div>
            <div className="flex gap-2">
              <Select
                value={selectedRequiredSkill}
                onValueChange={setSelectedRequiredSkill}
              >
                <SelectTrigger className="w-full">
                  <SelectValue placeholder="Select required skill" />
                </SelectTrigger>
                <SelectContent>
                  {skills.filter(skill => 
                    !required_skills.find(s => s.skill_id === skill.skill_id)
                  ).map((skill) => (
                    <SelectItem 
                      key={skill.skill_id} 
                      value={skill.skill_id.toString()}
                    >
                      {skill.skill_name}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
              <Button 
                type="button" 
                variant="outline" 
                onClick={handleAddRequiredSkill}
                disabled={!selectedRequiredSkill}
              >
                Add
              </Button>
            </div>
          </div>

          <div className="space-y-2">
            <h3 className="text-sm font-medium">Preferred Skills</h3>
            <div className="flex flex-wrap gap-2 mb-2">
              {preferred_skills.map((skill) => (
                <Badge 
                  key={skill.skill_id} 
                  variant="secondary"
                  className="flex items-center gap-1"
                >
                  {skill.skill_name}
                  <X
                    className="h-3 w-3 cursor-pointer"
                    onClick={() => handleRemovePreferredSkill(skill.skill_id)}
                  />
                </Badge>
              ))}
            </div>
            <div className="flex gap-2">
              <Select
                value={selectedPreferredSkill}
                onValueChange={setSelectedPreferredSkill}
              >
                <SelectTrigger className="w-full">
                  <SelectValue placeholder="Select preferred skill" />
                </SelectTrigger>
                <SelectContent>
                  {skills.filter(skill => 
                    !preferred_skills.find(s => s.skill_id === skill.skill_id)
                  ).map((skill) => (
                    <SelectItem 
                      key={skill.skill_id} 
                      value={skill.skill_id.toString()}
                    >
                      {skill.skill_name}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
              <Button 
                type="button" 
                variant="outline" 
                onClick={handleAddPreferredSkill}
                disabled={!selectedPreferredSkill}
              >
                Add
              </Button>
            </div>
          </div>

          <Button type="submit" className="w-full">
            Create Job
          </Button>
        </form>
      </CardContent>
    </Card>
  );
};

export default CreateJob;
