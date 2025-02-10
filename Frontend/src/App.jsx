import { useState, useEffect } from 'react';
import { Route,Routes,BrowserRouter } from 'react-router-dom';
import Login from './components/login'
import { Provider } from 'react-redux';
import store from './store/store';
import Dashboard from './components/dashboard';
function App() {
  return (
    <Provider store={store}>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Login/>}/>
        <Route path="/dashboard" element={<Dashboard/>}/>
      </Routes>
    </BrowserRouter>
    </Provider>
  );
}

export default App;
