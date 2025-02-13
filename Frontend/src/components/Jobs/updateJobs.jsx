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
import { getAllSkill } from '@/services/Skills/api';
import { getAllJobStatus, getAllRequiredSkill, getAllPreferredSkill, updateJob, saveRequiredSkill, savePreferredSkill, getAllJobs, deleteRequiredSkill} from '@/services/Jobs/api';
import { getJobs } from '@/store/Jobs/actions';
const UpdateJob = ({ 
  showEditDialog, 
  setShowEditDialog, 
  editJob
}) => {
  const data = useSelector((state) => state.Userdata);
  const dispatch = useDispatch();

  const [editFormData, setEditFormData] = useState({
    Job_id: editJob?.job_id,
    Job_title: editJob?.job_title || '',
    Job_description: editJob?.job_description || '',
    Job_Status_id: editJob?.job_Status_id || '',
    Job_Closed_reason: editJob?.job_Closed_reason || '',
    Job_Selected_Candidate_id: editJob?.job_Selected_Candidate_id || 0,
    Created_by: editJob?.created_by || 0
  });

  
  const handleInputChange = (e) => {
      const { name, value } = e.target;
      setEditFormData(prev => ({
          ...prev,
          [name]: value
        }));
    };
    
    const handleStatusChange = (value) => {
        setEditFormData(prev => ({
            ...prev,
            Job_Status_id: parseInt(value)
        }));
    };


    const [jobStatuses, setJobStatuses] = useState([]);
    const [skills, setSkills] = useState([]);
    const [rawRequiredSkill,setRawRequiredSkill] = useState([])
    const [raqPreferredSkill,setRawPreferredSkill] = useState([])
  
    useEffect(() => {
      const fetchData = async () => {
        const jobStatus = await getAllJobStatus(data[0].token);
        setJobStatuses(jobStatus.data);

        const skill = await getAllSkill(data[0].token);
        setSkills(skill.data);

        const requiredskills = await getAllRequiredSkill(data[0].token)
        const filteredRequiredSkills = requiredskills.data.filter(requiredskill => requiredskill.job_id === editJob.job_id && requiredskill.job_id > 0).map(skill => skill.skill)
        setRequired_Skills(filteredRequiredSkills)
        setRawRequiredSkill(requiredskills.data)

        const preferredskills = await getAllPreferredSkill(data[0].token)
        const filteredPreferredSkills = preferredskills.data.filter(preferredskill => preferredskill.job_id === editJob.job_id && preferredskill.job_id > 0).map(skill => skill.skill)
        setPreferred_Skills(filteredPreferredSkills)
        setRawPreferredSkill(preferredskills.data)
      };
      fetchData();
    }, []);
  
    const [required_skills, setRequired_Skills] = useState([]);
    const [preferred_skills, setPreferred_Skills] = useState([]);
  
    const [selectedRequiredSkill, setSelectedRequiredSkill] = useState("");
    const [selectedPreferredSkill, setSelectedPreferredSkill] = useState("");

    const [updatedRequiredSkill,setUpdatedRequiredSkill] = useState([])
    const [updatedPreferredSkill,setUpdatedPreferredSkill] = useState([])

    const [removedRequiredSkill,setRemovedRequiredSkill] = useState([])
    const [removedPreferredSkill,setRemovedPreferredSkill] = useState([])
  
    const handleAddRequiredSkill = () => {
      if (selectedRequiredSkill && !required_skills.find(skill => skill.skill_id === parseInt(selectedRequiredSkill))) {
        const skillToAdd = skills.find(skill => skill.skill_id === parseInt(selectedRequiredSkill));
        setRequired_Skills([...required_skills, skillToAdd]);
        setUpdatedRequiredSkill([...updatedRequiredSkill,skillToAdd])
        removedRequiredSkill.filter(skill => skill.skill.skill_id !== parseInt(selectedRequiredSkill))
      }
      setSelectedRequiredSkill("");
    };
  
    const handleAddPreferredSkill = () => {
      if (selectedPreferredSkill && !preferred_skills.find(skill => skill.skill_id === parseInt(selectedPreferredSkill))) {
        const skillToAdd = skills.find(skill => skill.skill_id === parseInt(selectedPreferredSkill));
        setPreferred_Skills([...preferred_skills, skillToAdd]);
        setUpdatedPreferredSkill([...updatedPreferredSkill,skillToAdd])
        removedPreferredSkill.filter(skill => skill.skill.skill_id !== parseInt(selectedPreferredSkill))
      }
      setSelectedPreferredSkill("");
    };
  
    const handleRemoveRequiredSkill = (skillId) => {
      setRequired_Skills(required_skills.filter(skill => skill.skill_id !== skillId));
      setUpdatedRequiredSkill(required_skills.filter(skill => skill.skill_id !== skillId))
      const skillToAdd = skills.find(skill => skill.skill_id === skillId);
      setRemovedRequiredSkill([...removedRequiredSkill,skillToAdd])
    };
  
    const handleRemovePreferredSkill = (skillId) => {
      const skillToAdd = rawRequiredSkill.filter(skill => skill.skill.skill_id === skillId);
      setPreferred_Skills(preferred_skills.filter(skill => skill.skill_id !== skillId));
      setUpdatedPreferredSkill(preferred_skills.filter(skill => skill.skill_id !== skillId))
      setRemovedPreferredSkill([...removedPreferredSkill,skillToAdd])
      console.log(removedPreferredSkill)
    };

  const handleUpdate = async () => {
    try {
        console.log(editFormData)
      await updateJob(data[0].token, editFormData);
      const result = await getAllJobs(data[0].token);
      updatedRequiredSkill.forEach(skill => {
        const response = saveRequiredSkill(data[0].token,editJob.job_id,skill.skill_id)
      });

      updatedPreferredSkill.forEach(skill => {
        const response = savePreferredSkill(data[0].token,editJob.job_id,skill.skill_id)
      });
      removedRequiredSkill[0].forEach(skill => {
        const id = deleteRequiredSkill(data[0].token,skill.required_job_skill_id)
      })
      removedPreferredSkill[0].forEach(skill => {
        const id = deleteRequiredSkill(data[0].token,skill.preferred_job_skill_id)
      })
      dispatch(getJobs(result.data));
      setShowEditDialog(false);
    } catch (err) {
      console.error('Error updating job:', err);
    }
  };

  return (
    <Dialog open={showEditDialog} onOpenChange={setShowEditDialog}>
      <DialogContent className="max-w-2xl">
        <DialogHeader>
          <DialogTitle>Edit Job</DialogTitle>
        </DialogHeader>
        
        <div className="space-y-4">
          <div className="space-y-2">
            <Label htmlFor="Job_title">Job Title</Label>
            <Input
              id="Job_title"
              name="Job_title"
              value={editFormData.Job_title}
              onChange={handleInputChange}
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="Job_description">Job Description</Label>
            <Textarea
              id="Job_description"
              name="Job_description"
              value={editFormData.Job_description}
              onChange={handleInputChange}
              className="min-h-[100px]"
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="Job_Status_id">Job Status</Label>
            <Select
              value={editFormData.Job_Status_id}
              onValueChange={handleStatusChange}
            >
              <SelectTrigger>
                <SelectValue placeholder="Select Status" />
              </SelectTrigger>
              <SelectContent>
                {jobStatuses.map((status) => (
                  <SelectItem 
                    key={status.job_Status_id} 
                    value={status.job_Status_id}
                  >
                    {status.job_Status_name}
                  </SelectItem>
                ))}
              </SelectContent>
            </Select>
          </div>

          {editFormData.Job_Status_id === 4 && (
            <div className="space-y-2">
              <Label htmlFor="Job_Closed_reason">Closing Reason</Label>
              <Input
                id="Job_Closed_reason"
                name="Job_Closed_reason"
                value={editFormData.Job_Closed_reason}
                onChange={handleInputChange}
              />
            </div>
          )}

          <div className="space-y-2">
            <Label>Required Skills</Label>
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
                      value={skill.skill_id}
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
            <Label>Preferred Skills</Label>
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
                      value={skill.skill_id}
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

export default UpdateJob;