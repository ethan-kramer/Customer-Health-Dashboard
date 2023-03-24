import Breadcrumbs from '@mui/material/Breadcrumbs';
import Typography from '@mui/material/Typography';
import Link from '@mui/material/Link';
import { useEffect, useState } from 'react';

import './UserPage.css';
import { Card, CardContent, CardMedia } from '@mui/material';

export default function UserPage({ user, onClearUser }) {
    const [testimonialCount, setTestimonialCount] = useState([]);
    const [weeklyTestimonialCount, setWeeklyTestimonialCount] = useState([]);
    const [averageRating, setAverageRating] = useState([]);
    const [userID, setUserID] = useState(user.UserID);

    console.log(userID);

    // Total Testimonals Count
    useEffect(() => {
        fetch(`https://localhost:7107/api/v1/data/${userID}/testimonialcount`)
            .then((response) => response.text())
            .then((text) => setTestimonialCount(parseInt(text) || 0))
    }, [userID]);

    // Weekly Testimonials Count -- In progress
    useEffect(() => {
        fetch(`https://localhost:7107/api/v1/data/${userID}/weeklytestimonial`)
            .then((response) => response.text())
            .then((text) =>  setWeeklyTestimonialCount(parseInt(text) || 0))
    }, [userID]);

    // Average Star Rating
    useEffect(() => {
        fetch(`https://localhost:7107/api/v1/data/${userID}/averagerating`)
            .then((response) => response.text())
            .then((text) => setAverageRating(parseInt(text) || 0))
    }, [userID]);

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
          {user.UserID}
                </Link>
          </Breadcrumbs>
            {/* Title */}
            <div className="customer-health-heading">
                <span>{user.UserID} Dashboard </span>
          </div>
            {/* Stats Cards */}
          <div style={{ display: 'flex' }}>
                <Card className="card" style={{ borderRadius: '20px', backgroundColor: '#0080001c' }}>
                    <CardContent className="content" style={{} }>
                        <Typography variant="h1" className="card-info">
                            {testimonialCount}
                        </Typography>
                        <Typography variant="h5" component="h2" className="title">
                            Total Testimonials
                        </Typography>
                    </CardContent>
                </Card>

                <Card className="card" style={{ borderRadius: '20px',backgroundColor: '#f9f9f9' }}>
                    <CardContent className="content" style={{ }}>
                        <Typography variant="h1" className="card-info">
                            {averageRating}/5
                        </Typography>
                        <Typography variant="h5" component="h2" className="title">
                            Average Star Rating
                        </Typography>
                    </CardContent>
                </Card>

                <Card className="card" style={{ borderRadius: '20px', backgroundColor: '#f100001a' }}>
                    <CardContent className="content" style={{  }}>
                        <Typography variant="h1" className="card-info">
                            50%
                        </Typography>
                        <Typography variant="h5" component="h2" className="title">
                            Overall Health
                        </Typography>
                    </CardContent>
                </Card>

            </div>
            <div style={{ display: 'flex' }}>

                <Card className="chart-card" style={{ borderRadius: '50px', width: '55%', backgroundColor: '#f9f9f9' }}>
                    <CardMedia
                        className="media"
                        image=""
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

                <Card className="chart-card" style={{ borderRadius: '50px', width: '25%', backgroundColor: '#f9f9f9' }}>
                    <CardMedia
                        className="media"
                        image=""
                        title=""
                    />
                    <CardContent className="content">
                        <Typography variant="h5" component="h3" className="title">
                            Another Chart
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
