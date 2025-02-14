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
import { useSelector, useDispatch } from 'react-redux';
import { updateFeedback, getAllFeedback } from '@/services/Interview/api';

const UpdateFeedback = ({ showEditDialog, setShowEditDialog, editFeedback }) => {
    const [feedbackData, setFeedbackData] = useState(editFeedback);
    const data = useSelector((state) => state.Userdata);
    const dispatch = useDispatch();

    useEffect(() => {
        setFeedbackData(editFeedback);
    }, [editFeedback]);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFeedbackData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleUpdate = async () => {
        try {
            await updateFeedback(data[0].token, feedbackData.feedback_id, feedbackData);
            setShowEditDialog(false);
        } catch (error) {
            console.error("Error updating feedback:", error);
        }
    };

    return (
        <Dialog open={showEditDialog} onOpenChange={setShowEditDialog}>
            <DialogContent className="bg-white text-black p-0 overflow-hidden">
                <DialogHeader className="pt-8 px-6">
                    <DialogTitle className="text-2xl text-center font-bold">
                        Update Feedback
                    </DialogTitle>
                </DialogHeader>

                <div className="px-6 space-y-4">
                    <Label>Technology</Label>
                    <Input
                        name="technology"
                        value={feedbackData.technology}
                        onChange={handleInputChange}
                    />

                    <Label>Rating</Label>
                    <Input
                        type="number"
                        name="rating"
                        value={feedbackData.rating}
                        onChange={handleInputChange}
                    />

                    <Label>Comments</Label>
                    <Textarea
                        name="comments"
                        value={feedbackData.comments}
                        onChange={handleInputChange}
                    />
                </div>

                <DialogFooter className="bg-gray-100 px-6 py-4">
                    <Button onClick={() => setShowEditDialog(false)} variant="ghost">
                        Cancel
                    </Button>
                    <Button variant="primary" onClick={handleUpdate}>
                        Update Feedback
                    </Button>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    );
};

export default UpdateFeedback;