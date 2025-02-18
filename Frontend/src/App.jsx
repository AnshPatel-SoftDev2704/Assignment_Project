import { useState, useEffect } from 'react';
import { Route, Routes, BrowserRouter } from 'react-router-dom';
import { Provider } from 'react-redux';
import store from './store/store';
import Login from './components/login';
import Dashboard from './components/dashboard';
import { ToastContainer } from 'react-toastify';
import CandidateDashboard from './components/CandidateDashboard';

import ShowUser from './components/User/showUser';
import SaveUser from './components/User/saveUser';
import DisplayUserRole from './components/User/showUserRoles';
import DisplaySkill from './components/Skill/showSkills';
import CreateSkill from './components/Skill/addSkills';
import ShowJobs from './components/Jobs/showJobs';
import CreateJob from './components/Jobs/saveJobs';
import CreateCandidate from './components/Candidate/addCandidate';
import ShowCandidates from './components/Candidate/showCandidates';
import CandidateApplicationStatus from './components/Candidate/showCandidateApplicationStatus';
import CreateInterview from './components/Interview/addInterview';
import DisplayInterview from './components/Interview/showInterview';
import AddFeedback from './components/Interview/saveFeedback';
import DisplayFeedback from './components/Interview/showFeedback';
import ShowDocuments from './components/Documents/showSubmittedDocument';

import CandidateProfile from './components/Candidate/editCandidateProfile';
import JobListings from './components/Candidate/showJobsToCandidate';
import DocumentForm from './components/Documents/saveDocument';

function App() {
  return (
    <Provider store={store}>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Login />} />
          
          <Route path="/dashboard" element={<Dashboard />}>
            <Route path="users" element={<ShowUser />} />
            <Route path="add-user" element={<SaveUser />} />
            <Route path="user-roles" element={<DisplayUserRole />} />
            <Route path="skills" element={<DisplaySkill />} />
            <Route path="add-skill" element={<CreateSkill />} />
            <Route path="jobs" element={<ShowJobs />} />
            <Route path="add-job" element={<CreateJob />} />
            <Route path="candidates" element={<ShowCandidates />} />
            <Route path="add-candidate" element={<CreateCandidate />} />
            <Route path="application-status" element={<CandidateApplicationStatus />} />
            <Route path="interviews" element={<DisplayInterview />} />
            <Route path="add-interview" element={<CreateInterview />} />
            <Route path="feedback" element={<DisplayFeedback />} />
            <Route path="add-feedback" element={<AddFeedback />} />
            <Route path="documents" element={<ShowDocuments />} />
          </Route>

          <Route path="/candidate" element={<CandidateDashboard />}>
            <Route path="profile" element={<CandidateProfile />} />
            <Route path="jobs" element={<JobListings />} />
            <Route path="documents" element={<DocumentForm />} />
          </Route>
        </Routes>
        <ToastContainer
        position='top-right'
        autoClose={3000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover/>
      </BrowserRouter>
    </Provider>
  );
}

export default App;