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

//{
//   parentUsers.map(parentUser => {
//     return (
//       <option onClickvalue="childTable()">{parentUser.userId}>{parentUser.username}</option>  // event based on userID
// )
//})
//}

function App() {
  // is everything supposed to be under app function?
  const [parentUsers, setParentUsers] = useState(null);
  // const [childUsers, setChildUsers] = useState(null);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);

  function childTable() {
    console.log('Selected parent');
  }

  // yarn format
  // Fetching data from API
  useEffect(() => {
    setLoading(true);
    fetch('https://localhost:7107/api/v1/parentusers')
      // fetch('https://localhost:7107/api/v1/parentusers/{parentUserId:INT}/childusers')
      .then((response) => response.json())
      .then((json) => setParentUsers(json))
      //  .then((json) => setChildUsers(json))
      .catch((error) => setError(error))
      .finally(() => setLoading(false));
  }, []);

  const renderContent = () => {
    if (loading) return <div>loading...</div>;

    if (error) return <div>{error.message}</div>;

    return (
      <div style={{ width: '100%', textAlign: 'left' }}>
        <select>
          {parentUsers.map((parentUser) => {
            // onchange event?
            return (
              <option value={parentUser.userId} onChange={childTable}>
                {parentUser.username}
              </option>
            );
          })}
        </select>
      </div>
    );
  };

  return <div className="App">{renderContent()}</div>;
}

export default App;
