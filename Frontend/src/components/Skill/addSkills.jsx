import { useState } from 'react';
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { useSelector, useDispatch } from 'react-redux';
import { saveSkill, getAllSkill } from '@/services/Skills/api';
import { getSkill } from '@/store/Skills/actions';

const CreateSkill = () => {
    const [skillData, setSkillData] = useState({
        skill_name: '',
        skill_description: ''
    });

    const data = useSelector((state) => state.Userdata);
    const dispatch = useDispatch();

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setSkillData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await saveSkill(data[0].token, skillData);
            const result = await getAllSkill(data[0].token);
            dispatch(getSkill(result.data));
            setSkillData({
                skill_name: '',
                skill_description: ''
            });
        } catch (error) {
            console.error("Error saving skill:", error);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-4 p-4">
            <div className="space-y-2">
                <Label htmlFor="skill_name">Skill Name</Label>
                <Input
                    id="skill_name"
                    name="skill_name"
                    value={skillData.skill_name}
                    onChange={handleInputChange}
                    required
                />
            </div>

            <div className="space-y-2">
                <Label htmlFor="skill_description">Skill Description</Label>
                <Input
                    id="skill_description"
                    name="skill_description"
                    value={skillData.skill_description}
                    onChange={handleInputChange}
                    required
                />
            </div>

            <Button type="submit">Create Skill</Button>
        </form>
    );
};

export default CreateSkill;
