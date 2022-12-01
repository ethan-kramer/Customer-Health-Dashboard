import * as React from 'react';
import { useEffect, useState } from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';

import './App.css';

function App() {
  const [parentUsers, setParentUsers] = useState(null);
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
  }, []);

  const renderContent = () => {
    if (loading) return <div>loading...</div>;

    if (error) return <div>{error.message}</div>;

    return (
      <div style={{ width: '100%', textAlign: 'left' }}>
        <TableContainer component={Paper}>
          <Table sx={{ minWidth: 650 }} aria-label="simple table">
            <TableHead>
              <TableRow>
                <TableCell>UserID</TableCell>
                <TableCell align="right">Username</TableCell>
                <TableCell align="right">Name</TableCell>
                <TableCell align="right">Company</TableCell>
                <TableCell align="right">Parent User</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              <TableRow>
                <TableCell component="th" scope="row"></TableCell>
                <TableCell align="right">{}</TableCell>
                <TableCell align="right">{}</TableCell>
                <TableCell align="right">{}</TableCell>
                <TableCell align="right">{}</TableCell>
              </TableRow>
            </TableBody>
          </Table>
        </TableContainer>
        <code>
          <pre>{JSON.stringify(parentUsers, undefined, 2)}</pre>
        </code>
      </div>
    );
  };

  return <div className="App">{renderContent()}</div>;
}

export default App;
