import React, { useEffect, useState } from 'react';
import { Card, CardHeader, CardTitle, CardContent } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { X } from 'lucide-react';
import { useSelector } from 'react-redux';
import { getCandidateDetailById, getAllCandidateSkill, updateCandidate, saveCandidateSkill } from '@/services/Candidate/api';
import { getAllSkill } from '@/services/Skills/api';
import { toast } from 'react-toastify';

const CandidateProfile = () => {
  const [profile, setProfile] = useState({
    candidate_name: '',
    candidate_DOB: '',
    candidate_email: '',
    candidate_password: '',
    candidate_Total_Work_Experience: '',
    phoneNo: '',
    CV_Path: ''
  });
  
  const [availableSkills, setAvailableSkills] = useState([]);
  const [selectedSkills, setSelectedSkills] = useState([]);
  const [updatedSkills,setUpdatedSkills] = useState([])
  const [selectedSkillId, setSelectedSkillId] = useState("");
  const [skillExperience, setSkillExperience] = useState("");
  const [cv,setCV] = useState()
  
  const [passwordData, setPasswordData] = useState({
    currentPassword: '',
    newPassword: '',
    confirmPassword: ''
  });
  const [passwordError, setPasswordError] = useState('');

  const data = useSelector((state) => state.Userdata);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const candidate = await getCandidateDetailById(data[0].token, data[0].user_id);
        setProfile({
          candidate_name: candidate.data.candidate_name || '',
          candidate_DOB: candidate.data.candidate_DOB || '',
          candidate_email: candidate.data.candidate_email || '',
          candidate_password: candidate.data.candidate_password || '',
          candidate_Total_Work_Experience: candidate.data.candidate_Total_Work_Experience || '',
          phoneNo: candidate.data.phoneNo || '',
          CV_Path: candidate.data.CV_Path || 'anything'
        });

        const candidateSkills = await getAllCandidateSkill(data[0].token);
        const filteredSkills = candidateSkills.data
          .filter(skill => skill.candidate_id === data[0].user_id)
          .map(skillData => ({
            skill_id: skillData.skill.skill_id,
            skill_name: skillData.skill.skill_name,
            total_Skill_Work_Experience: skillData.total_Skill_Work_experience
          }));
        setSelectedSkills(filteredSkills);

        const response = await getAllSkill(data[0].token);
        setAvailableSkills(response.data);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };
    fetchData();
  }, [data]);

  const handleProfileChange = (e) => {
    setProfile({
      ...profile,
      [e.target.name]: e.target.value
    });
  };

  const handlePasswordChange = (e) => {
    const { name, value } = e.target;
    setPasswordData(prev => ({
      ...prev,
      [name]: value
    }));
    setPasswordError('');
  };

  const handleAddSkill = () => {
    if (selectedSkillId && skillExperience) {
      const skill = availableSkills.find(s => s.skill_id === parseInt(selectedSkillId));
      if (skill && !selectedSkills.some(s => s.skill_id === skill.skill_id)) {
        setSelectedSkills([
          ...selectedSkills,
          {
            skill_id: skill.skill_id,
            skill_name: skill.skill_name,
            total_Skill_Work_Experience: parseInt(skillExperience)
          }
        ]);
        setUpdatedSkills([...updatedSkills,{
          skill_id: skill.skill_id,
          skill_name: skill.skill_name,
          experience: parseInt(skillExperience)
        }])
        setSelectedSkillId("");
        setSkillExperience("");
      }
    }
  };

  const handleRemoveSkill = (skillId) => {
    setSelectedSkills(selectedSkills.filter(s => s.skill_id !== skillId));
    setUpdatedSkills(updatedSkills.filter(s => s.skill_id !== skillId));
  };

  const validatePasswordChange = () => {
    if (passwordData.newPassword !== passwordData.confirmPassword) {
      setPasswordError("New passwords don't match");
      return false;
    }
    if (passwordData.newPassword.length < 6) {
      setPasswordError("New password must be at least 6 characters long");
      return false;
    }
    return true;
  };

  const handleSaveProfile = () => {
    if (passwordData.newPassword && !validatePasswordChange()) {
      return;
    }

    const updatedProfile = {
      ...profile
    };

    if (passwordData.newPassword) {
      updatedProfile.candidate_password = passwordData.newPassword;
    }

    try{
        const response = updateCandidate(data[0].token,updatedProfile,data[0].user_id,cv)

        if(response.status === 403)
        throw new Error("You Don't have Permission to Perform this Action")

        updatedSkills?.forEach(skill => {
            console.log(data[0].user_id)
            const result = saveCandidateSkill(data[0].token,skill,data[0].user_id)
        });
        setUpdatedSkills([])
    }
    catch(error)
    {
        console.error(error)
        setUpdatedSkills([])
    }
  };

  const validateProfile = () => {
    if (!profile.candidate_name.trim()) {
      toast.error("Name is required");
      return false;
    }
    if (!profile.candidate_email.trim()) {
      toast.error("Email is required");
      return false;
    }
    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(profile.candidate_email)) {
      toast.error("Invalid email format");
      return false;
    }
    return true;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (!validateProfile()) return;

      await handleSaveProfile();
      toast.success("Profile updated successfully");
    } catch (error) {
      toast.error(error.message || "Failed to update profile");
    }
  };

  return (
    <div className="p-6 max-w-4xl mx-auto space-y-6">
      <Card>
        <CardHeader>
          <CardTitle>Profile Information</CardTitle>
        </CardHeader>
        <CardContent className="space-y-4">
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div className="space-y-2">
              <label className="text-sm font-medium">Full Name</label>
              <Input
                name="candidate_name"
                value={profile.candidate_name}
                onChange={handleProfileChange}
              />
            </div>
            <div className="space-y-2">
              <label className="text-sm font-medium">Date of Birth</label>
              <Input
                type="date"
                name="candidate_DOB"
                value={profile.candidate_DOB ? new Date(profile.candidate_DOB).toISOString().split('T')[0] : ''}
                onChange={handleProfileChange}
              />
            </div>
            <div className="space-y-2">
              <label className="text-sm font-medium">Email</label>
              <Input
                type="email"
                name="candidate_email"
                value={profile.candidate_email}
                onChange={handleProfileChange}
              />
            </div>
            <div className="space-y-2">
              <label className="text-sm font-medium">Phone Number</label>
              <Input
                name="phoneNo"
                value={profile.phoneNo}
                onChange={handleProfileChange}
              />
            </div>
            <div className="space-y-2">
              <label className="text-sm font-medium">Total Work Experience (years)</label>
              <Input
                type="number"
                name="candidate_Total_Work_Experience"
                value={profile.candidate_Total_Work_Experience}
                onChange={handleProfileChange}
              />
            </div>
            <div className="space-y-2">
              <label className="text-sm font-medium">CV Path</label>
              <Input
                type="file"
                name="CV_Path"
                onChange={(e) => setCV(e.target.files[0])}
              />
            </div>
          </div>
        </CardContent>
      </Card>

      <Card>
        <CardHeader>
          <CardTitle>Change Password</CardTitle>
        </CardHeader>
        <CardContent className="space-y-4">
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div className="space-y-2">
              <label className="text-sm font-medium">Current Password</label>
              <Input
                type="password"
                name="currentPassword"
                value={passwordData.currentPassword}
                onChange={handlePasswordChange}
              />
            </div>
            <div className="space-y-2">
              <label className="text-sm font-medium">New Password</label>
              <Input
                type="password"
                name="newPassword"
                value={passwordData.newPassword}
                onChange={handlePasswordChange}
              />
            </div>
            <div className="space-y-2">
              <label className="text-sm font-medium">Confirm New Password</label>
              <Input
                type="password"
                name="confirmPassword"
                value={passwordData.confirmPassword}
                onChange={handlePasswordChange}
              />
            </div>
          </div>
          {passwordError && (
            <div className="text-red-500 text-sm mt-2">{passwordError}</div>
          )}
        </CardContent>
      </Card>

      <Card>
        <CardHeader>
          <CardTitle>Skills</CardTitle>
        </CardHeader>
        <CardContent className="space-y-4">
          <div className="flex flex-wrap gap-2">
            {selectedSkills.map((skill) => (
              <div
                key={skill.skill_id}
                className="flex items-center gap-2 bg-blue-100 px-3 py-1 rounded-full"
              >
                <span>{skill.skill_name} ({skill.total_Skill_Work_Experience} years)</span>
                <button
                  onClick={() => handleRemoveSkill(skill.skill_id)}
                  className="text-blue-600 hover:text-blue-800"
                >
                  <X className="h-4 w-4" />
                </button>
              </div>
            ))}
          </div>

          <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
            <div className="space-y-2">
              <label className="text-sm font-medium">Select Skill</label>
              <select
                className="w-full p-2 border rounded-md"
                value={selectedSkillId}
                onChange={(e) => setSelectedSkillId(e.target.value)}
              >
                <option value="">Select a skill</option>
                {availableSkills
                  .filter(skill => !selectedSkills.some(s => s.skill_id === skill.skill_id))
                  .map(skill => (
                    <option key={skill.skill_id} value={skill.skill_id}>
                      {skill.skill_name}
                    </option>
                  ))}
              </select>
            </div>
            <div className="space-y-2">
              <label className="text-sm font-medium">Experience (years)</label>
              <Input
                type="number"
                value={skillExperience}
                onChange={(e) => setSkillExperience(e.target.value)}
                min="0"
              />
            </div>
            <div className="flex items-end">
              <Button onClick={handleAddSkill}>Add Skill</Button>
            </div>
          </div>
        </CardContent>
      </Card>

      <div className="flex justify-end">
        <Button onClick={handleSubmit} className="bg-blue-600 hover:bg-blue-700">
          Save Profile
        </Button>
      </div>
    </div>
  );
};

export default CandidateProfile;