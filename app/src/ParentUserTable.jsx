import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import TablePagination from '@mui/material/TablePagination';
import { useEffect, useState } from 'react';

import './ParentUserTable.css'

const ParentUserTable = ({ onUserSelected }) => {
  const [parentUsers, setParentUsers] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  // table pagination
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(10);

  const handleChangePage = (event, newPage) => {
    // update page when it is changed
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    // update rows per page when changing amount desired
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  // Fetching data from API
  useEffect(() => {
    setLoading(true);
    fetch('https://localhost:7107/api/v1/parentusers')
      .then((response) => response.json())
      .then((json) => setParentUsers(json))
      .catch((error) => setError(error))
      .finally(() => setLoading(false));
  }, []); // empty dependency array bc need effect to run once for fetching API data

  if (loading) return <div>loading...</div>;

  if (error) return <div>{error.message}</div>;

  function handleTableRowClick(parentUser) {
    // call the on user selected prop method
    onUserSelected(parentUser);
  }

  // create a SearchBar component
  /*  const SearchBar = ({ value, onChange }) => {
              return (
                  <input
                      style={{ width: '50%', paddingBottom: '0.5rem' }}
                      type="text"
                      value={value}
                      placeholder="Search"
                      onChange={onChange}
                  />
              );
          };*/

  return (
      <div>
          <div className="customer-health-heading">
              <h1>Customer Health Dashboard</h1>
          </div>
          {parentUsers.length > 0 ? (
              <TableContainer className="parent-table" component={Paper}>
                  <Table size="small" aria-label="custom pagination table">
                      <TableHead>
                          <TableRow>
                              <TableCell>Company</TableCell>
                              <TableCell align="right">Surveys This Week</TableCell>
                          </TableRow>
                      </TableHead>
                      <TableBody>
                          {parentUsers
                              .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                              .map((parentUser) => (
                                  <TableRow
                                      key={parentUser.userId}
                                      onClick={() => handleTableRowClick(parentUser)}
                                  >
                                      <TableCell>{parentUser.username}</TableCell>
                                      <TableCell align="right">156</TableCell>
                                  </TableRow>
                              ))}
                      </TableBody>
                  </Table>
                  <TablePagination
                      component="div"
                      count={parentUsers.length}
                      page={page}
                      onPageChange={handleChangePage}
                      rowsPerPage={rowsPerPage}
                      onRowsPerPageChange={handleChangeRowsPerPage}
                  />
              </TableContainer>
          ) : (
              <div>No data</div>
          )}
      </div>
  );
};
export default ParentUserTable;
