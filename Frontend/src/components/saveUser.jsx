import { useState } from 'react';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { useSelector, useDispatch } from 'react-redux';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { getUser } from '@/store/Users/actions';
import {getAllUser} from '@/services/Users/api';
import {saveUser} from '@/services/Users/api';

const SaveUser = () => {
    const [userData, setUserData] = useState({
        name: '',
        email: '',
        contact: '',
        password: ''
    });

    const data = useSelector((state) => state.Userdata);
    const dispatch = useDispatch();
    const users = useSelector((state) => state.Users)

    const handleUserSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await saveUser(data[0].token, userData);
    
            const result = await getAllUser(data[0].token);
            dispatch(getUser(result.data));
        } catch (error) {
            console.error("Error saving user:", error);
            const result = await getAllUser(data[0].token);
            dispatch(getUser(result.data));
        } finally {
            setUserData({
                name: '',
                email: '',
                contact: '',
                password: ''
            });
        }
    };
    

    return (
        <>
            <Card>
                <CardHeader>
                    <CardTitle>Add New User</CardTitle>
                </CardHeader>
                <CardContent>
                    <form onSubmit={handleUserSubmit} className="space-y-4">
                        <Input
                            placeholder="Name"
                            value={userData.name}
                            onChange={(e) => setUserData({ ...userData, name: e.target.value })}
                        />
                        <Input
                            placeholder="Password"
                            value={userData.password}
                            onChange={(e) => setUserData({ ...userData, password: e.target.value })}
                        />
                        <Input
                            placeholder="Email"
                            value={userData.email}
                            onChange={(e) => setUserData({ ...userData, email: e.target.value })}
                        />
                        <Input
                            placeholder="Contact"
                            value={userData.contact}
                            onChange={(e) => setUserData({ ...userData, contact: e.target.value })}
                        />
                        <Button type="submit">Add User</Button>
                    </form>
                </CardContent>
            </Card>
        </>
    );
};

export default SaveUser;