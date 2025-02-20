import React, { useState, useEffect } from 'react';
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { getAllJobs, getAllPreferredSkill, getAllRequiredSkill } from '@/services/Jobs/api';
import { useSelector } from 'react-redux';
import { saveCandidateApplicationStatus } from '@/services/Candidate/api';
import { toast } from 'react-toastify';
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogFooter,
  DialogDescription,
} from "@/components/ui/dialog";
import { Eye } from 'lucide-react';
const JobListings = () => {
  const [jobs, setJobs] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showDetailsDialog,setShowDetailsDialog] = useState(false)
  const [selectedJob,setSelectedJob] = useState({})
  const [requiredSkills,setRequiredSkills] = useState([])
  const [preferredSkills,setPreferredSkills] = useState([])
  const data = useSelector((state) => state.Userdata)

  useEffect(() => {
    const fetchJobs = async () => {
      try {
        setLoading(true);
        const response = await getAllJobs(data[0].token);
        const requiredSkill = await getAllRequiredSkill(data[0].token)
        const preferredSkill = await getAllPreferredSkill(data[0].token)
        setRequiredSkills(requiredSkill.data)
        setPreferredSkills(preferredSkill.data)
        setJobs(response.data);
      } catch (error) {
        toast.error("Failed to fetch job listings");
      } finally {
        setLoading(false);
      }
    };
    fetchJobs();
  }, []);

  const handleApply = async (jobId) => {
    try {
      await saveCandidateApplicationStatus(data[0].token, data[0].user_id, jobId, 6, new Date().toISOString());
      toast.success("Application submitted successfully");
      const response = await getAllJobs(data[0].token);
      setJobs(response.data);
    } catch (error) {
      toast.error(error.message || "Failed to submit application");
    }
  };

  const handleShowDetails = async (job) => {
    setShowDetailsDialog(true)
    setSelectedJob(job)
  }

  if (loading) {
    return <div className="flex justify-center items-center h-64">Loading jobs...</div>;
  }

  return (
    <>
    <Card className="w-full">
      <CardContent className="p-6">
        <h2 className="text-2xl font-bold mb-6">Available Positions</h2>
        <div className="overflow-x-auto">
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Job Title</TableHead>
                <TableHead>Description</TableHead>
                <TableHead>Posted Date</TableHead>
                <TableHead className='text-center'>Action</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {jobs.map((job) => (
                <TableRow key={job.job_id}>
                  <TableCell className="font-medium">{job.job_title}</TableCell>
                  <TableCell className="max-w-md">{job.job_description}</TableCell>
                  <TableCell>{new Date(job.created_at).toLocaleDateString()}</TableCell>
                  <TableCell className='text-center'>
                  <Button
                    variant="ghost"
                    size="icon"
                    className="h-8 w-8 mr-2"
                    onClick={() => handleShowDetails(job)}
                  >
                    <Eye className="h-4 w-4" />
                    </Button>
                    <Button 
                      onClick={() => handleApply(job.job_id)}
                      className="bg-blue-500 hover:bg-blue-600"
                    >
                      Apply
                    </Button>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </div>
      </CardContent>
    </Card>
    <Dialog open={showDetailsDialog} onOpenChange={setShowDetailsDialog}>
      <DialogContent className="max-w-2xl">
      <DialogHeader>
          <DialogTitle>Job Details</DialogTitle>
      </DialogHeader>
      {selectedJob && (
          <div className='space-y-6'>
              <div className='grid grid-cols-2 gap-6'>
                  <div className='space-y-4'>
                      <div>
                          <h3 className='text-lg font-semibold mb-3'>Job Information</h3>
                          <div className='space-y-2'>
                              <p><span className='font-medium'>Title: </span>{selectedJob.job_title}</p>
                              <p><span className='font-medium'>Decription: </span>{selectedJob.job_description}</p>
                              {/* <p><span className='font-medium'>Status: </span>{selectedJob.job_Status.job_Status_name}</p> */}
                              <p><span className='font-medium'>Id: </span>{selectedJob.job_id}</p>
                              <p><span className='font-medium'>Closed Reason: </span>{selectedJob.job_Closed_reason}</p>
                              <p><span className='font-medium'>Selected Candidate Id: </span>{selectedJob.job_Selected_Candidate_id}</p>
                          </div>
                      </div>
                      <div className='space-y-4'>
                      {/* <div>
                          <h3 className='text-lg font-semibold mb-3'>Created By</h3>
                          <div className='space-y-2'>
                              <p><span className='font-medium'>Name: </span>{selectedJob.user.name}</p>
                              <p><span className='font-medium'>Email: </span>{selectedJob.user.email}</p>
                              <p><span className='font-medium'>Contact: </span>{selectedJob.user.contact}</p>
                          </div>
                      </div> */}
                          <div>
                          <h3 className="font-semibold mb-2">Required Skills:</h3>
                          <ul className="list-disc list-inside space-y-2">
                              {requiredSkills.map(skill => skill.job_id === selectedJob.job_id && (<li>{skill.skill.skill_name}</li>))}
                          </ul>
                          </div>
                          <div>
                          <h3 className="font-semibold mb-2">Preferred Skills:</h3>
                          <ul className="list-disc list-inside space-y-2">
                              {preferredSkills.map(skill =>skill.job_id === selectedJob.job_id && (<li>{skill.skill.skill_name}</li>))}
                          </ul>
                          </div>
                      </div>
                  </div>
              </div>
          </div>
      )}
      </DialogContent>
  </Dialog>
    </>
  );
};

export default JobListings;