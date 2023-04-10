import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import TablePagination from '@mui/material/TablePagination';
import { useEffect, useState } from 'react';
import * as React from 'react';
import { styled, alpha } from '@mui/material/styles';
import InputBase from '@mui/material/InputBase';
import SearchIcon from '@mui/icons-material/Search';
import Switch from '@mui/material/Switch';

import './ParentUserTable.css'

const ParentUserTable = ({ onUserSelected }) => {
  const [parentUsers, setParentUsers] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  // table pagination
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(25);

  const handleChangePage = (event, newPage) => {
    // update page when it is changed
      setPage(newPage);
      setSearchTerm("");
  };

  const handleChangeRowsPerPage = (event) => {
    // update rows per page when changing amount desired
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
    setSearchTerm("");
    };

    // Hometable data
    useEffect(() => {
        fetch(`https://localhost:7107/api/v1/data/hometable`)
            .then((response) => response.json())
            .then((json) => setParentUsers(json))
    }, []);


  if (loading) return <div>loading...</div>;

  if (error) return <div>{error.message}</div>;

  function handleTableRowClick(parentUser) {
    // call the on user selected prop method
      onUserSelected(parentUser); onUserSelected(parentUser);
    }

  // search bar save state
    const [searchTerm, setSearchTerm] = useState("");

  // adjsut parent user based on search 
    const filteredParentUsers = parentUsers.filter((parentUser) => {
        return parentUser.ParentUsername.toLowerCase().includes(searchTerm.toLowerCase());
    });

  // create a SearchBar component
    const Search = styled('div')(({ theme, tableWidth }) => ({
        position: 'relative',
        borderRadius: theme.shape.borderRadius,
        backgroundColor: '#FFFFFF',
        '&:hover': {
            backgroundColor: alpha(theme.palette.common.white, 0.25),
        },
        marginLeft: `calc(${tableWidth}px + ${theme.spacing(2)})`,
        width: '50%',
        [theme.breakpoints.up('sm')]: {
            marginLeft: `calc(${tableWidth}px + ${theme.spacing(60)})`,
            width: '50%',
        },
    }));

    // Searchbar Icon
    const SearchIconWrapper = styled('div')(({ theme }) => ({
        padding: theme.spacing(0, 2),
        height: '100%',
        position: 'absolute',
        pointerEvents: 'none',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
    }));

    const StyledInputBase = styled(InputBase)(({ theme }) => ({
        color: 'inherit',
        '& .MuiInputBase-input': {
            padding: theme.spacing(1, 1, 1, 0),
            paddingLeft: `calc(1em + ${theme.spacing(4)})`,
            transition: theme.transitions.create('width'),
            width: '100%',
            [theme.breakpoints.up('sm')]: {
                width: '20ch',
                '&:focus': {
                    width: '20ch',
                },
            },
        },
    }));

    return (
        <div>
            <div className = "user-nav">
            <div className="main-health-heading">
                Customer Health Dashboard
                </div>
            </div>
            <div className="table-container">
                {parentUsers.length > 0 ? ( // if there are parent users then display table
                    <>
                <Search>
                    <SearchIconWrapper>
                        <SearchIcon />
                    </SearchIconWrapper>
                    <StyledInputBase
                        placeholder="Search…"
                        inputProps={{ 'aria-label': 'search' }}
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                        onFocus={() => {
                            setHandleTableRowClick(null);
                            setInputFocus(true);
                        }}
                        autoFocus // add autoFocus prop here
                    />
                </Search>           
                <TableContainer className="parent-table" component={Paper}>
                    <Table size="small" aria-label="custom pagination table">
                        <TableHead>
                                <TableRow className="table-header" sx={{ padding: '10px', backgroundColor: '#B6D770' }}> 
                                <TableCell>
                                    Company                     
                                </TableCell>
                                <TableCell>
                                    Avg. Testimonials Sent
                                </TableCell>
                                <TableCell>
                                    Avg.Testimonials Completed
                                </TableCell>
                                <TableCell>
                                Avg. Surveys Sent
                                </TableCell>
                                <TableCell>
                                Avg. Surveys Completed
                                </TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {filteredParentUsers.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((parentUser) => (
                                <TableRow className="table-text"
                                    key={parentUser.ParentUsername}
                                    onClick={() => handleTableRowClick(parentUser)}
                                    sx={{ '&:hover': { backgroundColor: '#f5f5f5' } }}
                                >
                                    <TableCell>{parentUser.ParentUsername}</TableCell>  
                                    <TableCell>{parentUser.AVG_TR_SENT}</TableCell>  
                                    <TableCell>{parentUser.AVG_TR_COMPLETED}</TableCell>  
                                    <TableCell>{parentUser.AVG_SR_SENT}</TableCell>
                                    <TableCell>{parentUser.AVG_SR_COMPLETED}</TableCell>
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
                        </>
            ) : (
                <div>Loading...</div>
            )}
            </div>
        </div>
    )
};
            
export default ParentUserTable;
