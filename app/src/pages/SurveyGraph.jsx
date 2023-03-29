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

    console.log(survey);
    const surveyData = Array.from({ length: 53 }, (_, i) => {
        const index = i + 1;
        if (survey[index]) { // if value exists
            return {
                RequestsSent: survey[i].RequestsSent || 0,
                RequestsCompleted: survey[i].RequestsCompleted || 0,
                Year: survey[i].Year || 0,
                Week: survey[i].Week || 0
            }; // return those elememts in array
        } else {
            return { RequestsSent: 0, RequestsCompleted: 0, Week: 0 }; // else return 0
        }
    });

    const week = survey.map(item => ({ Year: item.Year, Week: item.Week }));
    const labels = week.map((week, index) => `Week ${week.Week}, ${week.Year}`);

    const data = {
        labels,
        datasets: [
            {
                label: 'Requests Sent',
                data: surveyData.map((week) => week.RequestsSent),
                backgroundColor: 'aqua',
                borderColor: 'black',
                borderWidth: 1,
            },
            {
                label: 'Requests Completed',
                data: surveyData.map((week) => week.RequestsCompleted),
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