import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'
import './index.css'
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
        <App name="Testimonial" />
  </React.StrictMode>
)
