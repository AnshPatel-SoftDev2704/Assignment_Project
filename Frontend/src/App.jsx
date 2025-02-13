import { useState, useEffect } from 'react';
import { Route,Routes,BrowserRouter } from 'react-router-dom';
import Login from './components/login'
import { Provider } from 'react-redux';
import store from './store/store';
import Dashboard from './components/dashboard';
import CandidateProfile from './components/Candidate/editCandidateProfile';
import CandidateDashboard from './components/CandidateDashboard';
function App() {
  return (
    <Provider store={store}>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Login/>}/>
        <Route path="/dashboard" element={<Dashboard/>}/>
        <Route path='/candidate' element={<CandidateDashboard/>}/>
      </Routes>
    </BrowserRouter>
    </Provider>
  );
}

export default App;
