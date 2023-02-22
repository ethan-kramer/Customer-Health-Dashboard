import Breadcrumbs from '@mui/material/Breadcrumbs';
import Typography from '@mui/material/Typography';
import Link from '@mui/material/Link';

import './UserPage.css';
import { Card, CardContent } from '@mui/material';

export default function UserPage({ user, onClearUser }) {
  const handleHomeClick = (event) => {
    event.preventDefault();
    onClearUser();
  };

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: '1rem' }}>
      <div className="customer-health-heading">
        <span style={{ fontWeight: 'bold', fontSize: '2rem' }}>{user.username}</span>
      </div>
      <Breadcrumbs
        aria-label="breadcrumb"
        className="my-breadcrumbs"
        separator={
          <Typography variant="body2" color="textSecondary">
            {'>'}
          </Typography>
        }
      >
        <Link color="inherit" href="/" onClick={handleHomeClick}>
          Home
        </Link>
        <Link color="inherit" href="/parentUsers">
          {user.username}
        </Link>
      </Breadcrumbs>
      <div style={{ display: 'flex', flexDirection: 'row', gap: '1rem' }}>
        <Card>
          <CardContent>
            <table className="data-table left-table">
              <thead>
                <tr>
                  <th colSpan="100%" className="transparent">
                    <span style={{ fontWeight: 'bold', fontSize: '1.5rem' }}>Avg. Star Rating</span>
                  </th>
                </tr>
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
          </CardContent>
        </Card>
        <Card>
          <CardContent>
            <h2>{"Last Week's Reviews"}</h2>
            <div className="circle"></div>
          </CardContent>
        </Card>
      </div>
    </div>
  );
}
