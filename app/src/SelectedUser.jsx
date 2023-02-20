import { useEffect, useState } from 'react';
import Breadcrumbs from '@mui/material/Breadcrumbs';
import Link from '@mui/material/Link';
import './SelectedUser.css';

const SelectedUser = (props) => {

    const { parentUsers } = props;
    const [selectedUser, setSelectedUser] = useState(null);
   

    return (
        <div className="selected-user-container">
            <Breadcrumbs aria-label="breadcrumb">
                <Link color="inherit" href="/">
                    Home
                </Link>
                <Link color="inherit" href="/parentUsers">
                    {parentUsers}
                </Link>
                <Link color="text.primary" aria-current="page" href="/selectedUser">
                    Selected User
                </Link>
            </Breadcrumbs>
            <h3 className="customer-health-heading">Customer Health:</h3>
            <p className="parent-user-text">{parentUsers}</p>
            <table className="data-table">
                <thead>
                    <tr>
                        <th>Column 1</th>
                        <th>Column 2</th>
                        <th>Column 3</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Row 1</td>
                        <td>Row 1</td>
                        <td>Row 1</td>
                    </tr>
                    <tr>
                        <td>Row 2</td>
                        <td>Row 2</td>
                        <td>Row 2</td>
                    </tr>
                    <tr>
                        <td>Row 3</td>
                        <td>Row 3</td>
                        <td>Row 3</td>
                    </tr>
                </tbody>
            </table>

        </div>

    );
}

export default SelectedUser;