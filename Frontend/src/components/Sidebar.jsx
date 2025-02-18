import React from 'react';
import { useNavigate } from 'react-router-dom';
import { FaUsers, FaUserPlus, FaUsersCog, FaList, FaBriefcase, 
         FaUserTie, FaCalendarPlus, FaComments, FaFile, FaPlus } from 'react-icons/fa';

const Sidebar = ({ isAdmin = false }) => {
    const navigate = useNavigate();

    const adminMenuItems = [
        { icon: <FaUsers className="w-5 h-5" />, label: 'Show Users', path: '/dashboard/users' },
        { icon: <FaUserPlus className="w-5 h-5" />, label: 'Add User', path: '/dashboard/add-user' },
        { icon: <FaUsersCog className="w-5 h-5" />, label: 'Show User Roles', path: '/dashboard/user-roles' },
        { icon: <FaList className="w-5 h-5" />, label: 'Show Skills', path: '/dashboard/skills' },
        { icon: <FaPlus className="w-5 h-5" />, label: 'Add Skill', path: '/dashboard/add-skill' },
        { icon: <FaBriefcase className="w-5 h-5" />, label: 'Show Jobs', path: '/dashboard/jobs' },
        { icon: <FaPlus className="w-5 h-5" />, label: 'Add Jobs', path: '/dashboard/add-job' },
        { icon: <FaUserTie className="w-5 h-5" />, label: 'Show Candidates', path: '/dashboard/candidates' },
        { icon: <FaPlus className="w-5 h-5" />, label: 'Add Candidate', path: '/dashboard/add-candidate' },
        { icon: <FaList className="w-5 h-5" />, label: 'Application Status', path: '/dashboard/application-status' },
        { icon: <FaCalendarPlus className="w-5 h-5" />, label: 'Show Interviews', path: '/dashboard/interviews' },
        { icon: <FaPlus className="w-5 h-5" />, label: 'Add Interview', path: '/dashboard/add-interview' },
        { icon: <FaComments className="w-5 h-5" />, label: 'Show Feedback', path: '/dashboard/feedback' },
        { icon: <FaPlus className="w-5 h-5" />, label: 'Add Feedback', path: '/dashboard/add-feedback' },
        { icon: <FaFile className="w-5 h-5" />, label: 'Show Documents', path: '/dashboard/documents' }
    ];

    const candidateMenuItems = [
        { icon: <FaUserTie className="w-5 h-5" />, label: 'Profile', path: '/candidate/profile' },
        { icon: <FaBriefcase className="w-5 h-5" />, label: 'Job Listings', path: '/candidate/jobs' },
        { icon: <FaFile className="w-5 h-5" />, label: 'Documents', path: '/candidate/documents' }
    ];

    const menuItems = isAdmin ? adminMenuItems : candidateMenuItems;

    return (
        <div className="fixed left-0 top-0 h-screen w-64 bg-gray-800 text-white p-4 overflow-y-auto">
            <div className="text-2xl font-bold mb-8">
                {isAdmin ? 'Admin Portal' : 'Candidate Portal'}
            </div>
            <nav>
                <ul className="space-y-2">
                    {menuItems.map((item) => (
                        <li key={item.path}>
                            <button
                                onClick={() => navigate(item.path)}
                                className="flex items-center space-x-3 w-full p-3 rounded-lg hover:bg-gray-700 transition-colors"
                            >
                                {item.icon}
                                <span>{item.label}</span>
                            </button>
                        </li>
                    ))}
                </ul>
            </nav>
        </div>
    );
};

export default Sidebar; 