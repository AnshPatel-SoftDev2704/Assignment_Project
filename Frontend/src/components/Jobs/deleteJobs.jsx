import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
  DialogFooter,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { useSelector, useDispatch } from 'react-redux';
import { deleteJob, getAllJobs } from "@/services/Jobs/api";
import { getJobs } from "@/store/Jobs/actions";
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const DeleteJob = ({ showDeleteDialog, setShowDeleteDialog, deleteJobs }) => {
  const data = useSelector((state) => state.Userdata);
  const dispatch = useDispatch();

  const handleDelete = async () => {
    if (!deleteJobs?.job_id) {
      toast.error('Invalid job selected for deletion');
      return;
    }

    if (!data?.[0]?.token) {
      toast.error('You must be authenticated to perform this action');
      return;
    }

    try {

      const response = await deleteJob(data[0].token, deleteJobs.job_id);
      
      if(response.status === 403)
        throw new Error("You Don't have Permission to Perform this Action")

      if(response.status === 200)
      {
      const jobs = await getAllJobs(data[0].token);
      dispatch(getJobs(jobs.data));
      
      toast.success({
        render: `Successfully deleted ${deleteJobs.job_title}`,
        isLoading: false,
        autoClose: 3000
      });

    }else{
        throw new Error("Unable to delete the Job")
    }
    setShowDeleteDialog(false);
    } catch (err) {
      console.error('Error deleting user:', err);
      
      toast.error(err.response?.data?.message || 'Failed to delete job. Please try again.');
    }
  };

  return (
    <Dialog open={showDeleteDialog} onOpenChange={setShowDeleteDialog}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Delete Job</DialogTitle>
          <DialogDescription>
            Are you sure you want to do this?
            {deleteJobs?.job_title} will be permanently deleted.
          </DialogDescription>
        </DialogHeader>
        <DialogFooter>
          <Button
            onClick={() => setShowDeleteDialog(false)}
            variant="ghost"
          >
            Cancel
          </Button>
          <Button
            onClick={handleDelete}
            variant="destructive"
          >
            Confirm
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
};

export default DeleteJob;