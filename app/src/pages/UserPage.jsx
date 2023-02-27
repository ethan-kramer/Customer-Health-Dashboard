import Breadcrumbs from '@mui/material/Breadcrumbs';
import Typography from '@mui/material/Typography';
import Link from '@mui/material/Link';

import './UserPage.css';
import { Card, CardContent, CardMedia } from '@mui/material';

export default function UserPage({ user, onClearUser }) {
  const handleHomeClick = (event) => {
    event.preventDefault();
    onClearUser();
  };

    return (
        <div style={{ display: 'flex', flexDirection: 'column', gap: '1rem' }}>
            {/* Breadcrumbs */}
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
            {/* Title */}
          <div className="customer-health-heading">
              <span>{user.username} Dashboard</span>
          </div>
            {/* Stats Cards */}
          <div style={{ display: 'flex' }}>
                <Card className="card" style={{ backgroundColor: '#B6D770' }}>
                    <CardMedia
                        className="media"
                        image="https://picsum.photos/400/300"
                        title="75"
                        href=""
                    />
                  <CardContent className="content">
                      <Typography variant="h5" component="h2" className="title">
                          Average Star Rating
                      </Typography>
                      <Typography variant="subtitle1" className="subtitle">
                        
                      </Typography>
                      <Typography variant="body1" className="description">
                          
                      </Typography>
                    </CardContent>
           
              </Card>

                <Card className="card" style={{ backgroundColor: '#B6D770' }}>
                    <CardMedia
                        className="media"
                        image="https://picsum.photos/500/300"
                        title=""
                    />
                  <CardContent className="content">
                      <Typography variant="h5" component="h2" className="title">
                         Review's Last Week
                      </Typography>
                      <Typography variant="subtitle1" className="subtitle">
                         
                      </Typography>
                      <Typography variant="body1" className="description">
                          
                      </Typography>
                    </CardContent>
              </Card>
                <Card className="card" style={{ backgroundColor: '#B6D770' }}>
                    <CardMedia
                        className="media"
                        image="https://picsum.photos/600/300"
                        title=""
                    />
                  <CardContent className="content">
                      <Typography variant="h5" component="h3" className="title">
                          Overall Trend
                      </Typography>
                      <Typography variant="subtitle1" className="subtitle">
                        
                      </Typography>
                      <Typography variant="body1" className="description">
                         
                      </Typography>
                    </CardContent>
                
              </Card>
          </div>
</div>
  );
}
