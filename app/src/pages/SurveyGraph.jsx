import React from 'react';
import { Line } from 'react-chartjs-2'
import {
    Chart as ChartJS,
    BarElement,
    CategoryScale,
    LinearScale,
    Tooltip,
    PointElement,
    LineElement,
    LineController,
    Legend
} from 'chart.js';

ChartJS.register(
    BarElement,
    CategoryScale, // x axis weeks
    LinearScale, // y axis number of requests
    Tooltip,
    Legend,
    PointElement,
    LineElement,
    LineController
)

const SurveyGraph = ({ survey }) => {

    function getWeek() {
        const currentDate = new Date();
        const startDate = new Date(currentDate.getFullYear(), 0, 1);
        const days = Math.floor((currentDate - startDate) / (24 * 60 * 60 * 1000));
        const weekNumber = Math.ceil(days / 7);
        return weekNumber;
    }

    const currentWeek = getWeek();

    const weekData = Array.from({ length: 52 }, (_, i) => {
        // const index = i + 1;
        if (survey[i]) { // if value exists
            return {
                RequestsSent: survey[i].RequestsSent || 0,
                RequestsCompleted: survey[i].RequestsCompleted || 0,
                Year: survey[i].Year || 0,
                Week: survey[i].Week || 0
            }; // return those elememts in array
        } else {
            return { RequestsSent: 0, RequestsCompleted: 0, Week: i }; // else return 0
        }
    });

    const requestsCompleted = weekData.map((data) => data.RequestsCompleted);   
    const requestsSent = weekData.map((data) => data.RequestsSent);               
    const weeksCompleted = weekData.map((data) => data.Week);  

    const weeks = Array.from({ length: 53 }, (_, i) => (currentWeek + i > 52 ? currentWeek + i - 52 : currentWeek + i));
    const formatted_dictionary = [];
    for (let i = 1; i < weeks.length; i++) {
        const weekData = {
            RequestsSent: 0,
            RequestsCompleted: 0,
            Year: currentWeek - weeks[i] >= 0 ? new Date().getFullYear() : new Date().getFullYear() - 1,
            Week: weeks[i],
        };
        const index = weeksCompleted.indexOf(weekData.Week);
        if (index !== -1) {
            weekData.RequestsCompleted = requestsCompleted[index];
            weekData.RequestsSent = requestsSent[index];
        }
        formatted_dictionary.push(weekData);
    }

    console.log("survey data: ", formatted_dictionary);

    const week = formatted_dictionary.map(item => ({ Year: item.Year, Week: item.Week }));
    const labels = week.map((week, index) => `Week ${week.Week}, ${week.Year}`);

    const data = {
        labels,
        datasets: [
            {
                label: 'Requests Sent',
                data: formatted_dictionary.map((week) => week.RequestsSent),
                backgroundColor: 'aqua',
                borderColor: 'black',
                borderWidth: 1,
            },
            {
                label: 'Requests Completed',
                data: formatted_dictionary.map((week) => week.RequestsCompleted),
                backgroundColor: 'blue',
                borderColor: 'black',
                borderWidth: 1,
            }
        ]
    }

    const options = {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: true,
            title: {
                display: true,
                text: 'Title of the Graph',
                font: {
                    size: 18,
                    weight: 'bold'
                }
            },
            scales: {
                y: {
                    suggestedMin: 0,
                    suggestedMax: survey.length
                }
            },
            backgroundColor: 'white'

        }
    }

    return (
        <div>
            <Line
                data={data}
                options={options}
                style={{ height: "400px", width: "600px" }} 
            >
            </Line>
        </div>
    )
}

export default SurveyGraph