import { useSelector } from 'react-redux';
import { Outlet, useNavigate } from 'react-router-dom';
import Sidebar from './Sidebar';
import { useEffect } from 'react';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import NotificationComponent from './NotificationComponent';

const Dashboard = () => {
    const navigate = useNavigate();
    const data = useSelector((state) => state.Userdata);

    useEffect(() => {
        if (!data || !data[0]) {
            toast.error("Please login to access the dashboard");
            navigate('/');
            return;
        }
    }, [data, navigate]);

    return (
        <div className="flex">
            <Sidebar isAdmin={true} />
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

export default Dashboard;
