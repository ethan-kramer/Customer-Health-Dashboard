import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import TablePagination from '@mui/material/TablePagination';
import { useEffect, useState } from 'react';

const ParentUserTable = (props) => {
    const [parentUsers, setParentUsers] = useState([]);
    const [selectedUserId, setSelectedUserId] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    // search bar
    const [searchValue, setSearchValue] = useState('');

    // table pagination
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(10);

    const handleChangePage = (event, newPage) => { // update page when it is changed
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => { // update rows per page when changing amount desired
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

    function sendInfo(parentUser) { // send username info to parent component
        props.sendInfo(parentUser.username);
    };


      


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
            {parentUsers.length > 0 ? ( // if there are parent users then display table

                < TableContainer component={Paper} >
                    <Table sx={{ minWidth: 650 }} size="small" aria-label="custom pagination table">
                      
                        <TableHead >
                            <TableRow sx={{ backgroundColor: "#B6D770" }}>
                                <TableCell sx={{ fontWeight: 'bold', fontSize: 16, color: '#555', textTransform: 'uppercase' }}>Company</TableCell>
                                <TableCell sx={{
                                    fontWeight: 'bold', fontSize: 16, color: '#555', textTransform: 'uppercase'}} align="right">Surveys This Week</TableCell>
                                {/* Search input  <input style={{ width: 160 }} type="text"  value={searchValue} onChange={handleSearch} /> */}
                            
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {parentUsers.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((parentUser) => (
                                <TableRow
                                    key={parentUser.userId}
                                    onClick={() => sendInfo(parentUser)}
                                    sx={{ '&:hover': { backgroundColor: '#f5f5f5' } }}>
                                    <TableCell>{parentUser.username}</TableCell>
                                    <TableCell sx={{ paddingLeft: '3rem' }} align="right"> 156
                                    </TableCell>
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
                </TableContainer >
            ) : (
                <div>No data</div>
            )}


        </div>

    )


}
export default ParentUserTable;