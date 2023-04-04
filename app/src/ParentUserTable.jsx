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

  // Toggle
    const [excludeZeros, setExcludeZeros] = useState(false);
    const [buttonText, setButtonText] = useState('Exclude 0');

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

    function fetchData(toggleIsOn) {
        const url = 'https://localhost:7107/api/v1/data/hometable';
        let queryString = '';

        if (toggleIsOn) { // check if true
            queryString = '?excludeZeros=true';

            fetch(url + queryString) // then fetch completion > 0
                .then(response => response.json())
                .then((json) => setParentUsers(json));
        }
        else { // otherwise fetch all
            fetch(url)
                .then(response => response.json())
                .then((json) => setParentUsers(json));
        }
    }

    useEffect(() => { // initialized to false (zeros show)
        fetchData(excludeZeros);
    }, [excludeZeros]);


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
        return parentUser.UserID.toLowerCase().includes(searchTerm.toLowerCase())
            || parentUser.AverageRequestsSent.toString().toLowerCase().includes(searchTerm.toLowerCase());
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
            <div className="toggle-button">
                <Switch defaultChecked 
            onClick={() => setExcludeZeros(!excludeZeros)}
                />
                {<span>Include Zeros</span>}
            </div>
           
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
                                    Completion Percentage
                                </TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {filteredParentUsers.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((parentUser) => (
                                <TableRow className="table-text"
                                    key={parentUser.UserID}
                                    onClick={() => handleTableRowClick(parentUser)}
                                    sx={{ '&:hover': { backgroundColor: '#f5f5f5' } }}
                                >
                                    <TableCell>{parentUser.UserID}</TableCell>  
                                    <TableCell>{parentUser.AverageRequestsSent}</TableCell>  
                                    <TableCell>{parentUser.AverageRequestsCompleted}</TableCell>  
                                    <TableCell>{(parentUser.CompletionPercentage * 100).toFixed(2)}%</TableCell>
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
