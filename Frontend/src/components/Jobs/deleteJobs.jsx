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

  const DeleteJob = ({ showDeleteDialog, setShowDeleteDialog, deleteJobs }) => {
    const data = useSelector((state) => state.Userdata);
    const dispatch = useDispatch();
  
    const handleDelete = async () => {
      try {
        const response = await deleteJob(data[0].token,deleteJobs.job_id)
        const jobs = await getAllJobs(data[0].token)
        dispatch(getJobs(jobs.data))
        setShowDeleteDialog(false);
      } catch (err) {
        console.error('Error deleting user:', err);
      }
    };
  
    return (
      <Dialog open={showDeleteDialog} onOpenChange={setShowDeleteDialog}>
        <DialogContent className="bg-white text-black p-0 overflow-hidden">
          <DialogHeader className="pt-8 px-6">
            <DialogTitle className="text-2xl text-center font-bold">
              Delete Job
            </DialogTitle>
            <DialogDescription className="text-center text-zinc-500">
              Are you sure you want to do this? <br />
              <span className="text-indigo-500 font-semibold">{deleteJobs?.job_title}</span> will be permanently deleted.
            </DialogDescription>
          </DialogHeader>
  
          <DialogFooter className="bg-gray-100 px-6 py-4">
            <div className="flex items-center justify-between w-full">
              <Button
                onClick={() => setShowDeleteDialog(false)}
                variant="ghost"
              >
                Cancel
              </Button>
              <Button
                variant="primary"
                onClick={handleDelete}
              >
                Confirm
              </Button>
            </div>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    );
  };
  
  export default DeleteJob;