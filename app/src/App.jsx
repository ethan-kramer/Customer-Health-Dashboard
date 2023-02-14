import * as React from 'react';
import { useEffect, useState } from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import TablePagination from '@mui/material/TablePagination';
import ParentUserTable from './ParentUserTable';
import SelectedUser from './SelectedUser';


import './App.css';

function App() {
    const [parentUsers, setParentUsers] = useState([]);
    const [selectParentUser, setSelectedParentUser] = useState(null);

    const receiveParentUserData = (data) => { // data from child component
        setParentUsers(data);
        setSelectedParentUser(data);
        console.log(data);
    }

    const renderContent = () => {

        if (selectParentUser != null) { // if user selected, render new component
            return (
                <div>
                    <SelectedUser
                        parentUsers={parentUsers} />
                </div>
            )
        }

        return (
            <div>
                <ParentUserTable // return main parent users table
                    sendInfo={receiveParentUserData}
                    parentUsers={parentUsers}
                    parentUserSelected={(parentUsers) => setSelectedParentUser(parentUsers)}>
                </ParentUserTable>
            </div>
        );
  };

  return <div className="App">{renderContent()}</div>;
}

export default App;
