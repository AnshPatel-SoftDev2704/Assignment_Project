import React, { useEffect, useState } from 'react';
import { Bell } from 'lucide-react';
import { 
  fetchUserNotification, 
  fetchCandidateNotification, 
  updateUserNotification, 
  updateCandiateNotification 
} from '@/services/Notification/api';
import { 
  Card,
  CardContent
} from '@/components/ui/card';
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { useSelector } from 'react-redux';

const NotificationComponent = () => {
  const [notifications, setNotifications] = useState([]);
  const [unreadCount, setUnreadCount] = useState(0);
  const userData = useSelector((state) => state.Userdata[0]);
  
  const fetchNotifications = async () => {
    try {
      let response;
      if (userData.role === 'Candidate') {
        response = await fetchCandidateNotification(userData.token);
      } else {
        response = await fetchUserNotification(userData.token);
      }
      
      if (response?.data) {
        let filteredNotifications;
        if(userData.role === "Candidate") {
          filteredNotifications = response.data
            .filter(res => res.candidate_id === userData.user_id && !res.status)
            .sort((a, b) => new Date(b.created_at) - new Date(a.created_at));
        } else {
          filteredNotifications = response.data
            .filter(res => res.user_id === userData.user_id && !res.status)
            .sort((a, b) => new Date(b.created_at) - new Date(a.created_at));
        }
        setNotifications(filteredNotifications);
        setUnreadCount(filteredNotifications.length);
      }
    } catch (error) {
      console.error('Error fetching notifications:', error);
    }
  };

  useEffect(() => {
    fetchNotifications();
    const intervalId = setInterval(fetchNotifications, 30000);
    
    return () => clearInterval(intervalId);
  }, [userData]);

  const handleMarkAsRead = async (notification) => {
    try {
      const updateData = { ...notification, status: true };
      if (userData.role === 'Candidate') {
        await updateCandiateNotification(
          userData.token,
          notification.notifications_Candidates_id,
          updateData
        );
      } else {
        await updateUserNotification(
          userData.token,
          notification.notifications_Users_id,
          updateData
        );
      }
      let updatedNotifications;
      if(userData.role === "Candidate")
      {
       updatedNotifications = notifications.filter(
        n => n.notifications_Candidates_id !== notification.notifications_Candidates_id
      );
    }
    else
    {
      updatedNotifications = notifications.filter(
        n => n.notifications_Users_id !== notification.notifications_Users_id);
    }
      setNotifications(updatedNotifications);
      setUnreadCount(updatedNotifications.length);
    } catch (error) {
      console.error('Error marking notification as read:', error);
    }
  };

  return (
    <div className="relative">
      <Popover>
        <PopoverTrigger>
          <div className="relative p-2">
            <Bell className="h-6 w-6" />
            {unreadCount > 0 && (
              <div className="absolute -top-1 -right-1 bg-red-500 text-white rounded-full w-5 h-5 flex items-center justify-center text-xs">
                {unreadCount}
              </div>
            )}
          </div>
        </PopoverTrigger>
        <PopoverContent className="w-80 max-h-96 overflow-y-auto p-0">
          {notifications.length > 0 ? (
            notifications.map((notification) => (
              <Card 
                key={userData.role === 'Candidate' ? 
                  notification.notifications_Candidates_id : 
                  notification.notifications_Users_id}
                className="mb-2 bg-blue-50 hover:bg-blue-100 transition-colors duration-200"
              >
                <CardContent className="p-4">
                  <p className="text-sm">{notification.message}</p>
                  <div className="flex justify-between items-center mt-2">
                    <span className="text-xs text-gray-500">
                      {new Date(notification.created_at).toLocaleString()}
                    </span>
                    <button
                      onClick={() => handleMarkAsRead(notification)}
                      className="text-xs text-blue-600 hover:text-blue-800"
                    >
                      Mark as read
                    </button>
                  </div>
                </CardContent>
              </Card>
            ))
          ) : (
            <div className="p-4 text-center text-gray-500">
              No notifications
            </div>
          )}
        </PopoverContent>
      </Popover>
    </div>
  );
};

export default NotificationComponent;