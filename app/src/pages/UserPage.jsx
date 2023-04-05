import Breadcrumbs from '@mui/material/Breadcrumbs';
import Typography from '@mui/material/Typography';
import Link from '@mui/material/Link';
import { useEffect, useState } from 'react';
import React from 'react';
import { Card, CardContent, CardMedia } from '@mui/material';
import { FaRegStar, FaRegPaperPlane, FaRegCommentDots } from 'react-icons/fa';


import './UserPage.css';
import TestimonialGraph from './TestimonialGraph.jsx';
import SurveyGraph from './SurveyGraph.jsx';



export default function UserPage({ user, onClearUser }) {
    const [testimonialCount, setTestimonialCount] = useState([]);
    const [surveyCount, setSurveyCount] = useState([]);
    const [averageRating, setAverageRating] = useState([]);
    const [userID, setUserID] = useState(user.UserID);

    const [surveyGraph, setSurveyGraph] = useState([]);
    const [testimonialGraph, setTestimonialGraph] = useState([]);

    console.log(userID);
   
    // Total Testimonals Count
    useEffect(() => {
        fetch(`https://localhost:7107/api/v1/data/${userID}/testimonialcount`)
            .then((response) => response.text())
            .then((text) => setTestimonialCount(parseInt(text) || 0))
    }, [userID]);

    // Total Surveys Count
    useEffect(() => {
        fetch(`https://localhost:7107/api/v1/data/${userID}/surveycount`)
            .then((response) => response.text())
            .then((text) => setSurveyCount(parseInt(text) || 0))
    }, [userID]);

    // Average Star Rating
    useEffect(() => {
        fetch(`https://localhost:7107/api/v1/data/${userID}/averagerating`)
            .then((response) => response.text())
            .then((text) => setAverageRating(parseInt(text) || 0))
    }, [userID]);

    // Survey graph
    useEffect(() => {
        fetch(`https://localhost:7107/api/v1/data/${userID}/surveygraph`)
            .then((response) => response.json())
            .then((json) => setSurveyGraph(json) || 0)
    }, [userID]);
    
    // Testimonial graph
    useEffect(() => {
        fetch(`https://localhost:7107/api/v1/data/${userID}/testimonialgraph`)
            .then((response) => response.json())
            .then((json) => setTestimonialGraph(json) || 0)
    }, [userID]);

  const handleHomeClick = (event) => {
    event.preventDefault();
    onClearUser();
    };

    return (
        <div>
        <div style={{ display: 'flex', flexDirection: 'column', gap: '1rem' }}>
                {/* Breadcrumbs */}
                <div  className = "user-nav">
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
                <span>{user.UserID} </span>
                    </div>
                </div>
            {/* Stats Cards */}
          <div  className="card-container" >
                    <Card className="card" style={{ borderRadius: '20px', backgroundColor: 'rgb(249, 249, 249)' }}>
                        <CardContent className="content" style={{}}>
                            <Typography variant="h1" className="card-info">
                                <FaRegCommentDots size={60} />
                            </Typography>
                        <Typography variant="h1" className="card-info">
                                {testimonialCount}
                        </Typography>
                        <Typography variant="h5" component="h2" className="title">
                            Total Testimonials
                        </Typography>
                    </CardContent>
                </Card>
                    <Card className="card" style={{ borderRadius: '20px', backgroundColor: 'rgb(249, 249, 249)' }}>
                        <CardContent className="content" style={{}}>
                            <Typography variant="h1" className="card-info">
                                <FaRegStar size={60} />
                            </Typography>
                        <Typography variant="h1" className="card-info">
                            {averageRating}/5
                        </Typography>
                        <Typography variant="h5" component="h2" className="title">
                            Average Star Rating
                        </Typography>
                    </CardContent>
                </Card>
                    <Card className="card" style={{ borderRadius: '20px', backgroundColor: 'rgb(249, 249, 249)' }}>
                        <CardContent className="content" style={{}}>
                            <Typography variant="h1" className="card-info">
                                <FaRegPaperPlane size={60} />
                            </Typography>
                        <Typography variant="h1" className="card-info">
                                {surveyCount}  
                        </Typography>
                        <Typography variant="h5" component="h2" className="title">
                            Total Surveys
                        </Typography>
                    </CardContent>
                    </Card>
                </div>
          
                <div className="test-graph">
                    <div className="testimonial-graph">
                        <h1>Testimonials This Year</h1>
                    <TestimonialGraph
                        testimonial={testimonialGraph}
                        />
                    </div>
                    {surveyGraph && surveyGraph.length ? ( // if no data,skip, else show graph
                        <div className="survey-graph">
                            <h1>Surveys This Year</h1>
                            <SurveyGraph survey={surveyGraph} />
                        </div>
                    ) : null}
                  
                </div>
            </div>
        </div>
  );
}
