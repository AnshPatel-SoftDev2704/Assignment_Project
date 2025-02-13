import React, { useState, useEffect } from 'react';
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { getAllJobs } from '@/services/Jobs/api';
import { useSelector } from 'react-redux';
import { saveCandidateApplicationStatus } from '@/services/Candidate/api';

const JobListings = () => {
  const [jobs, setJobs] = useState([]);
  const [loading, setLoading] = useState(true);
  const data = useSelector((state) => state.Userdata)

  useEffect(() => {
    const fetchJobs = async () => {
      try {
        const jobs = await getAllJobs(data[0].token)
        setJobs(jobs.data);
        setLoading(false);
      } catch (error) {
        console.error('Error fetching jobs:', error);
        setLoading(false);
      }
    };

    fetchJobs();
  }, []);

  const handleApply = async (jobId) => {
    try {
        const candidate_id =  data[0].user_id
        const todayDate = new Date().toISOString();
        const application_Status_id = 6;
        const response = await saveCandidateApplicationStatus(data[0].token,candidate_id,jobId,application_Status_id,todayDate)
    } catch (error) {
      console.error('Error applying for job:', error);
      alert('Failed to apply for the job. Please try again.');
    }
  };

  if (loading) {
    return <div className="flex justify-center items-center h-64">Loading jobs...</div>;
  }

  return (
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
                <TableHead>Action</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {jobs.map((job) => (
                <TableRow key={job.job_id}>
                  <TableCell className="font-medium">{job.job_title}</TableCell>
                  <TableCell className="max-w-md">{job.job_description}</TableCell>
                  <TableCell>{new Date(job.created_at).toLocaleDateString()}</TableCell>
                  <TableCell>
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
  );
};

export default JobListings;