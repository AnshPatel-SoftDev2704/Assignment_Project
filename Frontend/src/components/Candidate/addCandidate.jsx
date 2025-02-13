import { useEffect, useState } from 'react';
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Badge } from "@/components/ui/badge";
import { X } from "lucide-react";
import { useSelector, useDispatch } from 'react-redux';
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { getAllSkill } from '@/services/Skills/api';
import { saveCandidateDetails, saveCandidateSkill } from '@/services/Candidate/api';
const CreateCandidate = () => {
  const data = useSelector((state) => state.Userdata);
  const dispatch = useDispatch();

  const [candidateData, setCandidateData] = useState({
    Candidate_name: '',
    Candidate_DOB: '',
    Candidate_email: '',
    Candidate_password: '',
    Candidate_Total_Work_Experience: 0,
    PhoneNo: '',
    CV_Path: '',
    Role_id: ''
  });

  const [skills, setSkills] = useState([]);
  const [selectedSkills, setSelectedSkills] = useState([]);
  const [selectedSkill, setSelectedSkill] = useState("");
  const [skillExperience, setSkillExperience] = useState({});

  useEffect(() => {
    const fetchData = async () => {
      const skill = await getAllSkill(data[0].token);
      setSkills(skill.data);
    };
    fetchData();
  }, []);

  const handleAddSkill = () => {
    if (selectedSkill && !selectedSkills.find(skill => skill.skill_id === parseInt(selectedSkill))) {
      const skillToAdd = skills.find(skill => skill.skill_id === parseInt(selectedSkill));
      setSelectedSkills([...selectedSkills, skillToAdd]);
      setSkillExperience({
        ...skillExperience,
        [selectedSkill]: 0
      });
    }
    setSelectedSkill("");
  };

  const handleRemoveSkill = (skillId) => {
    setSelectedSkills(selectedSkills.filter(skill => skill.skill_id !== skillId));
    const { [skillId]: removedSkill, ...remainingSkills } = skillExperience;
    setSkillExperience(remainingSkills);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
        console.log(candidateData)
      const result = await saveCandidateDetails(data[0].token, candidateData);
      
      selectedSkills.forEach(async skill => {
        const skillData = {
          Candidate_id: result.data.candidate_id,
          Skill_id: skill.skill_id,
          Total_Skill_Work_experience: skillExperience[skill.skill_id]
        };
        await saveCandidateSkill(data[0].token, skillData);
      });

      setCandidateData({
        Candidate_name: '',
        Candidate_DOB: '',
        Candidate_email: '',
        Candidate_password: '',
        Candidate_Total_Work_Experience: 0,
        PhoneNo: '',
        CV_Path: '',
        Role_id: 6
      });
      setSelectedSkills([]);
      setSkillExperience({});
    } catch (error) {
      console.error("Error creating candidate:", error);
    }
  };

  return (
    <Card>
      <CardHeader>
        <CardTitle>Create New Candidate</CardTitle>
      </CardHeader>
      <CardContent>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div className="space-y-2">
            <Input
              placeholder="Candidate Name"
              value={candidateData.Candidate_name}
              onChange={(e) => setCandidateData({ ...candidateData, Candidate_name: e.target.value })}
            />
          </div>

          <div className="space-y-2">
            <Input
              type="date"
              value={candidateData.Candidate_DOB}
              onChange={(e) => setCandidateData({ ...candidateData, Candidate_DOB: e.target.value })}
            />
          </div>

          <div className="space-y-2">
            <Input
              type="email"
              placeholder="Email"
              value={candidateData.Candidate_email}
              onChange={(e) => setCandidateData({ ...candidateData, Candidate_email: e.target.value })}
            />
          </div>

          <div className="space-y-2">
            <Input
              type="password"
              placeholder="Password"
              value={candidateData.Candidate_password}
              onChange={(e) => setCandidateData({ ...candidateData, Candidate_password: e.target.value })}
            />
          </div>

          <div className="space-y-2">
            <Input
              type="number"
              placeholder="Total Work Experience (years)"
              value={candidateData.Candidate_Total_Work_Experience}
              onChange={(e) => setCandidateData({ ...candidateData, Candidate_Total_Work_Experience: parseInt(e.target.value) })}
            />
          </div>

          <div className="space-y-2">
            <Input
              type="tel"
              placeholder="Phone Number"
              value={candidateData.PhoneNo}
              onChange={(e) => setCandidateData({ ...candidateData, PhoneNo: e.target.value })}
            />
          </div>

          <div className="space-y-2">
            <Input
              type="file"
              onChange={(e) => setCandidateData({ ...candidateData, CV_Path: e.target.files[0] })}
            />
          </div>

          <div className="space-y-2">
            <h3 className="text-sm font-medium">Skills</h3>
            <div className="flex flex-wrap gap-2 mb-2">
              {selectedSkills.map((skill) => (
                <div key={skill.skill_id} className="space-y-1">
                  <Badge 
                    variant="secondary"
                    className="flex items-center gap-1"
                  >
                    {skill.skill_name}
                    <X
                      className="h-3 w-3 cursor-pointer"
                      onClick={() => handleRemoveSkill(skill.skill_id)}
                    />
                  </Badge>
                  <Input
                    type="number"
                    placeholder="Years of experience"
                    value={skillExperience[skill.skill_id]}
                    onChange={(e) => setSkillExperience({
                      ...skillExperience,
                      [skill.skill_id]: parseInt(e.target.value)
                    })}
                    className="w-32"
                  />
                </div>
              ))}
            </div>
            <div className="flex gap-2">
              <Select
                value={selectedSkill}
                onValueChange={setSelectedSkill}
              >
                <SelectTrigger className="w-full">
                  <SelectValue placeholder="Select skill" />
                </SelectTrigger>
                <SelectContent>
                  {skills.filter(skill => 
                    !selectedSkills.find(s => s.skill_id === skill.skill_id)
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
                onClick={handleAddSkill}
                disabled={!selectedSkill}
              >
                Add
              </Button>
            </div>
          </div>

          <Button type="submit" className="w-full">
            Create Candidate
          </Button>
        </form>
      </CardContent>
    </Card>
  );
};

export default CreateCandidate;