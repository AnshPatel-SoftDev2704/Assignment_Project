
import { useSelector } from 'react-redux';
import { Outlet } from 'react-router-dom';
import Sidebar from './Sidebar';
import NotificationComponent from './NotificationComponent';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
const CandidateDashboard = () => {
    const data = useSelector((state) => state.Userdata);

    return (
        <div className="flex">
            <Sidebar isAdmin={false} />
            <div className="ml-64 p-8 w-full">
                <div className="flex justify-between items-center">
                    <h1 className="text-3xl font-bold mb-6">Welcome {data?.[0]?.name || 'User'}</h1>
                    <NotificationComponent />
                </div>
                <Outlet />
            </div>
            <ToastContainer position="top-right" autoClose={3000} />
        </div>
    );
};

export default CandidateDashboard;