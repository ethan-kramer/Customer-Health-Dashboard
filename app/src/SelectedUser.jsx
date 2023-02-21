import { useEffect, useState } from 'react';
import Breadcrumbs from '@mui/material/Breadcrumbs';
import Link from '@mui/material/Link';
import './SelectedUser.css';
import Typography from '@mui/material/Typography';


const SelectedUser = (props) => {

    const { parentUsers } = props;
    const [selectedUser, setSelectedUser] = useState(null);
   

    return (
        <div>
            <div className="customer-health-heading">
                <h2>{parentUsers}</h2>
            </div>
            <div>
                <Breadcrumbs
                    aria-label="breadcrumb"
                    className="my-breadcrumbs"
                    separator={<Typography variant="body2" color="textSecondary">{'>'}</Typography>}>
                    <Link color="inherit" href="/">
                        Home
                    </Link>
                    <Link color="inherit" href="/parentUsers">
                        {parentUsers}
                    </Link>
                </Breadcrumbs>
            </div>
            <div class="container">
                <div class="selected-user-container">
                    <table class="data-table left-table">
                        <thead>
                            <h2>Avg. Star Rating</h2>
                            <tr>
                                <th>Column 1</th>
                                <th>Column 2</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Row 1</td>
                                <td>Row 1</td>
                            </tr>
                            <tr>
                                <td>Row 2</td>
                                <td>Row 2</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="selected-user-container">
                    <h2>Last Week's Reviews</h2>
                    <div class="circle"></div>

                </div>
            </div>
        </div>
    );

}

export default SelectedUser;