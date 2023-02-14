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
    const [ parentUsers, setParentUsers ] = useState([]);
    const [selectedUserId, setSelectedUserId] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    // table pagination
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(5);

    const [searchValue, setSearchValue] = useState(''); 



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


    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(event.target.value);
        setPage(0);
    };

    const handleSearch = (event) => {
        setSearchValue(event.target.value);
        setPage(0);
    };

        return (
            <div>
               
                < TableContainer component={Paper} >
                    <Table sx={{ minWidth: 650 }} size="small" aria-label="custom pagination table">
                        <TableHead >
                            <TableRow sx={{ backgroundColor: "#B6D770" }}>
                                <TableCell>Company</TableCell>
                                <TableCell>Current Week Surveys</TableCell>
                                <TableCell>Last Week Surveys</TableCell>
                                {/* Search input */}
                                <input style={{ width: 160 }} type="text"  value={searchValue} onChange={handleSearch} />
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {parentUsers.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((parentUser) => (
                                <TableRow
                                    key={parentUser.userId} onClick={() => sendInfo(parentUser)}>
                                    <TableCell>{parentUser.username}</TableCell>
                                    <TableCell style={{ width: 160 }} align="right">156</TableCell>
                                    <TableCell style={{ width: 160 }} align="right">126</TableCell>
                                </TableRow>

                            ))}
                        </TableBody>
                    </Table>
                    <TablePagination
                        rowsPerPageOptions={[5, 25, 100]}
                        component="div"
                        count={parentUsers.length}
                        rowsPerPage={rowsPerPage}
                        page={page}
                        onChangePage={handleChangePage}
                        onChangeRowsPerPage={handleChangeRowsPerPage}
                    />
                </TableContainer >
             
            </div>

        )

  
}
export default ParentUserTable;