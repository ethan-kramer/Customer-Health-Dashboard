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


import './App.css';

function App() {
    const [parentUsers, setParentUsers] = useState([]); 
    const [selectedUserId, setSelectedUserId] = useState(null);
   const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);


  // Fetching data from API
  useEffect(() => {
    setLoading(true);
    fetch('https://localhost:7107/api/v1/parentusers')
      .then((response) => response.json())
      .then((json) => setParentUsers(json))
      .catch((error) => setError(error))
          .finally(() => setLoading(false));
  }, []); // empty dependency array bc need effect to run once for fetching API data


    function handleChange(event) { 
        setSelectedUserId(event.userId);
        console.log(event.userId);
    }

  const renderContent = () => {
      if (loading) return <div>loading...</div>;
      if (error) return <div>{error.message}</div>;
      return (
        <TableContainer component={Paper }>
              <Table sx={{minWidth: 650}}  size="small" aria-label="a dense table">
                  <TableHead >
                      <TableRow sx={{ backgroundColor:"#B6D770" }}>
                          <TableCell>Company</TableCell>
                    <TableCell>Current Week Surveys</TableCell>
                    <TableCell>Last Week Surveys</TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {parentUsers.map((parentUser) => ( // calls function on every element in original array
                    <TableRow
                        key={parentUser.userId} onClick={() => handleChange(parentUser)}>
                        <TableCell>{parentUser.username}</TableCell>
                    </TableRow>
                ))}
            </TableBody>
              </Table>
          </TableContainer>
    );
  };

  return <div className="App">{renderContent()}</div>;
}

export default App;
