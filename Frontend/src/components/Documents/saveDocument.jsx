import React, { useEffect, useState } from 'react';
import { Card, CardHeader, CardTitle, CardContent } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { getDocumentType, saveDocument } from '@/services/Document/api';
import { useSelector } from 'react-redux';
import { Label } from "@/components/ui/label";
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select";
import { getAllJobs } from '@/services/Jobs/api';

const DocumentForm = () => {
  const [formData, setFormData] = useState({
    candidate_id:'',
    job_id: '',
    document_Type_id: '',
    Status: false,
    Approved_by:0,
    Document_path : 'anything'
  });
  const [documentType,setDocumentType] = useState([])
  const [jobs,setJobs] = useState([])
  const [file,setFile] = useState()
  const data = useSelector((state) => state.Userdata)

  useEffect(() => {
    const fetchData = async () => {
        const documenTypes = await getDocumentType(data[0].token)
        const jobs = await getAllJobs(data[0].token)
        setDocumentType(documenTypes.data)
        setJobs(jobs.data)
    }
    fetchData()
  },[])
  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleFileChange = (e) => {
    setFile(e.target.files[0])
  };

  const handleSubmit = async () => {
    formData.candidate_id = data[0].user_id
    formData.job_id = parseInt(formData.job_id)
    formData.document_Type_id = parseInt(formData.document_Type_id)
    try{
        const respone = await saveDocument(data[0].token,formData,file)
    }
    catch(error)
    {
        console.error(error)
    }
  };

  return (
    <div className="p-6 max-w-2xl mx-auto">
      <Card>
        <CardHeader>
          <CardTitle>Submit Document</CardTitle>
        </CardHeader>
        <CardContent className="space-y-4">
        <div className="space-y-2">
            <Label>Job Title</Label>
            <Select
                value={formData.job_id}
                onValueChange={(value) => 
                    setFormData(prev => ({ ...prev, job_id: value }))
                }
            >
                <SelectTrigger>
                    <SelectValue placeholder="Select Job" />
                </SelectTrigger>
                <SelectContent>
                    {jobs.map(job => (
                        <SelectItem 
                            key={job.job_id} 
                            value={job.job_id.toString()}
                        >
                            {job.job_title}
                        </SelectItem>
                    ))}
                </SelectContent>
            </Select>
          </div>
          <div className="space-y-2">
            <Label>Document Type</Label>
            <Select
                value={formData.document_Type_id}
                onValueChange={(value) => 
                    setFormData(prev => ({ ...prev, document_Type_id: value }))
                }
            >
                <SelectTrigger>
                    <SelectValue placeholder="Select status" />
                </SelectTrigger>
                <SelectContent>
                    {documentType.map(type => (
                        <SelectItem 
                            key={type.document_Type_id} 
                            value={type.document_Type_id.toString()}
                        >
                            {type.document_name}
                        </SelectItem>
                    ))}
                </SelectContent>
            </Select>
          </div>
          <div className="space-y-2">
            <label className="text-sm font-medium">File</label>
            <Input
              type="file"
              name="file"
              onChange={handleFileChange}
            />
          </div>
          <Button onClick={handleSubmit}>Submit</Button>
        </CardContent>
      </Card>
    </div>
  );
};

export default DocumentForm;
